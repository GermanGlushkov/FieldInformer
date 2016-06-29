using System;
using System.Collections;
using System.Xml;
using System.Text;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;


namespace FI.DataAccess.Serviced
{

	public class XmlCellsetWrapper2 : MarshalByRefObject 
	{
		private const char c8=(char)8;
		private const char c9=(char)9;
		private const char c10=(char)10;
		private const char c13=(char)13;

		public XmlCellsetWrapper2()
		{
		}


		public bool Ping()
		{
			return true;
		}

		public string BuildCellset(string Server , string Database, string Mdx )
		{
			AdomdConnection conn=null;
			AdomdCommand cmd=null;
			CellSet cst=null;

			try
			{
				// create connection
				conn=new AdomdConnection(this.GetConnectionString(Server, Database));				
				conn.Open();

				// open cellset
				cmd=conn.CreateCommand();
				cmd.CommandText=Mdx;
				cst=cmd.ExecuteCellSet();

				// build xml
				StringBuilder sb=new StringBuilder();

				int ax0PosCount=cst.Axes[0].Positions.Count;
				int ax0MemCount=cst.Axes[0].Set.Hierarchies.Count;
				int ax1PosCount=cst.Axes[1].Positions.Count;
				int ax1MemCount=cst.Axes[1].Set.Hierarchies.Count;

				// header
				sb.Append(ax0PosCount);
				sb.Append(c9);
				sb.Append(ax0MemCount);
				sb.Append(c13);
				sb.Append(ax1PosCount);
				sb.Append(c9);
				sb.Append(ax1MemCount);
				sb.Append(c13);

				// axis 0
				for(int i=0;i<ax0PosCount;i++)
				{
					for(int j=0;j<ax0MemCount;j++)
					{
						Member mem=cst.Axes[0].Positions[i].Members[j];

						sb.Append(mem.UniqueName);
						sb.Append(c8);
						sb.Append(mem.Caption);
						sb.Append(c8);
						sb.Append(mem.ChildCount);
						sb.Append(c8);
						sb.Append(mem.LevelDepth);						
						
						if(j<ax0MemCount-1)
							sb.Append(c9);
					}				

					if(i<ax0PosCount-1)
						sb.Append(c10);
				}
				sb.Append(c13);

				// axis 1
				for(int i=0;i<ax1PosCount;i++)
				{
					for(int j=0;j<ax1MemCount;j++)
					{
						Member mem=cst.Axes[1].Positions[i].Members[j];

						sb.Append(mem.UniqueName);
						sb.Append(c8);
						sb.Append(mem.Caption);
						sb.Append(c8);
						sb.Append(mem.ChildCount);
						sb.Append(c8);
						sb.Append(mem.LevelDepth);						
						
						if(j<ax1MemCount-1)
							sb.Append(c9);
					}				

					if(i<ax1PosCount-1)
						sb.Append(c10);
				}
				sb.Append(c13);

				// cells
				for(int i=0; i<ax0PosCount; i++)
				{
					for(int j=0; j<ax1PosCount; j++)
					{
						Cell cell=cst.Cells[i,j];

						sb.Append(this.GetCellValue(cell));
						sb.Append(c8);
						sb.Append(this.GetCellFormattedValue(cell));						

						if(j<ax1PosCount-1)
							sb.Append(c9);
					}

					if(i<ax0PosCount-1)
						sb.Append(c10);
				}


				return sb.ToString();
			}
			catch(Exception exc)
			{
				throw new Exception(exc.Message);
			}
			finally
			{
				// dispose resources
				if(conn!=null)				
					conn.Close(false);

				if(cmd!=null)
					cmd.Dispose();				
			}
		}

		private string GetCellValue(Cell cell)
		{
			try
			{
				return (cell.Value==null ? string.Empty : cell.Value.ToString());
			}
			catch(Exception exc)
			{
				return "#Err";
			}
		}

		private string GetCellFormattedValue(Cell cell)
		{
			try
			{
				return (cell.FormattedValue==null ? string.Empty : cell.FormattedValue);
			}
			catch(Exception exc)
			{
				return "#Err";
			}
		}

		public string GetSchemaMembers(string Server , string Database, string Cube, string[] UniqueNames)
		{
			return null;
		}


		public string GetMemberChildren(string Server , string Database, string Cube, string UniqueName , bool IfLeafAddItself)
		{
			return null;
		}


		public string GetMemberParentWithSiblings(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			return null;
		}


		public string GetMemberGrandParent(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			return null;
		}



		public string GetMemberParent(string Server , string Database, string Cube, string HierUniqueName, string MemUniqueName)
		{
			return null;
		}


		public string GetLevelMembers(string Server , string Database, string Cube, string UniqueName)
		{
			return null;
		}


		public string GetReportSchemaXml(string Server , string Database, string Cube, ref string OpenNodesXml)
		{
			return null;
		}


		public string GetReportXml(string Server , string Database, string Cube, ref string InReportXml)
		{
			return null;
		}







		private string GetConnectionString(string Server , string Database)
		{
			return "Data Source=" + Server + ";Initial Catalog=" + Database + ";Provider=MSOLAP;ConnectTo=8.0;Locale Identifier=1033;";
		}





	}
}
