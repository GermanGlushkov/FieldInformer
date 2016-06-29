/*

using System;
using System.Messaging;

namespace FI.DataAccess.OlapServices
{
	/// <summary>
	/// Summary description for QueryItem.
	/// </summary>
	internal class _QueryProcessor
	{
		internal _QueryProcessor(string CommandMQName , string ResponseMQName)
		{
			this.CommandMQName=CommandMQName;
			this.ResponseMQName=ResponseMQName;

			// declare handler for processor response queues
			MessageQueue mq = new MessageQueue(ResponseMQName);
			mq.ReceiveCompleted+=new ReceiveCompletedEventHandler(ResponseMQReceiveCompleted);
			mq.BeginReceive();
		}

		public enum StateEnum
		{
			NotInitialized,
			Idle,
			Busy
		}


		public string CommandMQName;
		public string ResponseMQName;
		private System.Diagnostics.Process _process=null;
		public Message ResponseMessage=null;
		public string CommandMessageId=null;
		private StateEnum _state=StateEnum.NotInitialized;
		private System.DateTime _execStartTime; 
		private System.DateTime _execEndTime; 

		public event EventHandler StateChanged;
		public event EventHandler QueryExecuted;


		public StateEnum State
		{
			get{return _state;}
		}


		public void SendCommandMQ(Message msg)
		{
			MessageQueue mq=new MessageQueue(CommandMQName);
			mq.Send(msg);
			ResponseMessage=null;
			_state=StateEnum.Busy;
			if(StateChanged!=null)
				StateChanged(this , EventArgs.Empty);
		}


		private void ResponseMQReceiveCompleted(object source, ReceiveCompletedEventArgs asyncResult)
		{
			MessageQueue mq = (MessageQueue)source;
			Message msg = mq.EndReceive(asyncResult.AsyncResult);
			try
			{
				HandleResponseMQMessage(msg);
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


		private void HandleResponseMQMessage(Message msg)
		{
			ResponseMessage=msg;
			_state=StateEnum.Idle;

			if(StateChanged!=null)
				StateChanged(this , EventArgs.Empty);

			if(QueryExecuted!=null)
				QueryExecuted(this , EventArgs.Empty);

			//debug
			FI.Common.LogWriter.Instance.WriteLogEntry("query executed: " + this.CommandMQName);
		}




		public void InitializeProcess()
		{
			System.Diagnostics.ProcessStartInfo si=new System.Diagnostics.ProcessStartInfo(FI.Common.AppConfig.DA_OlapProcessorPath);
			si.Arguments=@"tmq=" + this.CommandMQName + @" rmq=" + this.ResponseMQName;
			si.CreateNoWindow=true;
			si.UseShellExecute=false;
			_process=System.Diagnostics.Process.Start(si);
			_state=StateEnum.Idle;
			if (StateChanged != null)
				StateChanged(this, EventArgs.Empty);
		}

		public void TerminateProcess()
		{
			if(this._process==null)
				return; //process has not been initialized

			_process.CloseMainWindow();
			if(_process.WaitForExit(500)==false)
				_process.Kill();

			_process=null;
			_state=StateEnum.NotInitialized;
			_execEndTime=System.DateTime.Now;
			if (StateChanged != null)
				StateChanged(this, EventArgs.Empty);

		}



	}
}


*/