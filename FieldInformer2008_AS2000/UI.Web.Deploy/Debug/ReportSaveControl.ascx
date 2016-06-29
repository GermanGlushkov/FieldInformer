<%@ Reference Control="~/controls/fidatatablegrid.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.ReportSaveControl, FI.Web" %>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt">&nbsp;Save Report:</TD>
				</TR>
				<TR>
					<TD class="bckgr1"><asp:panel id="ReportPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt">&nbsp;Saving Options:</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="5" width="100%" border="0">
							<TR>
								<TD class="bckgr1" noWrap><asp:radiobutton id="radioSave" runat="server" GroupName="distr" Text="Save Changes"></asp:radiobutton></TD>
							</TR>
							<TR>
								<TD class="bckgr1" noWrap><asp:radiobutton id="radioDiscard" runat="server" GroupName="distr" Text="Discard Changes"></asp:radiobutton></TD>
							</TR>
							<TR>
								<TD class="bckgr1" noWrap><asp:radiobutton id="radioSaveAs" runat="server" GroupName="distr" Text="Discard Changes and Save Copy As..."></asp:radiobutton></TD>
								<TD class="bckgr1" noWrap align="left" width="100%">Name:
									<asp:textbox id="txtName" CssClass="tbl1_edit_box" Width="150px" Runat="server" MaxLength="50"></asp:textbox>Description:
									<asp:textbox id="txtDescription" CssClass="tbl1_edit_box" Width="350px" Runat="server" MaxLength="250"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" height="0px"><asp:button id="OkButton" Text="Ok" CssClass="tbl1_ctrl" Height="25px" Width="75px" Runat="server"
							CommandName="Ok" onclick="OkButton_Click"></asp:button><asp:button id="BackButton" Text="Back" CssClass="tbl1_ctrl" Height="25px" Width="75px" Runat="server"
							CommandName="Back" onclick="BackButton_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="bckgr1" vAlign="top" height="100%"><asp:table id="ErrTable" runat="server" CssClass="tbl1_err"></asp:table></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
