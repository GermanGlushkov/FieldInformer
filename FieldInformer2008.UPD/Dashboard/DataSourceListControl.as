package
{
	import flash.geom.*;
	import fl.containers.*;
	import fl.core.*;
	import fl.controls.*;
	import fl.events.*;
	import flash.display.*;
	import flash.net.*;
	import flash.events.*;
	import flash.display.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.*;
	import fl.controls.dataGridClasses.*;
	import fl.controls.DataGrid;
	import fl.data.DataProvider;
	import flash.filters.BlurFilter;
	
	public class DataSourceListControl extends ModalSprite
	{
		var _serviceUrl:String=null;
		var _userId:String=null;
		
		var _xml:XML;
		var _selectedId:String=null;
		
		function DataSourceListControl(serviceUrl:String, userId:String)
		{			
			_serviceUrl=serviceUrl;
			_userId=userId;
			
			//addEventListener(Event.ADDED_TO_STAGE, onAddedToStage);
			btnOk.addEventListener(MouseEvent.CLICK, onOk);
			btnCancel.addEventListener(MouseEvent.CLICK, onCancel);
			
			_grid.doubleClickEnabled=true;
			_grid.addEventListener(ListEvent.ITEM_CLICK, gridItemSelected)
			_grid.addEventListener(ListEvent.ITEM_DOUBLE_CLICK, onOk)
		}
		
		
		
		override public function get width():Number
		{
			return 350;
		}
		
		override public function get height():Number
		{
			return 235;
		}
		
		public function setDataSourceList(xml:XML)
		{
			_xml=xml;
		}
		
		override public function show(stageRef:Stage)
		{
			showReportsGrid();
			super.show(stageRef);
		}		
		
		
		function showReportsGrid()
		{						
			btnOk.enabled=false;
			if(_xml==null)
				return;
				
			var col1:DataGridColumn = new DataGridColumn("ID");
			col1.visible=false;			
			var col2:DataGridColumn = new DataGridColumn("NAME");
			col2.headerText = "Name";			
			col2.sortDescending=false;
			var col3:DataGridColumn = new DataGridColumn("DESCR");
			col3.headerText = "Description";			
			_grid.columns = [col1, col2, col3]; // DataGrid column Array
			
			var dp:DataProvider = new DataProvider(_xml);
			_grid.dataProvider=dp;
			_grid.sortItemsOn("NAME");
		}								
		
		private function gridItemSelected(e:ListEvent)
		{
			_selectedId=e.item["ID"];
			btnOk.enabled=true;
        }
		
		function onOk(evt:Event)
		{
			this.hide();
			if(_selectedId!=null)
				this.dispatchEvent(new Event("DataSourceSelected", true));
		}
		
		function onCancel(evt:Event)
		{			
			this.hide();
		}
		
		public function get SelectedId():String
		{
    		return _selectedId;
		}
		
	}
}