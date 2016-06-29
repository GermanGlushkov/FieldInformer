using System;

namespace FI.Common.DataAccess
{
	public interface ICustomSqlReportsDA
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
			ref string Sql , ref string Xsl , ref byte UndoCount , ref byte RedoCount);

		decimal InsertReport(
			decimal UserID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected,
			string Sql,
			string Xsl);

		void SaveReport(
			decimal UserID, 
			decimal ReportID, 
			string Sql,
			string Xsl);

		void UpdateReportHeader(
			decimal UserID, 
			decimal ReportID, 
			decimal ParentReportID, 
			byte SharingStatus, 
			string Name, 
			string Description,
			bool IsSelected);

		void SaveState(decimal ReportId, byte MaxStateCount, string Sql, string Xsl , ref byte UndoCount);

		void LoadState(decimal ReportId, short StateCode , ref string Sql , ref string Xsl, ref byte UndoCount , ref byte RedoCount);
		
		void DeleteReport(decimal UserId , decimal ReportId, bool DenyShared);
				
		void DeleteReportStates(decimal ReportId);

		decimal CreateSharedReport(decimal ParentReportId , decimal SubscriberUserId , int SubscriberSharingStatus);

		void DeleteSharedReport(decimal ParentReportId, decimal ChildReportId, ref short MaxSubscriberSharingStatus);

        Data.FIDataTable ReadReportResult(string Sql, string Database);

        object ExecuteScalarOLTP(string Sql, string Database);

	}
}
