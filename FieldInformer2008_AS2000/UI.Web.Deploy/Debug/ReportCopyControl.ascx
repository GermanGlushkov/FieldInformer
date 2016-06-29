<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.ReportCopyControl, FI.Web" %>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt">&nbsp;Export Report:</TD>
				</TR>
				<TR>
					<TD class="bckgr1"><asp:panel id="ReportPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt">&nbsp;To:</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="5" width="100%" border="0">
							<TR>
								<TD nowrap class="bckgr1">
									Name:
									<asp:TextBox Runat="server" id="txtName" Width="150px" MaxLength="50" CssClass="tbl1_edit_box"></asp:TextBox>
								</TD>
								<TD class="bckgr1" nowrap width="100%" align="left">
									Description:
									<asp:TextBox Runat="server" ID="txtDescription" Width="350px" MaxLength="250" CssClass="tbl1_edit_box"></asp:TextBox>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" height="0px">
						<asp:button id="CopyButton" CommandName="Copy" Runat="server" Text="Copy" Width="75px" Height="25px"
							CssClass="tbl1_ctrl" onclick="CopyButton_Click"></asp:button>
						<asp:button id="BackButton" CommandName="Back" Runat="server" Text="Back" Width="75px" Height="25px"
							CssClass="tbl1_ctrl" onclick="BackButton_Click"></asp:button>
					</TD>
				</TR>
				<TR>
					<TD class="bckgr1" height="100%" valign="top">
						<asp:Table ID="ErrTable" runat="server" CssClass="tbl1_err"></asp:Table></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
