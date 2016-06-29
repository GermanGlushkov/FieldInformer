using System;
using System.Runtime.Remoting;

namespace FI.DataAccess.OlapServices
{
	
	public class QueryProcessor:MarshalByRefObject, IDisposable
	{
		private delegate string XmlCstCommandDelegate(object[] args);

		internal QueryProcessor(int TcpPort, string Server, string Database)
		{
			_server=Server;
			_database=Database;
		}

		public enum StateEnum
		{
			Idle,
			Busy
		}
		
		private string _server;
		private string _database;
        private OlapServices.AdomdWrapper _adomd = new OlapServices.AdomdWrapper();
		private StateEnum _state=StateEnum.Idle;
		private string _taskId=null;
		private string _taskTag=null;
		private System.DateTime _allocatedOn;


        public void Dispose()
        {
            if (_adomd != null)
                _adomd.Dispose();
        }

		public StateEnum State
		{
			get{return _state;}
		}

		public string Server
		{
			get{return _server;}
		}

		public string Database
		{
			get{return _database;}
		}

		public string TaskId
		{
			get{return _taskId;}
		}

		public string TaskTag
		{
			get{return _taskTag;}
			set{_taskTag=value;}
		}

		public bool IsAllocated
		{
			get { return (_taskId!=null);}
		}

		public bool IsCurrentTaskValid
		{
			get { return (_taskId!=null && !ProcessorPool.Instance.IsTaskCanceled(_taskId)); }
		}

		public DateTime AllocatedOn
		{
			get { return _allocatedOn;}
		}

		public TimeSpan AllocatedSpan
		{
			get { return (_allocatedOn==DateTime.MinValue ? new TimeSpan(0) : DateTime.Now.Subtract(_allocatedOn));}
		}





		internal bool TryAllocate(string taskId)
		{
			lock(this)
			{
				if(this.IsAllocated)
					return false;

				if(taskId==null)
					throw new ArgumentNullException("Invalid cannot be null");
			
				if(ProcessorPool.Instance.IsTaskCanceled(taskId))
					throw new Exception("Task is canceled: " + _taskId);
                				
				_taskId=taskId;
				_allocatedOn=DateTime.Now;
                _state = StateEnum.Idle;	

				return true;
			}
		}

		internal void Release(string taskId, bool forceTerminate)
		{			
			lock(this)
			{
				if(!forceTerminate && _state==StateEnum.Busy)
					throw new Exception("Cannot release busy processor if ForceTerminate=false");	
				if(taskId!=_taskId)
					return; // already allocated by another task

                _adomd.CancelCommand();
				_taskId=null;
				_taskTag=null;
				_allocatedOn=DateTime.MinValue;
			}
		}


		private object ExecXmlCstCommand(System.Delegate commandDelegate, object[] args)
		{
			if(_state==StateEnum.Busy)
				throw new Exception("Unable to execute command: processor is busy");

			try
			{				
				_state=StateEnum.Busy;

				// invoke
				return DynamicInvokeCommand(commandDelegate, args);
			}
			catch( Exception exc)
			{
				// if task is not valid (for example terminated on cancel)
				if(!this.IsCurrentTaskValid)
					return null;

				throw exc;
			}
			finally
			{
				_state=StateEnum.Idle;
			}
		}


		private object DynamicInvokeCommand(System.Delegate commandDelegate, object[] agrs)
		{
			// execute
			try
			{
				object ret=(string)commandDelegate.DynamicInvoke(agrs);			
				return ret;
			}
			catch(Exception exc)
			{
				// throw inner exception if exists
				throw (exc.InnerException!=null ? exc.InnerException: exc);
			}
		}
			

		private delegate string BuildCellsetDelegate(string Serevr, string Database, string Mdx); 
		public string BuildCellset(string Mdx )
		{
			BuildCellsetDelegate commandDelegate=new BuildCellsetDelegate(_adomd.BuildCellset);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{this.Server, this.Database, Mdx});
		}

		private delegate string ValidateReportXmlDelegate(string Server, string Database, string Cube, ref string InReportXml); 
		public string ValidateReportXml(string Cube, string InReportXml)
		{
			ValidateReportXmlDelegate commandDelegate=new ValidateReportXmlDelegate(_adomd.ValidateReportXml);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{Server, Database, Cube, InReportXml});
		}

		private delegate string GetReportSchemaXmlDelegate(string Server, string Database, string Cube, ref string OpenNodesXml); 
		public string GetReportSchemaXml(string Cube, string OpenNodesXml)
		{
			GetReportSchemaXmlDelegate commandDelegate=new GetReportSchemaXmlDelegate(_adomd.GetReportSchemaXml);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{Server, Database, Cube, OpenNodesXml});
		}

		private delegate string GetSchemaMembersDelegate(string Server, string Database, string Cube, string[] UniqueNames); 
		public string GetSchemaMembers(string Cube, string[] UniqueNames)
		{
			GetSchemaMembersDelegate commandDelegate=new GetSchemaMembersDelegate(_adomd.GetSchemaMembers);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{Server, Database, Cube, UniqueNames});
		}

		private delegate string GetLevelMembersDelegate(string Server, string Database, string Cube, string LevelUniqueName); 
		public string GetLevelMembers(string Cube, string LevelUniqueName)
		{
			GetLevelMembersDelegate commandDelegate=new GetLevelMembersDelegate(_adomd.GetLevelMembers);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{Server, Database, Cube, LevelUniqueName});
		}

		private delegate string GetMemberChildrenDelegate(string Server, string Database, string Cube, string MemUniqueName, bool IfLeafAddItself); 
		public string GetMemberChildren(string Cube, string MemUniqueName, bool IfLeafAddItself)
		{
			GetMemberChildrenDelegate commandDelegate=new GetMemberChildrenDelegate(_adomd.GetMemberChildren);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{Server, Database, Cube, MemUniqueName, IfLeafAddItself});
		}

		private delegate string GetMemberParentWithSiblingsDelegate(string Server, string Database, string Cube, string HierUniqueName, string MemUniqueName); 
		public string GetMemberParentWithSiblings(string Cube, string HierUniqueName, string MemUniqueName)
		{
			GetMemberParentWithSiblingsDelegate commandDelegate=new GetMemberParentWithSiblingsDelegate(_adomd.GetMemberParentWithSiblings);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{Server, Database, Cube, HierUniqueName, MemUniqueName});
		}

		private delegate string GetMemberGrandParentDelegate(string Server, string Database, string Cube, string HierUniqueName, string MemUniqueName); 
		public string GetMemberGrandParent(string Cube, string HierUniqueName, string MemUniqueName)
		{
			GetMemberGrandParentDelegate commandDelegate=new GetMemberGrandParentDelegate(_adomd.GetMemberGrandParent);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{Server, Database, Cube, HierUniqueName, MemUniqueName});
		}

		private delegate string GetMemberParentDelegate(string Server, string Database, string Cube, string HierUniqueName, string MemUniqueName); 
		public string GetMemberParent(string Cube, string HierUniqueName, string MemUniqueName)
		{
			GetMemberParentDelegate commandDelegate=new GetMemberParentDelegate(_adomd.GetMemberParent);
			return (string)ExecXmlCstCommand(commandDelegate, new object[]{Server, Database, Cube, HierUniqueName, MemUniqueName});
		}

	}
}
