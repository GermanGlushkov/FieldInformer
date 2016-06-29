using System;

namespace FI.Common
{
	/// <summary>
	/// Summary description for XML.
	/// </summary>
	public class XML
	{
		public XML()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string XmlEncode(string InputString)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder(InputString);

			sb.Replace("&", "&amp;"); // MUST be done FIRST! Duh!
			sb.Replace(@"""", @"&quot;");
			sb.Replace("'", "&apos;");
			sb.Replace("<", "&lt;");
			sb.Replace(">", "&gt;");

			return sb.ToString();
		}


	}
}
