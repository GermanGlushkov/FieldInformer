using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FI.BusinessObjects;

namespace FI.UI.Web.MdxReport
{
	/// <summary>
	/// Summary description for PageBase.
	/// </summary>
	public partial class MdxPageBase : FI.UI.Web.PageBase
	{
		protected internal FI.BusinessObjects.CustomMdxReport _report;


		protected override void LoadSession()
		{
			base.LoadSession();

			//debug
			//LoadReport();
			//return;


			
			if(Session["Report"]==null)
				throw new Exception("Session failure : report");

			_report=(FI.BusinessObjects.CustomMdxReport)Session["Report"];
			
		}



		protected override void SaveSession()
		{
			Session["Report"]=_report;
			base.SaveSession();
		}

	}
}
