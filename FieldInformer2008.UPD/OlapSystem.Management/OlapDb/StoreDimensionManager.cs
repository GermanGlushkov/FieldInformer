using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Text;
using Microsoft.AnalysisServices;

namespace OlapSystem.Management.OlapDb
{
    public class StoreDimensionManager : DynamicDimensionManager
    {

        internal StoreDimensionManager(DatabaseManager dbManager) : base(dbManager)
        {
        }

        public override string SourceTableName
        {
            get { return "OLAP_STORE"; }
        }

        public override string DimensionName
        {
            get { return "Store"; }
        }

        public override string LevelToAdd
        {
            get { return null; }
        }
    }

}
