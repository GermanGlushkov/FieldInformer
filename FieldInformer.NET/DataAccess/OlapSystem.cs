using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
//using System.Messaging;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for OlapSystem.
	/// </summary>
	public class OlapSystem:MarshalByRefObject
	{
		public OlapSystem()
		{
//			FI.Common.LogWriter.Instance.WriteEventLogEntry("OlapSystem object created, hashcode=" + this.GetHashCode().ToString());
		}

		public void ResetOlapSystem()
		{			
			OlapServices.ProcessorPool.Instance.Reset();
			
			Distributions d=new Distributions();
			d.OnSystemRestart();
		}

		public void CancelOlapCommand(string TaskId)
		{
			OlapServices.ProcessorPool.Instance.ReleaseByTaskId(TaskId);
		}

		public bool IsTaskCanceled(string TaskId)
		{
			return OlapServices.ProcessorPool.Instance.IsTaskCanceled(TaskId);
		}

		public FI.Common.Data.FIDataTable GetOlapProcessorInfo()
		{
			return OlapServices.ProcessorPool.Instance.GetQueryProcessorInfo();
		}

		public string BuildCellset(string Server , string Database ,  string Mdx, string TaskId, string TaskTag)
		{
			if(Mdx==null || Mdx.Trim()=="")
				throw new Exception("Mdx is empty");

			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, TaskId);
			try
			{	
				processor.TaskTag=TaskTag;
				string ret=processor.BuildCellset(Mdx);
				return ret;			
			}
			finally
			{
				processor.Release(TaskId, false);				
			}
		}



		public string GetReportXml(string Server, string Database, string Cube, string InReportXml)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.GetReportXml(Cube, InReportXml);
				return ret;
			}
			finally
			{
				processor.Release(taskId, false);
			}
		}



		public string GetReportSchemaXml(string Server , string Database, string Cube, string OpenNodesXml)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.GetReportSchemaXml(Cube, OpenNodesXml);
				return ret;
			}
			finally
			{
				processor.Release(taskId, false);
			}
		}






		public string GetSchemaMembers(string Server , string Database, string Cube, string[] UniqueNames)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.GetSchemaMembers(Cube, UniqueNames);
				return ret;
			}
			finally
			{
				processor.Release(taskId, false);
			}
		}





		public string GetLevelMembers(string Server , string Database, string Cube, string LevelUniqueName)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.GetLevelMembers(Cube, LevelUniqueName);
				return ret;
			}
			finally
			{
				processor.Release(taskId, false);
			}
		}





		public string GetMemberChildren(string Server , string Database, string Cube, string MemUniqueName, bool IfLeafAddItself)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.GetMemberChildren(Cube, MemUniqueName, IfLeafAddItself);
				return ret;
			}
			finally
			{
				processor.Release(taskId, false);
			}
		}




		public string GetMemberParentWithSiblings(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.GetMemberParentWithSiblings(Cube, HierUniqueName, MemUniqueName);
				return ret;
			}
			finally
			{
				processor.Release(taskId, false);
			}
		}



		public string GetMemberGrandParent(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.GetMemberGrandParent(Cube, HierUniqueName, MemUniqueName);
				return ret;
			}
			finally
			{
				processor.Release(taskId, false);
			}
		}



		public string GetMemberParent(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.GetMemberParent(Cube, HierUniqueName, MemUniqueName);
				return ret;
			}
			finally
			{
				processor.Release(taskId, false);
			}
		}


		public string GetMeasuresHierarchyXml(string Server , string Database, string Cube)
		{
			// in singleton
			return FI.DataAccess.Serviced.OlapConnPool.Instance.GetMeasuresHierarchyXml(Server, Database, Cube);
		}



	}
}
