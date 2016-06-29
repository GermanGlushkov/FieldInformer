<%@ Reference Control="~/controls/fidropdowncontrol.ascx" %>
<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.OlapReport.FormatControl, UI.Web.Deploy" %>
<TABLE class="t_act" id="Table2" cellSpacing="0" cellPadding="0" width="100%" height="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" cellSpacing="3" cellPadding="3" width="100%" border="0">
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" width="100%">
						&nbsp;Format Member:
						<asp:PlaceHolder Runat="server" ID="pnlSourceMember" />&nbsp;&nbsp; Name:
						<asp:TextBox Runat="server" ID="txtName" CssClass="tbl1_edit" Width="150px" MaxLength="75" EnableViewState="False" />&nbsp; 
						Format:
						<asp:DropDownList Runat="server" ID="listFormat" CssClass="tbl1_edit" Width="100px" EnableViewState="False" />&nbsp;
						<asp:button id="btnFormat" Runat="server" Width="75px" Height="22px" CssClass="tbl1_ctrl" Text="Format" onclick="btnFormat_Click"></asp:button>
					</TD>
				</TR>
				<TR>
					<TD class="bckgr1" vAlign="top" height="20"><asp:table id="ErrTable" runat="server" CssClass="tbl1_err"></asp:table></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt">&nbsp;Existing Formatted Members: &nbsp;
						<asp:button id="btnReset" Runat="server" Width="120px" CssClass="tbl1_ctrl" Text="Reset Selected"
							Height="22px" onclick="btnReset_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD>
						<asp:panel id="pnlFormattedMembers" runat="server"></asp:panel>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<tr>
		<td height="100%" class="bckgr1"></td>
	</tr>
</TABLE>
