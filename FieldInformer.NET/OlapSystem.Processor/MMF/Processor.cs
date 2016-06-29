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
			int id=-1;

			if(args!=null && args.Length>0)
				for(int i=0;i<args.Length;i++)
				{
					if(args[i].StartsWith("id="))
						id=int.Parse(args[i].Substring(3));
				}

			InitializeRemoting(port);
//			FI.DataAccess.OlapServices.QueryProcessor proc=new FI.DataAccess.OlapServices.QueryProcessor(id);
//			proc.Run();
		}



		private static void InitializeRemoting(int TcpPort)
		{
			// register channel
			System.Runtime.Remoting.Channels.Tcp.TcpChannel chan=new System.Runtime.Remoting.Channels.Tcp.TcpChannel(TcpPort);
			System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(chan);

			// register wellknown type
			System.Runtime.Remoting.RemotingConfiguration.ApplicationName="QueryProcessor";
			System.Runtime.Remoting.RemotingConfiguration.RegisterWellKnownServiceType(typeof(FI.DataAccess.Serviced.XmlCellsetWrapper) , "QueryProcessor" , WellKnownObjectMode.Singleton);

		}

	}
}
