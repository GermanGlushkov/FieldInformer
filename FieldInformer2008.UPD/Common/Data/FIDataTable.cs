using System;
using System.Data;
using System.Runtime.Serialization;

namespace FI.Common.Data
{
	[Serializable]
	public class FIDataTable:System.Data.DataTable, ISerializable
	{
		public int TotalCount=0;

		public FIDataTable():base()
		{
		}


		// deserialization constructor
		public FIDataTable(SerializationInfo info, StreamingContext context)//:base(info , context)
		{
			Serialization.RawDeSerialize(info.GetString("State") , this , true);
			this.TotalCount=info.GetInt32("TotalCount");
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("State" , Serialization.RawSerialize(this , true), typeof(string));
			info.AddValue("TotalCount" , this.TotalCount);
		}






		public System.Data.DataRow FindRow(string ColumnName , string SearchValue)
		{
			System.Data.DataRow[] rows= FindRows(ColumnName, SearchValue);
			if(rows!=null)
				return rows[0];
			else
				return null;
		}


		public System.Data.DataRow[] FindRows(string ColumnName , string SearchValue)
		{
			if(this.PrimaryKey.Rank>1)
				throw new Exception("Multi-column Primary key not supported");

			System.Data.DataRow[] rows=this.Select(ColumnName + "='" + SearchValue.ToString() + "'");

			if(rows==null || rows.Length==0)
				return null;

			return rows;
		}




		public static string EscapeSearchColumn(string Expression)
		{

			Expression=Expression.Replace("]" , "[]]");
			Expression=Expression.Replace("[" , "[[]");

			Expression=Expression.Replace("'" , "''");
			for(int i=0 ; i<Expression.Length ; i++)
			{
				switch (Expression.Substring(i , 1))
				{
					case "~" :
					case "(" :
					case ")" :
					case "#" :
					case "\\" :
					case "/" :
					case "=" :
					case "<" :
					case ">" :
					case "+" :
					case "-" :
					case "*" :
					case "%" :
					case "&" :
					case "|" :
					case "^" :
					case "'" :
					case "\"" :
					case "[" :
					case "]" :
						Expression=Expression.Insert(i , "[");
						i+=2;
						Expression=Expression.Insert(i , "]");
						break;
				}

			}

			return Expression;
		}

		public static string EscapeSearchExpression(string Expression)
		{
			Expression=Expression.Replace("'" , "''");
			for(int i=0 ; i<Expression.Length ; i++)
			{
				switch (Expression.Substring(i , 1))
				{
					case "[" :
					case "]" :
					case "*" :
					//case "%" :
						Expression=Expression.Insert(i , "[");
						i+=2;
						Expression=Expression.Insert(i , "]");
						break;
				}

			}
			return Expression;
		}



		public static bool IsNumeric(System.Data.DataColumn Column)
		{
			if (Column.DataType==typeof(System.Boolean))
				return false;
			else if (Column.DataType==typeof(System.Char))
				return false;
			else if (Column.DataType==typeof(System.DateTime))
				return false;
			else if (Column.DataType==typeof(System.String))
				return false;
			else if (Column.DataType==typeof(System.TimeSpan))
                return false;
            else if (Column.DataType == typeof(System.Guid))
                return false;
            else if (Column.DataType == typeof(byte[]))
                return false;
			else
				return true;
		}

	}
}
