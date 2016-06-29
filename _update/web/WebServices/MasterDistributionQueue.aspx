<%@ Page language="c#" Codebehind="MasterDistributionQueue.aspx.cs" AutoEventWireup="false" Inherits="FI.UI.Web.WebServices.MasterDistributionQueue" EnableViewState=False %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Master Distribution Queue</title>
		<LINK href="../style/style2/StyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="WebForm1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 24px; COLOR: red">Master Distribution Queue</TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="t_act" id="Table2" cellSpacing="10" cellPadding="0" width="100%"
							border="0">
							<tr>
								<td>
									<TABLE class="bckgr1" id="Table2" cellSpacing="3" cellPadding="3" width="100%"
										border="0">
										<TR>
											<TD class="tbl1_capt" id="cellControls" runat="server">&nbsp;
												<asp:button id="btnEnqueue" CssClass="tbl1_ctrl" Height="25px" Width="100px" Text="Enqueue"
													Runat="server" CommandName="Reenqueue"></asp:button>&nbsp;
												<asp:button id="btnRemove" CssClass="tbl1_ctrl" Height="25px" Width="100px" Text="Cancel" Runat="server"
													CommandName="Remove"></asp:button>&nbsp;
												<asp:button id="btnSend" CssClass="tbl1_ctrl" Height="25px" Width="120px" Text="Send Enqueued"
													Runat="server" CommandName="Send"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:button id="btnDeleteDistr" CssClass="tbl1_ctrl" Height="25px" Width="120px" Text="Delete Distribution"
													Runat="server" CommandName="DeleteDistr"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:label runat="server" CssClass="tbl1_err" ID="lblError"></asp:label>
											</TD>
										</TR>
										<TR>
											<TD height="100%">
												<TABLE class="bckgr1" id="Table3" cellSpacing="3" cellPadding="3" width="100%"
													border="0">
													<TR>
														<TD><asp:panel id="ControlPanel" runat="server"></asp:panel></TD>
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