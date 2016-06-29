using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using FI.Common;
using FI.Common.Data;
using FI.Common.DataAccess;
using FI.BusinessObjects;
using FI.BusinessObjects.Olap;

/// <summary>
/// Summary description for DashboardServiceServer
/// </summary>
public class DashboardServiceServer
{
    public enum DataSourceType
    {
        OLAP = 1
    }

    //private static StringDictionary __commandDict = null;
    //public static void InitCommandDictionary()
    //{
    //    if(__commandDict==null)
    //        lock (typeof(DashboardServiceServer))
    //        {
    //            __commandDict = new StringDictionary();
    //            __commandDict.Add("COMMAND", "CMD");
    //            __commandDict.Add("ERROR", "ERR");
    //            __commandDict.Add("TYPE", "T");
    //        }

    //    return __commandDict;
    //}

    //public static string EncodeCommand(string command)
    //{
    //}

    //public static string DecodeCommand(string command)
    //{
    //}

    private static ListDictionary __cachedDataSources = new ListDictionary();
    public static int __maxCellsetAxis0Count = 40;
    public static int __maxCellsetAxis1Count = 100;

    public static void ClearReportsCache()
    {
        __cachedDataSources.Clear();
    }

    public static string ExecuteCommand(string command)
    {
        try
        {
            if (command == null || command == "")
                throw new Exception("Empty command");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(command);

            XmlElement cmdEl = doc.FirstChild as XmlElement;
            if (cmdEl != null && string.Compare(cmdEl.Name, "COMMAND", true) == 0)
            {
                string cmdType = cmdEl.GetAttribute("TYPE");
                if (string.Compare(cmdType, "GetUserDataSources", true) == 0)
                    return GetUserDataSources(cmdEl);
                else if (string.Compare(cmdType, "GetUserGauges", true) == 0)
                    return GetUserGauges(cmdEl);
                else if (string.Compare(cmdType, "GetUserGaugeConfig", true) == 0)
                    return GetUserGaugeConfig(cmdEl);
                else if (string.Compare(cmdType, "SaveUserGaugeConfig", true) == 0)
                {
                    SaveUserGaugeConfig(cmdEl);
                    return null;
                }
                else if (string.Compare(cmdType, "DeleteUserGaugeConfig", true) == 0)
                {
                    DeleteUserGaugeConfig(cmdEl);
                    return null;
                }
                else if (string.Compare(cmdType, "GetDataTable", true) == 0)
                    return GetDataTable(cmdEl);
                else if (string.Compare(cmdType, "ExecuteQueries", true) == 0)
                {
                    bool queryDef = (cmdEl.GetAttribute("QUERYDEF") == "1");
                    return ExecuteQueries(cmdEl, queryDef);
                }
            }

            throw new Exception("Unrecognized command");
        }
        catch (Exception exc)
        {
            return ErrorToXml(exc);
        }
    }
    
    public static string ErrorToXml(Exception exc)
    {
        XmlDocument doc = new XmlDocument();
        doc.AppendChild(doc.CreateElement("ERROR"));
        doc.FirstChild.InnerText=(exc.InnerException==null ? exc.Message : exc.InnerException.Message);
        return doc.OuterXml;
    }


    public static string GetUserDataSources(XmlElement cmdEl)
    {
        decimal userId = decimal.Parse(cmdEl.GetAttribute("USERID"));
        FIDataTable dt = ReportSystem.GetReportHeaders(userId, typeof(OlapReport));

        XmlDocument doc = new XmlDocument();
        XmlElement rootEl = (XmlElement)doc.AppendChild(doc.CreateElement("DATASOURCES"));
        if (dt != null && dt.Rows.Count != 0)
            foreach (DataRow dr in dt.Rows)
            {
                XmlElement el = (XmlElement)rootEl.AppendChild(doc.CreateElement("DATASOURCE"));
                el.SetAttribute("TYPE", "OLAP");
                el.SetAttribute("ID", dr["id"].ToString());
                el.SetAttribute("NAME", dr["name"].ToString());
                el.SetAttribute("DESCR", dr["description"].ToString());
            }
        return doc.OuterXml;
    }


    public static string GetUserGauges(XmlElement cmdEl)
    {
        decimal userId = decimal.Parse(cmdEl.GetAttribute("USERID"));

        IDashboardSystemDA dacObj = DataAccessFactory.Instance.GetDashboardSystemDA();
        FIDataTable dt = dacObj.GetUserGauges(userId);

        XmlDocument doc = new XmlDocument();
        XmlElement rootEl = (XmlElement)doc.AppendChild(doc.CreateElement("GAUGES"));
        if (dt != null && dt.Rows.Count != 0)
            foreach (DataRow dr in dt.Rows)
            {
                XmlElement el = (XmlElement)rootEl.AppendChild(doc.CreateElement("GAUGE"));
                el.SetAttribute("ID", dr["id"].ToString());
                el.SetAttribute("USERID", dr["user_id"].ToString());
                el.SetAttribute("NAME", dr["name"].ToString());
                el.SetAttribute("TYPE", dr["type"].ToString());
                el.SetAttribute("X", dr["x"].ToString());
                el.SetAttribute("Y", dr["y"].ToString());
                el.SetAttribute("WIDTH", dr["width"].ToString());
                el.SetAttribute("HEIGHT", dr["height"].ToString());
                el.SetAttribute("VISIBLE", dr["visible"].ToString());
            }
        return doc.OuterXml;
    }

