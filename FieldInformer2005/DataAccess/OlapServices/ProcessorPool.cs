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
		public static readonly int __BASETCPPORT=8090;
		private bool _available=true;

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
			if(TaskId==null || TaskId=="")
				throw new ArgumentException("Invlaid TaskId");
			if(_canceledTasks.Contains(TaskId))
				throw new Exception("Task is canceled: " +  TaskId.ToUpper());

			// check if not resetting
			WaitForAvalability();

			QueryProcessor proc=null;
			int count=0;
			int maxCount=FI.Common.AppConfig.DA_OlapProcessorCount;

			// find avalailable
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
								return proc;							
						}
					}

					// if possible to add processor			
					if(count<maxCount)
					{
						int port=__BASETCPPORT + _pool.Count +1;
						proc=new QueryProcessor(port, Server, Database);
						_pool.Add(proc);

						if(proc.TryAllocate(TaskId))
							return proc;
					}
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
			while(!_available)
				System.Threading.Thread.Sleep(100);
		}



		public FI.Common.Data.FIDataTable GetQueryProcessorInfo()
		{
			FI.Common.Data.FIDataTable ret=new FI.Common.Data.FIDataTable();
			ret.Columns.Add("Server", typeof(string));
			ret.Columns.Add("Database", typeof(string));
			ret.Columns.Add("State", typeof(string));
			ret.Columns.Add("AllocatedSpan", typeof(string));
			ret.Columns.Add("TaskId", typeof(string));
			ret.Columns.Add("TaskTag", typeof(string));

			lock(this)
			{
				for(int i=0;i<_pool.Count;i++)
				{
					QueryProcessor proc=(QueryProcessor)_pool[i];
                    ret.Rows.Add(new object[]{
						proc.Server, proc.Database, proc.State.ToString(), 
						proc.AllocatedSpan.ToString(), 
						proc.TaskId, proc.TaskTag});

				}
			}

			return ret;
		}


	}
}
