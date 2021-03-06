<%@ Page language="c#" Codebehind="ContactList.aspx.cs" AutoEventWireup="false" Inherits="FI.UI.Web.ContactList" %>
<%@ Register TagPrefix="uc1" TagName="TabView" Src="Controls/Tabs/TabView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Contacts</title>
		<LINK href="style/style<% =_cssStyleNum%>/StyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
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
												<asp:button id="btnAdd" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Add" Runat="server"
													CommandName="Add"></asp:button>
												<asp:button id="btnEdit" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Edit" Runat="server"
													CommandName="Edit"></asp:button>
												<asp:button id="btnDelete" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Delete" Runat="server"
													CommandName="Delete"></asp:button>
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
