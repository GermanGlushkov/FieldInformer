package
{
	import flash.geom.*;
	import fl.containers.*;
	import fl.core.*;
	import flash.display.*;
	import flash.net.*;
	import flash.events.*;
	import flash.display.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.*;
	
		
	public class DGaugePropertyPanel extends ModalSprite
	{
		var _serviceUrl:String=""; //"http://localhost/FieldINformer2008/UI.Web/WebServices/DashboardService.aspx";
		var _userId:String=""; //15394";
		var _gaugeId:String=""; //"c4e158c4-1c7a-4795-9cff-7a1a544c44a9";
		
		var _xmlLoader:URLLoader = new URLLoader();
		var _xmlSaver:URLLoader = new URLLoader();
		var _xml:XML=null;
		
		function DGaugePropertyPanel(serviceUrl:String, userId:String, gaugeId:String)
		{						
			_serviceUrl=serviceUrl;
			_userId=userId;
			_gaugeId=gaugeId;
			
			btnOk.addEventListener(MouseEvent.CLICK, onOk);
			btnCancel.addEventListener(MouseEvent.CLICK, onCancel);
			
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadXML);
			_xmlSaver.addEventListener(Event.COMPLETE, onSaveXML);
						
			executeUrlRequest();
		}
		
		override public function get width():Number
		{
			return 200;
		}
		
		override public function get height():Number
		{
			return 254;
		}
		
		
		function executeUrlRequest()
		{
			// xml request
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = "<COMMAND TYPE='GetUserGaugeConfig' USERID='" + _userId + "'>" + 
				"<GAUGE ID='" + _gaugeId + "' QUERYDEF='1' QUERYRESULT='1'/>" + 
			"</COMMAND>";
		
			_xmlLoader.load(req);
		}
		
		function onLoadXML (e:Event)
		{	
			try
			{
				_xml = new XML(e.target.data);				
				onLoadConfig();			
			}
			catch(exc:Error)
			{
				trace(exc.message);
			}
			
		}
		
		function onLoadConfig()		
		{
			txtName.text=_xml..GAUGE[0].@NAME;
			//numRefresh.value=Math.ceil(Number(_xml..GAUGE[0].@REFRESH)/60);
			
			//var n:NumericStepper=numMin as NumericStepper;
			//var dvc:DynamicValueControl =dynMin as DynamicValueControl;
			dynRefresh.initStatic(String(Number(_xml..GAUGE[0].@REFRESH)/60), 0, NaN);
			
			dynMin.initDynamic(_serviceUrl, _userId, "MIN", _xml..GAUGE[0]);
			dynVal1.initDynamic(_serviceUrl, _userId, "VAL1", _xml..GAUGE[0]);
			dynVal2.initDynamic(_serviceUrl, _userId, "VAL2", _xml..GAUGE[0]);
			dynMax.initDynamic(_serviceUrl, _userId, "MAX", _xml..GAUGE[0]);
			dynValDispl.initDynamic(_serviceUrl, _userId, "DISPVAL", _xml..GAUGE[0]);
			//numMin.value=Number(_xml..GAUGE[0].VAL.(@ID='MIN'));			
			//numMax.text="[QUERY]";
			
		}
		
		function saveConfig()
		{
			// xml request
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			
			var reqData:XML=<COMMAND TYPE='SaveUserGaugeConfig'><GAUGE/></COMMAND>;
			reqData.@USERID=_userId;
			reqData.GAUGE[0].@ID=_gaugeId;
			reqData.GAUGE[0].@NAME=txtName.text;
			reqData.GAUGE[0].@REFRESH=String(dynRefresh.getStaticValue()*60);
			reqData.GAUGE[0].appendChild(new XML(dynMin.getDynamicValueXml(true))); 
			reqData.GAUGE[0].appendChild(new XML(dynVal1.getDynamicValueXml(true))); 
			reqData.GAUGE[0].appendChild(new XML(dynVal2.getDynamicValueXml(true))); 
			reqData.GAUGE[0].appendChild(new XML(dynMax.getDynamicValueXml(true))); 
			reqData.GAUGE[0].appendChild(new XML(dynValDispl.getDynamicValueXml(true))); 
			trace(reqData);
			req.data=reqData.toXMLString();			
			_xmlSaver.load(req);
		}		
		
		function onSaveXML (e:Event)
		{				
			this.hide();
			dispatchEvent(new Event("ConfigSaved", true));
		}
		
		function onOk(evt:MouseEvent)
		{
			saveConfig();
		}
		
		function onCancel(evt:MouseEvent)
		{
			this.hide();
		}
	}
}