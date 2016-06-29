using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace FI.Common
{
	/// <summary>
	/// Summary description for LogWriter.
	/// </summary>
	public class LogWriter
	{
		System.Diagnostics.EventLog _eventLog;

		// singleton pattern
		private LogWriter()
		{
			string eventLogName=FI.Common.AppConfig.EventLogName;
			string eventLogSource=FI.Common.AppConfig.EventLogSource;

			try
			{
				_eventLog=InitEventLog(eventLogName, eventLogSource);
			}
			catch(Exception exc)
			{
				_eventLog=null;

				throw exc;
			}
		}
		public static readonly LogWriter Instance=new LogWriter();
		// singleton pattern


		public static void InitCommonEventLogs()
		{
			InitEventLog("FieldInformer.NET", "Web");
			InitEventLog("FieldInformer.NET", "Service");
			InitEventLog("FieldInformer.NET", "ConsoleClient");	
			InitEventLog("FieldInformer.NET", "OlapProcessor");			
		}

		private static System.Diagnostics.EventLog InitEventLog(string eventLogName, string eventLogSource)
		{
			if(eventLogName==null || eventLogName.Trim()=="")
				return null;
			if(eventLogSource==null || eventLogSource.Trim()=="")
				return null;

			try
			{
				if (!System.Diagnostics.EventLog.SourceExists(eventLogSource)) 
					System.Diagnostics.EventLog.CreateEventSource(eventLogSource , eventLogName);

				System.Diagnostics.EventLog ret=new System.Diagnostics.EventLog();
				ret.Source = eventLogSource;
				ret.Log = eventLogName;
				return ret;
			}
			catch(Exception exc)
			{
				throw exc;
			}			
		}


		private void WriteLine(string Message, string Path)
		{
			FileStream fs=null;
			StreamWriter sw=null; 

			try
			{
				fs = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
				sw = new StreamWriter(fs);
				sw.BaseStream.Seek(0, System.IO.SeekOrigin.End);
				sw.WriteLine(System.DateTime.Now.ToShortDateString()+ " " + System.DateTime.Now.ToShortTimeString() + " \t *** " + Message);
				sw.Flush();
			}
			finally
			{
				if(sw!=null)
					sw.Close();

				if(fs!=null)
					fs.Close();
			}
		}


		public void WriteException(Exception exc, string Path)
		{
			WriteLine(" Exception: " + exc.Source +" : " + exc.Message + "\r\n Stack trace: \r\n" + exc.StackTrace, Path);
		}

		public void WriteException(Exception exc)
		{
			string path=FI.Common.AppConfig.LogPath;
			if(path=="")
				path=Assembly.GetExecutingAssembly().FullName + ".log";

			WriteException(exc, path);
		}

		public void WriteLogEntry(string Message, string Path)
		{
			WriteLine(" Log Entry: " + Message, Path);
		}

		public void WriteLogEntry(string Message)
		{
			string path=FI.Common.AppConfig.LogPath;
			if(path=="")
				path=Assembly.GetExecutingAssembly().FullName + ".log";

			WriteLine(" Log Entry: " + Message,  path);
		}






		public void WriteEventLogEntry(string Message)
		{
			if(_eventLog!=null)
				_eventLog.WriteEntry(Message);
			else
				this.WriteLogEntry(Message); // write into file
		}

		public void WriteEventLogEntry(string Message, EventLogEntryType type)
		{
			if(_eventLog!=null)
				_eventLog.WriteEntry(Message, type);
			else
				this.WriteLogEntry(Message); // write into file
		}

		public void WriteEventLogEntry(Exception exc)
		{
			WriteEventLogEntry(exc, EventLogEntryType.Error);
		}

		public void WriteEventLogEntry(Exception exc, EventLogEntryType type)
		{
			this.WriteEventLogEntry(MessageFromException(exc), type);	
		}

		public string MessageFromException(Exception exc)
		{
			if(exc==null)
				return "";

			return (
				(exc.InnerException!=null? exc.InnerException : exc).Message + 
				"\r\n *****Stack trace*****\r\n" + exc.StackTrace
				);	
		}


	}
}
