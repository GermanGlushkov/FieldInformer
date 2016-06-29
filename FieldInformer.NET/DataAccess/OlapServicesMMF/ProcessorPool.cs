using System;
using System.Collections;
using System.Collections.Specialized;
using ThreadMessaging;

namespace FI.DataAccess.OlapServices
{
	/// <summary>
	/// Summary description for QueryManager.
	/// </summary>
	public class ProcessorPool:MarshalByRefObject
	{		
		private bool _resetting=false;
		private ArrayList _pool=new ArrayList();
		private StringCollection _canceledTasks=new StringCollection();

		#region singleton

		//in order to live forever
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// singleton pattern		
		public static readonly ProcessorPool Instance=new ProcessorPool();
		// singleton pattern

		#endregion singleton


		private ProcessorPool()
		{			
		}

		public bool Resetting
		{
			get { return _resetting; }
		}

		public QueryProcessorController GetAvailableFromPool(string Server, string Database, Guid newTaskId)
		{			
			QueryProcessorController proc=null;
			int count=0;
			int maxCount=FI.Common.AppConfig.DA_OlapProcessorCount;

			// find avalailable
			while(true)
			{
				// check if it's canceled already
				if(IsTaskCanceled(newTaskId))
					throw new Exception("Task is canceled: " +  newTaskId.ToString());

				lock(this)
				{
					for(int i=0;i<_pool.Count;i++)
					{
						proc=(QueryProcessorController)_pool[i];
						if(proc.Server==Server && proc.Database==Database)
						{
							count++;
							if(!proc.IsAllocated)
							{
								proc.Allocate(newTaskId);
								return proc;
							}
						}
					}

					// if possible to add processor			
					if(count<maxCount)
					{
						proc=new QueryProcessorController(Server, Database);
						_pool.Add(proc);

						proc.Allocate(newTaskId);
						return proc;
					}
				}


				// sleep till next loop
				System.Threading.Thread.Sleep(200);
			}			
			
		}



		public void ReleaseByTaskId(Guid TaskId)
		{		
			lock(this)
			{
				if(TaskId==Guid.Empty)
					throw new ArgumentException("Invalid TaskId:  Empty");
						
				if(!_canceledTasks.Contains(TaskId.ToString()))
					_canceledTasks.Add(TaskId.ToString());			

				for(int i=0;i<_pool.Count;i++) 
				{				
					QueryProcessorController proc=(QueryProcessorController)_pool[i];
					if(proc.TaskId==TaskId)
						proc.Release(true);				
				}
			}
		}


		public bool IsTaskCanceled(Guid taskId)
		{
			if(taskId==Guid.Empty)
				throw new ArgumentException("Invalid TaskId: Empty");
						
			return _canceledTasks.Contains(taskId.ToString());
		}

		public void Reset()
		{
			lock(this)
			{
				_resetting=true;

				// kill proesses
				KillWindowsProcesses();

				// create new processor pool
				_pool=new ArrayList();

				_resetting=false;
			}
		}

		private void KillWindowsProcesses()
		{
			string ProcessorName=System.IO.Path.GetFileNameWithoutExtension(FI.Common.AppConfig.DA_OlapProcessorPath);
			System.Diagnostics.Process[] processes=System.Diagnostics.Process.GetProcessesByName(ProcessorName);
			for(int i=0;i<processes.Length;i++)
			{
				processes[i].Kill();
				processes[i].Dispose(); //free process obj
			}
		}

//		private void TerminateProcessors()
//		{
//			lock(this)
//			{
//				while(_pool.Count>0)
//				{
//					((QueryProcessor)_pool[0]).Release(true);
//					_pool.RemoveAt(0);
//				}
//			}
//		}



	}
}
