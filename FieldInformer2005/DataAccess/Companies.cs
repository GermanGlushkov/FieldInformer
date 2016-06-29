using System;
using System.Data;
using System.Data.SqlClient;

namespace FI.DataAccess
{
	/// <summary>
	/// Summary description for Contacts.
	/// </summary>
	public class Companies:MarshalByRefObject
	{
		public Companies()
		{
		}


		public FI.Common.Data.FIDataTable ReadCompanies()
		{
			FI.Common.Data.FIDataTable dataTable=new FI.Common.Data.FIDataTable();

			DataBase.Instance.ExecuteCommand("dbo.sproc_LoadCompanies" , CommandType.StoredProcedure , null , dataTable);
			return dataTable;
		}

	}
}
