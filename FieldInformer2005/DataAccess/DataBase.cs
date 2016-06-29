using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for SQL.
	/// </summary>
	public sealed class DataBase
	{
		// singleton pattern
		private DataBase(){}
		public static readonly DataBase Instance=new DataBase();
		// singleton pattern


		private void ExecuteSqlResource(string name)
		{
			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(DataBase), name);
			StreamReader reader = new StreamReader(stream);

			try
			{
				string s = reader.ReadToEnd();

				// execute statents delimited by "GO" command
				int start=0;
				bool isquote=false;
				for(int i=2;i<s.Length;i++)
				{					
					// check if inside quote (also check quote escape)
					isquote=(s[i-1]!='\'' && s[i]=='\'');

					// if GO and not inside quote, execute
					if( 
						isquote==false &&
						(s[i-2]==' ' || s[i-2]=='\t' || s[i-2]=='\r' || s[i-2]=='\n') &&
						(s[i-1]=='G' || s[i-1]=='g') && 
						(s[i]=='O' || s[i]=='o') && 
						(i==s.Length-1 || s[i+1]==' ' || s[i+1]=='\t' || s[i+1]=='\r' || s[i+1]=='\n'))
					{
						string sql=s.Substring(start, i-start-2).Trim(new char[] {' ', '\t', '\r', '\n'});
						if(sql!=string.Empty)
							ExecuteCommand(sql, CommandType.Text, null, null);

						start=i+1;
					}
				}

			}
			catch(Exception exc)
			{
				throw exc;
			}
			finally
			{
				reader.Close();
			}
		}

		public void VerifyDbSchema()
		{
			string checkNewDbSQL=@"select top 1 1 from dbo.sysobjects where id = object_id(N'[dbo].[tcompany]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";

			object o=this.ExecuteScalar(checkNewDbSQL, null);
			if(o==null || o==DBNull.Value)
			{
				ExecuteSqlResource("DB.DBFINF_tables.sql");
				ExecuteSqlResource("DB.DBFINF_sprocs.sql");
				ExecuteSqlResource("DB.DBFINF_views.sql");
			}
			
			ExecuteSqlResource("DB.DBFINF_updates.sql");

			Common.LogWriter.Instance.WriteEventLogEntry("DB schema verified");
		}


		public FI.Common.Data.FIDataTable ExecutePagedCommand(int StartIndex, int RecordCount, SqlParameter[] SelectColumns, string Sql, string FilterExpression , string SortExpression)
		{

			// ------------- construct select list
			string selectList="";
			foreach(SqlParameter column in SelectColumns)
				selectList= selectList + column.ParameterName + ", ";

			// remove last coma and space
			selectList=selectList.Remove(selectList.Length-2,2);


			// ------------- construct variables definitions
			string varDecl="";
			foreach(SqlParameter column in SelectColumns)
			{
				varDecl=varDecl + "\r\nDECLARE @" + column.ParameterName + " "  + column.SqlDbType.ToString();
				if(column.Size>0)
					varDecl=varDecl + "(" + column.Size +")";
			}


			// ------------- construct variables list
			string varList="";
			foreach(SqlParameter column in SelectColumns)
				varList= varList + "@" + column.ParameterName + ", ";

			// remove last coma and space
			varList=varList.Remove(varList.Length-2,2);


			// ------------- construct cusor
			string cursorSql=@"
			DECLARE temp_cursor CURSOR STATIC FOR
			SELECT " + selectList + @"
			FROM 
			(" + Sql + ") TBL";

			if(FilterExpression!=null && FilterExpression!="")
				cursorSql=cursorSql + " WHERE  (" + FilterExpression + ")";

			if(SortExpression!=null && SortExpression!="")
				cursorSql=cursorSql + " ORDER BY " + SortExpression ;




			// ------------- construct temp table
			string tableSql=@"
				if object_id('tempdb..#tmp') is not null
					drop table #tmp

				create table #tmp
				(
					[_serno] int IDENTITY(1,1) PRIMARY KEY,";

			foreach(SqlParameter column in SelectColumns)
			{
				tableSql= tableSql + column.ParameterName + " " + column.SqlDbType.ToString() ;
				if(column.Size>0)
					tableSql=tableSql + "(" + column.Size +"), ";
				else
					tableSql=tableSql + ", ";
			}
			// remove last coma and space
			tableSql=tableSql.Remove(tableSql.Length-2,2);

			tableSql=tableSql + @"
				)
			";




			// ------------- construct sql
			string sql=@"

				SET NOCOUNT ON

				" + varDecl + @"

				" + tableSql + @"

				" + cursorSql + @"

				DECLARE @i int
				DECLARE @StartIndex int
				DECLARE @RecordCount int
				DECLARE @TotalCount int
				SET @i=0
				SET @StartIndex=" + StartIndex + @"+1
				SET @RecordCount=" + RecordCount + @"

				OPEN temp_cursor

				FETCH ABSOLUTE @StartIndex from temp_cursor INTO " + varList + @"

				WHILE @@FETCH_STATUS=0 AND @i<@RecordCount
					BEGIN
							INSERT INTO #tmp(" + selectList + @")
								VALUES(" + varList + @")

							FETCH NEXT from temp_cursor INTO " + varList + @"
							SET @i = @i + 1
					END

				-- WORKS FOR STATIC CURSORS ONLY !!
				SET @TotalCount = @@CURSOR_ROWS

				CLOSE temp_cursor
				DEALLOCATE temp_cursor



				SELECT 
					" + selectList + @"
					FROM #tmp
					order by [_serno]

				SELECT @TotalCount as TotalCount

				DROP TABLE #tmp
			";

			// get data
			DataSet dataSet=new DataSet();
			FI.Common.Data.FIDataTable resultTable=new FI.Common.Data.FIDataTable();
			FI.Common.Data.FIDataTable countTable=new FI.Common.Data.FIDataTable();
			dataSet.Tables.Add(resultTable);
			dataSet.Tables.Add(countTable);
			ExecuteCommand(sql , CommandType.Text , null, dataSet);

			// second table must have total count
			int totalCount=(int)dataSet.Tables[1].Rows[0][0];

			//first table is result
			FI.Common.Data.FIDataTable result=(FI.Common.Data.FIDataTable)dataSet.Tables[0];
			result.TotalCount=totalCount;

			return result;

		}


		public object ExecuteScalar(string sql, SqlParameter[] Parameters)
		{
			SqlConnection conn=new SqlConnection(FI.Common.AppConfig.DA_ConnectionString);
			SqlCommand cmd=new SqlCommand(sql);
			cmd.CommandTimeout=Common.AppConfig.DA_CommandTimeout;
			cmd.Connection=conn;

			if(Parameters!=null)
			{
				foreach(SqlParameter parameter in Parameters)
					cmd.Parameters.Add(parameter);
			}

			try
			{
				conn.Open();
				return cmd.ExecuteScalar();
			}
			finally
			{
				cmd.Dispose();
				conn.Close();
			}
		}

		public int ExecuteCommand(string Command , CommandType CommandType , SqlParameter[] Parameters , object Result)
		{
			return ExecuteCommand(Command , FI.Common.AppConfig.DA_ConnectionString  , CommandType , Parameters , Result);
		}

		public int ExecuteCommand(string Command , string ConnectionString, CommandType CommandType , SqlParameter[] Parameters , object Result)
		{
			int ret=0;

			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand command=new SqlCommand(Command);
			command.CommandTimeout=Common.AppConfig.DA_CommandTimeout;
			command.Connection=conn;
			command.CommandType=CommandType;

			if(Parameters!=null)
			{
				foreach(SqlParameter parameter in Parameters)
					command.Parameters.Add(parameter);
			}

			try
			{
				if(Result==null)
				{
					conn.Open();
					ret=command.ExecuteNonQuery();
				}
				else if( Result.GetType()==typeof(DataTable) ||  Result.GetType().IsSubclassOf(typeof(DataTable)) )
				{
					DataTable dataTable=(DataTable)Result;
					using (SqlDataAdapter adapter=new SqlDataAdapter(command))
					{
						ret=adapter.Fill(dataTable);
					}
				}
				else if(Result.GetType()==typeof(DataSet) ||  Result.GetType().IsSubclassOf(typeof(DataSet)))
				{
					DataSet dataSet=(DataSet)Result;
					using (SqlDataAdapter adapter=new SqlDataAdapter(command))
					{
						for(int i=0; i<dataSet.Tables.Count ; i++)
						{
							adapter.TableMappings.Add("Table" + (i==0 ? "" : i.ToString()) ,  dataSet.Tables[i].TableName );
						}

						if(adapter.TableMappings.Count==0)
						{
							adapter.MissingMappingAction=System.Data.MissingMappingAction.Passthrough;
							adapter.MissingSchemaAction=System.Data.MissingSchemaAction.Add;
						}
						ret=adapter.Fill(dataSet);
					}
				}

			}
			catch(Exception exc)
			{
				throw exc;
			}
			finally
			{
				command.Dispose();
				conn.Close();
			}

			return ret;
		}


	}
}
