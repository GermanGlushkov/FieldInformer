using System;
using FI.DataAccess;

namespace FI.BusinessObjects
{
	/// <summary>
	/// Summary description for DataAccessFactory.
	/// </summary>
	public class DataAccessFactory
	{
		private static string __dataAccessUrl;

		// singleton pattern
		private DataAccessFactory()
		{	
//			__dataAccessUrl="gtcp://127.0.0.1:8070/";
			__dataAccessUrl=FI.Common.AppConfig.ReadSetting("DataAccessUrl", "tcp://localhost:8070");
			if(!__dataAccessUrl.EndsWith("/"))
				__dataAccessUrl+="/";
		}
		public static readonly DataAccessFactory Instance=new DataAccessFactory();
		// singleton pattern


		public Users GetUsersDA()
		{			
			return (Users)Activator.GetObject(typeof(Users), __dataAccessUrl + "FI.DataAccess.Users");
		}

		public Contacts GetContactsDA()
		{
			return (Contacts)Activator.GetObject(typeof(Contacts), __dataAccessUrl + "FI.DataAccess.Contacts");
		}

		public Distributions GetDistributionsDA()
		{
			return (Distributions)Activator.GetObject(typeof(Distributions), __dataAccessUrl + "FI.DataAccess.Distributions");
		}

		public OlapSystem GetOlapSystemDA()
		{
			OlapSystem ret= (OlapSystem)Activator.GetObject(typeof(OlapSystem), __dataAccessUrl + "FI.DataAccess.OlapSystem");
			return ret;
		}

		public OlapReports GetOlapReportsDA()
		{
			return (OlapReports)Activator.GetObject(typeof(OlapReports), __dataAccessUrl + "FI.DataAccess.OlapReports");
		}

		public StorecheckReports GetStorecheckReportsDA()
		{
			return (StorecheckReports)Activator.GetObject(typeof(StorecheckReports), __dataAccessUrl + "FI.DataAccess.StorecheckReports");
		}

		public CustomSqlReports GetCustomSqlReportsDA()
		{
			return (CustomSqlReports)Activator.GetObject(typeof(CustomSqlReports), __dataAccessUrl + "FI.DataAccess.CustomSqlReports");
		}

		public CustomMdxReports GetCustomMdxReportsDA()
		{
			return (CustomMdxReports)Activator.GetObject(typeof(CustomMdxReports), __dataAccessUrl + "FI.DataAccess.CustomMdxReports");
		}

	}
}
