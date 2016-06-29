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
	
	
	
	public class TMeter extends Sprite
	{
		
		var _serviceUrl:String=""; //"http://localhost/FieldINformer2008/UI.Web/WebServices/DashboardService.aspx";
		var _userId:String=""; //15394";
		var _gaugeId:String=""; //"c4e158c4-1c7a-4795-9cff-7a1a544c44a9";
		
		var gName:String="TMeter";
		var gWidth:int;
		var gHeight:int;
		var gRefresh:int;
		var val:Number;
		var maxVal:Number;
		var minVal:Number;
		var valCaption:String="N/A";
		var _xml:XML;
		
		var _xmlLoader:URLLoader = new URLLoader();
		var _toolTip=null;
		var intervalId:int;
		
		var _propPanel:TMeterPropertyPanel=null;

		function TMeter()
		{			
			title_txt.selectable=false;
			txtHigh.selectable=false;
			txtMid.selectable=false; 
			txtZero.selectable=false;
			
			// some events
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadXML);
			this.addEventListener(MouseEvent.MOUSE_MOVE, showToolTip);
			this.addEventListener(MouseEvent.MOUSE_OUT, hideToolTip);				
		}				
		
		/*
		function onInit(evt:Event)
		{
			// get userId and gaugeId from URL
			try
			{
				var vars:URLVariables =new URLVariables(stage.loaderInfo.url);
				_userId=vars.userId;
				_gaugeId=vars.gaugeId;
				_serviceUrl=vars.serviceUrl;
				//title_txt.text=_userId + "," + _gaugeId  + "," + _serviceUrl;
			}
			catch(exc:Error)
			{
				trace(exc.message);
				//title_txt.text=exc.message;
			}
			
			init(_userId, _gaugeId, _serviceUrl);			
		}
		*/
			
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
		
		/*
		function initControls()
		{
			var numFmt = new TextFormat();
			numFmt.align = "left";
			numFmt.bold = true;
			numFmt.color = 0x000000;	
			
			var titleFmt = new TextFormat();
			titleFmt.align = "center";
			titleFmt.bold = true;
			titleFmt.color = 0xffffff;	
			
			title_txt.defaultTextFormat=titleFmt;	
			txtMid.defaultTextFormat=numFmt;
			txtHigh.defaultTextFormat=numFmt;
			txtZero.defaultTextFormat=numFmt;	
		}
		*/
		
		function loadData()
		{	
			gName=_xml..GAUGE[0].@NAME;
			gWidth=Number(_xml..GAUGE[0].@WIDTH);	
			gHeight=Number(_xml..GAUGE[0].@HEIGHT);
			gRefresh=0;
			gRefresh=Number(_xml..GAUGE[0].@REFRESH)*1000;
			val  = Number(getXmlValue("VAL"));
			maxVal  = Number(getXmlValue("MAX"));
			minVal  = Number(getXmlValue("MIN"));
			valCaption=String(val);			
			
			//trace(val);
			//trace(maxVal);
			//trace(minVal);
		}
		
		function getXmlValue(id:String):String
		{			
			var valEl:XMLList=_xml..GAUGE[0].VAL.(@ID==id);
			if(valEl.length()==0)
				return null;
			if(valEl[0].@TYPE=="QUERY")
			{
				var str:String=valEl.QUERY[0].RESULT[0];			
				if(str!=null)
				{
					var re:RegExp= /%/;
					str=str.replace(re, "");
					var re1:RegExp= /,/;
					str=str.replace(re1, "");
					var re2:RegExp= / /;
					str=str.replace(re2, "");
				}
				trace(str);
				return str;
			}
			
			return valEl;				
		}
		
		function setData()
		{		
			title_txt.text = gName;		
			
			// Math and stuffs
			var barLength:Number = 172;
			var barValue:Number = 0;
			var min:Number=minVal;
			var max:Number=maxVal;
			
			if(min>max)			
				min=val;
			
			if(val<=minVal)
				barValue=0;
			else if(val>=max)
				barValue=barLength;
			else
				barValue = (val-min)*(barLength/(max-min));
				
			/*
			trace(minVal);
			trace(val);
			trace(maxVal);
			trace(barValue);
			*/
		
			//Ease the bar so it doesn't look crappy.
			var bar:Number = mercury_mc.height;
			var do_tween:Object = new Tween(mercury_mc, "height", Regular.easeOut, bar, barValue, 1, true); 
			do_tween.start();
			
			// Set upper display value...
			txtHigh.text = String(maxVal);
			txtMid.text  = ""; 
			if(maxVal>minVal)
				txtMid.text  = String(minVal+(maxVal-minVal)/2.0);
			else if(maxVal==minVal)
				txtMid.text  =String(minVal);
			txtZero.text = String(minVal);
			
			// set interval
			clearInterval(intervalId);
			if(gRefresh>0)
				intervalId=setInterval(doRefresh, gRefresh);
		}
		
		
		function showToolTip(e:Event)
		{			
			if(_toolTip==null)
			{
				_toolTip=new toolTip_mc();
				/*
				_toolTip.txt_tooltip.autoSize="left";
				_toolTip.txt_tooltip.border=false;
				//_toolTip.txt_tooltip.borderColor=0x000000;
				_toolTip.txt_tooltip.textColor=0x000000;
				_toolTip.txt_tooltip.background=true;
				_toolTip.txt_tooltip.backgroundColor=0xffffff;
				*/
				_toolTip.txt_tooltip.defaultTextFormat=txtMid.defaultTextFormat;
				_toolTip.txt_tooltip.background=true;
				_toolTip.txt_tooltip.backgroundColor=0xffffff;
				_toolTip.txt_tooltip.autoSize="left";
				_toolTip.txt_tooltip.border=true;
				_toolTip.txt_tooltip.borderColor=0xcccccc;
				addChild(_toolTip);
			}
			
			_toolTip.txt_tooltip.text = valCaption;
			_toolTip.visible=true;
			_toolTip.x = this.mouseX+3;
			_toolTip.y = this.mouseY-_toolTip.txt_tooltip.height+3;
			_toolTip.txt_tooltip.selectable=false;
			
			var ds:DropShadowFilter = new DropShadowFilter(5, 45, 0x666666, 1, 7, 7, 0.7, 1, false, false, false);
			_toolTip.filters = new Array(ds);
		}
		
		function hideToolTip(e:Event)
		{
			if(_toolTip!=null)
				_toolTip.visible=false;
		}

		public function GetPropertyPanel():TMeterPropertyPanel
		{
			if(_propPanel!=null)			
				_propPanel.removeEventListener("ConfigSaved", onConfigSaved);
				
			_propPanel=new TMeterPropertyPanel(_serviceUrl, _userId, _gaugeId);
			_propPanel.addEventListener("ConfigSaved", onConfigSaved);

			return _propPanel;
		}
		
		function onConfigSaved(evt:Event)
		{
			doRefresh();
		}

	}
}