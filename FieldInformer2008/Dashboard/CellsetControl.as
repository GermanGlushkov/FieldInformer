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
	import flash.text.*;
	import flash.xml.*;
	import flash.filters.*;
		
	public class CellsetControl extends ModalSprite
	{			
		var _serviceUrl:String=null;
		var _userId:String=null;
		var _queryXml:XML=null;
		
		var _cellset:XML;
		var _xmlLoader:URLLoader = new URLLoader();
		
		var _rowHdrRows:int=0;
		var _rowHdrCols:int=0;
		var _colHdrRows:int=0;
		var _colHdrCols:int=0;
		
		static var CellWidth:int=100;
		static var CellHeight:int=18;
		
		var _toolTip:toolTip_mc=null;
				
		
		function CellsetControl(serviceUrl:String, userId:String)
		{						
			_serviceUrl=serviceUrl;
			_userId=userId;
			
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadXml);
			
			//addEventListener(Event.ADDED_TO_STAGE, onAddedToStage);
			btnOk.addEventListener(MouseEvent.CLICK, onOk);
			btnCancel.addEventListener(MouseEvent.CLICK, onCancel);
			
			_grid.doubleClickEnabled=true;
			_grid.addEventListener(ListEvent.ITEM_CLICK, gridItemSelected);
			_grid.addEventListener(ListEvent.ITEM_DOUBLE_CLICK, onOk);
			_grid.addEventListener(ListEvent.ITEM_ROLL_OVER, gridItemOver)
			
		}				
		
		override public function get width():Number
		{
			return 600;
		}
		
		override public function get height():Number
		{
			return 530;
		}
		
		override public function show(stageRef:Stage)
		{
			requestCellset();
			super.show(stageRef);
		}		
		
		public function init(queryXml:XML)
		{
			_queryXml=queryXml;			
		}
		
		function requestCellset()
		{
			if(_queryXml==null)
				return;
			btnOk.enabled=false;
			_progress.visible=true;
			_progress.invalidate();
			_grid.visible=false;
			_grid.removeAll();
			
			trace(_queryXml.toXMLString());
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = "<COMMAND TYPE='GetDataTable' USERID='" + _userId + "' DATASOURCE='OLAP' DATASOURCEID='" + _queryXml.@DATASOURCEID + "'/>" ;
			_xmlLoader.load(req);			
		}
		
		function onLoadXml(evt:Event)
		{			
			try
			{
				_cellset=new XML(evt.target.data);
			}
			catch(exc:Error)
			{
				_cellset=null;
				trace(exc);
			}
			
			buildGrid();
		}
		
		function buildGrid()
		{								
			hideToolTip();
			var g:DataGrid=_grid;
			_scroll.source=null;
			g.removeAllColumns();
			if(_cellset==null)			
				return;																			
			
			g.showHeaders=true;
			g.headerHeight=0;
			g.rowHeight=CellHeight;
			g.minColumnWidth=CellWidth;
			g.resizableColumns=false;
			
			try
			{
				_colHdrRows=int(_cellset..COLHDR[0].@ROWS);
				_colHdrCols=int(_cellset..COLHDR[0].@COLS);
				_rowHdrRows=int(_cellset..ROWHDR[0].@ROWS);
				_rowHdrCols=int(_cellset..ROWHDR[0].@COLS);
				
				CellsetCellRenderer.RowHeaders=_rowHdrCols;
				CellsetCellRenderer.ColumnHeaders=_colHdrRows;					
				CellsetCellRenderer.SelectedRow=-1;
				CellsetCellRenderer.SelectedColumn=-1;
			
				var col:String=_queryXml..RESULT.@COL;
				var row:String=_queryXml..RESULT.@ROW;					
				if(col!=null && col!="")
					CellsetCellRenderer.SelectedColumn=int(col)+CellsetCellRenderer.RowHeaders;										
				if(row!=null && row!="")
					CellsetCellRenderer.SelectedRow=int(row)+CellsetCellRenderer.ColumnHeaders;
				trace(CellsetCellRenderer.SelectedRow, CellsetCellRenderer.SelectedColumn);
				
				var i:int, j:int;
				for(i=0;i<(_rowHdrCols+_colHdrCols);i++)
				{
					var c:DataGridColumn=new DataGridColumn(String(i));
					c.width=CellWidth;
					c.minWidth=CellWidth;
					g.addColumn(c);
				}			
				
				var values:Array = null;
				for(i=0;i<_colHdrRows;i++)
				{
					values = new Array();
					for(j=0;j<_rowHdrCols;j++)
						values[String(j)] = "";
					for(j=0;j<_colHdrCols;j++)
						values[String(_rowHdrCols+j)] = _cellset..COLHDR[0].R[i].C[j].@N;
					g.addItem(values);
				}			
				for(i=0;i<_rowHdrRows;i++)
				{
					values = new Array();
					for(j=0;j<_rowHdrCols;j++)
						values[String(j)] =  _cellset..ROWHDR[0].R[i].C[j].@N;
					for(j=0;j<_colHdrCols;j++)
						values[String(_rowHdrCols+j)] =  _cellset..CELLS[0].R[i].C[j];
					g.addItem(values);
				}
				
				
				//var ccr:CellsetCellRenderer = new CellsetCellRenderer();
				
				g.setStyle("cellRenderer", CellsetCellRenderer);
				g.editable=false;													
				g.horizontalScrollPolicy = ScrollPolicy.OFF;
				g.verticalScrollPolicy = ScrollPolicy.OFF;
				
				g.x=0;
				g.y=0;
				g.width=(_rowHdrCols+_colHdrCols)*CellWidth;
				g.height=(_colHdrRows+_rowHdrRows)*CellHeight;
				_scroll.source=_grid;
				
				_progress.visible=false;
				g.visible=true;
			}
			catch(exc:Error)
			{
				_progress.visible=false;
				trace(exc);
			}
		}										
		
		private function gridItemSelected(e:ListEvent)
		{
			var col:int=int(e.columnIndex);
			var row:int=int(e.rowIndex);
			selectItem(row, col, false);
			gridItemOver(e);
        }
		
		private function selectItem(row:int, col:int, dispatchEvt:Boolean)
		{
			if(col>=_rowHdrCols && row>=_colHdrRows)
			{
				if(CellsetCellRenderer.SelectedColumn!=col || CellsetCellRenderer.SelectedRow!=row)
				{
					CellsetCellRenderer.SelectedColumn=col;
					CellsetCellRenderer.SelectedRow=row;
					_grid.invalidate();				
					btnOk.enabled=true;
				}
				
				if(dispatchEvt)								
					this.dispatchEvent(new Event("CellSelected", true));
			}
		}
		
		private function gridItemOver(e:ListEvent)
		{
			var col:int=int(e.columnIndex);
			var row:int=int(e.rowIndex);
			if(col==CellsetCellRenderer.SelectedColumn && row==CellsetCellRenderer.SelectedRow)
			{
				var lookup:String = "";
				var i:int=0;
				for(i=0;i<_colHdrRows;i++)
				{
					if(i>0)
						lookup+="\r";
					lookup+=String(_grid.getItemAt(i)[String(col)]);
				}
				for(i=0;i<_rowHdrCols;i++)
					lookup+="\r" + String(_grid.getItemAt(row)[String(i)]);
																					
				showToolTip(lookup)
			}
			else if(row<_colHdrRows && col>=_rowHdrCols)
			{
				var cellVal:String = String(_grid.getItemAt(row)[String(col)]);	
				showToolTip(cellVal); //_cellset..COLHDR[0].R[row].C[col-_rowHdrCols].@N
			}
			else if(col<_rowHdrCols  && row>=_colHdrRows)			
			{
				var cellVal:String = String(_grid.getItemAt(row)[String(col)]);	
				showToolTip(cellVal);
			}
			else
				hideToolTip();
        }
		
		public function getQueryXml():XML
		{
			var row:int=CellsetCellRenderer.SelectedRow;
			var col:int=CellsetCellRenderer.SelectedColumn;
			if(col>=_rowHdrCols && row>=_colHdrRows)
			{
				_queryXml=new XML("<QUERY USERID='" + _userId + "' DATASOURCE='OLAP' DATASOURCEID='" + _queryXml.@DATASOURCEID + "'/>");
				var i:int=0;
				for(i=0;i<_colHdrRows;i++)
				{
					var child:XML=<LOOKUP/>;
					child.@UN=_cellset..COLHDR[0].R[i].C[col-_rowHdrCols].@UN;
					_queryXml.appendChild(child);
				}
				for(i=0;i<_rowHdrCols;i++)
				{
					var child:XML=<LOOKUP/>;
					child.@UN=_cellset..ROWHDR[0].R[row-_colHdrRows].C[i].@UN;
					_queryXml.appendChild(child);
				}
					
				_queryXml.RESULT.@ROW=row;
				_queryXml.RESULT.@COL=col;
				_queryXml.RESULT=_cellset..CELLS[0].R[row-_colHdrRows].C[col-_rowHdrCols].text();
			}
			
			return _queryXml;
		}
		
		function showToolTip(msg:String)
		{
			this.useHandCursor = true;	
			
			if(_toolTip==null)
			{
				var fmt = new TextFormat();
				fmt.align = "left";
				fmt.bold = true;
				fmt.color = 0x000000;	
			
				_toolTip=new toolTip_mc();
				_toolTip.txt_tooltip.defaultTextFormat=fmt;
				_toolTip.txt_tooltip.background=true;
				_toolTip.txt_tooltip.backgroundColor=0xffffff;
				_toolTip.txt_tooltip.autoSize="left";
				_toolTip.txt_tooltip.border=true;
				_toolTip.txt_tooltip.borderColor=0xcccccc;
				addChild(_toolTip);
			}
			
			_toolTip.txt_tooltip.text = msg;
			_toolTip.visible=true;
			_toolTip.x = this.mouseX+3;
			_toolTip.y = this.mouseY-_toolTip.txt_tooltip.height+3;
			_toolTip.txt_tooltip.selectable=false;
			
			var ds:DropShadowFilter = new DropShadowFilter(5, 45, 0x666666, 1, 7, 7, 0.7, 1, false, false, false);
			_toolTip.filters = new Array(ds);
		}
		
		function hideToolTip()
		{
			if(_toolTip!=null)
				_toolTip.visible=false;
		}
		
		function onOk(evt:Event)
		{			
			selectItem(CellsetCellRenderer.SelectedRow, CellsetCellRenderer.SelectedColumn, true);			
			this.hide();
		}
		
		function onCancel(evt:Event)
		{			
			this.hide();
		}
		
		
	}
}