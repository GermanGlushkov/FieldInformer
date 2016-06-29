package ff.as3.ui 
{
	import flash.display.Sprite;
	import flash.display.DisplayObject;
	import flash.events.MouseEvent;
	import flash.geom.Point;
	import flash.geom.Rectangle;
	
	import com.yahoo.astra.fl.containers.BorderPane;

	public class DrawingCanvas extends BorderPane//Sprite 
	{
		public var iBounds:Rectangle;
		public var iLineColor:Number;
		public var iFillColor:Number;
		
		protected var iFrozenState:Boolean = false;

		protected var iOnSelectChangedHandler:Function = null;
		
		public function DrawingCanvas(aW:Number = 500, aH:Number = 200, aFillColor:Number = 0x00FF00, aLineColor:Number = 0x000000)
		{
//trace( "DrawingCanvas " + aW + ":" + aH );			
			super();
			this.iBounds = new Rectangle(0, 0, aW, aH);
			this.iLineColor = aLineColor;
			this.iFillColor = aFillColor;
			
			
			this.addEventListener(MouseEvent.MOUSE_UP, onMouseUp);
		}
		
		public function initCanvas(iFillColor:Number = 0x00FF00, iLineColor:Number = 0x000000):void
		{
			this.iLineColor = iLineColor;
			this.iFillColor = iFillColor;
			drawBounds();
		}
		
		public function drawBounds():void
		{
			this.graphics.clear();
			
			this.graphics.lineStyle(1.0, this.iLineColor, 1.0);
			this.graphics.beginFill(this.iFillColor, 1.0);
			this.graphics.drawRect(0, 0, this.iBounds.width, this.iBounds.height);
			this.graphics.endFill();
		}
		
	    public function addShape( aNewShape:PlaceholderSprite, aPoint:Point = null):void
	    {
            // makes the shapes slightly transparent, so you can see what's behind them
            aNewShape.alpha = 0.8;
            
			this.addChild(aNewShape);

			if ( aPoint == null )
			{
				aPoint = new Point( this.stage.mouseX, this.stage.mouseY ); 
				aPoint = this.stage.globalToLocal( aPoint ); 
			}
			aNewShape.x = aPoint.x;
			aNewShape.y = aPoint.y;
			
			aNewShape.handlerOnSelectChanged = this.iOnSelectChangedHandler;
		}
	    
	    public function removeShape( aShape:PlaceholderSprite):void
	    {
			this.removeChild( aShape );
			aShape.CleanUp();
			aShape = null;
			PlaceholderSprite.iSelectedSprite = null;
			if ( this.iOnSelectChangedHandler != null )
				this.iOnSelectChangedHandler( this );
	    }
		
		public function get isFrozenState():Boolean 
		{
			return this.iFrozenState;
		}
	
		
	    public function selectEmbed( aNewSelected:PlaceholderSprite = null ):void
	    {
			var aChanged:Boolean = false;
			if ( PlaceholderSprite.iSelectedSprite != null )
			{
				PlaceholderSprite.iSelectedSprite.hideSelected();
				PlaceholderSprite.iSelectedSprite = null;
				aChanged = true;
			}
			
			if ( aNewSelected != null )
			{
				PlaceholderSprite.iSelectedSprite = aNewSelected;
				PlaceholderSprite.iSelectedSprite.showSelected();
				aChanged = true;
			}

			if ( aChanged && this.iOnSelectChangedHandler != null )
				this.iOnSelectChangedHandler( this );
	    }
	
		
	    public function frezeShapes( aFreze:Boolean):void
	    {
		    var child:PlaceholderSprite;
			this.iFrozenState = aFreze;
		    for (var i:int=0; i < this.numChildren; i++)
		    {
		        child = this.getChildAt(i) as PlaceholderSprite;
				child.iFrozen = aFreze;
		    }
			if ( aFreze )
				this.selectEmbed( null );
	    }

		public function describeChildren():String
		{   
		    var desc:String = "";
		    var child:DisplayObject;
		    for (var i:int=0; i < this.numChildren; i++)
		    {
		        child = this.getChildAt(i);
		        desc += i + ": " + child + '\n';
		    }
		    return desc;
		}

		public function moveToBack(shape:PlaceholderSprite):void
		{
		    var index:int = this.getChildIndex(shape);
		    if (index > 0)
		    {
		        this.setChildIndex(shape, 0);
		    }
		}
		
		public function moveDown(shape:PlaceholderSprite):void
		{
		    var index:int = this.getChildIndex(shape);
		    if (index > 0)
		    {
		        this.setChildIndex(shape, index - 1);
		    }
		}
		
		public function moveToFront(shape:PlaceholderSprite):void
		{
		    var index:int = this.getChildIndex(shape);
		    if (index != -1 && index < (this.numChildren - 1))
		    {
		        this.setChildIndex(shape, this.numChildren - 1);
		    }
		}

		public function moveUp(shape:PlaceholderSprite):void
		{
		    var index:int = this.getChildIndex(shape);
		    if (index != -1 && index < (this.numChildren - 1))
		    {
		        this.setChildIndex(shape, index + 1);
		    }
		}
		
		public function embedByMousePoint() : PlaceholderSprite
		{
			var aRet : PlaceholderSprite = null;
			var aObjectsUnderPoint:Array = this.stage.getObjectsUnderPoint(new Point(this.stage.mouseX,this.stage.mouseY));
			if ( aObjectsUnderPoint != null )
			{
				for ( var aIdx = 0;  aIdx < (this.numChildren - 1); aIdx++ )
				{
					if ( aObjectsUnderPoint.indexOf( this.getChildAt(aIdx) ) >= 0 )
					{
						aRet = (this.getChildAt(aIdx) as PlaceholderSprite);
						break;
					}
				}
			}
			return aRet;
		}

		/**
		 * Traps all mouseUp events and sends them to the selected shape.
		 * Useful when you release the mouse while the selected shape is
		 * underneath another one (which prevents the selected shape from
		 * receiving the mouseUp event).
		 */
		public function onMouseUp(evt:MouseEvent):void 
		{
		    var aSelectedSprite:PlaceholderSprite = PlaceholderSprite.iSelectedSprite;
		    if (aSelectedSprite != null && aSelectedSprite.isSelected())
		    {
			    aSelectedSprite.onMouseUp(evt);
			}
			
		}
		
		//----------------------------------------------------
		// delegates
		public function set handlerOnSelectChanged( aHandler:Function )
		{
			this.iOnSelectChangedHandler = aHandler;
		}
	}
}