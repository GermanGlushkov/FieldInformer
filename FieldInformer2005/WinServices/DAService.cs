using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;

using System.Messaging;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace FI.WinServices
{
	public class DAService : System.ServiceProcess.ServiceBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DAService()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;

			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//

			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new DAService() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);

		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.ServiceName = "FieldInformer.NET Service";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			
			try
			{
				// reset system
				FI.DataAccess.OlapSystem sys=new FI.DataAccess.OlapSystem();
				sys.ResetOlapSystem();

				// set realtime priority
				System.Diagnostics.Process proc=System.Diagnostics.Process.GetCurrentProcess();
				proc.PriorityClass=System.Diagnostics.ProcessPriorityClass.RealTime;

				// config file
				System.Runtime.Remoting.RemotingConfiguration.Configure(System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);

				// db schema
				FI.DataAccess.DataBase.Instance.VerifyDbSchema();
						
				// event logs
				FI.Common.LogWriter.InitCommonEventLogs();
			}
			catch(Exception exc)
			{
				FI.Common.LogWriter.Instance.WriteException(exc);
				throw exc;
			}

		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			FI.DataAccess.OlapSystem sys=new FI.DataAccess.OlapSystem();
			sys.ResetOlapSystem();
		}

		
/*

		private void MQReceiveCompleted(object source, ReceiveCompletedEventArgs asyncResult)
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
			}
		}
		



		private void ProcessTask(Message taskMsg)
		{
			MessageQueue resultMq = new MessageQueue(FI.Common.AppConfig.OlapResultQueue);
			
			taskMsg.Formatter=new System.Messaging.ActiveXMessageFormatter();
			string message=(string)taskMsg.Body;
			string[] messageParts=System.Text.RegularExpressions.Regex.Split(message, FI.Common.AppConfig.MQMessageSeparator);
			string server=messageParts[0];
			string database=messageParts[1];
			string mdx=messageParts[2];

			//execute query
			string path=FI.Common.AppConfig.OlapQueryDir + @"\" + System.Guid.NewGuid() + ".cst";

			try
			{
				_adomd.BuildCellset(mdx, server , database , path);
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




		private void CreateQueues()
		{
			if(!MessageQueue.Exists(FI.Common.AppConfig.OlapTaskQueue)) 
			{
				MessageQueue taskMq=MessageQueue.Create(FI.Common.AppConfig.OlapTaskQueue);
				taskMq.SetPermissions("Everyone" , MessageQueueAccessRights.FullControl , AccessControlEntryType.Allow);
			}

			if(!MessageQueue.Exists(FI.Common.AppConfig.OlapResultQueue)) 
			{
				MessageQueue resultMq=MessageQueue.Create(FI.Common.AppConfig.OlapResultQueue);
				resultMq.SetPermissions("Everyone" , MessageQueueAccessRights.FullControl , AccessControlEntryType.Allow);
			}
		}


		private void InitializeRemoting()
		{
			TcpChannel channel=new TcpChannel(8085);
			ChannelServices.RegisterChannel(channel);

			RemotingConfiguration.ApplicationName="FIService";
			RemotingConfiguration.RegisterActivatedServiceType(typeof(FI.BusinessObjects.User));
		}
*/

	}
}
