<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReportListControl.ascx.cs" Inherits="FI.UI.Web.ReportListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TBODY>
		<TR>
			<TD>
				<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
					border="0">
						<TR>
							<TD class="tbl1_capt">
								<asp:button id="btnNew" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="New" Runat="server"
									CommandName="New"></asp:button>
								<asp:button id="btnEdit" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Edit" Runat="server"
									CommandName="Edit"></asp:button>
								<asp:button id="btnCopy" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Copy" Runat="server"
									CommandName="Copy"></asp:button>
								<asp:button id="btnDelete" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Delete" Runat="server"
									CommandName="Delete"></asp:button>
								<asp:button id="btnExport" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Export" Runat="server"
									CommandName="Export"></asp:button>
								<asp:button id="btnShare" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Share" Runat="server"
									CommandName="Share"></asp:button>
								<asp:button id="btnDispatch" CssClass="tbl1_ctrl" Height="25px" Width="75px" Text="Dispatch"
									Runat="server" CommandName="Dispatch"></asp:button>&nbsp;
							</TD>
						</TR>
						<TR>
							<TD class="bckgr1" height="100%" valign="top">
							<asp:panel id="ReportPanel" runat="server">
							</asp:panel></TD>
						</TR>
				</TABLE>
			</TD>
		</TR>
	</TBODY>
</TABLE>
