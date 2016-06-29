namespace FI.UI.Web.OlapReport
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;
	using FI.BusinessObjects.Olap;
	using FI.BusinessObjects.Olap.CalculatedMemberTemplates;
	using FI.Common.Data;

	/// <summary>
	///		Summary description for TableControl.
	/// </summary>
	public class GraphControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TableCell cellGraph;
		protected System.Web.UI.WebControls.Table tblMain;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkCat;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkSer;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkVal;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkScal;
		protected System.Web.UI.HtmlControls.HtmlSelect selType;
		public BusinessObjects.OlapReport _report;
		private Controller _contr;

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			_contr=new Controller(_report, this.Page);
			LoadGraphOptions();
			LoadGraph();
			base.Render (writer);
		}


		private void LoadGraphOptions()
		{
			//chkCat
			FI.BusinessObjects.OlapReport.GraphOptionsEnum graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowCategories;
			chkCat.Checked=((_report.GraphOptions & graphOption)==graphOption);

			//chkSer
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowSeries;
			chkSer.Checked=((_report.GraphOptions & graphOption)==graphOption);

			//chkVal
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowValues;
			chkVal.Checked=((_report.GraphOptions & graphOption)==graphOption);

			//chkScal
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.SetScaling;
			chkScal.Checked=((_report.GraphOptions & graphOption)==graphOption);


			// selType
			string[] names=System.Enum.GetNames(_report.GraphType.GetType());
			for(int i=0;i<names.Length;i++)
			{
				selType.Items.Add(names[i]);
				if(_report.GraphType.ToString()==names[i])
					selType.Items[i].Selected=true;
			}
		}


		public string GraphType
		{
			get { return Request.Form[this.UniqueID + ":selType"];}
		}

		public bool ShowCat
		{
			get { return (Request.Form[this.UniqueID + ":chkCat"]==null?false:true);}
		}

		public bool ShowSer
		{
			get { return (Request.Form[this.UniqueID + ":chkSer"]==null?false:true);}
		}

		public bool ShowVal
		{
			get { return (Request.Form[this.UniqueID + ":chkVal"]==null?false:true);}
		}

		public bool SetScal
		{
			get { return (Request.Form[this.UniqueID + ":chkScal"]==null?false:true);}
		}


		public void LoadGraph()
		{

			GRAPHCOMLib.GraphClass graph=new GRAPHCOMLib.GraphClass();
			
			//Type
			switch(_report.GraphType)
			{
				case FI.BusinessObjects.OlapReport.GraphTypeEnum.BarVertical:
					graph.setVertBarType();
					break;
				case FI.BusinessObjects.OlapReport.GraphTypeEnum.BarHorizontal:
					graph.setHorzBarType();
					break;
				case FI.BusinessObjects.OlapReport.GraphTypeEnum.BarStacked:
					graph.setStckBarType();
					break;
				case FI.BusinessObjects.OlapReport.GraphTypeEnum.LineHorizontal:
					graph.setLineType();
					break;
				case FI.BusinessObjects.OlapReport.GraphTypeEnum.Pie:
					graph.setPieType();
					break;
				default:
					throw new NotSupportedException();
			}

			//Cat
			FI.BusinessObjects.OlapReport.GraphOptionsEnum graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowCategories;
			if((_report.GraphOptions & graphOption)==graphOption)
				graph.setShowCategories(1);

			//Ser
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowSeries;
			if((_report.GraphOptions & graphOption)==graphOption)
				graph.setShowSeries(1);

			//Val
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowValues;
			if((_report.GraphOptions & graphOption)==graphOption)
				graph.setShowValues(1);

			//Scal
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.SetScaling;
			if((_report.GraphOptions & graphOption)==graphOption)
				graph.setScalingBySets(1);




			// data
			if(_report.State==Report.StateEnum.Executed && (_report==null || _report.Cellset.IsValid==false))
			{
				this.cellGraph.CssClass="tbl1_err";
				this.cellGraph.Text="Report contains no data";
				return;
			}
			
			int Ax0MemCount=_report.Cellset.Axis0TupleMemCount;
			int Ax1MemCount=_report.Cellset.Axis1TupleMemCount;
			int Ax0PosCount=_report.Cellset.Axis0PosCount;
			int Ax1PosCount=_report.Cellset.Axis1PosCount;
			
			if(Ax0PosCount==0 || Ax1PosCount==0)
			{
				this.cellGraph.Text="Report contains no data";
				return;
			}


			// important!! graph restrictions:
			if(Ax0PosCount>32767)
				Ax0PosCount=32767;
			if(Ax1PosCount>127)
				Ax1PosCount=127;


			//col captions			
			System.Text.StringBuilder sbCols=new System.Text.StringBuilder();
			for(int i=0;i<Ax0PosCount;i++)
			{
				for(int imem=0;imem<Ax0MemCount;imem++)
				{
					sbCols.Append(_report.Cellset.GetCellsetMember(0 , imem , i).Name);
					if(imem<Ax0MemCount-1)
						sbCols.Append(":");
				}
				sbCols.Append("\t");
			}

			// row captions
			System.Text.StringBuilder sbRows=new System.Text.StringBuilder();
			for(int j=0;j<Ax1PosCount;j++)
			{
				for(int jmem=0;jmem<Ax1MemCount;jmem++)
				{
					sbRows.Append(_report.Cellset.GetCellsetMember(1 , jmem , j).Name);
					if(jmem<Ax1MemCount-1)
						sbRows.Append(":");
				}
				sbRows.Append("\t");
			}

			// data
			double[] values=new double[Ax0PosCount*Ax1PosCount];
			for(int i=0;i<Ax0PosCount;i++)
			{
				for(int j=0;j<Ax1PosCount;j++)
				{
					string val=_report.Cellset.GetCell(i,j).Value;
					try
					{
						if(val=="")
							values[i*Ax1PosCount + j]=0;
						else
							values[i*Ax1PosCount + j]=double.Parse(val);
					}
					catch(Exception exc)
					{
						values[i*Ax1PosCount + j]=0;
					}
				}
			}

			
			if(sbCols.Length>0) //remove last tab
				sbCols.Remove(sbCols.Length-1,1);
			if(sbRows.Length>0) //remove last tab
				sbRows.Remove(sbRows.Length-1,1);
				

			graph.setValues(Ax1PosCount , Ax0PosCount , ref values[0]);
			graph.setLegends(sbRows.ToString());
			graph.setLabels(sbCols.ToString());




			//delete old jpg files if exist
			string[] filePaths=System.IO.Directory.GetFiles(FI.Common.AppConfig.TempDir  , "*.JPG");
			if(filePaths!=null && filePaths.Length>0)
			{
				for(int i=0;i<filePaths.Length;i++)
				{
					try
					{
						DateTime crTime=System.IO.File.GetCreationTime(filePaths[i]);
						if(crTime.Minute<DateTime.Now.Minute || crTime.DayOfYear<DateTime.Now.DayOfYear )
							System.IO.File.Delete(filePaths[i]);
					}
					catch(Exception exc)
					{
						//do nothing
						exc=null;
					}
				}
			}

			//write to file and display, it will overwite itself if needed
			string imgName=_report.GetType().Name + _report.ID.ToString() + "." + DateTime.Now.Ticks.ToString() + ".JPG";
			string imgVirtPath=Request.ApplicationPath + "/" + FI.Common.AppConfig.TempVirtualDir + "/" + imgName;
			string imgPhysPath=FI.Common.AppConfig.TempDir + @"\" + imgName;
			graph.WriteFile(imgPhysPath , 0);
			System.Web.UI.WebControls.Image img=new System.Web.UI.WebControls.Image();
			img.ImageUrl=imgVirtPath;
			this.cellGraph.Controls.Add(img);

		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
