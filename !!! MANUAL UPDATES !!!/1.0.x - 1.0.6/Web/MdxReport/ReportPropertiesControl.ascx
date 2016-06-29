<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReportPropertiesControl.ascx.cs" Inherits="FI.UI.Web.MdxReport.ReportPropertiesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<table height="0px" cellSpacing="0" cellPadding="0" border="1">
	<tr>
		<td noWrap colSpan="2"><font color="navy" size="-1">&nbsp;<b>Name:</b>&nbsp;<input class="tbl1_edit_box" id="txtName" type="text" maxLength="50" name="txtName" runat="server">
				&nbsp;&nbsp; <b>Description:</b>&nbsp;<input class="tbl1_edit_box" id="txtDescr" type="text" maxLength="255" size="75" name="txtDescr"
					runat="server"> &nbsp;&nbsp;<asp:button id="btnUpdate" runat="server" Text="Save" CssClass="tbl1_ctrl" Runat="server"
					Height="22px" Width="75px" CommandName="Update"></asp:button>
			</font>
		</td>
	</tr>
</table>
