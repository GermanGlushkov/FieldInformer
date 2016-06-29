using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace OlapSystem.Management.OlapDb
{
    public class SalesCallDataManager
    {
        private DatabaseManager _dbManager;

        internal SalesCallDataManager(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        public void UpdateSalesCallFactView()
        {
            // read ini sql
            string validCallSql = "";
            if (_dbManager.SalesppIniPath != null && _dbManager.SalesppIniPath != string.Empty)
            {
                IniFile iniFile = new IniFile(_dbManager.SalesppIniPath);
                validCallSql = iniFile.IniReadValue("FieldInformer", "VALIDCALL");
            }

            // run sproc
            SqlConnection conn = _dbManager.GetDataSourceConnection();
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            try
            {
                cmd.CommandText = "EXEC spp.proc_create_V_OLAP_SALESCALL_FACT '" + validCallSql.Replace("'", "''") + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                // ignore error cause otherwise default view will be created inside sproc
                exc = null;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            
        }
    }
}
