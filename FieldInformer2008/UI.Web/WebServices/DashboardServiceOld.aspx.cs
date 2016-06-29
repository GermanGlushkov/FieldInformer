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

using FI.BusinessObjects;
using FI.UI.Web.WebServices;



public partial class WebServices_DashboardServiceOld : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        XmlDocument aOuterXml = new XmlDocument();
        aOuterXml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><string xmlns=\"http://www.fieldforce.com/\"></string>");

        byte[] aResponseBytes = null;
        bool aZip = false;
        bool aCrypt = false;

        XmlDocument aReqXml = HttpCommandEnvelope.ParseRequest(this.Page.Request.Params.Get("xml"), ref aZip, ref aCrypt, null);
        if (aReqXml != null)
        {
            DataSet aResult = ExecuteRequest(aReqXml);
            if (aResult == null)
                aResult = new DataSet();
            aOuterXml.DocumentElement.InnerXml = HttpCommandEnvelope.SerializeResponse(aResult, aZip, aCrypt, null).OuterXml;
        }

        //DataTable dt = ReportSystem.GetReportHeaders(Convert.ToDecimal(1), typeof(OlapReport)) as DataTable;
        //DataSet aResult1 = new DataSet();
        //aResult1.Tables.Add(dt);
        //aOuterXml.DocumentElement.InnerXml = HttpCommandEnvelope.SerializeResponse(aResult1, aZip, aCrypt, null).OuterXml;

        System.IO.MemoryStream aStream = new System.IO.MemoryStream();
        XmlTextWriter aXmlTextWriter = new XmlTextWriter(aStream, System.Text.Encoding.UTF8);
        aOuterXml.WriteTo(aXmlTextWriter);
        aXmlTextWriter.Flush();
        aStream.Position = 0;
        aResponseBytes = new byte[aStream.Length];
        aStream.Read(aResponseBytes, 0, aResponseBytes.Length);
        aStream.Close();
        aXmlTextWriter.Close();

        this.Response.Clear();
        this.Response.BinaryWrite(aResponseBytes);
        this.Response.End();

    }


    public static DataSet ExecuteRequest(XmlDocument req)
    {
        try
        {
            string cmd = GetCommandName(req);
            if (string.Compare(cmd, "get_list_of_olap_reports", true) == 0)
            {
                byte[] userIdBytes = GetCommandParameter(req, "user_id");
                long userId = BitConverter.ToInt64(userIdBytes, 0);
                
                // exec
                DataTable dt=ReportSystem.GetReportHeaders(Convert.ToDecimal(userId), typeof(OlapReport)) as DataTable;
                DataSet ret = new DataSet();
                ret.Tables.Add(dt);
                return ret;
            }
            else if (string.Compare(cmd, "GetOlapReportResult", true) == 0)
            {
                byte[] reportIdBytes = GetCommandParameter(req, "report_id");
                long reportId = BitConverter.ToInt64(reportIdBytes, 0);

                // todo

                return null;
            }
        }
        catch (Exception exc)
        {
            return ExceptionResult(exc);
        }

        return new DataSet();
    }

    public static void GetOlapReportList(decimal userId)
    {
        ReportSystem.GetReportHeaders(userId, typeof(OlapReport));
    }

    public static void GetOlapReportResult(decimal reportId)
    {
    }

    public static string GetCommandName(XmlDocument doc)
    {
        string xpath = "/command[@dialect='eforce']";
        XmlElement el = doc.SelectSingleNode(xpath) as XmlElement;
        if (el == null)
            return "";
        XmlCDataSection cmdName = el.FirstChild as XmlCDataSection;
        return (cmdName == null ? "" : cmdName.Value);
    }

    public static byte[] GetCommandParameter(XmlDocument doc, string paramName)
    {
        if (paramName == null || paramName == "")
            return null;

        string xpath = string.Format("/command[@dialect='eforce']/p[@n='{0}']", paramName.Replace("'", "''"));
        XmlElement el = doc.SelectSingleNode(xpath) as XmlElement;
        if (el == null)
            return null;

        XmlCDataSection paramVal = el.FirstChild as XmlCDataSection;
        if (paramVal == null)
            return null;

        return Convert.FromBase64String(paramVal.Value);
    }

    public static DataSet ExceptionResult(Exception exc)
    {
        DataSet ret = new DataSet();
        ret.Tables.Add("Exceptions");
        ret.Tables[0].Columns.Add("Message", typeof(string));

        ret.Tables[0].Rows.Add(new object[] { exc.Message });

        return ret;
    }
}
