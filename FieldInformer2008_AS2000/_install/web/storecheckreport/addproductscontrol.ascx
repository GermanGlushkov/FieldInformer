<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.StorecheckReport.AddProductsControl, App_Web_5-b9tahu" %>
<%@ Register TagPrefix="uc1" TagName="ExecuteControl" Src="ExecuteControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportPropertiesControl" Src="ReportPropertiesControl.ascx" %>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="2" cellPadding="0" width="100%"
				border="0">
				<TR>
					<td height="100%">
						<table height="100%" cellSpacing="0" cellPadding="2" width="100%" border="0">
							<tr>
								<TD class="tbl1_capt" vAlign="middle" align="left" height="25" nowrap>
									Add Products:
									<asp:button id="btnAdd" runat="server" Text="Add" CssClass="tbl1_ctrl" Runat="server" Height="22px"
										Width="75px" CommandName="Add" onclick="btnAdd_Click"></asp:button>
									&nbsp;
									<asp:button id="btnBack" runat="server" Text="Back" CssClass="tbl1_ctrl" Runat="server" Height="22px"
										Width="75px" CommandName="Back" onclick="btnBack_Click"></asp:button>
									&nbsp;<asp:Label ID="lblError" CssClass="tbl1_err" Runat="server"></asp:Label>
								</TD>
							</tr>
							<tr>
								<TD height="100%" id="cellContents" runat="server">
								</TD>
							</tr>
						</table>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
