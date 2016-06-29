package
{
	import fl.controls.*;
	
    import flash.display.DisplayObject;
    import flash.display.Graphics;
    import flash.display.JointStyle;
    import flash.display.LineScaleMode;
    import flash.display.Sprite;
	import flash.display.Shape;
	import flash.display.MovieClip;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.text.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.Elastic;


	import ff.as3.ui.VParamDataInformerMC;

	/**
	 * 
	 */
	public class Gauge_IO extends VParamDataInformerMC
	{
		
	//--------------------------------------
	//  Constructor
	//--------------------------------------
	
		/**
		 * Constructor.
		 */
		public function Gauge_IO()
		{
			super();
			
			// Set the background image if any.
			//bg_loader.contentPath = this['bgimage'];
			
			// Set the title and title color.
			txt_avalue.text = "0";
			txt_bvalue.text = "0";

			this.needle_a_mc.rotation = -50;
			this.needle_b_mc.rotation = -50;
			this.av_needle_a.rotation = -50;
			this.av_needle_b.rotation = -50;
			
			if ( super.iTextFieldHeader != null && this.iAppConfig.getTagIndex("caption") >= 0 )
				super.iTextFieldHeader.text = this.iAppConfig.getValueText("caption");
				
			this.ShowData();
		}
	
		//-----------------------------------------------------------
		// constructor's helpers
		
		override protected function constructor_default_AppConfig() : void
		{
			super.constructor_default_AppConfig();
			try
			{
				//var aToMerge:XML = <config><v t='caption' >IO Gauge</v><v t='queryStartup' >select 100 as inrange, 100 as outrange, cast(rand()*100 as int) as avalue, cast(rand()*100 as int) as bvalue, cast(rand()*100 as int) as av_avalue, cast(rand()*100 as int) as av_bvalue</v><v t='queryRecurring' >select 100 as inrange, 100 as outrange, cast(rand()*100 as int) as avalue, cast(rand()*100 as int) as bvalue, cast(rand()*100 as int) as av_avalue, cast(rand()*100 as int) as av_bvalue</v></config>;
				var aToMerge:XML = <config><v t='caption' >SOK</v>
<v t='queryRecurring' ><![CDATA[]]></v>
<v t='queryRecurring.P1' a='sql.FormItem' ><![CDATA[*]]><o><v n='t' >Combo</v><v n='l' ><![CDATA[Category]]></v><v n='s' ><i l="All" d="*" /><i l="019 LEIPOMO,EINES,VALM." d="019" /><i l="021 JOGURTTI" d="021" /><i l="027 MAITOPOHJ. JÄLKIRUOAT" d="027" /></v></o></v>
				</config>;
				this.iAppConfig.mergeIn( aToMerge );
				
				var aQuery:String = "select nown.[IdxY], nown.[IdxM], nown.[Prc],own.[IdxY],own.[IdxM],own.[Prc],own.[Category] from [DBSALESPP_ZZZ].[dbo].[map_io_1] nown, [DBSALESPP_ZZZ].[dbo].[map_io_1] own where own.[Own]=1 and nown.[Own]=0 and nown.[Category]=own.[Category] and substring(nown.[Category],1,len(@P1))=@P1";
				
				//if ( !this.iAppConfig.hasOwnProperty("queryRecurring") || this.iAppConfig.queryRecurring[0].toString() == "")
				this.iAppConfig.replaceValueText( "queryRecurring", aQuery );
				
				this.iAppConfig.replaceValueText("frequency", "0")					;
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
			return 300;
		}
		
		override protected function get origDocHeight():Number 
		{
			return 200;
		}

		override protected function configureContent():void
		{
			this.iPaneContent.x=0;
			this.iPaneContent.setSize(300,158);
			this.iPaneContent.configuration = 
			[
			];
		}
		
		//----------------------------------------------------
		// protected (overridable) functions
		
		override protected function ShowData() : void
		{
			trace( this.iDATA );

	//	var aDataProvider:fl.data.DataProvider = new fl.data.DataProvider(aXML);
				var intInRange:Number = 200;
				var intOutRange:Number  = 200;
				
				var aIdxY1:Number    = 0;
				var aIdxY2:Number    = 0;
				
				var aIdxM1:Number    = 0;
				var aIdxM2:Number    = 0;

				var aPrc1:String    = "";
				var aPrc2:String    = "";

				this.iTextFieldHeader.text = this.iAppConfig.getValueText("caption");
				if ( this.iDATA != null )
				{
					for each (var r:XML in this.iDATA..r) 
					{
						aIdxY1 = r.c1;
						aIdxM1 = r.c2;
						aPrc1 = r.c3;
						
						aIdxY2 = r.c4;
						aIdxM2 = r.c5;
						aPrc2 = r.c6;
						if ( this.iTextFieldHeader.text == this.iAppConfig.getValueText("caption") )
							this.iTextFieldHeader.text += " (" + r.c7 + ")";
					}
				}
				
				//set text output displays.
				txt_avalue.text  = String(aPrc1);
				txt_bvalue.text  = String(aPrc2);
				
				txtInRange.text  = String(aIdxY1);
				txtOutRange.text = String(aIdxY2);

				// Do the main in/out needles.
				var a_degree:Number = intInRange == 0 ? 0 : ((aIdxY1*100)/intInRange)-50;
				var a_pointer:Number = this.needle_a_mc.rotation;
				var do_Tween1:Object = new Tween(needle_a_mc,"rotation",Elastic.easeOut,a_pointer,a_degree,9,false); //hour.rotation,degree
				do_Tween1.start();
				
		
				// Do the hist/average needle
				var a_avdegree = intInRange == 0 ? 0 : ((aIdxM1*100)/intInRange)-50;
				var a_avpointer = av_needle_a.rotation;
				var do_Tween3:Object = new Tween(av_needle_a,"rotation",Elastic.easeOut,a_avpointer,a_avdegree,9,false); //hour.rotation,degree
				do_Tween3.start();
				
				var b_degree = intOutRange == 0 ? 0 : ((aIdxY2*100)/intOutRange)-50;
				var b_pointer = needle_b_mc.rotation;
				var do_Tween2:Object = new Tween(needle_b_mc,"rotation",Elastic.easeOut,b_pointer,b_degree,9,false); //hour.rotation,degree
				do_Tween2.start();
		
				var b_avdegree = intOutRange == 0 ? 0 : ((aIdxM2*100)/intOutRange)-50;
				var b_avpointer = av_needle_b.rotation;
				var do_Tween4:Object = new Tween(av_needle_b,"rotation",Elastic.easeOut,b_avpointer,b_avdegree,9,false); //hour.rotation,degree
				do_Tween4.start();
		}
			
	}
}

