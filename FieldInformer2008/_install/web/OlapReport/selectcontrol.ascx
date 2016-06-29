<%@ control language="c#" inherits="FI.UI.Web.OlapReport.SelectControl, App_Web_selectcontrol.ascx.cf0b5421" enableviewstate="False" %>
<asp:Table id="tblMain" runat="server" CellPadding="1" CellSpacing="2" Height="100%" Width="100%">
	<asp:TableRow>
		<asp:TableCell CssClass="tbl1_hdr_l">
		&nbsp;&nbsp;&nbsp;&nbsp; Select: &nbsp;
				<asp:button id="btnUpdate" runat="server" CommandName="Update" Runat="server" Text="Update" Width="75px" Height="22px"
				CssClass="tbl1_ctrl"></asp:button>
				<asp:button id="btnPivot" runat="server" CommandName="Pivot" Runat="server" Text="Pivot" Width="75px" Height="22px"
				CssClass="tbl1_ctrl"></asp:button>		
		&nbsp;&nbsp;&nbsp;&nbsp;
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell>
			<asp:Table id="tbl1" runat="server" CellPadding="0" CellSpacing="2" Height="100%">
				<asp:TableRow>
					<asp:TableCell VerticalAlign=top>
						<table id="tblRows" runat="server" cellpadding=0 cellspacing=0></table>
					</asp:TableCell>
					<asp:TableCell VerticalAlign=Top>
						<table id="tblColumns" runat="server" cellpadding=0 cellspacing=0></table>
					</asp:TableCell>
					<asp:TableCell VerticalAlign=top>
						<table id="tblFilter" runat="server" cellpadding=0 cellspacing=0></table>
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell Height="100%"></asp:TableCell>
	</asp:TableRow>
</asp:Table>
