<%@ Register TagPrefix="uc1" TagName="TabView" Src="../Controls/Tabs/TabView.ascx" %>
<%@ Page language="c#" Codebehind="Table.aspx.cs" AutoEventWireup="false" Inherits="FI.UI.Web.StorecheckReport.Table" validateRequest="false" %>
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
	<body MS_POSITIONING="GridLayout">
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
									<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="2" cellPadding="0" width="100%"
										border="0">
										<TR>
											<TD class="tbl1_capt" vAlign="middle" align="left">
												<table height="0px" cellSpacing="1" cellPadding="2">
													<tr>
														<TD><asp:button id="btnClose" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Close" Runat="server"
																CommandName="Close"></asp:button>&nbsp;
														</TD>
														<TD>
															<table Height="22" Width="75" cellpadding="0" cellspacing="0" border="1">
																<tr>
																	<asp:HyperLink Target="_blank" NavigateUrl="../ReportPrint.aspx" Runat="server" id="HyperLink1">
																		<td Class="tbl1_ctrl">
																			Print
																		</td>
																	</asp:HyperLink>
																</tr>
															</table>
														</TD>
														<td>&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
														<td><asp:table id="ErrTable" runat="server" CssClass="tbl1_err"></asp:table></td>
													</tr>
												</table>
											</TD>
										</TR>
										<TR>
											<td height="100%">
												<table height="100%" cellSpacing="0" cellPadding="2" width="100%" border="0">
													<tr>
														<TD height="100%" id="cellContents" runat="server">
														</TD>
													</tr>
												</table>
											</td>
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
