<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReportLoadControl.ascx.cs" Inherits="FI.UI.Web.StorecheckReport.ReportLoadControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt">&nbsp;Load Report:</TD>
				</TR>
				<TR>
					<TD class="bckgr1"><asp:panel id="ReportPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt">&nbsp;Parameters:</TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="bckgr1" id="paramsTable" height="100%" cellSpacing="0" cellPadding="5" width="100%"
							border="0" runat="server">
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" height="0px"><asp:button id="btnLoad" CommandName="Load Report" Runat="server" Width="100px" Height="25px"
							CssClass="tbl1_ctrl" Text="Load Report"></asp:button><asp:button id="BackButton" CommandName="Back" Runat="server" Width="75px" Height="25px" CssClass="tbl1_ctrl"
							Text="Back"></asp:button></TD>
				</TR>
				<TR>
					<TD class="bckgr1" vAlign="top" height="100%"><asp:table id="ErrTable" runat="server" CssClass="tbl1_err"></asp:table></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
