using System;


namespace FI.Common.DataAccess
{
	public interface IUsersDA
	{
		FI.Common.Data.FIDataTable ReadUser(decimal UserID);

		FI.Common.Data.FIDataTable ReadUser(string CompanyNameShort, string Logon, string Password);

		string GetUserCurrentSession(decimal UserId);

		FI.Common.Data.FIDataTable ReadUsers();

		FI.Common.Data.FIDataTable ReadUsers(decimal CompanyId);

		decimal InsertUser(decimal CompanyId ,  string Logon, string Password, DateTime PasswordTimestamp, string Name, string Email, bool IsAdmin, byte CssStyle);

		void UpdateUser(decimal Id ,  string Logon, string Password, DateTime PasswordTimestamp, string Name, string Email, bool IsAdmin, byte CssStyle);

		void UpdateUserSession(decimal Id , string ConnectionAddress , string SessionId, bool IsLoggedIn);

		void DeleteUser(decimal UserId);

		void InsertPageHitAudit(decimal UserId, decimal CompanyId , string ConnectionAddress , string SessionId , System.DateTime Timestamp);

		decimal GetCompanyIdByShortName(string ShortName);

		System.Data.DataTable ReadCompanies();
	}
}
