using System;
using System.Runtime.Remoting;



namespace OlapSystem.Processor
{

	class Processor
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		// [STAThread]  NB!!!! HOST PROCESS SHOULDN'T BE STA !!!!!!!!!!!!!!!!!!!!!!!!1
		static void Main(string[] args)
		{
			int port=-1;

			if(args!=null && args.Length>0)
				for(int i=0;i<args.Length;i++)
				{
					if(args[i].StartsWith("port="))
						port=int.Parse(args[i].Substring(5));
				}

			//debug
			if(port==-1)
			{
				port=8090;
			}

			InitializeRemoting(port);
			Console.ReadLine();
		}



		private static void InitializeRemoting(int TcpPort)
		{
			// register channel
			System.Runtime.Remoting.Channels.Tcp.TcpChannel chan=new System.Runtime.Remoting.Channels.Tcp.TcpChannel(TcpPort);
			System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(chan);

			// register wellknown type
			System.Runtime.Remoting.RemotingConfiguration.ApplicationName="QueryProcessor";
			System.Runtime.Remoting.RemotingConfiguration.RegisterWellKnownServiceType(typeof(FI.DataAccess.Serviced.XmlCellsetWrapper) , "QueryProcessor" , WellKnownObjectMode.Singleton);
//			System.Runtime.Remoting.RemotingConfiguration.RegisterWellKnownServiceType(typeof(FI.DataAccess.Serviced.XmlCellsetWrapper) , "QueryProcessor" , WellKnownObjectMode.SingleCall);
//			System.Runtime.Remoting.RemotingConfiguration.RegisterWellKnownServiceType(typeof(FI.DataAccess.Serviced.XmlCellsetWrapper2) , "QueryProcessor2" , WellKnownObjectMode.SingleCall);

		}

	}
}
