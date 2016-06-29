<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashboardServiceTest.aspx.cs" Inherits="WebServices_DashboardServiceTest" ValidateRequest="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Service:
        <asp:TextBox ID="txtServiceUrl" runat="server" Width="454px"></asp:TextBox>&nbsp;
        <br />
        UserId:&nbsp;
        <asp:TextBox ID="txtUserId" runat="server" Width="84px"></asp:TextBox><br />
        <br />
        DataSourceType:
        <asp:TextBox ID="txtDataSourceType" runat="server" Width="84px"></asp:TextBox><br />
        DataSourceId: &nbsp; &nbsp;
        <asp:TextBox ID="txtDataSourceId" runat="server" Width="84px"></asp:TextBox><br />
        <br />
        Commands:&nbsp;
        <br />
        <asp:Button ID="btnGetUserDataSources" runat="server" Text="GetUserDataSources" OnClick="btnGetUserDataSources_Click" /><br />
        <asp:Button ID="btnGetUserGauges" runat="server" Text="GetUserGauges" OnClick="btnGetUserGauges_Click" /><br />
        <asp:Button ID="btnSaveUserGaugeConfig" runat="server" Text="SaveUserGaugeConfig" OnClick="btnSaveUserGaugeConfig_Click" /><br />
        <asp:Button ID="btnSaveUserGaugeContainerConfig" runat="server" Text="SaveUserGaugeContainerConfig" OnClick="btnSaveUserGaugeContainerConfig_Click" /><br />
        <asp:Button ID="btnGetUserGaugeConfig" runat="server" Text="GetUserGaugeConfig" OnClick="btnGetUserGaugeConfig_Click" /><br />
        <asp:Button ID="btnGetUserGaugeConfigExec" runat="server" Text="GetUserGaugeConfig(ExecuteQueries)" OnClick="btnGetUserGaugeConfigExec_Click" /><br />
        <asp:Button ID="btnGetDataTable" runat="server" Text="GetDataTable" OnClick="btnGetDataTable_Click" /><br />
        <asp:Button ID="btnExecuteQueries" runat="server" Text="ExecuteQueries" OnClick="btnExecuteQueries_Click" />&nbsp;<br />
        <br />
        Request:<br />
        <asp:TextBox ID="txtCommand" runat="server" Height="356px" Width="1064px" EnableViewState="false"></asp:TextBox><br />
        <br />
        Response:<br />
        <asp:TextBox ID="txtResponse" runat="server" Height="356px" Width="1064px" EnableViewState="false"></asp:TextBox></div>
    </form>
</body>
</html>
