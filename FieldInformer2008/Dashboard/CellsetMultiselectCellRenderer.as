package
{
	import fl.controls.listClasses.CellRenderer;
    import flash.text.TextFormat;
    import fl.controls.listClasses.ListData  
	import fl.controls.listClasses.ICellRenderer;  
		
	public class CellsetMultiselectCellRenderer extends CellRenderer implements ICellRenderer
	{		
	
		public static var ColumnHeaders:int=-1;
		public static var RowHeaders:int=-1;
		public static var SelectedCells:Array=null;
		private static var _styleDef:Object=null;
		
		public function CellsetMultiselectCellRenderer():void
		{
            super();
		}
		
		/*
		public override function set listData(value:ListData):void//Override the seter function of listData
		{
			super.listData = value;
			//var tf:TextField=textField;
			//if(!(data.un==null || data.un==""))
			//	textField.backgroundColor=0xcccccc;                    
		}
		*/
		
		public static function getStyleDefinition():Object  
        {  
			if(_styleDef==null)
            	_styleDef=CellRenderer.getStyleDefinition();  
			return _styleDef;
        }
		
		override protected function drawBackground():void 
		{						
        	var styleDef=getStyleDefinition();
			
			if(isHeader(listData.row, listData.column))
			{				
				if(isSelectedHeader(listData.row, listData.column))
					setCellStyle(styleDef.overSkin);
				else
					setCellStyle(styleDef.selectedDisabledSkin);
			}
			else
			{
				if(isSelectedCell(listData.row, listData.column))
					setCellStyle(styleDef.downSkin);
				else
					setCellStyle(styleDef.disabledSkin);
			}
												
            super.drawBackground();
        }
		
		public function setCellStyle(style:Object)
		{
			var curStyle:Object=getStyleValue("upSkin");
			if(curStyle!=style)
			{
				setStyle("upSkin", style);
				setStyle("downSkin", style);
				setStyle("overSkin", style);
				setStyle("selectedUpSkin", style);
				setStyle("selectedDownSkin", style);
				setStyle("selectedOverSkin", style);
			}
		}
		
		public static function Clear()
		{
			ColumnHeaders=-1;
			RowHeaders=-1;
			SelectedCells=null;
		}
		
		public static function isPlaceholderHeader(row:int, col:int):Boolean
		{
			return (col<RowHeaders && row<ColumnHeaders);
		}
		
		public static function isHeader(row:int, col:int):Boolean
		{
			return (col<RowHeaders || row<ColumnHeaders);
		}
		
		public static function isSelectedHeader(row:int, col:int):Boolean
		{
			if(SelectedCells==null)
				return false;
			if(!isHeader(row, col))
				return false;
			if(isPlaceholderHeader(row, col))
				return false;
				
			var selRow:int=-1;
			var selCol:int=-1;			
			for(var i:int=0;i<SelectedCells.length;i++)
			{
				selRow=SelectedCells[i][0];
				selCol=SelectedCells[i][1];				
				if(selRow==row || selCol==col)
					return true;
			}
			
			return false;
		}
		
		public static function isSelectedCell(row:int, col:int):Boolean
		{
			if(SelectedCells==null)
				return false;
			if(isHeader(row, col))
				return false;			
			
			var selRow:int=-1;
			var selCol:int=-1;			
			for(var i:int=0;i<SelectedCells.length;i++)
			{
				selRow=SelectedCells[i][0];
				selCol=SelectedCells[i][1];
				if(selRow==row && selCol==col)
					return true;
			}
			
			return false;
		}
		
		public static function addSelectedCell(row:int, col:int, doChecks:Boolean):Boolean
		{
			if(doChecks)
			{
				if(isHeader(row, col))
					return false;			
				if(isSelectedCell(row, col))
					return false; 
			}
			
			if(SelectedCells==null)			
				SelectedCells=new Array();
			
			SelectedCells.push([row, col]);
			return true;
		}
		
		public static function getSelectedRows():Array
		{
			var ret:Array=new Array();			
			if(SelectedCells!=null)
			{
				for(var i:int=0;i<SelectedCells.length;i++)
				{
					var row:int=SelectedCells[i][0];
					if(ret.indexOf(row)<0)
						ret.push(row);
				}
			}
			return ret;
		}
		
		public static function getSelectedColumns():Array
		{
			var ret:Array=new Array();			
			if(SelectedCells!=null)
			{
				for(var i:int=0;i<SelectedCells.length;i++)
				{
					var col:int=SelectedCells[i][1];
					if(ret.indexOf(col)<0)
						ret.push(col);
				}
			}
			return ret;
		}
		
		
		public static function changeSelectedCellState(row:int, col:int)
		{
			if(SelectedCells==null)
				SelectedCells=new Array();
			if(isHeader(row, col))
				return;			
			
			var selRow:int=-1;
			var selCol:int=-1;			
			var toAdd:Boolean=true;
			for(var i:int=SelectedCells.length-1;i>=0;i--)
			{
				selRow=SelectedCells[i][0];
				selCol=SelectedCells[i][1];
				if(selRow==row && selCol==col)
				{
					SelectedCells.splice(i,1);
					toAdd=false;
				}
			}
			
			if(toAdd)
				addSelectedCell(row, col, false);			
		}
	}
}