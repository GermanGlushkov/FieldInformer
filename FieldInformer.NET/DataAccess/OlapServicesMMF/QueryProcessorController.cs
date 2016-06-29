using System;
using System.IO;
using System.Reflection;

using FI.Common;
using ThreadMessaging;

namespace FI.DataAccess.OlapServices
{

	public class QueryProcessorController:MarshalByRefObject
	{
		public enum StateEnum
		{
			Idle,
			Busy
		}
		
		private string _server;
		private string _database;
		private System.Diagnostics.Process _process=null;
		private ChannelManager _chnManager;
		private StateEnum _state=StateEnum.Idle;
		private Guid _taskId=Guid.Empty;



		internal QueryProcessorController(string Server, string Database)
		{
			_server=Server;
			_database=Database;			
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

		public Guid TaskId
		{
			get{return _taskId;}
		}

		public int ProcessorId
		{
			get { return this.GetHashCode();}
		}

		private void InitializeProcess()
		{
			if(_process!=null && !_process.HasExited)
				return;

			// process will host remote XmlCellsetWrapper
			// it will also activate tcp channel
			System.Diagnostics.ProcessStartInfo si=new System.Diagnostics.ProcessStartInfo(FI.Common.AppConfig.DA_OlapProcessorPath);
			si.Arguments=@"id=" + this.ProcessorId.ToString();
			si.CreateNoWindow=true;
			si.UseShellExecute=false;
			_process=System.Diagnostics.Process.Start(si);	

			_state=StateEnum.Idle;	
		}

		private void TerminateProcess()
		{			
			if(_process==null || _process.HasExited)
				return; 

			_process.Kill();
			_process.Dispose();
			_process=null;

			_state=StateEnum.Idle;
		}


		public bool IsAllocated
		{
			get { return (_taskId!=Guid.Empty);}
		}

		internal void Allocate(Guid taskId)
		{
			if(taskId==Guid.Empty)
				throw new ArgumentNullException("taskId cannot be Guid.Empty");
			if(_taskId==taskId)
				return;

			if(this.IsAllocated)
				throw new Exception("processor is already allocated by another task: " + _taskId);
			
			if(ProcessorPool.Instance.IsTaskCanceled(taskId))
				throw new Exception("Task is canceled: " + _taskId);

			_taskId=taskId;			

			// log
			if(FI.Common.AppConfig.IsDebugMode)			
				FI.Common.LogWriter.Instance.WriteEventLogEntry(
					string.Format("Processor allocated.\r\n ProcessId: {0}\r\nTaskId: {1}\r\nServer: {2}\r\nDatabase: {3}\r\n", 					
					(_process==null || _process.HasExited ? "Unknown" : _process.Id.ToString()),
					_taskId,
					_server,
					_database));
		}

		internal void Release(bool ForceTerminate)
		{						
			if(!ForceTerminate && _state==StateEnum.Busy)
				throw new Exception("Cannot release busy processor if ForceTerminate=false");			

			// log
			if(FI.Common.AppConfig.IsDebugMode)							
				FI.Common.LogWriter.Instance.WriteEventLogEntry(
					string.Format("Releazing processor.\r\nForce Terminate: {0}\r\n ProcessId: {1}\r\nTaskId: {2}\r\nServer: {3}\r\nDatabase: {4}\r\n", 
					ForceTerminate, 
					(_process==null || _process.HasExited ? "Unknown" : _process.Id.ToString()),
					_taskId,
					_server,
					_database));

			if(ForceTerminate)
				this.TerminateProcess();

			_taskId=Guid.Empty;
		}


		private MemoryStream ProcessTask(object[] arguments)
		{
			return ProcessTask(arguments, false);
		}

		private MemoryStream ProcessTask(object[] arguments, bool forceReinit)
		{
			if(arguments==null || arguments.Length==0)
				throw new ArgumentNullException();

			if(_state==StateEnum.Busy)
				throw new Exception("Unable to execute command: processor is busy");

			if(forceReinit)		
			{
				FI.Common.LogWriter.Instance.WriteEventLogEntry("Reinit processor: " + this.ProcessorId.ToString());
				ReinitProcessor();
			}

			byte[] taskData;
			byte[] responseData;
			try
			{				
				// state
				_state=StateEnum.Busy;

				// init channel manager
				if(_chnManager==null)
					_chnManager=new ChannelManager(ChannelManager.CommandChannelTypes.Controller, this.ProcessorId);

				// init process
				InitializeProcess();
				
				// send cmd
				_chnManager.SendCmdStream();
				FI.Common.LogWriter.Instance.WriteEventLogEntry("task command sent");

				// get response (accept)
				bool ack=_chnManager.ReceiveCmdStream(false);
				FI.Common.LogWriter.Instance.WriteEventLogEntry("task ack: " + ack.ToString());

				// if nothing received, might be error in process, reinit, process again with reinit (if current process was not with reinit)
				if(!ack)
				{
					if(forceReinit)
						ProcessTask(arguments, forceReinit);		
					else
						throw  new Exception("ProcessChannel communication failed");
				}

				// send task
				MemoryStream taskStream=new MemoryStream();
				Serialization.SerializeValue(taskStream, byte.MinValue); // task flag
				for(int i=0;i<arguments.Length;i++)
					Serialization.SerializeValue(taskStream, arguments[i]);				
				taskData=taskStream.ToArray();
				taskStream.Close();

				_chnManager.SendDataStream(taskData);
				FI.Common.LogWriter.Instance.WriteEventLogEntry("task sent, len: " + taskData.Length.ToString());

				// receive response
				responseData=_chnManager.ReceiveDataStream();					
				FI.Common.LogWriter.Instance.WriteEventLogEntry("response received, len: " + responseData.Length.ToString());
				
				if(responseData==null)
				{					
					ReinitProcessor();
					FI.Common.LogWriter.Instance.WriteEventLogEntry("QueryProcessor execution timeout");
					throw new Exception("QueryProcessor execution timeout.");
				}

				// parse responses
				if(responseData[0]==(byte)2)
				{
					// read and return response
					MemoryStream response=new MemoryStream();
					response.Write(responseData, 1, responseData.Length-1);
					response.Position=0;
					return response;
				}
				else if(responseData[0]==(byte)3)
				{
					// read error
					string s=System.Text.Encoding.Unicode.GetString(responseData, 1, responseData.Length-1);

					// throw
					throw new Exception("QueryProcessor error: \r\n" + s);
				}
				else				
					throw new Exception("Invalid stream received during ProcessChannel communication");

			}
			catch(Exception exc)
			{				
				// cleanup channel
				if(exc is SemaphoreFailedException)
				{
					_chnManager.CleanupChannels();
					exc=new Exception("ProcessChannel communication failed: SemaphoreFailedException.");
				}

				throw exc;
			}
			finally
			{
				_state=StateEnum.Idle;
				//FI.Common.LogWriter.Instance.WriteLogEntry("Query executed by process: " + _process.Id.ToString());
			}
		}		

		private void ReinitProcessor()
		{		
			if(_chnManager!=null)
				_chnManager.CleanupChannels();
			this.TerminateProcess();
			this.InitializeProcess();
		}










		
		public string BuildCellset(string Mdx )
		{
			MemoryStream stream=ProcessTask(new object[]{"BuildCellset", this.Server, this.Database, Mdx});
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}
		
		public string GetReportXml(string Cube, string InReportXml)
		{
			MemoryStream stream=ProcessTask(new object[]{"GetReportXml", this.Server, this.Database, Cube, InReportXml});
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}

		public string GetReportSchemaXml(string Cube, string OpenNodesXml)
		{
			MemoryStream stream=ProcessTask(new object[]{"GetReportSchemaXml", this.Server, this.Database, Cube, OpenNodesXml});
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}

		public string GetSchemaMembers(string Cube, string[] UniqueNames)
		{			
			object[] args=new object[UniqueNames.Length+4];
			args[0]="GetSchemaMembers";
			args[1]=this.Server;
			args[2]=this.Database;
			args[3]=Cube;
			for(int i=0;i<UniqueNames.Length;i++)
				args[i+4]=UniqueNames[i];

			MemoryStream stream=ProcessTask(args);
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}

		public string GetLevelMembers(string Cube, string LevelUniqueName)
		{
			MemoryStream stream=ProcessTask(new object[]{"GetLevelMembers", this.Server, this.Database, Cube, LevelUniqueName});
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}

		public string GetMemberChildren(string Cube, string MemUniqueName, bool IfLeafAddItself)
		{
			MemoryStream stream=ProcessTask(new object[]{"GetMemberChildren", this.Server, this.Database, MemUniqueName, IfLeafAddItself});
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}

		public string GetMemberParentWithSiblings(string Cube, string HierUniqueName, string MemUniqueName)
		{
			MemoryStream stream=ProcessTask(new object[]{"GetMemberParentWithSiblings", this.Server, this.Database, Cube, HierUniqueName, MemUniqueName});
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}

		public string GetMemberGrandParent(string Cube, string HierUniqueName, string MemUniqueName)
		{
			MemoryStream stream=ProcessTask(new object[]{"GetMemberGrandParent", this.Server, this.Database, Cube, HierUniqueName, MemUniqueName});
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}

		public string GetMemberParent(string Cube, string HierUniqueName, string MemUniqueName)
		{
			MemoryStream stream=ProcessTask(new object[]{"GetMemberParent", this.Server, this.Database, Cube, HierUniqueName, MemUniqueName});
			try
			{
				return Serialization.DeserializeValue(stream, typeof(string)) as string;
			}
			finally
			{
				stream.Close();
			}
		}

	}
}
