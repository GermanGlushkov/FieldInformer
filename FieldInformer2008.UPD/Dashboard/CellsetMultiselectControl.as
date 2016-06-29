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
		
	public class CellsetMultiselectControl extends ModalSprite
	{			
		var _serviceUrl:String=null;
		var _userId:String=null;
		var _dataSourceId:String=null;
		var _valuesXml:XML=null;
		
		var _cellset:XML;
		var _xmlLoader:URLLoader = new URLLoader();
		
		var _rowHdrRows:int=0;
		var _rowHdrCols:int=0;
		var _colHdrRows:int=0;
		var _colHdrCols:int=0;
		
		static var CellWidth:int=100;
		static var CellHeight:int=18;
		
		var _toolTip:toolTip_mc=null;
		var _selectionChanged:Boolean=false;
		
		
		function CellsetMultiselectControl(serviceUrl:String, userId:String)
		{						
			_serviceUrl=serviceUrl;
			_userId=userId;
			
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadXml);
			
			//addEventListener(Event.ADDED_TO_STAGE, onAddedToStage);
			btnOk.addEventListener(MouseEvent.CLICK, onOk);
			btnCancel.addEventListener(MouseEvent.CLICK, onCancel);
			
			//_grid.doubleClickEnabled=true;
			//_grid.addEventListener(ListEvent.ITEM_DOUBLE_CLICK, onOk);
			_grid.addEventListener(ListEvent.ITEM_CLICK, gridItemSelected);
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
		
		public function init(valuesXml:XML, dataSourceId:String)
		{
			_valuesXml=valuesXml;			
			_dataSourceId=dataSourceId;
			_selectionChanged=false;
			
		}
		
		function requestCellset()
		{
			if(_valuesXml==null)
				return;
			btnOk.enabled=false;
			_progress.visible=true;
			_progress.invalidate();
			_grid.visible=false;
			_grid.removeAll();
			
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = "<COMMAND TYPE='GetDataTable' USERID='" + _userId + "' DATASOURCE='OLAP' DATASOURCEID='" + _dataSourceId + "'/>" ;
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
				
				// renderer
				CellsetMultiselectCellRenderer.Clear();
				CellsetMultiselectCellRenderer.RowHeaders=_rowHdrCols;
				CellsetMultiselectCellRenderer.ColumnHeaders=_colHdrRows;					
				for each(var resultXml:XML in _valuesXml..RESULT)
					CellsetMultiselectCellRenderer.addSelectedCell(resultXml.@ROW, resultXml.@COL, true); //with do checks
			
				var col:String=_valuesXml..RESULT.@COL;
				var row:String=_valuesXml..RESULT.@ROW;					
				
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
								
				
				g.setStyle("cellRenderer", CellsetMultiselectCellRenderer);
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
			selectItem(row, col);
			gridItemOver(e);
        }
		
		private function selectItem(row:int, col:int)
		{			
			CellsetMultiselectCellRenderer.changeSelectedCellState(row, col);
			_grid.invalidate();				
			btnOk.enabled=true;
			_selectionChanged=true;
		}
		
		private function gridItemOver(e:ListEvent)
		{			
			var col:int=int(e.columnIndex);
			var row:int=int(e.rowIndex);
			
			if(CellsetMultiselectCellRenderer.isPlaceholderHeader(row, col))
				hideToolTip();				
			else if(CellsetMultiselectCellRenderer.isHeader(row, col))
			{
				var cellVal:String = String(_grid.getItemAt(row)[String(col)]);	
				showToolTip(cellVal);
			}
			else if(CellsetMultiselectCellRenderer.isSelectedCell(row, col))
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
			else
				hideToolTip();
			
        }
		
		public function getValuesXml():XML
		{			
			if(_selectionChanged)
			{
				var prevXml:XML=_valuesXml;
				
				var row:int=-1;
				var col:int=-1;			
				_valuesXml=<VALUES><HEADERS></HEADERS><QUERIES/></VALUES>
				
				// queries
				for(var j:int=0;j<CellsetMultiselectCellRenderer.SelectedCells.length;j++)
				{
					var row:int=CellsetMultiselectCellRenderer.SelectedCells[j][0];
					var col:int=CellsetMultiselectCellRenderer.SelectedCells[j][1];
					
					// query element
					var queryXml=<QUERY DATASOURCE='OLAP'/>;
					queryXml.@USERID=_userId;
					queryXml.@DATASOURCEID=_dataSourceId;
					
					// lookups
					var i:int=0;
					for(i=0;i<_colHdrRows;i++)
					{
						var child:XML=<LOOKUP/>;
						child.@UN=_cellset..COLHDR[0].R[i].C[col-_rowHdrCols].@UN;
						queryXml.appendChild(child);
					}
					for(i=0;i<_rowHdrCols;i++)
					{
						var child:XML=<LOOKUP/>;
						child.@UN=_cellset..ROWHDR[0].R[row-_colHdrRows].C[i].@UN;
						queryXml.appendChild(child);
					}
					
					// result
					queryXml.RESULT.@ROW=row;
					queryXml.RESULT.@COL=col;
					queryXml.RESULT=_cellset..CELLS[0].R[row-_colHdrRows].C[col-_rowHdrCols].text();					
					
					_valuesXml.QUERIES[0].appendChild(queryXml);
				}			
								
				// col headers
				var colType:String=null;
				var selCols:Array=CellsetMultiselectCellRenderer.getSelectedColumns();
				for(j=0;j<selCols.length;j++)
				{
					var col:int=selCols[j];
					var hdr:XML=<HEADER/>;
					var defaultCaption:String="";
					for(i=0;i<_colHdrRows;i++)
					{
						// header un child
						var child:XML=<LOOKUP/>;
						child.@UN=_cellset..COLHDR[0].R[i].C[col-_rowHdrCols].@UN;
						hdr.appendChild(child);						
						
						// construct caption and type
						if(i>0)
							defaultCaption+="-";
						defaultCaption+=_cellset..COLHDR[0].R[i].C[col-_rowHdrCols].@N;
					}
					
					// lookup caption from prev xml
					var matchedPrevHdr:XML=null;
					if(prevXml!=null)
					{
						for each(var prevHdr:XML in prevXml..HEADER)
						{
							matchedPrevHdr=prevHdr; 
							for each(var lookup:XML in hdr.LOOKUP)
								if(prevHdr.LOOKUP.(@UN==lookup.@UN).length()<=0)
								{
									matchedPrevHdr=null;
									break; //next prev hdr
								}
							if(matchedPrevHdr!=null)
								break;
						}
					}
					
					// assign pos, type, caption
					hdr.@POS="COL";
					hdr.@CAPTION=defaultCaption;
					if(matchedPrevHdr!=null)
					{
						colType=matchedPrevHdr.@TYPE;
						hdr.@CAPTION=matchedPrevHdr.@CAPTION;
					}
					
					_valuesXml.HEADERS[0].appendChild(hdr);
				}
				
				// row headers
				var rowType:String=null;
				var selRows:Array=CellsetMultiselectCellRenderer.getSelectedRows();
				for(j=0;j<selRows.length;j++)
				{
					var row:int=selRows[j];
					var hdr:XML=<HEADER/>;
					var defaultCaption:String="";
					for(i=0;i<_rowHdrCols;i++)
					{
						// header un child
						var child:XML=<LOOKUP/>;
						child.@UN=_cellset..ROWHDR[0].R[row-_colHdrRows].C[i].@UN;
						hdr.appendChild(child);						
						
						// construct caption and type
						if(i>0)
							defaultCaption+="-";
						defaultCaption+=_cellset..ROWHDR[0].R[row-_colHdrRows].C[i].@N;
					}
					
					// lookup caption from prev xml
					var matchedPrevHdr:XML=null;
					if(prevXml!=null)
					{
						for each(var prevHdr:XML in prevXml..HEADER)
						{
							matchedPrevHdr=prevHdr; 
							for each(var lookup:XML in hdr.LOOKUP)
								if(prevHdr.LOOKUP.(@UN==lookup.@UN).length()<=0)
								{
									matchedPrevHdr=null;
									break; //next prev hdr
								}
							if(matchedPrevHdr!=null)
								break;
						}
					}
					
					// assign pos, type, caption
					hdr.@POS="ROW";
					hdr.@CAPTION=defaultCaption;
					if(matchedPrevHdr!=null)
					{
						rowType=matchedPrevHdr.@TYPE;
						hdr.@CAPTION=matchedPrevHdr.@CAPTION;
					}
					
					_valuesXml.HEADERS[0].appendChild(hdr);
				}
					
				
				// finally assign type
				if(rowType!=null)
				{
					if(rowType=="SERIES")
						colType="CATEGORIES";
					else
					{
						rowType="CATEGORIES";
						colType="SERIES";
					}
				}
				else
				{
					if(colType=="SERIES")
						rowType="CATEGORIES";
					else
					{
						colType="CATEGORIES";
						rowType="SERIES";
					}
				}
				for each(var hdrXml:XML in _valuesXml..HEADER)
					if(hdrXml.@POS=="ROW")
						hdrXml.@TYPE=rowType;
					else
						hdrXml.@TYPE=colType;
			}
			
								
			//trace(_valuesXml);
			
			return _valuesXml;
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
			_toolTip.x = this.mouseX+5;
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
			//selectItem(CellsetMultiselectCellRenderer.SelectedRow, CellsetMultiselectCellRenderer.SelectedColumn, true);			
			
			if(_selectionChanged)								
				this.dispatchEvent(new Event("CellSelected", true));
			this.hide();
		}
		
		function onCancel(evt:Event)
		{			
			this.hide();
		}
		
		
	}
}