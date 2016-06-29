/*

using System;
using System.Messaging;

namespace FI.DataAccess.OlapServices
{
	/// <summary>
	/// Summary description for QueryManager.
	/// </summary>
	public class _QueryManager:IDisposable
	{
		public _QueryManager()
		{
			Initialize();
		}

		private QueryProcessor[] _processingQueue;
		private int _idleProcessorCount;


		private QueryProcessor GetIdleProcessor()
		{
			while(true)
			{
				// wait for idle 
				if(_idleProcessorCount==0)
					System.Threading.Thread.Sleep(100);
				else
				{
					// first look for idle processor
					for(int i=0;i<_processingQueue.Length;i++) 
					{
						if(_processingQueue[i].State==QueryProcessor.StateEnum.Idle)
						{
							return _processingQueue[i];
						}
					}
					// then look for not initialized processor
					for(int i=0;i<_processingQueue.Length;i++) 
					{
						if(_processingQueue[i].State==QueryProcessor.StateEnum.NotInitialized)
						{
							_processingQueue[i].InitializeProcess();
							return _processingQueue[i];
						}
					}
					//get back to wait if not found
				}
					
			}

			//should never reach this line
			throw new Exception("GetIdleProcessor internal error");
		}




		private void Initialize()
		{
			// kill existing processes
			KillExistingProcesses();

			// create queues and processors
			CreateQueue(FI.Common.AppConfig.DA_OlapCommandQueue);
			CreateQueue(FI.Common.AppConfig.DA_OlapResponseQueue);
			for(int i=0;i<FI.Common.AppConfig.DA_OlapProcessingQueueCount;i++)
			{
				string commandMQ=FI.Common.AppConfig.DA_OlapProcessingQueuePattern.Replace("#" , i.ToString()) + ".command";
				string responseMQ=FI.Common.AppConfig.DA_OlapProcessingQueuePattern.Replace("#" , i.ToString()) + ".response";
				CreateQueue(commandMQ);
				CreateQueue(responseMQ);
			}

			// initialize processors
			_processingQueue=new QueryProcessor[FI.Common.AppConfig.DA_OlapProcessingQueueCount];
			_idleProcessorCount=_processingQueue.Length;
			for(int i=0;i<_processingQueue.Length;i++)
			{
				string commandMQ=FI.Common.AppConfig.DA_OlapProcessingQueuePattern.Replace("#" , i.ToString()) + ".command";
				string responseMQ=FI.Common.AppConfig.DA_OlapProcessingQueuePattern.Replace("#" , i.ToString()) + ".response";
				_processingQueue[i]=new QueryProcessor(commandMQ , responseMQ);
				_processingQueue[i].StateChanged+=new EventHandler(QueryProcessor_StateChanged);
				_processingQueue[i].QueryExecuted+=new EventHandler(QueryProcessor_QueryExecuted);
			}

			/// declare handler
			MessageQueue commandMq = new MessageQueue(FI.Common.AppConfig.DA_OlapCommandQueue);
			commandMq.ReceiveCompleted+=new ReceiveCompletedEventHandler(CommandMQReceiveCompleted);

			// begin receive
			commandMq.BeginReceive();
			// debug
			//while(true)
				//System.Threading.Thread.Sleep(500);
		}

		private void KillExistingProcesses()
		{
			string ProcessorName=System.IO.Path.GetFileNameWithoutExtension(FI.Common.AppConfig.DA_OlapProcessorPath);
			System.Diagnostics.Process[] processes=System.Diagnostics.Process.GetProcessesByName(ProcessorName);
			for(int i=0;i<processes.Length;i++)
			{
				processes[i].CloseMainWindow();
				if(processes[i].WaitForExit(2000)==false) //wait 2 secs
					processes[i].Kill();
			}
		}

		private void TerminateProcessors()
		{
			// kill existing processes
			for(int i=0;i<this._processingQueue.Length;i++)
			{
				this._processingQueue[i].TerminateProcess();
			}
		}


		private static MessageQueue CreateQueue(string Name)
		{
			if(MessageQueue.Exists(Name)==false)
			{
				MessageQueue mq=MessageQueue.Create(Name);
				MessageQueue.EnableConnectionCache=false;
				mq.SetPermissions("Everyone" , MessageQueueAccessRights.FullControl , AccessControlEntryType.Allow);
				return mq;
			}
			else
				return new MessageQueue(Name);
		}


		private void QueryProcessor_StateChanged(object sender, EventArgs e)
		{
			QueryProcessor processor=(QueryProcessor)sender;

			if( processor.State==QueryProcessor.StateEnum.Busy)
				_idleProcessorCount--;
		}





		private void CommandMQReceiveCompleted(object source, ReceiveCompletedEventArgs asyncResult)
		{
			MessageQueue mq = (MessageQueue)source;
			Message msg = mq.EndReceive(asyncResult.AsyncResult);
			try
			{
				HandleCommandMQMessage(msg);
			}
			catch(Exception exc)
			{
				FI.Common.LogWriter.Instance.WriteException(exc);
				throw exc;
			}
			finally
			{
				mq.BeginReceive();
			}
		}




		private void HandleCommandMQMessage(Message msg)
		{
			// if mdx, forward command to processor
			// by now it's allways mdx
			QueryProcessor proc=this.GetIdleProcessor();
			proc.CommandMessageId=msg.Id;
			proc.SendCommandMQ(msg);
		}

		private void QueryProcessor_QueryExecuted(object sender, EventArgs e)
		{
			// forward to response queue
			QueryProcessor proc=(QueryProcessor)sender;

			MessageQueue mq=new MessageQueue(FI.Common.AppConfig.DA_OlapResponseQueue);
			Message msg=proc.ResponseMessage;
			msg.CorrelationId=proc.CommandMessageId;
			mq.Send(msg);

			_idleProcessorCount++;
		}



		#region IDisposable Members

		public void Dispose()
		{
			this.TerminateProcessors();
			this.KillExistingProcesses();
		}

		#endregion



	}
}


*/