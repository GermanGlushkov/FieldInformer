using System;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.AnalysisServices.AdomdClient;


namespace OlapSystem.Migration
{
    public class ReportValidator
    {        
        ReportsValidation _reports = null;
        SourceSchemaMap _map;
        DestSchema _destSchema = new DestSchema();
        CubeDef _srcCube;
        CubeDef _destCube;
        StringCollection _srcUnmatched = new StringCollection();
        StringCollection _destUnmatched = new StringCollection();

        Hashtable _cchIndex;
        Hashtable _chnIndex;
        Hashtable _storeChnIndex;
        Hashtable _storeCchIndex;
        Hashtable _storePcodeIndex;
        Hashtable _timeMonthlyIndex;
        Hashtable _timeWeeklyIndex;
        Hashtable _salesforceIndex;
        Hashtable _storeSalesforceIndex;

        public string DefaultMapFilePath
        {
            get { return ConfigurationManager.AppSettings["SchemaMapFilePath"]; }
        }

        public SourceSchemaMap.MapDataTable MapTable
        {
            get { return (_map==null ? null : _map.Map); }
        }

        public DestSchema.SchemaDataTable DestSchemaTable
        {
            get { return _destSchema.Schema; }
        }

        public ReportsValidation.OlapReportsDataTable ReportsTable
        {
            get { return (_reports==null ? null : _reports.OlapReports); }
        }

        public StringCollection UnmatchedObjects
        {
            get { return _destUnmatched;}
        }

        public void LoadAdomdObjects()
        {
            AdomdConnection srcConn = new AdomdConnection(ConfigurationManager.AppSettings["SrcOlapConnection"]);
            srcConn.Open();
            _srcCube = srcConn.Cubes[ConfigurationManager.AppSettings["SrcCube"]];

            string destWindowsUserName = ConfigurationManager.AppSettings["DestWindowsUserName"];
            string destWindowsPassword = ConfigurationManager.AppSettings["DestWindowsPassword"];
            if (destWindowsUserName!=null && destWindowsUserName != "")
                SecurityHelper.Impersonate(destWindowsUserName, destWindowsPassword);

            AdomdConnection destConn = new AdomdConnection(ConfigurationManager.AppSettings["DestOlapConnection"]);
            destConn.Open();
            _destCube = destConn.Cubes[ConfigurationManager.AppSettings["DestCube"]];
        }

        public void CloseAdomdObjects()
        {
            if (_srcCube != null && _srcCube.ParentConnection != null)
                _srcCube.ParentConnection.Close(true);

            if (_destCube != null && _destCube.ParentConnection != null)
                _destCube.ParentConnection.Close(true);
        }

