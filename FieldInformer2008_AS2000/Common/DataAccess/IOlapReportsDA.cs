using System;

namespace FI.Common.DataAccess
{
    public interface IOlapReportsDA
    {

        Data.FIDataTable ReadReportHeader(decimal UserId, decimal ReportID);

        Data.FIDataTable ReadReportHeaders(decimal UserId);

        Data.FIDataTable ReadUsersWithChildReports(decimal ParentReportId, int ParentReportType);

        void ReadReport(decimal UserID, decimal ReportID,
            ref decimal ParentReportId,
            ref string Name,
            ref string Description,
            ref short SharingStatus,
            ref short MaxSubscriberSharingStatus,
            ref bool IsSelected,
            ref byte GraphType,
            ref string GraphTheme,
            ref int GraphOptions,
            ref short GraphWidth,
            ref short GraphHeight,
            ref int GraphNum1,
            ref string SchemaServer,
            ref string SchemaDatabase,
            ref string SchemaName,
            ref string ReportXml, ref string SchemaXml, ref string OpenNodesXml, ref byte UndoCount, ref byte RedoCount);

        decimal InsertReport(
            decimal UserID,
            decimal ParentReportID,
            byte SharingStatus,
            string Name,
            string Description,
            bool IsSelected,
            byte GraphType,
            string GraphTheme,
            int GraphOptions,
            short GraphWidth,
            short GraphHeight,
            int GraphNum1,
            string ReportXml,
            string OpenNodesXml);

        void SaveReport(
            decimal UserID,
            decimal ReportID,
            string ReportXml,
            string OpenNodesXml);

        void UpdateReportHeader(
            decimal UserID,
            decimal ReportID,
            decimal ParentReportID,
            byte SharingStatus,
            string Name,
            string Description,
            bool IsSelected,
            string OpenNodesXml,
            byte GraphType,
            string GraphTheme,
            int GraphOptions,
            short GraphWidth,
            short GraphHeight,
            int GraphNum1
            );

        void SaveState(decimal ReportId, byte MaxStateCount, string ReportXml, ref byte UndoCount);

        string LoadState(decimal ReportId, short StateCode, ref byte UndoCount, ref byte RedoCount);

        void DeleteReport(decimal UserId, decimal ReportId, bool DenyShared);

        void DeleteReportStates(decimal ReportId);

        decimal CreateSharedReport(decimal ParentReportId, decimal SubscriberUserId, int SubscriberSharingStatus);

        void DeleteSharedReport(decimal ParentReportId, decimal ChildReportId, ref short MaxSubscriberSharingStatus);

    }
}
