<%@ Control Language="c#" AutoEventWireup="false" Codebehind="FIDataEdit.ascx.cs" Inherits="FI.UI.Web.Controls.FIDataEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="True" %>
<TABLE class="bckgr1" id="Table1" height="100%" cellSpacing="0" cellPadding="5" width="100%" border="0">
	<TR>
		<TD class="tbl1_capt" vAlign="center" align="left" height="0px">
			<asp:label id="CaptionLabel" runat="server"></asp:label>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top" align="left">
			<TABLE class="bckgr1" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD valign="center" align="left">
						<asp:table id="EditTable" runat="server"></asp:table>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="tbl1_capt" vAlign="center" align="left" height="0px">
			<asp:button id="InsertButton" CommandName="Insert" Runat="server" Text="Insert" Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>
			<asp:button id="UpdateButton" CommandName="Update" Runat="server" Text="Update" Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>
			<asp:button id="DeleteButton" CommandName="Delete" Runat="server" Text="Delete" Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>
			<asp:button id="CancelButton" CommandName="Cancel" Runat="server" Text="Cancel" Width="75px" Height="22px" CssClass="tbl1_ctrl"></asp:button>
		</TD>
	</TR>
	<TR>
		<TD class="tbl1_err" vAlign="center" align="left" height="0px">
			<asp:label id="ErrorLabel" runat="server"></asp:label>
		</TD>
	</TR>
	<TR>
		<TD height="100%" class="bckgr1">
		</TD>
	</TR>
</TABLE>
