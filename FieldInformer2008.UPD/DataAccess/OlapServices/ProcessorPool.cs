using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;

namespace FI.DataAccess.OlapServices
{
	/// <summary>
	/// Summary description for QueryManager.
	/// </summary>
	public class ProcessorPool:MarshalByRefObject
	{		
		private ArrayList _pool=new ArrayList();
		private StringCollection _canceledTasks=new StringCollection();
		//public static readonly int __BASETCPPORT=8090;
        private bool _available = true;
        public static readonly int __MAXPROCESSORCOUNT = 100;

		#region singleton

		//in order to live forever
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// singleton pattern
		private ProcessorPool()
		{			
		}
		public static readonly ProcessorPool Instance=new ProcessorPool();
		// singleton pattern

		#endregion singleton


		public QueryProcessor GetAvailableFromPool(string Server, string Database, string TaskId)
        {
            //Common.LogWriter.Instance.WriteEventLogEntry(
            //    string.Format("ProcessorPool request: server {0}, database {1}.", Server, Database));

			if(TaskId==null || TaskId=="")
				throw new ArgumentException("Invlaid TaskId");
			if(_canceledTasks.Contains(TaskId))
				throw new Exception("Task is canceled: " +  TaskId.ToUpper());

			// check if not resetting
			WaitForAvalability();

			QueryProcessor proc=null;
			int count=0;
            int maxCount = __MAXPROCESSORCOUNT; //FI.Common.AppConfig.DA_OlapProcessorCount;

			// find avalailable
            QueryProcessor ret = null;
			while(true)
			{
				lock(this)
				{
					for(int i=0;i<_pool.Count;i++)
					{
						proc=(QueryProcessor)_pool[i];
						if(proc.Server==Server && proc.Database==Database)
						{
							count++;
							if(proc.TryAllocate(TaskId))
								ret=proc;							
						}
					}

					// if possible to add processor			
                    if (ret==null && count < maxCount)
					{
						//int port=__BASETCPPORT + _pool.Count +1;
						proc=new QueryProcessor(Server, Database);
						_pool.Add(proc);

						if(proc.TryAllocate(TaskId))
							ret=proc;
					}
				}

                if (ret != null)
                {
                    //Common.LogWriter.Instance.WriteEventLogEntry(
                    //    string.Format("ProcessorPool count {0}.", _pool.Count.ToString()));
                    return ret;
                }

				// sleep till next loop
				System.Threading.Thread.Sleep(200);
			}			
			
		}



		public void ReleaseByTaskId(string taskId)
		{		
			if(taskId==null || taskId=="")
				throw new ArgumentException("Invalid TaskId");
						
			if(!_canceledTasks.Contains(taskId))
				_canceledTasks.Add(taskId);			

			// check if not resetting
			WaitForAvalability();

			lock(this)
			{
				for(int i=0;i<_pool.Count;i++) 
				{				
					QueryProcessor proc=(QueryProcessor)_pool[i];
					if(proc.TaskId==taskId)
						proc.Release(taskId, true);				
				}
			}
		}


		public bool IsTaskCanceled(string taskId)
		{
			if(taskId==null || taskId=="")
				throw new ArgumentException("Invalid TaskId");
						
			return _canceledTasks.Contains(taskId);
		}

		public void Reset()
		{			
			try
			{
				_available=false;
                DisposeAllProcessors();
			}
			finally
			{
				_available=true;
			}
		}

        private void DisposeAllProcessors()
        {
            lock(this)
			{
                while (_pool.Count > 0)
                {
                    QueryProcessor proc = (QueryProcessor)_pool[0];
                    proc.Dispose();
                    _pool.RemoveAt(0);
                }
			}
        }

		private void WaitForAvalability()
		{
            int count = 0;
            while (!_available)
            {
                System.Threading.Thread.Sleep(100);
                count += 100;
                if (count / 10000 > 0)
                    Common.LogWriter.Instance.WriteEventLogEntry("WaitForAvalability 100 sec passed.");
            }

            if(count > 10000)
                Common.LogWriter.Instance.WriteEventLogEntry(string.Format("WaitForAvalability took {0} sec.", (count/1000).ToString()));
		}



		public FI.Common.Data.FIDataTable GetQueryProcessorInfo()
		{
			FI.Common.Data.FIDataTable ret=new FI.Common.Data.FIDataTable();
			ret.Columns.Add("Server", typeof(string));
			ret.Columns.Add("Database", typeof(string));
			ret.Columns.Add("State", typeof(string));
			ret.Columns.Add("AllocatedSpan", typeof(string));
			ret.Columns.Add("TaskId", typeof(string));
            ret.Columns.Add("TaskDescription", typeof(string));

			lock(this)
			{
				for(int i=0;i<_pool.Count;i++)
				{
					QueryProcessor proc=(QueryProcessor)_pool[i];
                    ret.Rows.Add(new object[]{
						proc.Server, proc.Database, proc.State.ToString(), 
						proc.AllocatedSpan.ToString(), 
						proc.TaskId, proc.TaskDescription});

				}
			}

			return ret;
		}


	}
}
