using System;
using System.Data;

namespace FI.Common.Data
{
	/// <summary>
	/// Summary description for Serialization.
	/// </summary>
	public class Serialization
	{
		public Serialization()
		{
		}

		public static readonly char ColumnSeparator=(char)1;
		public static readonly char RowSeparator=(char)2;
		public static readonly char TableSeparator=(char)3;

		public static string RawSerialize(System.Data.DataTable dataTable , bool SerializeSchema)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();

			if(SerializeSchema)
			{
				// first row has column names
				for(int i=0;i<dataTable.Columns.Count; i++)
				{
					if(i!=0)
						sb.Append(ColumnSeparator);
					sb.Append(dataTable.Columns[i].ColumnName);
				}

				sb.Append(RowSeparator);

				// second row has column datatypes
				for(int i=0;i<dataTable.Columns.Count; i++)
				{
					if(i!=0)
						sb.Append(ColumnSeparator);
					sb.Append(dataTable.Columns[i].DataType.ToString());
				}
			}


			// row data
			for(int i=0;i<dataTable.Rows.Count; i++)
			{
				if(i!=0 || SerializeSchema)
					sb.Append(RowSeparator);

				System.Data.DataRow row=dataTable.Rows[i];

				for(int j=0; j<dataTable.Columns.Count ; j++)
				{
					if(j!=0)
						sb.Append(ColumnSeparator);

                    if (dataTable.Columns[j].DataType == typeof(byte[]))
                        continue; // byte array not supported

					sb.Append(Convert.ToString(row[j], System.Globalization.CultureInfo.InvariantCulture));
				}
			}

			return sb.ToString();
		}



		public static void RawDeSerialize(string tableState, System.Data.DataTable dataTable , bool DeSerializeSchema)
		{
			string[] rowStates=tableState.Split(RowSeparator);

			if(DeSerializeSchema && (rowStates==null || rowStates.Length<2))
				throw new Exception("tableState is invalid , at least 2 rows expected");

			

			for(int i=0;i<rowStates.Length; i++)
			{
				if(DeSerializeSchema)
				{
					if(i==0)
					{
						// first row has column names
						string[] colNames=rowStates[0].Split(ColumnSeparator);
						for(int j=0;j<colNames.Length;j++)
						{
							dataTable.Columns.Add(colNames[j]);
						}
						continue;
					}

					if(i==1)
					{
						// second row has column datatypes
						string[] colDataTypes=rowStates[1].Split(ColumnSeparator);
						for(int j=0;j<colDataTypes.Length;j++)
						{
							dataTable.Columns[j].DataType=System.Type.GetType(colDataTypes[j]);
						}
						continue;
					}
				}

				string[] values=rowStates[i].Split(ColumnSeparator);
				System.Data.DataRow row=dataTable.NewRow();
				for(int j=0;j<values.Length;j++)
				{
					if(dataTable.Columns[j].DataType!=typeof(string) && values[j]=="")
						row[j]=DBNull.Value;
					else
						try
                        {
                            if (dataTable.Columns[j].DataType == typeof(byte[]))
                                row[j] = null; // byte array not supported
                            else if (dataTable.Columns[j].DataType == typeof(Guid))
                                row[j] = new Guid(values[j] as string);
                            else
							    row[j]=Convert.ChangeType(values[j] , dataTable.Columns[j].DataType , System.Globalization.CultureInfo.InvariantCulture );
						}
						catch(Exception exc)
						{
							throw exc;
						}
				}
				dataTable.Rows.Add(row);

			}
		}



		public static string RawSerialize(System.Data.DataSet dataSet , bool SerializeSchema)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();

			for(int i=0; i<dataSet.Tables.Count;i++)
			{
				//table name
				if(SerializeSchema)
				{
					if(i!=0)
						sb.Append(TableSeparator);

					sb.Append(dataSet.Tables[i].TableName);
				}

				//table separator
				if(i!=0 || SerializeSchema)
					sb.Append(TableSeparator);

				//table state
				sb.Append(RawSerialize(dataSet.Tables[i] , SerializeSchema));
			}

			return sb.ToString();
		}

		public static void RawDeSerialize(string setState, System.Data.DataSet dataSet , bool DeSerializeSchema)
		{
			string[] tableStates=setState.Split(TableSeparator);

			if(DeSerializeSchema && (tableStates==null || tableStates.Length<1))
				throw new Exception("setState is invalid , at least 1 row expected");

			// if schema , first is table name, then table state
			bool tableName=false;
			if(DeSerializeSchema)
				tableName=true;

			for(int i=0;i<tableStates.Length; i++)
			{
				if(tableName)
				{
					dataSet.Tables.Add(tableStates[i]);
				}
				else
				{
					if(DeSerializeSchema)
					{
						// to last added table
						RawDeSerialize(tableStates[i] , dataSet.Tables[dataSet.Tables.Count-1] , DeSerializeSchema);
					}
					else
					{
						// to current existing table
						RawDeSerialize(tableStates[i] , dataSet.Tables[i] , DeSerializeSchema);
					}
				}

				if(DeSerializeSchema)
					tableName=!tableName;
			}
		}



	}
}
