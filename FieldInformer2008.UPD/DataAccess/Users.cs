using System;
using System.Data;
using System.Data.SqlClient;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for Contacts.
	/// </summary>
    public class Users : MarshalByRefObject, FI.Common.DataAccess.IUsersDA
	{
		public Users()
		{
		}

		public FI.Common.Data.FIDataTable ReadUser(decimal UserID)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@UserId" , UserID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadUserByUserId" , CommandType.StoredProcedure , parameters , dataTable);
			return dataTable;
		}


		public FI.Common.Data.FIDataTable ReadUser(string CompanyNameShort, string Logon, string Password)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[3];
			parameters[0]=new SqlParameter("@CompanyNameShort" , CompanyNameShort);
			parameters[1]=new SqlParameter("@Logon" , Logon);
			parameters[2]=new SqlParameter("@Password" , Password);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadUserByAuthentication" , CommandType.StoredProcedure , parameters , dataTable);
			return dataTable;
		}


		public string GetUserCurrentSession(decimal UserId)
		{
			string sessionId="";

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserId);
			parameters[1]=new SqlParameter("@SessionId" ,SqlDbType.VarChar , 50);
			parameters[1].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_GetUserCurrentSession" , CommandType.StoredProcedure , parameters , null);
			
			if(parameters[1].Value!=System.DBNull.Value)
				sessionId=(string)parameters[1].Value;
			
			return sessionId;
		}


		public FI.Common.Data.FIDataTable ReadUsers()
		{
			return ReadUsers(0);
		}

		public FI.Common.Data.FIDataTable ReadUsers(decimal CompanyId)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@CompanyId" , CompanyId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadUsers" , CommandType.StoredProcedure , parameters , dataTable);
			return dataTable;
		}


		public decimal InsertUser(decimal CompanyId ,  string Logon, string Password, DateTime PasswordTimestamp, string Name, string Email, bool IsAdmin, byte CssStyle)
		{
			decimal id=0;

			SqlParameter[] parameters=new SqlParameter[9];
			parameters[0]=new SqlParameter("@Id" , id);
			parameters[0].Direction=ParameterDirection.Output;
			parameters[1]=new SqlParameter("@CompanyId" , CompanyId);
			parameters[2]=new SqlParameter("@Logon" , Logon);
			parameters[3]=new SqlParameter("@Password" , Password);
			parameters[4]=new SqlParameter("@PasswordTimestamp" , PasswordTimestamp);
			parameters[5]=new SqlParameter("@Name" , Name);
			parameters[6]=new SqlParameter("@Email" , Email);
			parameters[7]=new SqlParameter("@IsAdmin" , IsAdmin);
			parameters[8]=new SqlParameter("@CssStyle" , CssStyle);

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertUser" , CommandType.StoredProcedure , parameters , null);

			if(parameters[0].Value!=System.DBNull.Value)
				id=(decimal)parameters[0].Value;
			
			return id;
		}


		public void UpdateUser(decimal Id ,  string Logon, string Password, DateTime PasswordTimestamp, string Name, string Email, bool IsAdmin, byte CssStyle)
		{
			SqlParameter[] parameters=new SqlParameter[8];
			parameters[0]=new SqlParameter("@Id" , Id);
			parameters[1]=new SqlParameter("@Logon" , Logon);
			parameters[2]=new SqlParameter("@Password" , Password);
			parameters[3]=new SqlParameter("@PasswordTimestamp" , PasswordTimestamp);
			parameters[4]=new SqlParameter("@Name" , Name);
			parameters[5]=new SqlParameter("@Email" , Email);
			parameters[6]=new SqlParameter("@IsAdmin" , IsAdmin);
			parameters[7]=new SqlParameter("@CssStyle" , CssStyle);

			DataBase.Instance.ExecuteCommand("dbo.sproc_UpdateUser" , CommandType.StoredProcedure , parameters , null);
		}


		public void UpdateUserSession(decimal Id , string ConnectionAddress , string SessionId, bool IsLoggedIn)
		{
			SqlParameter[] parameters=new SqlParameter[4];
			parameters[0]=new SqlParameter("@Id" , Id);
			parameters[1]=new SqlParameter("@ConnectionAddress" , ConnectionAddress);
			parameters[2]=new SqlParameter("@SessionId" , SessionId);
			parameters[3]=new SqlParameter("@IsLoggedIn" , IsLoggedIn);

			DataBase.Instance.ExecuteCommand("dbo.sproc_UpdateUserSession" , CommandType.StoredProcedure , parameters , null);
		}

		public void DeleteUser(decimal UserId)
		{
			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0]=new SqlParameter("@UserId" , UserId);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteUser" , CommandType.StoredProcedure , parameters , null);
		}




		public void InsertPageHitAudit(decimal UserId, decimal CompanyId , string ConnectionAddress , string SessionId , System.DateTime Timestamp)
		{
			SqlParameter[] parameters=new SqlParameter[5];
			parameters[0]=new SqlParameter("@UserId" , UserId);
			parameters[1]=new SqlParameter("@CompanyId" , CompanyId);
			parameters[2]=new SqlParameter("@ConnectionAddress" , ConnectionAddress);
			parameters[3]=new SqlParameter("@SessionId" , SessionId);
			parameters[4]=new SqlParameter("@Timestamp" , Timestamp);

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertAudit" , CommandType.StoredProcedure , parameters , null);
		}



		
		/// <summary>
		/// Method is here for historical reasons in order to use regitered wellknown object in configuratoin file
		/// </summary>
		public decimal GetCompanyIdByShortName(string ShortName)
		{
			DataTable dt=new DataTable();
			
			ShortName=(ShortName==null ? "" : ShortName);
			DataBase.Instance.ExecuteCommand(
				"select id from tcompany where short_name='" + ShortName.Replace("'", "''") + "'" , 
				CommandType.Text , null , dt);
			
			return (decimal)(dt==null || dt.Rows.Count==0 ? decimal.Zero : dt.Rows[0][0]);
		}

		/// <summary>
		/// Method is here for historical reasons in order to use regitered wellknown object in configuratoin file
		/// </summary>
		public DataTable ReadCompanies()
		{
			DataTable dt=new DataTable();
			
			DataBase.Instance.ExecuteCommand(
				"select id as Id, short_name as ShortName, name as Name, olap_server as OlapServer, olap_db as OlapDb, ping as Ping from tcompany" , 
				CommandType.Text , null , dt);
			
			return dt;
		}

	}
}
