<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ Reference Control="~/storecheckreport/reportpropertiescontrol.ascx" %>
<%@ Reference Control="~/storecheckreport/executecontrol.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportPropertiesControl" Src="ReportPropertiesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExecuteControl" Src="ExecuteControl.ascx" %>
<%@ Control Language="c#" Inherits="FI.UI.Web.StorecheckReport.DesignControl" CodeFile="DesignControl.ascx.cs" %>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="2" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left">
						<table height="0px" cellSpacing="1" cellPadding="2">
							<tr>
								<TD>
									<asp:button id="btnSave" CommandName="Save" Runat="server" Text="Save" Width="75px" Height="22px"
										CssClass="tbl1_ctrl" onclick="btnSave_Click"></asp:button>
										<asp:button id="btnClose" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Close" Runat="server"
										CommandName="Close" onclick="btnClose_Click"></asp:button></TD>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td>
									<uc1:ExecuteControl id="ExC" runat="server"></uc1:ExecuteControl></td>
								<td><asp:table id="ErrTable" runat="server" CssClass="tbl1_err"></asp:table></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<td height="100%">
						<table height="100%" cellSpacing="0" cellPadding="2" width="100%" border="0">
							<tr>
								<TD vAlign="top" height="0">
									<uc1:ReportPropertiesControl id="RPrC" runat="server"></uc1:ReportPropertiesControl>
								</TD>
							</tr>
							<tr>
								<TD class="tbl1_capt" vAlign="middle" align="left" height="25">
									Products:
									<asp:button id="btnAdd" runat="server" Text="Add" CssClass="tbl1_ctrl" Runat="server" Height="22px"
										Width="75px" CommandName="Add" onclick="btnAdd_Click"></asp:button>
									&nbsp;
									<asp:button id="btnRemove" runat="server" Text="Remove" CssClass="tbl1_ctrl" Runat="server"
										Height="22px" Width="75px" CommandName="Remove" onclick="btnRemove_Click"></asp:button>
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
