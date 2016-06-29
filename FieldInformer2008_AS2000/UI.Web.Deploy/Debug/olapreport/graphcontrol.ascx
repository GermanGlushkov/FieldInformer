<%@ control language="c#" inherits="FI.UI.Web.OlapReport.GraphControl, FI.Web" enableviewstate="False" %>
<%@ Register Assembly="DevExpress.XtraCharts.v8.2.Web, Version=8.2.6.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v8.2, Version=8.2.6.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<asp:table id="tblMain" Width="100%" Height="100%" CellSpacing="2" CellPadding="1" runat="server">
	<asp:TableRow runat="server" ID="Tablerow1" NAME="Tablerow1">
		<asp:TableCell CssClass="tbl1_hdr_l" runat="server" ID="Tablecell1" NAME="Tablecell1">
		&nbsp;&nbsp;&nbsp;&nbsp; Graph: &nbsp;								
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell>
			<table height="0px" cellSpacing="0" cellPadding="1" border="1">
				<tr>
					<td noWrap><font color="navy" size="-2">&nbsp;<b>Type</b>
							<select class="tbl1_edit" id="selType" name="selCalcType" runat="server">
							</select>
						    </font>
					</td>
					<td noWrap><font color="navy" size="-2">&nbsp;<b>Theme</b>
							<select class="tbl1_edit" id="selColorTheme" name="selColorTheme" runat="server">
							</select>
							</font>
					</td>
					<td noWrap><font color="navy" size="-2"> &nbsp;<b>Data</b> 					        
					        &nbsp;&nbsp;Values:<select class="tbl1_edit" id="selNumOptions" name="selNumOptions" runat="server" />
							&nbsp;&nbsp;Pivot:<input type="checkbox" id="chkRotateAxes" runat="server" class="tbl1_edit" NAME="chkRotateAxes">
						    </font>
					</td>
					<td noWrap><font color="navy" size="-2"> &nbsp;<b>Point Labels</b> 
					        &nbsp;&nbsp;Values:<input type="checkbox" id="chkValues" runat="server" class="tbl1_edit" NAME="chkLabVisible">		
							&nbsp;&nbsp;Series:<input type="checkbox" id="chkSeries" runat="server" class="tbl1_edit" NAME="chkSeries">
							&nbsp;&nbsp;Categories:<input type="checkbox" id="chkCat" runat="server" class="tbl1_edit" NAME="chkCat">
						&nbsp;</font>
					</td>
					<td noWrap><font color="navy" size="-2"> &nbsp;<b>Image</b> 
					        &nbsp;&nbsp;Width:
							<asp:TextBox Runat="server" ID="txtWidth" CssClass="tbl1_edit" Width="45px" MaxLength="4" EnableViewState="False" />
							&nbsp;&nbsp;Height:
							<asp:TextBox Runat="server" ID="txtHeight" CssClass="tbl1_edit" Width="45px" MaxLength="4" EnableViewState="False" />
							&nbsp;&nbsp;Pie Columns:
							<asp:TextBox Runat="server" ID="txtPieColumns" CssClass="tbl1_edit" Width="25px" MaxLength="2" EnableViewState="False" />
							</font>
					</td>
					<td noWrap>
						<asp:button id="btnUpdate" runat="server" CommandName="Update" Text="Update"
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