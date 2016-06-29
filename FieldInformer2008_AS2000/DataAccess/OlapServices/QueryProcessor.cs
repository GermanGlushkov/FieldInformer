using System;
using System.Runtime.Remoting;

namespace FI.DataAccess.OlapServices
{

    public class QueryProcessor : MarshalByRefObject
    {
        private delegate string XmlCstCommandDelegate(object[] args);

        internal QueryProcessor(int TcpPort, string Server, string Database)
        {
            _tcpPort = TcpPort;
            _server = Server;
            _database = Database;
        }

        public enum StateEnum
        {
            Idle,
            Busy
        }

        private int _tcpPort;
        private string _server;
        private string _database;
        private System.Diagnostics.Process _process = null;
        private FI.DataAccess.Serviced.XmlCellsetWrapper _xmlCst;
        private StateEnum _state = StateEnum.Idle;
        private string _taskId = null;
        private string _taskTag = null;
        private System.DateTime _allocatedOn;


        public StateEnum State
        {
            get { return _state; }
        }

        public int TcpPort
        {
            get { return _tcpPort; }
        }

        public string Server
        {
            get { return _server; }
        }

        public string Database
        {
            get { return _database; }
        }

        public string TaskId
        {
            get { return _taskId; }
        }

        public string TaskTag
        {
            get { return _taskTag; }
            set { _taskTag = value; }
        }


        private string XmlCellsetUrl
        {
            get { return "tcp://localhost:" + TcpPort.ToString() + "/QueryProcessor"; }
        }

        private string XmlCellset2Url
        {
            get { return "tcp://localhost:" + TcpPort.ToString() + "/QueryProcessor2"; }
        }

        public bool IsAllocated
        {
            get { return (_taskId != null); }
        }

        public bool IsCurrentTaskValid
        {
            get { return (_taskId != null && !ProcessorPool.Instance.IsTaskCanceled(_taskId)); }
        }

        public DateTime AllocatedOn
        {
            get { return _allocatedOn; }
        }

        public TimeSpan AllocatedSpan
        {
            get { return (_allocatedOn == DateTime.MinValue ? new TimeSpan(0) : DateTime.Now.Subtract(_allocatedOn)); }
        }

        public int ProcessId
        {
            get { return (_process == null || _process.HasExited ? 0 : _process.Id); }
        }

        private void InitializeProcess()
        {
            if (_process != null && !_process.HasExited)
                return;

            // process will host remote XmlCellsetWrapper
            // it will also activate tcp channel
            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo(FI.Common.AppConfig.DA_OlapProcessorPath);
            si.Arguments = @"port=" + this.TcpPort.ToString();
            si.CreateNoWindow = true;
            si.UseShellExecute = false;
            _process = System.Diagnostics.Process.Start(si);
            _process.PriorityClass = System.Diagnostics.ProcessPriorityClass.BelowNormal;

            _xmlCst = (FI.DataAccess.Serviced.XmlCellsetWrapper)System.Activator.GetObject(typeof(FI.DataAccess.Serviced.XmlCellsetWrapper), XmlCellsetUrl);

            // attempts to ping remote objects
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    _xmlCst.Ping();

                    // if ping successful, break
                    break;
                }
                catch (Exception exc)
                {
                    // if last iteration, throw exception
                    if (i == 9)
                    {
                        Exception newExc = new Exception("Unable to initialize remoting host process: " + exc.Message);
                        FI.Common.LogWriter.Instance.WriteEventLogEntry(newExc);
                        throw exc;
                    }
                }
            }

