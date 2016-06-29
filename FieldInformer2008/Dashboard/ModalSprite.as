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
	import flash.filters.*;
	
	public class ModalSprite extends Sprite
	{
		var _bck:Sprite=null;
		var _stage:Stage=null;
		protected var _enableDragging:Boolean=false;
		
		function ModalSprite()
		{
			this.addEventListener(MouseEvent.MOUSE_UP, mouseUpEvt);
			this.addEventListener(MouseEvent.MOUSE_DOWN, mouseDownEvt);			
		}		
		
		function mouseDownEvt(evt:MouseEvent)
		{							
			if(_enableDragging)
				this.startDrag();
		}
		
		function mouseUpEvt(evt:MouseEvent)
		{							
			if(_enableDragging)
				this.stopDrag();
		}
		
		public function show(stageRef:Stage)
		{				
			_stage=stageRef;
			if(_bck==null)
			{				
				_bck=createBlurBackground();
				//_bck.addEventListener(MouseEvent.CLICK, onBackgroundClick);
			}
			
			_stage.addChild(_bck);
			this.x=(_stage.stageWidth-this.width)/2;
			if(this.x<1)
				this.x=1;
			this.y=(_stage.stageHeight-this.height)/2;
			if(this.y<1)
				this.y=1;
			_stage.addChild(this);						
			
			var ds:DropShadowFilter = new DropShadowFilter(5, 45, 0x333333, 1, 7, 7, 0.7, 1, false, false, false)
			this.filters = new Array(ds);
		}
		
		public function isShowing():Boolean
		{
			if(_bck==null || _bck.parent==null)
				return false;
			return true;
		}
		
		public function hide()
		{
			_stage.removeChild(_bck);
			_stage.removeChild(this);
		}		
		
		
		private function createBlurBackground():Sprite 
		{
			var myBackground:Sprite = new Sprite();
			var BackgroundBD:BitmapData = new BitmapData(_stage.stageWidth, _stage.stageHeight, true, 0xFF000000);
					var stageBackground:BitmapData = new BitmapData(_stage.stageWidth, _stage.stageHeight);
					stageBackground.draw(_stage);
					var rect:Rectangle = new Rectangle(0, 0, _stage.stageWidth, _stage.stageHeight);
					var point:Point = new Point(0, 0);
					var multiplier:uint = 200;
					BackgroundBD.merge(stageBackground, rect, point, multiplier, multiplier, multiplier, multiplier);
					BackgroundBD.applyFilter(BackgroundBD, rect, point, new BlurFilter(2, 2));
					var bitmap:Bitmap = new Bitmap(BackgroundBD);
					myBackground.addChild(bitmap);
			
			return myBackground;
		}
		
		function onBackgroundClick(evt:MouseEvent)
		{
			this.hide();
		}
	}
	
	
}