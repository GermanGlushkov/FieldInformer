using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

/// <summary>
/// Summary description for DashboardServiceClient
/// </summary>
public class DashboardServiceClient
{
    public static string GenerateGetUserDataSourcesRequest(string userId)
    {
        return string.Format("<COMMAND TYPE='GetUserDataSources' USERID='{0}'/>",
            userId);
    }

    public static string GenerateGetUserGaugesRequest(string userId)
    {
        return string.Format("<COMMAND TYPE='GetUserGauges' USERID='{0}'/>",
            userId);
    }

    public static string GenerateGetUserGaugeConfigRequest(string userId, bool executeQueries)
    {
        return string.Format(
@"<COMMAND TYPE='GetUserGaugeConfig' USERID='{0}'> 
    <GAUGE ID='c4e158c4-1c7a-4795-9cff-7a1a544c44a9' QUERYDEF='1' QUERYRESULT='{1}'/>
    <GAUGE ID='c4e158c4-1c7a-4795-9cff-7a1a544c44a8' QUERYDEF='1' QUERYRESULT='{1}'/>
</COMMAND>",
           userId, (executeQueries ? "1" : "0"));
    }

    public static string GenerateSaveUserGaugeContainerConfigRequest(string userId)
    {
        return string.Format(
@"<COMMAND TYPE='SaveUserGaugeConfig' USERID='{0}'>
    <GAUGE ID='c4e158c4-1c7a-4795-9cff-7a1a544c44a9' X='0' Y='0' WIDTH='100' HEIGHT='400'/>
    <GAUGE ID='c4e158c4-1c7a-4795-9cff-7a1a544c44a8' X='200' Y='200' WIDTH='100' HEIGHT='100'/>
</COMMAND>",
           userId
);
    }

    public static string GenerateSaveUserGaugeConfigRequest(string userId)
    {
        return string.Format(
@"<COMMAND TYPE='SaveUserGaugeConfig' USERID='{0}'>
    <GAUGE ID='c4e158c4-1c7a-4795-9cff-7a1a544c44a9' NAME='test' TYPE='TMeter' X='0' Y='0' WIDTH='100' HEIGHT='400' VISIBLE='True' REFRESH='10'>
        <VAL ID='MIN' TYPE='STATIC'>0.565</VAL>
		<VAL ID='MAX' TYPE='STATIC'>1200</VAL>
		<VAL ID='VAL' TYPE='QUERY'>
			<QUERY DATASOURCE='OLAP' DATASOURCEID='25107'>
        			<LOOKUP UN='[Measures].[Selection 0 Semiadd]'/>
        			<LOOKUP UN='[Store].[Central Chain].[Chain].&amp;[****KESKO]&amp;[K-CITYMARKET]'/>
    		</QUERY>            
		</VAL>
    </GAUGE>
    <GAUGE ID='c4e158c4-1c7a-4795-9cff-7a1a544c44a8' NAME='test1' TYPE='TMeter' X='0' Y='0' WIDTH='100' HEIGHT='100' VISIBLE='True' REFRESH='2'>some config</GAUGE>
</COMMAND>",
           userId
);
    }

    public static string GenerateGetDataTableRequest(string userId, string dataSourceType, string dataSourceId)
    {
        return string.Format("<COMMAND TYPE='GetDataTable'  USERID='{0}' DATASOURCE='{1}' DATASOURCEID='{2}'/>",
            userId, dataSourceType, dataSourceId);
    }

    public static string GenerateExecuteQueriesRequest(string userId, string dataSourceType, string dataSourceId)
    {
        return string.Format(
@"<COMMAND TYPE='ExecuteQueries'>
    <QUERY USERID='{0}' DATASOURCE='{1}' DATASOURCEID='{2}'>
        <LOOKUP UN='[Measures].[Selection 0 Semiadd]'/>
        <LOOKUP UN='[Store].[Central Chain].[Chain].&amp;[****KESKO]&amp;[K-CITYMARKET]'/>
    </QUERY>
    <QUERY USERID='{0}' DATASOURCE='{1}' DATASOURCEID='{2}'>
        <LOOKUP UN='[Measures].[Selection 1 Semiadd]'/>
        <LOOKUP UN='[Store].[Central Chain].[Chain].&amp;[****KESKO]&amp;[K-SUPERMARKET]'/>
    </QUERY>
</COMMAND>",
            userId, dataSourceType, dataSourceId);
    }
}
