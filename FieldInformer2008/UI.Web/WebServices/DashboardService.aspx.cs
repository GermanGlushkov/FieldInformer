using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Net;
using System.IO;
using System.Text;

using FI.BusinessObjects;
using FI.UI.Web.WebServices;



public partial class WebServices_DashboardService : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //FI.Common.LogWriter.Instance.WriteEventLogEntry("DashboardService request: " + Request.TotalBytes.ToString());
        //FI.Common.LogWriter.Instance.WriteEventLogEntry("DashboardService request: " + Request.UserHostAddress.ToString());
        byte[] commandBytes = Request.BinaryRead(Request.TotalBytes);
        string command = Encoding.UTF8.GetString(commandBytes);
        string response = DashboardServiceServer.ExecuteCommand(command);

        
        this.Response.Clear();
        if(response!=null && response!="")
            this.Response.BinaryWrite(Encoding.UTF8.GetBytes(response));
        this.Response.End();
    }


}
