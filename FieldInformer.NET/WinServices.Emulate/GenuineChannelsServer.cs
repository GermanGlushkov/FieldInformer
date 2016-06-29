//using System;
//using System.Collections;
//using System.Runtime.Remoting;
//using System.Runtime.Remoting.Channels;
//using System.Runtime.Remoting.Lifetime;
//using System.Runtime.Remoting.Messaging;
//
////using Belikov.GenuineChannels;
////using Belikov.GenuineChannels.BroadcastEngine;
////using Belikov.GenuineChannels.DotNetRemotingLayer;
////using Belikov.GenuineChannels.Logbook;
//
//
//
//namespace WinServices.Emulate
//{
//	/// <summary>
//	/// Summary description for GenuineChannelsServer.
//	/// </summary>
//	public class GenuineChannelsServer: MarshalByRefObject
//	{
//		public GenuineChannelsServer()
//		{
//		}
//
//		/// <summary>
//		/// This is to insure that when created as a Singleton, the first instance never dies,
//		/// regardless of the expired time.
//		/// </summary>
//		/// <returns></returns>
//		public override object InitializeLifetimeService()
//		{
//			return null;
//		}
//
//		public void Initialize()
//		{
//			
//			try
//			{
//				// setup .NET remoting
//				GenuineGlobalEventProvider.GenuineChannelsGlobalEvent += new GenuineChannelsGlobalEventHandler(GenuineChannelsEventHandler);
//				//GlobalLoggerContainer.Logger = new BinaryLog(@"c:\tmp\server.log", false);
//				RemotingConfiguration.Configure("WinServices.Emulate.exe.config");				
//
//				// bind the server
//				RemotingServices.Marshal(new FI.DataAccess.Users(), "FI.DataAccess.Users");
//				RemotingServices.Marshal(new FI.DataAccess.Contacts(), "FI.DataAccess.Contacts");
//				RemotingServices.Marshal(new FI.DataAccess.Distributions(), "FI.DataAccess.Distributions");
//				RemotingServices.Marshal(new FI.DataAccess.OlapSystem(), "FI.DataAccess.OlapSystem");
//				RemotingServices.Marshal(new FI.DataAccess.OlapReports(), "FI.DataAccess.OlapReports");
//				RemotingServices.Marshal(new FI.DataAccess.StorecheckReports(), "FI.DataAccess.StorecheckReports");
//				RemotingServices.Marshal(new FI.DataAccess.CustomSqlReports(), "FI.DataAccess.CustomSqlReports");
//				RemotingServices.Marshal(new FI.DataAccess.CustomMdxReports(), "FI.DataAccess.CustomMdxReports");
//			}
//			catch(Exception exc)
//			{
//				throw exc;
//			}
//		}
//
//		public static void GenuineChannelsEventHandler(object sender, GenuineEventArgs e)
//		{
//			if (e.SourceException == null)
//				FI.Common.LogWriter.Instance.WriteEventLogEntry(string.Format("---Global event: {0}\r\nRemote host: {1}", 
//					e.EventType,
//					e.HostInformation == null ? "<unknown>" : e.HostInformation.ToString()));
//			else
//				FI.Common.LogWriter.Instance.WriteEventLogEntry(string.Format("---Global event: {0}\r\nRemote host: {1}\r\nException: {2}", 
//					e.EventType, 
//					e.HostInformation == null ? "<unknown>" : e.HostInformation.ToString(), 
//					e.SourceException));
//		}
//	}
//}
