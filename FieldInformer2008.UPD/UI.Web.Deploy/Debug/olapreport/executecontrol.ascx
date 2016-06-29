<%@ Reference Page="~/PageBase.aspx" %>
<%@ control language="c#" inherits="FI.UI.Web.OlapReport.ExecuteControl, UI.Web.Deploy" %>
<table cellpadding="0" cellspacing="0">
	<tr>
		<TD><asp:button id="btnUndo" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Undo" Runat="server"
				CommandName="Undo" onclick="btnUndo_Click"></asp:button></TD>
		<TD><asp:button id="btnRedo" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Redo" Runat="server"
				CommandName="Redo" onclick="btnRedo_Click"></asp:button></TD>
		<td class="tbl1_err" nowrap="true">&nbsp;&nbsp;<asp:Label ID="lblStatus" Runat="server" Width="20px"></asp:Label>&nbsp;</td>
		<TD><asp:button id="btnRefresh" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Refresh" Runat="server"
				CommandName="Refresh"></asp:button></TD>
		<TD><asp:button id="btnExecute" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Run Query" Runat="server"
				CommandName="Execute" onclick="btnExecute_Click"></asp:button></TD>
		<TD><asp:button id="btnCancel" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Cancel" Runat="server"
				CommandName="Cancel" onclick="btnCancel_Click"></asp:button></TD>
	</tr>
</table>
