using System;

namespace FI.Common.DataAccess
{
	public interface IDistributionsDA
	{
		
		Data.FIDataTable ReadDistribution(decimal UserID , decimal DistributionID);

		Data.FIDataTable ReadDistributions(decimal UserID);

        int GetQueuedDistributionsCount(decimal companyId);

		decimal ReadNextQueuedDistribution(decimal companyId);

		decimal InsertDistribution(decimal UserID , decimal ReportID , decimal ContactID , int ReportType , string FrequencyType , string FrequencyValue, int Format);

		void UpdateDistribution(decimal UserID , decimal DistributionID , decimal ReportID , decimal ContactID , int ReportType , string FrequencyType , string FrequencyValue, int Format);

		void DeleteDistribution(decimal DistributionID);

		void DeleteDistributionsByContact(decimal UserID , decimal ContactID);

		void DeleteDistributionsByReport(decimal ReportID , int ReportType);

		Data.FIDataTable ReadDistributionsWithContactsPage(decimal UserID, decimal ReportID , int ReportType , int StartIndex , int RecordCount , string FilterExpression , string SortExpression);

		Data.FIDataTable ReadReportDistributionLog(decimal UserID, decimal ReportID , int ReportType , int StartIndex , int RecordCount , string FilterExpression , string SortExpression);

		Data.FIDataTable ReadDistributionLog(decimal UserID , int StartIndex , int RecordCount , string FilterExpression , string SortExpression);

		Data.FIDataTable ReadDistributionQueue(decimal CompanyId, int StartIndex , int RecordCount , string FilterExpression , string SortExpression);

		Data.FIDataTable GetDistributionInfo(bool checkOnly);

		decimal GetDistributionOwnerId(decimal distributionId);

		decimal[] GetActiveDistributionQueueItems(decimal distributionId);

		void GetQueueItemInfo(decimal queueItemId, out decimal distributionId, out string status, out DateTime timestamp);

        DateTime GetCurrentTimestamp();

		void EnqueueReportDistributions(decimal ReportID , int ReportType);

		void EnqueueDistribution(decimal distributionId, string message);

		bool CancelQueuedItem(decimal queueItemId, string message);

		void WriteDistributionQueueOk(decimal queueItemId , bool isFromCache);

		void WriteDistributionQueueError(decimal queueItemId , string message);

		void WriteDistributionQueueExecuting(decimal queueItemId , Guid taskGuid);

		void WriteDistributionQueueCanceled(decimal queueItemId, string message);

	}
}
