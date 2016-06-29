package ff.as3.ui 
{ 

	// Import GetDefinitionByName to dynamically refer to library clips
	import flash.utils.getDefinitionByName;
	import flash.system.Security;

	import ff.as3.data.*;
	import ff.as3.ui.BaseDataInformerMC;
	import ff.as3.ui.skin.Skin001;
	import ff.as3.ui.FormUI;

	import com.adobe.as3Validators.as3DataValidation;

	import com.yahoo.astra.fl.containers.Form;
	import com.yahoo.astra.fl.containers.BoxPane;
	import com.yahoo.astra.fl.containers.HBoxPane;
	import com.yahoo.astra.fl.containers.VBoxPane;
	import com.yahoo.astra.fl.containers.BorderPane;
	import com.yahoo.astra.layout.modes.BorderConstraints;
	import com.yahoo.astra.layout.modes.VerticalAlignment;
	import com.yahoo.astra.layout.modes.HorizontalAlignment;
	import com.yahoo.astra.containers.formClasses.FormItem;
	import com.yahoo.astra.containers.formClasses.FormLayoutStyle;
	import com.yahoo.astra.fl.utils.FlValueParser;
	import com.yahoo.astra.fl.managers.AlertManager;
	import com.yahoo.astra.utils.InstanceFactory;

	import flash.net.*;
	import flash.xml.*;
	import flash.display.*;
	import flash.text.*;
	import flash.events.*;
	import flash.utils.*;
	import flash.ui.ContextMenu;
	import flash.ui.ContextMenuItem;
	
	import flash.geom.Rectangle;
	import flash.geom.Point;

	import fl.controls.*;
	import fl.controls.Button;
	import fl.controls.DataGrid;
	import fl.controls.dataGridClasses.DataGridColumn;
	import fl.data.DataProvider;
	
	import flash.net.SharedObject;
    import flash.net.SharedObjectFlushStatus;
	
	public class VParamDataInformerMC extends BaseDataInformerMC
	{
		/**-----------------------------------------------------------------------------
		 * Properties.
		 */
		protected var iArraySkins:Array = new Array;

		protected var iContextMenu:ContextMenu = null;
		protected var iMenuULPoint = new Point(0,0);
		
		protected var iHBoxToolbar:HBoxPane = null;
		protected var iTextFormatToolbar:TextFormat = null;
		protected var iButtonParameters:Button = null;
		
		protected var iHBoxHeader:HBoxPane = null;
		public var iTextFieldHeader:TextField = null;
		protected var iTextFormatHeader:TextFormat = null;

		protected var iPaneContent:BoxPane = null;

		protected var iDisplayObjectParameters:DisplayObject = null;
		//protected var iDataGridParameters:DataGrid = null;

		protected var iDebugMode = false;
		
		/**-----------------------------------------------------------------------------
		 * Constructor.
		 */
		public function VParamDataInformerMC()
		{
			super();

			if(this.stage)
			{
				//this.stage.scaleMode = StageScaleMode.NO_SCALE;
				this.stage.align = StageAlign.TOP_LEFT;
			}

			this.width = this.origDocWidth;
			this.height = this.origDocHeight;

			this.debugMode = iDebugMode;

			this.iTextFormatToolbar = new TextFormat("Arial", 10, 0xcc0000, true, false, false, null, null, "right");
			this.iTextFormatHeader = new TextFormat("Arial", 16, 0x333333, true, false, false, null, null, "center");

			this.iArraySkins.push( new InstanceFactory(Skin001, {width:10, colors:[0xEEEEEE, 0x666666, 0xEEEEEE], alphas:[0.5, 0.5, 0.5], ratios:[0, 120, 200], height:10,gradientRotation:90}) );
			this.iArraySkins.push( new InstanceFactory(Skin001, {width:10, colors:[0x666666, 0x000000, 0x000000], alphas:[1, 1, 1], ratios:[0, 120, 200], height:10,gradientRotation:90}) );
			this.iArraySkins.push( new InstanceFactory(Skin001, {width:10, colors:[0xEEEEEE, 0x666666, 0xEEEEEE], alphas:[0, 0, 0], ratios:[0, 120, 200], height:10,gradientRotation:90}) );

			this.iHBoxToolbar = new HBoxPane();
			this.iHBoxToolbar.debugMode = iDebugMode;
			this.configureToolbar();
			
			this.iHBoxHeader = new HBoxPane();
			this.iHBoxHeader.debugMode = iDebugMode;
			this.configureHeader();
			
			this.createPaneContent();
			this.configureContent();
			
			this.configureContextMenu();

			this.configuration = 
			[
				{target: this.iHBoxToolbar, constraint: BorderConstraints.TOP}//, percentWidth: 100
				,
				{target: this.iHBoxHeader, constraint: BorderConstraints.TOP}//, percentWidth: 100
				,
				{target: this.iPaneContent, constraint: BorderConstraints.CENTER}//, percentWidth: 100, percentHeight: 100
			];

		}
		
		//-----------------------------------------------------------
		// constructor's helpers
		
		protected function createPaneContent():void
		{
			this.iPaneContent = new VBoxPane(); 
			//this.iPaneContent.setStyle("skin", iArraySkins[2]);
			this.iPaneContent.paddingTop = this.iPaneContent.paddingBottom = 1;
			this.iPaneContent.paddingRight = this.iPaneContent.paddingLeft = 2;
			this.iPaneContent.horizontalGap = 1;
			this.iPaneContent.debugMode = iDebugMode;
		}
				
		
		//----------------------------------------------------
		// protected (overridable) properties
		
		protected function get origDocWidth():Number 
		{
			if ( this.stage )
				return this.stage.stageWidth;
			else
				return NaN;
		}
		
		protected function get origDocHeight():Number 
		{
			if ( this.stage )
				return this.stage.stageHeight;
			else
				return NaN;
		}
		


		//----------------------------------------------------
		// protected (overridable) functions

		protected function configureToolbar():void
		{
			this.iHBoxToolbar.x=0;
			this.iHBoxToolbar.y=0;			
			this.iHBoxToolbar.horizontalAlign = HorizontalAlignment.RIGHT;
			this.iHBoxToolbar.verticalAlign = VerticalAlignment.TOP;
			//allow this container to determine its optimal size
			if ( this.stage )
				this.iHBoxToolbar.setSize(this.origDocWidth, 12);
			else
				this.iHBoxToolbar.height = 12;
			////this.iHBoxToolbar.autoSize = true;
			//this.iHBoxToolbar.debugMode = iDebugMode;
			this.iHBoxToolbar.paddingTop = this.iHBoxToolbar.paddingBottom = 1;
			this.iHBoxToolbar.paddingRight = this.iHBoxToolbar.paddingLeft = 2;
			this.iHBoxToolbar.horizontalGap = 0;

			this.iHBoxToolbar.setStyle("skin", iArraySkins[0]);

			this.iButtonParameters = new Button();
			this.iButtonParameters.toggle = true;
			this.iButtonParameters.width = 12;
			this.iButtonParameters.height = 10;
			this.iButtonParameters.label = "";
			this.iButtonParameters.setStyle("icon", Shape);
			this.iButtonParameters.setStyle("upSkin", "ScrollArrowDown_upSkin");
			this.iButtonParameters.setStyle("overSkin", "ScrollArrowDown_upSkin");
			this.iButtonParameters.setStyle("downSkin", "ScrollArrowDown_upSkin");
			this.iButtonParameters.setStyle("selectedUpSkin", "ScrollArrowUp_upSkin");
			this.iButtonParameters.setStyle("selectedOverSkin", "ScrollArrowUp_upSkin");
			this.iButtonParameters.setStyle("selectedDownSkin", "ScrollArrowUp_upSkin");
			this.iButtonParameters.setStyle("textPadding", 2);
			this.iButtonParameters.textField.defaultTextFormat = this.iTextFormatToolbar;
			this.iButtonParameters.textField.setTextFormat( this.iTextFormatToolbar );
			this.iButtonParameters.setStyle("textFormat", this.iTextFormatToolbar);
			this.iButtonParameters.addEventListener(MouseEvent.CLICK, _toggleButtonParameters);
		
			//to add the title and subtitle as children of this container, we
			//may use addChild(), like any other DisplayObjectContainer. However,
			//we're using this container's configuration property because we
			//want the subtitle to grow fluidly to be aligned to the right
			this.iHBoxToolbar.configuration =
			[
				{target: this.iButtonParameters, constraint: BorderConstraints.RIGHT} //
			];

//			this.addChild( this.iHBoxToolbar );
		}
		
		protected function configureHeader():void
		{
			this.iHBoxHeader.horizontalAlign = HorizontalAlignment.CENTER;
			this.iHBoxHeader.verticalAlign = VerticalAlignment.TOP;
			//allow this container to determine its optimal size
			this.iHBoxHeader.setSize(this.origDocWidth, 30);
			////this.iHBoxHeader.autoSize = true;
			//this.iHBoxHeader.debugMode = iDebugMode;
			this.iHBoxHeader.paddingTop = this.iHBoxHeader.paddingBottom = 1;
			this.iHBoxHeader.paddingRight = this.iHBoxHeader.paddingLeft = 2;
			this.iHBoxHeader.horizontalGap = 0;

			this.iTextFieldHeader = new TextField();
			this.iTextFieldHeader.height = 28;
			this.iTextFieldHeader.defaultTextFormat = this.iTextFormatHeader;
			this.iTextFieldHeader.text = " ";//
			if ( this.iAppConfig != null && this.iTextFieldHeader != null
				&& this.iAppConfig.getTagIndex("caption") >= 0 )
				this.iTextFieldHeader.text = this.iAppConfig.getValueText("caption");

			this.iHBoxHeader.configuration =
			[
				{target: this.iTextFieldHeader, constraint: BorderConstraints.LEFT, percentWidth: 100} //
			];

//			this.addChild( this.iHBoxToolbar );
		}
		
		protected function configureContent():void
		{
		}

		protected function configureContextMenu():void
		{
			this.iContextMenu = new ContextMenu();
			this.iContextMenu.hideBuiltInItems();
			this.iContextMenu.addEventListener(flash.events.ContextMenuEvent.MENU_SELECT, this.onContextMenuSelect);
			this.contextMenu = this.iContextMenu;
		}
		
		//----------------------------------------------------
		// public (overridable) properties

		override public function get className():String 
		{
			return "ff.ui.VParamSqlInformerMC";
		}
		
		//----------------------------------------------------
		// protected overrides
		
		override protected function ShowData() : void
		{
			// called e.g.on start and when iAppConfig or iDATA object changed
			if ( this.iTextFieldHeader != null && this.iAppConfig.getTagIndex("caption") >= 0 )
				this.iTextFieldHeader.text = this.iAppConfig.getValueText("caption");
		}
		
		
		//----------------------------------------------------
		// own protected (overridable) helpers

		/**
		 * When the edit button is toggled, we need to update it's
		 * includeInLayout configuration option to add or remove the
		 * parameters form from the layout.
		 */
		protected function _toggleButtonParameters(event:MouseEvent):void
		{
			this.EditParameters( this.iButtonParameters.selected );
		}

		protected function EditParameters(aBegin:Boolean, aCancel:Boolean = false):void
		{
			if ( aBegin )
			{
				this.showDisplayObjectParameters();
			}
			else
			{
				if ( this.iDisplayObjectParameters is DataGrid )
				{
					var aDataGrid:DataGrid = this.iDisplayObjectParameters as DataGrid;
					aDataGrid.destroyItemEditor();
				}
				
				if ( aCancel )
					this.hideDisplayObjectParameters( false );
				else
				{
					if ( this.iDisplayObjectParameters is FormUI )
					{
						(this.iDisplayObjectParameters as FormUI).fireCollect( false );
					}
					else if ( this.iDisplayObjectParameters is DataGrid )
					{
						this.enterParameters();
					}
				}
			}
		}
		
		protected function createDisplayObjectParameters():void
		{
			if ( this.iDisplayObjectParameters == null )
			{
				var aStageRect:Rectangle = this.getBounds( this.stage );
				if ( 1==1 || this.iSOConfig != null ) // embedded
				{
					var aFormUI:FormUI = new FormUI( <r><formHeader /><formWidth>240</formWidth><labelWidth>98</labelWidth></r> );//<noButtons>1</noButtons></r> );
					aFormUI.x = (aStageRect.left + aStageRect.right)/2 - aFormUI.form.width/2;
					if ( aFormUI.x < 0 )
						aFormUI.x = 0;
					aFormUI.y = aStageRect.top + this.iPaneContent.y;
					
					aFormUI.HandlerOnSubmitValidationSuccess = this.HandlerOnSubmitParameters;
					aFormUI.HandlerOnCancel = this.HandlerOnCancelParameters;

					aFormUI.contextMenu = this.iContextMenu;
					this.iDisplayObjectParameters = aFormUI;
					
					aFormUI.btnCancel.visible = true;
				}
				else
				{
					var aDataGrid : DataGrid = new DataGrid();
					
					aDataGrid.width = this.width - 2;
					aDataGrid.x = (aStageRect.left + aStageRect.right)/2 - aDataGrid.width/2;
					aDataGrid.y = aStageRect.top + this.iPaneContent.y;
					
					aDataGrid.showHeaders = false;
					aDataGrid.editable = true;
		
					var tagColumn:DataGridColumn = new DataGridColumn("Tag");
					tagColumn.editable = false;
					tagColumn.resizable = true;
					tagColumn.width = 50;
					aDataGrid.addColumn(tagColumn);
					
					var valColumn:DataGridColumn = new DataGridColumn("Val");
					valColumn.editable = true;
					valColumn.resizable = true;
					valColumn.width = aDataGrid.width - tagColumn.width;
					aDataGrid.addColumn(valColumn);
					
					aDataGrid.setSize(this.origDocWidth, this.origDocHeight);			
					tagColumn.width = tagColumn.minWidth = aDataGrid.width / 3;
					valColumn.width = valColumn.minWidth = aDataGrid.width * 2 / 3 + aDataGrid.width % 3;
	
					this.iDisplayObjectParameters = aDataGrid;
				}
			}
		}

		protected function showDisplayObjectParameters():void
		{
			while ( this.iQueryQueue.length > 0 )
				this.iQueryQueue.pop();
			this.iDataLoader.timer.reset();
			
			if ( this.iDisplayObjectParameters == null )
				this.createDisplayObjectParameters();
				
			if ( this.iDisplayObjectParameters != null )
				this.stage.addChild( this.iDisplayObjectParameters );
			else
				return;
				
			this.showParameters();
		}

		protected function hideDisplayObjectParameters( aReload:Boolean = true):void
		{
			try
			{
				if ( this.iDisplayObjectParameters != null )
				{
					this.stage.removeChild( this.iDisplayObjectParameters );
				}
			}
			catch(e:Error)
			{
				trace( "AppConfig::str2Date: " + e.message);
			}
			 
			this.iDisplayObjectParameters = null;

			if ( aReload )
			{
				// refresh view
				this.ShowData();
				
				// restart loading
				if ( this.iFirstLoad == true )
				{
					if ( this.iAppConfig != null && this.iAppConfig.getTagIndex("queryStartup") >= 0 )
						this.iQueryQueue.push( this.constructCommandByPrefix( "queryStartup" ) );
					this.iFirstLoad = false;
				}
				else if ( this.iAppConfig.getTagIndex("queryRecurring") >= 0 )
				{
					this.iQueryQueue.push( this.constructCommandByPrefix( "queryRecurring" ) );
				}
				this.nextLoadData();
			}
		}
		
		
		protected function showParameters():void
		{
			var aArray:Array = null;
			if ( this.iDisplayObjectParameters is DataGrid )
			{
				aArray = new Array();
				if ( this.iAppConfig != null )
				{
					this.iAppConfig.getPairs( aArray );
				}
				(this.iDisplayObjectParameters as DataGrid).dataProvider = new DataProvider( aArray );//;new DataProvider([ {Tag:"SQL", Val:this.iQueryQueue[0]},  {Tag:"Sec", Val:this.iDataLoader.timer.delay/1000} ]); 
			}
			else if ( this.iDisplayObjectParameters is FormUI )
			{
				var aFormUI:FormUI = ( this.iDisplayObjectParameters as FormUI);
				aFormUI.iFrozen = false;
				var aRegExpr:RegExp = /(\w|)+.FormItem/;///xsl.FormItem/;
				// {t:<v.@t>, a:<v.@t>, v:<v.toString()}
				var i:int;
				aArray = this.iAppConfig.getArray(aRegExpr, false);
				var aFiW:Object = null;
				var aX:XML = null;
				if ( aArray != null && aArray.length > 0 )
				{
					for( i=0; i < aArray.length; i++ )
					{
						try
						{
							aX = new XML( aArray[i].v );
							aX.normalize();

							aFiW = new Object;
							AppConfig.parseXML(aX,aFiW,false);
							
							aFormUI.addFormItem( aArray[i].t, aX.text(), aFiW );
						}
						catch(error:Error)
						{
							trace(error);
						}
					}
				}
			}
		}

		protected function enterParameters( aCollectedData:* = null ):void
		{
			if ( this.iDisplayObjectParameters is DataGrid )
			{
				var aDataGrid:DataGrid = this.iDisplayObjectParameters as DataGrid;
				try
				{
					for( var i:Number=0; i < aDataGrid.dataProvider.length; i++ )
					{
						var aItem:Object = aDataGrid.dataProvider.getItemAt(i);
						this.iAppConfig.replaceValueText( aItem.Tag, aItem.Val );
					}
				}
				catch (aError:Error) 
				{
					trace("VParamSqlInformerMC.enterParameters: " + aError);
				}
			}
			else if( this.iDisplayObjectParameters is FormUI )
			{
				// TODO: assign changed values back to this.iConfigDB:
				var aT:String = null;
				var aV:String = null;
				var aX:XML = null;
				var aO:Object = null;
	
				if ( aCollectedData != null )
				{
					for (aT in aCollectedData) 
					{  
						this.iAppConfig.replaceValueText( aT, aCollectedData[aT] );
/*						
						aX = this.iAppConfig.getNode(aT);
						if ( aX != null )
						{
							try
							{
								var aXs:XMLList = aX.elements("*");
								aX.replace("*", AppConfig.cdata(aCollectedData[aT]));
								aX.appendChild(	aXs );		
							}
							catch(error:Error)
							{
								trace(error);
							}
						}
*/						
					}
				}
			}

			try
			{
				this.validate_AppConfig();
				this.notify_AppConfig();
			}
			catch (aError:Error) 
			{
				trace("VParamSqlInformerMC.enterParameters: " + aError);
			}

			this.hideDisplayObjectParameters();
		}



		protected function onContextMenuSelect( aEvent:flash.events.ContextMenuEvent ): void
		{
			this.iMenuULPoint = new Point( this.stage.mouseX, this.stage.mouseY ); 
			this.iContextMenu.customItems = new Array;
			this.iContextMenu.hideBuiltInItems();

			if ( this.iDisplayObjectParameters != null && this.iDisplayObjectParameters.visible )
			{
				var aContextMenuItem1:ContextMenuItem = new ContextMenuItem("Apply ");
				this.iContextMenu.customItems.push(aContextMenuItem1);
				aContextMenuItem1.addEventListener(ContextMenuEvent.MENU_ITEM_SELECT, this.onContextMenuItemSelectHandler);

				var aContextMenuItem2:ContextMenuItem = new ContextMenuItem("Cancel ");
				this.iContextMenu.customItems.push(aContextMenuItem2);
				aContextMenuItem2.addEventListener(ContextMenuEvent.MENU_ITEM_SELECT, this.onContextMenuItemSelectHandler);
			}
			else
		    {
				var aContextMenuItem:ContextMenuItem = new ContextMenuItem("Edit ");
				this.iContextMenu.customItems.push(aContextMenuItem);
				aContextMenuItem.addEventListener(ContextMenuEvent.MENU_ITEM_SELECT, this.onContextMenuItemSelectHandler);
			}
		}

		protected function onContextMenuItemSelectHandler(event:ContextMenuEvent):void 
		{
			var aDataGrid:DataGrid = this.iDisplayObjectParameters as DataGrid;
			if ( event.target.caption.length >= 5 && event.target.caption.substr(0, 5) == "Edit " )
			{
				this.iButtonParameters.selected = true;
				this.EditParameters( true );
			}
			else if ( event.target.caption.length >= 6 && event.target.caption.substr(0, 6) == "Apply " )
			{
				this.iButtonParameters.selected = false;
				this.EditParameters( false );
			}
			else if ( event.target.caption.length >= 5 && event.target.caption.substr(0, 7) == "Cancel " )
			{
				this.iButtonParameters.selected = false;
				this.EditParameters( false, true );
			}
			this.iButtonParameters.setFocus();
        }

		protected function HandlerOnSubmitParameters( aSender:Object, aCollectedData:* = null) : void
		{
			try 
			{
				if ( aSender is FormUI )
				{
					this.enterParameters( aCollectedData );
				}
			} 
			catch (aError:Error) 
			{
				trace(aError);
			}
		}
		
		protected function HandlerOnCancelParameters( aSender:Object )
		{
			this.iButtonParameters.selected = false;
			this.EditParameters( false, true );
			this.iButtonParameters.setFocus();
		}

	}

}
