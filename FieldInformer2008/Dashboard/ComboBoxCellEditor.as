package { 
    import fl.controls.ComboBox;
    import fl.controls.DataGrid;
    import fl.data.DataProvider;
    import fl.controls.listClasses.ICellRenderer; 
    import fl.controls.listClasses.ListData; 
    import flash.display.DisplayObject;
    import flash.events.Event;
    import flash.events.MouseEvent;
    import fl.core.InvalidationType;
    
    public class ComboBoxCellEditor extends ComboBox implements ICellRenderer { 
        private var _listData:ListData; 
        private var _data:Object; 
        private var _selected:Boolean; 
    	private var _owner:DataGrid;
        
        public function ComboBoxCellEditor(owner:DataGrid) { 
            _owner = owner;
			super();
        }   				
        
		public function get owner():DataGrid { 
            return _owner; 
        } 
		
        override public function close():void 
		{			
            highlightCell();
            highlightedCell = -1;
            if (! isOpen) { return; }
            dispatchEvent(new Event(Event.CLOSE));
            _owner.stage.removeEventListener(MouseEvent.MOUSE_DOWN, onStageClick);
            isOpen = false;
            _owner.stage.removeChild(list);
        }
        
        override protected function onListChange(event:Event):void 
		{			
            editableValue = null;
            dispatchEvent(event);
            invalidate(InvalidationType.SELECTED);
            if (isKeyDown) { return; }
            var itm = _owner.editedItemPosition;
            var col = _owner.getColumnAt(itm.columnIndex);
            _owner.editField(itm.rowIndex, col.dataField, selectedLabel);
            dispatchEvent(new Event(Event.CHANGE));
            close();
        }
    
        public function set data(d:Object):void { 			
            _data = d; 
        } 
        public function get data():Object { 
            return _data; 
        } 
        public function set listData(ld:ListData):void { 
            _listData = ld; 
			selectedIndex=findItemIndex(_listData.label);
			textField.text=_listData.label;
        } 
		
        public function get listData():ListData { 
            return _listData; 
        } 
        public function set selected(s:Boolean):void { 
            _selected = s; 
        } 
        public function get selected():Boolean { 
            return _selected; 
        } 
        public function setMouseState(state:String):void { 

        } 
		
		private function findItemIndex (dataString:String):int 
		{
			var index:int = -1;
			for (var i = 0; i < this.length; i++) 
			{
				var o:Object=this.getItemAt(i);
				if (o.label == dataString) 
				{
					index = i;
					break;
				}
			}
			return index;
		}
    } 
}