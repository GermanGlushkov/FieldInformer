namespace FI.UI.Web.OlapReport
{
	
	using System;
	using System.Data;
	using System.Collections;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using FI.BusinessObjects;
	using FI.BusinessObjects.Olap;
	using FI.BusinessObjects.Olap.CalculatedMemberTemplates;
	using FI.Common.Data;
	using DevExpress.XtraCharts;


	/// <summary>
	///		Summary description for TableControl.
	/// </summary>
	public class GraphControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TableCell cellGraph;
		protected System.Web.UI.WebControls.Table tblMain;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkCat;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkSeries;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkValues;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkScal;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkRotateAxes;
		protected System.Web.UI.HtmlControls.HtmlSelect selType;
		protected System.Web.UI.HtmlControls.HtmlSelect selColorTheme;
		protected System.Web.UI.WebControls.TextBox hSize;
		protected System.Web.UI.WebControls.TextBox vSize;
		private ChartControl _chCtrl;
		public BusinessObjects.OlapReport _report;
		private Controller _contr;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			_chCtrl=new ChartControl();
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
			
			// selType
			string[] names=System.Enum.GetNames(_report.GraphType.GetType());
			Array.Sort(names);
			for(int i=0;i<names.Length;i++)
			{
				selType.Items.Add(names[i]);
				if(_report.GraphType.ToString()==names[i])
					selType.Items[i].Selected=true;
			}

			// selColorTheme
			string theme=(_report.GraphTheme==null || _report.GraphTheme=="" ? _chCtrl.AppearanceName : _report.GraphTheme);
			string[] themes=_chCtrl.GetAppearanceNames();
			for(int i=0;i<themes.Length;i++)
			{
				selColorTheme.Items.Add(themes[i]);				
				if(theme==themes[i])				
					selColorTheme.Items[i].Selected=true;
			}		
			
			// hSize and vSize
			hSize.Text=AdjustGraphSize(_report.GraphWidth).ToString();
			vSize.Text=AdjustGraphSize(_report.GraphHeight).ToString();
			
			//chkRotateAxes
			FI.BusinessObjects.OlapReport.GraphOptionsEnum graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.RotateAxes;
			chkRotateAxes.Checked=((_report.GraphOptions & graphOption)==graphOption);

			//chkScal
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.SetScaling;
			chkScal.Checked=((_report.GraphOptions & graphOption)==graphOption);

			//chkValues
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowValues;
			chkValues.Checked=((_report.GraphOptions & graphOption)==graphOption);

			//chkSeries
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowSeries;
			chkSeries.Checked=((_report.GraphOptions & graphOption)==graphOption);

			//chkCat
			graphOption=FI.BusinessObjects.OlapReport.GraphOptionsEnum.ShowCategories;
			chkCat.Checked=((_report.GraphOptions & graphOption)==graphOption);

		}


		public string GraphType
		{
			get { return Request.Form[this.UniqueID + ":selType"];}
		}

		public string GraphTheme
		{
			get { return Request.Form[this.UniqueID + ":selColorTheme"];}
		}

		public short AdjustGraphSize(short val)
		{			
			return (val<200 ? (short)200 : (val>3200 ? (short)3200 : val));
		}
		public short GraphWidth
		{
			get 
			{ 
				short ret=800;
				try
				{
					ret=short.Parse(Request.Form[this.UniqueID + ":hSize"]);
				}
				catch{}
				return AdjustGraphSize(ret);
			}
		}

		public short GraphHeight
		{
			get 
			{ 
				short ret=600;
				try
				{
					ret=short.Parse(Request.Form[this.UniqueID + ":vSize"]);
				}
				catch{}
				return AdjustGraphSize(ret);
			}
		}

		public bool ShowSeries
		{
			get { return (Request.Form[this.UniqueID + ":chkSeries"]==null?false:true);}
		}

		public bool ShowCat
		{
			get { return (Request.Form[this.UniqueID + ":chkCat"]==null?false:true);}
		}

		public bool ShowValues
		{
			get { return (Request.Form[this.UniqueID + ":chkValues"]==null?false:true);}
		}

		public bool SetScal
		{
			get { return (Request.Form[this.UniqueID + ":chkScal"]==null?false:true);}
		}

		public bool SetRotateAxes
		{
			get { return (Request.Form[this.UniqueID + ":chkRotateAxes"]==null?false:true);}
		}


		public void LoadGraph()
		{								
			if(_report.State!=Report.StateEnum.Executed)
				return;
			
			bool rotateGraph=false;
			bool rotateAxes=((_report.GraphOptions & OlapReport.GraphOptionsEnum.RotateAxes)>0);

			int seriesPosCount=(rotateAxes ? _report.Cellset.Axis1PosCount: _report.Cellset.Axis0PosCount);
			int seriesMemCount=(rotateAxes ? _report.Cellset.Axis1TupleMemCount: _report.Cellset.Axis0TupleMemCount);
			int catPosCount=(rotateAxes ? _report.Cellset.Axis0PosCount: _report.Cellset.Axis1PosCount);
			int catMemCount=(rotateAxes ? _report.Cellset.Axis0TupleMemCount: _report.Cellset.Axis1TupleMemCount);
			
			if(seriesPosCount>256)
				seriesPosCount=256;			

			if(catPosCount>1024)
				catPosCount=1024;

			bool showValues=(_report.GraphOptions & OlapReport.GraphOptionsEnum.ShowValues)>0;
			bool showSeries=(_report.GraphOptions & OlapReport.GraphOptionsEnum.ShowSeries)>0;
			bool showCats=(_report.GraphOptions & OlapReport.GraphOptionsEnum.ShowCategories)>0;
			bool setScaling=(_report.GraphOptions & OlapReport.GraphOptionsEnum.SetScaling)>0;

			// create series
			for(int i=0;i<seriesPosCount;i++)
			{
				string name="";
				for(int j=0; j<seriesMemCount;j++)				
					name+=(j==0 ? "" : " | ")+ (rotateAxes ? _report.Cellset.GetCellsetMember(1, j, i).Name : _report.Cellset.GetCellsetMember(0, j, i).Name);

				Series series=new Series();
				series.Name=name;	
				_chCtrl.Series.Add(series);					

				// type				
				if(_report.GraphType==OlapReport.GraphTypeEnum.Pie)
				{
					// limit number of series
					if(seriesPosCount>6)
						seriesPosCount=6;

					// disable scaling for pie
					setScaling=false;

					series.ChangeView(ViewType.Pie);

					PiePointOptions ppo=(PiePointOptions)series.PointOptions;
					PieSeriesView psw=(PieSeriesView)series.View;		
					PieSeriesLabel psl=(PieSeriesLabel)series.Label;
					
					psl.Position=PieSeriesLabelPosition.TwoColumns;
					series.ShowInLegend=false; // cause it's shown in pie anyway
					showValues=true;
					showCats=true;

					this.chkCat.Checked=true;
					this.chkCat.Disabled=true;
					this.chkValues.Checked=true;
					this.chkValues.Disabled=true;
				}
				else
				{					
					if(_report.GraphType==OlapReport.GraphTypeEnum.BarVertical)			
					{
						series.ChangeView(ViewType.Bar);
					}
					else if(_report.GraphType==OlapReport.GraphTypeEnum.BarHorizontal)				
					{
						series.ChangeView(ViewType.Bar);	
						rotateGraph=true;
					}	
					else if(_report.GraphType==OlapReport.GraphTypeEnum.StackedBarVertical)				
						series.ChangeView(ViewType.StackedBar);
					else if(_report.GraphType==OlapReport.GraphTypeEnum.StackedBarHorizontal)
					{
						series.ChangeView(ViewType.StackedBar);
						rotateGraph=true;
					}
					else if(_report.GraphType==OlapReport.GraphTypeEnum.LineHorizontal)				
						series.ChangeView(ViewType.Line);

					series.LegendText=name;
					series.ShowInLegend=true;
					series.ValueScaleType=ScaleType.Numerical;				
					series.Visible=true;	
					
					// labels orientation
					XYDiagram diag=(XYDiagram)_chCtrl.Diagram;		
					diag.Rotated=rotateGraph;		
					if(rotateGraph)
					{
						diag.AxisY.Label.Antialiasing=true;
						diag.AxisY.Label.Angle=315;	
					}
					else
					{
						diag.AxisX.Label.Antialiasing=true;
						diag.AxisX.Label.Angle=315;	
					}

					// if scaling
					if(setScaling)
						diag.AxisY.Visible=false;

//					if(setScaling)
//					{
//						// hide axis, cause it won't display real values
//						if(seriesPosCount<10)
//							diag.AxisY.Visible=false;	
//						if(i<10)
//						{
//							SecondaryAxisY axisY=new SecondaryAxisY(name);
//							
//							axisY.Alignment=AxisAlignment.Near;
//							axisY.Title.Text = name;
//							axisY.Title.Visible = true;		
//							axisY.Title.Font=new Font(axisY.Title.Font.Name, 10);
//							axisY.Label.Antialiasing=true;
//							axisY.Label.Angle=315;	
//
//							diag.SecondaryAxesY.Add(axisY);	
//							((XYDiagramSeriesViewBase)series.View).AxisY = axisY;
//						}
//					}
				}	
	
				
				// prepare scaling ranges
				double scalingMin=double.MaxValue;
				double scalingMax=double.MinValue;
				if(setScaling)
				{
					for(int l=0;l<catPosCount;l++)
					{
						string val=(rotateAxes ? _report.Cellset.GetCell(l, i).Value : _report.Cellset.GetCell(i, l).Value);
						double dVal=0;
						double.TryParse(val, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dVal);
						if(dVal<scalingMin)
							scalingMin=dVal;
						if(dVal>scalingMax)
							scalingMax=dVal;
					}
				}
				scalingMin=scalingMin-(scalingMax-scalingMin)*0.1;

				// set data
				for(int l=0; l<catPosCount;l++)	
				{	
					string argument="";
					for(int m=0;m<catMemCount;m++)
						argument+=(m==0 ? "" : " | ")+ (rotateAxes ? _report.Cellset.GetCellsetMember(0, m, l).Name : _report.Cellset.GetCellsetMember(1, m, l).Name);

					string val=(rotateAxes ? _report.Cellset.GetCell(l, i).Value : _report.Cellset.GetCell(i, l).Value);
					string fVal=(rotateAxes ? _report.Cellset.GetCell(l, i).FormattedValue : _report.Cellset.GetCell(i, l).FormattedValue);
					double dVal=0;
					double.TryParse(val, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dVal);
				
					if(setScaling)
						dVal=(scalingMax-scalingMin==0 ? 0 : (scalingMin+dVal)/(scalingMax-scalingMin));					
					
					// custom point label
					string customLabel=string.Empty;
					if(!showValues && !showSeries&& !showCats)
						series.Label.Visible=false;
					else
					{
						customLabel=(showSeries ? series.Name : string.Empty);
						customLabel+=(showCats ? (customLabel!=string.Empty ? " | " : string.Empty) + argument : string.Empty);
						customLabel+=(showValues ? (customLabel!=string.Empty ? ": " : string.Empty) + fVal : string.Empty);
					}	

					CustomChartSeriesPoint sp=new CustomChartSeriesPoint(argument, dVal, customLabel);
					series.Points.Add(sp);					
				}																
			}
									
			
			ChartTitle title=new ChartTitle();
			title.Alignment=StringAlignment.Center;
			title.Lines=new string[]{ _report.Name, _report.Description };
			_chCtrl.Titles.Add(title);
			
			if(_report.GraphTheme!=null && _report.GraphTheme!="")
				_chCtrl.AppearanceName=_report.GraphTheme;
			_chCtrl.Width=AdjustGraphSize(_report.GraphWidth);
			_chCtrl.Height=AdjustGraphSize(_report.GraphHeight);

			string imgNamePrefix=_report.GetType().Name + _report.ID.ToString();

			//delete older images of same report
			string[] filePaths=System.IO.Directory.GetFiles(FI.Common.AppConfig.TempDir  , imgNamePrefix + "*.PNG");
			if(filePaths!=null && filePaths.Length>0)
				for(int j=0;j<filePaths.Length;j++)
				{
					try
					{
						System.IO.File.Delete(filePaths[j]);
					}
					catch(Exception exc)
					{
						//do nothing
						exc=null;
					}
				}
			

			//write to file and display, it will overwite itself if needed
			string imgName=imgNamePrefix + "." + DateTime.Now.ToString("yyyyMMddHHssfff") + ".PNG";
			string imgVirtPath=Request.ApplicationPath + "/" + FI.Common.AppConfig.TempVirtualDir + "/" + imgName;
			string imgPhysPath=FI.Common.AppConfig.TempDir + @"\" + imgName;		
	
			_chCtrl.ExportToImage(imgPhysPath, System.Drawing.Imaging.ImageFormat.Png);

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
