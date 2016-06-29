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
	
	public class OlapReportMultiLookupControl extends MovieClip
	{
		var _serviceUrl:String=null;
		var _userId:String=null;
		var _valuesXml:XML=null;
		var _dataSourcesXml:XML=null;
		
		var _xmlLoader:URLLoader = new URLLoader();
		
		var _dsList:DataSourceListControl=null;
		var _cstControl:CellsetMultiselectControl=null;
								
		var _dataSourceId:String="";
		var _dataSourceIdValid:Boolean=false;
		var _name:String="";
		var _descr:String="";
		var _queriesValid:Boolean=false;
		var _queriesCount:int=0;				
		var _isChanged:Boolean=false;
			
		function OlapReportMultiLookupControl()
		{			
			
			
			//addEventListener(Event.ADDED_TO_STAGE, onAddedToStage);
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadXml);
			
			btnReport.doubleClickEnabled=true;
			btnReport.addEventListener(MouseEvent.DOUBLE_CLICK, showReportsList);			
			
			btnLookup.doubleClickEnabled=true;
			btnLookup.addEventListener(MouseEvent.DOUBLE_CLICK, showLookup);			
			
			btnOk.addEventListener(MouseEvent.CLICK, onOk);
			btnCancel.addEventListener(MouseEvent.CLICK, onCancel);			
			
		}
		
		
		
		override public function get width():Number
		{
			return 270;
		}
		
		override public function get height():Number
		{
			return 120;
		}
		
		public function init(serviceUrl:String, userId:String, valuesXml:XML)
		{
			_serviceUrl=serviceUrl;
			_userId=userId;
			_valuesXml=valuesXml;			
			
			_isChanged=false;
			_queriesValid=false;
			_dataSourceId="";
			_dataSourceIdValid=false;
						
			// xml request
			btnOk.enabled=false;
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = "<COMMAND TYPE='GetUserDataSources' USERID='" + _userId + "'/>" ;
			_xmlLoader.load(req);						
			
		}		
				
		function showReportsList(ev:MouseEvent)
		{						
			if(_dataSourcesXml==null)
				return;
			if(_dsList==null)
			{
				_dsList=new DataSourceListControl(_serviceUrl, _userId);
				_dsList.addEventListener("DataSourceSelected", onDataSourceSelected);
				_dsList.setDataSourceList(_dataSourcesXml);				
			}
			_dsList.show(stage);				
		}										
		
		function showLookup(evt:MouseEvent)
		{						
			if(!_dataSourceIdValid)
				return;
			if(_cstControl==null)
			{
				_cstControl=new CellsetMultiselectControl(_serviceUrl, _userId);
				_cstControl.addEventListener("CellSelected", onLookupSelected);
			}
			_cstControl.init(_valuesXml, _dataSourceId);
			_cstControl.show(stage);				
			
			//onLookupSelected(evt);
		}										
		
		function onLoadXml(evt:Event)
		{			
			_dataSourcesXml=new XML(evt.target.data);
			refreshData();
			refreshControls();			
		}
		
		function refreshData()
		{			
			_queriesValid=false;
			_dataSourceIdValid=false;			
			_queriesCount=0;
			
			try
			{
				trace(_valuesXml);
				if(_valuesXml!=null)
					_queriesCount=_valuesXml.QUERIES.QUERY.length();
					
				// if ds id not set, take it from first query
				if((_dataSourceId==null || _dataSourceId=="") && _queriesCount>0)
					_dataSourceId=_valuesXml.QUERIES.QUERY[0].@DATASOURCEID;	
					
				// validate against ds list
				_dataSourceId=_dataSourcesXml..DATASOURCE.(@ID==_dataSourceId).@ID;			
				if(_dataSourceId!=null && _dataSourceId!="")					
				{
						_dataSourceIdValid=true;				
						_name=_dataSourcesXml..DATASOURCE.(@ID==_dataSourceId).@NAME;
						_descr=_dataSourcesXml..DATASOURCE.(@ID==_dataSourceId).@DESCR;
				}
				
				// check query results
				var count:int=0;
				for (var i:int = 0; i<_queriesCount; i++)
				{
					if(_valuesXml.QUERIES.QUERY[0].@DATASOURCEID==_dataSourceId
						&& ("RESULT" in _valuesXml.QUERIES.QUERY[i]))
						count++;
					else
						break;
											
				}
				if(count>0 && count==_queriesCount)
					_queriesValid=true;
			}
			catch(exc:Error)
			{
				trace(exc);
			}							
			
			if(!_dataSourceIdValid)
			{
				_name="N/A";
				_descr="N/A";
			}
			if(!_queriesValid)
				_queriesCount=0;
		}
		
		function refreshControls()
		{								
			txtName.text=_name;
			txtDescr.text=_descr;
			txtVal.text=String(_queriesCount);
			
			btnOk.enabled=isQueryChanged();
			btnLookup.visible=_dataSourceIdValid;
		}
		
		function onOk(evt:MouseEvent)
		{			
			if(this.isQueryChanged())				
				this.dispatchEvent(new Event("QueryChanged", false));			
			this.dispatchEvent(new Event("CloseRequest", false));			
		}
		
		function onCancel(evt:MouseEvent)
		{			
			_queriesValid=false;
			this.dispatchEvent(new Event("CloseRequest", false));			
		}
		
		function onDataSourceSelected(evt:Event)
		{						
			if(_dataSourceId!=_dsList.SelectedId)			
			{
				_dataSourceId=_dsList.SelectedId;
				_valuesXml=<VALUES/>;
				_isChanged=true;				
			}
			refreshData();			
			refreshControls();			
						
			if(_isChanged)			
				showLookup(new MouseEvent(""));			
		}				
		
		function onLookupSelected(evt:Event)
		{								
			_isChanged=true;
			_valuesXml=_cstControl.getValuesXml();
			refreshData();
			refreshControls();
		}				
		
		public function isQueryChanged():Boolean
		{
			return (_isChanged && _queriesValid);
		}

		public function getValuesXml():XML
		{
			if(!_queriesValid)
				return null;
			return _valuesXml;
		}
		
	}
}