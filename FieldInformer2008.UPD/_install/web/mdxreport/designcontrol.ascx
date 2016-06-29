<%@ Reference Control="~/mdxreport/reportpropertiescontrol.ascx" %>
<%@ Reference Control="~/mdxreport/executecontrol.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportPropertiesControl" Src="ReportPropertiesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExecuteControl" Src="ExecuteControl.ascx" %>
<%@ control language="c#" inherits="FI.UI.Web.MdxReport.DesignControl, App_Web_designcontrol.ascx.bf415a2a" %>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="2" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left">
						<table height="0px" cellSpacing="1" cellPadding="2">
							<tr>
								<TD>
									<asp:button id="btnSave" CommandName="Save" Runat="server" Text="Save" Width="75px" Height="22px"
										CssClass="tbl1_ctrl" onclick="btnSave_Click"></asp:button>
									<asp:button id="btnClose" CssClass="tbl1_ctrl" Height="22px" Width="75px" Text="Close" Runat="server"
										CommandName="Close" onclick="btnClose_Click"></asp:button></TD>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td>
									<uc1:ExecuteControl id="ExC" runat="server"></uc1:ExecuteControl></td>
								<td><asp:table id="ErrTable" runat="server" CssClass="tbl1_err"></asp:table></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" height="0">
						<uc1:ReportPropertiesControl id="RPrC" runat="server"></uc1:ReportPropertiesControl>
					</TD>
				<TR>
					<TD class="bckgr1" vAlign="top" width="100%" height="100%">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td TD class="tbl1_capt" vAlign="middle" align="left">&nbsp;Query:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnIMportOlapReport" CommandName="Import" Runat="server" Width="125px" Height="25px"
										CssClass="tbl1_ctrl" Text="Import Olap Report" onclick="btnIMportOlapReport_Click"></asp:button>
								</td>
							</tr>
							<tr>
								<td valign="top">
									<asp:TextBox Width="100%" Height="200px" Runat="server" ID="txtMdx" TextMode="MultiLine"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td TD class="tbl1_capt" vAlign="middle" align="left">
									&nbsp;Xsl stylesheet:&nbsp;<asp:button id="btnUpdateXsl" CommandName="Update Xsl" Runat="server" Width="75px" Height="25px"
										CssClass="tbl1_ctrl" Text="Update Xsl" onclick="btnUpdateXsl_Click"></asp:button>
								</td>
							</tr>
							<tr>
								<td valign="top">
									<asp:TextBox Width="100%" Height="400px" Runat="server" ID="txtXsl" TextMode="MultiLine"></asp:TextBox>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>