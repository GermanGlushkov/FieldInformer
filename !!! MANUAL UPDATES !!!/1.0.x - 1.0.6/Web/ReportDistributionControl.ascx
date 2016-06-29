<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReportDistributionControl.ascx.cs" Inherits="FI.UI.Web.ReportDistributionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE class="t_act" id="Table2" height="100%" cellSpacing="10" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE class="bckgr1" id="Table2" height="100%" cellSpacing="3" cellPadding="3" width="100%"
				border="0">
				<TR>
					<TD class="tbl1_capt">&nbsp;Distribute Report:</TD>
				</TR>
				<TR>
					<TD class="bckgr1"><asp:panel id="ReportPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt">&nbsp;To Contacts:</TD>
				</TR>
				<TR>
					<TD><asp:panel id="DistributionPanel" runat="server"></asp:panel></TD>
				</TR>
				<TR>
					<TD class="tbl1_capt">&nbsp;Distribution Options:</TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="5" width="100%" border="0">
							<TR>
								<TD class="bckgr1">
									<asp:RadioButton id="radioNone" GroupName="distr" Text="None" runat="server"></asp:RadioButton>
								</TD>
							</TR>
							<TR>
								<TD class="bckgr1"><asp:radiobutton id="radioWeekDays" runat="server" Text="WeekDays:" GroupName="distr"></asp:radiobutton></TD>
								<TD class="bckgr1" width="70%">
									&nbsp;&nbsp;<asp:CheckBox id="chkMon" Text="Mon" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkTue" Text="Tue" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkWed" Text="Wed" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkThu" Text="Thu" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkFri" Text="Fri" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkSat" Text="Sat" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkSun" Text="Sun" runat="server"></asp:CheckBox>
								</TD>
							</TR>
							<TR>
								<TD class="bckgr1">
									<asp:RadioButton id="radioWeeks" GroupName="distr" Text="Weeks" runat="server"></asp:RadioButton>&nbsp;(On 
									Monday)
								</TD>
								<TD class="bckgr1" width="70%">
									&nbsp;&nbsp;<asp:CheckBox id="chkWeek1" Text="Week1" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkWeek2" Text="Week2" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkWeek3" Text="Week3" runat="server"></asp:CheckBox>
									&nbsp;&nbsp;<asp:CheckBox id="chkWeek4" Text="Week4" runat="server"></asp:CheckBox>
								</TD>
							</TR>
							<TR>
								<TD class="bckgr1">
									<asp:RadioButton id="radioMonthly" GroupName="distr" Text="Monthly" runat="server"></asp:RadioButton>&nbsp;(1st 
									Day Of Month)
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tbl1_capt" vAlign="middle" align="left" height="0px">
						<asp:button id="UpdateButton" CommandName="Update" Runat="server" Text="Update" Width="75px"
							Height="25px" CssClass="tbl1_ctrl"></asp:button>
						<asp:button id="ViewLogButton" Text="View Log" CssClass="tbl1_ctrl" Height="25px" Width="75px"
							Runat="server" CommandName="Cancel"></asp:button>
						<asp:button id="SendNowButton" CommandName="Send Now" Runat="server" Text="Send Now (To Selected Contacts)"
							Width="220px" Height="25px" CssClass="tbl1_ctrl"></asp:button>
						<asp:button id="BackButton" CommandName="Back" Runat="server" Text="Back" Width="75px" Height="25px"
							CssClass="tbl1_ctrl"></asp:button>
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
