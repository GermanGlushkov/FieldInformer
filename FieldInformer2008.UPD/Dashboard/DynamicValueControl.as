package
{
	import flash.geom.*;
	import fl.containers.*;
	import fl.core.*;
	import fl.controls.*;
	import flash.display.*;
	import flash.net.*;
	import flash.events.*;
	import flash.display.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.*;
	
	public class DynamicValueControl extends Sprite
	{
		var _val:Number=NaN;
		var _min:Number=NaN;
		var _max:Number=NaN;
		var _query:String=null;
		var _prevText:String="";
		
		var _serviceUrl:String=null;
		var _userId:String=null;
		
		var _dynId:String=null;
		var _queryXml:XML=null;
		
		var _dynQuery:DynamicQueryControl=null;
		//var _txt:TextInput=new TextInput();
		//var _propRect:Sprite=new Sprite();
		
		public function DynamicValueControl()
		{			
			this.scaleY=1;
			this.scaleX=1;			
			
			txt.addEventListener(MouseEvent.MOUSE_DOWN, onTxtMouseDown);
			txt.addEventListener(KeyboardEvent.KEY_UP, onTxtKeyUp);
			//addEventListener(Event.ADDED_TO_STAGE, onAddedToStage);
			
			propBtn.doubleClickEnabled = true;
			propBtn.addEventListener(MouseEvent.DOUBLE_CLICK, onPropDoubleClick);
		}		
		
		
		function onPropDoubleClick(evt:MouseEvent)
		{
			if(_dynQuery==null)
			{
				_dynQuery=new DynamicQueryControl();
				_dynQuery.addEventListener("QueryChanged", onQueryXmlChanged);
			}
			_dynQuery.init(_serviceUrl, _userId, _queryXml);
			_dynQuery.show(stage);
		}
		
		function onTxtMouseDown(evt:MouseEvent)
		{
			if(isNaN(Number(txt.text)))
				txt.text="";
		}
		
		function onTxtKeyUp(evt:KeyboardEvent)
		{
			setStaticValue(txt.text);
		}		
		
		public function initDynamic(serviceUrl:String, userId:String, dynId:String, parentXml:XML)
		{			
			_serviceUrl=serviceUrl;
			_userId=userId;
			_dynId=dynId;			
			
			_val=NaN;
			_min=NaN;
			_max=NaN;
			
			var dynXml:XML=null;
			try
			{
				if(parentXml!=null)
					dynXml=parentXml..VAL.(@ID==dynId)[0];
				setDynamicValue(dynXml);			
			}
			catch(exc:Error)
			{
			}
		}
		 
		public function initStatic(val:String, min:Number, max:Number)
		{			
			_serviceUrl=null;
			_userId=null;
			
			propBtn.visible=false;
			txt.width=placeholder.width;
			
			if(min>max)
				min=max;
			_min=min;
			_max=max;
			setStaticValue(val);			
		}				
		
		function setDynamicValue(valXml:XML)
		{						
			if(valXml==null || (valXml.name()=="VAL" && valXml.attribute("TYPE")!="QUERY"))
				setStaticValue(valXml);
			else
			{
				try
				{
					if(valXml.name()=="QUERY")
						_queryXml=valXml;
					else
						_queryXml=valXml.QUERY[0];
					txt.text= "Q:" + String(_queryXml.RESULT[0]);
				}
				catch(exc:Error)
				{
					txt.text="N/A";
				}
			}
		}
		
		function setStaticValue(val:String)
		{
			_queryXml=null;
			
			var newVal:Number=Number(val);
			if(txt.text!="-" && txt.text!="" && (isNaN(newVal) || newVal<_min || newVal>_max))
			{
				txt.text=String(_prevText);
			}
			else
			{							
				if(txt.text!=val)
					txt.text=val;
				_prevText=val;
				if(val=="")
					_val=NaN;
				else
					_val=newVal;
			}
		}
		
		public function isStaticValue():Boolean
		{
			if(_queryXml==null)
				return true;
			return false;
		}
		
		public function getStaticValue():Number
		{
			if(isNaN(_val))
			{
				if(_min>0)
					return _min;
				return 0;			
			}
			else
				return _val;
		}
		
		public function getStaticValueString():String
		{
			trace(_val);
			if(isNaN(_val))
				return "";			
			else
				return String(_val);
		}
		
		function onQueryXmlChanged(evt:Event)
		{
			setDynamicValue(_dynQuery.getQueryXml());
		}
		
		public function getDynamicValueXml(removeResult:Boolean):String
		{					
			if(_queryXml==null)
				return "<VAL ID='" + _dynId + "' TYPE='STATIC'>" + String(getStaticValueString())  + "</VAL>";
			else
			{
				trace("_dynId:" + _queryXml.toXMLString());
				if(removeResult)
					delete _queryXml.RESULT;
				return "<VAL ID='" + _dynId + "' TYPE='QUERY'>" + _queryXml.toXMLString()  + "</VAL>";
			}
		}		
		
	}
}