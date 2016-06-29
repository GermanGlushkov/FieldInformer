<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ page language="c#" inherits="FI.UI.Web.WebServices.DistributionDetails, UI.Web.Deploy" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Report Distribution Details</title>
		<LINK href="../style/style2/StyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="frmMain" method="post" runat="server">
			<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 24px; COLOR: red">
						<P>Report Distribution Details</P>
					</TD>
				</TR>
				<TR>
					<TD height="100%">
						<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
							border="0">
							<TR>
								<TD class="tbl1_capt">&nbsp;Report:</TD>
							</TR>
							<TR>
								<TD class="bckgr1"><asp:panel id="ReportPanel" runat="server"></asp:panel></TD>
							</TR>
							<TR>
								<TD class="tbl1_capt">&nbsp;Log:</TD>
							</TR>
							<TR>
								<TD><asp:panel id="DistributionLogPanel" runat="server"></asp:panel></TD>
							</TR>
							<TR>
								<TD class="tbl1_capt" vAlign="middle" align="left" height="0px">&nbsp;
									<asp:button id="BackButton" CommandName="Back" Runat="server" Text="Back" Width="75px" Height="22px"
										CssClass="tbl1_ctrl" onclick="BackButton_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD class="bckgr1" height="100%"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
