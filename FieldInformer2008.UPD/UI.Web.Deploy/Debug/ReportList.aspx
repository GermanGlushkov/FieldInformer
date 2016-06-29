<%@ Reference Control="~/ReportListControl.ascx" %>
<%@ Reference Control="~/storecheckreport/storecheckreportloadcontrol.ascx" %>
<%@ Reference Control="~/mdxreport/mdxreportloadcontrol.ascx" %>
<%@ Reference Control="~/sqlreport/sqlreportloadcontrol.ascx" %>
<%@ Reference Control="~/olapreport/olapreportloadcontrol.ascx" %>
<%@ Reference Control="~/ReportSaveControl.ascx" %>
<%@ Reference Control="~/ReportSharingControl.ascx" %>
<%@ Reference Control="~/ReportCopyControl.ascx" %>
<%@ Reference Control="~/ReportDeleteControl.ascx" %>
<%@ Reference Control="~/ReportExportControl.ascx" %>
<%@ Reference Control="~/ReportDistributionLogControl.ascx" %>
<%@ Reference Control="~/ReportDistributionControl.ascx" %>
<%@ Reference Page="~/PageBase.aspx" %>
<%@ Reference Control="~/controls/tabs/tabview.ascx" %>
<%@ Reference Control="~/ReportSaveControl.ascx" %>
<%@ Reference Control="~/ReportSharingControl.ascx" %>
<%@ Reference Control="~/ReportCopyControl.ascx" %>
<%@ Reference Control="~/ReportDeleteControl.ascx" %>
<%@ Reference Control="~/ReportExportControl.ascx" %>
<%@ Reference Control="~/ReportDistributionLogControl.ascx" %>
<%@ Reference Control="~/ReportDistributionControl.ascx" %>
<%@ Reference Control="~/ReportListControl.ascx" %>
<%@ Reference Page="~/PageBase.aspx" %>
<%@ page language="c#" inherits="FI.UI.Web.ReportList, UI.Web.Deploy" validaterequest="false" %>
<%@ Register TagPrefix="uc1" TagName="ReportListControl" Src="ReportListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TabView" Src="Controls/Tabs/TabView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report List</title>
		<LINK href="style/style<% =_cssStyleNum%>/StyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
	<form id="WebForm1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD height="1"><uc1:tabview id="TabView1" runat="server"></uc1:tabview></TD>
				</TR>
				<TR>
					<TD>
						<asp:Panel Runat="server" ID="ContentsPanel" Width="100%" Height="100%"></asp:Panel>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>