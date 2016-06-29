using System;
using ThreadMessaging;

namespace FI.DataAccess.OlapServices
{
	/// <summary>
	/// Summary description for ChannelManager.
	/// </summary>
	public class ChannelManager
	{		
		public static readonly int MAXACKSIZE=1024; 
		public static readonly int MAXDATASIZE=20971520; // 20 MB
		public static readonly int COMMANDTIMEOUT=10; // 10 secs
		public static readonly int EXECTIMEOUT=21600; // 6 hours

		public enum CommandChannelTypes
		{
			Controller,
			Processor
		}

		private int _channelId;
		private CommandChannelTypes _cmdChannelType;
		private ProcessChannel _dataChannel=null;
		private ProcessChannel _contrCmdChannel=null;
		private ProcessChannel _processorCmdChannel=null;

		public ChannelManager(CommandChannelTypes cmdChannelType, int channelId)
		{
			_cmdChannelType=cmdChannelType;
			_channelId=channelId;
		}

		public void InitChannels()
		{			
			if(_dataChannel==null)
			{
				_dataChannel=new ProcessChannel(1, "Global\\task_" + _channelId.ToString(), MAXDATASIZE);
				_contrCmdChannel=new ProcessChannel(1, "Global\\contrcmd_" + _channelId.ToString(), MAXACKSIZE);
				_processorCmdChannel=new ProcessChannel(1, "Global\\proccmd_" + _channelId.ToString(), MAXACKSIZE);
			}
		}

		
		private ProcessChannel GetInboundCmdChannel()
		{
			InitChannels();
			return (_cmdChannelType==CommandChannelTypes.Controller ? _processorCmdChannel : _contrCmdChannel);
		}

		private ProcessChannel GetOutboundCmdChannel()
		{
			InitChannels();
			return (_cmdChannelType==CommandChannelTypes.Controller ? _contrCmdChannel : _processorCmdChannel);
		}

		public void SendCmdStream()
		{
			try
			{
				GetOutboundCmdChannel().Send(0, TimeSpan.FromSeconds(COMMANDTIMEOUT));
			}
			catch(SemaphoreFailedException exc)
			{
				// ignore if nothing to receive and wait for response again		
				FI.Common.LogWriter.Instance.WriteEventLogEntry(_cmdChannelType.ToString() + ": SendCmdStream SemaphoreFailedException");
				throw exc;
			}
		}

		public bool ReceiveCmdStream(bool infinite)
		{		
			object ret=null;
			try
			{
				if(infinite)
					ret=this.GetInboundCmdChannel().Receive();
				else
					ret=this.GetInboundCmdChannel().Receive(TimeSpan.FromSeconds(COMMANDTIMEOUT));
			}
			catch(SemaphoreFailedException exc)
			{
				// ignore if nothing to receive and wait for response again		
				FI.Common.LogWriter.Instance.WriteEventLogEntry(_cmdChannelType.ToString() + ": ReceiveCmdStream SemaphoreFailedException");
				exc=null;
			}
	
			return (ret!=null);
		}


		public void SendDataStream(byte[] data)
		{
			try
			{
				_dataChannel.Send(data, TimeSpan.FromSeconds(COMMANDTIMEOUT));
			}
			catch(SemaphoreFailedException exc)
			{
				// ignore if nothing to receive and wait for response again		
				FI.Common.LogWriter.Instance.WriteEventLogEntry(_cmdChannelType.ToString() + ": SendDataStream SemaphoreFailedException");
				throw exc;;
			}
		}

		public byte[] ReceiveDataStream()
		{		
			byte[] ret=null;
			try
			{
				ret=_dataChannel.Receive(TimeSpan.FromSeconds(EXECTIMEOUT)) as byte[];
			}
			catch(SemaphoreFailedException exc)
			{
				// ignore if nothing to receive and wait for response again	
				FI.Common.LogWriter.Instance.WriteEventLogEntry(_cmdChannelType.ToString() + ":ReceiveDataStream SemaphoreFailedException");	
				exc=null;
			}
	
			return ret;
		}

		public void CleanupChannels()
		{
			if(_dataChannel==null)
				return;

			// cleanup channel - receive data
			try
			{
				_dataChannel.Receive(TimeSpan.FromMilliseconds(100));
			}
			catch(SemaphoreFailedException exc)
			{
				// ignore nothing to receive
			}

			// cleanup channel - receive data
			try
			{
				_contrCmdChannel.Receive(TimeSpan.FromMilliseconds(100));
			}
			catch(SemaphoreFailedException exc)
			{
				// ignore nothing to receive
			}

			// cleanup channel - receive data
			try
			{
				_processorCmdChannel.Receive(TimeSpan.FromMilliseconds(100));
			}
			catch(SemaphoreFailedException exc)
			{
				// ignore nothing to receive
			}
		}

	}
}
