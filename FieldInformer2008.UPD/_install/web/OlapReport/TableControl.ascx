<%@ control language="c#" inherits="FI.UI.Web.OlapReport.TableControl, App_Web_tablecontrol.ascx.cf0b5421" enableviewstate="False" %>
<asp:Table id="tblMain" runat="server" CellPadding="1" CellSpacing="2" Height="100%" Width="100%">
	<asp:TableRow runat="server">
		<asp:TableCell CssClass="tbl1_hdr_l" runat="server">
		&nbsp;&nbsp;&nbsp;&nbsp; Table: &nbsp;
		<asp:button id="btnSort" runat="server" CommandName="Sort" Runat="server" Text="Sort" Width="75px"
				Height="22px" CssClass="tbl1_ctrl"></asp:button>	
		<asp:button id="btnPivot" runat="server" CommandName="Pivot" Runat="server" Text="Pivot" Width="75px"
				Height="22px" CssClass="tbl1_ctrl"></asp:button>
		<asp:button id="btnDrillDown" runat="server" CommandName="DrillDown" Runat="server" Text="Drill Down"
				Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>
		<asp:button id="btnDrillUp" runat="server" CommandName="DrillUp" Runat="server" Text="Drill Up"
				Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>
		<asp:button id="btnRemove" runat="server" CommandName="Remove" Runat="server" Text="Remove"
				Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>	
		&nbsp;&nbsp;&nbsp;&nbsp;																
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow runat="server">
		<asp:TableCell runat="server">
			<asp:Panel Runat="server" id="pnlPivot" EnableViewState="false" Width="1px"></asp:Panel>
			<!--<asp:Table id="tblPivot" runat="server" CellPadding="0" CellSpacing="0" Height="100%" Width="100%"  EnableViewState="False"></asp:Table>-->
		</asp:TableCell>
	</asp:TableRow>
</asp:Table>
