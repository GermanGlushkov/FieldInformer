using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.AnalysisServices;

namespace OlapSystem.Management.OlapDb
{
    public class DatabaseManager
    {
        private string _olapServer;
        private string _olapDb;
        private string _sourceServer;
        private string _sourceDatabase;
        private string _sourceUserId;
        private string _sourceUserPwd;
        private string _salesppIniPath;

        private Server _adomdServer;
        private Database _adomdDatabase;
        private ProductDimensionManager _prodDimManger;
        private StoreDimensionManager _storeDimManger;
        private SalesCallDataManager _salesCallDataManager;


        public DatabaseManager(string olapServer, string olapDatabase, 
            string sourceServer, string sourceDatabase, string sourceUserId, string sourceUserPwd, string salesppIniPath)
        {
            _olapServer = olapServer;
            _olapDb = olapDatabase;
            _sourceServer = sourceServer;
            _sourceDatabase = sourceDatabase;
            _sourceUserId = sourceUserId;
            _sourceUserPwd = sourceUserPwd;
            _salesppIniPath = salesppIniPath;

            _prodDimManger = new ProductDimensionManager(this);
            _storeDimManger = new StoreDimensionManager(this);
            _storeDimManger = new StoreDimensionManager(this);
            _salesCallDataManager = new SalesCallDataManager(this);
        }

        public string OlapServer
        {
            get { return _olapServer; }
        }

        public string OlapDatabase
        {
            get { return _olapDb; }
        }

        public string SourceServer
        {
            get { return _sourceServer; }
        }

        public string SourceDatabase
        {
            get { return _sourceDatabase; }
        }

        public string SourceUserId
        {
            get { return _sourceUserId; }
        }

        public string SourceUserPwd
        {
            get { return _sourceUserPwd; }
        }

        public string SalesppIniPath
        {
            get { return _salesppIniPath; }
        }


        internal Database AdomdDatabase
        {
            get { return _adomdDatabase; }
        }

        public void Connect()
        {
            if (_adomdServer != null && _adomdServer.Connected)
                return;

            // connect to server
            string connString = string.Format("Data Source={0}", this.OlapServer);
            _adomdServer = new Server();
            _adomdServer.Connect(connString);            

            // get database
            _adomdDatabase = _adomdServer.Databases[this.OlapDatabase];
            if (_adomdDatabase == null)
                throw new Exception(string.Format("Database '{0}' does not exists on server '{1}'",
                    this.OlapDatabase, this.OlapServer));
        }

        public void Disconnect()
        {
            if (_adomdServer == null || !_adomdServer.Connected)
                return;

            _adomdServer.Disconnect();
        }

        internal SqlConnection GetDataSourceConnection()
        {
            // conn string
            string connStr = string.Format("Data Source={0};Initial Catalog={1};", this.SourceServer, this.SourceDatabase);
            if (this.SourceUserId != null && this.SourceUserId != "")
                connStr += string.Format("User Id={0};Password={1};", this.SourceUserId, this.SourceUserPwd);
            else
                connStr += "Integrated Security=SSPI;";

            return new SqlConnection(connStr);
        }


        public void PreProcess()
        {
            this.Connect();

            try
            {
                _salesCallDataManager.UpdateSalesCallFactView();
                _prodDimManger.CheckAndUpdateDimension();
                _storeDimManger.CheckAndUpdateDimension();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                this.Disconnect();
            }
        }

        public void ProcessOlapDatabase()
        {
            this.Connect();

            try
            {
                _adomdDatabase.Process(ProcessType.ProcessFull);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                this.Disconnect();
            }
        }
    }
}