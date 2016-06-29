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
	import flash.text.*;
	
	
	
	
	public class DGauge extends Sprite
	{
		
		var _serviceUrl:String=""; //"http://localhost/FieldINformer2008/UI.Web/WebServices/DashboardService.aspx";
		var _userId:String=""; //15394";
		var _gaugeId:String=""; //"c4e158c4-1c7a-4795-9cff-7a1a544c44a9";
		
		var gName:String="DGauge";
		var gWidth:int;
		var gHeight:int;
		var gRefresh:int;
		var val1:Number;
		var val2:Number;
		var dispVal:String;
		var maxVal:Number;
		var minVal:Number;
		var val1Caption:String="N/A";
		var val2Caption:String="N/A";
		var _xml:XML;
		
		var _xmlLoader:URLLoader = new URLLoader();
		var _toolTip=null;
		var intervalId:int;
		
		var _propPanel:DGaugePropertyPanel=null;

		function DGauge()
		{			
			title_txt.selectable=false;
			display_txt.selectable=false;
			txt_TopVal.selectable=false;
			txt_BottomVal.selectable=false;
			txt_RightVal.selectable=false;
			txt_LeftVal.selectable=false;
			
			// some events
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadXML);
			this.addEventListener(MouseEvent.MOUSE_MOVE, showToolTip);
			this.addEventListener(MouseEvent.MOUSE_OUT, hideToolTip);				
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
			dispVal  = getXmlValue("DISPVAL");
			maxVal  = Number(getXmlValue("MAX"));
			minVal  = Number(getXmlValue("MIN"));
			
			val1Caption=getXmlValue("VAL1");			
			val2Caption=getXmlValue("VAL2");			
			
			val1  = NaN; 
			if(val1Caption!=null && val1Caption!="")
				val1=Number(val1Caption);
				
			val2  = NaN; 
			if(val2Caption!=null && val2Caption!="")
				val2=Number(val2Caption);
		}
		
		function getXmlValue(id:String):String
		{			
			var valEl:XMLList=_xml..GAUGE[0].VAL.(@ID==id);
			if(valEl.length()==0)
				return "";
			if(valEl[0].@TYPE=="QUERY")
			{
				var str:String=valEl[0].QUERY[0].RESULT[0];			
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
			title_txt.text=gName;						
			display_txt.text = dispVal;
			txt_TopVal.text="";
			txt_BottomVal.text="";
			txt_RightVal.text="";
			txt_LeftVal.text="";
			hour_mc.rotation=0;
			mc_histinstance.rotation=0;
			
			if(minVal>maxVal)			
			{
				if(!isNaN(val1) && val1<maxVal)
					minVal=val1;
				if(!isNaN(val2) && val2<maxVal && (isNaN(val1) || val2<val1))
					minVal=val2;
				else
					minVal=maxVal;
			}
			
			if(!isNaN(val1))
			{
				if(val1<=minVal)
					val1=minVal;
				else if(val1>=maxVal)
					val1=maxVal;
			}
			
			if(!isNaN(val2))
			{
				if(val2<=minVal)
					val2=minVal;
				else if(val2>=maxVal)
					val2=maxVal;
			}
				
			txt_TopVal.text    = String(minVal);
			if(maxVal>minVal)
			{				
				txt_BottomVal.text =  String(minVal+(maxVal-minVal)/2);
				txt_RightVal.text   =  String(minVal+(maxVal-minVal)/4);
				txt_LeftVal.text  =  String(minVal+(maxVal-minVal)*3/4);
			}							
			
			if(!isNaN(val1) && maxVal>minVal)
			{
				hour_mc.visible=true;
				var degree = (360/(maxVal-minVal))*(val1-minVal);
				var nhour  = hour_mc.rotation;
				var do_Tween:Object = new Tween(hour_mc,"rotation", Regular.easeOut,nhour,degree,1,true); 
				do_Tween.start();
			}
			else
				hour_mc.visible=false;
			
			if(!isNaN(val2) && maxVal>minVal)
			{
				mc_histinstance.visible=true;
				var degree = (360/(maxVal-minVal))*(val2-minVal);
				var nhour  = mc_histinstance.rotation;
				var do_Tween:Object = new Tween(mc_histinstance,"rotation", Regular.easeOut,nhour,degree,1,true); 
				do_Tween.start();
			}			
			else
				mc_histinstance.visible=false;
						
			// set interval
			clearInterval(intervalId);
			if(gRefresh>0)
				intervalId=setInterval(doRefresh, gRefresh);					
		}
		
		
		function showToolTip(e:Event)
		{			
			var txt:String="";
			if(val1Caption!="")			
				txt+="ARROW 1: " + val1Caption + "\r";
			if(val2Caption!="")
				txt+="ARROW 2: " + val2Caption + "\r";
			if(dispVal!="")
				txt+="DISPLAY: " + dispVal + "\r";			
			
			if(txt=="")
				return;
			
			if(_toolTip==null)
			{
				_toolTip=new toolTip_mc();
				
				var tf:TextFormat=txt_LeftVal.defaultTextFormat;
				tf.align="left"; 				
				_toolTip.txt_tooltip.defaultTextFormat=tf;
				
				_toolTip.txt_tooltip.background=true;
				_toolTip.txt_tooltip.backgroundColor=0xffffff;
				_toolTip.txt_tooltip.autoSize="left";
				_toolTip.txt_tooltip.border=true;
				_toolTip.txt_tooltip.borderColor=0xcccccc;
				addChild(_toolTip);
			}
			
			_toolTip.txt_tooltip.text = txt;
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

		public function GetPropertyPanel():DGaugePropertyPanel
		{
			if(_propPanel!=null)			
				_propPanel.removeEventListener("ConfigSaved", onConfigSaved);
				
			_propPanel=new DGaugePropertyPanel(_serviceUrl, _userId, _gaugeId);
			_propPanel.addEventListener("ConfigSaved", onConfigSaved);

			return _propPanel;
		}
		
		function onConfigSaved(evt:Event)
		{
			doRefresh();
		}

	}
}