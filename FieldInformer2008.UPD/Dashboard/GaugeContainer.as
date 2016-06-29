package
{
	import flash.geom.*;
	import fl.containers.*;
	import flash.display.*;
	import flash.net.*;
	import flash.events.*;
	import fl.events.*;
	import fl.controls.*;
	import flash.ui.*;
	import flash.filters.*;
	
	import com.yahoo.astra.fl.controls.Menu; 
	import com.yahoo.astra.fl.events.MenuEvent;
	
	public class GaugeContainer extends MovieClip
	{
		var _serviceUrl:String="";
		var _userId:String="";
		var _gaugeConfig:XML=null;
		
		var _lastUrlReq:URLRequest=null;
		var _isResize:Boolean=false;
		var _isMove:Boolean=false;
		var _moveStartX:int;
		var _moveStartY:int;
		var _resizeShape:Sprite = new Sprite();
		//var headerRect:Sprite = new Sprite();
		var _propRect:Sprite = new Sprite();
		var _borderRect:Sprite = new Sprite();
		var _loaderInfo:LoaderInfo = null;
		
		var _gauge:DisplayObject=null;
		var _gaugeProp:ModalSprite=null;
		
		var _mnu:Menu=null;
			
		public function GaugeContainer(serviceUrl:String, userId:String, gaugeConfig:XML)
		{			
			this.visible=false;
			_serviceUrl=serviceUrl;
			_userId=userId;
			_gaugeConfig=gaugeConfig;
			
			//headerRect.doubleClickEnabled = true;			
			//headerRect.addEventListener(MouseEvent.DOUBLE_CLICK,onDoubleClick);
			_propRect.doubleClickEnabled = true;
			_propRect.addEventListener(MouseEvent.DOUBLE_CLICK,onDoubleClick);
			_propRect.addEventListener(MouseEvent.CLICK,onClick);
				
			this.addEventListener(Event.ADDED_TO_STAGE, addedToStage);
			//loader.addEventListener(Event.COMPLETE, finishLoading);												
		}
	
		function addedToStage(evt:Event)
		{			
			this.addEventListener(MouseEvent.MOUSE_DOWN, mouseDownEvt);
			stage.addEventListener(MouseEvent.MOUSE_UP, mouseUpEvt);
			stage.addEventListener(MouseEvent.MOUSE_MOVE, mouseMoveEvt);
		}
		
		public function load()
		{			
			_gauge=null;
			if(_gaugeConfig.@TYPE=="TMeter")
			{
				var tm:TMeter=new TMeter();
				tm.init(_userId, _gaugeConfig.@ID, _serviceUrl);
				this.addChildAt(tm,1);
				_gauge=tm;
			}
			else if(_gaugeConfig.@TYPE=="DGauge")
			{
				var dg:DGauge=new DGauge();
				dg.init(_userId, _gaugeConfig.@ID, _serviceUrl);
				this.addChildAt(dg,1);
				_gauge=dg;
			}
			else if(_gaugeConfig.@TYPE=="Chart")
			{
				var ch:Chart=new Chart();
				ch.init(_userId, _gaugeConfig.@ID, _serviceUrl);
				this.addChildAt(ch,1);
				_gauge=ch;
			}
			
			if(_gauge!=null)
			{
				this.x=Number(_gaugeConfig.@X);
				this.y=Number(_gaugeConfig.@Y);
				doResize(Number(_gaugeConfig.@WIDTH),Number(_gaugeConfig.@HEIGHT));
					
				this.visible=true;
			}
		}				
		
		public function saveConfig()
		{
			// xml request
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = 
			"<COMMAND TYPE='SaveUserGaugeConfig' USERID='" + _userId + "'>" + 
    		"<GAUGE ID='" + _gaugeConfig.@ID + "' X='" + this.x + "' Y='" + this.y + "' WIDTH='" + panel.width + "' HEIGHT='" + panel.height + "'/>" + 
			"</COMMAND>"
			var ldr:URLLoader = new URLLoader();
			ldr.load(req);
		}
		
		public function deleteConfig()
		{
			// xml request
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = 
			"<COMMAND TYPE='DeleteUserGaugeConfig' USERID='" + _userId + "'>" + 
    		"<GAUGE ID='" + _gaugeConfig.@ID + "'/>" + 
			"</COMMAND>"
			var ldr:URLLoader = new URLLoader();
			ldr.load(req);
		}
		
		function mouseDownEvt(evt:MouseEvent)
		{			
			hideMenu();
			
			parent.setChildIndex(this, parent.numChildren - 1);
			var tpoint:Point = localToGlobal(new Point(mouseX,mouseY));			
			
			if(resizeCorner.hitTestPoint(tpoint.x,tpoint.y,true))
			{				
				if(_gaugeProp!=null && _gaugeProp.parent!=null)
					removeChild(_gaugeProp);
					
				_isResize=true;
				showResizeShape();
			}
			else if(_gaugeProp!=null && _gaugeProp.hitTestPoint(tpoint.x,tpoint.y,true))
			{
				// don't move prop panel
				//trace("nomove");
			}
			else
			{
				_isMove=true;
				_moveStartX=this.x;
				_moveStartY=this.y;
				this.startDrag(false, null);
			}
		}		
		
		function mouseUpEvt(evt:MouseEvent)
		{							
			hideMenu();
			
			if(_isResize)
			{
				doResize(_resizeShape.width, _resizeShape.height);
				hideResizeShape();
				_isResize=false;
				saveConfig();
			}
			else if(_isMove)
			{
				this.stopDrag();
				_isMove=false;
				if(_moveStartX!=this.x || _moveStartY!=this.y)
					saveConfig();
			}
		}
	
		function mouseMoveEvt(evt:MouseEvent)
		{
			if(_isResize)
				showResizeShape();			
		}		
		
		
		function showResizeShape()
		{
			var x:int=mouseX;
			var y:int=mouseY;
			if(x<30)
				x=30;
			if(y<30)
				y=30;
			_resizeShape.graphics.clear();
			_resizeShape.graphics.lineStyle(1, 0xcccccc);
			_resizeShape.graphics.drawRect(0,0,x,y);
			if(_resizeShape.parent==null)
				this.addChild(_resizeShape);
		}
		
		function hideResizeShape()
		{
			_resizeShape.graphics.clear();
			if(_resizeShape.parent!=null)
				this.removeChild(_resizeShape);
		}
		
		function onDoubleClick(evt:MouseEvent)
		{
			hideMenu();
				
			//load(_lastUrlReq);
			if((_gauge as TMeter)!=null)
				_gaugeProp=(_gauge as TMeter).GetPropertyPanel();			
			else if((_gauge as DGauge)!=null)
				_gaugeProp=(_gauge as DGauge).GetPropertyPanel();			
			else if((_gauge as Chart)!=null)
				_gaugeProp=(_gauge as Chart).GetPropertyPanel();			
			else
				return;
				
			_gaugeProp.show(stage);
		}
		
		function onClick(evt:MouseEvent)
		{
			showMenu();
		}
		
		function showMenu()
		{
			if(_mnu==null)
			{
				var mnuItems:XML =   
				<root>  
					<menuitem label="Properties..." />  
					<menuitem label="Remove" />  
				</root>  
				_mnu = Menu.createMenu(stage, mnuItems);
				_mnu.addEventListener(MenuEvent.ITEM_CLICK, onMenuClick); 
				_mnu.width=100;
			}
			
			_mnu.show(stage.mouseX-_mnu.width, stage.mouseY);
			stage.focus=null;			
		}
		
		function hideMenu()
		{			
			if(_mnu!=null)
				_mnu.visible=false;
		}
		
		function onMenuClick(evt:MenuEvent)
		{
			if(evt.label=="Properties...")
				onDoubleClick(null);
			else if(evt.label=="Remove")
			{
				deleteConfig();
				this.parent.removeChild(this);
			}
			
			_mnu.visible=false;
		}
		
		public function doResize(w:Number,h:Number)
		{																
			panel.width=w;
			panel.height=h;									
			
			_propRect.graphics.clear();
			_propRect.graphics.lineStyle(1, 0xcccccc);
			_propRect.graphics.beginFill(0xcccccc);
			_propRect.graphics.drawRect(w-14, 4 , 10 , 10);
			if(_propRect.parent==null)
				this.addChild(_propRect);
				
			_borderRect.graphics.clear();
			_borderRect.graphics.lineStyle(1, 0x999999);
			_borderRect.graphics.drawRect(0,0,w,h);
			if(_borderRect.parent==null)
				this.addChild(_borderRect);			
			
			var ds:DropShadowFilter = new DropShadowFilter(5, 45, 0x666666, 1, 7, 7, 0.7, 1, false, false, false)
			panel.filters = new Array(ds);

			_gauge.x=1;
			_gauge.y=1;			
			//_gauge.scaleX=(w-2)/_gauge.width;
			//_gauge.scaleY=(h-2)/_gauge.height;			
			_gauge.width=w-2;
			_gauge.height=h-2;			
			
			resizeCorner.x=w-3;
			resizeCorner.y=h-3;
			
			//this.width=w;
			//this.height=h;
		}
	
	}

}

