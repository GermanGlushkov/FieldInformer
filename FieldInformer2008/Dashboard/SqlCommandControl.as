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
	import fl.controls.dataGridClasses.*;
	import fl.controls.DataGrid;
	import flash.filters.BlurFilter;
	import flash.xml.*;
	
	public class SqlCommandControl extends MovieClip
	{
		var _serviceUrl:String=null;
		var _userId:String=null;
		
		var _queryXml:XML=null;
		var _queryValid:Boolean=false;
		
		var _xmlLoader:URLLoader = new URLLoader();
		var _xml:XML=null;
		
		var _querySql:String="";				
		var _queryResult:String="";				
			
		function SqlCommandControl()
		{			
			
			//addEventListener(Event.ADDED_TO_STAGE, onAddedToStage);
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadExecuteXml);			
			
			btnExecute.addEventListener(MouseEvent.CLICK, onExecute);
			btnOk.addEventListener(MouseEvent.CLICK, onOk);
			btnCancel.addEventListener(MouseEvent.CLICK, onCancel);			
			
		}
		
		public function init(serviceUrl:String, userId:String, queryXml:XML)
		{
			_serviceUrl=serviceUrl;
			_userId=userId;
			_queryXml=queryXml;						
			
			refreshData();
			refreshControls();
			btnOk.enabled=false; // until execute
		}		
				
		
		
		function refreshData()
		{			
			_queryValid=false;
			_querySql="";
			_queryResult="";
			try
			{
				if("SQL" in _queryXml)					
				{
					_querySql=_queryXml.SQL[0];				
					
					if("RESULT" in _queryXml)					
					{
						_queryResult=_queryXml.RESULT[0];				
						if(!isNaN(Number(_queryResult)))
							_queryValid=true;
					}
				}								
				
				if("ERROR" in _queryXml)
					_querySql=_queryXml.ERROR[0];				
			}
			catch(exc:Error)
			{
			}									
			
			if(!_queryValid)
				_queryResult="N/A";
		}
		
		function refreshControls()
		{								
			txtQuery.text=_querySql;
			txtVal.text=_queryResult;
			
			btnOk.enabled=isQueryChanged();
			progress.visible=false;
		}
		
		function onExecute(evt:MouseEvent)
		{					
			_queryXml=new XML("<QUERY USERID='" + _userId + "' DATASOURCE='SQLSCALAR'><SQL></SQL></QUERY>");
			_queryXml.SQL=txtQuery.text;
			
			txtVal.text="";
			progress.visible=true;
			btnOk.enabled=false;
			
			// xml request
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = 
			"<COMMAND TYPE='ExecuteQueries' QUERYDEF='1'>" + 
    		_queryXml.toXMLString() + 
			"</COMMAND>"
			_xmlLoader.load(req);
		}
		
		function onLoadExecuteXml(evt:Event)
		{			
			progress.visible=false;
			
			_queryXml=new XML(evt.target.data);			
			
			refreshData();
			refreshControls();			
		}
		
		function onOk(evt:MouseEvent)
		{			
			if(this.isQueryChanged())				
				this.dispatchEvent(new Event("QueryChanged", true));			
			this.dispatchEvent(new Event("CloseRequest", true));			
		}
		
		function onCancel(evt:MouseEvent)
		{			
			_queryValid=false;
			this.dispatchEvent(new Event("CloseRequest", true));			
		}		
		
		public function isQueryChanged():Boolean
		{
			return (_queryValid);
		}

		public function getQueryXml():XML
		{
			if(!_queryValid)
				return null;
			return _queryXml;
		}
		
	}
}