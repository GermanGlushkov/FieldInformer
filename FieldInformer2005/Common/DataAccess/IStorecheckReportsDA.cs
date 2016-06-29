using System;
using System.Collections.Specialized;

namespace FI.Common.DataAccess
{
	public interface IStorecheckReportsDA
	{
		
		Data.FIDataTable ReadReportHeader(decimal UserId, decimal ReportID);

		Data.FIDataTable ReadReportHeaders(decimal UserId);
		
		Data.FIDataTable ReadUsersWithChildReports(decimal ParentReportId , int ParentReportType);

		void  ReadReport(decimal UserID, decimal ReportID, 
			ref decimal ParentReportId, 
			ref string Name, 
			ref string Description,
			ref short SharingStatus,
			ref short MaxSubscriberSharingStatus,
			ref bool IsSelected,
			ref string ProductsXml , ref byte ProductsLogic , ref short Days , ref string FilterXml , ref System.DateTime CacheTimestamp, ref bool CacheExists,
			ref bool InSelOnly , ref bool InBSelOnly, ref byte DataSource,
			ref string OltpDatabase,
			ref byte UndoCount , ref byte RedoCount);

		decimal InsertReport(
			decimal UserID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected,
			string ProductsXml , byte ProductsLogic , short Days , string FilterXml , System.DateTime CacheTimestamp, bool InSelOnly , bool InBSelOnly, byte DataSource
			);

		void SaveReport(
			decimal UserID, 
			decimal ReportID, 
			string ProductsXml , byte ProductsLogic , short Days , string FilterXml , System.DateTime CacheTimestamp, bool InSelOnly , bool InBSelOnly, byte DataSource
			);

		void UpdateReportHeader(
			decimal UserID, 
			decimal ReportID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected,
			DateTime CacheTimestamp);

		void SaveState(decimal ReportId, byte MaxStateCount, 
			string ProductsXml , byte ProductsLogic , short Days , string FilterXml , 
			bool InSelOnly, bool InBSelOnly, byte DataSource,
			ref byte UndoCount);

		void LoadState(decimal ReportId, short StateCode , 
			ref string ProductsXml , ref byte ProductsLogic , ref short Days , ref string FilterXml , 
			ref bool InSelOnly, ref bool InBSelOnly, ref byte DataSource,
			ref byte UndoCount , ref byte RedoCount);
		
		void DeleteReport(decimal UserId , decimal ReportId, bool DenyShared);
				
		void DeleteReportStates(decimal ReportId);

		decimal CreateSharedReport(decimal ParentReportId , decimal SubscriberUserId , int SubscriberSharingStatus);

		void DeleteSharedReport(decimal ParentReportId, decimal ChildReportId, ref short MaxSubscriberSharingStatus);

		Data.FIDataTable GetSppProductsPage(string Database , StringCollection ProdSernList , bool IncludeProdSernList , int StartIndex , int RecordCount , string FilterExpression , string SortExpression);

		void CreateReportCache(decimal ReportId, string Database, StringCollection ProductsSernList, byte ProductsJoinLogic, DateTime StartDate , DateTime EndDate, bool InSelOnly , bool InBSelOnly, byte DataSource);

		void DeleteReportCache(decimal ReportId);

		Data.FIDataTable GetReportPage(decimal ReportId, string Database, byte PageType , int StartIndex , int RecordCount , string FilterExpression , string SortExpression);
	}
}
