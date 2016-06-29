<%@ Control Language="c#" AutoEventWireup="false" Codebehind="GraphControl.ascx.cs" Inherits="FI.UI.Web.OlapReport.GraphControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<asp:table id="tblMain" Width="100%" Height="100%" CellSpacing="2" CellPadding="1" runat="server">
	<asp:TableRow runat="server" ID="Tablerow1" NAME="Tablerow1">
		<asp:TableCell CssClass="tbl1_hdr_l" runat="server" ID="Tablecell1" NAME="Tablecell1">
		&nbsp;&nbsp;&nbsp;&nbsp; Graph: &nbsp;								
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell>
			<table height="0px" cellSpacing="0" cellPadding="0" border="1">
				<tr>
					<td noWrap><font color="navy" size="-2">&nbsp;<b>Type</b>
							<select class="tbl1_edit" id="selType" name="selCalcType" runat="server">
							</select>&nbsp; </font>
					</td>
					<td noWrap><font color="navy" size="-2">&nbsp;<b>Theme</b>
							<select class="tbl1_edit" id="selColorTheme" name="selColorTheme" runat="server">
							</select>&nbsp; </font>
					</td>
					<td noWrap><font color="navy" size="-2"> &nbsp;<b>Data</b> &nbsp;&nbsp;Scaling By Series:<input type="checkbox" id="chkScal" runat="server" class="tbl1_edit" NAME="chkScal">
							&nbsp;&nbsp;Rotate Axes:<input type="checkbox" id="chkRotateAxes" runat="server" class="tbl1_edit" NAME="chkRotateAxes">
						</font>
					</td>
					<td noWrap><font color="navy" size="-2"> &nbsp;<b>Point Labels</b> &nbsp;&nbsp;Values:<input type="checkbox" id="chkValues" runat="server" class="tbl1_edit" NAME="chkLabVisible">
							&nbsp;&nbsp;Series:<input type="checkbox" id="chkSeries" runat="server" class="tbl1_edit" NAME="chkSeries">
							&nbsp;&nbsp;Categories:<input type="checkbox" id="chkCat" runat="server" class="tbl1_edit" NAME="chkCat">
						</font>
					</td>
					<td noWrap><font color="navy" size="-2"> &nbsp;<b>Size</b> &nbsp;&nbsp;Width:
							<asp:TextBox Runat="server" ID="hSize" CssClass="tbl1_edit" Width="45px" MaxLength="4" EnableViewState="False" />
							&nbsp;&nbsp;Height:
							<asp:TextBox Runat="server" ID="vSize" CssClass="tbl1_edit" Width="45px" MaxLength="4" EnableViewState="False" />
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
</asp:table>