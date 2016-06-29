using System;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.AnalysisServices.AdomdClient;


namespace OlapSystem.Migration
{
    public class ReportValidator
    {        
        ReportsValidation _reports = null;
        SourceSchemaMap _map;
        DestSchema _destSchema = new DestSchema();
        CubeDef _srcCube;
        CubeDef _destCube;
        StringCollection _srcUnmatched = new StringCollection();
        StringCollection _destUnmatched = new StringCollection();

        Hashtable _cchIndex;
        Hashtable _chnIndex;
        Hashtable _storeChnIndex;
        Hashtable _storePcodeIndex;
        Hashtable _timeMonthlyIndex;
        Hashtable _timeWeeklyIndex;

        public string DefaultMapFilePath
        {
            get { return ConfigurationManager.AppSettings["SchemaMapFilePath"]; }
        }

        public SourceSchemaMap.MapDataTable MapTable
        {
            get { return (_map==null ? null : _map.Map); }
        }

        public DestSchema.SchemaDataTable DestSchemaTable
        {
            get { return _destSchema.Schema; }
        }

        public ReportsValidation.OlapReportsDataTable ReportsTable
        {
            get { return (_reports==null ? null : _reports.OlapReports); }
        }

        public StringCollection UnmatchedObjects
        {
            get { return _destUnmatched;}
        }

        public void LoadAdomdObjects()
        {
            AdomdConnection srcConn = new AdomdConnection(ConfigurationManager.AppSettings["SrcOlapConnection"]);
            srcConn.Open();
            _srcCube = srcConn.Cubes[ConfigurationManager.AppSettings["SrcCube"]];

            AdomdConnection destConn = new AdomdConnection(ConfigurationManager.AppSettings["DestOlapConnection"]);
            destConn.Open();
            _destCube = destConn.Cubes[ConfigurationManager.AppSettings["DestCube"]];
        }

        public void CloseAdomdObjects()
        {
            if (_srcCube != null && _srcCube.ParentConnection != null)
                _srcCube.ParentConnection.Close(true);

            if (_destCube != null && _destCube.ParentConnection != null)
                _destCube.ParentConnection.Close(true);
        }

