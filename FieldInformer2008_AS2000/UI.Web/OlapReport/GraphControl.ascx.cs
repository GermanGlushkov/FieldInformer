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
    public partial class GraphControl : System.Web.UI.UserControl
    {
        public static byte __MaxPieCount = 10;
        public static short __MaxSeriesCount = 256;
        public static short __MaxCategoriesCount = 1024;
        public static Size __MinChartSize = new Size(200, 200);
        public static Size __MaxChartSize = new Size(3200, 3200);
        public static Size __MaxPieSize = new Size(1200, 1200);
        public static Size __DefaultPieSize = new Size(500, 500);
        public static Size __DefaultChartSize = new Size(800, 600);


        private DevExpress.XtraCharts.Web.WebChartControl _chCtrl;
        public BusinessObjects.OlapReport _report;
        private Controller _contr;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            _chCtrl = new DevExpress.XtraCharts.Web.WebChartControl();
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            _contr = new Controller(_report, this.Page);
            LoadGraphOptions();
            LoadGraph();
            
            base.Render(writer);
        }


        private void LoadGraphOptions()
        {
            // selType
            string[] names = System.Enum.GetNames(_report.GraphType.GetType());
            Array.Sort(names);
            for (int i = 0; i < names.Length; i++)
            {
                selType.Items.Add(names[i]);
                if (_report.GraphType.ToString() == names[i])
                    selType.Items[i].Selected = true;
            }

            // selColorTheme
            string theme = (_report.GraphTheme == null || _report.GraphTheme == "" ? _chCtrl.AppearanceName : _report.GraphTheme);
            string[] themes = _chCtrl.GetAppearanceNames();
            for (int i = 0; i < themes.Length; i++)
            {
                selColorTheme.Items.Add(themes[i]);
                if (theme == themes[i])
                    selColorTheme.Items[i].Selected = true;
            }

            // selNumOptions
            selNumOptions.Items.Add(new ListItem("Values", "0"));
            selNumOptions.Items.Add(new ListItem("Scaling by Series", "1"));
            selNumOptions.Items.Add(new ListItem("Percent by Categories", "2"));
            if ((_report.GraphOptions & OlapReport.GraphOptionsEnum.ScalingBySeries) > 0)
            {
                if (_report.GraphType == OlapReport.GraphTypeEnum.Pie)
                    selNumOptions.SelectedIndex = 0;

                else
                    selNumOptions.SelectedIndex = 1;
            }
            else if ((_report.GraphOptions & OlapReport.GraphOptionsEnum.PercentByCategories) > 0)
                selNumOptions.SelectedIndex = 2;
            else
                selNumOptions.SelectedIndex = 0;

            // txtWidth and txtHeight
            Size size = AdjustGraphWidth(new Size(_report.GraphWidth, _report.GraphHeight), _report.GraphType);
            txtWidth.Text = size.Width.ToString();
            txtHeight.Text = size.Height.ToString();

            // areaPieColumns and txtPieColumns
            this.areaPieColumns.Visible = (_report.GraphType == OlapReport.GraphTypeEnum.Pie || _report.GraphType == OlapReport.GraphTypeEnum.PieBrokenApart);
            txtPieColumns.Text = _report.GraphPieColumns.ToString();

            // areaMixedLinePos and txtMixedLinePos
            this.areaMixedLinePos.Visible = (_report.GraphType == OlapReport.GraphTypeEnum.LineWithBars);
            // this.selMixedLinePos will be handled in LoadGraph()
            //this.txtMixedLinePos.Text = _report.GraphMixedLinePosition.ToString();

            //chkRotateAxes
            OlapReport.GraphOptionsEnum graphOption = OlapReport.GraphOptionsEnum.Pivot;
            chkRotateAxes.Checked = ((_report.GraphOptions & graphOption) == graphOption);

            //chkValues
            graphOption = OlapReport.GraphOptionsEnum.ShowValues;
            chkValues.Checked = ((_report.GraphOptions & graphOption) == graphOption);

            //chkSeries
            graphOption = OlapReport.GraphOptionsEnum.ShowSeries;
            chkSeries.Checked = ((_report.GraphOptions & graphOption) == graphOption);

            //chkCat
            graphOption = OlapReport.GraphOptionsEnum.ShowCategories;
            chkCat.Checked = ((_report.GraphOptions & graphOption) == graphOption);
        }


        public string GraphType
        {
            get { return Request.Form[this.UniqueID + ":selType"]; }
        }

        public string GraphTheme
        {
            get { return Request.Form[this.UniqueID + ":selColorTheme"]; }
        }

        public Size AdjustGraphWidth(Size size, OlapReport.GraphTypeEnum type)
        {
            Size ret = size;
            if (type == OlapReport.GraphTypeEnum.PieBrokenApart)
            {
                // if change from other graph type and values is big, set default for PieBrokenApart
                if (_report.GraphType != OlapReport.GraphTypeEnum.PieBrokenApart)
                    ret = AdjustSize(ret, __DefaultPieSize, __DefaultPieSize, __DefaultPieSize);
                else
                    ret = AdjustSize(ret, __MinChartSize, __MaxPieSize, __DefaultPieSize);
            }
            else
                ret = AdjustSize(ret, __MinChartSize, __MaxChartSize, __DefaultChartSize);

            return ret;
        }

        private Size AdjustSize(Size size, Size min, Size max, Size defaultSize)
        {
            Size ret = size;

            if (ret.Height == 0 && ret.Width == 0)
            {
                ret.Height = defaultSize.Height;
                ret.Width = defaultSize.Width;
            }
            else
            {
                if (ret.Height < min.Height)
                    ret.Height = min.Height;
                if (ret.Width < min.Width)
                    ret.Width = min.Width;

                if (ret.Height > max.Height)
                    ret.Height = max.Height;
                if (ret.Width > max.Width)
                    ret.Width = max.Width;
            }

            return ret;
        }

        public Size GraphSize
        {
            get
            {
                Size ret = __DefaultChartSize;
                try
                {
                    ret.Width = short.Parse(Request.Form[this.UniqueID + ":txtWidth"]);
                }
                catch { }
                try
                {
                    ret.Height = short.Parse(Request.Form[this.UniqueID + ":txtHeight"]);
                }
                catch { }
                return AdjustGraphWidth(ret, (OlapReport.GraphTypeEnum)Enum.Parse(typeof(OlapReport.GraphTypeEnum), this.GraphType));
            }
        }

        public byte GraphPieColumns
        {
            get
            {
                byte ret = 0;
                try
                {
                    ret = byte.Parse(Request.Form[this.UniqueID + ":txtPieColumns"]);
                }
                catch { }
                return ret;
            }
        }

        public byte GraphMixedLinePos
        {
            get
            {
                byte ret = 0;
                try
                {
                    ret = byte.Parse(Request.Form[this.UniqueID + ":selMixedLinePos"]);
                }
                catch { }
                return ret;
            }
        }

        public bool ShowSeries
        {
            get { return (Request.Form[this.UniqueID + ":chkSeries"] == null ? false : true); }
        }

        public bool ShowCat
        {
            get { return (Request.Form[this.UniqueID + ":chkCat"] == null ? false : true); }
        }

        public bool ShowValues
        {
            get { return (Request.Form[this.UniqueID + ":chkValues"] == null ? false : true); }
        }

        public bool SetPercentByCategories
        {
            get { return (Request.Form[this.UniqueID + ":selNumOptions"] == "2" ? true : false); }
        }

        public bool SetScalingBySeries
        {
            get { return (Request.Form[this.UniqueID + ":selNumOptions"] == "1" ? true : false); }
        }

        public bool SetPivot
        {
            get { return (Request.Form[this.UniqueID + ":chkRotateAxes"] == null ? false : true); }
        }



        public void LoadGraph()
        {
            int t1 = System.Environment.TickCount;

            if (_report.State != Report.StateEnum.Executed)
                return;

            _chCtrl.BeginInit();

            bool rotateGraph = false;
            bool pivotAxes = ((_report.GraphOptions & OlapReport.GraphOptionsEnum.Pivot) > 0);

            int seriesPosCount = (pivotAxes ? _report.Cellset.Axis1PosCount : _report.Cellset.Axis0PosCount);
            int seriesMemCount = (pivotAxes ? _report.Cellset.Axis1TupleMemCount : _report.Cellset.Axis0TupleMemCount);
            int catPosCount = (pivotAxes ? _report.Cellset.Axis0PosCount : _report.Cellset.Axis1PosCount);
            int catMemCount = (pivotAxes ? _report.Cellset.Axis0TupleMemCount : _report.Cellset.Axis1TupleMemCount);
            if (seriesPosCount == 0 || catPosCount == 0)
                return;

            Size size = new Size(_report.GraphWidth, _report.GraphHeight);
            size = this.AdjustGraphWidth(size, _report.GraphType);

            // limit number of series            
            if (seriesPosCount > __MaxSeriesCount)
                seriesPosCount = __MaxSeriesCount;
            if (_report.GraphType == OlapReport.GraphTypeEnum.Pie || _report.GraphType == OlapReport.GraphTypeEnum.PieBrokenApart)
                if (seriesPosCount > __MaxPieCount)
                    seriesPosCount = __MaxPieCount;

            // limit number of categories
            if (catPosCount > __MaxCategoriesCount)
                catPosCount = __MaxCategoriesCount;

            bool showValues = (_report.GraphOptions & OlapReport.GraphOptionsEnum.ShowValues) > 0;
            bool showSeries = (_report.GraphOptions & OlapReport.GraphOptionsEnum.ShowSeries) > 0;
            bool showCats = (_report.GraphOptions & OlapReport.GraphOptionsEnum.ShowCategories) > 0;
            bool setScaling = _report.GraphType != OlapReport.GraphTypeEnum.Pie && ((_report.GraphOptions & OlapReport.GraphOptionsEnum.ScalingBySeries) > 0);
            bool setPerc = !setScaling && ((_report.GraphOptions & OlapReport.GraphOptionsEnum.PercentByCategories) > 0);
            byte graphPieColumns = (_report.GraphPieColumns == 0 ?
                Convert.ToByte(Math.Sqrt(seriesPosCount)) : _report.GraphPieColumns);
            byte graphMixedLinePos = _report.GraphMixedLinePosition;
            graphMixedLinePos = (graphMixedLinePos <= 0 || graphMixedLinePos > seriesPosCount ? (byte)1 : graphMixedLinePos);

            // theme
            if (_report.GraphTheme != null && _report.GraphTheme != "")
                _chCtrl.AppearanceName = _report.GraphTheme;

            // create series
            for (int i = 0; i < seriesPosCount; i++)
            {
                string name = "";
                for (int j = 0; j < seriesMemCount; j++)
                    name += (j == 0 ? "" : " | ") + (pivotAxes ? _report.Cellset.GetCellsetMember(1, j, i).Name : _report.Cellset.GetCellsetMember(0, j, i).Name);

                Series series = new Series();
                series.Name = name;


                // type				
                if (_report.GraphType == OlapReport.GraphTypeEnum.Pie || _report.GraphType == OlapReport.GraphTypeEnum.PieBrokenApart)
                {
                    // if pie, each of series is displayed as individual graph
                    if (_report.GraphType == OlapReport.GraphTypeEnum.PieBrokenApart)
                        _chCtrl.Series.Clear();
                    _chCtrl.Series.Add(series);

                    series.ChangeView(ViewType.Pie);

                    PiePointOptions ppo = (PiePointOptions)series.PointOptions;
                    PieSeriesView psw = (PieSeriesView)series.View;
                    PieSeriesLabel psl = (PieSeriesLabel)series.Label;

                    psl.Position = PieSeriesLabelPosition.TwoColumns;
                    series.PointOptions.PointView = PointView.Undefined;

                    if (_report.GraphType == OlapReport.GraphTypeEnum.Pie)
                    {
                        SimpleDiagram sd = new SimpleDiagram();
                        sd.LayoutDirection = LayoutDirection.Horizontal;
                        sd.Dimension = graphPieColumns;
                        _chCtrl.Diagram = sd;
                    }

                    // legend
                    if (showCats)
                        _chCtrl.Legend.Visible = false;

                    series.Label.OverlappingOptions.ResolveOverlapping = true;
                    series.LegendPointOptions.PointView = PointView.Argument;

                    _chCtrl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                    _chCtrl.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                    _chCtrl.Legend.Direction = LegendDirection.LeftToRight;


                    // series title
                    SeriesTitle sTitle = new SeriesTitle();
                    sTitle.Text = name;
                    sTitle.Alignment = StringAlignment.Center;
                    sTitle.Dock = ChartTitleDockStyle.Bottom;
                    psw.Titles.Add(sTitle);
                }
                else
                {
                    // diagram
                    if (_chCtrl.Diagram == null)
                        _chCtrl.Diagram = new XYDiagram();
                    XYDiagram diag = (XYDiagram)_chCtrl.Diagram;


                    //// panes
                    //if (_report.GraphType == OlapReport.GraphTypeEnum.LineWithBars && graphMixedLinePos <= seriesPosCount && diag.Panes.Count == 0)
                    //{
                    //    // add new pane
                    //    XYDiagramPane pane = new XYDiagramPane();
                    //    pane.Visible = true;
                    //    pane.SizeMode = PaneSizeMode.UseWeight;
                    //    pane.Weight = 0.66;
                    //    diag.PaneLayoutDirection = PaneLayoutDirection.Vertical;
                    //    diag.Panes.Add(pane);

                    //    // set default pane weight
                    //    diag.DefaultPane.SizeMode = PaneSizeMode.UseWeight;
                    //    diag.DefaultPane.Weight = 0.33;
                    //}

                    // add series
                    _chCtrl.Series.Add(series);

                    // add series to secondary pane if needed (in case of LineWithBars)
                    if (diag.Panes.Count > 0 && i != graphMixedLinePos - 1)
                        ((XYDiagramSeriesViewBase)series.View).Pane = diag.Panes[0];

                    if (_report.GraphType == OlapReport.GraphTypeEnum.BarVertical)
                    {
                        series.ChangeView(ViewType.Bar);
                    }
                    else if (_report.GraphType == OlapReport.GraphTypeEnum.BarHorizontal)
                    {
                        series.ChangeView(ViewType.Bar);
                        rotateGraph = true;
                    }
                    else if (_report.GraphType == OlapReport.GraphTypeEnum.StackedBarVertical)
                        series.ChangeView(ViewType.StackedBar);
                    else if (_report.GraphType == OlapReport.GraphTypeEnum.StackedBarHorizontal)
                    {
                        series.ChangeView(ViewType.StackedBar);
                        rotateGraph = true;
                    }
                    else if (_report.GraphType == OlapReport.GraphTypeEnum.LineHorizontal)
                        series.ChangeView(ViewType.Line);
                    else if (_report.GraphType == OlapReport.GraphTypeEnum.LineWithBars)
                    {
                        if (i == graphMixedLinePos - 1)
                        {
                            //if (!setScaling)
                            //{
                            //    // create secondary axis
                            //    SecondaryAxisY axisY = new SecondaryAxisY(series.Name);
                            //    axisY.Visible = true;
                            //    axisY.Title.Text = series.Name;
                            //    axisY.Title.Visible = true;
                            //    axisY.Alignment = AxisAlignment.Near;
                            //    ((XYDiagram)_chCtrl.Diagram).SecondaryAxesY.Add(axisY);
                            //    ((XYDiagramSeriesViewBase)series.View).AxisY = axisY;

                            //    // set default axis title
                            //    ((XYDiagram)_chCtrl.Diagram).AxisY.Title.Text = "Bar axis";
                            //    ((XYDiagram)_chCtrl.Diagram).AxisY.Title.Visible = true;
                            //}

                            // change view
                            series.ChangeView(ViewType.Line);
                        }
                        else
                            series.ChangeView(ViewType.Bar);
                    }

                    // 20 pixels per label (250 pixels for legend), othervise overlapping resolution will never finish
                    if ((size.Width - 250) / catPosCount > 20)
                    {
                        series.Label.OverlappingOptions.ResolveOverlapping = true;
                        PointOverlappingOptions poo = series.Label.OverlappingOptions as PointOverlappingOptions;
                        if (poo != null)
                            poo.AttractToMarker = true;
                    }

                    series.LegendText = name;
                    series.ShowInLegend = true;
                    series.ValueScaleType = ScaleType.Numerical;
                    series.PointOptions.PointView = PointView.Undefined;
                    series.LegendPointOptions.PointView = PointView.SeriesName;
                    series.Visible = true;

                    // labels orientation
                    diag.Rotated = rotateGraph;
                    if (rotateGraph)
                    {
                        diag.AxisY.Label.Antialiasing = true;
                        diag.AxisY.Label.Angle = 315;
                    }
                    else
                    {
                        diag.AxisX.Label.Antialiasing = true;
                        diag.AxisX.Label.Angle = 315;
                    }

                    // if scaling
                    if (setScaling)
                        diag.AxisY.Visible = false;
                }


                // prepare scaling ranges                
                double scalingMin = double.MaxValue;
                double scalingMax = double.MinValue;
                if (setScaling)
                {
                    for (int l = 0; l < catPosCount; l++)
                    {
                        string val = (pivotAxes ? _report.Cellset.GetCell(l, i).Value : _report.Cellset.GetCell(i, l).Value);
                        double dVal = 0;
                        double.TryParse(val, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dVal);
                        if (dVal < scalingMin)
                            scalingMin = dVal;
                        if (dVal > scalingMax)
                            scalingMax = dVal;
                    }
                }
                scalingMin = scalingMin - (scalingMax - scalingMin) * 0.1;

                // set data
                double percSum = 0;
                for (int l = 0; l < catPosCount; l++)
                {
                    string argument = "";
                    for (int m = 0; m < catMemCount; m++)
                        argument += (m == 0 ? "" : " | ") + (pivotAxes ? _report.Cellset.GetCellsetMember(0, m, l).Name : _report.Cellset.GetCellsetMember(1, m, l).Name);

                    string val = (pivotAxes ? _report.Cellset.GetCell(l, i).Value : _report.Cellset.GetCell(i, l).Value);

                    double dVal = 0;
                    double.TryParse(val, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dVal);
                    if (setPerc)
                        percSum += dVal;
                    else if (setScaling)
                        dVal = (scalingMax - scalingMin == 0 ? 0 : (dVal - scalingMin) / (scalingMax - scalingMin));

                    string fVal = (pivotAxes ? _report.Cellset.GetCell(l, i).FormattedValue : _report.Cellset.GetCell(i, l).FormattedValue);

                    SeriesPoint sp = new SeriesPoint(argument, new double[] { dVal });
                    sp.Tag = fVal;
                    series.Points.Add(sp);
                }

                // set custom labels and value as percentage
                for (int l = 0; l < catPosCount; l++)
                {
                    SeriesPoint sp = series.Points[l];
                    if (setPerc)
                    {
                        sp.Values[0] = sp.Values[0] / percSum;
                        sp.Tag = sp.Values[0].ToString("P");
                    }

                    // custom point label
                    string customLabel = string.Empty;
                    if (!showValues && !showSeries && !showCats)
                        series.Label.Visible = false;
                    else
                    {
                        string fVal = (string)sp.Tag;
                        if (fVal == null || fVal == "")
                            fVal = "0";
                        customLabel = (showSeries ? series.Name : string.Empty);
                        customLabel += (showCats ? (customLabel != string.Empty ? " | " : string.Empty) + sp.Argument : string.Empty);
                        customLabel += (showValues ? (customLabel != string.Empty ? ": " : string.Empty) + fVal : string.Empty);
                    }

                    series.Points[l].Tag = customLabel;
                }

                // for pie, layout each of series individually                                
                if (_report.GraphType == OlapReport.GraphTypeEnum.PieBrokenApart)
                {
                    _chCtrl.Titles.Clear();
                    _chCtrl.EndInit();
                    LayoutGraph(_report.ID.ToString() + "." + i.ToString(), size, graphPieColumns);

                    // init next chart
                    if (i < seriesPosCount - 1)
                    {
                        _chCtrl = new DevExpress.XtraCharts.Web.WebChartControl();
                        _chCtrl.BeginInit();
                    }
                }
            }


            int t2 = System.Environment.TickCount;

            if (_report.GraphType != OlapReport.GraphTypeEnum.PieBrokenApart)
            {
                ChartTitle title = new ChartTitle();
                title.Alignment = StringAlignment.Center;
                title.Lines = new string[] { _report.Name, _report.Description };
                _chCtrl.Titles.Add(title);

                LayoutGraph(_report.ID.ToString(), size, 0);
            }

            // if LineWithBars
            if (_report.GraphType == OlapReport.GraphTypeEnum.LineWithBars)
            {
                // fill combo
                for (int i = 0; i < _chCtrl.Series.Count; i++)
                    this.selMixedLinePos.Items.Add(new ListItem(_chCtrl.Series[i].Name, (i + 1).ToString()));
                this.selMixedLinePos.SelectedIndex = graphMixedLinePos - 1;

                // set line as last
                if (!(_chCtrl.Series[_chCtrl.Series.Count - 1].View is LineSeriesView))
                {
                    // roll down through series
                    for (int i = graphMixedLinePos - 1; i < _chCtrl.Series.Count - 1; i++)
                        _chCtrl.Series.Swap(i, i + 1);
                }
            }

            int t3 = System.Environment.TickCount;
            double time1 = (t3 - t2) / 1000.0;
            double time2 = (t2 - t1) / 1000.0;
            t1 = 0;
        }


        void LayoutGraph(string id, Size size, int createHostTableColumns)
        {
            // set image host control
            System.Web.UI.Control hostControl = this.cellGraph;
            if (createHostTableColumns > 0)
            {
                // create table, row and cell if needed
                HtmlTable tbl = (this.cellGraph.Controls.Count == 0 ? null : this.cellGraph.Controls[0]) as HtmlTable;
                if (tbl == null)
                {
                    tbl = new HtmlTable();
                    tbl.Border = 0;
                    tbl.CellSpacing = 0;
                    tbl.CellPadding = 0;
                    this.cellGraph.Controls.Add(tbl);
                }

                HtmlTableRow row = (tbl.Rows.Count == 0 ? null : tbl.Rows[tbl.Rows.Count - 1]);
                if (row == null || row.Cells.Count == createHostTableColumns)
                {
                    row = new HtmlTableRow();
                    tbl.Rows.Add(row);
                }

                HtmlTableCell cell = new HtmlTableCell();
                row.Cells.Add(cell);
                hostControl = cell;
            }


            string imgNamePrefix = _report.GetType().Name + "." + id;

            ////delete older images of same report
            //string[] filePaths = System.IO.Directory.GetFiles(FI.Common.AppConfig.TempDir, imgNamePrefix + "*.PNG");
            //if (filePaths != null && filePaths.Length > 0)
            //    for (int j = 0; j < filePaths.Length; j++)
            //    {
            //        try
            //        {
            //            System.IO.File.Delete(filePaths[j]);
            //        }
            //        catch (Exception exc)
            //        {
            //            //do nothing
            //            exc = null;
            //        }
            //    }


            ////write to file and display, it will overwite itself if needed
            //string imgName = imgNamePrefix + "." + DateTime.Now.ToString("yyyyMMddHHssfff") + ".PNG";
            //string imgVirtPath = Request.ApplicationPath + "/" + FI.Common.AppConfig.TempVirtualDir + "/" + imgName;
            //string imgPhysPath = FI.Common.AppConfig.TempDir + @"\" + imgName;

            // custom draw labels
            if (_chCtrl.Series.Count > 0 && _chCtrl.Series[0].Label.Visible)
                _chCtrl.CustomDrawSeriesPoint += new CustomDrawSeriesPointEventHandler(_chCtrl_CustomDrawSeriesPoint);

            // export
            _chCtrl.Width = size.Width;
            _chCtrl.Height = size.Height;
            _chCtrl.BorderOptions.Thickness = 1;
            _chCtrl.BorderOptions.Color = Color.LightGray;
            _chCtrl.EnableViewState = false;
            _chCtrl.EnableCallBacks = false;
            _chCtrl.SaveStateOnCallbacks = false;
            _chCtrl.EnableClientSideAPI = false;      

            //System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
            //img.ImageUrl = imgVirtPath;
            //hostControl.Controls.Add(img) ;	           

            hostControl.Controls.Add(_chCtrl);
        }

        void _chCtrl_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            string customLabel = (string)e.SeriesPoint.Tag;
            if (customLabel != string.Empty)
                e.LabelText = customLabel;
            if (_report.GraphType == OlapReport.GraphTypeEnum.Pie)
                if (_chCtrl.Series.Count > 0 && _chCtrl.Series[0] != e.Series)
                    e.LegendText = "";
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

        }
        #endregion
    }
}
