<%@ Control Language="c#" AutoEventWireup="false" Codebehind="GraphControl.ascx.cs" Inherits="FI.UI.Web.OlapReport.GraphControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>

<asp:Table id="tblMain" runat="server" CellPadding="1" CellSpacing="2" Height="100%" Width="100%">
	<asp:TableRow runat="server">
		<asp:TableCell CssClass="tbl1_hdr_l" runat="server">
		&nbsp;&nbsp;&nbsp;&nbsp; Graph: &nbsp;
		<asp:button id="btnPivot" runat="server" CommandName="Pivot" Runat="server" Text="Pivot" Width="75px"
				Height="22px" CssClass="tbl1_ctrl"></asp:button>
		&nbsp;&nbsp;&nbsp;&nbsp;																
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell>
			<table height="0px" cellSpacing="0" cellPadding="0" border="1">
				<tr>
					<td noWrap><font color="navy" size="-2">&nbsp;<b>Type:</b>
							<select class="tbl1_edit" id="selType" name="selCalcType" runat="server">
							</select>
						</font>
					</td>
					<td noWrap><font color="navy" size="-2"> &nbsp;<b>Labels:</b> &nbsp;&nbsp;Categories<input type="checkbox" id="chkCat" runat="server" class="tbl1_edit">
							&nbsp;&nbsp;Series<input type="checkbox" id="chkSer" runat="server" class="tbl1_edit" NAME="Checkbox1">
							&nbsp;&nbsp;Values<input type="checkbox" id="chkVal" runat="server" class="tbl1_edit" NAME="Checkbox2">
						</font>
					</td>
					<td noWrap><font color="navy" size="-2"> &nbsp;<b>Scaling By Sets:</b> <input type="checkbox" id="chkScal" runat="server" class="tbl1_edit" NAME="Checkbox2">
						</font>
					</td>
					<td noWrap>
						<asp:button id="btnUpdate" runat="server" CommandName="Update" Runat="server" Text="Update"
							Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>
					</td>
				</tr>
			</table>
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell ID="cellGraph" Runat="server"></asp:TableCell>
	</asp:TableRow>
</asp:Table>
