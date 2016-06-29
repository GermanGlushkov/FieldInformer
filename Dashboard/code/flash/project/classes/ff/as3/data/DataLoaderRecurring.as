package ff.as3.data { 

	//common
	import flash.utils.ByteArray;
	import flash.utils.*;//CompressionAlgorithm;
	import flash.net.*;
/*	
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
*/

	import flash.xml.*;
/*	
	import flash.xml.XMLDocument;
	import flash.xml.XMLNode;
*/

	import flash.events.*;
/*	
	import flash.events.Event;
	import flash.events.TimerEvent;
	import flash.events.ProgressEvent;
	import flash.events.HTTPStatusEvent;
	import flash.events.SecurityErrorEvent;
	import flash.events.IOErrorEvent;
	import flash.events.IEventDispatcher;
*/

	public class DataLoaderRecurring extends ff.as3.data.DataLoader	
	{
		protected var iLastDate:Date = null;
		protected var iTimer:Timer = null;
		protected var iHandlerOnTimer:Function = null;

		public function DataLoaderRecurring(aConnectionString:String, aExternalParams:Object, aDelayMilliseconds:Number)
		{
			super(aConnectionString, aExternalParams);
			
			this.iTimer = new Timer(aDelayMilliseconds);
			this.iTimer.addEventListener(TimerEvent.TIMER, _timerHandler);
		}

		//----------------------------------------------------
		// public properties
		override public function get className():String 
		{
			return "ff.data.DataLoaderRecurring";
		}

		public function get timer():Timer 
		{
			return this.iTimer;
		}
		
		public function get lastDate():Date 
		{
			return this.iLastDate;
		}
		
		//----------------------------------------------------
		// delegates
		public function set HandlerOnTimer( aHandler:Function )
		{
			this.iHandlerOnTimer = aHandler;
		}
		
		
		//----------------------------------------------------
		// events handlers for agregated objects

		protected function _timerHandler(aEvent:TimerEvent):void 
		{
			if ( iHandlerOnTimer != null )
			{
				iHandlerOnTimer( this, aEvent );
			}
		}
		

	}
	
}