    public static string GetUserGaugeConfig(XmlElement cmdEl)
    {
        decimal userId = decimal.Parse(cmdEl.GetAttribute("USERID"));

        IDashboardSystemDA dacObj = DataAccessFactory.Instance.GetDashboardSystemDA();
        XmlDocument doc = new XmlDocument();
        XmlElement rootEl = (XmlElement)doc.AppendChild(doc.CreateElement("GAUGES"));

        foreach (XmlElement reqEl in cmdEl.SelectNodes("GAUGE"))
        {
            Guid gaugeId = new Guid(reqEl.GetAttribute("ID"));
            bool queryDef = (reqEl.GetAttribute("QUERYDEF") == "1");
            bool execQueries = (reqEl.GetAttribute("QUERYRESULT") == "1");

            FIDataTable dt = dacObj.GetUserGaugeConfig(userId, gaugeId);
            XmlElement el = (XmlElement)rootEl.AppendChild(doc.CreateElement("GAUGE"));
            if (dt != null && dt.Rows.Count != 0)
            {
                DataRow dr = dt.Rows[0];
                el.SetAttribute("ID", dr["id"].ToString());
                el.SetAttribute("USERID", dr["user_id"].ToString());
                el.SetAttribute("NAME", dr["name"].ToString());
                el.SetAttribute("TYPE", dr["type"].ToString());
                el.SetAttribute("X", dr["x"].ToString());
                el.SetAttribute("Y", dr["y"].ToString());
                el.SetAttribute("WIDTH", dr["width"].ToString());
                el.SetAttribute("HEIGHT", dr["height"].ToString());
                el.SetAttribute("VISIBLE", dr["visible"].ToString());
                el.SetAttribute("REFRESH", dr["refresh"].ToString());

                // config, parse and execute if needed
                el.InnerXml = dr["config"].ToString();
                if (execQueries)
                {
                    foreach(XmlElement queryEl in el.SelectNodes("VAL[@TYPE='QUERY']"))
                        ExecuteQueries(queryEl, queryDef);                    
                }
            }
        }
        return doc.OuterXml;
    }

    public static void DeleteUserGaugeConfig(XmlElement cmdEl)
    {
        IDashboardSystemDA dacObj = DataAccessFactory.Instance.GetDashboardSystemDA();

        decimal userId = decimal.Parse(cmdEl.GetAttribute("USERID"));
        foreach (XmlElement el in cmdEl.SelectNodes("GAUGE"))
        {
            dacObj.DeleteUserGaugeConfig(
                new Guid(el.GetAttribute("ID")),
                userId
                );
        }
    }

    public static void SaveUserGaugeConfig(XmlElement cmdEl)
    {
        IDashboardSystemDA dacObj = DataAccessFactory.Instance.GetDashboardSystemDA();

        decimal userId = decimal.Parse(cmdEl.GetAttribute("USERID"));
        foreach (XmlElement el in cmdEl.SelectNodes("GAUGE"))
        {
            dacObj.SaveUserGaugeConfig(
                new Guid(el.GetAttribute("ID")),
                userId,
                (!el.HasAttribute("NAME") ? null : el.GetAttribute("NAME")),
                (!el.HasAttribute("TYPE") ? null : el.GetAttribute("TYPE")),
                (!el.HasAttribute("X") ? -1 : int.Parse(el.GetAttribute("X"))),
                (!el.HasAttribute("Y") ? -1 : int.Parse(el.GetAttribute("Y"))),
                (!el.HasAttribute("WIDTH") ? -1 : int.Parse(el.GetAttribute("WIDTH"))),
                (!el.HasAttribute("HEIGHT") ? -1 : int.Parse(el.GetAttribute("HEIGHT"))),
                (!el.HasAttribute("VISIBLE") ? -1 : int.Parse(el.GetAttribute("WIDTH"))),
                (!el.HasAttribute("REFRESH") ? -1 : int.Parse(el.GetAttribute("REFRESH"))),
                (el.InnerXml=="" ? null : el.InnerXml)
                );
        }
    }

