using System;
using System.Data;
using System.Data.SqlClient;

namespace Setup.PlugIn.DBConverter
{
	public class Converter
	{
		public Converter()
		{
		}

		public void Convert(int CompanyId, string OlapServerIp, string SqlServerIP , string SaPassword)
		{
			try
			{
				// IN ORDER TO CONVERT SRW REPORTS:
				// 1) Replace DBFINF with DBFINF_SRWgold
				// 2) Replace DBWEB with DBWEB_SRWgold
				// 3) Replace +0 with +10000

				this.CleanupDestinationDb(CompanyId, SqlServerIP, SaPassword);
				this.ConvertWithSql(CompanyId, OlapServerIp, SqlServerIP, SaPassword);
				this.ConvertStorecheckReports(CompanyId, SqlServerIP, SaPassword);
				this.ConvertOlap(CompanyId, SqlServerIP, SaPassword);
			}
			catch(Exception exc)
			{
				throw exc;
			}
		}

		public static void InitializeRemoting()
		{
			System.Runtime.Remoting.RemotingConfiguration.Configure(System.Reflection.Assembly.GetExecutingAssembly().Location +  ".config");
		}

		private void ConvertOlap(int CompanyId, string SqlServerIP, string SaPassword)
		{

			SqlConnection con=new SqlConnection("server=" + SqlServerIP + "; User ID=sa; Password=" + SaPassword + "; Database=DBFINF;");
			con.Open();


			// olap reports  -----------------
			DataTable table=new DataTable();
			SqlDataAdapter adapter=new SqlDataAdapter();

			SqlCommand cmd=con.CreateCommand();
			cmd.CommandText=@"
			SELECT rpt_id+0 as rpt_id, case rpt_parent_report_id when 0 then 0 else rpt_parent_report_id+0 end as rpt_parent_report_id, case when rpt_sharing_status>2 then rpt_sharing_status else 0 end as rpt_sharing_status, user_id+0 as user_id, rpt_name, rpt_description, rpt_open, rpt_selected, rpt_current_rows, rpt_current_columns, rpt_current_filters, rpt_order, rpt_order_tuple, rpt_percentage_type, rpt_percentage_dim, rpt_percentage_measure, rpt_sum_dims, rpt_avg_dims, rpt_min_dims, rpt_max_dims, rpt_graph_type, rpt_graph_category, rpt_graph_series, rpt_graph_value, rpt_graph_set_scaling, rpt_empty_rows, rpt_empty_cols, rpt_nonempty_rows, rpt_nonempty_cols, rpt_nodes_open, rpt_time_range_enabled, rpt_time_range_prompt, rpt_time_range_start, rpt_time_range_end, rpt_time_range_level
					FROM DBWEB.dbo.treports 
					WHERE user_id IN (SELECT user_id FROM DBWEB.dbo.tusers WHERE company_id=" + CompanyId.ToString() + @")
			";

			adapter.SelectCommand=cmd;
			adapter.Fill(table);
			adapter.Dispose();

			foreach(DataRow row in table.Rows)
			{
				FI.BusinessObjects.OlapReport.GraphOptionsEnum graphOptions=FI.BusinessObjects.OlapReport.GraphOptionsEnum.None;
				string reportXml=@"<R></R>";
				string openNodes="<NODES></NODES>";


				// create graphOptions
				//rpt_graph_category, rpt_graph_series, rpt_graph_value, rpt_graph_set_scaling
				if( ((bool)row["rpt_graph_category"])==true)
					graphOptions=graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowCategories;
				if( ((bool)row["rpt_graph_series"])==true)
					graphOptions=graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowSeries;
				if( ((bool)row["rpt_graph_value"])==true)
					graphOptions=graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowValues;
				if( ((bool)row["rpt_graph_set_scaling"])==true)
					graphOptions=graphOptions | FI.BusinessObjects.OlapReport.GraphOptionsEnum.SetScaling;


				// create openNodes
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				if(row["rpt_nodes_open"]!=System.DBNull.Value && row["rpt_nodes_open"].ToString().Trim()!="")
				{
					string[] nodes=System.Text.RegularExpressions.Regex.Split(row["rpt_nodes_open"].ToString().Trim() , System.Text.RegularExpressions.Regex.Escape("$~$") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
					if(nodes!=null && nodes.Length>0)
					{
						sb.Append("<NODES>");
						for(int i=0;i<nodes.Length;i++)
						{
							string node=nodes[i].Trim();

							for(int j=0;j<i;j++)
								if(nodes[j].Trim()==node) //if already been added
								{
									node="";
									break;
								}

							if(node!="")
								sb.Append(@"<ON UN=""" + FI.Common.XML.XmlEncode(node) + @"""/>");

						}
						sb.Append("</NODES>");
						openNodes=sb.ToString();
					}
				}


				cmd=con.CreateCommand();
				cmd.CommandText=@"
				SET IDENTITY_INSERT DBFINF.dbo.t_olap_reports  ON

				INSERT INTO DBFINF.dbo.t_olap_reports(
					id, 
					parent_report_id, 
					sharing_status, 
					user_id, 
					name, 
					description, 
					is_selected, 
					graph_type, 
					graph_options, 
					data, 
					open_nodes, 
					timestamp
					)
					VALUES(
					" + row["rpt_id"].ToString() + @" , 
					" + row["rpt_parent_report_id"].ToString() + @" , 
					" + row["rpt_sharing_status"].ToString() + @" , 
					" + row["user_id"].ToString() + @" , 
					'" + row["rpt_name"].ToString().Replace("'" , "''") + @"' , 
					'" + row["rpt_description"].ToString().Replace("'" , "''") + @"' , 
					" + (row["rpt_open"].ToString()=="True"?1:0).ToString() + @" , 
					" + row["rpt_graph_type"].ToString() + @" ,
					" + ((int)graphOptions).ToString()  + @" , 
					'" + reportXml.Replace("'" , "''")  + @"' , 
					'" + openNodes.Replace("'" , "''")  + @"' , 
					GetDate()
					)

				SET IDENTITY_INSERT DBFINF.dbo.t_olap_reports  OFF

							";
				cmd.ExecuteNonQuery();


				if(row["rpt_parent_report_id"].ToString()!="0")
					continue; // will handle shared reports from parent report in the end of riutine





				// load report ---------
				FI.BusinessObjects.OlapReport report=null;

				FI.BusinessObjects.User user=new FI.BusinessObjects.User((decimal)row["user_id"] , false);
				try
				{
					report=(FI.BusinessObjects.OlapReport)user.ReportSystem.GetReport( (decimal)row["rpt_id"] , typeof(FI.BusinessObjects.OlapReport) , true);
					//report=(FI.BusinessObjects.OlapReport)user.ReportSystem.NewReport( typeof(FI.BusinessObjects.OlapReport));
				}
				catch(Exception exc)
				{
					if(exc.Message.StartsWith("Database ") && exc.Message.EndsWith(" does not exist."))
					{
						FI.Common.LogWriter.Instance.WriteException(exc);
						continue; //next report
					}
					else
						throw exc;
				}
				




				// row  hierarchies and members
				string[] strHiers=System.Text.RegularExpressions.Regex.Split( (string)row["rpt_current_rows"] , System.Text.RegularExpressions.Regex.Escape("##@@##") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				for(int i=0;i<strHiers.Length;i++)
				{
					string strHier=strHiers[i].Trim();
					string[] strMems=System.Text.RegularExpressions.Regex.Split( strHier , System.Text.RegularExpressions.Regex.Escape("$~$") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
					FI.BusinessObjects.Olap.Hierarchy curHier=null;
					System.Collections.ArrayList listMems=new System.Collections.ArrayList();

					for(int j=0;j<strMems.Length;j++)
					{
						string strMem=strMems[j].Trim();
						if(strMem=="")
							continue;

						if(curHier==null)
							foreach(FI.BusinessObjects.Olap.Hierarchy hier in report.Schema.Hierarchies)
								if(strMem.StartsWith(hier.UniqueName))
								{
									curHier=hier;
									hier.Axis=report.Axes[1];
								}

						if(curHier==null)
							continue; // cannot recognize hierarchy , continue trying

						listMems.Add(strMem);
					}

					// add found to hier
					if(listMems.Count==0)
						continue;

					System.Xml.XmlElement memsEl=report.Schema.GetSchemaMembers((string[])listMems.ToArray(typeof(string)));
					foreach(System.Xml.XmlElement el in memsEl.ChildNodes)
						curHier.AddMember(new FI.BusinessObjects.Olap.SchemaMember(curHier , el), true);
				}


				// col hierarchies and members
				strHiers=System.Text.RegularExpressions.Regex.Split( (string)row["rpt_current_columns"] , System.Text.RegularExpressions.Regex.Escape("##@@##") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				for(int i=0;i<strHiers.Length;i++)
				{
					string strHier=strHiers[i].Trim();
					string[] strMems=System.Text.RegularExpressions.Regex.Split( strHier , System.Text.RegularExpressions.Regex.Escape("$~$") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
					FI.BusinessObjects.Olap.Hierarchy curHier=null;
					System.Collections.ArrayList listMems=new System.Collections.ArrayList();

					for(int j=0;j<strMems.Length;j++)
					{
						string strMem=strMems[j].Trim();
						if(strMem=="")
							continue;

						if(curHier==null)
							foreach(FI.BusinessObjects.Olap.Hierarchy hier in report.Schema.Hierarchies)
								if(strMem.StartsWith(hier.UniqueName))
								{
									curHier=hier;									
									hier.Axis=report.Axes[0];
								}

						if(curHier==null)
							continue; // cannot recognize hierarchy , continue trying

						listMems.Add(strMem);
					}

					// add found to hier
					if(listMems.Count==0)
						continue;

					System.Xml.XmlElement memsEl=report.Schema.GetSchemaMembers((string[])listMems.ToArray(typeof(string)));
					foreach(System.Xml.XmlElement el in memsEl.ChildNodes)
						curHier.AddMember(new FI.BusinessObjects.Olap.SchemaMember(curHier , el), true);
				}



				// filt hierarchies and members
				strHiers=System.Text.RegularExpressions.Regex.Split( (string)row["rpt_current_filters"] , System.Text.RegularExpressions.Regex.Escape("##@@##") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				for(int i=0;i<strHiers.Length;i++)
				{
					string strHier=strHiers[i].Trim();
					string[] strMems=System.Text.RegularExpressions.Regex.Split( strHier , System.Text.RegularExpressions.Regex.Escape("$~$") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
					FI.BusinessObjects.Olap.Hierarchy curHier=null;
					System.Collections.ArrayList listMems=new System.Collections.ArrayList();

					for(int j=0;j<strMems.Length;j++)
					{
						string strMem=strMems[j].Trim();
						if(strMem=="")
							continue;

						if(curHier==null)
							foreach(FI.BusinessObjects.Olap.Hierarchy hier in report.Schema.Hierarchies)
								if(strMem.StartsWith(hier.UniqueName))
								{
									curHier=hier;
									if(hier.Axis.Ordinal!=2)
										throw new Exception("Hierarchy already moved to rows or columns");
								}

						if(curHier==null)
							continue; // cannot recognize hierarchy , continue trying

						listMems.Add(strMem);
						break; // one per dimension
					}

					// add found to hier
					if(listMems.Count==0)
						continue;

					System.Xml.XmlElement memsEl=report.Schema.GetSchemaMembers((string[])listMems.ToArray(typeof(string)));
					foreach(System.Xml.XmlElement el in memsEl.ChildNodes)
					{
						curHier.DataMembers.Clear();
						curHier.CalculatedMembers.Clear();
						curHier.AddMember(new FI.BusinessObjects.Olap.SchemaMember(curHier , el), true);
						break;
					}
				}



				// time range members
				//rpt_time_range_enabled, rpt_time_range_prompt, rpt_time_range_start, rpt_time_range_end, rpt_time_range_level
				string timeRangeLevelUn=(string)row["rpt_time_range_level"];
				if((bool)row["rpt_time_range_enabled"]==true)
					foreach(FI.BusinessObjects.Olap.Level level in report.Schema.Levels)
					{
						if(timeRangeLevelUn==level.UniqueName)
							if(level.Hierarchy.Axis.Ordinal!=2)
							{
								level.Hierarchy.DataMembers.Clear();
								level.Hierarchy.CalculatedMembers.Clear();

								FI.BusinessObjects.Olap.CalculatedMemberTemplates.FilteredByNameSet timeRangeMem=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.FilteredByNameSet(
									null,
									level.Hierarchy , 
									level , 
									(string)row["rpt_time_range_start"] , 
									(string)row["rpt_time_range_end"]);
								timeRangeMem.Prompt=(bool)row["rpt_time_range_prompt"];
								level.Hierarchy.CalculatedMembers.Add(timeRangeMem, true);
							}
					}


				// add percentage measures
				// rpt_percentage_type, rpt_percentage_dim, rpt_percentage_measure
				if(row["rpt_percentage_type"].ToString()=="1") // ratio to vis sum
				{
					FI.BusinessObjects.Olap.Hierarchy ratioHier=report.Schema.Hierarchies[(string)row["rpt_percentage_dim"]];
					FI.BusinessObjects.Olap.Hierarchy measuresHier=report.Schema.Hierarchies["[Measures]"];
					if(ratioHier!=null && measuresHier!=null)
					{
						System.Collections.ArrayList listNewMems=new System.Collections.ArrayList();
						foreach(FI.BusinessObjects.Olap.DataMember measure in measuresHier.DataMembers)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToVisAggr newMeasure=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToVisAggr(
								null,
								measuresHier , 
								measure , 
								ratioHier, 
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate.AggregateFunction.SUM);
							listNewMems.Add(newMeasure);
						}

						measuresHier.DataMembers.Clear();
						measuresHier.CalculatedMembers.Clear();

						foreach(FI.BusinessObjects.Olap.DataMember newMeasure in listNewMems)
							measuresHier.AddMember(newMeasure, true);
					}
				}
				else if(row["rpt_percentage_type"].ToString()=="2") // ratio to vis avg
				{
					FI.BusinessObjects.Olap.Hierarchy ratioHier=report.Schema.Hierarchies[(string)row["rpt_percentage_dim"]];
					FI.BusinessObjects.Olap.Hierarchy measuresHier=report.Schema.Hierarchies["[Measures]"];
					if(ratioHier!=null && measuresHier!=null)
					{
						System.Collections.ArrayList listNewMems=new System.Collections.ArrayList();
						foreach(FI.BusinessObjects.Olap.DataMember measure in measuresHier.DataMembers)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToVisAggr newMeasure=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToVisAggr(
								null,
								measuresHier , 
								measure , 
								ratioHier, 
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate.AggregateFunction.AVG);
							listNewMems.Add(newMeasure);
						}

						measuresHier.DataMembers.Clear();
						measuresHier.CalculatedMembers.Clear();

						foreach(FI.BusinessObjects.Olap.DataMember newMeasure in listNewMems)
							measuresHier.AddMember(newMeasure, true);
					}
				}
				else if(row["rpt_percentage_type"].ToString()=="3") // ratio to vis min
				{
					FI.BusinessObjects.Olap.Hierarchy ratioHier=report.Schema.Hierarchies[(string)row["rpt_percentage_dim"]];
					FI.BusinessObjects.Olap.Hierarchy measuresHier=report.Schema.Hierarchies["[Measures]"];
					if(ratioHier!=null && measuresHier!=null)
					{
						System.Collections.ArrayList listNewMems=new System.Collections.ArrayList();
						foreach(FI.BusinessObjects.Olap.DataMember measure in measuresHier.DataMembers)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToVisAggr newMeasure=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToVisAggr(
								null,
								measuresHier , 
								measure , 
								ratioHier, 
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate.AggregateFunction.MIN);
							listNewMems.Add(newMeasure);
						}

						measuresHier.DataMembers.Clear();
						measuresHier.CalculatedMembers.Clear();

						foreach(FI.BusinessObjects.Olap.DataMember newMeasure in listNewMems)
							measuresHier.AddMember(newMeasure, true);
					}
				}
				else if(row["rpt_percentage_type"].ToString()=="4") // ratio to vis max
				{
					FI.BusinessObjects.Olap.Hierarchy ratioHier=report.Schema.Hierarchies[(string)row["rpt_percentage_dim"]];
					FI.BusinessObjects.Olap.Hierarchy measuresHier=report.Schema.Hierarchies["[Measures]"];
					if(ratioHier!=null && measuresHier!=null)
					{
						System.Collections.ArrayList listNewMems=new System.Collections.ArrayList();
						foreach(FI.BusinessObjects.Olap.DataMember measure in measuresHier.DataMembers)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToVisAggr newMeasure=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToVisAggr(
								null,
								measuresHier , 
								measure , 
								ratioHier, 
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate.AggregateFunction.MAX);
							listNewMems.Add(newMeasure);
						}

						measuresHier.DataMembers.Clear();
						measuresHier.CalculatedMembers.Clear();

						foreach(FI.BusinessObjects.Olap.DataMember newMeasure in listNewMems)
							measuresHier.AddMember(newMeasure, true);
					}
				}
				else if(row["rpt_percentage_type"].ToString()=="5") // ratio to parent
				{
					FI.BusinessObjects.Olap.Hierarchy ratioHier=report.Schema.Hierarchies[(string)row["rpt_percentage_dim"]];
					FI.BusinessObjects.Olap.Hierarchy measuresHier=report.Schema.Hierarchies["[Measures]"];
					if(ratioHier!=null && measuresHier!=null)
					{
						System.Collections.ArrayList listNewMems=new System.Collections.ArrayList();
						foreach(FI.BusinessObjects.Olap.DataMember measure in measuresHier.DataMembers)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToParent newMeasure=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToParent(
								null,
								measuresHier , 
								measure , 
								ratioHier);
							listNewMems.Add(newMeasure);
						}

						measuresHier.DataMembers.Clear();
						measuresHier.CalculatedMembers.Clear();

						foreach(FI.BusinessObjects.Olap.DataMember newMeasure in listNewMems)
							measuresHier.AddMember(newMeasure, true);
					}
				}
				else if(row["rpt_percentage_type"].ToString()=="6") // ratio to all
				{
					FI.BusinessObjects.Olap.Hierarchy ratioHier=report.Schema.Hierarchies[(string)row["rpt_percentage_dim"]];
					FI.BusinessObjects.Olap.Hierarchy measuresHier=report.Schema.Hierarchies["[Measures]"];
					if(ratioHier!=null && measuresHier!=null)
					{
						System.Collections.ArrayList listNewMems=new System.Collections.ArrayList();
						foreach(FI.BusinessObjects.Olap.DataMember measure in measuresHier.DataMembers)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToAll newMeasure=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.MemToAll(
								null,
								measuresHier , 
								measure , 
								ratioHier);
							listNewMems.Add(newMeasure);
						}

						measuresHier.DataMembers.Clear();
						measuresHier.CalculatedMembers.Clear();

						foreach(FI.BusinessObjects.Olap.DataMember newMeasure in listNewMems)
							measuresHier.AddMember(newMeasure, true);
					}
				}
				else if(row["rpt_percentage_type"].ToString()=="7") // ratio to measure
				{
					FI.BusinessObjects.Olap.Hierarchy measuresHier=report.Schema.Hierarchies["[Measures]"];

					// in order to get schema members
					if(measuresHier.IsOpen==false)
					{
						measuresHier.Open();
						measuresHier.Close();
					}

					FI.BusinessObjects.Olap.SchemaMember ratioMeasure=measuresHier.SchemaMembers.Find((string)row["rpt_percentage_measure"]);
					if(ratioMeasure!=null)
					{
						System.Collections.ArrayList listNewMems=new System.Collections.ArrayList();
						foreach(FI.BusinessObjects.Olap.DataMember measure in measuresHier.DataMembers)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.MeasureToMeasure newMeasure=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.MeasureToMeasure(
								null,
								measuresHier , 
								measure , 
								ratioMeasure,
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.MeasureToMeasure.Operations.DIVIDE
								);
							listNewMems.Add(newMeasure);
						}

						measuresHier.DataMembers.Clear();
						measuresHier.CalculatedMembers.Clear();

						foreach(FI.BusinessObjects.Olap.DataMember newMeasure in listNewMems)
							measuresHier.AddMember(newMeasure, true);
					}
				}





				// add visual aggregate measures
				// rpt_sum_dims, rpt_avg_dims, rpt_min_dims, rpt_max_dims
				strHiers=System.Text.RegularExpressions.Regex.Split( (string)row["rpt_sum_dims"] , System.Text.RegularExpressions.Regex.Escape("$~$") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				for(int i=0;i<strHiers.Length;i++)
				{
					string strHier=strHiers[i].Trim();
					if(strHier=="")
						continue;

					foreach(FI.BusinessObjects.Olap.Hierarchy hier in report.Schema.Hierarchies)
						if(hier.UniqueName==strHier && hier.Axis.Ordinal!=2)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate visAggrMem=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate(
								null,
								hier , 
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate.AggregateFunction.SUM);
							hier.AddMember(visAggrMem, true);
						}
				}

				strHiers=System.Text.RegularExpressions.Regex.Split( (string)row["rpt_avg_dims"] , System.Text.RegularExpressions.Regex.Escape("$~$") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				for(int i=0;i<strHiers.Length;i++)
				{
					string strHier=strHiers[i].Trim();
					if(strHier=="")
						continue;

					foreach(FI.BusinessObjects.Olap.Hierarchy hier in report.Schema.Hierarchies)
						if(hier.UniqueName==strHier && hier.Axis.Ordinal!=2)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate visAggrMem=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate(
								null,
								hier , 
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate.AggregateFunction.AVG);
							hier.AddMember(visAggrMem, true);
						}
				}

				strHiers=System.Text.RegularExpressions.Regex.Split( (string)row["rpt_min_dims"] , System.Text.RegularExpressions.Regex.Escape("$~$") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				for(int i=0;i<strHiers.Length;i++)
				{
					string strHier=strHiers[i].Trim();
					if(strHier=="")
						continue;

					foreach(FI.BusinessObjects.Olap.Hierarchy hier in report.Schema.Hierarchies)
						if(hier.UniqueName==strHier && hier.Axis.Ordinal!=2)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate visAggrMem=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate(
								null,
								hier , 
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate.AggregateFunction.MIN);
							hier.AddMember(visAggrMem, true);
						}
				}

				strHiers=System.Text.RegularExpressions.Regex.Split( (string)row["rpt_max_dims"] , System.Text.RegularExpressions.Regex.Escape("$~$") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				for(int i=0;i<strHiers.Length;i++)
				{
					string strHier=strHiers[i].Trim();
					if(strHier=="")
						continue;

					foreach(FI.BusinessObjects.Olap.Hierarchy hier in report.Schema.Hierarchies)
						if(hier.UniqueName==strHier && hier.Axis.Ordinal!=2)
						{
							FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate visAggrMem=new FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate(
								null,
								hier , 
								FI.BusinessObjects.Olap.CalculatedMemberTemplates.VisualAggregate.AggregateFunction.MAX);
							hier.AddMember(visAggrMem, true);
						}
				}


				// empty, non-empty axes
				// rpt_empty_rows, rpt_empty_cols, rpt_nonempty_rows, rpt_nonempty_cols
				if((bool)row["rpt_nonempty_rows"]==true) //confused in old version
					report.Axes[1].EmptyTupleOption=FI.BusinessObjects.Olap.Axis.EmptyTupleOptionEnum.HIDE_EMPTY;
				else if((bool)row["rpt_empty_rows"]==true)
					report.Axes[1].EmptyTupleOption=FI.BusinessObjects.Olap.Axis.EmptyTupleOptionEnum.HIDE_NONEMPTY;
				else
					report.Axes[1].EmptyTupleOption=FI.BusinessObjects.Olap.Axis.EmptyTupleOptionEnum.NONE;

				if((bool)row["rpt_nonempty_cols"]==true) //confused in old version
					report.Axes[0].EmptyTupleOption=FI.BusinessObjects.Olap.Axis.EmptyTupleOptionEnum.HIDE_EMPTY;
				else if((bool)row["rpt_empty_cols"]==true)
					report.Axes[0].EmptyTupleOption=FI.BusinessObjects.Olap.Axis.EmptyTupleOptionEnum.HIDE_NONEMPTY;
				else
					report.Axes[0].EmptyTupleOption=FI.BusinessObjects.Olap.Axis.EmptyTupleOptionEnum.NONE;
				


				// order , order tuple - last , after all calc members added
				// rpt_order, rpt_order_tuple
				// NOTE : percentage measures are not translated , so they cannot be ordered
				string[] strOrderMems=System.Text.RegularExpressions.Regex.Split( (string)row["rpt_order_tuple"] , System.Text.RegularExpressions.Regex.Escape("],[") , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				string strOrder=(string)row["rpt_order"];
				if(strOrder=="BASC")
					report.Axes[0].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BASC; // columns only in old reports
				if(strOrder=="BDESC")
					report.Axes[0].Order=FI.BusinessObjects.Olap.Axis.OrderEnum.BDESC; // columns only in old reports

				if(report.Axes[0].Order!=FI.BusinessObjects.Olap.Axis.OrderEnum.NONE)
					foreach(FI.BusinessObjects.Olap.Hierarchy hier in report.Axes[0].Hierarchies) // columns only in old reports
					{
						for(int i=0;i<strOrderMems.Length;i++)
						{
							string orderMem=strOrderMems[i];

							if(orderMem.StartsWith("[")==false)
								orderMem="[" + orderMem;
							if(orderMem.EndsWith("]")==false)
								orderMem=orderMem + "]";

							// change old-styled visual aggregate names
							orderMem=orderMem.Replace("[** SUM **]" , "[*VIS SUM*]");
							orderMem=orderMem.Replace("[** AVG **]" , "[*VIS AVG*]");
							orderMem=orderMem.Replace("[** MIN **]" , "[*VIS MIN*]");
							orderMem=orderMem.Replace("[** MAX **]" , "[*VIS MAX*]");

							if(hier.DataMembers[orderMem]!=null)
							{
								hier.OrderTupleMember=orderMem;
								break;
							}
						}
					}

				// save report
				report.SaveState();
				report.Close(true);
				
			}


			// update shared reports 
			cmd=con.CreateCommand();
			cmd.CommandText=@"

				UPDATE child
				SET child.data=parent.data
				FROM DBFINF.dbo.t_olap_reports parent , DBFINF.dbo.t_olap_reports child 
				WHERE child.parent_report_id=parent.id
							";
			cmd.ExecuteNonQuery();

		}


		private void CleanupDestinationDb(int CompanyId, string SqlServerIP, string SaPassword)
		{
			SqlConnection con=new SqlConnection("server=" + SqlServerIP + "; User ID=sa; Password=" + SaPassword + "; Database=DBFINF;");
			con.Open();


			SqlCommand cmd=con.CreateCommand();
			cmd.CommandText=@"

				DELETE tdistribution_log WHERE distribution_id IN(select distribution_id from tdistribution where contact_id in (select id from tcontacts where user_id in (select id from tusers where company_id=(" + CompanyId.ToString() + @"+0)  )))
				DELETE tdistribution where contact_id in (select id from tcontacts where user_id in (select id from tusers where company_id=(" + CompanyId.ToString() + @"+0)  ))

				DELETE tcontacts where user_id in (select id from  tusers where company_id=(" + CompanyId.ToString() + @"+0)  )

				DELETE t_olap_reports_state  where rpt_id in (select id from t_olap_reports where user_id in (select id from  tusers where company_id=(" + CompanyId.ToString() + @"+0)  ))
				DELETE t_olap_reports where user_id in (select id from tusers where company_id=(" + CompanyId.ToString() + @"+0)  )

				DELETE t_sql_reports_state  where rpt_id in (select id from  t_sql_reports where id in (select id from  tusers where company_id=(" + CompanyId.ToString() + @"+0)  ))
				DELETE t_sql_reports where user_id in (select id from  tusers where company_id=(" + CompanyId.ToString() + @"+0)  )

				DELETE t_mdx_reports_state  where rpt_id in (select id from  t_mdx_reports where user_id in (select id from  tusers where company_id=(" + CompanyId.ToString() + @"+0)  ))
				DELETE t_mdx_reports where user_id in (select id from  tusers where company_id=(" + CompanyId.ToString() + @"+0)  )

				DELETE t_storecheck_reports_state  where rpt_id in (select id from  t_storecheck_reports where user_id in (select id from  tusers where company_id=(" + CompanyId.ToString() + @"+0)  ))
				DELETE t_storecheck_reports where user_id in (select id from  tusers where company_id=(" + CompanyId.ToString() + @"+0)  )

				DELETE tusers where company_id=(" + CompanyId.ToString() + @"+0)

				DELETE tcompany where id=(" + CompanyId.ToString() + @"+0)

				";

			cmd.ExecuteNonQuery();

		}


		private void ConvertWithSql(int CompanyId, string OlapServerIp, string SqlServerIP, string SaPassword)
		{

			SqlConnection con=new SqlConnection("server=" + SqlServerIP + "; User ID=sa; Password=" + SaPassword + "; Database=DBFINF;");
			con.Open();



			// companies -----------------
			SqlCommand cmd=con.CreateCommand();
			cmd.CommandText=@"

			SET IDENTITY_INSERT DBFINF.dbo.tcompany  ON

			INSERT INTO DBFINF.dbo.tcompany(id, name, short_name, olap_server, olap_provider, olap_db, olap_cube, src_db, src_user, ping)
				SELECT company_id+0, company_name, company_short_name, '" + OlapServerIp + @"' , olap_provider, olap_db, olap_cube, src_db, src_user, ping
					FROM DBWEB.dbo.tcompany WHERE company_id=(" + CompanyId.ToString() + @" )

			SET IDENTITY_INSERT DBFINF.dbo.tcompany  OFF

			";
			cmd.ExecuteNonQuery();
	




			// users -----------------
			cmd=con.CreateCommand();
			cmd.CommandText=@"

			SET IDENTITY_INSERT DBFINF.dbo.tusers  ON

			INSERT INTO DBFINF.dbo.tusers(id, company_id, logon, pwd, pwd_timestamp, name, email, conn_address, session_id, is_admin, is_logged_in)
				SELECT user_id+0, company_id+0, user_logon, user_pwd, GetDate(), user_name, user_email, user_conn_address, user_session_id, user_admin, user_logged_in
					FROM DBWEB.dbo.tusers WHERE company_id=(" + CompanyId.ToString() + @" )

			SET IDENTITY_INSERT DBFINF.dbo.tusers  OFF

			";
			cmd.ExecuteNonQuery();



			// contacts -----------------
			cmd=con.CreateCommand();
			cmd.CommandText=@"

			SET IDENTITY_INSERT DBFINF.dbo.tcontacts  ON

			INSERT INTO DBFINF.dbo.tcontacts(id, user_id, name, email, distr_format)
				SELECT contact_id+0, user_id+0, contact_name, contact_email, distr_format
					FROM DBWEB.dbo.tcontacts WHERE user_id IN(SELECT user_id FROM DBWEB.dbo.tusers WHERE company_id=(" + CompanyId.ToString() + @" )  )

			SET IDENTITY_INSERT DBFINF.dbo.tcontacts  OFF

			";
			cmd.ExecuteNonQuery();




			// distributions -----------------
			cmd=con.CreateCommand();
			cmd.CommandText=@"

			SET IDENTITY_INSERT DBFINF.dbo.tdistribution  ON

			INSERT INTO DBFINF.dbo.tdistribution(id, contact_id, rpt_id, rpt_type, freq_type, freq_value)
				SELECT distribution_id+0, contact_id+0, rpt_id+0, rpt_type, rpt_distr_freq_type, rpt_distr_freq_value
					FROM DBWEB.dbo.tdistribution WHERE contact_id IN(SELECT contact_id FROM DBWEB.dbo.tcontacts WHERE user_id IN(SELECT user_id FROM DBWEB.dbo.tusers WHERE company_id=(" + CompanyId.ToString() + @" )  ))

			SET IDENTITY_INSERT DBFINF.dbo.tdistribution  OFF

			";
			cmd.ExecuteNonQuery();




			// distribution_log -----------------
			cmd=con.CreateCommand();
			cmd.CommandText=@"

			SET IDENTITY_INSERT DBFINF.dbo.tdistribution_log  ON

			INSERT INTO DBFINF.dbo.tdistribution_log(distribution_id, id, status, message, timestamp)
				SELECT distribution_id+0, log_id+0, status, message, timestamp
					FROM DBWEB.dbo.tdistribution_log WHERE distribution_id IN(SELECT distribution_id FROM DBWEB.dbo.tdistribution WHERE contact_id IN(SELECT contact_id FROM DBWEB.dbo.tcontacts WHERE user_id IN(SELECT user_id FROM DBWEB.dbo.tusers WHERE company_id=(" + CompanyId.ToString() + @" )  )))

			SET IDENTITY_INSERT DBFINF.dbo.tdistribution_log  OFF

			";
			cmd.ExecuteNonQuery();




			
			// sql reports  -----------------
			cmd=con.CreateCommand();
			cmd.CommandText=@"

			SET IDENTITY_INSERT DBFINF.dbo.t_sql_reports  ON

			INSERT INTO DBFINF.dbo.t_sql_reports(id, parent_report_id, sharing_status, user_id, name, description, is_selected, sql, xsl, timestamp)
				SELECT rpt_id+0, case rpt_parent_report_id when 0 then 0 else rpt_parent_report_id+0 end, case when rpt_sharing_status>2 then rpt_sharing_status else 0 end, user_id+0, rpt_name, rpt_description, rpt_open, rpt_sql, 
				'" + FI.BusinessObjects.CustomSqlReport.DefaultXsl().Replace("'" , "''") +  @"' , 
				GetDate()
					FROM DBWEB.dbo.t_sql_reports WHERE user_id IN(SELECT user_id FROM DBWEB.dbo.tusers WHERE company_id=(" + CompanyId.ToString() + @" )  )

			SET IDENTITY_INSERT DBFINF.dbo.t_sql_reports  OFF

			";
			cmd.ExecuteNonQuery();


			



			// mdx reports  -----------------
			cmd=con.CreateCommand();
			cmd.CommandText=@"

			SET IDENTITY_INSERT DBFINF.dbo.t_mdx_reports  ON

			INSERT INTO DBFINF.dbo.t_mdx_reports(id, parent_report_id, sharing_status, user_id, name, description, is_selected, mdx, xsl, timestamp)
				SELECT rpt_id+0, case rpt_parent_report_id when 0 then 0 else rpt_parent_report_id+0 end, case when rpt_sharing_status>2 then rpt_sharing_status else 0 end, user_id+0, rpt_name, rpt_description, rpt_open, rpt_mdx, 
				'" + FI.BusinessObjects.CustomMdxReport.DefaultXsl().Replace("'" , "''") +  @"' , 
				GetDate()
					FROM DBWEB.dbo.t_mdx_reports WHERE user_id IN(SELECT user_id FROM DBWEB.dbo.tusers WHERE company_id=(" + CompanyId.ToString() + @" )  )

			SET IDENTITY_INSERT DBFINF.dbo.t_mdx_reports  OFF

			";
			cmd.ExecuteNonQuery();



			con.Close();
		}




		private void ConvertStorecheckReports(int CompanyId, string SqlServerIP, string SaPassword)
		{
			SqlConnection con=new SqlConnection("server=" + SqlServerIP + "; User ID=sa; Password=" + SaPassword + "; Database=DBFINF;");
			con.Open();
			


			// storecheck reports  -----------------
			DataTable table=new DataTable();
			SqlDataAdapter adapter=new SqlDataAdapter();

			SqlCommand cmd=con.CreateCommand();
			cmd.CommandText=@"
			SELECT rpt_id+0 as rpt_id, user_id+0 as user_id, rpt_name, rpt_description, rpt_days, rpt_show_all, rpt_products, rpt_products_logic, rpt_selected, rpt_saved, rpt_open, rpt_cache_date, rpt_filter 
					FROM DBWEB.dbo.t_storecheck_reports WHERE user_id IN(SELECT user_id FROM DBWEB.dbo.tusers WHERE company_id=(" + CompanyId.ToString() + @" )  )
			";

			adapter.SelectCommand=cmd;
			adapter.Fill(table);

			foreach(DataRow row in table.Rows)
			{
				string productsXml="<PRODUCTS />";
				string filterXml="<FILTER />";


				// create filterXml
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				if(row["rpt_filter"]!=System.DBNull.Value && row["rpt_filter"].ToString().Trim()!="")
				{
					string[] filters=System.Text.RegularExpressions.Regex.Split(row["rpt_filter"].ToString().Trim() , " AND " , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
					if(filters!=null && filters.Length>0)
					{
						sb.Append("<FILTER>");
						for(int i=0;i<filters.Length;i++)
						{
							string filter=filters[i].Trim();
							string[] filtParts=System.Text.RegularExpressions.Regex.Split(filter , " Like " , System.Text.RegularExpressions.RegexOptions.IgnoreCase);
							if(filtParts.Length!=2)
								throw new Exception("invalid filter expression: " + filter);

							string colName=filtParts[0].Trim();
							string colValue=filtParts[1].Trim();
							colValue=colValue.Trim(new char[]{'\''});

							sb.Append(@"<COL N=""" + FI.Common.XML.XmlEncode(colName) + @""" V=""" + FI.Common.XML.XmlEncode(colValue) + @"""/>");
						}
						sb.Append("</FILTER>");
						filterXml=sb.ToString();
					}
				}


				// create productsXml
				sb=new System.Text.StringBuilder();
				if(row["rpt_products"]!=System.DBNull.Value && row["rpt_products"].ToString().Trim()!="")
				{
					string[] prodserns=row["rpt_products"].ToString().Trim().Split(new char[]{','});
					if(prodserns!=null && prodserns.Length>0)
					{
						sb.Append("<PRODUCTS>");
						for(int i=0;i<prodserns.Length;i++)
						{
							string prodsern=prodserns[i].Trim();
							prodsern=prodsern.Trim(new char[]{'\''});

							if(prodsern.Length!=15)
								throw new Exception("invalid prodsern: " + prodsern);

							sb.Append(@"<PR SN=""" + FI.Common.XML.XmlEncode(prodsern) + @"""/>");
						}
						sb.Append("</PRODUCTS>");
						productsXml=sb.ToString();
					}
				}


				cmd=con.CreateCommand();
				cmd.CommandText=@"
				SET IDENTITY_INSERT DBFINF.dbo.t_storecheck_reports  ON

				INSERT INTO DBFINF.dbo.t_storecheck_reports(
					id, 
					parent_report_id, 
					sharing_status, 
					user_id, 
					name, 
					description, 
					is_selected, 
					products_xml, 
					products_logic, 
					days, 
					filter_xml, 
					cache_timestamp, 
					timestamp,
					insel,
					inbsel)
					VALUES(
					" + row["rpt_id"].ToString() + @" , 
					0 , 
					0 , 
					" + row["user_id"].ToString() + @" , 
					'" + row["rpt_name"].ToString().Replace("'" , "''") + @"' , 
					'" + row["rpt_description"].ToString().Replace("'" , "''") + @"' , 
					" + (row["rpt_open"].ToString()=="True"?1:0).ToString() + @" , 
					'" + productsXml.Replace("'" , "''") + @"' , 
					" + (row["rpt_products_logic"].ToString()=="True"?1:0).ToString() + @" , 
					" + row["rpt_days"].ToString() + @" , 
					'" + filterXml.Replace("'" , "''") + @"' , 
					GetDate(),
					GetDate(),
					0,
					0
					)

				SET IDENTITY_INSERT DBFINF.dbo.t_storecheck_reports  OFF

							";
				cmd.ExecuteNonQuery();
			}

		}




	}
}
