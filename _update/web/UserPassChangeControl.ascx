<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UserPassChangeControl.ascx.cs" Inherits="FI.UI.Web.UserPassChangeControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
	<TR>
		<TD class="tbl1_capt">&nbsp;<font color=red>Password expired, please change:</font></TD>
	</TR>
	<TR>
		<TD>
			<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="5" width="100%" border="0">
				<TR>
					<TD nowrap class="bckgr1">
						New Password (1):
					</TD>
					<TD nowrap class="bckgr1" width="100%">
						<asp:TextBox TextMode="Password" Runat="server" id="txtPass1" Width="200px" MaxLength="50" CssClass="tbl1_edit_box"></asp:TextBox>
					</TD>
				</TR>
				<TR>
					<TD nowrap class="bckgr1">
						New Password (2):
					</TD>
					<TD nowrap class="bckgr1" width="100%">
						<asp:TextBox TextMode="Password" Runat="server" id="txtPass2" Width="200px" MaxLength="50" CssClass="tbl1_edit_box"></asp:TextBox>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="tbl1_capt" vAlign="middle" align="left" height="0px">
			<asp:button id="btnUpdate" CommandName="Update" Runat="server" Text="Update" Width="75px" Height="25px"
				CssClass="tbl1_ctrl"></asp:button>
		</TD>
	</TR>
	<TR>
		<TD class="bckgr1" height="100%" valign="top">
			<asp:Table ID="ErrTable" runat="server" CssClass="tbl1_err"></asp:Table></TD>
	</TR>
</TABLE>
