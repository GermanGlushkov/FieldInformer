using System;

namespace FI.BusinessObjects.Olap
{
	/// <summary>
	/// Summary description for Cell.
	/// </summary>
	public class Cell
	{
		private string _value=null;
		private string _fValue=null;

		internal Cell(string Value , string FormattedValue)
		{
			_value=Value;
			_fValue=FormattedValue;
		}

		public string Value
		{
			get{return _value;}
		}

		public string FormattedValue
		{
			get{return _fValue;}
		}


	}
}
