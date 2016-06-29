<%@ Control Language="c#" AutoEventWireup="false" Codebehind="FormatControl.ascx.cs" Inherits="FI.UI.Web.OlapReport.FormatControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt">&nbsp;Format Measures:</TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="bckgr1" id="formatTable" height="100%" cellSpacing="0" cellPadding="1" border="0"
							runat="server">
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" height="0px"><asp:button id="btnUpdate" CommandName="Load ReportUpdate" Runat="server" Width="75px" Height="25px"
							CssClass="tbl1_ctrl" Text="Update"></asp:button></TD>
				</TR>
				<TR>
					<TD class="bckgr1" vAlign="top" height="100%"><asp:table id="ErrTable" runat="server" CssClass="tbl1_err"></asp:table></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