        public void LoadMap(string filePath)
        {
            if (_map != null)
                return;
            if(filePath==null || filePath=="")
                throw new ArgumentException("Invalid path");

            try
            {
                // load schema from file
                _map = new SourceSchemaMap();
                _map.BeginInit();
                if (File.Exists(filePath))
                    _map.ReadXml(filePath);                
                _map.AcceptChanges();

                // remove unmatched members from source schema
                for (int i = _map.Map.Rows.Count - 1; i >= 0; i--)
                {
                    if (SourceSchemaMap.LookupAdomdSchemaObject(_srcCube, _map.Map[i]) == null)
                        _map.Map.Rows.RemoveAt(i);
                }

                // add or validate existing memebrs
                foreach (Dimension dim in _srcCube.Dimensions)
                {
                    if (_map.Map.FindBySourceUniqueName("Dimension:" + dim.UniqueName) == null)
                        _map.Map.AddMapRow("Dimension:" + dim.UniqueName, "");

                    foreach (Hierarchy hier in dim.Hierarchies)
                    {
                        if (_map.Map.FindBySourceUniqueName("Hierarchy:" + hier.UniqueName) == null)
                            _map.Map.AddMapRow("Hierarchy:" + hier.UniqueName, "");

                        foreach (Level lev in hier.Levels)
                        {
                            if (_map.Map.FindBySourceUniqueName("Level:" + lev.UniqueName) == null)
                                _map.Map.AddMapRow("Level:" + lev.UniqueName, "");
                        }

                        // measures handled separately
                        if (dim.DimensionType != DimensionTypeEnum.Measure)
                        {
                            // default member or first member in first level
                            Member defMem = (Member)SourceSchemaMap.LookupAdomdSchemaObject(_srcCube, "Member", hier.DefaultMember);
                            if (defMem == null)
                                defMem = hier.Levels[0].GetMembers(0, 1)[0];
                            if (_map.Map.FindBySourceUniqueName("Member:" + defMem.UniqueName) == null)
                                _map.Map.AddMapRow("Member:" + defMem.UniqueName, "");

                            // calculated members
                            MemberCollection mems =
                                (hier.Levels[0].LevelType == LevelTypeEnum.All ?
                                hier.Levels[1].GetMembers() : hier.Levels[0].GetMembers());
                            foreach (Member mem in mems)
                            {
                                if (mem.Type != MemberTypeEnum.Formula)
                                    continue;

                                if (_map.Map.FindBySourceUniqueName("Member:" + mem.UniqueName) == null)
                                    _map.Map.AddMapRow("Member:" + mem.UniqueName, "");
                            }
                        }
                    }

                    // measures
                    if (dim.DimensionType == DimensionTypeEnum.Measure)
                        foreach (Measure mea in _srcCube.Measures)
                        {
                            if (_map.Map.FindBySourceUniqueName("Measure:" + mea.UniqueName) == null)
                                _map.Map.AddMapRow("Measure:" + mea.UniqueName, "");
                        }


                }

                // load dest schema
                _destSchema.BeginInit();

                // empty member
                _destSchema.Schema.AddSchemaRow("");

                // dimension hierarchies
                foreach (Dimension dim in _destCube.Dimensions)
                {
                    if (_destSchema.Schema.FindByUniqueName("Dimension:" + dim.UniqueName) == null)
                        _destSchema.Schema.AddSchemaRow("Dimension:" + dim.UniqueName);

                    foreach (Hierarchy hier in dim.Hierarchies)
                    {
                        if (_destSchema.Schema.FindByUniqueName("Hierarchy:" + hier.UniqueName) == null)
                            _destSchema.Schema.AddSchemaRow("Hierarchy:" + hier.UniqueName);

                        foreach (Level lev in hier.Levels)
                        {
                            if (_destSchema.Schema.FindByUniqueName("Level:" + lev.UniqueName) == null)
                                _destSchema.Schema.AddSchemaRow("Level:" + lev.UniqueName);
                        }

                        // measures handeled separately
                        if (dim.DimensionType != DimensionTypeEnum.Measure)
                        {
                            // default member or first member in first level
                            Member defMem = (Member)SourceSchemaMap.LookupAdomdSchemaObject(_destCube, "Member", hier.DefaultMember);
                            if (defMem == null)
                                defMem = hier.Levels[0].GetMembers(0, 1)[0];
                            if (_destSchema.Schema.FindByUniqueName("Member:" + defMem.UniqueName) == null)
                                _destSchema.Schema.AddSchemaRow("Member:" + defMem.UniqueName);

                            // calculated members
                            MemberCollection mems =
                                (hier.Levels[0].LevelType == LevelTypeEnum.All ?
                                hier.Levels[1].GetMembers() : hier.Levels[0].GetMembers());
                            foreach (Member mem in mems)
                            {
                                if (mem.Type != MemberTypeEnum.Formula)
                                    continue;

                                if (_destSchema.Schema.FindByUniqueName("Member:" + mem.UniqueName) == null)
                                    _destSchema.Schema.AddSchemaRow("Member:" + mem.UniqueName);
                            }
                        }
                    }

                    // measures
                    if (dim.DimensionType == DimensionTypeEnum.Measure)
                        foreach (Measure mea in _destCube.Measures)
                        {
                            if (_destSchema.Schema.FindByUniqueName("Measure:" + mea.UniqueName) == null)
                                _destSchema.Schema.AddSchemaRow("Measure:" + mea.UniqueName);
                        }
                }


                // merge dest schema to file
                foreach (SourceSchemaMap.MapRow srcRow in _map.Map.Rows)
                {
                    // assign is empty and exact match
                    if (srcRow.IsDestUniqueNameNull() || srcRow.DestUniqueName == "")
                    {
                        DestSchema.SchemaRow destRow = _destSchema.Schema.FindByUniqueName(srcRow.SourceUniqueName);
                        srcRow.DestUniqueName = (destRow == null ? "" : destRow.UniqueName);
                    }
                    else // check assigned
                    {
                        DestSchema.SchemaRow destRow = _destSchema.Schema.FindByUniqueName(srcRow.DestUniqueName);
                        if (destRow == null)
                            srcRow.DestUniqueName = "";
                    }
                }
            }
            finally
            {
                if (_map != null)
                    _map.EndInit();

                if (_destSchema != null)
                {
                    _destSchema.AcceptChanges();
                    _destSchema.EndInit();
                }
            }
        }

