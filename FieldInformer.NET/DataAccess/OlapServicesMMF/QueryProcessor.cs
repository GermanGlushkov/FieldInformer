using System;
using System.Collections;
using System.IO;
using System.Text;
using FI.Common;
using ThreadMessaging;

using FI.DataAccess.Serviced;

namespace FI.DataAccess.OlapServices
{

	public class QueryProcessor
	{
		private int _id;
		private ChannelManager _chnManager;

		public QueryProcessor(int id)
		{
			_id=id;
			_chnManager=new ChannelManager(ChannelManager.CommandChannelTypes.Processor, _id);
		}		

		public int Id
		{
			get{return _id;}
		}	

		public void Run()
		{
			// loop
			while(true)
			{
				int t2,t1;

				try
				{
					// waitfor cmd
//					FI.Common.LogWriter.Instance.WriteEventLogEntry("waiting for command");
					_chnManager.ReceiveCmdStream(true);
//					FI.Common.LogWriter.Instance.WriteEventLogEntry("command received");

					// accept
					_chnManager.SendCmdStream();					
//					FI.Common.LogWriter.Instance.WriteEventLogEntry("command accept sent");

					// waitfor task
					t1=System.Environment.TickCount;

					byte[] taskData=_chnManager.ReceiveDataStream();
					if(taskData.Length<=1 || taskData[0]!=0)
						throw new Exception("Invalid task stream");
					MemoryStream taskStream=new MemoryStream(taskData, 1, taskData.Length-1);
					MemoryStream responseStream=null;

					t2=System.Environment.TickCount;
					FI.Common.LogWriter.Instance.WriteEventLogEntry("task received, length=" + taskData.Length.ToString() + ", time=" + (t2-t1).ToString());

					// execute
					try
					{
						t1=System.Environment.TickCount;

						responseStream=new MemoryStream();
						responseStream.WriteByte(2); // response header
						taskStream.Position=0;
						ExecuteTask(taskStream, responseStream);

						t2=System.Environment.TickCount;
						FI.Common.LogWriter.Instance.WriteEventLogEntry("task executed, time=" + (t2-t1).ToString());
					}
					catch(Exception exc)
					{					
//						FI.Common.LogWriter.Instance.WriteEventLogEntry("Execute task exception: " + exc.Message);
						if(responseStream!=null)
							responseStream.Close();
						responseStream=new MemoryStream();
						responseStream.WriteByte(3); // error header

						byte[] excBytes=Encoding.Unicode.GetBytes(exc.Message);
						responseStream.Write(excBytes, 0, excBytes.Length);			
					}
				
					// send response
					t1=System.Environment.TickCount;
					_chnManager.SendDataStream(responseStream.ToArray());
					t2=System.Environment.TickCount;
					FI.Common.LogWriter.Instance.WriteEventLogEntry("response sent, len=" + responseStream.Length.ToString() + ", time=" + (t2-t1).ToString());
				
					if(taskStream!=null)
						taskStream.Close();
					if(responseStream!=null)
						responseStream.Close();
				}
				catch(Exception exc)
				{
					FI.Common.LogWriter.Instance.WriteEventLogEntry("Processor exception: " + exc.Message + "\r\n" + exc.StackTrace);
					throw exc;
				}
			}
		}


		private void ExecuteTask(MemoryStream taskStream, MemoryStream responseStream)
		{
			string tag=(string)Serialization.DeserializeValue(taskStream, typeof(string));
			string str=null;

			switch(tag)
			{
				case "BuildCellset":				
					str=BuildCellset(taskStream);
					break;
				
				case "GetReportXml":				
					str=GetReportXml(taskStream);
					break;
				
				case "GetReportSchemaXml":				
					str=GetReportSchemaXml(taskStream);
					break;
				
				case "GetSchemaMembers":				
					str=GetSchemaMembers(taskStream);
					break;
				
				case "GetLevelMembers":				
					str=GetLevelMembers(taskStream);
					break;
				
				case "GetMemberChildren":				
					str=GetMemberChildren(taskStream);
					break;
				
				case "GetMemberParentWithSiblings":				
					str=GetMemberParentWithSiblings(taskStream);
					break;
				
				case "GetMemberGrandParent":				
					str=GetMemberGrandParent(taskStream);
					break;
				
				case "GetMemberParent":				
					str=GetMemberParent(taskStream);
					break;
				
				default:
					throw new Exception("Invalid task tag: " + tag);
			}

			str=(str==null ? string.Empty : str);
			Serialization.SerializeValue(responseStream, str);
		}


		
		public string BuildCellset(MemoryStream data)
		{
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string mdx=Serialization.DeserializeValue(data, typeof(string)) as string;

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.BuildCellset(server, database, mdx);
		}
		
