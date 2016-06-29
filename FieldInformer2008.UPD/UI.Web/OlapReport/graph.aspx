<%@ Reference Page="~/PageBase.aspx" %>
<%@ Reference Control="~/olapreport/reportpropertiescontrol.ascx" %>
<%@ Reference Control="~/olapreport/executecontrol.ascx" %>
<%@ Reference Control="~/controls/tabs/tabview.ascx" %>
<%@ Reference Control="~/olapreport/slicecontrol.ascx" %>
<%@ Reference Control="~/olapreport/graphcontrol.ascx" %>
<%@ Reference Page="~/olapreport/olappagebase.aspx" %>
<%@ Page language="c#" Inherits="FI.UI.Web.OlapReport.Graph" enableViewState="False" CodeFile="Graph.aspx.cs" CodeFileBaseClass="FI.UI.Web.OlapReport.OlapPageBase" %>
<%@ Register TagPrefix="uc1" TagName="ExecuteControl" Src="ExecuteControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="GraphControl" Src="GraphControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportPropertiesControl" Src="ReportPropertiesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TabView" Src="../Controls/Tabs/TabView.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SliceControl" Src="SliceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TableControl" Src="TableControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report</title>
		<LINK href="../style/style<% =_cssStyleNum%>/StyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!-- against image cache -->
		<meta http-equiv="Expires" content="Fri, Jun 12 1981 08:20:00 GMT">
		<meta http-equiv="Pragma" content="no-cache">
		<meta http-equiv="Cache-Control" content="no-cache">
	</HEAD>
	<body>
		<form id="WebForm1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD height="1"><uc1:tabview id="TvC" runat="server"></uc1:tabview></TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD>
									<TABLE class="bckgr1" id="Table3" height="100%" cellSpacing="2" cellPadding="0" width="100%"
										border="0">
										<TR>
											<TD class="tbl1_capt" vAlign="middle" align="left">
												<table height="0px" cellSpacing="1" cellPadding="2">
													<tr>
														<TD>
															<asp:button id="btnSave" CommandName="Save" Runat="server" Text="Save" Width="75px" Height="22px"
																CssClass="tbl1_ctrl" onclick="btnSave_Click"></asp:button>
															<asp:button id="btnClose" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Close" Runat="server"
																CommandName="Close" onclick="btnClose_Click"></asp:button></TD>
														<td>&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
														<td>
															<uc1:ExecuteControl id="ExC" runat="server"></uc1:ExecuteControl></td>
														<td><asp:table id="ErrTable" runat="server" CssClass="tbl1_err"></asp:table></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<TD class="bckgr1" vAlign="top" width="100%" height="100%">
												<table height="0px" cellSpacing="0" cellPadding="0" width="100%">
													<tr>
														<TD><uc1:slicecontrol id="SlC" runat="server"></uc1:slicecontrol></TD>
														<TD width="100%">
															<uc1:GraphControl id="GrC" runat="server"></uc1:GraphControl>
														</TD>
													</tr>
												</table>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
