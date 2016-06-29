<%@ Reference Control="~/storecheckreport/designcontrol.ascx" %>
<%@ Reference Control="~/storecheckreport/addproductscontrol.ascx" %>
<%@ Reference Control="~/controls/tabs/tabview.ascx" %>
<%@ Reference Page="~/storecheckreport/storecheckpagebase.aspx" %>
<%@ Register TagPrefix="uc1" TagName="TabView" Src="../Controls/Tabs/TabView.ascx" %>
<%@ page language="c#" inherits="FI.UI.Web.StorecheckReport.Design, FI.Web" validaterequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report</title>
		<LINK href="../style/style<% =_cssStyleNum%>/StyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="WebForm1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD height="1"><uc1:tabview id="TvC" runat="server"></uc1:tabview></TD>
				</TR>
				<TR>
					<TD id="contentsCell" runat="server">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
