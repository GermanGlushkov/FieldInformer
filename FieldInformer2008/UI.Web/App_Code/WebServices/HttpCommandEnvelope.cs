using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Web;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;


namespace FI.UI.Web.WebServices
{
    public class HttpCommandEnvelope
    {
        static public XmlDocument ParseRequest(string aXmlVar, ref bool aZip, ref bool aCrypt, byte[] aEncryptionKey)
        {
            XmlDocument aRet = null;
            aEncryptionKey = (aEncryptionKey != null && aEncryptionKey.Length == 16) ? aEncryptionKey : System.Text.Encoding.ASCII.GetBytes("1234567890123456");
            try
            {
                if (string.IsNullOrEmpty(aXmlVar))
                    return aRet;

                System.Xml.XmlDocument aXmlRequest = new System.Xml.XmlDocument();
                aXmlRequest.LoadXml(aXmlVar);
                byte[] aBytes = null;
                int aSizeByAtt = int.Parse(aXmlRequest.DocumentElement.Attributes["size"].Value);
                aZip = ( aXmlRequest.DocumentElement.HasAttribute("crypt") && aXmlRequest.DocumentElement.Attributes["crypt"].Value != "0" );
                aCrypt = ( aXmlRequest.DocumentElement.HasAttribute("zip") && aXmlRequest.DocumentElement.Attributes["zip"].Value != "0" );

                if ( aZip || aCrypt )
                {
                    string aHex = aXmlRequest.DocumentElement.InnerText;
                    aBytes = new byte[aHex.Length / 2];
                    for (int i = 0; i < aHex.Length; i += 2)
                    {
                        string a2Hex = aHex.Substring(i, 2);
                        aBytes[i / 2] = byte.Parse(a2Hex, System.Globalization.NumberStyles.AllowHexSpecifier);

                    }

                    if (aCrypt)
                    {
                        if (aEncryptionKey == null)
                            aEncryptionKey = System.Text.Encoding.ASCII.GetBytes("1234567890123456");
                        Rijndael iCryptoService = new RijndaelManaged();
                        iCryptoService.KeySize = 128;
                        iCryptoService.BlockSize = 256;
                        iCryptoService.Mode = CipherMode.ECB;//.CBC;//.ECB;
                        iCryptoService.Padding = PaddingMode.None;

                        MemoryStream aMemoryStream = new MemoryStream();
                        iCryptoService.Padding = PaddingMode.None;
                        iCryptoService.GenerateIV();
                        byte[] aIv = aIv = iCryptoService.IV;
                        CryptoStream aCryptoStream = new CryptoStream(aMemoryStream, iCryptoService.CreateDecryptor(aEncryptionKey, aIv), CryptoStreamMode.Write);
                        aCryptoStream.Write(aBytes, 0, (int)aBytes.Length);
                        aCryptoStream.FlushFinalBlock();
                        aBytes = aMemoryStream.ToArray();
                    }

                    if (aZip)
                    {
                        // 2 first bytes are length (high first)
                        int aZipLen;
                        int aOff = 0;
                        MemoryStream aOutput = new MemoryStream();
                        while (aOutput.Position < aSizeByAtt && aOff < aBytes.Length)
                        {
                            aZipLen = BitConverter.ToInt16( aBytes, aOff );
                            if (aZipLen == 0)
                                break;
                            aOff += 2;
                            MemoryStream aInput = new MemoryStream();
                            //write the incoming bytes to the MemoryStream
                            aInput.Write(aBytes, aOff, aZipLen);
                            //set our position to the start of the Stream
                            aInput.Position = 0;
                            System.IO.Compression.DeflateStream aDeflateStream = new DeflateStream(aInput, CompressionMode.Decompress, true);
                            int aBufLen = 32000;
                            byte[] aBuf = new byte[aBufLen];
                            Int16 aUtfLen = 0; 
                            while (true)
                            {
                                int aNumRead = aDeflateStream.Read(aBuf, 0, aBufLen);
                                if (aNumRead <= 0)
                                {
                                    break;
                                }
                                if (aUtfLen == 0)
                                {
                                    aUtfLen = BitConverter.ToInt16(aBuf, 0);
                                    if ( aNumRead > 2 )
                                        aOutput.Write(aBuf, 2, aNumRead - 2);
                                }
                                else
                                    aOutput.Write(aBuf, 0, aNumRead);
                            }
                            aDeflateStream.Close();
                            aOff += aZipLen;
                        }
                        aBytes = aOutput.ToArray();
                        aOutput.Close();
                    }
                    string aUtfXml = System.Text.Encoding.UTF8.GetString(aBytes, 0, aSizeByAtt);
                    aRet = new XmlDocument();
                    aRet.LoadXml(aUtfXml);
                }
                else
                {
                    for (int aC = 0; aXmlRequest.DocumentElement.HasChildNodes && aC < aXmlRequest.DocumentElement.ChildNodes.Count; aC++)
                    {
                        if (aXmlRequest.DocumentElement.ChildNodes[aC].NodeType == XmlNodeType.Element)
                        {
                            aRet = new XmlDocument();
                            aRet.LoadXml(aXmlRequest.DocumentElement.ChildNodes[aC].OuterXml);
                            break;
                        }
                    }
                }
            }
            catch
            {
                aRet = null;
            }
            return aRet;
        }

