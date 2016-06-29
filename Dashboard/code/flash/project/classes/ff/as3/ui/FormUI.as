package ff.as3.ui { 

	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
	import flash.net.sendToURL;
	import flash.net.FileFilter;
	import flash.net.FileReference;

	import flash.xml.XMLDocument;
	import flash.xml.XMLNode;

	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.display.DisplayObject;

	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.MouseEvent;
	import flash.events.TextEvent;
	
	import fl.core.UIComponent;
	
	import fl.controls.Button;
	import fl.controls.TextArea;
	import fl.controls.TextInput;
	import fl.controls.Label;
	import fl.controls.NumericStepper;
	import fl.controls.ComboBox;
	import fl.controls.RadioButton;
	import fl.controls.RadioButtonGroup;
	
    import flash.geom.Rectangle;
    import flash.geom.Point;

	import flash.utils.getDefinitionByName;
	
	import flash.text.TextField;
	import flash.text.TextFormat;	
	import flash.text.TextFieldAutoSize;
	import flash.text.TextFormatAlign

	import com.adobe.as3Validators.as3DataValidation;
	
	import com.yahoo.astra.containers.formClasses.FormItem;
	import com.yahoo.astra.containers.formClasses.FormItemContainer;
	import com.yahoo.astra.containers.formClasses.FormLayoutStyle;

	import com.yahoo.astra.fl.containers.Form;

	import com.yahoo.astra.managers.FormDataManager;
	import com.yahoo.astra.events.FormDataManagerEvent;
	
	import com.yahoo.astra.fl.managers.AlertManager;
	import com.yahoo.astra.fl.utils.FlValueParser;
	import com.yahoo.astra.utils.IValueParser;
	import com.yahoo.astra.utils.ValueParser;
	
	import com.yahoo.astra.layout.LayoutContainer;
	import com.yahoo.astra.layout.modes.BoxLayout;

	import be.boulevart.as3.security.*;

	import ff.as3.data.DataLoader;
	import ff.as3.data.FileUploader;
	import ff.as3.data.AppConfig;
	
	import ff.as3.ui.skin.*;

	/**
	 * 
	 */
	public class FormUI extends Sprite 
	{
		//--------------------------------------
		//  Constructor
		//--------------------------------------
		public function FormUI(aParams:XML=null) 
		{
			super();
			
			this._create( aParams );

			this.addEventListener(MouseEvent.MOUSE_DOWN, this.OnMouseDown);
			this.addEventListener(MouseEvent.MOUSE_UP, this.OnMouseUp);
		}
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		protected var iSkinDefinitionPrefix:String = "skin_Form_";

		protected var iInputTextFormat:TextFormat = new TextFormat("Verdana", 12, 0x384848);
		protected var iStaticTextFormat:TextFormat = new TextFormat("Verdana", 12, 0x384848);

		protected var iHeadTextFormat:TextFormat = new TextFormat("Verdana", 16, 0x384848, true);
		//protected var iTextFormat:TextFormat = new TextFormat("Verdana", 16, 0x08f808);
		protected var iLabelTextFormat:TextFormat = new TextFormat("Verdana", 12, 0xfefefe, true);
		protected var iInstructionTextFormat:TextFormat = new TextFormat("Verdana", 10, 0x384848);

		protected var iHotButtonTextFormat:TextFormat = new TextFormat("Verdana", 12, 0xAC1C14, true);
		protected var iButtonTextFormat:TextFormat = new TextFormat("Verdana", 12, 0x384848, true);

		protected var iClassRef_item_label_skin:Class = null;

		protected var iForm : Form = null;
		
		protected var iBtnSubmit : Button = null;
		protected var iBtnCancel : Button = null;

		protected var iInputTextWidth:Number = 200;

		protected var iFormDataManager : FormDataManager = null;

		protected var iValidator : as3DataValidation = null;

		protected var iHandlerOnSubmitValidationSuccess:Function = null;
		protected var iHandlerOnCancel:Function = null;
		
		protected var iSilentDataCollection:Boolean = true;
		
		protected var iNumStepInputChangeBusy:Boolean = false;
		
		public var iFrozen:Boolean = true;
		
		public var iGroupRButton:Boolean = false;
		protected var iManagedItems = new Array;

		/**
		 * 'protected' constructor(s).
		 */
		protected function _create(aParams:XML)
		{
			// init outer container layout
			this.initStaticLayout(aParams);
		}
		
		protected function initStaticLayout(aParams:XML) : void
		{
			if ( aParams != null && aParams.hasOwnProperty("skinPrefix"))
				this.iSkinDefinitionPrefix = aParams.hasOwnProperty("skinPrefix");

			if ( this.iForm != null )
			{
				while ( this.iForm.numChildren > 0 )
					this.iForm.removeChildAt(0);
				this.removeChild( this.iForm );
			}

			//form
			var aFormHeadingString:String = null;
			try
			{
				if ( aParams != null && aParams is XML && aParams..formHeader.length() > 0)
					aFormHeadingString = aParams..FormHeader[0].toString();
			}
			catch(e:Error)
			{
			}
			this.iForm = new Form( aFormHeadingString );
			this.iForm.autoSize = true;
			//this.iForm.indicatorLocation = FormLayoutStyle.INDICATOR_RIGHT;
			this.iForm.verticalGap  = 2;
			this.iForm.horizontalGap  = 2;
			this.iForm.itemVerticalGap  = 2;
			this.iForm.itemHorizontalGap  = 2;
			//this.iForm.labelAlign = FormLayoutStyle.RIGHT;
			this.iForm.labelWidth = 88;
			try
			{
				if ( aParams != null && aParams is XML && aParams..labelWidth.length() > 0)
					this.iForm.labelWidth = aParams..labelWidth[0].toString();
			}
			catch(e:Error)
			{
			}
			
			this.iForm.width = 400;
			try
			{
				if ( aParams != null && aParams is XML && aParams..formWidth.length() > 0)
					this.iForm.width = aParams..formWidth[0].toString();
			}
			catch(e:Error)
			{
			}
	
			this.iInputTextWidth = this.iForm.width - this.iForm.labelWidth - 2 * this.iForm.horizontalGap - this.iForm.itemHorizontalGap;

			this.setStyleByEnvironment(this.iForm, "skin", this.iSkinDefinitionPrefix + "skin");
//			this.setStyleByEnvironment(this.iForm, "indicatorSkin", this.iSkinDefinitionPrefix + "indicatorSkin");
			this.iForm.setStyle( "indicatorSkin", this.iSkinDefinitionPrefix + "indicatorSkin");

			this.iForm.setStyle("headTextFormat", this.iHeadTextFormat);
			this.iForm.setStyle("instructionTextFormat", this.iInstructionTextFormat);
			this.iForm.setStyle("textFormat", this.iLabelTextFormat);
			
			this.iClassRef_item_label_skin = null;
			try
			{
				this.iClassRef_item_label_skin = getDefinitionByName(this.iSkinDefinitionPrefix + "item_label_skin") as Class;
			}
			catch(error:Error)
			{
				trace(this.className + ".initStaticLayout: " + error);
			}

			// Init validator to be used.
			this.iValidator = new as3DataValidation();
			// Init and Attach FormDataManager to Form to collect user input data.
			this.iFormDataManager = new FormDataManager( FlValueParser ); 
			this.iFormDataManager.functionValidationPassed = handlerValidationPassed;
			this.iFormDataManager.functionValidationFailed = handlerValidationFailed;
			//this.iFormDataManager.dataSource = null;

			// add static items:
			this.initStaticItems( aParams );
			
			this.iForm.formDataManager = this.iFormDataManager;  
			
			this.iFormDataManager.addTrigger(null, handlerDataCollectionSuccess, handlerDataCollectionFail);
			this.iFormDataManager.collectData();

			// Attach Form on the stage.
			this.addChild(this.iForm);
		}
		
		protected function initStaticItems(aParams:XML) : void
		{
			try
			{
				if ( aParams != null && aParams is XML && aParams..noButtons.length() > 0 && aParams..noButtons[0] == "1" )
					return;
			}
			catch(e:Error)
			{
			}
			// add static items:
			
			//submit button
			this.iBtnSubmit = new Button();
			this.iBtnSubmit.setStyle("textFormat", this.iHotButtonTextFormat);
			this.iBtnSubmit.width = this.iInputTextWidth / 2 - this.iForm.itemHorizontalGap;
			this.iBtnSubmit.label = "OK";
			this.iBtnSubmit.addEventListener(MouseEvent.CLICK, handlerClickSubmit);
			this.iBtnSubmit.visible = false;

			//cancel button
			this.iBtnCancel = new Button();
			this.iBtnCancel.setStyle("textFormat", this.iHotButtonTextFormat);
			this.iBtnCancel.width = this.iInputTextWidth / 2 - this.iForm.itemHorizontalGap;
			this.iBtnCancel.label = "Cancel";
			this.iBtnCancel.addEventListener(MouseEvent.CLICK, handlerClickCancel);
			this.iBtnCancel.visible = false;

			//var aFiHiddenEmpty : FormItem = new FormItem("", makeHiddenTextInput());

			var aFiUpload : FormItem = new FormItem("", this.iBtnSubmit, this.iBtnCancel);
			aFiUpload.itemAlign = FormLayoutStyle.HORIZONTAL;
			this.iForm.addItem( aFiUpload );
		}
		
		public function addFormItem(aID:String, aData:String, aFiW:Object)  : void
		{
			if ( aFiW != null )
			{
				var aFi : FormItem = null;
				var aTextInput:TextInput = null;
				var aRequired:Boolean = aFiW.hasOwnProperty("r") ? aFiW.r : true;
				var aErrorString:String = "";
				var aValidatorFunc:Function = null;
				var aLabelPlaceholder : Label = null;
				var aRButton:RadioButton = null;
				var aControls:Array = new Array();
	
				if ( this.iGroupRButton )
				{
					aRButton = new RadioButton();
					aRButton.group = new RadioButtonGroup(aID);
					aRButton.label = "";
					aRButton.alpha = 0.66;
					aRButton.selected = true;
					aRButton.width = aRButton.height;
					aRButton.addEventListener(MouseEvent.CLICK, this.OnGroupRButtonClick)
				}
					
				if ( aFiW.hasOwnProperty("t") && aFiW.t == "DoubleClick" )
				{
					aTextInput = this.makeTextInput(aData == null ? "" : aData);
					aTextInput.editable = false;
					aTextInput.doubleClickEnabled = true;
					aTextInput.addEventListener(flash.events.MouseEvent.DOUBLE_CLICK, this.handlerBrowseInputDoubleClick);
					aTextInput.addEventListener(flash.events.Event.CHANGE, this.handlerTextInputChange);
						
					aLabelPlaceholder = new Label();
					aLabelPlaceholder.width = (this.iForm.horizontalGap > 1 ? this.iForm.horizontalGap : 1);
					aLabelPlaceholder.visible = false;

					aControls.push( aTextInput );
					aControls.push( aLabelPlaceholder );
				}
				else if ( aFiW.hasOwnProperty("t") && aFiW.t == "Static" )
				{
					this.iForm.addItem( new FormItem("", this.makeStaticText(aFiW.hasOwnProperty("l") ? aFiW.l : (aData != null ? aData : ""))), false );
				}
				else if ( aFiW.hasOwnProperty("t") && aFiW.t == "Combo" )
				{
					var aDP:XML = null;
					try
					{
						aDP = new XML( aFiW.s );
						aDP.normalize();
					}
					catch(error:Error)
					{
						aDP = null;
						trace(error);
					}
					aTextInput = this.makeTextInput( aData != null ? aData : "" );
					aTextInput.addEventListener(flash.events.Event.CHANGE, this.handlerTextInputChange);
					aTextInput.visible = false;
					aTextInput.width = (this.iForm.horizontalGap > 1 ? this.iForm.horizontalGap : 1);

					var aCombo:ComboBox = this.makeComboBox(aDP);
					
					if ( aTextInput.text != "" )
					{
						var aI:int = 0;
						var aArray:Array = aCombo.dataProvider.toArray();
						for ( aI = 0; aArray != null && aI < aArray.length; aI++ )
						{																					 
							if ( aArray[aI].data == aTextInput.text )
							{
								aCombo.selectedItem = aArray[aI];
								break;
							}
						}
					}
					aTextInput.text = aCombo.selectedItem.data;

					aCombo.addEventListener(Event.CHANGE, this.handlerComboInputChange);
						
					aControls.push( aCombo );
					aControls.push( aTextInput );
					//aFi = new FormItem(" " + (aFiW.hasOwnProperty("l") ? aFiW.l : ""), aCombo, aTextInput);
				}
				else if ( aFiW.hasOwnProperty("t") && aFiW.t == "Number" )
				{
					var aNumInput:NumericStepper = this.makeNumericStepper(aData != null ? aData : "");
					aNumInput.textField.addEventListener(flash.events.Event.CHANGE, this.handlerTextInputChange);
					aTextInput = aNumInput.textField;

					aControls.push( aNumInput );
					//aFi = new FormItem(" " + (aFiW.hasOwnProperty("l") ? aFiW.l : ""), aNumInput);
				}
				else if ( aFiW.hasOwnProperty("t") && aFiW.t == "Date" )
				{
					var aDate:Date = new Date(); //now
					if ( aData != null && aData.length == 8 )
					{
						try
						{
							aDate = AppConfig.str2Date( aData );
						}
						catch (aError:Error) 
						{
							trace(this.className + ".addFormItem: " + aError);
						}
					}
					
					aTextInput = this.makeTextInput( AppConfig.date2Str(aDate) );
					aTextInput.addEventListener(flash.events.Event.CHANGE, this.handlerTextInputChange);
					aTextInput.visible = false;
					aTextInput.width = 1;

					var aY : NumericStepper =  this.makeNumericStepper(aDate.fullYear);
					aY.minimum = 0;
					aY.maximum = 9999;
					aY.value = aDate.fullYear;
					aY.addEventListener(Event.CHANGE,handlerDateInputChange);
					aY.addEventListener(TextEvent.TEXT_INPUT,handlerDateInputChange);
					aY.width = 99;
					
					var aM : NumericStepper =  this.makeNumericStepper(aDate.month+1);
					aM.minimum = 1;
					aM.maximum = 12;
					aM.value = aDate.month+1;
					aM.addEventListener(Event.CHANGE,handlerDateInputChange);
					aM.addEventListener(TextEvent.TEXT_INPUT,handlerDateInputChange);
					aM.width = 49;
					
					var aD : NumericStepper =  this.makeNumericStepper(aDate.date);
					aD.minimum = 1;
					aD.maximum = 31;
					aD.value = aDate.date;
					aD.addEventListener(Event.CHANGE,handlerDateInputChange);
					aD.addEventListener(TextEvent.TEXT_INPUT,handlerDateInputChange);
					aD.width = 49;
					
					aControls.push( aY );
					aControls.push( aM );
					aControls.push( aD );
					aControls.push( aTextInput );
					//aFi = new FormItem(" " + (aFiW.hasOwnProperty("l") ? aFiW.l : ""), aY, aM, aD, aTextInput );
				}
				else if ( aFiW.hasOwnProperty("t") && aFiW.t == "Money" )
				{
					var aInt : int = 0;
					var aDec2 : int = 0;
					
					var aMoney : Number = 0;
					try
					{
						aMoney = new Number( aData );
					}
					catch(e:Error)
					{
					}
					aInt = Math.floor( Math.abs(aMoney) );
					aDec2 = Math.floor( Math.abs(aMoney) * 100 - aInt * 100 );
					
					aMoney = aInt + aDec2 / 100.
					
					aTextInput = this.makeTextInput( aMoney.toFixed(2) );
					
					aTextInput.addEventListener(flash.events.Event.CHANGE, this.handlerTextInputChange);
					aTextInput.visible = false;
					aTextInput.width = 1;

					var aNsI : NumericStepper =  this.makeNumericStepper(aInt);
					aNsI.minimum = 0;
					aNsI.maximum = 99999;
					aNsI.value = aInt;
					aNsI.addEventListener(Event.CHANGE,handlerMoneyInputChange);
					aNsI.addEventListener(TextEvent.TEXT_INPUT,handlerMoneyInputChange);
					aNsI.width = 159;
					aNsI.setStyle("textAlign", "right");
					var aNsIF:TextFormat = new TextFormat();
					with (aNsIF)
					{
						align = TextFormatAlign.RIGHT;
						font = "Verdana";
						size = 12;
						bold = true;
					}
					aNsI.setStyle( "textFormat", aNsIF );
					
					var aLabel:Label = this.makeStaticText("€," );
					aLabel.setStyle( "textFormat", aNsIF );
					aLabel.width = aLabel.textField.getLineMetrics(0).width + 8;
					
					var aNsD2 : NumericStepper =  this.makeNumericStepper(aDec2);
					aNsD2.minimum = 0;
					aNsD2.stepSize = 5;
					aNsD2.maximum = 99;
					aNsD2.value = aDec2;
					aNsD2.addEventListener(Event.CHANGE,handlerMoneyInputChange);
					aNsD2.addEventListener(TextEvent.TEXT_INPUT,handlerMoneyInputChange);
					aNsD2.width = 49;
					
					aControls.push( aNsI );
					aControls.push( aLabel );
					aControls.push( aNsD2 );
					aControls.push( aTextInput );
					//aFi = new FormItem(" " + (aFiW.hasOwnProperty("l") ? aFiW.l : ""), aY, aM, aD, aTextInput );
				}
				else
				{
					aTextInput = this.makeTextInput(aData == null ? "" : aData);
					aTextInput.addEventListener(flash.events.Event.CHANGE, this.handlerTextInputChange);
						
					aLabelPlaceholder = new Label();
					aLabelPlaceholder.width = (this.iForm.horizontalGap > 1 ? this.iForm.horizontalGap : 1);
					aLabelPlaceholder.visible = false;
					
					aControls.push( aTextInput );
					aControls.push( aLabelPlaceholder );
					//aFi = new FormItem(" " + (aFiW.hasOwnProperty("l") ? aFiW.l : ""), aTextInput, aLabelPlaceholder);
				}

				if ( aControls.length > 0 )
				{
					if ( aRButton != null )
						aFi = new FormItem(" " + (aFiW.hasOwnProperty("l") ? aFiW.l : ""), aRButton, aControls);
					else
						aFi = new FormItem(" " + (aFiW.hasOwnProperty("l") ? aFiW.l : ""), aControls);

					if ( this.iClassRef_item_label_skin != null )
						aFi.skin = new this.iClassRef_item_label_skin();
					if ( aFiW.hasOwnProperty("it") )
						aFi.instructionText = aFiW.it;
					aFi.itemAlign = FormLayoutStyle.HORIZONTAL;
					aFi.itemHorizontalGap = 1;
					this.iForm.addItem( aFi, aRequired );
				}
				
				if ( aTextInput != null )
				{
					aErrorString = (aFiW.hasOwnProperty("l") ? aFiW.l : aID);
					while ( aErrorString.length > 0 && ( aErrorString.substr( aErrorString.length - 1 ) == ":" || aErrorString.substr( aErrorString.length - 1 ) == " " ) )
						aErrorString = aErrorString.substr(0, aErrorString.length - 1);
					aValidatorFunc = this.iValidator.isNotEmpty;
					if ( aFiW.hasOwnProperty("vf") )
					{
						aValidatorFunc = AppConfig.getFuncByName( this.iValidator, aFiW.vf );
					}
					if ( aValidatorFunc == null && aRequired )
						aValidatorFunc = this.iValidator.isNotEmpty;
						
					if ( aValidatorFunc == this.iValidator.isNotEmpty )
						aErrorString += " is empty.";
					else if ( aValidatorFunc == this.iValidator.isDigit )
						aErrorString += " is not Digital.";
					else if ( aValidatorFunc == this.iValidator.isIP )
						aErrorString += " is not ip.";
					else if ( aValidatorFunc == this.iValidator.isHttpURL )
						aErrorString += " is not Http URL.";
					else if ( aValidatorFunc == this.iValidator.isEmail )
						aErrorString += " is not Email.";
					else 
						aErrorString += " is wrong.";
					
					this.iManagedItems.push( {"id" : aID, "source" : aTextInput, "property" : null, "required" : true , "validation" : aValidatorFunc, "validatorExtraParam" : null, "eventTargetObj" : aFi, "functionValidationPassed" : null, "functionValidationFailed" : null, "errorString" : aErrorString, "excluded" : false, "exclButton" : aRButton } );
					this.iFormDataManager.addItem(aID, aTextInput, null, true, aValidatorFunc, null, aFi, null, null, aErrorString);
	
					this.iFormDataManager.collectData();
				}
			}
		}

		//----------------------------------------------------
		// delegates
		
		public function set HandlerOnSubmitValidationSuccess( aHandler:Function )
		{
			this.iHandlerOnSubmitValidationSuccess = aHandler;
		}
		
		public function set HandlerOnCancel( aHandler:Function )
		{
			this.iHandlerOnCancel = aHandler;
			if ( this.iBtnCancel != null )
				this.iBtnCancel.visible = ( aHandler != null );
		}
		
		//----------------------------------------------------
		// public properties
		
		public function get className():String 
		{
			return "ff.ui.FormUI";
		}
		
		public function get form() : Form 
		{
			return this.iForm;
		}
		
		public function get inputTextWidth():Number
		{
			return this.iInputTextWidth;
		}
		public function set inputTextWidth( aNum:Number ):void
		{
			this.iInputTextWidth = aNum;
		}

		public function get btnSubmit() : Button 
		{
			return this.iBtnSubmit;
		}

		public function get btnCancel() : Button 
		{
			return this.iBtnCancel;
		}
		
		//----------------------------------------------------
		// public helpers
		
		public function fireCollect(aSilent:Boolean = true) 
		{
			this.iSilentDataCollection = aSilent;
			try
			{
				this.iFormDataManager.collectData();
			}
			catch(error:Error)
			{
				trace(this.className + ".fireCollect: " + error);
			}
			this.iSilentDataCollection = true;
		}


		//----------------------------------------------------
		// protected helpers
		
		protected function setStyleByEnvironment( aUIComponent:UIComponent, aStyleName:String, aDefinitionName:String):void
		{
			try
			{
				var aClassRef:Class = getDefinitionByName(aDefinitionName) as Class;
				if ( aClassRef != null )
				{
					//trace( classRef ); 
					aUIComponent.setStyle(aStyleName, new aClassRef());
				}
			}
			catch(error:Error)
			{
				trace(this.className + ".setStyleByEnvironment: " + error);
			}
		}

		protected function makeTextInput(aParams:* = null) : TextInput 
		{
			var aInput : TextInput = new TextInput();
			//aInput.maxChars = 35;
			aInput.width = this.iInputTextWidth;
			if ( aParams != null )
			{
				if(aParams is String)
					aInput.text = aParams;
				else if(aParams is XML)
					aInput.text = aParams.toString();
			}
			this.setStyleByEnvironment(aInput, "upSkin", this.iSkinDefinitionPrefix + "TextInput_upSkin");
			aInput.setStyle("textFormat", this.iInputTextFormat);
			this.setStyleByEnvironment(aInput, "focusRectSkin", this.iSkinDefinitionPrefix + "TextInput_focusRectSkin");
			return aInput;
		}
		
		protected function makeNumericStepper(aParams:* = null) : NumericStepper 
		{
			var aInput : NumericStepper = new NumericStepper();
			//aInput.maxChars = 35;
			aInput.width = this.iInputTextWidth;
			aInput.minimum = 0;
			aInput.maximum = 8888;
			if ( aParams != null )
			{
				if(aParams is Array)
					aInput.value = aParams[0];
				else
					aInput.value = aParams;
			}
			this.setStyleByEnvironment(aInput.textField, "upSkin", this.iSkinDefinitionPrefix + "TextInput_upSkin");
			this.setStyleByEnvironment(aInput, "TextInput_upskin", this.iSkinDefinitionPrefix + "TextInput_upSkin");
			aInput.textField.setStyle("textFormat", this.iInputTextFormat);
			this.setStyleByEnvironment(aInput, "focusRectSkin", this.iSkinDefinitionPrefix + "TextInput_focusRectSkin");
			return aInput;
		}
		
		protected function makeComboBox(aParams:* = null) : ComboBox 
		{
			var aInput : ComboBox = new ComboBox();
			//aInput.maxChars = 35;
			aInput.width = this.iInputTextWidth;
			if ( aParams != null )
			{
				if(aParams is Array)
					aInput.dataProvider = aParams;
				else if(aParams is XML)
				{
					var aXMLList:XMLList = aParams.children();
					for ( var a:Number = 0; aXMLList != null && a < aXMLList.length(); a++ )
					{
						var aLabel:String = "";
						if ( aXMLList[a].hasOwnProperty("@l") )
						{
							aLabel = aXMLList[a].@l;
						}
						else if ( aXMLList[a].child("l") != null && aXMLList[a].child("l").length() > 0 )
						{
							aLabel = aXMLList[a].child("l")[0].toString();
						}
						var aData:String = aLabel;
						if ( aXMLList[a].hasOwnProperty("@d") )
						{
							aData = aXMLList[a].@d;
						}
						else if ( aXMLList[a].child("d") != null && aXMLList[a].child("d").length() > 0 )
						{
							aData = aXMLList[a].child("d")[0].toString();
						}
						aInput.addItem( { label: aLabel, data:aData } );
					}
					aInput.selectedIndex = 0;
				}
			}
//			this.setStyleByEnvironment(aInput.textField, "upSkin", this.iSkinDefinitionPrefix + "TextInput_upSkin");
//			this.setStyleByEnvironment(aInput, "TextInput_upskin", this.iSkinDefinitionPrefix + "TextInput_upSkin");
//			aInput.textField.setStyle("textFormat", this.iInputTextFormat);
			this.setStyleByEnvironment(aInput, "focusRectSkin", this.iSkinDefinitionPrefix + "TextInput_focusRectSkin");
			return aInput;
		}

		protected function makeStaticText(aParams:* = null) : Label 
		{
			var aLabel : Label = new Label();
			//aInput.maxChars = 35;
			aLabel.width = this.iInputTextWidth;
			if ( aParams != null )
			{
				if(aParams is String)
					aLabel.text = aParams;
			}
			aLabel.setStyle("textFormat", this.iStaticTextFormat);
			return aLabel;
		}


		protected function makeHiddenTextInput(aWidth:int=1) : TextInput 
		{
			var aInput : TextInput = new TextInput();
			aInput.visible = false;
			aInput.enabled = false;
			aInput.editable = false;
			aInput.width = 1;
			return aInput;
		}

		//----------------------------------------------------
		// event's handlers
		protected function handlerValidationPassed(e : FormDataManagerEvent) : void 
		{
			if(e.target is FormItem) 
			{
				var aFormItem : FormItem = e.target as FormItem;
				if(aFormItem.requiredIndicator) aFormItem.requiredIndicator.visible = false;
			}
			/*
			if(e.target is FormItem) {
				// Let's stop the indicator movieClip.
				var checkBox : DisplayObject = (e.target as FormItem).requiredIndicator;
				if(checkBox is MovieClip) { 
					checkBox["triangleMC"].gotoAndStop(1);
					checkBox.alpha = .3;
				}
			}
			*/
		}

		protected function handlerValidationFailed(e : FormDataManagerEvent) : void {
			if(e.target is FormItem) {
				var aFormItem : FormItem = e.target as FormItem;
				if(aFormItem.requiredIndicator) 
				{
					var aIndicator : Object = aFormItem.requiredIndicator
					aIndicator.visible = true;
				}
			}
			/*
			if(e.target is FormItem) {
				var checkBox : DisplayObject = (e.target as FormItem).requiredIndicator;
				if(checkBox is MovieClip) { 
					checkBox["triangleMC"].gotoAndPlay(1);
					checkBox.alpha = 1;
				}
			}
			*/
		}
		
		
		protected function handlerClickSubmit(e:MouseEvent) 
		{
			this.fireCollect( false );
		}
		
		
		protected function handlerClickCancel(e:MouseEvent) 
		{
			try
			{
				this.iHandlerOnCancel( this );
			}
			catch(error:Error)
			{
				trace(this.className + ".handlerClickCancel: " + error);
			}
		}
		

		protected function handlerDataCollectionFail(e : FormDataManagerEvent) : void 
		{
			var aResultTxt : String = "";
			//this.iBtnSubmit.visible = false;
			for (var i:String in FormDataManager.failedData) {  
				aResultTxt += /*i + " : " + */FormDataManager.failedData[i] + "\n\n";  
			} 
			if ( !this.iSilentDataCollection )
				AlertManager.createAlert(this, aResultTxt, "Problems:");
		}
		
		protected function handlerDataCollectionSuccess(e : FormDataManagerEvent) : void 
		{
			try
			{
				if ( !this.iSilentDataCollection )
				{
					for (var i:String in e.collectedData) 
					{  
						var val : Object = e.collectedData[i];
						// skip file to upload 
						if ( i == "File") 
						{
						} 
						else 
						{
						}
					}
					if ( this.iBtnSubmit != null )
						this.iBtnSubmit.enabled = false;
					if ( iHandlerOnSubmitValidationSuccess != null )
						this.iHandlerOnSubmitValidationSuccess( this, e.collectedData );
					if ( this.iBtnSubmit != null )
						this.iBtnSubmit.enabled = true;
				}
				else
				{
					if ( this.iBtnSubmit != null )
						this.iBtnSubmit.visible = true;
				}
			}
			catch(error:Error)
			{
				trace(this.className + "Error trying to upload file: " + error);
			}
		}
		
		protected function handlerTextInputChange(e:Event):void 
		{
			this.iFormDataManager.collectData();
		}
		
		protected function handlerComboInputChange( e:Event ):void
		{
			try
			{
				var aCombo:ComboBox = e.target as ComboBox;
				
				if( aCombo.parent != null )
				{
					var aContainer:LayoutContainer = aCombo.parent as LayoutContainer;
					var aOff:uint = 0;
					if ( aContainer.numChildren > 0 && (aContainer.getChildAt( 0 ) is RadioButton) )
						aOff = 1;
					if ( aContainer.numChildren > aOff + 1 )
					{
						var aT : TextInput = aContainer.getChildAt( aOff + 1 ) as TextInput;
						
						aT.text = aCombo.selectedItem.data;
					}
				}
			}
			catch(error:Error)
			{
				trace(this.className + "handlerComboInputChange: " + error);
			}
		}

		
		protected function handlerDateInputChange( e:Event ):void
		{
			if ( !this.iNumStepInputChangeBusy )
			{
				this.iNumStepInputChangeBusy = true;
				try
				{
					var aContainer:LayoutContainer = null;
					
					if ( e.target is NumericStepper )
					{
						aContainer = (e.target.parent as LayoutContainer);
					}
					else if ( e.target is TextInput )
					{
						aContainer = (e.target.parent.parent as LayoutContainer);
					}
					
					if( aContainer != null )
					{
						var aN1 : NumericStepper;
						var aN2 : NumericStepper;
						var aN3 : NumericStepper;
						var aT : TextInput;
						
						var aOff:uint = 0;
						if ( aContainer.numChildren > 0 && (aContainer.getChildAt( 0 ) is RadioButton) )
							aOff = 1;
						if ( aContainer.numChildren > aOff + 3 
							 && aContainer.getChildAt( aOff + 0 ) is NumericStepper
							 && aContainer.getChildAt( aOff + 1 ) is NumericStepper
							 && aContainer.getChildAt( aOff + 2 ) is NumericStepper
							 && aContainer.getChildAt( aOff + 3 ) is TextInput
							 )	// 
						{
							aN1 = aContainer.getChildAt( aOff + 0 ) as NumericStepper;
							aN2 = aContainer.getChildAt( aOff + 1 ) as NumericStepper;
							aN3 = aContainer.getChildAt( aOff + 2 ) as NumericStepper;
							
							if ( aN1 == e.target || aN2 == e.target.parent || aN2 == e.target || aN2 == e.target.parent )
							{
								var aMonthArray:Array=new Array("31","28","31","30","31","30","31","31","30","31","30","31");
								if(String(aN1.value/4).length==3) //leap year
								{
									aMonthArray=new Array("31","29","31","30","31","30","31","31","30","31","30","31");
								}
								if ( aN3.maximum != aMonthArray[aN2.value - 1] )
									aN3.maximum = aMonthArray[aN2.value - 1];
							}
							
							aT = aContainer.getChildAt( aOff + 3 ) as TextInput;
							aT.text = AppConfig.date2Str( new Date( aN1.value, aN2.value - 1, aN3.value ) );
						}
					}
				}
				catch(error:Error)
				{
					trace(this.className + "handlerDateInputChange: " + error);
				}
				this.iNumStepInputChangeBusy = false;
			}

			//trace(aDateStr);
		}

		protected function handlerMoneyInputChange( e:Event ):void
		{
			if ( !this.iNumStepInputChangeBusy )
			{
				this.iNumStepInputChangeBusy = true;
				try
				{
					var aContainer:LayoutContainer = null;
					
					if ( e.target is NumericStepper )
					{
						aContainer = (e.target.parent as LayoutContainer);
					}
					else if ( e.target is TextInput )
					{
						aContainer = (e.target.parent.parent as LayoutContainer);
					}
					
					if( aContainer != null )
					{
						var aN1 : NumericStepper;
						var aN2 : NumericStepper;
						var aN3 : NumericStepper;
						var aT : TextInput;
						
						var aOff:uint = 0;
						if ( aContainer.numChildren > 0 && (aContainer.getChildAt( 0 ) is RadioButton) )
							aOff = 1;
						if ( aContainer.numChildren > aOff + 2 
							 && aContainer.getChildAt( aOff + 0 ) is NumericStepper
							 // 1 - euro sign
							 && aContainer.getChildAt( aOff + 2 ) is NumericStepper
							 && aContainer.getChildAt( aOff + 3 ) is TextInput
							 )	// 
						{
							aN1 = aContainer.getChildAt( aOff + 0 ) as NumericStepper;
							aN2 = aContainer.getChildAt( aOff + 2 ) as NumericStepper;
							aT = aContainer.getChildAt( aOff + 3 ) as TextInput;
							var aMoney : Number = aN1.value + aN2.value / 100.
							aT.text =  aMoney.toFixed(2);
						}
					}
				}
				catch(error:Error)
				{
					trace(this.className + "handlerMoneyInputChange: " + error);
				}
				this.iNumStepInputChangeBusy = false;
			}

			//trace(aDateStr);
		}
		
		protected function handlerBrowseInputDoubleClick(e:MouseEvent) 
		{
		}
		
		public function ShowDebug(aMessage:String) 
		{
//			AlertManager.createAlert(this, aMessage, "DebugInfo");
		}
		
		
		protected function OnMouseDown(evt:MouseEvent):void 
		{
			if ( !this.iFrozen )
			{
				// limits dragging to the area inside the canvas
				var aBoundsRect:Rectangle = this.parent.getRect(this.parent);
				aBoundsRect.width -= this.width;
				aBoundsRect.height -= this.height;
				this.startDrag(false, aBoundsRect);
			}
		}
		
		protected function OnMouseUp(aMouseEvent:MouseEvent):void 
		{
			if ( !this.iFrozen )
				this.stopDrag();
		}
		
		protected function OnGroupRButtonClick( aMouseEvent:MouseEvent):void 
		{
			if ( aMouseEvent.target is RadioButton )
				this.ToggleExclusion( (aMouseEvent.target as RadioButton).group.name );
		}
		
		public function ToggleExclusion( aID : String ):void 
		{
			var aFormItem:Object = this.findFormItemByChild( aID );
			
			if ( aFormItem != null )
			{
				var aExclButton:RadioButton = aFormItem["exclButton"];
				if ( aFormItem.excluded )
				{
					this.iFormDataManager.addItem(aFormItem["id"], aFormItem["source"], aFormItem["property"], aFormItem["required"], aFormItem["validation"], aFormItem["validatorExtraParam"], aFormItem["eventTargetObj"], aFormItem["functionValidationPassed"], aFormItem["functionValidationFailed"], aFormItem["errorString"] );
					aFormItem["excluded"] = false;
				}
				else
				{
					var aTempR:RadioButton = new RadioButton()
					aTempR.group = aExclButton.group;
					aTempR.selected = true; 
					this.iFormDataManager.removeItem( aFormItem.id );
					aFormItem["excluded"] = true;
				}
				this.iFormDataManager.collectData();
				for ( var a:uint = 0; a < aExclButton.parent.numChildren; a++ )
				{
					var aChild:DisplayObject = aExclButton.parent.getChildAt( a );
					if ( aChild != aExclButton )
					{
						aChild.visible = !aFormItem.excluded;
					}
				}
			}
		}
		
		protected function findFormItemByChild( aID : String ) : Object
		{
			for ( var a:uint = 0; a < this.iManagedItems.length; a++ )
			{
				if ( this.iManagedItems[a]["id"] == aID )
					return this.iManagedItems[a];
			}
			return null;
		}

	}
}

