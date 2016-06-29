<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" %>
<%@ Register TagPrefix="FF" Namespace="ff.dashboardHelpersLib" Assembly="dashboardHelpersLib" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string aXmlDataSet = ConfigurationManager.ConnectionStrings["XmlDataSource"].ConnectionString;
        aXmlDataSet = HttpRuntime.AppDomainAppPath + (aXmlDataSet.StartsWith(@"\") ? aXmlDataSet.Substring(1) : aXmlDataSet);

        System.Xml.XmlDocument aOuterXml = new System.Xml.XmlDocument();
        aOuterXml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><string xmlns=\"http://www.fieldforce.com/\"></string>");

        byte[] aResponseBytes = null;
        bool aZip = false;
        bool aCrypt = false;

        System.Xml.XmlDocument aReqXml = ff.dashboardHelpersLib.HttpCommandEnvelope.ParseRequest(this.Page.Request.Params.Get("xml"), ref aZip, ref aCrypt, null);
        if (aReqXml != null )
        {
            System.Xml.XmlDocument aResult = ff.dashboardHelpersLib.XmlBroker.DoBrokerage(aReqXml);

            aOuterXml.DocumentElement.InnerXml = ff.dashboardHelpersLib.HttpCommandEnvelope.SerializeResponse(aResult, aZip, aCrypt, null).OuterXml;
        }

        System.IO.MemoryStream aStream = new System.IO.MemoryStream();
        System.Xml.XmlTextWriter aXmlTextWriter = new System.Xml.XmlTextWriter(aStream, System.Text.Encoding.UTF8);
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

    
</script>

<asp:Content ID="ContentDefault" ContentPlaceHolderID="ContentMasterPage" Runat="Server">
 
</asp:Content>
