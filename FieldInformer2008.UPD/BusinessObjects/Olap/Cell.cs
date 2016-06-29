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

			if(_fValue!=null && _fValue!=string.Empty)
			{
				// custom formatting
				/*
				string formatString=FI.Common.AppConfig.CustomNumberFormatString;
				if(_fValue!=null && _fValue.EndsWith("%") && !formatString.EndsWith("%"))
					formatString+="%";

				if(formatString!=null && formatString!=string.Empty && _value!=null && _value!=string.Empty)
				{
					System.Globalization.NumberFormatInfo formatInfo=FI.Common.AppConfig.CustomNumberFormatInfo;				

					double d=0;
					if(double.TryParse(_value, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out d))
						_fValue=d.ToString(formatString, formatInfo);
				}
				*/

				
				string replaceComma=FI.Common.AppConfig.NumberFormatReplaceComma;
				if(replaceComma!=string.Empty)
					_fValue=_fValue.Replace(",", replaceComma);

				string replaceDot=FI.Common.AppConfig.NumberFormatReplaceDot;
				if(replaceDot!=string.Empty)
					_fValue=_fValue.Replace(".", replaceDot);

				string replaceSpace=FI.Common.AppConfig.NumberFormatReplaceSpace;
				if(replaceSpace!=string.Empty)
					_fValue=_fValue.Replace(" ", replaceSpace);

				// strip last dot or comma				
				if(_fValue.EndsWith(".") || _fValue.EndsWith(","))
					_fValue=_fValue.Substring(1);
			}
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
