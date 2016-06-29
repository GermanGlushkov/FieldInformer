package
{
	import flash.geom.*;
	import fl.containers.*;
	import fl.core.*;
	import fl.controls.*;
	import flash.display.*;
	import flash.net.*;
	import flash.events.*;
	import fl.events.*;
	import flash.display.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.*;
	import fl.controls.dataGridClasses.*;
	import fl.controls.DataGrid;
	import flash.filters.BlurFilter;
	import flash.xml.*;
	
	public class DynamicQueryControl extends ModalSprite
	{
		var _serviceUrl:String=null;
		var _userId:String=null;		
		var _queryXml:XML=null;
		
		function DynamicQueryControl()
		{									
			_cmbQueryType.addEventListener(Event.CHANGE, onQueryTypeChange);
			_olapRptCtrl.addEventListener("CloseRequest", onCloseRequest);			
			_sqlCmdCtrl.addEventListener("CloseRequest", onCloseRequest);			
		}				
		
		/*
		override public function get width():Number
		{
			return 270.50;
		}
		
		override public function get height():Number
		{
			return 152;
		}
		*/
		
		
		public function init(serviceUrl:String, userId:String, queryXml:XML)
		{
			_serviceUrl=serviceUrl;
			_userId=userId;
			_queryXml=queryXml;			
			if(_queryXml==null)
				_queryXml=new XML("<QUERY USERID='" + _userId + "' DATASOURCE='OLAP'/>");				
			
			refreshControls();
		}		
		
		function onQueryTypeChange(evt:Event)
		{
			var type:String=_cmbQueryType.selectedItem.data;
			if(type!=_queryXml.@DATASOURCE)
				_queryXml=new XML("<QUERY USERID='" + _userId + "' DATASOURCE='" + type + "'/>");							
			refreshControls();
		}
		
		function refreshControls()
		{			
			_olapRptCtrl.x=0;
			_olapRptCtrl.y=32;
			if(_olapRptCtrl.parent!=null)
				removeChild(_olapRptCtrl);
				
			_sqlCmdCtrl.x=0;
			_sqlCmdCtrl.y=32;
			if(_sqlCmdCtrl.parent!=null)
				removeChild(_sqlCmdCtrl);
				
			if(_queryXml.@DATASOURCE=="OLAP")
			{
				_olapRptCtrl.init(_serviceUrl, _userId, _queryXml);
				addChild(_olapRptCtrl);
			}
			else if(_queryXml.@DATASOURCE=="SQLSCALAR")
			{
				_sqlCmdCtrl.init(_serviceUrl, _userId, _queryXml);
				addChild(_sqlCmdCtrl);
			}
			
			selectComboBoxItem(_queryXml.@DATASOURCE);
		}
		
		private function selectComboBoxItem (dataString:String) 
		{
			for (var i = 0; i < _cmbQueryType.length; i++) 
			{
				if (_cmbQueryType.getItemAt(i).data.toString() == dataString) 
				{
					if(_cmbQueryType.selectedIndex!=i)
						_cmbQueryType.selectedIndex=i;
					return;
				}
			}
		}

		function onCloseRequest(evt:Event)
		{
			this.hide();
		}
		
		public function getQueryXml():XML
		{
			if(_olapRptCtrl.parent!=null)
				return _olapRptCtrl.getQueryXml();
			else if(_sqlCmdCtrl.parent!=null)
				return _sqlCmdCtrl.getQueryXml();
			else 
				return null;
		}
		
		
	}
}