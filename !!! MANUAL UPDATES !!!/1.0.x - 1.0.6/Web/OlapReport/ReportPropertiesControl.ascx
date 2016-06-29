<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReportPropertiesControl.ascx.cs" Inherits="FI.UI.Web.OlapReport.ReportPropertiesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<table height="0px" cellSpacing="0" cellPadding="0" border="1">
	<tr>
		<td noWrap colSpan="2"><font color="navy" size="-2">&nbsp;<b>Name:</b>&nbsp;<input class="tbl1_edit_box" id="txtName" type="text" maxLength="50" name="txtName" runat="server">
				&nbsp;&nbsp; <b>Description:</b>&nbsp;<input class="tbl1_edit_box" id="txtDescr" type="text" maxLength="255" size="75" name="txtDescr"
					runat="server">
				<br>
				<b>&nbsp;Hide Empty: </b>Rows<input id="chkHideEmptyRows" type="checkbox" name="chkHideEmptyRows" runat="server">
				Columns<input id="chkHideEmptyCols" type="checkbox" name="chkHideEmptyCols" runat="server">
				&nbsp;&nbsp; <b>Hide NON Empty: </b>Rows<input id="chkHideNonEmptyRows" type="checkbox" name="chkHideNonEmptyRows" runat="server">
				Columns<input id="chkHideNonEmptyCols" type="checkbox" name="chkHideNonEmptyCols" runat="server">
				&nbsp;&nbsp;<asp:button id="btnUpdate" runat="server" CommandName="Save" Width="75px" Height="22px" Runat="server"
					CssClass="tbl1_ctrl" Text="Save"></asp:button>
			</font>
		</td>
	</tr>
	<tr>
		<td noWrap colSpan="4"><font color="navy" size="-2"><b>&nbsp;Add Time Range:</b> &nbsp;&nbsp;Prompt<input id="chkTimeRangePrompt" type="checkbox" name="chkTimeRangePrompt" runat="server">
				&nbsp;&nbsp;Level
				<select class="tbl1_edit" id="selTimeRangeLevel" name="selTimeRangeLevel" runat="server">
				</select>
				&nbsp;&nbsp;Range Start <input class="tbl1_edit" id="txtTimeRangeStart" type="text" maxLength="8" size="8" name="txtTimeRangeStart"
					runat="server"> &nbsp;&nbsp;Range End <input class="tbl1_edit" id="txtTimeRangeEnd" type="text" maxLength="8" size="8" name="txtTimeRangeEnd"
					runat="server"> &nbsp;&nbsp;<asp:button id="btnAddTimeRange" runat="server" CommandName="Add" Width="50px" Height="22px"
					Runat="server" CssClass="tbl1_ctrl" Text="Add"></asp:button>
			</font>
		</td>
	</tr>
	<tr>
		<td noWrap colSpan="4"><font color="navy" size="-2"><b>&nbsp;Add Calc. Measure:</b> &nbsp;&nbsp;Type
				<select class="tbl1_edit" id="selCalcType" name="selCalcType" runat="server">
				</select>
				&nbsp;&nbsp;Dimension
				<select class="tbl1_edit" id="selCalcDim" style="WIDTH: 150px" name="selCalcDim" runat="server">
				</select>
				&nbsp;&nbsp;Measure
				<select class="tbl1_edit" id="selCalcMeasure" style="WIDTH: 250px" name="selCalcMeasure"
					runat="server">
				</select>
				&nbsp;&nbsp;<asp:button id="btnAddCalc" runat="server" CommandName="Add" Width="50px" Height="22px" Runat="server"
					CssClass="tbl1_ctrl" Text="Add"></asp:button>
			</font>
		</td>
	</tr>
</table>
