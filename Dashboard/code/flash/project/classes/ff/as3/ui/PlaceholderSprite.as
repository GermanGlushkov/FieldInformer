package ff.as3.ui 
{
	import flash.utils.Timer;

	import flash.net.URLRequest; 
	import flash.net.URLVariables;
	
	import flash.display.Loader;
	import flash.display.LoaderInfo;
	import flash.display.Shape;
	import flash.display.Sprite;
	
	import flash.system.LoaderContext; 

	import flash.events.MouseEvent;
	import flash.events.Event; 
	import flash.events.ProgressEvent; 
	import flash.events.IOErrorEvent; 
	import flash.events.TimerEvent; 

	import flash.geom.Rectangle;
	import flash.geom.Point;

	import flash.xml.*;

	import flash.net.SharedObject;
    import flash.net.SharedObjectFlushStatus;

	import be.boulevart.as3.security.*;

	public class PlaceholderSprite extends Sprite 
	{
		public var iGuid:String = null;
		
	    public var iW:Number = 0;
	    public var iH:Number = 0;
		public var iLineColor:Number = 0x000000;
		public var iFillColor:Number = 0xDDDDEE;
		
		public var iShapeType:String = "PlaceholderSprite";
		
		public var iLoader:Loader = null;
		protected var iLoaderContext:LoaderContext = null;
		public var iURLRequest:URLRequest = null;
		protected var iTimer:Timer = null;
		
		protected var iConfig:XML = null;
		public var iSOConfig:SharedObject = null;
		
		protected var iLoaded:Boolean = false;

		public var iFrozen:Boolean = false;

		protected var iOnSelectChangedHandler:Function = null;

		/**
		 * An instance of a purely geometric shape, that is, one that defines
		 * a shape mathematically but not visually.
		 */
		//public var geometricShape:IGeometricShape;
		
		/**
		 * Keeps track of the currently selected shape.
		 * This is a static property, so there can only be one PlaceholderSprite
		 * selected at any given time.
		 */
		public static var iSelectedSprite:PlaceholderSprite;
		
		/**
		 * Holds a border rectangle that is shown when this PlaceholderSprite instance is selected.
		 */
		public var iSelectionIndicator:Shape;
		
		public function PlaceholderSprite(aGuid:String, aUrl:String, aConfig:String, aURLVariables:URLVariables, aW:Number = 0, aH:Number= 0, aMethod:String = "GET")
		{
//trace(aUrl);
			this.iGuid = aGuid;
			this.iSOConfig = SharedObject.getLocal(this.iGuid, "/");
trace("PlaceholderSprite: " + aConfig);
			this.iSOConfig.data.config = be.boulevart.as3.security.Base64.encode(aConfig);
			var aFlushResult:Object = this.iSOConfig.flush();
//trace(this.iGuid + ": " + aFlushResult);
			
aUrl=aUrl;		

			this.iURLRequest = new URLRequest( aUrl );
			iURLRequest.method = aMethod;
			//var aURLVariables:URLVariables = new URLVariables();
			//aURLVariables.config = be.boulevart.as3.security.Base64.encode(aConfig);
			aURLVariables.SOCid = this.iGuid;
			iURLRequest.data = aURLVariables;

			this.iConfig = new XML(aConfig);

			this.iLoaderContext = new LoaderContext();
			this.iLoaderContext.checkPolicyFile = true;

			if ( aW == 0 )
				this.iW = this.iConfig.@w;
			else
				this.iW = aW;
				
			if ( aH == 0 )
				this.iH = this.iConfig.@h;
			else
				this.iH = aH;
			
			//this.iLineColor = aLineColor;
			//this.iFillColor = aFillColor;
			this.addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
			this.addEventListener(MouseEvent.MOUSE_UP, onMouseUp);

			this.iTimer = new Timer(500,1);
			this.iTimer.addEventListener("timer", this.onTimerHandler);
			this.iTimer.start();
			
			this.drawShape();
		}
		
		// 'Handmade:)' destructor
		public function CleanUp():void
		{
			if ( this.iLoader != null )
			{
				try
				{
					if ( !this.iLoaded )
						this.iLoader.close();
					this.removeChild(this.iLoader);
				}
				catch (aError:Error) 
				{
					trace(aError);
				}
				this.iLoader = null;
			}
			if ( this.iTimer != null )
			{
				try
				{
					this.iTimer.stop();
				}
				catch (aError:Error) 
				{
					trace(aError);
				}
				this.iTimer = null;
			}
			
			if ( this.iSOConfig != null )
			{
				try
				{
					this.iSOConfig.clear();
				}
				catch (aError:Error) 
				{
					trace(aError);
				}
				this.iSOConfig = null;
			}

			iLoaderContext = null;
			iURLRequest = null;
			iConfig = null;
		}
		
		
		public function drawShape():void
		{
            // to be overridden in subclasses
			if ( !this.iLoaded )
			{
				with( this.graphics )
				{
					clear();
					beginFill(this.iFillColor, 1);
					moveTo(0, 0);
					lineTo(0, this.iH);
					lineTo(this.iW, this.iH);
					lineTo(this.iW, 0);
					endFill();
					lineStyle(1.0, this.iLineColor, 1.0);
					drawRect(0, 0, this.iW, this.iH);
				}
			}
		}
		
		private function onMouseDown(evt:MouseEvent):void 
		{
			if ( !this.iFrozen )
			{
				this.showSelected();
				
				// limits dragging to the area inside the canvas
				var aBoundsRect:Rectangle = this.parent.getRect(this.parent);
				aBoundsRect.width -= this.iW;
				aBoundsRect.height -= this.iH;
				this.startDrag(false, aBoundsRect);
			}
		}
		
		public function onMouseUp(aMouseEvent:MouseEvent):void 
		{
			if ( !this.iFrozen )
				this.stopDrag();
		}
		
		public function showSelected():void
		{
		    if (this.iSelectionIndicator == null)
		    {
		        // draws a red rectangle around the selected shape
		        this.iSelectionIndicator = new Shape();
		        this.iSelectionIndicator.graphics.lineStyle(1.0, 0x880000, 0.5);
			    this.iSelectionIndicator.graphics.drawRect(-1, -1, this.iW + 1, this.iH + 1);
			    this.addChild(this.iSelectionIndicator);
		    }
		    else
		    {
		        this.iSelectionIndicator.visible = true;
		    }
		    
		    if (PlaceholderSprite.iSelectedSprite != this)
		    {
    		    if (PlaceholderSprite.iSelectedSprite != null)
    		    {
    		        PlaceholderSprite.iSelectedSprite.hideSelected();
    		    }
		        PlaceholderSprite.iSelectedSprite = this;
				if ( this.iOnSelectChangedHandler != null )
					this.iOnSelectChangedHandler( this );
			}
		}
		
		public function hideSelected():void
		{
		    if (this.iSelectionIndicator != null)
		    {		    
		        this.iSelectionIndicator.visible = false;
		    }
		}
		
		/**
		 * Returns true if this shape's selection rectangle is currently showing.
		 */
		public function isSelected():Boolean
		{
		    return !(this.iSelectionIndicator == null || this.iSelectionIndicator.visible == false);
		}
		
		
		public override function toString():String
		{
		    return this.iShapeType + /*" of size " + this.size + */" at " + this.x + ", " + this.y;
		}
		
		protected function onTimerHandler(event:TimerEvent):void 
		{
            //trace("onTimerHandler: " + event);
			if ( this.iLoader == null )
			{
				this.iLoader = new Loader();
				this.iLoader.contentLoaderInfo.addEventListener(ProgressEvent.PROGRESS, onProgressHandler);
				this.iLoader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, onIoErrorHandler);;
				this.iLoader.contentLoaderInfo.addEventListener(Event.INIT, onInitHandler);
				this.iLoader.contentLoaderInfo.addEventListener(Event.COMPLETE, onCompleteHandler);
	
				this.iLoader.load(this.iURLRequest, this.iLoaderContext);
			}
        }
	
        protected function onInitHandler(aEvent:Event):void 
		{
			//trace("onInitHandler: " + aEvent);
        }
		
        protected function onIoErrorHandler(aEvent:IOErrorEvent):void 
		{
			trace("PlaceholderSprite.onIoErrorHandler: " + aEvent);
		}
		
		protected function onCompleteHandler(aEvent:Event)
		{
			this.iLoader.contentLoaderInfo.removeEventListener(Event.COMPLETE, onCompleteHandler);
			this.iLoader.contentLoaderInfo.removeEventListener(Event.INIT, onInitHandler);
			this.iLoader.contentLoaderInfo.removeEventListener(ProgressEvent.PROGRESS, onProgressHandler);
			this.iLoader.contentLoaderInfo.removeEventListener(IOErrorEvent.IO_ERROR, onIoErrorHandler);
			this.addChild(this.iLoader);
			this.iLoaded = true;
			with( this.graphics )
			{
				clear();
			}
	} 
		
		protected function onProgressHandler(mProgress:ProgressEvent) 
		{ 
			var percent:Number = mProgress.bytesLoaded/mProgress.bytesTotal;
			//trace("onProgressHandler: " + percent); 
		} 
		
		
		public function toXmlString():String
		{
			var aConfig:String = this.iSOConfig.data.config;
			//aConfig = be.boulevart.as3.security.Base64.decode(aConfig);
			//trace(aConfig);
			
			var aEltStr:String = "<embed x='" + this.x + "' y='" + this.y + "' w='" + this.iW.toString() + "' h='" + this.iH.toString() + "' guid='" + this.iGuid + "' >";

			aEltStr += "<url><![CDATA[" + be.boulevart.as3.security.Base64.encode(this.iURLRequest.url) + "]]></url>";
			aEltStr += "<vars>";
			for(var aKeyStr:String in this.iURLRequest.data)
			{
//trace(aKeyStr + ": " + this.iURLRequest.data[aKeyStr] );					
				aEltStr += "<" + aKeyStr + "><![CDATA[" + be.boulevart.as3.security.Base64.encode(this.iURLRequest.data[aKeyStr])  + "]]></" + aKeyStr + ">";
			}
			aEltStr += "</vars>";
			aEltStr += "<config><![CDATA[" + aConfig + "]]></config></embed>";
//trace(aEltStr);					
			
			return aEltStr;
		}
		
		static public function fromXml(aElt:XML, aMethod:String = "GET"):PlaceholderSprite
		{
//trace( aElt.toString() );
			var aX:Number = aElt.@x;
			var aY:Number = aElt.@y;
			var aW:Number = aElt.@w;
			var aH:Number = aElt.@h;

			var aGuid:String = aElt.@guid;

//trace( "Guid: " + aGuid );

			var aConfig:String = "";
			var aUrl:String = "";

			var aName:String = "";
			var aEltList:XMLList = aElt.*;
			var aURLVariables:URLVariables = new URLVariables();
			for (var a1:int = 0; a1 < aEltList.length(); a1++)
			{ 
				aName = aEltList[a1].name();
//trace( aEltList[a1] );
				if ( aName == "url" )
				{
					aUrl = be.boulevart.as3.security.Base64.decode(aEltList[a1].toString());
				}
				else if ( aName == "config" )
				{
					aConfig = be.boulevart.as3.security.Base64.decode(aEltList[a1].toString());
				}
				else if ( aName == "vars" )
				{
					var aVar:String = null;
					var aVarsList:XMLList = aEltList[a1].*;
					for (var a2:int = 0; a2 < aVarsList.length(); a2++)
					{ 
						aVar = aVarsList[a2].name();
						aURLVariables[aVar] = be.boulevart.as3.security.Base64.decode(aVarsList[a2].toString());
//trace( aVar + ": " +  aURLVariables[aVar] );								
					} 
				}
			} 
trace( "Url: " + aUrl );
trace( "Config: " + aConfig );

			return new PlaceholderSprite(aGuid, aUrl, aConfig, aURLVariables, aW, aH, aMethod );						
		}

		
		//----------------------------------------------------
		// double click
		
		public function set handlerOnSelectChanged( aHandler:Function )
		{
			this.iOnSelectChangedHandler = aHandler;
		}

	}
}