            _state = StateEnum.Idle;
        }

        private void Terminate()
        {
            if (_process == null || _process.HasExited)
                return;

            _process.Kill();
            _process.Dispose();
            _process = null;

            _state = StateEnum.Idle;
        }



        //		public bool IsCurrentTaskCancelled()
        //		{
        //			return (this.IsAllocated && ProcessorPool.Instance.IsTaskCanceled(this.TaskId));
        //		}

        internal bool TryAllocate(string taskId)
        {
            lock (this)
            {
                if (this.IsAllocated)
                    return false;

                if (taskId == null)
                    throw new ArgumentNullException("Invalid cannot be null");

                if (ProcessorPool.Instance.IsTaskCanceled(taskId))
                    throw new Exception("Task is canceled: " + _taskId);

                try
                {
                    // init
                    InitializeProcess();
                }
                catch (System.Net.Sockets.SocketException sexc)
                {
                    FI.Common.LogWriter.Instance.WriteEventLogEntry(sexc);
                    FI.Common.LogWriter.Instance.WriteEventLogEntry("TryAllocate: reinitializing processor..");

                    // reinit 
                    Terminate();
                    InitializeProcess();
                }
                catch (Exception exc)
                {
                    // if not access denied, throw
                    if (exc.Message.IndexOf("Access denied") < 0)
                        throw exc;

                    FI.Common.LogWriter.Instance.WriteEventLogEntry(exc);
                    FI.Common.LogWriter.Instance.WriteEventLogEntry("TryAllocate: reinitializing processor..");

                    // reinit 
                    Terminate();
                    InitializeProcess();
                }
                finally
                {
                    _state = StateEnum.Idle;
                }

                //				// init process
                //				InitializeProcess();

                _taskId = taskId;
                _allocatedOn = DateTime.Now;

                // log
                //				if(FI.Common.AppConfig.IsDebugMode)			
                //					FI.Common.LogWriter.Instance.WriteEventLogEntry(
                //						string.Format("Processor allocated.\r\n ProcessId: {0}\r\nTaskId: {1}\r\nServer: {2}\r\nDatabase: {3}\r\nPort: {4}", 					
                //						(_process==null || _process.HasExited ? "Unknown" : _process.Id.ToString()),
                //						_taskId,
                //						_server,
                //						_database,
                //						_tcpPort));

                return true;
            }
        }

        internal void Release(string taskId, bool forceTerminate)
        {
            lock (this)
            {
                if (!forceTerminate && _state == StateEnum.Busy)
                    throw new Exception("Cannot release busy processor if ForceTerminate=false");
                if (taskId != _taskId)
                    return; // already allocated by another task

                // log
                //				if(FI.Common.AppConfig.IsDebugMode)							
                //					FI.Common.LogWriter.Instance.WriteEventLogEntry(
                //						string.Format("Releazing processor.\r\nForce Terminate: {0}\r\n ProcessId: {1}\r\nTaskId: {2}\r\nServer: {3}\r\nDatabase: {4}\r\nPort: {5}", 
                //						forceTerminate, 
                //						(_process==null || _process.HasExited ? "Unknown" : _process.Id.ToString()),
                //						_taskId,
                //						_server,
                //						_database,
                //						_tcpPort));

                if (forceTerminate)
                    this.Terminate();

                _taskId = null;
                _taskTag = null;
                _allocatedOn = DateTime.MinValue;
            }
        }


        private object ExecXmlCstCommand(System.Delegate commandDelegate, object[] args)
        {
            if (_state == StateEnum.Busy)
                throw new Exception("Unable to execute command: processor is busy");

            try
            {
                _state = StateEnum.Busy;

                // init
                InitializeProcess();

                // invoke
                return DynamicInvokeCommand(commandDelegate, args);
            }
            catch (System.Net.Sockets.SocketException sexc)
            {
                // if task is not valid (for example terminated on cancel)
                if (!this.IsCurrentTaskValid)
                    return null;

                FI.Common.LogWriter.Instance.WriteEventLogEntry(sexc);
                FI.Common.LogWriter.Instance.WriteEventLogEntry("ExecXmlCstCommand: reinitializing processor..");

                // reinit 
                Terminate();
                InitializeProcess();

                // invoke
                return DynamicInvokeCommand(commandDelegate, args);
            }
            catch (Exception exc)
            {
                // if task is not valid (for example terminated on cancel)
                if (!this.IsCurrentTaskValid)
                    return null;

                // if access denied, try one more time
                if (exc.Message.IndexOf("Access denied") >= 0)
                {
                    FI.Common.LogWriter.Instance.WriteEventLogEntry(exc);
                    FI.Common.LogWriter.Instance.WriteEventLogEntry("ExecXmlCstCommand: reinitializing processor..");

                    // reinit 
                    Terminate();
                    InitializeProcess();

                    // invoke
                    return DynamicInvokeCommand(commandDelegate, args);
                }

                throw exc;
            }
            finally
            {
                _state = StateEnum.Idle;
            }
        }


        private object DynamicInvokeCommand(System.Delegate commandDelegate, object[] agrs)
        {
            // execute
            try
            {
                object ret = (string)commandDelegate.DynamicInvoke(agrs);
                return ret;
            }
            catch (Exception exc)
            {
                // throw inner exception if exists
                throw (exc.InnerException != null ? exc.InnerException : exc);
            }
        }


        private delegate string BuildCellsetDelegate(string Serevr, string Database, string Mdx);
        public string BuildCellset(string Mdx)
        {
            BuildCellsetDelegate commandDelegate = new BuildCellsetDelegate(_xmlCst.BuildCellset);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { this.Server, this.Database, Mdx });
        }

        private delegate string GetReportXmlDelegate(string Server, string Database, string Cube, ref string InReportXml);
        public string GetReportXml(string Cube, string InReportXml)
        {
            GetReportXmlDelegate commandDelegate = new GetReportXmlDelegate(_xmlCst.GetReportXml);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { Server, Database, Cube, InReportXml });
        }

        private delegate string GetReportSchemaXmlDelegate(string Server, string Database, string Cube, ref string OpenNodesXml);
        public string GetReportSchemaXml(string Cube, string OpenNodesXml)
        {
            GetReportSchemaXmlDelegate commandDelegate = new GetReportSchemaXmlDelegate(_xmlCst.GetReportSchemaXml);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { Server, Database, Cube, OpenNodesXml });
        }

        private delegate string GetSchemaMembersDelegate(string Server, string Database, string Cube, string[] UniqueNames);
        public string GetSchemaMembers(string Cube, string[] UniqueNames)
        {
            GetSchemaMembersDelegate commandDelegate = new GetSchemaMembersDelegate(_xmlCst.GetSchemaMembers);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { Server, Database, Cube, UniqueNames });
        }

        private delegate string GetLevelMembersDelegate(string Server, string Database, string Cube, string LevelUniqueName);
        public string GetLevelMembers(string Cube, string LevelUniqueName)
        {
            GetLevelMembersDelegate commandDelegate = new GetLevelMembersDelegate(_xmlCst.GetLevelMembers);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { Server, Database, Cube, LevelUniqueName });
        }

        private delegate string GetMemberChildrenDelegate(string Server, string Database, string Cube, string MemUniqueName, bool IfLeafAddItself);
        public string GetMemberChildren(string Cube, string MemUniqueName, bool IfLeafAddItself)
        {
            GetMemberChildrenDelegate commandDelegate = new GetMemberChildrenDelegate(_xmlCst.GetMemberChildren);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { Server, Database, Cube, MemUniqueName, IfLeafAddItself });
        }

        private delegate string GetMemberParentWithSiblingsDelegate(string Server, string Database, string Cube, string HierUniqueName, string MemUniqueName);
        public string GetMemberParentWithSiblings(string Cube, string HierUniqueName, string MemUniqueName)
        {
            GetMemberParentWithSiblingsDelegate commandDelegate = new GetMemberParentWithSiblingsDelegate(_xmlCst.GetMemberParentWithSiblings);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { Server, Database, Cube, HierUniqueName, MemUniqueName });
        }

        private delegate string GetMemberGrandParentDelegate(string Server, string Database, string Cube, string HierUniqueName, string MemUniqueName);
        public string GetMemberGrandParent(string Cube, string HierUniqueName, string MemUniqueName)
        {
            GetMemberGrandParentDelegate commandDelegate = new GetMemberGrandParentDelegate(_xmlCst.GetMemberGrandParent);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { Server, Database, Cube, HierUniqueName, MemUniqueName });
        }

        private delegate string GetMemberParentDelegate(string Server, string Database, string Cube, string HierUniqueName, string MemUniqueName);
        public string GetMemberParent(string Cube, string HierUniqueName, string MemUniqueName)
        {
            GetMemberParentDelegate commandDelegate = new GetMemberParentDelegate(_xmlCst.GetMemberParent);
            return (string)ExecXmlCstCommand(commandDelegate, new object[] { Server, Database, Cube, HierUniqueName, MemUniqueName });
        }

    }
}
