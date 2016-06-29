<%@ control language="c#" inherits="FI.UI.Web.OlapReport.SliceControl, FI.Web" enableviewstate="False" %>
<asp:Table id="tblMain" runat="server" CellPadding="1" CellSpacing="2" Height="100%" Width="100%">
	<asp:TableRow>
		<asp:TableCell CssClass="tbl1_hdr_l" Wrap=False>
		&nbsp;&nbsp; Filter:	&nbsp;&nbsp;					
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell>
			<asp:Table id="tblSlice" runat="server" CellPadding="0" CellSpacing="0" Height="100%" Width="100%"></asp:Table>
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell Height=100%>
		</asp:TableCell>
	</asp:TableRow>
</asp:Table>
