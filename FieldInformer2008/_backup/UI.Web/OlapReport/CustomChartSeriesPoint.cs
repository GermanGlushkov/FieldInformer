using System;
using DevExpress.XtraCharts;

namespace FI.UI.Web.OlapReport
{
	public class CustomChartSeriesPoint : SeriesPoint
	{
		private string _customLabel;
		public CustomChartSeriesPoint(string argument, double value, string customLabel):base(argument, new double[] {value})
		{
			_customLabel=customLabel;
		}

		protected override bool CustomLabelSupported
		{
			get
			{
				return true;
			}
		}

		protected override string GetCustomLabel()
		{
			return _customLabel;
		}


	}
}
