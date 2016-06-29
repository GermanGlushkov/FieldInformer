using System;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace Setup.PlugIn
{
	/// <summary>
	/// Summary description for InstallSql.
	/// </summary>
	public class InstallSql
	{
		public InstallSql()
		{
		}

		private SqlConnection _conn=new SqlConnection();

		public void Connect(string Server, string Database , string User , string Password)
		{
			try
			{
				_conn.ConnectionString="server=" + Server + "; User ID=" + User + "; Password=" + Password + "; Database=" + Database + ";Connect Timeout=30;";
				_conn.Open();
			}
			catch(SqlException exc)
			{
				string msg="Sql Connect: " + exc.Message;
				throw new Exception(msg);
			}
		}

		public void Disconnect()
		{
			if(_conn!=null && _conn.State==ConnectionState.Open)
				_conn.Close();
		}

		public string ExecuteSqlFile(string FolderName, string FileName, bool ThrowErrors)
		{
			return ExecuteSql(GetSqlFileText(FolderName, FileName) , ThrowErrors);
		}


		public string ExecuteSql(string Sql, bool ThrowErrors)
		{
			string errString="";

			string[] sqls=SplitSqlBatch(Sql);
			for(int i=0;i<sqls.Length;i++)
			{
				SqlCommand cmd=new SqlCommand();
				cmd.CommandTimeout=1800;
				cmd.Connection=_conn;
				cmd.CommandText=sqls[i];
				if(cmd.CommandText==null || cmd.CommandText.Trim()=="")
					return "";

				try
				{
					cmd.ExecuteNonQuery();
				}
				catch(SqlException exc)
				{
					string msg="Sql Execute: " + exc.Message + "\r\nQuery: " + sqls[i];

					if(ThrowErrors)
						throw new Exception(msg);
					else
						errString=errString + msg + "\r\n";
				}
			}	
			
			return errString;
		}

		public string[] SplitSqlBatch(string Sql)
		{
			if(Sql.Length>2 && Sql.Substring(Sql.Length-2,2).ToUpper()=="GO")
				Sql=Sql.Remove(Sql.Length-2,2);

			Sql=System.Text.RegularExpressions.Regex.Replace(Sql , "\t" , " " , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			Sql=System.Text.RegularExpressions.Regex.Replace(Sql , " GO " , "\r\nGO\r\n" , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			Sql=System.Text.RegularExpressions.Regex.Replace(Sql , "\r\nGO " , "\r\nGO\r\n" , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			Sql=System.Text.RegularExpressions.Regex.Replace(Sql , " GO\r\n" , "\r\nGO\r\n" , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			return System.Text.RegularExpressions.Regex.Split(Sql , "\r\nGO\r\n" , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		}

		public string GetSqlFileText(string FolderName, string FileName)
		{
			try
			{
				string dir=Assembly.GetExecutingAssembly().Location;
				string path=Directory.GetParent(dir).FullName + @"\" + FolderName + @"\" + FileName;
				StreamReader sr=new StreamReader(path);
				string result=sr.ReadToEnd();
				sr.Close();
				return result;
			}
			catch(Exception exc)
			{
				string msg="Sql GetSqlFileText (" + FileName + "): " + exc.Message;
				throw new Exception(msg);
			}
		}

	}
}
