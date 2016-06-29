using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Data.Common;
using System.Web;
using System.Web.Security;
using System.Web.Management;
using System.Runtime.CompilerServices;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;

using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;


namespace ff.dashboardHelpersLib
{
    public class XmlBroker
    {
        public static XmlDocument DoBrokerage(XmlDocument aRequest)
        {
            XmlDocument aRet = null;
            bool aSchemaNeeded = false;
            DataSet aDataSet = new DataSet("ds");
            byte[] aBytes = null;
            try
            {
                // parse request here
                if (aRequest.DocumentElement != null && aRequest.DocumentElement.HasChildNodes)
                {
/*
                    bool aTransactionNeeded = aRequest.DocumentElement.HasAttribute("transaction") ? (aRequest.DocumentElement.Attributes["transaction"].Value == "1") : false;
                    string aDialect = aRequest.DocumentElement.HasAttribute("dialect") ? aRequest.DocumentElement.Attributes["dialect"].Value.ToLower() : "";
                    string aCommandType = aRequest.DocumentElement.HasAttribute("type") ? aRequest.DocumentElement.Attributes["type"].Value.ToLower() : "text";
                    string aExecute = aRequest.DocumentElement.HasAttribute("execute") ? aRequest.DocumentElement.Attributes["execute"].Value.ToLower() : "query";
                    aSchemaNeeded = aRequest.DocumentElement.HasAttribute("schema") ? (aRequest.DocumentElement.Attributes["schema"].Value == "1") : false;

                    DbCommand aCommand = iDbProvider.EnsureConnection().CreateCommand();
                    aCommand.CommandTimeout = 0;
                    aCommand.CommandText = "";

                    if (aDialect.ToLower() == "xsql")
                    {
                        for (int aN = 0; aRequest.DocumentElement.ChildNodes != null && aN < aRequest.DocumentElement.ChildNodes.Count; aN++)
                        {
                            if (aRequest.DocumentElement.ChildNodes[aN].NodeType == XmlNodeType.CDATA)
                            {
                                aCommand.CommandText += System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(aRequest.DocumentElement.ChildNodes[aN].Value));
                            }
                        }

                        if (aCommandType.ToLower() == "StoredProcedure".ToLower())
                            aCommand.CommandType = CommandType.StoredProcedure;
                        else
                            aCommand.CommandType = CommandType.Text;

                        aCommand.Prepare();

                        if (aCommand.CommandType == CommandType.StoredProcedure)
                            SqlCommandBuilder.DeriveParameters((SqlCommand)aCommand);
                        else
                        {
                            string aXPath = "p[@n]";
                            XmlNodeList aParams = aRequest.DocumentElement.SelectNodes(aXPath);
                            for (int aP = 0; aParams != null && aP < aParams.Count; aP++)
                            {
                                XmlElement aEltParam = (XmlElement)aParams[aP];

                                DbParameter aParam = iDbProvider.ProviderFactory().CreateParameter();
                                aParam.DbType = DbType.String;
                                aParam.ParameterName = "@" + aEltParam.Attributes["n"].Value;
                                aCommand.Parameters.Add(aParam);
                            }
                        }

                        for (int i = 0; i < aCommand.Parameters.Count; i++)
                        {
                            DbParameter aParam = aCommand.Parameters[i];
                            if (aParam.Direction != ParameterDirection.ReturnValue)
                            {
                                // null - as default
                                aParam.Value = System.DBNull.Value;

                                string aXPath = "p[@n='" + (aParam.ParameterName.StartsWith("@") ? aParam.ParameterName.Substring(1) : aParam.ParameterName) + "']";
                                XmlElement aEltParam = (XmlElement)aRequest.DocumentElement.SelectSingleNode(aXPath);
                                if (aEltParam != null)
                                {
                                    if (aEltParam.HasAttribute("r"))
                                    {
                                        // value from http file referenced by name
                                        if (aHttpFiles != null)
                                        {
                                            string aFileName = System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(aEltParam.Attributes["r"].Value));
                                            foreach (string aFileKey in aHttpFiles)
                                            {
                                                HttpPostedFile aPostedFile = aHttpFiles[aFileKey];
                                                if (aPostedFile != null && aPostedFile.FileName.ToLower() == aFileName.ToLower())
                                                {
                                                    aBytes = new byte[aPostedFile.InputStream.Length];
                                                    aPostedFile.InputStream.Position = 0;
                                                    aPostedFile.InputStream.Read(aBytes, 0, (int)aBytes.Length);
                                                    XmlElement aEltXslt = (XmlElement)aEltParam.SelectSingleNode("x");
                                                    if (aEltXslt != null)
                                                        aBytes = efmHelper.ProcessXSQL(iDbProvider, aBytes, aEltXslt);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        aBytes = efmHelper.GetCDATA(aEltParam);
                                        XmlElement aEltXslt = (XmlElement)aEltParam.SelectSingleNode("x");
                                        if (aEltXslt != null)
                                            aBytes = efmHelper.ProcessXSQL(iDbProvider, aBytes, aEltXslt);
                                    }
                                    if (aBytes == null)
                                        aParam.Value = System.DBNull.Value;
                                    else
                                    {
                                        switch (aParam.DbType)
                                        {
                                            case DbType.Binary:
                                                aParam.Value = aBytes;
                                                break;

                                            case DbType.String:
                                                aBytes = System.Text.UnicodeEncoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, aBytes);
                                                aParam.Value = System.Text.UnicodeEncoding.Unicode.GetString(aBytes);
                                                break;

                                            case DbType.AnsiString:
                                                aParam.Value = System.Text.Encoding.Default.GetString(aBytes);
                                                break;

                                            case DbType.Int16:
                                                aParam.Value = Int16.Parse(System.Text.Encoding.Default.GetString(aBytes));
                                                break;
                                            case DbType.Int32:
                                                aParam.Value = Int32.Parse(System.Text.Encoding.Default.GetString(aBytes));
                                                break;
                                            case DbType.Int64:
                                                aParam.Value = Int64.Parse(System.Text.Encoding.Default.GetString(aBytes));
                                                break;

                                            case DbType.Boolean:
                                                {
                                                    bool aBoolVal = false;
                                                    string aBoolStr = System.Text.Encoding.Default.GetString(aBytes);
                                                    try
                                                    {
                                                        aBoolVal = Boolean.Parse(aBoolStr);
                                                    }
                                                    catch
                                                    {
                                                        try
                                                        {
                                                            if (Int64.Parse(aBoolStr) != 0)
                                                                aBoolVal = true;
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    aParam.Value = aBoolVal;
                                                }
                                                break;

                                            default:
                                                aParam.Value = System.Text.Encoding.Default.GetString(aBytes);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    aTransactionOwned = aTransactionNeeded && (aTransaction == null);
                    if (aTransactionOwned)
                        aTransaction = iDbProvider.EnsureConnection().BeginTransaction();
                    aCommand.Transaction = aTransaction;

                    if (aExecute.ToLower() == "query")
                    {
                        SqlDataAdapter aSqlDataAdapter = new SqlDataAdapter();
                        aSqlDataAdapter.SelectCommand = (SqlCommand)aCommand;
                        aSqlDataAdapter.Fill(aDataSet);
                        XmlElement aEltXslt = (XmlElement)aRequest.DocumentElement.SelectSingleNode("x");
                        if (aEltXslt != null)
                        {
                            XmlDocument aXmlDS = HttpCommandEnvelope.DataSet2Xml(aDataSet);
                            aBytes = System.Text.Encoding.UTF8.GetBytes(aXmlDS.DocumentElement.OuterXml);
                            aBytes = efmHelper.ProcessXSQL(iDbProvider, aBytes, aEltXslt);
                            aDataSet = new DataSet("ds");
                            DataTable aDataTable = aDataSet.Tables.Add("XmlDS");
                            DataColumn aDataColumn = aDataTable.Columns.Add("XmlDS", typeof(String));
                            DataRow aDataRow = aDataTable.NewRow();
                            aDataRow[aDataColumn] = System.Text.Encoding.UTF8.GetString(aBytes);
                            aDataSet.AcceptChanges();
                        }
                    }
                    else if (aExecute.ToLower() == "NonQuery".ToLower())
                    {
                        DataTable aDataTable = aDataSet.Tables.Add("ExecuteNonQuery");
                        DataColumn aDataColumn = aDataTable.Columns.Add("Affected", typeof(int));
                        DataRow aDataRow = aDataTable.NewRow();
                        aDataRow[aDataColumn] = aCommand.ExecuteNonQuery();
                        aDataTable.Rows.Add(aDataRow);
                        aDataSet.AcceptChanges();
                    }
                    try
                    {
                        if (aTransactionOwned)
                        {
                            aTransaction.Commit();
                            aTransactionOwned = false;
                        }
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            if (aTransactionOwned)
                                aTransaction.Rollback();
                            aTransactionOwned = false;
                        }
                        catch
                        {
                        }
                        throw e;
                    }
 */
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            aRet = DataSet2Xml(aDataSet, aSchemaNeeded);
            return aRet;
        }

        static public XmlDocument DataSet2Xml(DataSet aDataSet, bool aSchemaNeeded)
        {
            XmlDocument aXmlDocumentDS = new XmlDocument();
            aXmlDocumentDS.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><ds/>");
            try
            {
                XmlAttribute aXmlAttribute = aXmlDocumentDS.CreateAttribute("t");
                aXmlAttribute.Value = aDataSet.Tables.Count.ToString();
                aXmlDocumentDS.DocumentElement.Attributes.Append(aXmlAttribute);
                for (int iT = 0; iT < aDataSet.Tables.Count; iT++)
                {
                    //table
                    XmlElement aXmlElementTable = aXmlDocumentDS.CreateElement("t" + (iT + 1).ToString());
                    aXmlAttribute = aXmlDocumentDS.CreateAttribute("n");
                    aXmlAttribute.Value = aDataSet.Tables[iT].TableName;
                    aXmlElementTable.Attributes.Append(aXmlAttribute);

                    aXmlAttribute = aXmlDocumentDS.CreateAttribute("c");
                    aXmlAttribute.Value = aDataSet.Tables[iT].Columns.Count.ToString();
                    aXmlElementTable.Attributes.Append(aXmlAttribute);

                    // schema
                    if (aSchemaNeeded)
                    {
                        XmlElement aXmlElementColumns = aXmlDocumentDS.CreateElement("s");

                        for (int iC = 0; iC < aDataSet.Tables[iT].Columns.Count; iC++)
                        {
                            XmlElement aXmlElementColumn = aXmlDocumentDS.CreateElement("c" + (iC + 1).ToString());
                            aXmlAttribute = aXmlDocumentDS.CreateAttribute("n");
                            aXmlAttribute.Value = aDataSet.Tables[iT].Columns[iC].ColumnName;
                            aXmlElementColumn.Attributes.Append(aXmlAttribute);

                            if (aDataSet.Tables[iT].Columns[iC].DataType != typeof(string))
                            {
                                aXmlAttribute = aXmlDocumentDS.CreateAttribute("t");
                                aXmlAttribute.Value = aDataSet.Tables[iT].Columns[iC].DataType.Name;
                                aXmlElementColumn.Attributes.Append(aXmlAttribute);
                            }

                            if (aDataSet.Tables[iT].Columns[iC].MaxLength > 0)
                            {
                                aXmlAttribute = aXmlDocumentDS.CreateAttribute("s");
                                aXmlAttribute.Value = aDataSet.Tables[iT].Columns[iC].MaxLength.ToString();
                                aXmlElementColumn.Attributes.Append(aXmlAttribute);
                            }
                            aXmlElementColumns.AppendChild(aXmlElementColumn);
                        }
                        aXmlElementTable.AppendChild(aXmlElementColumns);
                    }
                    //data
                    aXmlAttribute = aXmlDocumentDS.CreateAttribute("r");
                    aXmlAttribute.Value = aDataSet.Tables[iT].Rows.Count.ToString();
                    aXmlElementTable.Attributes.Append(aXmlAttribute);

                    XmlElement aXmlElementRows = aXmlDocumentDS.CreateElement("d");

                    for (int iR = 0; iR < aDataSet.Tables[iT].Rows.Count; iR++)
                    {
                        XmlElement aXmlElementRow = aXmlDocumentDS.CreateElement("r");// + (iR + 1).ToString());
                        aXmlAttribute = aXmlDocumentDS.CreateAttribute("n");
                        aXmlAttribute.Value = (iR + 1).ToString();
                        aXmlElementRow.Attributes.Append(aXmlAttribute);
                        for (int iC = 0; iC < aDataSet.Tables[iT].Columns.Count; iC++)
                        {
                            XmlElement aXmlElementColumn = aXmlDocumentDS.CreateElement("c" + (iC + 1).ToString());
                            if (aDataSet.Tables[iT].Rows[iR][iC] != System.DBNull.Value)
                            {
                                if (aDataSet.Tables[iT].Columns[iC].DataType == typeof(System.Byte[]))
                                {
                                    byte[] aColBytes = (byte[])aDataSet.Tables[iT].Rows[iR][iC];
                                    //HttpUtility.UrlEncode(System.Convert.ToBase64String(aColBytes, Base64FormattingOptions.None));
                                    aXmlElementColumn.InnerText = System.Convert.ToBase64String(aColBytes, Base64FormattingOptions.None);

                                }
                                else
                                    aXmlElementColumn.InnerText = aDataSet.Tables[iT].Rows[iR][iC].ToString();
                            }

                            if (string.IsNullOrEmpty(aXmlElementColumn.InnerText))
                            {
                                string d = aXmlElementColumn.InnerText;
                                //aXmlElementColumn.AppendChild(aXmlDocumentDS.CreateCDataSection(""));
                            }

                            aXmlElementRow.AppendChild(aXmlElementColumn);
                        }
                        aXmlElementRows.AppendChild(aXmlElementRow);
                    }
                    aXmlElementTable.AppendChild(aXmlElementRows);

                    aXmlDocumentDS.DocumentElement.AppendChild(aXmlElementTable);
                }

            }
            catch (Exception e)
            {
                string aDebug = e.Message;
                throw e;
            }
            return aXmlDocumentDS;
        }
    }
}