        public void LoadMap(string filePath)
        {
            if (_map != null)
                return;
            if(filePath==null || filePath=="")
                throw new ArgumentException("Invalid path");

            try
            {
                // load schema from file
                _map = new SourceSchemaMap();
                _map.BeginInit();
                if (File.Exists(filePath))
                    _map.ReadXml(filePath);                
                _map.AcceptChanges();

                // remove unmatched members from source schema
                for (int i = _map.Map.Rows.Count - 1; i >= 0; i--)
                {
                    if (SourceSchemaMap.LookupAdomdSchemaObject(_srcCube, _map.Map[i]) == null)
                        _map.Map.Rows.RemoveAt(i);
                }

                // add or validate existing memebrs
                foreach (Dimension dim in _srcCube.Dimensions)
                {
                    if (_map.Map.FindBySourceUniqueName("Dimension:" + dim.UniqueName) == null)
                        _map.Map.AddMapRow("Dimension:" + dim.UniqueName, "");

                    foreach (Hierarchy hier in dim.Hierarchies)
                    {
                        if (_map.Map.FindBySourceUniqueName("Hierarchy:" + hier.UniqueName) == null)
                            _map.Map.AddMapRow("Hierarchy:" + hier.UniqueName, "");

                        foreach (Level lev in hier.Levels)
                        {
                            if (_map.Map.FindBySourceUniqueName("Level:" + lev.UniqueName) == null)
                                _map.Map.AddMapRow("Level:" + lev.UniqueName, "");
                        }

                        // measures handled separately
                        if (dim.DimensionType != DimensionTypeEnum.Measure)
                        {
                            // default member or first member in first level
                            Member defMem = (Member)SourceSchemaMap.LookupAdomdSchemaObject(_srcCube, "Member", hier.DefaultMember);
                            if (defMem == null)
                                defMem = hier.Levels[0].GetMembers(0, 1)[0];
                            if (_map.Map.FindBySourceUniqueName("Member:" + defMem.UniqueName) == null)
                                _map.Map.AddMapRow("Member:" + defMem.UniqueName, "");

                            // calculated members
                            MemberCollection mems =
                                (hier.Levels[0].LevelType == LevelTypeEnum.All ?
                                hier.Levels[1].GetMembers() : hier.Levels[0].GetMembers());
                            foreach (Member mem in mems)
                            {
                                if (mem.Type != MemberTypeEnum.Formula)
                                    continue;

                                if (_map.Map.FindBySourceUniqueName("Member:" + mem.UniqueName) == null)
                                    _map.Map.AddMapRow("Member:" + mem.UniqueName, "");
                            }
                        }
                    }

                    // measures
                    if (dim.DimensionType == DimensionTypeEnum.Measure)
                        foreach (Measure mea in _srcCube.Measures)
                        {
                            if (_map.Map.FindBySourceUniqueName("Measure:" + mea.UniqueName) == null)
                                _map.Map.AddMapRow("Measure:" + mea.UniqueName, "");
                        }


                }

                // load dest schema
                _destSchema.BeginInit();

                // empty member
                _destSchema.Schema.AddSchemaRow("");

                // dimension hierarchies
                foreach (Dimension dim in _destCube.Dimensions)
                {
                    if (_destSchema.Schema.FindByUniqueName("Dimension:" + dim.UniqueName) == null)
                        _destSchema.Schema.AddSchemaRow("Dimension:" + dim.UniqueName);

                    foreach (Hierarchy hier in dim.Hierarchies)
                    {
                        if (_destSchema.Schema.FindByUniqueName("Hierarchy:" + hier.UniqueName) == null)
                            _destSchema.Schema.AddSchemaRow("Hierarchy:" + hier.UniqueName);

                        foreach (Level lev in hier.Levels)
                        {
                            if (_destSchema.Schema.FindByUniqueName("Level:" + lev.UniqueName) == null)
                                _destSchema.Schema.AddSchemaRow("Level:" + lev.UniqueName);
                        }

                        // measures handeled separately
                        if (dim.DimensionType != DimensionTypeEnum.Measure)
                        {
                            // default member or first member in first level
                            Member defMem = (Member)SourceSchemaMap.LookupAdomdSchemaObject(_destCube, "Member", hier.DefaultMember);
                            if (defMem == null)
                                defMem = hier.Levels[0].GetMembers(0, 1)[0];
                            if (_destSchema.Schema.FindByUniqueName("Member:" + defMem.UniqueName) == null)
                                _destSchema.Schema.AddSchemaRow("Member:" + defMem.UniqueName);

                            // calculated members
                            MemberCollection mems =
                                (hier.Levels[0].LevelType == LevelTypeEnum.All ?
                                hier.Levels[1].GetMembers() : hier.Levels[0].GetMembers());
                            foreach (Member mem in mems)
                            {
                                if (mem.Type != MemberTypeEnum.Formula)
                                    continue;

                                if (_destSchema.Schema.FindByUniqueName("Member:" + mem.UniqueName) == null)
                                    _destSchema.Schema.AddSchemaRow("Member:" + mem.UniqueName);
                            }
                        }
                    }

                    // measures
                    if (dim.DimensionType == DimensionTypeEnum.Measure)
                        foreach (Measure mea in _destCube.Measures)
                        {
                            if (_destSchema.Schema.FindByUniqueName("Measure:" + mea.UniqueName) == null)
                                _destSchema.Schema.AddSchemaRow("Measure:" + mea.UniqueName);
                        }
                }


                // merge dest schema to file
                foreach (SourceSchemaMap.MapRow srcRow in _map.Map.Rows)
                {
                    // assign is empty and exact match
                    if (srcRow.IsDestUniqueNameNull() || srcRow.DestUniqueName == "")
                    {
                        DestSchema.SchemaRow destRow = _destSchema.Schema.FindByUniqueName(srcRow.SourceUniqueName);
                        srcRow.DestUniqueName = (destRow == null ? "" : destRow.UniqueName);
                    }
                    else // check assigned
                    {
                        DestSchema.SchemaRow destRow = _destSchema.Schema.FindByUniqueName(srcRow.DestUniqueName);
                        if (destRow == null)
                            srcRow.DestUniqueName = "";
                    }
                }
            }
            finally
            {
                if (_map != null)
                    _map.EndInit();

                if (_destSchema != null)
                {
                    _destSchema.AcceptChanges();
                    _destSchema.EndInit();
                }
            }
        }

