using System;
using System.Data;
using System.Data.SqlClient;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for Contacts.
	/// </summary>
    public class Contacts : MarshalByRefObject, FI.Common.DataAccess.IContactsDA
	{
		public Contacts()
		{
		}

		public FI.Common.Data.FIDataTable ReadContact(decimal UserID , decimal ContactID)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ContactId" , ContactID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadContact" , CommandType.StoredProcedure , parameters , dataTable);
			return dataTable;
		}


		public decimal InsertContact(decimal UserID , string ContactName , string ContactEmail, string DistributionFormat)
		{
			decimal ContactID=-1;

			SqlParameter[] parameters=new SqlParameter[5];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ContactId" , ContactID);
			parameters[1].Direction=ParameterDirection.Output;
			parameters[2]=new SqlParameter("@ContactName" , ContactName);
			parameters[3]=new SqlParameter("@ContactEmail" , ContactEmail);
			parameters[4]=new SqlParameter("@DistributionFormat" , DistributionFormat);
			

			DataBase.Instance.ExecuteCommand("dbo.sproc_InsertContact" , CommandType.StoredProcedure , parameters , null);

			if(parameters[1].Value!=System.DBNull.Value)
				ContactID=(decimal)parameters[1].Value;
			
			return ContactID;
		}


		public void UpdateContact(decimal UserID , decimal ContactID , string ContactName , string ContactEmail, string DistributionFormat)
		{
			SqlParameter[] parameters=new SqlParameter[5];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ContactId" , ContactID);
			parameters[2]=new SqlParameter("@ContactName" , ContactName);
			parameters[3]=new SqlParameter("@ContactEmail" , ContactEmail);
			parameters[4]=new SqlParameter("@DistributionFormat" , DistributionFormat);

			DataBase.Instance.ExecuteCommand("dbo.sproc_UpdateContact" , CommandType.StoredProcedure , parameters , null);
		}


		public void DeleteContact(decimal UserID , decimal ContactID)
		{
			SqlParameter[] parameters=new SqlParameter[2];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@ContactId" , ContactID);

			DataBase.Instance.ExecuteCommand("dbo.sproc_DeleteContact" , CommandType.StoredProcedure , parameters , null);
		}


		public FI.Common.Data.FIDataTable ReadContactsPage(decimal UserID , int StartIndex , int RecordCount , string FilterExpression , string SortExpression)
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			SqlParameter[] parameters=new SqlParameter[6];
			parameters[0]=new SqlParameter("@UserId" , UserID);
			parameters[1]=new SqlParameter("@StartIndex" , StartIndex);
			parameters[2]=new SqlParameter("@RecordCount" , RecordCount);
			parameters[3]=new SqlParameter("@FilterExpression" , FilterExpression ); 
			parameters[4]=new SqlParameter("@SortExpression" , SortExpression); 
			parameters[5]=new SqlParameter("@TotalCount" , dataTable.TotalCount);
			parameters[5].Direction=ParameterDirection.Output;

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadContactsPage" , CommandType.StoredProcedure , parameters , dataTable);
			
			if(parameters[5].Value!=System.DBNull.Value)
				dataTable.TotalCount=(int)parameters[5].Value;

			return dataTable;
		}


	}
}
