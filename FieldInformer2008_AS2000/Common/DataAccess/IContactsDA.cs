using System;

namespace FI.Common.DataAccess
{
	public interface IContactsDA
	{
		Data.FIDataTable ReadContact(decimal UserID , decimal ContactID);

		decimal InsertContact(decimal UserID , string ContactName , string ContactEmail, string DistributionFormat);

		void UpdateContact(decimal UserID , decimal ContactID , string ContactName , string ContactEmail, string DistributionFormat);

		void DeleteContact(decimal UserID , decimal ContactID);

		Data.FIDataTable ReadContactsPage(decimal UserID , int StartIndex , int RecordCount , string FilterExpression , string SortExpression);
		
	}
}
