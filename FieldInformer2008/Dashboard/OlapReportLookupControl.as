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
	
	public class OlapReportLookupControl extends MovieClip
	{
		var _serviceUrl:String=null;
		var _userId:String=null;
		
		var _queryXml:XML=null;
		var _queryValid:Boolean=false;
		
		var _xmlLoader:URLLoader = new URLLoader();
		var _xml:XML=null;
		var _dsList:DataSourceListControl=null;
		var _cstControl:CellsetControl=null;
								
		var _dataSourceId:String="";
		var _dataSourceIdValid:Boolean=false;
		var _name:String="";
		var _descr:String="";
		var _queryResult:String="";				
		var _queryChanged:Boolean=false;
			
		function OlapReportLookupControl()
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
		
		public function init(serviceUrl:String, userId:String, queryXml:XML)
		{
			_serviceUrl=serviceUrl;
			_userId=userId;
			_queryXml=queryXml;			
			
			_queryChanged=false;
			_queryValid=false;
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
			if(_xml==null)
				return;
			if(_dsList==null)
			{
				_dsList=new DataSourceListControl(_serviceUrl, _userId);
				_dsList.addEventListener("DataSourceSelected", onDataSourceSelected);
				_dsList.setDataSourceList(_xml);				
			}
			_dsList.show(stage);				
		}										
		
		function showLookup(evt:MouseEvent)
		{						
			if(!_dataSourceIdValid)
				return;
			if(_cstControl==null)
			{
				_cstControl=new CellsetControl(_serviceUrl, _userId);
				_cstControl.addEventListener("CellSelected", onLookupSelected);
			}
			_cstControl.init(_queryXml);
			_cstControl.show(stage);				
			
			//onLookupSelected(evt);
		}										
		
		function onLoadXml(evt:Event)
		{			
			_xml=new XML(evt.target.data);
			refreshData();
			refreshControls();			
		}
		
		function refreshData()
		{			
			_queryValid=false;
			_dataSourceIdValid=false;
			try
			{
				_dataSourceId=_queryXml.@DATASOURCEID;			
				_dataSourceId=_xml..DATASOURCE.(@ID==_dataSourceId).@ID;
				if(_dataSourceId!=null && _dataSourceId!="")					
				{
					_dataSourceIdValid=true;				
					_name=_xml..DATASOURCE.(@ID==_dataSourceId).@NAME;
					_descr=_xml..DATASOURCE.(@ID==_dataSourceId).@DESCR;
					
					if("RESULT" in _queryXml)					
					{
						_queryValid=true;
						_queryResult=_queryXml.RESULT[0];				
					}
				}				
			}
			catch(exc:Error)
			{
			}			
			
			if(!_dataSourceIdValid)
			{
				_name="N/A";
				_descr="N/A";
			}
			if(!_queryValid)
				_queryResult="N/A";
		}
		
		function refreshControls()
		{								
			txtName.text=_name;
			txtDescr.text=_descr;
			txtVal.text=_queryResult;
			
			btnOk.enabled=isQueryChanged();
			btnLookup.visible=_dataSourceIdValid;
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
		
		function onDataSourceSelected(evt:Event)
		{						
			trace(_dataSourceId, _dsList.SelectedId);
			if(_dataSourceId!=_dsList.SelectedId)
			{
				_queryChanged=true;
				_queryXml=new XML("<QUERY USERID='" + _userId + "' DATASOURCE='OLAP' DATASOURCEID='" + _dsList.SelectedId + "'></QUERY>");				
			}
			refreshData();
			refreshControls();
		}				
		
		function onLookupSelected(evt:Event)
		{								
			_queryChanged=true;
			_queryXml=_cstControl.getQueryXml();
			trace(_queryXml);
			
			refreshData();
			refreshControls();
		}				
		
		public function isQueryChanged():Boolean
		{
			return (_queryChanged && _queryValid);
		}

		public function getQueryXml():XML
		{
			if(!_queryValid)
				return null;
			return _queryXml;
		}
		
	}
}