        public void SaveMap(string filePath)
        {
            if (_map == null)
                return;

            _map.WriteXml(filePath);
            _map.AcceptChanges();
        }

        public void LoadReports()
        {
            if (_reports != null)
                return;

            SqlConnection reportConn = null;
            try
            {
                // create dataset
                _reports = new ReportsValidation();
                _reports.BeginInit();

                // open connections
                reportConn = new SqlConnection(ConfigurationManager.AppSettings["DBFINFConnection"]);
                reportConn.Open();
                string companyId = ConfigurationManager.AppSettings["DBFINFCompanyId"];

                // commit unsaved reports
                SqlCommand cmd = reportConn.CreateCommand();
                cmd.CommandText = string.Format(
@"update t_olap_reports set data=s.data, open_nodes='<NODES />'
from t_olap_reports, t_olap_reports_state s, tusers u 
where 
t_olap_reports.user_id=u.id and u.company_id={0}
and t_olap_reports.id=s.rpt_id and s.is_current=1", companyId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                // delete report history
                cmd = reportConn.CreateCommand();
                cmd.CommandText = string.Format(
@"delete from t_olap_reports_state 
where rpt_id in (select r.id from t_olap_reports r , tusers u
where r.user_id=u.id and u.company_id={0})", companyId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                // load reports data
                cmd = reportConn.CreateCommand();
                cmd.CommandText = string.Format(
@"select r.Id, r.Name, r.Description, u.Name as [User], r.data as ReportXml, 
0 as ObjectsTotal, 0 as ObjectsInvalid, 0 as ObjectsInvalidNew, 0 as InvalidDiff 
from tusers u inner join t_olap_reports r
on u.id=r.user_id
where u.company_id={0}
order by r.Id", companyId);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.AcceptChangesDuringFill = true;
                ad.Fill(_reports.OlapReports);
                cmd.Dispose();
            }
            catch (Exception exc)
            {
                _reports = null;
                throw exc;
            }
            finally
            {
                if (reportConn != null)
                    reportConn.Close();

                if (_reports != null)                
                    _reports.EndInit();                
            }
        }

        public void ValidateReports()
        {
            if (_reports == null)
                return;

            try
            {
                _reports.BeginInit();

                foreach (ReportsValidation.OlapReportsRow row in _reports.OlapReports)
                    ConvertOlapReport(row);
            }
            finally
            {
                if (_reports != null)
                    _reports.EndInit();
            }
        }

        
        public void SaveReports()
        {
            if (_reports == null)
                return;

            SqlConnection reportConn = null ;
            try
            {
                _reports.BeginInit();

                // open connection
                reportConn = new SqlConnection(ConfigurationManager.AppSettings["DBFINFConnection"]);
                reportConn.Open();

                // save changed and accept cahnges
                for (int i = 0; i < _reports.OlapReports.Count; i++)
                {
                    ReportsValidation.OlapReportsRow row = _reports.OlapReports[i];
                    if (row.RowState != System.Data.DataRowState.Modified)
                        continue;

                    // save xml
                    SqlCommand cmd = reportConn.CreateCommand();
                    cmd.CommandText = "update t_olap_reports set data=@data where id=@id";
                    cmd.Parameters.AddWithValue("@id", row.Id);
                    cmd.Parameters.AddWithValue("@data", row.ReportXml);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    // accept
                    row.AcceptChanges();
                }
            }
            finally
            {
                if (reportConn != null)
                    reportConn.Close();

                if (_reports != null)
                    _reports.EndInit();
            }
        }


        private void ConvertOlapReport(ReportsValidation.OlapReportsRow row)
        {
            if (row == null || row.IsReportXmlNull() || row.ReportXml == "")
                return;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(row.ReportXml);

            // clear stats
            row.ObjectsInvalid = 0;
            row.ObjectsInvalidNew = 0;
            row.InvalidDiff = 0;

            // axes
            foreach (XmlElement axisEl in doc.GetElementsByTagName("A"))
            {
                int axisOrdinal = int.Parse(axisEl.GetAttribute("ORD"));

                // dimensions
                foreach (XmlElement el in axisEl.GetElementsByTagName("D"))
                {
                    string un = el.GetAttribute("UN");
                    un = ValidateOlapObject(SchemaObjectType.ObjectTypeDimension, un, row);
                    el.SetAttribute("UN", un);
                    row.ObjectsTotal++;
                }

                // hierarchies
                foreach (XmlElement el in axisEl.GetElementsByTagName("H"))
                {
                    string un = el.GetAttribute("UN");
                    un = ValidateOlapObject(SchemaObjectType.ObjectTypeHierarchy, un, row);
                    el.SetAttribute("UN", un);
                    row.ObjectsTotal++;
                }

                // levels
                foreach (XmlElement el in axisEl.GetElementsByTagName("L"))
                {
                    string un = el.GetAttribute("UN");
                    un = ValidateOlapObject(SchemaObjectType.ObjectTypeLevel, un, row);
                    el.SetAttribute("UN", un);
                    row.ObjectsTotal++;
                }

                // members
                foreach (XmlElement el in axisEl.GetElementsByTagName("M"))
                {
                    string un = el.GetAttribute("UN");
                    string calc = el.GetAttribute("C");

                    if (calc != "1")
                    {
                        un = ValidateOlapObject(SchemaObjectType.ObjectTypeMember, un, row);
                        el.SetAttribute("UN", un);
                        row.ObjectsTotal++;
                    }
                }

                // calc members, cannot use enumerator because list of nodes is being changed inside
                XmlNodeList list = axisEl.GetElementsByTagName("M");
                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement el = list[i] as XmlElement;
                    if (el == null)
                        continue;

                    // special conversion of [Time].[Monthly].[Year].&[XXXX].Children calculated member
                    bool converted = ConvertYearChildrenCalculatedMember(el);
                    if (converted)
                        row.ObjectsTotal++;
                }

                // order tuple members
                foreach (XmlElement el in axisEl.GetElementsByTagName("OTM"))
                {
                    string un = el.GetAttribute("UN");

                    un = ValidateOlapObject(SchemaObjectType.ObjectTypeMember, un, row);
                    el.SetAttribute("UN", un);
                    row.ObjectsTotal++;
                }
            }

            // save back
            row.ReportXml = doc.OuterXml;
        }

        private string ValidateOlapObject(SchemaObjectType type, string uniqueName, ReportsValidation.OlapReportsRow row)
        {
            // resolve source object
            object srcObject = (_srcUnmatched.Contains(uniqueName) ? null : SourceSchemaMap.LookupAdomdSchemaObject(_srcCube, type, uniqueName));
            if (srcObject == null)
            {
                if (!_srcUnmatched.Contains(uniqueName))
                    _srcUnmatched.Add(uniqueName);
                row.ObjectsInvalid++;
            }

            // try to convert manually
            string destUniqueName = null;
            if (!_destUnmatched.Contains(uniqueName))
            {
                if (uniqueName.StartsWith("[Central Chain]"))
                    destUniqueName = LookupCentralChain(uniqueName);
                else if (uniqueName.StartsWith("[Chain]"))
                    destUniqueName = LookupChain(uniqueName);
                else if (uniqueName.StartsWith("[Store].[Chain]"))
                    destUniqueName = LookupStoreChain(uniqueName);
                else if (uniqueName.StartsWith("[Store].[Central Chain]"))
                    destUniqueName = LookupStoreCentralChain(uniqueName);
                else if (uniqueName.StartsWith("[Store].[Postal Code]"))
                    destUniqueName = LookupStorePostalCode(uniqueName);
                else if (uniqueName.StartsWith("[Time].[Monthly]"))
                    destUniqueName = LookupTimeMonthly(uniqueName);
                else if (uniqueName.StartsWith("[Time].[Weekly]"))
                    destUniqueName = LookupTimeWeekly(uniqueName);
                else if (uniqueName.StartsWith("[Expand Compound Products]"))
                    destUniqueName = LookupExpandCompoundProducts(uniqueName);
                //else if (uniqueName.StartsWith("[Salesforce]"))
                //    destUniqueName = LookupSalesforce(type, uniqueName);
                else if (uniqueName.StartsWith("[Store].[Salesforce]"))
                    destUniqueName = LookupStoreSalesforce(type, uniqueName);
                else if (uniqueName.StartsWith("[Order].[Attributes]"))
                    destUniqueName = LookupOrderAttributes(type, uniqueName);
                else if (uniqueName.StartsWith("[MSA].[Attributes]"))
                    destUniqueName = LookupMSAAttributes(type, uniqueName);

                // convert using map
                if (destUniqueName == null)
                    destUniqueName = _map.ConvertSchemaObjectUN(type, uniqueName);                
            }

            // lookup dest object (if matched)
            if (destUniqueName == null || SourceSchemaMap.LookupAdomdSchemaObject(_destCube, type, destUniqueName) == null)
            {
                destUniqueName=null;
                if (!_srcUnmatched.Contains(uniqueName) && !_destUnmatched.Contains(uniqueName))
                    _destUnmatched.Add(uniqueName);
                row.ObjectsInvalidNew++;
            }
            
            // diff
            row.InvalidDiff = (row.ObjectsInvalidNew - row.ObjectsInvalid);

            // return 
            return (destUniqueName == null ? uniqueName : destUniqueName);
        }

        private string LookupExpandCompoundProducts(string uniqueName)
        {
            if (uniqueName.EndsWith(".&[4]"))
                return "[Expand Compound Products].[Expand Compound Products].[Expand].&[No]";
            else if (uniqueName.EndsWith(".&[3]"))
                return "[Expand Compound Products].[Expand Compound Products].[Expand].&[Yes]";
            else
                return null;
        }

        private string LookupCentralChain(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_cchIndex == null)
            {
                _cchIndex = new Hashtable();
                Hierarchy hier = _destCube.Dimensions["Central Chain"].Hierarchies[0];

                foreach (Member m in hier.Levels["Central Chain"].GetMembers())
                    if (m.Name != "Undefined") // undefined handeled separately
                        _cchIndex.Add(m.Name.ToUpper(), m.UniqueName);

                // undefined
                _cchIndex.Add("Undefined", "[Store].[Central Chain].[Central Chain].&[Default Trade Group]&[Undefined]");
            }


            // get name and lookup from index
            string ret=null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter>=0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4).ToUpper();
                ret = _cchIndex[name] as string;
            }

