using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AnalysisServices;

namespace OlapSystem.Management.OlapDb
{
    public abstract class DynamicDimensionManager
    {
        private DatabaseManager _dbManager;

        public abstract string SourceTableName { get;}
        public abstract string DimensionName { get;}

        public DynamicDimensionManager(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        public DatabaseManager DatabaseManager
        {
            get { return _dbManager; }
        }

        public void CheckAndUpdateDimension()
        {
            // remove invalid Attributes
            RemoveInvalidCustomAttributes();

            // create missing Attributes
            CreateMissingCustomAttributes();
        }


        private DataTable GetCustomDataSourceColumns()
        {
            SqlConnection conn = this.DatabaseManager.GetDataSourceConnection();
            conn.Open();

            // datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("COLUMN_NAME", typeof(string));
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };

            string sql = string.Format(@"
SELECT COLUMN_NAME
from INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_CATALOG=(SELECT DB_NAME()) AND TABLE_SCHEMA='spp' AND TABLE_NAME='{0}'
AND LEFT(COLUMN_NAME, 6)='GRP@#@' ",
                 this.SourceTableName);
            SqlDataAdapter ad = new SqlDataAdapter(sql, conn);

            try
            {
                // get columns
                ad.Fill(dt);
            }
            finally
            {
                ad.Dispose();
                conn.Close();
            }

            return dt;
        }

        private void CreateMissingCustomAttributes()
        {
            // get existing custom columns
            DataTable dataColumns = GetCustomDataSourceColumns();
            foreach (DataRow r in dataColumns.Rows)
                CreateCustomAttribute(r[0].ToString());
        }


        private void RemoveInvalidCustomAttributes()
        {
            bool update = false;
            Database db = this.DatabaseManager.AdomdDatabase;

            // get the Store dimension
            Dimension dim = db.Dimensions.FindByName(this.DimensionName);
            if (dim == null)
                throw new Exception("Dimension not found: " + this.DimensionName);

            // get existing custom columns
            DataTable dataColumns = GetCustomDataSourceColumns();

            // check and remove
            for (int i = dim.Attributes.Count - 1; i >= 0; i--)
            {
                DimensionAttribute attr = dim.Attributes[i];

                // custom attribute consists of 1 column
                if (attr.KeyColumns.Count != 1)
                    continue;

                // get key column info
                DataItem di = attr.KeyColumns[0];
                string tableId = ((ColumnBinding)di.Source).TableID;
                string colName = ((ColumnBinding)di.Source).ColumnID;

                // check if it's not custom
                if (!colName.StartsWith("GRP@#@"))
                    continue;

                // check if valid
                if (dataColumns.Rows.Find(colName) != null)
                    continue;

                // remove datasourceview column
                if (db.DataSourceViews[0].Schema.Tables[tableId].Columns.Contains(colName))
                {
                    db.DataSourceViews[0].Schema.Tables[tableId].Columns.Remove(colName);
                    update = true;
                }

                // remove hierarchies that contain this attribute
                for (int j = dim.Hierarchies.Count - 1; j >= 0; j--)
                {
                    foreach (Level lev in dim.Hierarchies[j].Levels)
                        if (lev.SourceAttribute == attr)
                        {
                            dim.Hierarchies.Remove(dim.Hierarchies[j]);
                            update = true;
                            j--;
                            break; // level loop
                        }
                }
                // remove attribute
                if (dim.Attributes.Contains(attr))
                {

                    dim.Attributes.Remove(attr);
                    update = true;
                    i--; // attribute loop
                }
            }

            if (update)
            {
                // update datasource
                db.DataSourceViews[0].Update(UpdateOptions.ExpandFull | UpdateOptions.AlterDependents);

                // update dimension
                dim.Update(UpdateOptions.ExpandFull | UpdateOptions.AlterDependents);
            }
        }

        private void CreateCustomAttribute(string colName)
        {
            bool update = false;
            Database db = this.DatabaseManager.AdomdDatabase;

            // get column caption
            if (colName == null || colName == string.Empty)
                throw new ArgumentException("Empty columnName");
            string attrCaption = (colName.StartsWith("GRP@#@") ? colName.Substring(6) : colName) + " Attr";
            string hierName = (colName.StartsWith("GRP@#@") ? colName.Substring(6) : colName);            

            // get the Store dimension
            Dimension dim = db.Dimensions.FindByName(this.DimensionName);
            if (dim == null)
                throw new Exception("Dimension not found: " + this.DimensionName);

            // get key column info
            DataItem di = dim.KeyAttribute.KeyColumns[0];
            string tableId = ((ColumnBinding)di.Source).TableID;

            // change datasource 
            if (!db.DataSourceViews[0].Schema.Tables[tableId].Columns.Contains(colName))
            {
                db.DataSourceViews[0].Schema.Tables[tableId].Columns.Add(colName, typeof(string));
                db.DataSourceViews[0].Update(UpdateOptions.ExpandFull | UpdateOptions.AlterDependents);
            }

            // find or create attribute
            DimensionAttribute attr = dim.Attributes.FindByName(attrCaption);
            if (attr == null)
            {
                attr = dim.Attributes.Add(attrCaption);
                attr.Usage = AttributeUsage.Regular;
                attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], tableId, colName));
                attr.NameColumn = CreateDataItem(db.DataSourceViews[0], tableId, colName);
                attr.AttributeHierarchyVisible = false;
                attr.MemberNamesUnique = false;

                update = true;
            }


            // find or create hiearachy                
            Hierarchy hier = dim.Hierarchies.FindByName(hierName);
            if (hier == null)
            {
                hier = dim.Hierarchies.Add(hierName);
                hier.MemberKeysUnique = MemberKeysUnique.NotUnique;
                hier.Levels.Add(attrCaption.Replace(" Attr", "")).SourceAttributeID = attr.ID;
                hier.Levels.Add(dim.KeyAttribute.Name.Replace(" Attr", "")).SourceAttributeID = dim.KeyAttribute.ID;                

                update = true;
            }

            // update
            if(update)
                dim.Update(UpdateOptions.ExpandFull | UpdateOptions.AlterDependents);
        }


        private DataItem CreateDataItem(DataSourceView dsv, string tableName, string columnName)
        {
            DataTable dataTable = ((DataSourceView)dsv).Schema.Tables[tableName];
            DataColumn dataColumn = dataTable.Columns[columnName];
            return new DataItem(tableName, columnName,
                OleDbTypeConverter.GetRestrictedOleDbType(dataColumn.DataType));
        }




    }

}
