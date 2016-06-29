using System;
using System.IO;
using System.Reflection;

namespace FI.UI.ConsoleClient
{


	public class MsmdarchWrapper
	{
		public static string DefaultOlapDbName="DBSALESPP";

		public MsmdarchWrapper()
		{
		}

		public static void ArchiveOlapDb(string dbName, string archivePath)
		{
			dbName=(dbName==null ? DefaultOlapDbName  :dbName);
			archivePath=(archivePath==null ? GetDefaultArchivePath() : archivePath);
			string msmdarchDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName ;
			string args=string.Format(@" /a ""{0}"" ""{1}"" ""{2}"" ""{3}""",  GetComputerName() , GetRootDir(true), dbName, archivePath);

			System.Diagnostics.ProcessStartInfo psi=new System.Diagnostics.ProcessStartInfo("msmdarch.exe", args);
			psi.CreateNoWindow=true;
			psi.WorkingDirectory=msmdarchDir;
			System.Diagnostics.Process process=System.Diagnostics.Process.Start(psi);
			process.WaitForExit();
			process.Close();
		}

		public static void RestoreOlapDb(string archivePath)
		{
			archivePath=(archivePath==null ? GetDefaultArchivePath() : archivePath);
			string msmdarchDir=Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName ;
			string args=string.Format(@" /r ""{0}"" ""{1}"" ""{2}""",  GetComputerName() , GetRootDir(true), archivePath);

			System.Diagnostics.ProcessStartInfo psi=new System.Diagnostics.ProcessStartInfo("msmdarch.exe", args);
			psi.CreateNoWindow=true;
			psi.WorkingDirectory=msmdarchDir;
			System.Diagnostics.Process process=System.Diagnostics.Process.Start(psi);
			process.WaitForExit();
			process.Close();
		}

		public static string GetComputerName()
		{
			return System.Environment.MachineName;
		}

		public static string GetDefaultArchivePath()
		{
			return GetRootDir(true) + DefaultOlapDbName + ".CAB";
		}

		public static string GetRootDir(bool appendSlash)
		{
			string ret=null;
			Microsoft.Win32.RegistryKey key=Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\OLAP Server\CurrentVersion");
			if(key!=null)
				ret=(string)key.GetValue("RootDir", null);

			if(ret!=null)
			{
				if(ret.EndsWith(@"\"))
					ret=(appendSlash ? ret : ret.Substring(0, ret.Length-1));
				else
					ret=(appendSlash ? ret + @"\" : ret);
			}
			return ret;
		}
	}
}
