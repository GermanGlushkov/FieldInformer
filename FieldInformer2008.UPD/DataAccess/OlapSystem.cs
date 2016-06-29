using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
//using System.Messaging;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for OlapSystem.
	/// </summary>
    public class OlapSystem : MarshalByRefObject, FI.Common.DataAccess.IOlapSystemDA
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
            FI.Common.LogWriter.Instance.WriteEventLogEntry("Cancelling Olap Command, TaskId=" + TaskId);
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

		public string BuildCellset(string Server , string Database , string Cube, string Mdx, string TaskId, string TaskDescrition, string ReportType, decimal ReportId)
		{
			if(Mdx==null || Mdx.Trim()=="")
				throw new Exception("Mdx is empty");

			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, TaskId);
			try
			{	                
                // execute
                processor.TaskDescription = TaskDescrition;
				string ret=processor.BuildCellset(Mdx);

                // save as report 
                if (ReportId >= 0)
                {
                    if (string.Compare(ReportType, "Olap", true) == 0)
                    {
                        // get cube processing time
                        DateTime cubeProcessedOn = DateTime.MinValue;
                        if(Cube!=null && Cube!="")
                            cubeProcessedOn=processor.GetCubeLastProcessed(Cube);

                        OlapReports rpt = new OlapReports();
                        rpt.UpdateReportCache(ReportId, Server, Database, Cube, Mdx, TaskId, ret, cubeProcessedOn);
                    }
                }

                // return
				return ret;			
			}
			finally
			{
				processor.Release(TaskId, false);				
			}
		}


        public DateTime GetCubeLastProcessed(string Server, string Database, string Cube, bool throwExc)
        {
            string taskId = Guid.NewGuid().ToString();
            OlapServices.QueryProcessor processor = OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
            DateTime ret = DateTime.MinValue;
            try
            {
                // get cube processing time
                if (Cube != null && Cube != "")
                    ret = processor.GetCubeLastProcessed(Cube);
            }
            catch (Exception exc)
            {
                if (throwExc)
                    throw exc;
            }
            finally
            {
                processor.Release(taskId, false);
            }
            return ret;
        }

		public string ValidateReportXml(string Server, string Database, string Cube, string InReportXml)
		{
			string taskId=Guid.NewGuid().ToString();
			OlapServices.QueryProcessor processor=OlapServices.ProcessorPool.Instance.GetAvailableFromPool(Server, Database, taskId);
			try
			{
				string ret=processor.ValidateReportXml(Cube, InReportXml);
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


	}
}
