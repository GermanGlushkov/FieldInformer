<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ Reference Control="~/ReportGridControl.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.ReportListControl, App_Web_reportlistcontrol.ascx.cdcab7d2" %>
<TABLE class="t_act" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TBODY>
		<TR>
			<TD>
				<TABLE class="bckgr1" height="100%" cellSpacing="3" cellPadding="3" width="100%"
					border="0">
						<TR>
							<TD class="tbl1_capt">
								<asp:button id="btnNew" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="New" Runat="server"
									CommandName="New" onclick="btnNew_Click"></asp:button>
								<asp:button id="btnEdit" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Edit" Runat="server"
									CommandName="Edit" onclick="btnEdit_Click"></asp:button>
								<asp:button id="btnCopy" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Copy" Runat="server"
									CommandName="Copy" onclick="btnCopy_Click"></asp:button>
								<asp:button id="btnDelete" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Delete" Runat="server"
									CommandName="Delete" onclick="btnDelete_Click"></asp:button>
								<asp:button id="btnExport" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Export" Runat="server"
									CommandName="Export" onclick="btnExport_Click"></asp:button>
								<asp:button id="btnShare" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Share" Runat="server"
									CommandName="Share" onclick="btnShare_Click"></asp:button>
								<asp:button id="btnDispatch" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Distribute"
									Runat="server" CommandName="Dispatch" onclick="btnDispatch_Click"></asp:button>&nbsp;
							</TD>
						</TR>
						<TR>
							<TD class="bckgr1" valign="top" height="100%">
							<asp:panel id="ReportPanel" runat="server"  >
							</asp:panel></TD>
						</TR>
				</TABLE>
			</TD>
		</TR>
	</TBODY>
</TABLE>