    public static object ExecuteDataSource(string userId, string type, string id)
    {
        if (type == "OLAP")
        {
            // get cached or retrieve
            Cellset cst = null;
            if (__cachedDataSources.Contains(type + "." + id))
                return __cachedDataSources[type + "." + id] as Cellset;
            else
            {
                decimal rptId = decimal.Parse(id);
                IOlapReportsDA dacObj = DataAccessFactory.Instance.GetOlapReportsDA();
                string cellsetStr = dacObj.GetCachedReportResult(rptId);
                if (cellsetStr != null && cellsetStr != "")
                {
                    cst = new Cellset();
                    cst.LoadCellset(cellsetStr, __maxCellsetAxis0Count, __maxCellsetAxis1Count);
                    __cachedDataSources[type + "." + id] = cst;
                }

                //if (cst == null)
                //{
                //    User usr = new User(decimal.Parse(userId), false);
                //    OlapReport rpt=usr.ReportSystem.GetReport(rptId, typeof(OlapReport), true) as OlapReport;
                //    if (rpt != null)
                //    {
                //        rpt.Execute();
                //        cst = rpt.Cellset;
                //    }
                //}

                return cst;
            }
        }
        else
            throw new NotSupportedException("Not supported datasource type: " + type);
    }

    public static string GetDataTable(XmlElement cmdEl)
    {
        string userId = cmdEl.GetAttribute("USERID");
        string dataSource = cmdEl.GetAttribute("DATASOURCE").ToUpper();
        string dataSourceId = cmdEl.GetAttribute("DATASOURCEID");

        Cellset cst = ExecuteDataSource(userId, dataSource, dataSourceId) as Cellset;
        return (cst == null ? null : cst.ToXmlString(__maxCellsetAxis0Count, __maxCellsetAxis1Count));
    }




    public static string ExecuteSqlCommandQuery(XmlElement queryEl)
    {
        string dataSource = queryEl.GetAttribute("DATASOURCE").ToUpper();
        if (dataSource != "SQLSCALAR")
            return null;

        XmlElement sqlEl=queryEl.SelectSingleNode("SQL") as XmlElement;
        if (sqlEl == null)
            return null;
        string sql = sqlEl.InnerText;
        ICustomSqlReportsDA dacObj = DataAccessFactory.Instance.GetCustomSqlReportsDA();
        object ret=dacObj.ExecuteScalarOLTP(sql, null);
        return (ret == null || ret == DBNull.Value ? "" : ret.ToString());
    }

    public static void ExecuteOlapReportQuery(XmlElement queryEl, out int axis0Pos, out int axis1Pos, out Cell cell)
    {
        cell = null;
        axis0Pos = -1;
        axis1Pos = -1;
        string userId = queryEl.GetAttribute("USERID");
        string dataSource = queryEl.GetAttribute("DATASOURCE").ToUpper(); 
        if (dataSource != "OLAP")
            return;
        string dataSourceId = queryEl.GetAttribute("DATASOURCEID");
        

        // get cellset
        Cellset cst = ExecuteDataSource(userId, dataSource, dataSourceId) as Cellset;

        // lookup cell
        if (cst != null)
        {
            StringCollection lookups = new StringCollection();
            foreach (XmlElement lookupEl in queryEl.SelectNodes("LOOKUP"))
                lookups.Add(lookupEl.GetAttribute("UN"));

            cell = cst.LookupCell(lookups, out axis0Pos, out axis1Pos);
        }
    }

    public static string ExecuteQueries(XmlElement cmdEl, bool leaveQueryDef)
    {
        // retrieve data sources, cache them and lookup values
        foreach (XmlElement queryEl in cmdEl.SelectNodes("QUERY"))
        {
            string dataSource = queryEl.GetAttribute("DATASOURCE").ToUpper();            
            if (dataSource == "OLAP")
            {
                Cell cell = null;
                int axis0Pos = -1;
                int axis1Pos = -1;
                ExecuteOlapReportQuery(queryEl, out axis0Pos, out axis1Pos, out cell);
                if (!leaveQueryDef)
                    queryEl.InnerText = null;

                XmlElement resultEl = (XmlElement)queryEl.AppendChild(cmdEl.OwnerDocument.CreateElement("RESULT"));
                if (cell != null)
                {
                    resultEl.SetAttribute("COL", axis0Pos.ToString());
                    resultEl.SetAttribute("ROW", axis1Pos.ToString());
                    resultEl.InnerXml = cell.FormattedValue;
                }
            }
            else if (dataSource == "SQLSCALAR")
            {
                string ret = "";
                bool hasResult = false;
                try
                {
                    ret = ExecuteSqlCommandQuery(queryEl);
                    hasResult = true;
                }
                catch (Exception exc)
                {
                    // ignore
                }

                if (!leaveQueryDef)
                    queryEl.InnerText = null;
                if (hasResult)
                {
                    XmlElement resultEl = (XmlElement)queryEl.AppendChild(cmdEl.OwnerDocument.CreateElement("RESULT"));
                    resultEl.InnerText = ret;
                }
            }
            else
                throw new NotSupportedException("Not supported datasource type: " + dataSource);
        }

        return cmdEl.InnerXml;
        /*
        XmlDocument doc = new XmlDocument();
        XmlElement rootEl=(XmlElement)doc.AppendChild(doc.CreateElement("VALUES"));
        foreach(string val in cellVals)
        {
            XmlElement valEl = (XmlElement)rootEl.AppendChild(doc.CreateElement("QUERYVAL"));
            valEl.InnerXml = (val==null ? "" : val);
        }
        */
    }
}
