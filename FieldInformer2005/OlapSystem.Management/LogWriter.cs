using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace OlapSystem.Management
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
			string eventLogName=AppConfig.EventLogName;
			string eventLogSource=AppConfig.EventLogSource;

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
			System.Diagnostics.EventLog ret=null;
			eventLogName=(eventLogName==null || eventLogName.Trim()=="" ? "FieldInformer.NET" : eventLogName );
			eventLogSource=(eventLogSource==null || eventLogSource.Trim()=="" ? "Undefined" : eventLogSource );

//			if(eventLogName==null || eventLogName.Trim()=="")
//				throw new Exception("Cannot create event log with empty name, please specify EventLogName");
//			if(eventLogSource==null || eventLogSource.Trim()=="")
//				throw new Exception("Cannot create event log with empty source, please specify EventLogSource");

			try
			{
				if (!System.Diagnostics.EventLog.SourceExists(eventLogSource)) 
					System.Diagnostics.EventLog.CreateEventSource(eventLogSource , eventLogName);

				ret=new System.Diagnostics.EventLog();
				ret.Source = eventLogSource;
				ret.Log = eventLogName;
				return ret;
			}
			catch(Exception exc)
			{
				throw exc;
			}			
		}


	





		public void WriteEventLogEntry(string Message)
		{
            if (_eventLog != null)
                _eventLog.WriteEntry(Message);
            else
                throw new Exception("Unable to write to event log : not initialized. \r\nMessage to write:\r\n" + Message);
		}

		public void WriteEventLogEntry(string Message, EventLogEntryType type)
		{
			if(_eventLog!=null)
                _eventLog.WriteEntry(Message, type);
            else
                throw new Exception("Unable to write to event log : not initialized. \r\nMessage to write:\r\n" + Message);
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
