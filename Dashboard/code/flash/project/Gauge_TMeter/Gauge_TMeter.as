package
{
	import fl.controls.Button;
	
	import flash.display.Shape;
	import flash.display.MovieClip;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.display.DisplayObject;
	import flash.text.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.Regular;

	import flash.geom.Point;
	import flash.geom.Rectangle;

	import com.adobe.as3Validators.as3DataValidation;

	import com.yahoo.astra.fl.containers.Form;
	import com.yahoo.astra.fl.containers.HBoxPane;
	import com.yahoo.astra.fl.containers.VBoxPane;
	import com.yahoo.astra.fl.containers.BorderPane;
	import com.yahoo.astra.layout.modes.BorderConstraints;
	import com.yahoo.astra.layout.modes.VerticalAlignment;
	import com.yahoo.astra.layout.modes.HorizontalAlignment;
	import com.yahoo.astra.containers.formClasses.FormItem;
	import com.yahoo.astra.containers.formClasses.FormLayoutStyle;
	import com.yahoo.astra.fl.utils.FlValueParser;
	import com.yahoo.astra.utils.IValueParser;
	import com.yahoo.astra.utils.ValueParser;

	import ff.as3.data.AppConfig;
	import ff.as3.ui.VParamDataInformerMC;
	import ff.as3.ui.FormUI;

	/**
	 * 
	 */
	public class Gauge_TMeter extends VParamDataInformerMC
	{
		
	//--------------------------------------
	//  Constructor
	//--------------------------------------
	
		/**
		 * Constructor.
		 */
		public function Gauge_TMeter()
		{
			super();
			
			// Set the title and title color.
			var myFormat_fmt:TextFormat = new TextFormat();
			myFormat_fmt.align = "center";
			myFormat_fmt.bold = true;
			myFormat_fmt.color = 0xcccccc;
			
			txtMid.defaultTextFormat =myFormat_fmt;
			txtHigh.defaultTextFormat =myFormat_fmt;
			txtZero.defaultTextFormat =myFormat_fmt;
			
			txtZero.text = "0";
		}
	
		//-----------------------------------------------------------
		// constructor's helpers
		override protected function constructor_default_AppConfig() : void
		{
			super.constructor_default_AppConfig();
			try
			{
				var aToMerge:XML = new XML("<config>"
										   +"<v t='caption' a='.FormItem' ><![CDATA[T Gauge]]><o><v n='t' ><![CDATA[Text]]></v><v n='l' ><![CDATA[Name:]]></v><v n='it' ><![CDATA[Gauge's caption text.\n]]></v></o></v>"
										   +"<v t='frequency' a='.FormItem' ><![CDATA[88]]><o><v n='t' ><![CDATA[Number]]></v><v n='l' ><![CDATA[Frequency:]]></v><v n='it' ><![CDATA[Refresh frequency in seconds.\nTo change double click on it.]]></v></o></v>"
										   +"<v t='valueStart' a='.FormItem' ><![CDATA[0]]><o><v n='t' ><![CDATA[DoubleClick]]></v><v n='l' ><![CDATA[Start:]]></v><v n='it' ><![CDATA[Start value for the scale.\nTo change double click on it.]]></v></o></v>"
										   +"<v t='valueEnd' a='.FormItem' ><![CDATA[100]]><o><v n='t' ><![CDATA[DoubleClick]]></v><v n='l' ><![CDATA[End:]]></v><v n='it' ><![CDATA[End value for the scale.\nTo change double click on it.]]></v></o></v>"
										   +"<v t='valueStep' a='.FormItem' ><![CDATA[10]]><o><v n='t' ><![CDATA[DoubleClick]]></v><v n='l' ><![CDATA[Step:]]></v><v n='it' ><![CDATA[Unit of the scale.\nTo change double click on it.]]></v></o></v>"
										   +"<v t='value' a='.FormItem' ><![CDATA[50]]><o><v n='t' ><![CDATA[DoubleClick]]></v><v n='l' ><![CDATA[Data:]]></v><v n='it' ><![CDATA[Method of 'temperature reading'.\nTo change double click on it.]]></v></o></v>"

										   +"<v t='queryStartup' a='' ><![CDATA[select cast(rand()*100 as int), 0, 100]]></v>"
										   +"<v t='queryRecurring' a='' ><![CDATA[select cast(rand()*100 as int), 0, 100]]></v>"
										   
										   +"</config>");
				aToMerge.normalize();
				this.iAppConfig.mergeIn( aToMerge );
trace( this.iAppConfig.xml.toXMLString() ); 				
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
			return 150;
		}
		
		override protected function get origDocHeight():Number 
		{
			return 300;
		}

		override protected function configureContent():void
		{
			this.iPaneContent.x=0;
			this.iPaneContent.y=48;
			this.iPaneContent.setSize(160,270);
			this.iPaneContent.configuration = 
			[
			];
		}


		//----------------------------------------------------
		// protected (overridable) functions

		override protected function ShowData() : void
		{
			// show e.g. header
			super.ShowData();
			
			if ( this.iDATA == null )
				return;
			
			trace( this.iDATA );
		//	var aDataProvider:fl.data.DataProvider = new fl.data.DataProvider(aXML);
			var currVal:Number  = 69;//Num aXML.d.r.c1;
			var nullVal:Number = 0;
			var highVal:Number = 100;
			for each (var r:XML in this.iDATA..r) 
			{
				currVal  = r.c1;
				nullVal  = r.c2;
				highVal  = r.c3;
			}
			
			// Math and stuffs
			var barLength:Number = 174;
			//var offset    = 0; 
			var offset:Number    = 22; 
			var bar:Number = mercury_mc.height;
			var barValue:Number = (currVal*(barLength-offset))/highVal;
			//Ease the bar so it doesn't look crappy.
			var do_Tween:Object = new Tween(this.mercury_mc,"height", Regular.easeOut, bar, barValue, 15, false); 
			do_Tween.start();
			//bar_mc._width = (currVal*(barLength-offset))/baseVal;
	
			// Do the tool tip. Why? Why not! 
			var tt_text = " Value " + String(currVal) + " ";
			//this.toolTip(this, tt_text);
			// Set upper display value...
			txtHigh.text = String(highVal);
			txtMid.text  = String((highVal+nullVal)/2);
			txtZero.text = String( nullVal );
		}

		// Tool Tip 
/*		
		function toolTip(tagTarget:Sprite, tipText:String) {
			var h:*;
			tagTarget.onRollOver = function() {
				tagTarget.useHandCursor = false;
				h = this.createEmptyMovieClip("h", 10);
				//position relative to cursor.
				h.y = this.ymouse-30;
				h.x = this.xmouse;
				//attach movie clip.
				h.attachMovie("toolTip_mc", "toolTip_MC", 10);
				//format textfield
				h.toolTip_MC.txt_tooltip.autoSize=true;
				h.toolTip_MC.txt_tooltip.border=true;
				h.toolTip_MC.txt_tooltip.borderColor=0x808080;
				h.toolTip_MC.txt_tooltip.background=true;
				h.toolTip_MC.txt_tooltip.backgroundColor=0x000000;
				//set text.
				h.toolTip_MC.txt_tooltip.text = tipText;
				
				this.addChild( h );
				
				h.onMouseMove = function() {
				h.y = this.ymouse-30;
				h.x = this.xmouse;
				}
			}
		
			//remove tool tip on mouse out.
			tagTarget.onRollOut = function() {
				this.removeChild(h);
			}
		}
*/		
				
	}
}
