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
	
	public class DynamicMultiQueryControl extends ModalSprite
	{
		var _serviceUrl:String=null;
		var _userId:String=null;		
		var _valuesXml:XML=null;
		
		function DynamicMultiQueryControl()
		{									
			//_cmbQueryType.addEventListener(Event.CHANGE, onQueryTypeChange);
			_olapRptCtrl.addEventListener("CloseRequest", onCloseRequest);			
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
		
		
		public function init(serviceUrl:String, userId:String, valuesXml:XML)
		{
			trace(valuesXml);
			_serviceUrl=serviceUrl;
			_userId=userId;
			_valuesXml=valuesXml;			
			if(_valuesXml==null)
				_valuesXml=new XML("<VALUES/>");				
			
			refreshControls();
		}		
		
		/*
		function onQueryTypeChange(evt:Event)
		{
			var type:String=_cmbQueryType.selectedItem.data;
			if(type!=_valuesXml.@DATASOURCE)
				_valuesXml=new XML("<QUERY USERID='" + _userId + "' DATASOURCE='" + type + "'/>");							
			refreshControls();
		}
		*/
		
		function refreshControls()
		{			
			_olapRptCtrl.x=0;
			_olapRptCtrl.y=32;
			_olapRptCtrl.init(_serviceUrl, _userId, _valuesXml);

			/*
			if(_olapRptCtrl.parent!=null)
				removeChild(_olapRptCtrl);
				
			if(_valuesXml.@DATASOURCE=="OLAP")
			{
				_olapRptCtrl.init(_serviceUrl, _userId, _valuesXml);
				addChild(_olapRptCtrl);
			}
			
			selectComboBoxItem(_valuesXml.@DATASOURCE);			
			*/
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
			if(_olapRptCtrl.isQueryChanged())
			{
				_valuesXml=_olapRptCtrl.getValuesXml();
				this.dispatchEvent(new Event("QueryChanged", false));			
			}
			this.hide();
		}		
		
		public function getValuesXml():XML
		{
			return _valuesXml;
		}
		
		
	}
}