using System;
using FI.Common.DataAccess;

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


		public IUsersDA GetUsersDA()
		{			
			return (IUsersDA)Activator.GetObject(typeof(IUsersDA), __dataAccessUrl + "IUsersDA");
		}

		public IContactsDA GetContactsDA()
		{
			return (IContactsDA)Activator.GetObject(typeof(IContactsDA), __dataAccessUrl + "IContactsDA");
		}

		public IDistributionsDA GetDistributionsDA()
		{
			return (IDistributionsDA)Activator.GetObject(typeof(IDistributionsDA), __dataAccessUrl + "IDistributionsDA");
		}

		public IOlapSystemDA GetOlapSystemDA()
		{
			IOlapSystemDA ret= (IOlapSystemDA)Activator.GetObject(typeof(IOlapSystemDA), __dataAccessUrl + "IOlapSystemDA");
			return ret;
		}

		public IOlapReportsDA GetOlapReportsDA()
		{
			return (IOlapReportsDA)Activator.GetObject(typeof(IOlapReportsDA), __dataAccessUrl + "IOlapReportsDA");
		}

		public IStorecheckReportsDA GetStorecheckReportsDA()
		{
			return (IStorecheckReportsDA)Activator.GetObject(typeof(IStorecheckReportsDA), __dataAccessUrl + "IStorecheckReportsDA");
		}

		public ICustomSqlReportsDA GetCustomSqlReportsDA()
		{
			return (ICustomSqlReportsDA)Activator.GetObject(typeof(ICustomSqlReportsDA), __dataAccessUrl + "ICustomSqlReportsDA");
		}

		public ICustomMdxReportsDA GetCustomMdxReportsDA()
		{
			return (ICustomMdxReportsDA)Activator.GetObject(typeof(ICustomMdxReportsDA), __dataAccessUrl + "ICustomMdxReportsDA");
		}

	}
}
