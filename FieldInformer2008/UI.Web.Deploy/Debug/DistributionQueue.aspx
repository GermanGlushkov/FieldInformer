<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ Reference Page="~/PageBase.aspx" %>
<%@ Reference Control="~/controls/tabs/tabview.ascx" %>
<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ Reference Page="~/PageBase.aspx" %>
<%@ Register TagPrefix="uc1" TagName="TabView" Src="Controls/Tabs/TabView.ascx" %>
<%@ page language="c#" inherits="FI.UI.Web.DistributionQueue, UI.Web.Deploy" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Distribution Queue</title>
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
						<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
							border="0">
							<tr>
								<td>
									<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
										border="0">
										<TR>
											<TD class="tbl1_capt" id="cellControls" runat="server">&nbsp;
												<asp:button id="btnEnqueue" CommandName="Enqueue" Runat="server" Text="Enqueue" Width="90px"
													Height="25px" CssClass="tbl1_ctrl" onclick="btnEnqueue_Click"></asp:button>&nbsp;
												<asp:button id="btnRemove" CommandName="Remove" Runat="server" Text="Cancel" Width="90px" Height="25px"
													CssClass="tbl1_ctrl" onclick="btnRemove_Click"></asp:button>&nbsp;
												<asp:button id="btnSend" CommandName="Send" Runat="server" Text="Send Queued" Width="90px" Height="25px"
													CssClass="tbl1_ctrl" onclick="btnSend_Click"></asp:button>
											</TD>
										</TR>
										<TR>
											<TD height="100%">
												<TABLE class="bckgr1" id="Table3" height="100%" cellSpacing="3" cellPadding="3" width="100%"
													border="0">
													<TR>
														<TD><asp:panel id="ControlPanel" runat="server" height="100%"></asp:panel></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
