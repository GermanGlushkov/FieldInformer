package
{
	import fl.controls.listClasses.CellRenderer;
    import flash.text.TextFormat;
    import fl.controls.listClasses.ListData  
	import fl.controls.listClasses.ICellRenderer;  
		
	public class CellsetCellRenderer extends CellRenderer implements ICellRenderer
	{		
	
		public static var ColumnHeaders:int=-1;
		public static var RowHeaders:int=-1;
		public static var SelectedColumn:int=-1;
		public static var SelectedRow:int=-1;
		
		public function CellsetCellRenderer():void
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
            return CellRenderer.getStyleDefinition();  
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
			if(!isHeader(row, col))
				return false;
			if(isPlaceholderHeader(row, col))
				return false;
			if(SelectedRow==row || SelectedColumn==col)
				return true;
			return false;
		}
		
		public static function isSelectedCell(row:int, col:int):Boolean
		{
			if(isHeader(row, col))
				return false;			
			if(SelectedRow==row && SelectedColumn==col)
				return true;
			return false;
		}
		
	}
}