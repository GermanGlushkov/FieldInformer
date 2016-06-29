<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ Page language="c#" Inherits="FI.UI.Web.WebServices.DistributionManager" EnableViewState=False CodeFile="DistributionManager.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Distribution Manager</title>
		<LINK href="../style/style2/StyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="WebForm1" method="post" runat="server">
			<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 24px; COLOR: red">
						<P>Distribution Manager</P>
					</TD>
				</TR>
				<TR height="100%">
					<td>
						<TABLE class="bckgr1" id="Table2" cellSpacing="3" cellPadding="3" height="100%" width="100%"
							border="0">
							<TR>
								<TD class="tbl1_capt" id="cellControls" runat="server">&nbsp;
									<asp:button id="btnRefresh" CommandName="Refresh" Runat="server" Text="Refresh" Width="120px"
										Height="25px" CssClass="tbl1_ctrl"></asp:button>&nbsp;
									<asp:button id="btnDetails" CssClass="tbl1_ctrl" Height="25px" Width="120px" Text="Show Log Details"
										Runat="server" CommandName="Details" onclick="btnDetails_Click"></asp:button>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnEnqueue" CssClass="tbl1_ctrl" Height="25px" Width="120px" Text="Enqueue"
										Runat="server" CommandName="Reenqueue" onclick="btnEnqueue_Click"></asp:button>&nbsp;
									<asp:button id="btnSend" CssClass="tbl1_ctrl" Height="25px" Width="120px" Text="Send Enqueued"
										Runat="server" CommandName="Send" onclick="btnSend_Click"></asp:button>&nbsp;
									<asp:button id="btnRemove" CommandName="Remove" Runat="server" Text="Cancel Enqueued" Width="120px"
										Height="25px" CssClass="tbl1_ctrl" onclick="btnRemove_Click"></asp:button>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnDeleteDistr" CssClass="tbl1_ctrl" Height="25px" Width="120px" Text="Delete Distribution"
										Runat="server" CommandName="DeleteDistr" ForeColor="Red" onclick="btnDeleteDistr_Click"></asp:button>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label runat="server" CssClass="tbl1_err" ID="lblError"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD>
									<asp:panel id="ControlPanel" runat="server"></asp:panel>
								</TD>
							</TR>
							<TR>
								<TD class="bckgr1" height="100%"></TD>
							</TR>
						</TABLE>
					</td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
