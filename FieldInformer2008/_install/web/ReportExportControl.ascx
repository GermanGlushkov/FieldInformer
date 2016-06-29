<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.ReportExportControl, App_Web_reportexportcontrol.ascx.cdcab7d2" %>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt">&nbsp;Export Report:</TD>
				</TR>
				<TR>
					<TD class="bckgr1"><asp:panel id="ReportPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt">&nbsp;To:</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="5" width="100%" border="0">
							<TR>
								<TD class="bckgr1" nowrap><asp:radiobutton id="radioExcel" runat="server" Text="Excel CSV" GroupName="distr"></asp:radiobutton></TD>
								<TD class="bckgr1" nowrap width="100%" align="left">
									&nbsp;&nbsp;&nbsp;<asp:HyperLink id="hrefExcel" runat="server" Target="_blank"></asp:HyperLink>
								</TD>
							</TR>
							<TR>
								<TD class="bckgr1" nowrap><asp:radiobutton id="radioHtml" runat="server" Text="HTML" GroupName="distr"></asp:radiobutton></TD>
								<TD class="bckgr1" nowrap width="100%" align="left">
									&nbsp;&nbsp;&nbsp;<asp:HyperLink id="hrefHtml" runat="server" Target="_blank"></asp:HyperLink>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" height="0px">
						<asp:button id="ExportButton" CommandName="Export" Runat="server" Text="Export" Width="75px"
							Height="25px" CssClass="tbl1_ctrl" onclick="UpdateButton_Click"></asp:button>
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
