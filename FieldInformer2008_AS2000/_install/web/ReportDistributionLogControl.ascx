<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.ReportDistributionLogControl, App_Web_c9lqn3es" %>
</style>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt">&nbsp;Report:</TD>
				</TR>
				<TR>
					<TD class="bckgr1"><asp:panel id="ReportPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt">&nbsp;Distribution Log:</TD>
				</TR>
				<TR>
					<TD><asp:panel id="DistributionLogPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" height="0px">
						<asp:button id="BackButton" CommandName="Back" Runat="server" Text="Back" Width="75px" Height="22px"
							CssClass="tbl1_ctrl" onclick="BackButton_Click"></asp:button>
					</TD>
				</TR>
				<TR>
					<TD class="bckgr1" height="100%"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>