        public void SaveMap(string filePath)
        {
            if (_map == null)
                return;

            _map.WriteXml(filePath);
            _map.AcceptChanges();
        }

        public void LoadReports()
        {
            if (_reports != null)
                return;

            SqlConnection reportConn = null;
            try
            {
                // create dataset
                _reports = new ReportsValidation();
                _reports.BeginInit();

                // open connections
                reportConn = new SqlConnection(ConfigurationManager.AppSettings["DBFINFConnection"]);
                reportConn.Open();
                string companyId = ConfigurationManager.AppSettings["DBFINFCompanyId"];

                // commit unsaved reports
                SqlCommand cmd = reportConn.CreateCommand();
                cmd.CommandText = string.Format(
@"update t_olap_reports set data=s.data
from t_olap_reports, t_olap_reports_state s, tusers u 
where 
t_olap_reports.user_id=u.id and u.company_id={0}
and t_olap_reports.id=s.rpt_id and s.is_current=1", companyId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                // delete report history
                cmd = reportConn.CreateCommand();
                cmd.CommandText = string.Format(
@"delete from t_olap_reports_state 
where rpt_id in (select r.id from t_olap_reports r , tusers u
where r.user_id=u.id and u.company_id={0})", companyId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                // load reports data
                cmd = reportConn.CreateCommand();
                cmd.CommandText = string.Format(
@"select r.Id, r.Name, r.Description, u.Name as [User], r.data as ReportXml, 
0 as ObjectsTotal, 0 as ObjectsInvalid, 0 as ObjectsInvalidNew, 0 as InvalidDiff 
from tusers u inner join t_olap_reports r
on u.id=r.user_id
where u.company_id={0}
order by r.Id", companyId);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.AcceptChangesDuringFill = true;
                ad.Fill(_reports.OlapReports);
                cmd.Dispose();
            }
            catch (Exception exc)
            {
                _reports = null;
            }
            finally
            {
                if (reportConn != null)
                    reportConn.Close();

                if (_reports != null)                
                    _reports.EndInit();                
            }
        }

        public void ValidateReports()
        {
            if (_reports == null)
                return;

            try
            {
                _reports.BeginInit();

                foreach (ReportsValidation.OlapReportsRow row in _reports.OlapReports)
                    ValidateOlapReport(row);
            }
            finally
            {
                if (_reports != null)
                    _reports.EndInit();
            }
        }

        
        public void SaveReports()
        {
            if (_reports == null)
                return;

            SqlConnection reportConn = null ;
            try
            {
                _reports.BeginInit();

                // open connection
                reportConn = new SqlConnection(ConfigurationManager.AppSettings["DBFINFConnection"]);
                reportConn.Open();

                // save changed and accept cahnges
                for (int i = 0; i < _reports.OlapReports.Count; i++)
                {
                    ReportsValidation.OlapReportsRow row = _reports.OlapReports[i];
                    if (row.RowState != System.Data.DataRowState.Modified)
                        continue;

                    // save xml
                    SqlCommand cmd = reportConn.CreateCommand();
                    cmd.CommandText = "update t_olap_reports set data=@data where id=@id";
                    cmd.Parameters.AddWithValue("@id", row.Id);
                    cmd.Parameters.AddWithValue("@data", row.ReportXml);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    // accept
                    row.AcceptChanges();
                }
            }
            finally
            {
                if (reportConn != null)
                    reportConn.Close();

                if (_reports != null)
                    _reports.EndInit();
            }
        }


        private void ValidateOlapReport(ReportsValidation.OlapReportsRow row)
        {
            if (row == null || row.IsReportXmlNull() || row.ReportXml == "")
                return;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(row.ReportXml);

            // clear stats
            row.ObjectsInvalid = 0;
            row.ObjectsInvalidNew = 0;
            row.InvalidDiff = 0;

            // dimensions
            foreach (XmlElement el in doc.GetElementsByTagName("D"))
            {
                string un = el.GetAttribute("UN");
                un=ValidateOlapObject(SchemaObjectType.ObjectTypeDimension, un, row);
                el.SetAttribute("UN", un);
            }

            // hierarchies
            foreach (XmlElement el in doc.GetElementsByTagName("H"))
            {
                string un = el.GetAttribute("UN");
                un=ValidateOlapObject(SchemaObjectType.ObjectTypeHierarchy, un, row);
                el.SetAttribute("UN", un);
            }

            // levels
            foreach (XmlElement el in doc.GetElementsByTagName("L"))
            {
                string un = el.GetAttribute("UN");
                un=ValidateOlapObject(SchemaObjectType.ObjectTypeLevel, un, row);
                el.SetAttribute("UN", un);
            }

            // members
            foreach (XmlElement el in doc.GetElementsByTagName("M"))
            {
                string un = el.GetAttribute("UN");
                string calc = el.GetAttribute("C");

                if (calc != "1")
                {
                    un=ValidateOlapObject(SchemaObjectType.ObjectTypeMember, un, row);
                    el.SetAttribute("UN", un);
                }
            }

            // save back
            row.ReportXml = doc.OuterXml;
        }

        private string ValidateOlapObject(SchemaObjectType type, string uniqueName, ReportsValidation.OlapReportsRow row)
        {
            // resolve source object
            object srcObject = (_srcUnmatched.Contains(uniqueName) ? null : SourceSchemaMap.LookupAdomdSchemaObject(_srcCube, type, uniqueName));
            if (srcObject == null)
            {
                if (!_srcUnmatched.Contains(uniqueName))
                    _srcUnmatched.Add(uniqueName);
                row.ObjectsInvalid++;
            }

            // try to convert manually
            string destUniqueName = null;
            if (!_destUnmatched.Contains(uniqueName))
            {
                if (uniqueName.StartsWith("[Central Chain]"))
                    destUniqueName = LookupCentralChain(uniqueName);
                else if (uniqueName.StartsWith("[Chain]"))
                    destUniqueName = LookupChain(uniqueName);
                else if (uniqueName.StartsWith("[Store].[Chain]"))
                    destUniqueName = LookupStoreChain(uniqueName);
                else if (uniqueName.StartsWith("[Store].[Postal Code]"))
                    destUniqueName = LookupStorePostalCode(uniqueName);
                else if (uniqueName.StartsWith("[Time].[Monthly]"))
                    destUniqueName = LookupTimeMonthly(uniqueName);
                else if (uniqueName.StartsWith("[Time].[Weekly]"))
                    destUniqueName = LookupTimeWeekly(uniqueName);
                else if (uniqueName.StartsWith("[Expand Compound Products]"))
                    destUniqueName = LookupExpandProductGroups(uniqueName);

                // convert using map
                if (destUniqueName == null)
                    destUniqueName = _map.ConvertSchemaObjectUN(type, uniqueName);
            }

            // lookup dest object (if matched)
            if (destUniqueName == null || SourceSchemaMap.LookupAdomdSchemaObject(_destCube, type, destUniqueName) == null)
            {
                destUniqueName=null;
                if (!_srcUnmatched.Contains(uniqueName) && !_destUnmatched.Contains(uniqueName))
                    _destUnmatched.Add(uniqueName);
                row.ObjectsInvalidNew++;
            }
            
            // diff
            row.InvalidDiff = (row.ObjectsInvalidNew - row.ObjectsInvalid);

            // return 
            return (destUniqueName == null ? uniqueName : destUniqueName);
        }

        private string LookupExpandProductGroups(string uniqueName)
        {
            if (uniqueName.EndsWith(".&[4]"))
                return "[Expand Compound Products].[Expand Compound Products].[Expand].&[Yes]";
            else if (uniqueName.EndsWith(".&[3]"))
                return "[Expand Compound Products].[Expand Compound Products].[Expand].&[No]";
            else
                return null;
        }

        private string LookupCentralChain(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_cchIndex == null)
            {
                _cchIndex = new Hashtable();
                Hierarchy hier = _destCube.Dimensions["Central Chain"].Hierarchies[0];

                foreach (Member m in hier.Levels["Central Chain"].GetMembers())
                    _cchIndex.Add(m.Name, m.UniqueName);
            }

            // get name and lookup from index
            string ret=null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter>=0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4);
                ret = _cchIndex[name] as string;
            }

            return ret;
        }

