using System;

namespace FI.BusinessObjects.Crystal
{
	/// <summary>
	/// Summary description for CutomerParameter.
	/// </summary>
	public class CutomerParameter:KeyListParameter
	{
		public static string TypeCode="CUSTOMER";

		public CutomerParameter(string Caption, int MaxKeyCount, string OltpDatabase):base(Caption, MaxKeyCount, OltpDatabase)
		{
		}


		public FI.Common.Data.FIDataTable GetSppCustomersPage(bool IncludeExistingCompanies, int StartIndex , int RecordCount, string FilterExpression , string SortExpression)
		{
			//FI.DataAccess.StorecheckReports dacObj=new FI.DataAccess.StorecheckReports();
			//return dacObj.GetSppProductsPage(this.OltpDatabase , this._keyList , IncludeExistingProducts , StartIndex , RecordCount , FilterExpression , SortExpression);
			return null;
		}

		protected override void SaveToXml(System.Xml.XmlDocument doc, System.Xml.XmlElement el)
		{
			base.SaveToXml (doc, el);
			el.SetAttribute("TYPE" , CutomerParameter.TypeCode);
		}

//		public CrystalDecisions.Shared.ParameterFields Parameters
//		{
//			get 
//			{ 
//				return null; 
//			}
//		}


	}
}
