<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ page language="c#" inherits="FI.UI.Web.WebServices.OlapProcessorManager, FI.Web" enableviewstate="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Olap Processor Manager</title>
		<LINK href="../style/style2/StyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="WebForm1" method="post" runat="server">
			<TABLE id="Table1" class="t_act" cellSpacing="10" cellPadding="0" width="100%" height="100%"
				border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 24px; COLOR: red">
						<P>Olap Processor Info</P>
					</TD>
				</TR>
				<TR>
					<td height="100%">
						<TABLE class="bckgr1" id="Table2" cellSpacing="3" cellPadding="3" width="100%"  height="100%" border="0">
							<TR>
								<TD class="tbl1_capt" id="cellControls" runat="server">
									<asp:button id="btnRefresh" CommandName="Refresh" Runat="server" Text="Refresh" Width="100px"
										Height="25px" CssClass="tbl1_ctrl" onclick="btnRefresh_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label runat="server" CssClass="tbl1_err" ID="lblError"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD>
									<TABLE class="bckgr1" id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD><asp:panel id="ControlPanel" runat="server"></asp:panel></TD>
										</TR>
									</TABLE>
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
