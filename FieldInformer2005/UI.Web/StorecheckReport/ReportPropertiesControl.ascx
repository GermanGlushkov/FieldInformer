<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReportPropertiesControl.ascx.cs" Inherits="FI.UI.Web.StorecheckReport.ReportPropertiesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<table height="0px" cellSpacing="0" cellPadding="0" border="1" width="100">
	<tr>
		<td noWrap colspan="1">
			<font color="navy" size="-2">&nbsp;<b>Name:</b>&nbsp;<input class="tbl1_edit_box" id="txtName" type="text" maxLength="50" name="txtName" runat="server">
				&nbsp;&nbsp; <b>Description:</b>&nbsp;<input class="tbl1_edit_box" id="txtDescr" type="text" maxLength="255" size="75" name="txtDescr"
					runat="server"></font>
		</td>
		<td noWrap>
			<font color="navy" size="-2"></font>
		</td>
	</tr>
	<tr>
		<td noWrap colspan="2">
			<font color="navy" size="-2">&nbsp;<b>Date To Count From(Today):</b> &nbsp;<asp:Label ID="lblToday" Runat="server" CssClass="tbl1_edit_box"></asp:Label>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Period:</b> &nbsp;<asp:Label ID="lblPeriod" Runat="server" CssClass="tbl1_edit_box"></asp:Label>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Number of Days:</b> &nbsp;<asp:TextBox MaxLength="3" id="txtDays" Runat="server" Width="35px" CssClass="tbl1_edit_box"></asp:TextBox>
				</font>
		</td>
	</tr>
	<tr>
		<td noWrap width="100%">
			<font color="navy" size="-2">
				&nbsp;<b>Selection Option:</b>&nbsp;<asp:DropDownList ID="ddlSelection" Runat="server" CssClass="tbl1_edit_box"></asp:DropDownList>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Product Join Logic:</b>&nbsp;<asp:DropDownList ID="ddlLogic" Runat="server" CssClass="tbl1_edit_box"></asp:DropDownList>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Data Source:</b>&nbsp;<asp:DropDownList ID="ddlDataSource" Runat="server" CssClass="tbl1_edit_box"></asp:DropDownList>
				</font>
		</td>
		<td noWrap>
			<font color="navy" size="-2"></font>
		</td>
	</tr>
</table>
