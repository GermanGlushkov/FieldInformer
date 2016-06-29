<%@ Control Language="c#" Inherits="FI.UI.Web.OlapReport.GraphControl" enableViewState="False" CodeFile="GraphControl.ascx.cs" %>
<asp:table id="tblMain" Width="100%" Height="100%" CellSpacing="2" CellPadding="1" runat="server">
	<asp:TableRow runat="server" ID="Tablerow1">
		<asp:TableCell CssClass="tbl1_hdr_l" runat="server" ID="Tablecell1">
		&nbsp;&nbsp;&nbsp;&nbsp; Graph: &nbsp;								
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell>
			<table height="0px" cellSpacing="0" cellPadding="1" border="1">
				<tr>
					<td noWrap>
						<asp:button id="btnUpdate" runat="server" CommandName="Update" Text="Update"
							Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>
					</td>
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
							
							<asp:PlaceHolder runat="server" ID="areaPieColumns" Visible="true">
							&nbsp;&nbsp;Pie Columns:
							<asp:TextBox Runat="server" ID="txtPieColumns" CssClass="tbl1_edit" Width="25px" MaxLength="2" EnableViewState="False" />
							</asp:PlaceHolder>
							
							<asp:PlaceHolder runat="server" ID="areaMixedLinePos" Visible="true">
							&nbsp;&nbsp;Line Series:							
							<select class="tbl1_edit" id="selMixedLinePos" runat="server">
							</select>
							</asp:PlaceHolder>
							
							</font>
					</td>
				</tr>
			</table>
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell ID="cellGraph" Runat="server"></asp:TableCell>
	</asp:TableRow>
</asp:table>