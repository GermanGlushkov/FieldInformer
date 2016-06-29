using System;

namespace FI.Common.DataAccess
{
	public interface IDashboardSystemDA
	{
        FI.Common.Data.FIDataTable GetUserGauges(decimal userId);
        FI.Common.Data.FIDataTable GetUserGaugeConfig(decimal userId, Guid gaugeId);
        void SaveUserGaugeConfig(
            Guid gaugeId,
            decimal userId,
            string name,
            string type,
            int x,
            int y,
            int width,
            int height,
            int visible,
            int refresh,
            string config);

        void DeleteUserGaugeConfig(
            Guid gaugeId,
            decimal userId);
	}
}
