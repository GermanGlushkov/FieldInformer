package
{
	import flash.geom.*;
	import fl.containers.*;
	import flash.display.*;
	import flash.net.*;
	import flash.events.*;
	import flash.display.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.*;
	import flash.filters.*;
	import fl.controls.*;
	import flash.text.TextFormat;
    import fl.controls.CheckBox;
	
	import com.yahoo.astra.fl.charts.BarChart;
	import com.yahoo.astra.fl.charts.axes.NumericAxis;
	import com.yahoo.astra.fl.charts.legend.Legend;
	import com.yahoo.astra.fl.charts.series.Series;
	import com.yahoo.astra.fl.charts.series.StackedBarSeries;
	import com.yahoo.astra.fl.charts.series.BarSeries;
	import com.yahoo.astra.fl.charts.series.LineSeries;
	
	public class Chart extends Sprite
	{
		
		var _serviceUrl:String=""; //"http://localhost/FieldINformer2008/UI.Web/WebServices/DashboardService.aspx";
		var _userId:String=""; //15394";
		var _gaugeId:String=""; //"c4e158c4-1c7a-4795-9cff-7a1a544c44a9";
		
		var gName:String="Chart";
		var gWidth:int;
		var gHeight:int;
		var gRefresh:int;
		var gShowLegend:Boolean;
		var _xml:XML;
		
		var _xmlLoader:URLLoader = new URLLoader();
		var _toolTip=null;
		var intervalId:int;
		
		var _propPanel:ChartPropertyPanel=null;

		function Chart()
		{								
			_title.selectable=false;
			
			// some events
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadXML);
		}				
		
			
		public function init(userId:String, gaugeId:String, serviceUrl:String)
		{		
			_userId=userId;
			_gaugeId=gaugeId;
			_serviceUrl=serviceUrl;
			
			// request
			ExecuteUrlRequest();		
		}
		
		
		function ExecuteUrlRequest()
		{
			// xml request
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = "<COMMAND TYPE='GetUserGaugeConfig' USERID='" + _userId + "'>" + 
				"<GAUGE ID='" + _gaugeId + "' QUERYDEF='0' QUERYRESULT='1'/>" + 
			"</COMMAND>";
		
			_xmlLoader.load(req);
		}
		
		function onLoadXML (e:Event)
		{	
			try
			{
				_xml = new XML(e.target.data);				
				loadData();
				setData();			
			}
			catch(exc:Error)
			{
				trace(exc.message);
			}
			
		}
		
		public function doRefresh()
		{	
			// request
			try
			{
				ExecuteUrlRequest();					
			}
			catch(exc:Error)
			{
				trace(exc.message);
			}
		}
		
		
		function loadData()
		{	
			gName=_xml..GAUGE[0].@NAME;
			gWidth=Number(_xml..GAUGE[0].@WIDTH);	
			gHeight=Number(_xml..GAUGE[0].@HEIGHT);
			gRefresh=0;
			gRefresh=Number(_xml..GAUGE[0].@REFRESH)*1000;
			gShowLegend=(_xml..GAUGE[0].VALUES.@LEGEND=="1");
			
			//trace(maxVal);
			//trace(minVal);
		}
		
		function setData()
		{		
			// title
			_title.text=gName;
			
			// categories
			var catList:Array=new Array();
			for each(var catXml:XML in _xml..HEADER.(@TYPE=="CATEGORIES"))
				catList.push(catXml.@CAPTION);
							 
			// series
			var seriesList:Array=new Array();
			for each(var seriesXml:XML in _xml..HEADER.(@TYPE=="SERIES"))
			{
				var s:Series = new BarSeries();
				if(seriesXml.@CHARTTYPE=="Line")
				   s=new LineSeries();
				else if(seriesXml.@CHARTTYPE=="Stacked Bar")
				   s=new StackedBarSeries();
				s.displayName = seriesXml.@CAPTION;
								
				var data:Array=new Array();
				var rowNo:String=seriesXml.@POS;
				for each(var catXml:XML in _xml..HEADER.(@TYPE=="CATEGORIES"))
				{
					var foundRsultXml:XML=null;
					for each(var resultXml:XML in _xml..RESULT)
					{
						foundRsultXml=resultXml;
						var queryXml:XML=resultXml.parent();
						for each(var seriesLookup:XML in seriesXml.LOOKUP)
						{
							if(queryXml.LOOKUP.(@UN==seriesLookup.@UN).length()<=0)
							{
								foundRsultXml=null;
								break;
							}
						}
						
						if(foundRsultXml!=null)
						{
							for each(var catLookup:XML in catXml.LOOKUP)
							{
								if(queryXml.LOOKUP.(@UN==catLookup.@UN).length()<=0)
								{
									foundRsultXml=null;
									break;
								}							
							}
						}
							
						if(foundRsultXml!=null)
							break;
					}					
					
					if(foundRsultXml!=null)
						data.push(getXmlValue(foundRsultXml));
					else
						data.push(NaN);				
				}				
				
				s.dataProvider = data;
				seriesList.push(s);
			}
			
							 
			// chart and legend
			_chart.setStyle("textFormat", new TextFormat("_sans", 9, 0x000000, false));
			_chart.dataProvider = seriesList;
			_chart.categoryNames = catList;			
			
			_legend.visible=gShowLegend;
			_legend.setStyle("direction", "horizontal");			
			_legend.setStyle("textFormat", new TextFormat("_sans", 9, 0x000000, false));
			//_legend.setStyle("contentPadding", 0);						
			_chart.legend = _legend;
			
			// set interval
			clearInterval(intervalId);
			if(gRefresh>0)
				intervalId=setInterval(doRefresh, gRefresh);
		}
		
		
		function getXmlValue(resultXml:XML):Number
		{			
			var str:String=resultXml;			
			trace(str);
			if(str!=null)
			{
				var re:RegExp= /%/;
				str=str.replace(re, "");
				var re1:RegExp= /,/;
				str=str.replace(re1, "");
				var re2:RegExp= / /;
				str=str.replace(re2, "");
			}
			return Number(str);
		}
		

		public function GetPropertyPanel():ChartPropertyPanel
		{
			if(_propPanel!=null)			
				_propPanel.removeEventListener("ConfigSaved", onConfigSaved);
				
			_propPanel=new ChartPropertyPanel(_serviceUrl, _userId, _gaugeId);
			_propPanel.addEventListener("ConfigSaved", onConfigSaved);

			return _propPanel;
		}
		
		function onConfigSaved(evt:Event)
		{
			doRefresh();
		}

	}
}