        static public XmlDocument SerializeResponse(DataSet aDataSet, bool aZip, bool aCrypt, byte[] aEncryptionKey)
        {
            XmlDocument aXmlResponse = new XmlDocument();
            aXmlResponse.LoadXml("<d/>");
            aEncryptionKey = (aEncryptionKey != null && aEncryptionKey.Length == 16) ? aEncryptionKey : System.Text.Encoding.ASCII.GetBytes("1234567890123456");
            try
            {
                aXmlResponse.DocumentElement.Attributes.Append(aXmlResponse.CreateAttribute("zip"));
                aXmlResponse.DocumentElement.Attributes["zip"].Value = aZip ? "1" : "0";
                aXmlResponse.DocumentElement.Attributes.Append(aXmlResponse.CreateAttribute("crypt"));
                aXmlResponse.DocumentElement.Attributes["crypt"].Value = aCrypt ? "1" : "0";
                XmlCDataSection aCData = aXmlResponse.CreateCDataSection("");
                aXmlResponse.DocumentElement.AppendChild(aCData);

                XmlDocument aXmlDocumentDS = new XmlDocument();
                aXmlDocumentDS.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><ds/>");
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

                    //data
                    aXmlAttribute = aXmlDocumentDS.CreateAttribute("r");
                    aXmlAttribute.Value = aDataSet.Tables[iT].Rows.Count.ToString();
                    aXmlElementTable.Attributes.Append(aXmlAttribute);

                    XmlElement aXmlElementRows = aXmlDocumentDS.CreateElement("d");

                    for (int iR = 0; iR < aDataSet.Tables[iT].Rows.Count; iR++)
                    {
                        XmlElement aXmlElementRow = aXmlDocumentDS.CreateElement("r");// + (iR + 1).ToString());
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

                byte [] aBytes = System.Text.Encoding.UTF8.GetBytes(aXmlDocumentDS.DocumentElement.OuterXml);

                aXmlResponse.DocumentElement.Attributes.Append(aXmlResponse.CreateAttribute("size"));
                aXmlResponse.DocumentElement.Attributes["size"].Value = aBytes.Length.ToString();

                if ( aZip )
                {
                    int aBufLen = 32000;
                    int aOff = 0;
                    MemoryStream aWholeZip = new MemoryStream();
                    while (aOff < aBytes.Length)
                    {
                        int aLen = (aBytes.Length - aOff) > aBufLen ? aBufLen : (aBytes.Length - aOff);

                        MemoryStream aOutput = new MemoryStream();
                        System.IO.Compression.DeflateStream aDeflateStream = new DeflateStream(aOutput, CompressionMode.Compress, true);
                        aDeflateStream.Write(aBytes, aOff, aLen);
                        aDeflateStream.Close();
                        byte[] aChunk = aOutput.ToArray();
                        byte[] aChunkSize = BitConverter.GetBytes((Int16)aChunk.Length);
                        aWholeZip.Write(aChunkSize, 0, aChunkSize.Length);
                        aWholeZip.Write(aChunk, 0, aChunk.Length);
                        aOff += aLen;
                    }
                    // zero length at the end
                    aWholeZip.Write(BitConverter.GetBytes((Int16)0), 0, 2);
                    aBytes = aWholeZip.ToArray();
                }

                if ( aCrypt )
                {
                    Rijndael iCryptoService = new RijndaelManaged();
                    iCryptoService.KeySize = 128;
                    iCryptoService.BlockSize = 256;
                    iCryptoService.Mode = CipherMode.ECB;//.CBC;//.ECB;

                    MemoryStream aMemoryStream = new MemoryStream();
                    iCryptoService.GenerateIV();
                    byte[] aIv = iCryptoService.IV;
                    CryptoStream aCryptoStream = new CryptoStream(aMemoryStream, iCryptoService.CreateEncryptor(aEncryptionKey, aIv), CryptoStreamMode.Write);
                    aCryptoStream.Write(aBytes, 0, aBytes.Length);
                    aCryptoStream.FlushFinalBlock();
                    aBytes = new byte[aMemoryStream.Length];
                    aMemoryStream.Position = 0;
                    aMemoryStream.Read(aBytes, 0, aBytes.Length);
                }

                // base64 encoding 3:4
                aCData.Data = System.Convert.ToBase64String(aBytes);
            }
            catch (Exception e)
            {
                string aDebug = e.Message;
                throw e;
            }
            return aXmlResponse;
        }

/*
        protected virtual void WriteResponse(HttpResponse aResponse, DataSet aDataSet, bool aZip, bool aCrypt)
        {

            //aXmlDocumentDS.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><list><entry><Name>N1</Name><Type>T1</Type><License>GPL</License><Description>D18</Description></entry><entry><Name>N2</Name><Type>T1</Type><License>Free</License><Description>D2</Description></entry><entry><Name>N1</Name><Type>T1</Type><License>GPL</License><Description>D18</Description></entry><entry><Name>N2</Name><Type>T1</Type><License>Free</License><Description>D2</Description></entry></list>");
            aBytes = System.Text.Encoding.UTF8.GetBytes(aXmlDocumentDS.OuterXml);
            Int32 aUtf8Length = (Int32)aBytes.Length;

            if ( aZip )
                aBytes = ff.efm.efmCoreLib.IO.CompressByDeflate(aBytes);
            if ( aCrypt )
                aBytes = ff.efm.efmCoreLib.IO.EncryptByRijndael(aUtf8Length, aBytes.Length, aBytes, aEncryptionKey);

            // base64 encoding 3:4
            aCData.Data = System.Convert.ToBase64String(aBytes);
        }
        catch(Exception e)
        {
        }
*/
    }
}