            return ret;
        }

        private string LookupChain(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_chnIndex == null)
            {
                _chnIndex = new Hashtable();
                Hierarchy hier = _destCube.Dimensions["Chain"].Hierarchies[0];
                foreach (Member m in hier.Levels["Chain"].GetMembers())
                {
                    if (m.Name != "Undefined") // undefined handeled separately
                        _chnIndex.Add(m.Name.ToUpper(), m.UniqueName);
                }

                // undefined
                _chnIndex.Add("Undefined", "[Store].[Chain].[Chain].&[Undefined]&[Undefined]");
            }


            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4).ToUpper();
                ret = _chnIndex[name] as string;
            }

            return ret;
        }


        private string LookupStoreCentralChain(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_storeCchIndex == null)
            {
                _storeCchIndex = new Hashtable();

                Hierarchy hier = _destCube.Dimensions["Store"].Hierarchies["Central Chain"];
                foreach (Member m in hier.Levels["Central Chain"].GetMembers())
                {
                    // get key
                    int delim = m.UniqueName.IndexOf(".&[");
                    string key = m.UniqueName.Substring(delim + 1, m.UniqueName.Length - delim - 1).ToUpper();
                    key = key.Replace("&[", ".&[");

                    _storeCchIndex.Add(key, m.UniqueName);
                }
                foreach (Member m in hier.Levels["Chain"].GetMembers())
                {
                    // get key
                    int delim = m.UniqueName.IndexOf(".&[");
                    string key = m.UniqueName.Substring(delim + 1, m.UniqueName.Length - delim - 1).ToUpper();
                    key = key.Replace("&[", ".&[");

                    _storeCchIndex.Add(key, m.UniqueName);
                }
            }

            // get key and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if(delimiter>=0)
                delimiter = uniqueName.LastIndexOf(".&[", delimiter-1);
            if (delimiter >= 0)
            {
                string key = uniqueName.Substring(delimiter, uniqueName.Length - delimiter).ToUpper();
                ret = _storeCchIndex[key] as string;
            }

            return ret;
        }

        private string LookupStoreChain(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_storeChnIndex == null)
            {
                _storeChnIndex = new Hashtable();

                Hierarchy hier = _destCube.Dimensions["Store"].Hierarchies["Chain"];
                foreach (Member m in hier.Levels["Chain"].GetMembers())
                {
                    if (m.Name != "Undefined") // undefined handeled separately
                    {
                        if (!_storeChnIndex.ContainsKey(m.Name.ToUpper()))
                            _storeChnIndex.Add(m.Name.ToUpper(), m.UniqueName);
                    }
                }

                // undefined
                _storeChnIndex.Add("Undefined", "[Store].[Chain].[Chain].&[Undefined]&[Undefined]");
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4).ToUpper();
                ret = _storeChnIndex[name] as string;
            }

            return ret;
        }


        private string LookupStorePostalCode(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_storePcodeIndex == null)
            {
                _storePcodeIndex = new Hashtable();

                Hierarchy hier = _destCube.Dimensions["Store"].Hierarchies["Postal Code"];
                foreach (Member m in hier.Levels["Postal Code"].GetMembers())
                {
                    // undefined handeled separately 
                    // and also check if code already added, data is incorrect
                    if (m.Name != "Undefined" && !_storePcodeIndex.ContainsKey(m.Name.ToUpper()))
                        _storePcodeIndex.Add(m.Name.ToUpper(), m.UniqueName);
                }

                // undefined
                _storePcodeIndex.Add("Undefined", "[Store].[Postal Code].[Postal Code].&[Undefined]&[Undefined]");
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4).ToUpper();
                ret = _storePcodeIndex[name] as string;
            }

            return ret;
        }

        private string LookupTimeMonthly(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_timeMonthlyIndex == null)
            {
                _timeMonthlyIndex = new Hashtable();
                Hierarchy hier = _destCube.Dimensions["Time"].Hierarchies["Monthly"];
                foreach (Member m in hier.Levels["Month"].GetMembers())
                    _timeMonthlyIndex.Add(m.Name, m.UniqueName);
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4);
                ret = _timeMonthlyIndex[name] as string;
            }

            return ret;
        }

        private string LookupTimeWeekly(string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // create index if not exists
            if (_timeWeeklyIndex == null)
            {
                _timeWeeklyIndex = new Hashtable();
                Hierarchy hier = _destCube.Dimensions["Time"].Hierarchies["Weekly"];
                foreach (Member m in hier.Levels["Week"].GetMembers())
                    _timeWeeklyIndex.Add(m.Name, m.UniqueName);
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4);
                ret = _timeWeeklyIndex[name] as string;
            }

            return ret;
        }



        private string LookupSalesforce(SchemaObjectType type, string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // if from group level, which does not exist
            if(type == SchemaObjectType.ObjectTypeMember && 
                uniqueName.StartsWith("[Salesforce].[Group].&[") &&
                uniqueName.IndexOf(".&[")==uniqueName.LastIndexOf(".&["))
                return "[Salesforce].[Salesforce].[All]";

            // create index if not exists
            if (_salesforceIndex == null)
            {
                _salesforceIndex = new Hashtable();

                Hierarchy hier = _destCube.Dimensions["Salesforce"].Hierarchies["Salesforce"];
                foreach (Member m in hier.Levels["Salesforce"].GetMembers())
                {
                    if (m.Name != "Undefined") // undefined handeled separately
                    {
                        int delim = m.UniqueName.LastIndexOf(".&[");
                        string key = m.UniqueName.Substring(delim + 3, m.UniqueName.Length - delim - 4).ToUpper();
                        _salesforceIndex.Add(key, m.UniqueName);
                    }
                }

                // undefined
                _salesforceIndex.Add("0", "[Salesforce].[Salesforce].[All].UNKNOWNMEMBER");
                _salesforceIndex.Add("Undefined", "[Salesforce].[Salesforce].[All].UNKNOWNMEMBER");
            }

            // get key and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string key = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4).ToUpper();
                ret = _salesforceIndex[key] as string;
            }

            return ret;
        }

        private string LookupStoreSalesforce(SchemaObjectType type, string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            // if from group level, which does not exist
            if (type == SchemaObjectType.ObjectTypeMember &&
                uniqueName.StartsWith("[Store].[Salesforce].[Group].&[") &&
                uniqueName.IndexOf(".&[") == uniqueName.LastIndexOf(".&["))
                return "[Salesforce Stores].[Salesforce Stores].[All]";

            // create index if not exists
            if (_storeSalesforceIndex == null)
            {
                _storeSalesforceIndex = new Hashtable();

                Hierarchy hier = _destCube.Dimensions["Salesforce Stores"].Hierarchies[0];
                foreach (Member m in hier.Levels["Salesforce"].GetMembers())
                {
                    if (m.Name != "Undefined") // undefined handeled separately
                    {
                        int delim = m.UniqueName.LastIndexOf(".&[");
                        string key = m.UniqueName.Substring(delim + 3, m.UniqueName.Length - delim - 4).ToUpper();
                        _storeSalesforceIndex.Add(key, m.UniqueName);
                    }
                }

                foreach (Member m in hier.Levels["Store"].GetMembers())
                {
                    int delim = m.UniqueName.LastIndexOf("&[");
                    string key = m.UniqueName.Substring(delim + 2, m.UniqueName.Length - delim - 3).ToUpper();
                    if (!_storeSalesforceIndex.ContainsKey(key))
                        _storeSalesforceIndex.Add(key, m.UniqueName);
                }

                // undefined
                _storeSalesforceIndex.Add("0", "[Salesforce Stores].[Salesforce Stores].[All].UNKNOWNMEMBER");
                _storeSalesforceIndex.Add("Undefined", "[Salesforce Stores].[Salesforce Stores].[All].UNKNOWNMEMBER");
            }

            // get name and lookup from index
            string ret = null;
            int delimiter = uniqueName.LastIndexOf(".&[");
            if (delimiter >= 0)
            {
                string name = uniqueName.Substring(delimiter + 3, uniqueName.Length - delimiter - 4).ToUpper();
                ret = _storeSalesforceIndex[name] as string;
            }

            return ret;
        }

        private bool ConvertYearChildrenCalculatedMember(XmlElement el)
        {
            if (el == null || el.GetAttribute("C") != "1" || el.GetAttribute("T") != "MEMCHILDRENSET")
                return false;

            XmlElement childEl=(el.ChildNodes.Count==0 ? null : el.ChildNodes[0] as XmlElement);
            if (childEl == null)
                return false;

            string childUN = childEl.GetAttribute("UN");
            if(childUN.StartsWith("[Time].[Monthly].[Year].&["))
            {
                // that's what we're looking for, replace it with quarters
                XmlElement parentEl = (XmlElement)el.ParentNode;
                
                string quarterUN=childUN.Replace("Year", "Quarter");
                quarterUN=quarterUN.Remove(quarterUN.Length-1);                
                
                // Q1
                string un=quarterUN + "Q1]";
                ((XmlElement)el.ChildNodes[0]).SetAttribute("UN", un);
                el.SetAttribute("N", "*SET " + un.Replace("[", "").Replace("]", "").Replace("&","").Replace(".Quarter","") + ".Children*");
                el.SetAttribute("UN", "[" + el.GetAttribute("N") + "]");

                // Q2
                un = quarterUN + "Q2]";
                XmlElement newEl2 = (XmlElement)el.CloneNode(true);
                ((XmlElement)newEl2.ChildNodes[0]).SetAttribute("UN", un);
                newEl2.SetAttribute("N", "*SET " + un.Replace("[", "").Replace("]", "").Replace("&", "").Replace(".Quarter", "") + ".Children*");
                newEl2.SetAttribute("UN", "[" + el.GetAttribute("N") + "]");
                parentEl.InsertAfter(newEl2, el);

                // Q3
                un = quarterUN + "Q3]";
                XmlElement newEl3 = (XmlElement)el.CloneNode(true);
                ((XmlElement)newEl3.ChildNodes[0]).SetAttribute("UN", un);
                newEl3.SetAttribute("N", "*SET " + un.Replace("[", "").Replace("]", "").Replace("&", "").Replace(".Quarter", "") + ".Children*");
                newEl3.SetAttribute("UN", "[" + el.GetAttribute("N") + "]");
                parentEl.InsertAfter(newEl3, newEl2);

                // Q4
                un = quarterUN + "Q4]";
                XmlElement newEl4 = (XmlElement)el.CloneNode(true);
                ((XmlElement)newEl4.ChildNodes[0]).SetAttribute("UN", un);
                newEl4.SetAttribute("N", "*SET " + un.Replace("[", "").Replace("]", "").Replace("&", "").Replace(".Quarter", "") + ".Children*");
                newEl4.SetAttribute("UN", "[" + el.GetAttribute("N") + "]");
                parentEl.InsertAfter(newEl4, newEl3);

                return true;
            }

            return false;
        }


        
        private string LookupOrderAttributes(SchemaObjectType type, string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            if (type != SchemaObjectType.ObjectTypeMember)
                return null;

            // if member is from last level
            Member mem = (Member)_srcCube.GetSchemaObject(type, uniqueName);
            if (mem == null || mem.LevelDepth < 2)
                return null;
            
            // get siblings and find position
            MemberCollection siblings=mem.Parent.GetChildren();
            int index = 0;
            for (int i = 0; i < siblings.Count; i++)
                if (siblings[i].UniqueName == mem.UniqueName)
                {
                    index = i;
                    break;
                }

            // get parent from destination and find child by position
            string destParentUn=_map.ConvertSchemaObjectUN(SchemaObjectType.ObjectTypeMember, mem.Parent.UniqueName);
            Member destParent = (Member)_destCube.GetSchemaObject(SchemaObjectType.ObjectTypeMember, destParentUn);
            Member ret = destParent.GetChildren()[index];

            return ret.UniqueName;
        }


        private string LookupMSAAttributes(SchemaObjectType type, string uniqueName)
        {
            if (_destCube == null)
                throw new Exception("Destination cube not accessible");

            if (type != SchemaObjectType.ObjectTypeMember)
                return null;

            // if member is from last level
            Member mem = (Member)_srcCube.GetSchemaObject(type, uniqueName);
            if (mem == null || mem.LevelDepth < 2)
                return null;

            // get siblings and find position
            MemberCollection siblings = mem.Parent.GetChildren();
            int index = 0;
            for (int i = 0; i < siblings.Count; i++)
                if (siblings[i].UniqueName == mem.UniqueName)
                {
                    index = i;
                    break;
                }

            // get parent from destination and find child by position
            string destParentUn = _map.ConvertSchemaObjectUN(SchemaObjectType.ObjectTypeMember, mem.Parent.UniqueName);
            Member destParent = (Member)_destCube.GetSchemaObject(SchemaObjectType.ObjectTypeMember, destParentUn);
            Member ret = destParent.GetChildren()[index];

            return ret.UniqueName;
        }


    }
}
