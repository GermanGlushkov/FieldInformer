<%@ Control Language="c#" Inherits="FI.UI.Web.MdxReport.ExecuteControl" CodeFile="ExecuteControl.ascx.cs" %>
<table cellpadding="0" cellspacing="0">
	<tr>
		<TD><asp:button id="btnUndo" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Undo" Runat="server"
				CommandName="Undo" onclick="btnUndo_Click"></asp:button></TD>
		<TD><asp:button id="btnRedo" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Redo" Runat="server"
				CommandName="Redo" onclick="btnRedo_Click"></asp:button></TD>
		<td class="tbl1_err" nowrap><asp:Label ID="lblStatus" Runat="server" Width="20px"></asp:Label></td>
		<TD><asp:button id="btnRefresh" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Refresh" Runat="server"
				CommandName="Refresh"></asp:button></TD>
	</tr>
</table>
