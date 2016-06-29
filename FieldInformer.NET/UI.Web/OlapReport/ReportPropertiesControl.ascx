<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReportPropertiesControl.ascx.cs" Inherits="FI.UI.Web.OlapReport.ReportPropertiesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<table class="tbl1_T" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td class="tbl1_C1" noWrap colSpan="2"><font size="-2">&nbsp;<b>Name:</b>&nbsp;<input class="tbl1_edit_box" id="txtName" type="text" maxLength="50" name="txtName" runat="server">
				&nbsp;&nbsp; <b>Description:</b>&nbsp;<input class="tbl1_edit_box" id="txtDescr" type="text" maxLength="255" size="75" name="txtDescr"
					runat="server"> </font>
		</td>
		<td class="tbl1_C1"><asp:button id="btnProperties" runat="server" CssClass="sel_open" Runat="server" Height="13px"
				Width="13px" CommandName="Properties"></asp:button></td>
	</tr>
	<tr id="emptyTuplesCell" runat="server">
		<td class="tbl1_H" noWrap><font size="-2">&nbsp;Empty Cells:&nbsp;</font>
		</td>
		<td class="tbl1_C1" noWrap><font size="-2">&nbsp;&nbsp;Rows:
				<select class="tbl1_edit" id="selRowEmpty" name="selRowEmpty" runat="server">
				</select>
				&nbsp;&nbsp;Columns:
				<select class="tbl1_edit" id="selColEmpty" name="selColEmpty" runat="server">
				</select>
			</font>
		</td>
		<td class="tbl1_C1"><asp:button id="btnRowsUpdate" runat="server" CssClass="tbl1_ctrl" Runat="server" Height="22px"
				Width="75px" CommandName="UpdateEmptyCells" Text="Update"></asp:button></td>
	</tr>
	<tr id="timeRangeCell" runat="server">
		<td class="tbl1_H" noWrap><font size="-2">&nbsp;Add Time Range: &nbsp;</font>
		</td>
		<td class="tbl1_C1" noWrap><font size="-2">&nbsp;&nbsp;Prompt: <input id="chkTimeRangePrompt" type="checkbox" name="chkTimeRangePrompt" runat="server">
				&nbsp;&nbsp;Level :
				<select class="tbl1_edit" id="selTimeRangeLevel" name="selTimeRangeLevel" runat="server">
				</select>
				&nbsp;&nbsp;Range Start :<input class="tbl1_edit" id="txtTimeRangeStart" type="text" maxLength="8" size="8" name="txtTimeRangeStart"
					runat="server"> &nbsp;&nbsp;Range End :<input class="tbl1_edit" id="txtTimeRangeEnd" type="text" maxLength="8" size="8" name="txtTimeRangeEnd"
					runat="server"> </font>
		</td>
		<td class="tbl1_C1"><asp:button id="btnAddTimeRange" runat="server" CssClass="tbl1_ctrl" Runat="server" Height="22px"
				Width="75px" CommandName="Add" Text="Add"></asp:button></td>
	</tr>
	<tr id="calcRatioCell" runat="server">
		<td class="tbl1_H" noWrap><font size="-2">&nbsp;Add Ratio: &nbsp;</font>
		</td>
		<td class="tbl1_C1" noWrap><font size="-2">&nbsp;&nbsp;Type
				<select class="tbl1_edit" id="selRatioType" name="selRatioType" runat="server">
				</select>
				&nbsp;&nbsp;Measure
				<select class="tbl1_edit" id="selRatioMeasure" style="WIDTH: 250px" name="selRatioMeasure"
					runat="server">
				</select>
				&nbsp;&nbsp;Dimension
				<select class="tbl1_edit" id="selRatioDim" style="WIDTH: 150px" name="selRatioDim" runat="server">
				</select>
			</font>
		</td>
		<td class="tbl1_C1"><asp:button id="btnAddRatio" runat="server" CssClass="tbl1_ctrl" Runat="server" Height="22px"
				Width="75px" CommandName="Add" Text="Add"></asp:button></td>
	</tr>
	<tr id="calcMeaCell" runat="server">
		<td class="tbl1_H" noWrap><font size="-2">&nbsp;Add Calc. Measure: &nbsp;</font>
		</td>
		<td class="tbl1_C1" noWrap><font size="-2">&nbsp;&nbsp;Measure1
				<select class="tbl1_edit" id="selCalcMeasure1" style="WIDTH: 250px" name="selCalcMeasure1"
					runat="server">
				</select>
				&nbsp;&nbsp;Operation
				<select class="tbl1_edit" id="selCalcOperation" name="selCalcOperation" runat="server">
				</select>
				&nbsp;&nbsp;Measure2
				<select class="tbl1_edit" id="selCalcMeasure2" style="WIDTH: 250px" name="selCalcMeasure2"
					runat="server">
				</select>
			</font>
		</td>
		<td class="tbl1_C1"><asp:button id="btnAddCalc" runat="server" CssClass="tbl1_ctrl" Runat="server" Height="22px"
				Width="75px" CommandName="Add" Text="Add"></asp:button></td>
	</tr>
</table>
