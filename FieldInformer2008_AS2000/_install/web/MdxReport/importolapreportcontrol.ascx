<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.MdxReport.ImportOlapReportControl, App_Web_m6bevd8c" %>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt">&nbsp;Import Olap Report:</TD>
				</TR>
				<TR>
					<TD class="bckgr1"><asp:panel id="ReportPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" height="0px">
						<asp:button id="btnImport" CommandName="Import" Runat="server" Text="Import" Width="75px" Height="25px"
							CssClass="tbl1_ctrl" onclick="btnImport_Click"></asp:button>
						<asp:button id="BackButton" CommandName="Back" Runat="server" Text="Back" Width="75px" Height="25px"
							CssClass="tbl1_ctrl" onclick="BackButton_Click"></asp:button>
					</TD>
				</TR>
				<TR>
					<TD class="bckgr1" height="100%" valign="top">
						<asp:Table ID="ErrTable" runat="server" CssClass="tbl1_err"></asp:Table></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>