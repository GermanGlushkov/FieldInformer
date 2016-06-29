<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReportGridControl.ascx.cs" Inherits="FI.UI.Web.ReportGridControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<TABLE class="bckgr1" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
	border="0">
	<TR id="cellHeader" runat="server">
		<TD class="bckgr1" vAlign="middle" noWrap align="left" height="0px"><asp:button id="btnPrevPage" CssClass="tbl1_pgr_arrow" Text="<" Runat="server"></asp:button><asp:button id="btnNextPage" CssClass="tbl1_pgr_arrow" Text=">" Runat="server"></asp:button><asp:label id="lblLiteral1" Runat="server">&nbsp;&nbsp;Page</asp:label><asp:textbox id="txtCurPage" CssClass="tbl1_edit_box" Runat="server" EnableViewState="false"
				MaxLength="3" Width="35px"  Columns=1 Rows=1 ></asp:textbox><asp:label id="lblLiteral2" Runat="server">of</asp:label>&nbsp;<asp:label id="lblPageCount" CssClass="tbl1_edit_box" Runat="server">0</asp:label><asp:label id="lblLiteral3" Runat="server">&nbsp;(Rows: </asp:label>
			<asp:label id="lblRowCount" CssClass="tbl1_edit_box" Runat="server">0</asp:label><asp:label id="lblLiteral4" Runat="server">)&nbsp;&nbsp;&nbsp;PageSize:</asp:label><asp:textbox id="txtPageSize" CssClass="tbl1_edit_box" Runat="server" MaxLength="3" Width="35px"  Columns=1 Rows=1></asp:textbox>&nbsp;
			<asp:button id="btnUpdatePager" CssClass="tbl1_pgr" Text="Update" Runat="server"></asp:button>&nbsp;&nbsp;<asp:label id="lblError" CssClass="tbl1_err" runat="server"></asp:label>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" align="left" height="100%">
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD vAlign="top" align="left"><asp:table id="GridTable" runat="server"></asp:table></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
