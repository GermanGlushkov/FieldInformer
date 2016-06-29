<%@ Reference Page="~/PageBase.aspx" %>
<%@ Reference Control="~/controls/tabs/tabview.ascx" %>
<%@ Reference Control="~/UserPassChangeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TabView" Src="Controls/Tabs/TabView.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportListControl" Src="ReportListControl.ascx" %>
<%@ page language="c#" inherits="FI.UI.Web.Default, App_Web_default.aspx.cdcab7d2" validaterequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title><% =FI.Common.AppConfig.CompanyName%></title>
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
						<TABLE class="t_act" id="Table3" height="100%" cellSpacing="10" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD valign="top" align="center">
									<table class="t_act" cellpadding="2">
							            <TR>
								            <TD valign="top" align="center" style="height:25px">
												            <asp:button id="btnLogout" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Logout" Runat="server"
													            CommandName="Logout" onclick="btnLogout_Click"></asp:button>
								            </TD>
							            </TR>
										<tr>
											<td>
												<asp:Label Runat="server" ID="lblCompany">Database:</asp:Label>
												<asp:TextBox Runat="server" ID="txtCompany" MaxLength="15" width="150"></asp:TextBox>												
											</td>
										</tr>
										<tr>
											<td>
												<asp:Label Runat="server" ID="lblLogin">User Login:</asp:Label>
												<asp:TextBox Runat="server" ID="txtLogin" MaxLength="50" width="150"></asp:TextBox>												
											</td>
										</tr>
										<tr>
											<td>
												<asp:Label Runat="server" ID="lblPassword">Password:</asp:Label>
												<asp:TextBox TextMode="Password" Runat="server" ID="txtPassword" MaxLength="50" width="150"></asp:TextBox>												
											</td>
										</tr>
										<tr>
											<td>
												<asp:CheckBox Runat="server" ID="chkRemember" Text=" Remember Me" EnableViewState="False"></asp:CheckBox>
												<br />
												<asp:CheckBox Runat="server" ID="chkForce" Text=" Force Login" EnableViewState="False"></asp:CheckBox>
											</td>
										</tr>
										<tr>
											<td align="right" nowrap>
                                                <asp:Button ID="btnLogin" runat="server" CommandName="Login" CssClass="tbl1_ctrl"
                                                    Height="25px" OnClick="btnLogin_Click" Text="Login" Width="75px" />
											</td>
										</tr>
									</table>
								</TD>
								<TD class="bckgr1" valign="top" width="100%">
									<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
										border="0">
										<TR>
											<TD class="tbl1_capt" align="center" height="25"><asp:Label Runat="server" ID="lblVersion" EnableViewState="False"></asp:Label>&nbsp;<asp:Label Runat="server" ID="lblHeader" ForeColor="red"></asp:Label></TD>
										</TR>
										<TR>
										    <TD height="100%" align="left" valign="top" runat="server" id="cellPassChange"  EnableViewState="True">                                                
											</TD>
										</TR>
										<TR>
											<TD height="100%" align="left" valign="top" runat="server" id="cellContents">
											    <object width="800" height="600" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0"> 
                                                    <param name="movie" value="Dashboard.swf"> 
                                                    <param name="quality" value="high"> 
                                                    <param name="allowScriptAccess" value="sameDomain" />
	                                                <param name="allowFullScreen" value="false" />
                                                    <embed src="Dashboard/Dashboard.swf" quality="high" width="800" height="600" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer"
                                                    flashvars="<%= _flashVarsString %>"
                                                    />
                                                </object>
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