        private string LookupChain(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_chnIndex == null)
            {
                _chnIndex = new Hashtable();
                Hierarchy hier = _destCube.Dimensions["Chain"].Hierarchies[0];
                foreach (Member m in hier.Levels["Chain"].GetMembers())
                    _chnIndex.Add(m.Name, m.UniqueName);
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4);
                ret = _chnIndex[name] as string;
            }

            return ret;
        }

        private string LookupStoreChain(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_storeChnIndex == null)
            {
                _storeChnIndex = new Hashtable();

                Hierarchy hier = _destCube.Dimensions["Store"].Hierarchies["Chain"];
                foreach (Member m in hier.Levels["Chain"].GetMembers())
                {
                    if (m.Name != "Undefined") // undefined handeled separately
                        _storeChnIndex.Add(m.Name, m.UniqueName);
                }

                // undefined
                _storeChnIndex.Add("Undefined", "[Store].[Chain].[Chain].&[Undefined]&[Undefined]");
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4);
                ret = _storeChnIndex[name] as string;
            }

            return ret;
        }


        private string LookupStorePostalCode(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_storePcodeIndex == null)
            {
                _storePcodeIndex = new Hashtable();

                Hierarchy hier = _destCube.Dimensions["Store"].Hierarchies["Postal Code"];
                foreach (Member m in hier.Levels["Postal Code"].GetMembers())
                {
                    // undefined handeled separately 
                    // and also check if code already added, data is incorrect
                    if (m.Name != "Undefined" && !_storePcodeIndex.ContainsKey(m.Name)) 
                        _storePcodeIndex.Add(m.Name, m.UniqueName);
                }

                // undefined
                _storePcodeIndex.Add("Undefined", "[Store].[Postal Code].[Postal Code].&[Undefined]&[Undefined]");
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4);
                ret = _storePcodeIndex[name] as string;
            }

            return ret;
        }

        private string LookupTimeMonthly(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_timeMonthlyIndex == null)
            {
                _timeMonthlyIndex = new Hashtable();
                Hierarchy hier = _destCube.Dimensions["Time"].Hierarchies["Monthly"];
                foreach (Member m in hier.Levels["Month"].GetMembers())
                    _timeMonthlyIndex.Add(m.Name, m.UniqueName);
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4);
                ret = _timeMonthlyIndex[name] as string;
            }

            return ret;
        }

        private string LookupTimeWeekly(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_timeWeeklyIndex == null)
            {
                _timeWeeklyIndex = new Hashtable();
                Hierarchy hier = _destCube.Dimensions["Time"].Hierarchies["Weekly"];
                foreach (Member m in hier.Levels["Week"].GetMembers())
                    _timeWeeklyIndex.Add(m.Name, m.UniqueName);
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4);
                ret = _timeWeeklyIndex[name] as string;
            }

            return ret;
        }

    }
}
