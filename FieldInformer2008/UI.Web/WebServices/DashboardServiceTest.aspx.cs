using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Util;

using System.Net;
using System.IO;
using System.Text;
using System.Xml;

using FI.Common.DataAccess;
using FI.BusinessObjects;

public partial class WebServices_DashboardServiceTest : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!this.IsPostBack)
        {
            this.txtServiceUrl.Text = "http://213.180.3.218/eforce.net/WebServices/DashboardService.aspx";
            //this.txtServiceUrl.Text = "http://localhost:59295/UI.Web/WebServices/DashboardService.aspx";
            this.txtUserId.Text = "15394";
            this.txtDataSourceType.Text = "OLAP";
            this.txtDataSourceId.Text = "25107";
        }

        this.txtCommand.Text = "";
        this.txtResponse.Text = "";

        /*
        string request =
            @"<COMMAND TYPE='ExecuteQueries' QUERYDEF='0'>
                <QUERY DATASOURCE='SQLSCALAR'>
                    <SQL>select * from FieldForce_MSCRM_MOBILE.dbo._SyncSubscription</SQL>
                </QUERY>
                <QUERY DATASOURCE='SQLSCALAR'>
                    <SQL>select * from FieldForce_MSCRM_MOBILE.dbo._SernoReference</SQL>
                </QUERY>
            </COMMAND>";
        ExecuteRequest(request);         
        */

    }


    private void ExecuteRequest(string command)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(this.txtServiceUrl.Text);
        request.Method = WebRequestMethods.Http.Post;
        request.ContentType = "text/xml";
        byte[] bytes = Encoding.UTF8.GetBytes(command);
        request.ContentLength = bytes.Length;
        request.GetRequestStream().Write(bytes, 0, bytes.Length);

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);         
        string responseString = reader.ReadToEnd();
        response.Close();

        this.txtCommand.Text = command;
        this.txtResponse.Text = responseString;

        //


        //// show command
        //this.txtCommand.Text = command;
        //byte[] bytes = null;

        //// construct and exec request
        //WebRequest req = WebRequest.Create(this.txtServiceUrl.Text);
        //req.Method = "POST";

        //Stream reqStream=req.GetRequestStream();
        //bytes = Encoding.UTF8.GetBytes(command);
        //reqStream.Write(bytes, 0, bytes.Length);
        
        //// get and show response
        //WebResponse resp=req.GetResponse();
        //Stream respStream=resp.GetResponseStream();
        //bytes= new byte[respStream.Length];
        //respStream.Read(bytes, 0, bytes.Length);
        //this.txtResponse.Text = Encoding.UTF8.GetString(bytes);
    }

    protected void btnGetUserDataSources_Click(object sender, EventArgs e)
    {
        string cmd = DashboardServiceClient.GenerateGetUserDataSourcesRequest(txtUserId.Text);
        ExecuteRequest(cmd);
    }

    protected void btnGetUserGauges_Click(object sender, EventArgs e)
    {
        string cmd = DashboardServiceClient.GenerateGetUserGaugesRequest(txtUserId.Text);
        ExecuteRequest(cmd);
    }

    protected void btnGetUserGaugeConfig_Click(object sender, EventArgs e)
    {
        string cmd = DashboardServiceClient.GenerateGetUserGaugeConfigRequest(txtUserId.Text, false);
        ExecuteRequest(cmd);
    }

    protected void btnGetUserGaugeConfigExec_Click(object sender, EventArgs e)
    {
        string cmd = DashboardServiceClient.GenerateGetUserGaugeConfigRequest(txtUserId.Text, true);
        ExecuteRequest(cmd);
    }

    protected void btnSaveUserGaugeConfig_Click(object sender, EventArgs e)
    {
        string cmd = DashboardServiceClient.GenerateSaveUserGaugeConfigRequest(txtUserId.Text);
        ExecuteRequest(cmd);
    }

    protected void btnSaveUserGaugeContainerConfig_Click(object sender, EventArgs e)
    {
        string cmd = DashboardServiceClient.GenerateSaveUserGaugeContainerConfigRequest(txtUserId.Text);
        ExecuteRequest(cmd);
    }

    protected void btnGetDataTable_Click(object sender, EventArgs e)
    {
        string cmd = DashboardServiceClient.GenerateGetDataTableRequest(txtUserId.Text, txtDataSourceType.Text, txtDataSourceId.Text);
        ExecuteRequest(cmd);
    }

    protected void btnExecuteQueries_Click(object sender, EventArgs e)
    {
        string cmd = DashboardServiceClient.GenerateExecuteQueriesRequest(txtUserId.Text, txtDataSourceType.Text, txtDataSourceId.Text);
        ExecuteRequest(cmd);
    }
}
