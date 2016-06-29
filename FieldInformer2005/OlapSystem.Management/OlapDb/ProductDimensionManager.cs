using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Text;
using Microsoft.AnalysisServices;

namespace OlapSystem.Management.OlapDb
{
    public class ProductDimensionManager:DynamicDimensionManager
    {
        internal ProductDimensionManager(DatabaseManager dbManager) : base(dbManager)
        {
        }

        public override string SourceTableName
        {
            get { return "OLAP_PRODUCT"; }
        }

        public override string DimensionName
        {
            get {return "Product"; }
        }
    }

}