		public string GetReportXml(MemoryStream data)
		{
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string cube=Serialization.DeserializeValue(data, typeof(string)) as string;
			string inReportXml=Serialization.DeserializeValue(data, typeof(string)) as string;

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.GetReportXml(server, database, cube, ref inReportXml);
		}

		public string GetReportSchemaXml(MemoryStream data)
		{
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string cube=Serialization.DeserializeValue(data, typeof(string)) as string;
			string openNodesXml=Serialization.DeserializeValue(data, typeof(string)) as string;

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.GetReportSchemaXml(server, database, cube, ref openNodesXml);
		}

		public string GetSchemaMembers(MemoryStream data)
		{			
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string cube=Serialization.DeserializeValue(data, typeof(string)) as string;
			ArrayList uniqueNames=new ArrayList();
			while(data.Position<data.Length)
				uniqueNames.Add((string)Serialization.DeserializeValue(data, typeof(string)));

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.GetSchemaMembers(server, database, cube, (string[])uniqueNames.ToArray(typeof(string)));
		}

		public string GetLevelMembers(MemoryStream data)
		{
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string cube=Serialization.DeserializeValue(data, typeof(string)) as string;
			string levelUniqueName=Serialization.DeserializeValue(data, typeof(string)) as string;

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.GetLevelMembers(server, database, cube, levelUniqueName);
		}

		public string GetMemberChildren(MemoryStream data)
		{
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string cube=Serialization.DeserializeValue(data, typeof(string)) as string;
			string memUniqueName=Serialization.DeserializeValue(data, typeof(string)) as string;
			bool ifLeafAddItself=(bool)Serialization.DeserializeValue(data, typeof(bool));

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.GetMemberChildren(server, database, cube, memUniqueName, ifLeafAddItself);
		}

		public string GetMemberParentWithSiblings(MemoryStream data)
		{
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string cube=Serialization.DeserializeValue(data, typeof(string)) as string;
			string hierUniqueName=Serialization.DeserializeValue(data, typeof(string)) as string;
			string memUniqueName=Serialization.DeserializeValue(data, typeof(string)) as string;

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.GetMemberParentWithSiblings(server, database, cube, hierUniqueName, memUniqueName);
		}

		public string GetMemberGrandParent(MemoryStream data)
		{
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string cube=Serialization.DeserializeValue(data, typeof(string)) as string;
			string hierUniqueName=Serialization.DeserializeValue(data, typeof(string)) as string;
			string memUniqueName=Serialization.DeserializeValue(data, typeof(string)) as string;

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.GetMemberGrandParent(server, database, cube, hierUniqueName, memUniqueName);
		}

		public string GetMemberParent(MemoryStream data)
		{
			// arguments
			string server=Serialization.DeserializeValue(data, typeof(string)) as string;
			string database=Serialization.DeserializeValue(data, typeof(string)) as string;
			string cube=Serialization.DeserializeValue(data, typeof(string)) as string;
			string hierUniqueName=Serialization.DeserializeValue(data, typeof(string)) as string;
			string memUniqueName=Serialization.DeserializeValue(data, typeof(string)) as string;

			// execute
			XmlCellsetWrapper cst=new XmlCellsetWrapper();
			return cst.GetMemberParent(server, database, cube, hierUniqueName, memUniqueName);
		}


	}
}
