<%@ Control Language="c#" AutoEventWireup="false" Codebehind="FilteredByNameSetControl.ascx.cs" Inherits="FI.UI.Web.OlapReport.CalculatedMemberControls.FilteredByNameSetControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table class="bckgr1">
	<tr>
		<td class="tbl1_capt"><asp:label id="lblName" Runat="server"></asp:label></td>
		<td>&nbsp;Level:
			<asp:dropdownlist id="ddlLevel" CssClass="tbl1_edit" Runat="server"></asp:dropdownlist></td>
		<td>&nbsp;From:
			<asp:textbox id="txtGrOrEq" CssClass="tbl1_edit" Runat="server"></asp:textbox></td>
		<td>&nbsp;To:
			<asp:textbox id="txtLessOrEq" CssClass="tbl1_edit" Runat="server"></asp:textbox></td>
		<td>
			&nbsp;
			<asp:Button ID="btnUpdate" Text="Update" Runat="server" CssClass="tbl1_ctrl" Width="75px"></asp:Button></td>
		<td id="cellErr" runat="server" class="tbl1_err">
		</td>
	</tr>
</table>
