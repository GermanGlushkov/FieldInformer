/*

using System;
using System.Messaging;



namespace OlapSystem.Processor
{

	class Processor
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{

			if(args!=null && args.Length>0)
				for(int i=0;i<args.Length;i++)
					{
						if(args[i].StartsWith("tmq="))
							_taskQueue=args[i].Substring(4);
						else if(args[i].StartsWith("rmq="))
							_resultQueue=args[i].Substring(4);
					}

			//debug
			if(_taskQueue==null || _taskQueue=="")
			{
				_taskQueue=@".\Private$\fi.net.processqueue.0.command";
				_resultQueue=@".\Private$\fi.net.processqueue.0.response";
			}

			StartMQReceive();
		}


		private static FI.DataAccess.Serviced.XmlCellsetWrapper _xmlCst=null;
		private static string _taskQueue=null;
		private static string _resultQueue=null;
 
		private static void StartMQReceive()
		{
			try
			{
				_xmlCst=new FI.DataAccess.Serviced.XmlCellsetWrapper();

				MessageQueue taskMq = new MessageQueue(_taskQueue);
				//taskMq.ReceiveCompleted+=new ReceiveCompletedEventHandler(MQReceiveCompleted);
				while(true)
					ProcessTask(taskMq.Receive());
				//taskMq.BeginReceive();
			}
			catch(Exception exc)
			{
				FI.Common.LogWriter.Instance.WriteException(exc);
				throw exc;
			}
		}		


		/*
		private static void MQReceiveCompleted(object source, ReceiveCompletedEventArgs asyncResult)
		{
			MessageQueue taskMq = (MessageQueue)source;
			Message taskMsg = taskMq.EndReceive(asyncResult.AsyncResult);
			try
			{
				ProcessTask(taskMsg);
			}
			catch(Exception exc)
			{
				FI.Common.LogWriter.Instance.WriteException(exc);
				throw exc;
			}
			finally
			{
				taskMq.BeginReceive();
				//System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
			}
		}
		*/
		
/*

		private static void ProcessTask(Message taskMsg)
		{
			MessageQueue resultMq = new MessageQueue(_resultQueue);
			
			taskMsg.Formatter=new System.Messaging.ActiveXMessageFormatter();
			string message=(string)taskMsg.Body;
			string[] messageParts=System.Text.RegularExpressions.Regex.Split(message, FI.Common.AppConfig.MQMessageSeparator);
			string server=messageParts[0];
			string database=messageParts[1];
			string mdx=messageParts[2];

			//execute query
			string path=FI.Common.AppConfig.DA_OlapQueryDir + @"\" + System.Guid.NewGuid() + ".cst";

			try
			{
				_xmlCst.BuildCellset(mdx, server , database , path);
			}
			catch(Exception exc)
			{
				//write log, send path back
				FI.Common.LogWriter.Instance.WriteException(exc);
			}

			// send result
			Message resultMsg=new Message();
			resultMsg.Formatter=new System.Messaging.ActiveXMessageFormatter();
			resultMsg.CorrelationId = taskMsg.Id;
			resultMsg.Body=path;
			resultMq.Send(resultMsg);

		}



	}
}


*/