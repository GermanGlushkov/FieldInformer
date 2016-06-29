package
{
	import flash.system.Security;
	
	import flash.ui.ContextMenu;
	import flash.ui.ContextMenuItem;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.display.DisplayObject;
	
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.events.ContextMenuEvent;

	import fl.controls.*;
	import fl.controls.Button;
	import fl.controls.DataGrid;
	import fl.controls.dataGridClasses.DataGridColumn;
	import fl.controls.List;
	import fl.data.DataProvider;

	import flash.display.*;
	import flash.text.*;

	import flash.xml.*;
	
	import flash.geom.Point;

	import flash.net.URLRequest;
    import flash.net.URLVariables;
    import flash.net.sendToURL;

	import com.adobe.as3Validators.as3DataValidation;

	import com.yahoo.astra.fl.containers.HBoxPane;
	import com.yahoo.astra.fl.containers.VBoxPane;
	import com.yahoo.astra.fl.containers.BorderPane;
	import com.yahoo.astra.layout.modes.BorderConstraints;
	import com.yahoo.astra.layout.modes.VerticalAlignment;
	import com.yahoo.astra.layout.modes.HorizontalAlignment;
	import com.yahoo.astra.fl.controls.Menu;
	import com.yahoo.astra.fl.events.MenuEvent;
	import com.yahoo.astra.fl.utils.FlValueParser;
	import com.yahoo.astra.utils.IValueParser;
	import com.yahoo.astra.utils.ValueParser;

	import be.boulevart.as3.security.*;

	import ff.as3.ui.*;//VParamDataInformerMC BaseDataInformerMC


	/**
	 * 
	 */
	public class DashboardTest extends ff.as3.ui.VParamDataInformerMC 
	{
		protected var iUiList:XML = null;
		
		protected var iDrawingCanvas:DrawingCanvas = null;
		
		protected var iXmlDb:XML = null;
		protected var iDbLoaded:Boolean = false;
		
		protected var iUrlBase:String = null;

		/**
		 * Constructor.
		 */
		public function DashboardTest()
		{
			this.iDebugMode = false;
			super();
			this.iDebugMode = false;
			
			this.iUrlBase = "http://localhost/eforcemobile/Image.aspx";
			if ( stage != null )
			{
				var aUrlParts:Array = stage.loaderInfo.url.split("://");
				var aValue:String = aUrlParts[0] + "://";
				var aWwwPart:Array = aUrlParts[1].split("?");
				aValue += aWwwPart[0];
				if ( aValue.substring(0,4) != "file" )
					this.iUrlBase = aValue;
			}

			//trace(this.iUrlBase);
			//this.iHBoxHeader.visible= false;
			
			if ( this.iDebugMode && this.iLoaderParamsObj.hasOwnProperty("BrokerUrl") )
				this.iTextFieldHeader.appendText(" "+ this.iLoaderParamsObj.BrokerUrl);

			
			if(this.stage)
			{
				this.stage.scaleMode = StageScaleMode.NO_SCALE;
				this.stage.align = StageAlign.TOP_LEFT;
				this.stage.addEventListener(Event.RESIZE, onStageResizeHandler, false, 0, true);
			}

			this.ShowData();

		}


		
		//-----------------------------------------------------------
		// constructor's helpers
		//-----------------------------------------------------------
		// constructor's helpers
		
		override protected function constructor_default_AppConfig() : void
		{
			super.constructor_default_AppConfig();
			try
			{
				var aToMerge:XML = <config><v t='caption' ><![CDATA[DASHBOARD ver. 0.2.2]]></v><v t='delay'><![CDATA[0.3]]></v><v t='queryStartup' /><v t='frequency'><![CDATA[0]]></v><v t='queryRecurring' /></config>;
				this.iAppConfig.mergeIn( aToMerge );

				var aValue:String = this.iAppConfig.getValueText("brokerUrl");
				if ( aValue == null || aValue.toString() == "")
					this.iAppConfig.replaceValueText( "brokerUrl", "http://localhost/eforcemobile/Broker.aspx" );

				var aQuery:String = "select [image] from dbo.efm_tIMAGE where image_id in (select image_id from dbo.efm_tENTITY where entity_id=";
				aValue = this.iAppConfig.getValueText("db");
				if ( aValue == null || aValue.toString() == "" )
					aQuery += "516";
				else
					aQuery += aValue;
				aQuery += ")";

				aValue = this.iAppConfig.getValueText("queryStartup");
				if ( aValue == null || aValue.toString() == "" )
					aQuery += " select substring(ui_name,6, len(ui_name) - 6 + 1) as [name], ui_id as [uiid], ui_config as [config] from eforcemobile.dbo.vw_efm_Uis where ui_type='swf' and ui_name like 'Info %' order by ui_name";
				else 
					aQuery += " " + aValue.toString();
//trace(aQuery);				
				this.iAppConfig.replaceValueText( "queryStartup", aQuery );
			}
			catch (aError:Error) 
			{
				trace("BaseSqlInformerMC.constructor_default_AppConfig: " + aError);
			}
		}


		
		//----------------------------------------------------
		// protected (overridable) properties
		
		override protected function get origDocWidth():Number 
		{
			return 1280;
		}
		
		override protected function get origDocHeight():Number 
		{
			return 720;
		}

		//----------------------------------------------------
		// protected (overridable) functions


		override protected function configureToolbar():void
		{
			super.configureToolbar();
			this.iHBoxToolbar.horizontalAlign = HorizontalAlignment.LEFT;


			this.iHBoxToolbar.configuration =
			[
				{target: this.iButtonParameters, constraint: BorderConstraints.LEFT} //
			];
		}
		
		override protected function configureHeader():void
		{
			super.configureHeader();

			this.iTextFormatHeader = new TextFormat("Arial", 16, 0x333333, true, false, false, null, null, "left");
			this.iHBoxHeader.horizontalAlign = HorizontalAlignment.LEFT;
			this.iTextFieldHeader.defaultTextFormat = this.iTextFormatHeader;

			this.iHBoxHeader.configuration =
			[
				{target: this.iTextFieldHeader, constraint: BorderConstraints.LEFT, percentWidth: 100} //
			];
		}

		override protected function configureContent():void
		{
			this.iPaneContent.x=0;
			this.iPaneContent.y=42;
			this.iPaneContent.setSize(origDocWidth - 8,origDocHeight - 42 - 8);

			this.iDrawingCanvas = new DrawingCanvas(this.iPaneContent.width, this.iPaneContent.height); 
			this.iDrawingCanvas.initCanvas(0xFFFFFF, 0xCCCCCC );

			this.iDrawingCanvas.handlerOnSelectChanged = this.onCanvasSelectionChanged;

			this.iDrawingCanvas.frezeShapes( false );

			this.iPaneContent.configuration = 
			[
				{target: this.iDrawingCanvas, constraint: BorderConstraints.TOP, percentWidth: 100, percentHeight: 100}
			];
		}


		//----------------------------------------------------
		// protected (overridable) functions
		
		override protected function onLoadFinished(  aSender:Object )
		{
		}
		
		override protected function onLoadData( aSender:Object, aXML:XML )
		{
			//trace( this.iDATA );
			if ( this.iDbLoaded )
			{
				this.iUiList = aXML;
			}
			else
			{
				this.iDATA = aXML;
				this.iDbLoaded = true;
				var aXmlDbString:String = this.iDATA.r[0].c1[0].toString();
//trace(this.iDATA);
				aXmlDbString = be.boulevart.as3.security.Base64.decode(aXmlDbString);
//trace(aXmlDbString);
				this.iXmlDb = new XML( aXmlDbString );
				if ( this.iXmlDb.hasOwnProperty( "embed" ) )
				{
					for ( var aE:Number = 0; aE < this.iXmlDb.child("embed").length(); aE++ )
					{
						var aX:Number = this.iXmlDb.child("embed")[aE].@x;
						var aY:Number = this.iXmlDb.child("embed")[aE].@y;
						var aPlaceholderSprite:PlaceholderSprite = PlaceholderSprite.fromXml(this.iXmlDb.child("embed")[aE]/*, "GET"*/ );						
						this.iDrawingCanvas.addShape( aPlaceholderSprite );
						aPlaceholderSprite.x = aX;
						aPlaceholderSprite.y = aY;
					}
				}
//trace(this.iXmlDb.toString());
	
				this.ShowData();
			}
		}

		//----------------------------------------------------
		// own protected (overridable) helpers

		//-----------------------------------------------------------
		// own events
		
		/**
		 * When the stage resizes, resize the applicationand reposition
		 * the loading status dialog.
		 */
		protected function onStageResizeHandler(event:Event):void
		{
			//resize the application to match the stage dimensions (liquid)
			if ( this.stage != null )
			{
				this.width = this.stage.stageWidth;
				this.height = this.stage.stageHeight;
				this.drawNow();
			}
		}
		
		protected function onButtonAddClick(aMouseEvent:MouseEvent):void 
		{
			if ( this.iUiList.selectedItem != null )
			{
				//trace( this.iUiList.selectedItem.c1 ); 
				var aURLVariables:URLVariables = new URLVariables();
				var aValue:String = this.iAppConfig.getValueText("brokerUrl");
//trace("BrokerUrl: " + this.iLoaderParamsObj.BrokerUrl );					
				if ( aValue == null || aValue.toString() == "")
				{
					this.iDrawingCanvas.addShape( new PlaceholderSprite(be.boulevart.as3.security.GUID.create(), "http://localhost/TestWebService/Info" + this.iUiList.selectedItem.c1 + ".swf", this.iUiList.selectedItem.c3, aURLVariables), this.iMenuULPoint );
				}
				else
				{
					aURLVariables.id = this.iUiList.selectedItem.c2;
					aURLVariables.t = "application/x-shockwave-flash";
					aURLVariables.BrokerUrl = aValue;
					this.iDrawingCanvas.addShape( new PlaceholderSprite(be.boulevart.as3.security.GUID.create(), this.iUrlBase, this.iUiList.selectedItem.c3, aURLVariables), this.iMenuULPoint );
				}
			}
		}
		
		/**
		 * Traps all mouseUp events and sends them to the selected shape.
		 * Useful when you release the mouse while the selected shape is
		 * underneath another one (which prevents the selected shape from
		 * receiving the mouseUp event).
		 */
		protected function onPaneContentMouseUp(aMouseEvent:MouseEvent):void 
		{
		    var aSelectedSprite:PlaceholderSprite = PlaceholderSprite.iSelectedSprite;
		    if (aSelectedSprite != null && aSelectedSprite.isSelected())
		    {
			    aSelectedSprite.onMouseUp(aMouseEvent);
			}
		}
		
		protected function onButtonRemoveClick(event:MouseEvent):void
		{
		    var aSelectedSprite:PlaceholderSprite = PlaceholderSprite.iSelectedSprite;
		    if (aSelectedSprite != null && aSelectedSprite.isSelected())
		    {
				this.iDrawingCanvas.removeShape( aSelectedSprite );
			}
		}
		
		protected function SaveDB()
		{
			var aShape:PlaceholderSprite;
			var aEmbedded:XML = <embedded/>;
			for (var i:int=0; i < this.iDrawingCanvas.numChildren; i++)
			{
				try
				{
					aShape = this.iDrawingCanvas.getChildAt(i) as PlaceholderSprite;
					var aElt:XML = new XML(aShape.toXmlString());
					aEmbedded.appendChild(aElt);
				}
				catch (aError:Error) 
				{
					trace(aError);
				}
			}
			this.iXmlDb = aEmbedded;
			// send it back to server
			try
			{
				var aUpdate:String = "<command dialect='XSQL' transaction='1' type='StoredProcedure' execute='query'>"; 
				
				// required/default parameters
				aUpdate += "<![CDATA[" + be.boulevart.as3.security.Base64.encode("eForceMobile.dbo.efm_IMAGE_Insert") + "]]>";
				aUpdate += "<p n='image_id'><![CDATA[" + be.boulevart.as3.security.Base64.encode("0") + "]]></p>";

				aUpdate += "<p n='enity_id'>"; 

				var aValue:String = this.iAppConfig.getValueText("db");
				if ( aValue == null || aValue.toString() == "" )
					aUpdate += be.boulevart.as3.security.Base64.encode("516");
				else
					aUpdate += be.boulevart.as3.security.Base64.encode(aValue);
				aUpdate += "</p>";
				aUpdate += "<p n='image'><![CDATA[";
				aUpdate += be.boulevart.as3.security.Base64.encode(this.iXmlDb.toString());
				aUpdate += "]]></p>";
				aUpdate += "</command>";
				
				aEmbedded = new XML(aUpdate);

//trace( aEmbedded.toString() );

				this.iDataLoader.sendUpdate(aEmbedded, true, true );
			}
			catch (aError:Error) 
			{
				trace(aError);
			}
		}

		override protected function onContextMenuItemSelectHandler(event:ContextMenuEvent):void 
		{
			if ( event.target.caption.length >= 16 && event.target.caption.substr(0, 16) == "Remove selected " )
			{
				var aSelectedSprite:PlaceholderSprite = PlaceholderSprite.iSelectedSprite;
				if (aSelectedSprite != null && aSelectedSprite.isSelected())
				{
					this.iDrawingCanvas.removeShape( aSelectedSprite );
				}
			}
			else if ( event.target.caption.length > 4 && event.target.caption.substr(0, 4) == "New " )
			{
				for each(var aN:XML in this.iUiList..r)
				{
					if ( event.target.caption.substr(4) == aN.c1.toString() )
					{
						var aURLVariables:URLVariables = new URLVariables();
						var aValue:String = this.iAppConfig.getValueText("brokerUrl");
		//trace("BrokerUrl: " + this.iLoaderParamsObj.BrokerUrl );					
						if ( aValue != null && aValue.toString() != "")
						{
							aURLVariables.id = aN.c2;
							aURLVariables.t = "application/x-shockwave-flash";
							aURLVariables.BrokerUrl = aValue;
							this.iDrawingCanvas.addShape( new PlaceholderSprite(be.boulevart.as3.security.GUID.create(), this.iUrlBase, aN.c3, aURLVariables/*, "GET"*/), this.iMenuULPoint );
						}
						break;
					}
				}
			}
        }


		
		protected function onCanvasSelectionChanged(source:Object):void
		{
		}

		
		
		override protected function onContextMenuSelect( aEvent:flash.events.ContextMenuEvent ): void
		{
			super.onContextMenuSelect( aEvent );
			
			this.iContextMenu.customItems = new Array;
			this.iContextMenu.hideBuiltInItems();

			var aContextMenuItem:ContextMenuItem = null;
			
			var aEmbedByMouse:PlaceholderSprite = this.iDrawingCanvas.embedByMousePoint();
			if ( aEmbedByMouse != null )
				this.iDrawingCanvas.selectEmbed( aEmbedByMouse );
		    if ( PlaceholderSprite.iSelectedSprite != null )
		    {
				aContextMenuItem = new ContextMenuItem("Remove selected gauge(s)");
				this.iContextMenu.customItems.push(aContextMenuItem);
				aContextMenuItem.addEventListener(ContextMenuEvent.MENU_ITEM_SELECT, this.onContextMenuItemSelectHandler);
			}

			aContextMenuItem = new ContextMenuItem("Add gauge:");
			aContextMenuItem.enabled = false;
			aContextMenuItem.separatorBefore = true;
			this.iContextMenu.customItems.push(aContextMenuItem);
			if ( this.iUiList != null )
			{
				for each(var aN:XML in this.iUiList..r)
				{
					aContextMenuItem = new ContextMenuItem("New " + aN.c1.toString());
					this.iContextMenu.customItems.push(aContextMenuItem);
					aContextMenuItem.addEventListener(ContextMenuEvent.MENU_ITEM_SELECT, this.onContextMenuItemSelectHandler);
				}
			}
		}
	}
}
