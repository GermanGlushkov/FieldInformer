/*
Copyright (c) 2009 Yahoo! Inc.  All rights reserved.  
The copyrights embodied in the content of this file are licensed under the BSD (revised) open source license
*/
package {	import fl.controls.Button;	import fl.controls.CheckBox;	import fl.controls.ComboBox;	import fl.controls.TextInput;	import fl.data.DataProvider;	import com.yahoo.astra.containers.formClasses.FormLayoutStyle;	import com.yahoo.astra.events.FormDataManagerEvent;	import com.yahoo.astra.fl.containers.Form;	import com.yahoo.astra.fl.utils.FlValueParser;	import com.yahoo.astra.layout.LayoutContainer;	import com.yahoo.astra.layout.modes.BoxLayout;	import com.yahoo.astra.managers.FormDataManager;	import flash.events.MouseEvent;		/**	 * @author kayoh	 */	public class ParametersForForm extends ParametersContactForm {		//--------------------------------------		//  Constructor		//--------------------------------------		public function ParametersForForm() {			initParameterContainer();		}		//--------------------------------------		//  Properties		//--------------------------------------		private var generateButton : Button;		private var prarameterContainer : Form;		private var contactForm : Form;		private var formDataManager : FormDataManager;		private var showErrorTextFormItem : LayoutContainer;		private var showErrorBoxFormItem : LayoutContainer;		private var ErrBoxCheckBoxClr : CheckBox;		private var ErrBoxCheckBoxAlpha : CheckBox; 		private var ErrTextCheckBox : CheckBox;		//--------------------------------------		//  Private Methods		//--------------------------------------		private function initParameterContainer() : void {			// Iinit UIs			var labelHeadingInput : TextInput = new TextInput();			labelHeadingInput.width = 250;			labelHeadingInput.text = "Contact Us.";						//subHeadLabel			var subFormHeadingInput : TextInput = new TextInput();			subFormHeadingInput.width = 150;			subFormHeadingInput.text = " is required field." ;			var subFormHeadingCheckBox : CheckBox = new CheckBox();			subFormHeadingCheckBox.label = "(asterisk";			subFormHeadingCheckBox.width = 80;						var autoSizeInput : CheckBox = new CheckBox();			autoSizeInput.label = "true;";			autoSizeInput.selected = true;						var labelAlignInput : ComboBox = attLabelAlignComboBox();						var labelWidthInput : TextInput = new TextInput();						var indicatorLocationInput : ComboBox = attRequiredLocationComboBox();								//SetStyles			//skin			var skinStyleCheckBox : CheckBox = new CheckBox(); 			skinStyleCheckBox.label = "(" + '"' + "skin" + '"' + ", " + '"' + "FormSkin" + '"' + ")";			skinStyleCheckBox.width = 250;									//indicatorSkin			var indicatorSkinStyleCheckBox : CheckBox = new CheckBox(); 			indicatorSkinStyleCheckBox.label = "(" + '"' + "indicatorSkin" + '"' + ", " + '"' + "customReqIndicator" + '"' + ")";			indicatorSkinStyleCheckBox.width = 250;						//textFormat			var textFormatCheckBox : CheckBox = new CheckBox(); 			textFormatCheckBox.label = "(" + '"' + "textFormat" + '"' + ",\n new TextFormat(" + '"' + "Times" + '"' + ", 12, 0xFF0000))";			textFormatCheckBox.width = 250;						//headTextFormat			var headTextFormatFormatCheckBox : CheckBox = new CheckBox(); 			headTextFormatFormatCheckBox.label = "(" + '"' + "headTextFormat" + '"' + ",\n new TextFormat(" + '"' + "Times" + '"' + ", 12, 0x00FF00))";			headTextFormatFormatCheckBox.width = 250;						//instructionTextFormat			var instructionTextFormatCheckBox : CheckBox = new CheckBox(); 			instructionTextFormatCheckBox.label = "(" + '"' + "instructionTextFormat" + '"' + ",\n new TextFormat(" + '"' + "Times" + '"' + ", 12, 0x0000FF))";			instructionTextFormatCheckBox.width = 250;							//Error handling			var showErrorMessageTextInput : CheckBox = new CheckBox();			showErrorMessageTextInput.label = "true;";			showErrorMessageTextInput.addEventListener(MouseEvent.CLICK, handlerShowErrTextOption);						var mode : BoxLayout = new BoxLayout();			mode.direction = "vertical";      			showErrorTextFormItem = new LayoutContainer(mode);			showErrorTextFormItem.addChild(showErrorMessageTextInput);			var showErrorMessageBoxInput : CheckBox = new CheckBox();			showErrorMessageBoxInput.label = "true;";			showErrorMessageBoxInput.addEventListener(MouseEvent.CLICK, handlerShowErrBoxOption);									showErrorBoxFormItem = new LayoutContainer(mode);			showErrorBoxFormItem.addChild(showErrorMessageBoxInput);							ErrBoxCheckBoxClr = new CheckBox(); 			ErrBoxCheckBoxClr.label = "setStyle(" + '"' + "errorBoxColor" + '"' + ", " + '"' + "0xff0000" + '"' + ")";			ErrBoxCheckBoxClr.width = 250;      			ErrBoxCheckBoxAlpha = new CheckBox(); 			ErrBoxCheckBoxAlpha.label = "setStyle(" + '"' + "errorBoxAlpha" + '"' + ", " + ".5" + ")";			ErrBoxCheckBoxAlpha.width = 250;						// gaps and paddings			//horizontalGap			var horizontalGapTextInput : TextInput = numberInputText();			horizontalGapTextInput.text = FormLayoutStyle.DEFAULT_HORIZONTAL_GAP.toString();						//verticalGap			var verticalGapTextInput : TextInput = numberInputText();			verticalGapTextInput.text = FormLayoutStyle.DEFAULT_VERTICAL_GAP.toString();						//itemHorizontalGap			var itemHorizontalGapTextInput : TextInput = numberInputText();			itemHorizontalGapTextInput.text = FormLayoutStyle.DEFAULT_FORMITEM_HORIZONTAL_GAP.toString();						//itemVerticalGap			var itemVerticalGapTextInput : TextInput = numberInputText();			itemVerticalGapTextInput.text = FormLayoutStyle.DEFAULT_FORMITEM_VERTICAL_GAP.toString();						//padding			var paddingsInput : TextInput = numberInputText();			paddingsInput.text = "0";						generateButton = new Button();			generateButton.label = "GENERATE FORM!";			generateButton.width = 250;						// Init prarameterContainer			prarameterContainer = new Form("Available Parameters for Form.");			prarameterContainer.autoSize = true;			prarameterContainer.setStyle("skin", "FormSkin");			prarameterContainer.horizontalGap = 0;			prarameterContainer.verticalGap = 15;			prarameterContainer.paddingLeft = prarameterContainer.paddingRight = prarameterContainer.paddingTop = prarameterContainer.paddingBottom = 10;						formDataManager = new FormDataManager(FlValueParser);  			prarameterContainer.formDataManager = formDataManager;  						formDataManager.addTrigger(generateButton, handlerGenerateButtonClicked);			// Init data Array to set dataSource.			var myFormDataArr : Array = [{label:"formHeading =", items:labelHeadingInput, id:"formHeading", source:labelHeadingInput},			{label:"subFormHeading", items:[subFormHeadingCheckBox,",",subFormHeadingInput,")"], id:"subFormHeading", source:subFormHeadingCheckBox, property:subFormHeadingInput},			{label:"autoSize =", items:autoSizeInput, id:"autoSize", source:autoSizeInput},			{label:"labelAlign =", items:labelAlignInput, id:"labelAlign", source:labelAlignInput},			{label:"labelWidth =", items:labelWidthInput, id:"labelWidth", source:labelWidthInput},			{label:"indicatorLocation =", items:indicatorLocationInput, id:"indicatorLocation", source:indicatorLocationInput},			{label:"setStyle", items:skinStyleCheckBox, id:"skin", source:skinStyleCheckBox, property:"FormSkin"},			{label:"setStyle", items:indicatorSkinStyleCheckBox, id:"indicatorSkin", source:indicatorSkinStyleCheckBox, property:"customReqIndicator"},			{label:"setStyle", items:textFormatCheckBox, id:"textFormat", source:textFormatCheckBox, property:{font:"Times", color:"0xff0000", size:"12"}},			{label:"setStyle", items:headTextFormatFormatCheckBox, id:"headTextFormat", source:headTextFormatFormatCheckBox, property:{font:"Times", color:"0x00ff00", size:"12"}},			{label:"setStyle", items:instructionTextFormatCheckBox, id:"instructionTextFormat", source:instructionTextFormatCheckBox, property:{font:"Times", color:"0x0000ff", size:"12"}},			{label:"horizontalGap=", items:[horizontalGapTextInput, "verticalGap = ", verticalGapTextInput], id:["horizontalGap", "verticalGap"], source:[horizontalGapTextInput,verticalGapTextInput]},			{label:"itemHorizontalGap=", items:[itemHorizontalGapTextInput, "itemVerticalGap = ", itemVerticalGapTextInput], id:["itemHorizontalGap", "itemVerticalGap"], source:[itemHorizontalGapTextInput,itemVerticalGapTextInput]},			{label:"paddingTop =", items:["paddingBottom = paddingLeft = paddingRight =", paddingsInput], id:["paddingTop","paddingBottom","paddingLeft","paddingRight"], source:[paddingsInput,paddingsInput,paddingsInput,paddingsInput]},			{label:"showErrorMessageText =", items:showErrorTextFormItem, id:"showErrorMessageText", source:showErrorMessageTextInput},			{label:"showErrorMessageBox =", items:showErrorBoxFormItem, id:"showErrorMessageBox", source:showErrorMessageBoxInput},			{label:"", items:generateButton}];									prarameterContainer.dataSource = myFormDataArr;						this.addChild(prarameterContainer);		}		private function handlerShowErrTextOption(e : MouseEvent) : void {			var cb : CheckBox = CheckBox(e.target);			if(cb.selected) {				ErrTextCheckBox = new CheckBox(); 				ErrTextCheckBox.label = "formDataManager.errorString = " + '"' + "DOH!  " + '"';				ErrTextCheckBox.width = 250;      				formDataManager.addItem("errorString", ErrTextCheckBox, "DOH!");				showErrorTextFormItem.addChild(ErrTextCheckBox);			} else {				formDataManager.removeItem("errorString");				showErrorTextFormItem.removeChild(ErrTextCheckBox);			}		}		private function handlerShowErrBoxOption(e : MouseEvent) : void {			var cb : CheckBox = CheckBox(e.target);			if(cb.selected) {				formDataManager.addItem("errorBoxColor", ErrBoxCheckBoxClr, "0xff6600");				formDataManager.addItem("errorBoxAlpha", ErrBoxCheckBoxAlpha, .8);				showErrorBoxFormItem.addChild(ErrBoxCheckBoxClr);				showErrorBoxFormItem.addChild(ErrBoxCheckBoxAlpha);			} else {				formDataManager.removeItem("errorBoxColor");				formDataManager.removeItem("errorBoxAlpha");				showErrorBoxFormItem.removeChild(ErrBoxCheckBoxClr);				showErrorBoxFormItem.removeChild(ErrBoxCheckBoxAlpha);			}		}						private function handlerGenerateButtonClicked(e : FormDataManagerEvent) : void {			generateNewContactForm();      /*			for(var i in FormDataManager.collectedData) {				trace(i, "::", FormDataManager.collectedData[i])			}*/		}		private function generateNewContactForm() : void {			if(contactForm) {				this.removeChild(contactForm);			}			contactForm = super.buildForm(FormDataManager.collectedData);			contactForm.x = prarameterContainer.width + 25;			this.addChild(contactForm);		}				private function numberInputText() : TextInput {			var textInput : TextInput = new TextInput();			textInput.width = 30;			textInput.restrict = "0-9";			textInput.maxChars = 3;			return textInput;		}		private function attRequiredLocationComboBox() : ComboBox {			var items : XML = <items>					        <item label="FormLayoutStyle.INDICATOR_LABEL_RIGHT" data="label_right" />					        <item label="FormLayoutStyle.INDICATOR_RIGHT" data="right" />					        <item label="FormLayoutStyle.INDICATOR_LEFT" data="left" />					    </items>;			var dp : DataProvider = new DataProvider(items);			var comboBox : ComboBox = new ComboBox();			comboBox.dataProvider = dp;			comboBox.width = 250;			return comboBox;		}		private function attLabelAlignComboBox() : ComboBox {			var items : XML = <items>					        <item label="FormLayoutStyle.RIGHT" data="right" />					        <item label="FormLayoutStyle.LEFT" data="left" />					        <item label="FormLayoutStyle.TOP" data="top" />					    </items>;			var dp : DataProvider = new DataProvider(items);			var comboBox : ComboBox = new ComboBox();			comboBox.dataProvider = dp;			comboBox.width = 250;			return comboBox;		}	}}