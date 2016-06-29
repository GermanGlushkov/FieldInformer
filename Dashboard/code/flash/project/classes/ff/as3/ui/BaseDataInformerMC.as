package ff.as3.ui 
{ 

	import be.boulevart.as3.security.*;
	
	import ff.as3.data.DataLoaderRecurring;
	import ff.as3.data.AppConfig;

	import com.yahoo.astra.fl.containers.BorderPane;
	

	//common
	import flash.utils.*;
	import flash.net.*;
	import flash.xml.*;
	import flash.events.*;

	// Import GetDefinitionByName to dynamically refer to library clips
	import flash.utils.getDefinitionByName;
	
	import flash.system.Security;
	import flash.display.*;
	import flash.text.*;
	import fl.transitions.*;
	import fl.motion.easing.*;
	
	import flash.net.SharedObject;
    import flash.net.SharedObjectFlushStatus;

	public class BaseDataInformerMC extends BorderPane
	{
		/**-----------------------------------------------------------------------------
		 * Properties.
		 */
		protected var iLoaderParamsObj:Object = null;
		
		protected var iAppConfig:AppConfig = null;

		protected var iDataLoader:DataLoaderRecurring = null;
		
		protected var iQueryQueue:Array = new Array();
		
		protected var iFirstLoad:Boolean = true;

		protected var iSOConfigGuid:String = null;
		protected var iSOConfig:SharedObject = null;
		
		protected var iTimer:Timer = null;
		
		protected var iDATA:XML = null;

		/**-----------------------------------------------------------------------------
		 * Constructor.
		 */
		public function BaseDataInformerMC()
		{
			super();
			
			// create appconfig & fill with default values
			this.iAppConfig = new AppConfig();
			this.constructor_default_AppConfig();

			// read loader info (config,shared object id & etc)
			this.constructor_ReadLoaderInfo();

			// setup shared object if exists
			this.constructor_SetupSharedObject();

			// validate appconfig
			this.validate_AppConfig();
			
			if ( this.iAppConfig != null && this.iAppConfig.getTagIndex("queryStartup") >= 0 )
				this.iQueryQueue.push( this.constructCommandByPrefix( "queryStartup" ) );
			else if ( this.iAppConfig != null && this.iAppConfig.getTagIndex("queryRecurring") >= 0 )
				this.iQueryQueue.push( this.constructCommandByPrefix( "queryRecurring" ) );

			if ( this.iAppConfig != null && this.iQueryQueue != null && this.iQueryQueue.length > 0 )
			{
				var aDelay:Number = 0.1;
				if ( this.iAppConfig.getTagIndex("delay") >= 0 ) 
					aDelay = parseFloat(this.iAppConfig.getValueText("delay"));
				this.iTimer = new Timer(aDelay * 1000);
				this.iTimer.addEventListener(TimerEvent.TIMER, onOwnTimerHandler);
				this.iTimer.start();
				//nextLoadData();
			}
		}
		
		//-----------------------------------------------------------
		// constructor's helpers
		
		protected function constructor_default_AppConfig() : void
		{
//trace("BaseDataInformerMC::constructor_AppConfig");
			try
			{
				var aToMerge:XML = <config><v t='caption' /><v t='brokerUrl' /><v t='delay' ><![CDATA[0.1]]></v><v t='frequency' ><![CDATA[random]]></v><v t='queryRecurring' /></config>;
				this.iAppConfig.mergeIn( aToMerge );
			}
			catch (aError:Error) 
			{
				trace("BaseDataInformerMC.constructor_default_AppConfig: " + aError);
			}
		}
		
		protected function constructor_ReadLoaderInfo() : void
		{
			// get external parameters
			try
			{
//trace(LoaderInfo(this.root.loaderInfo).parameters);
				this.iLoaderParamsObj = LoaderInfo(this.root.loaderInfo).parameters;

				// merge config given by loader
				this.iAppConfig.mergeInObjectProperty( this.iLoaderParamsObj, "config" );

				// broker url can by overriden by other variable
				if ( this.iLoaderParamsObj.hasOwnProperty("BrokerUrl") )
				{
					this.iAppConfig.replaceValueText( "brokerUrl", this.iLoaderParamsObj.BrokerUrl );
				}

				// db_id can by overriden by other variable
				if ( this.iLoaderParamsObj.hasOwnProperty("db") )
				{
					this.iAppConfig.replaceValueText( "db_id", this.iLoaderParamsObj.db );
				}
			}
			catch (aError:Error) 
			{
				trace("BaseDataInformerMC.constructor_ReadLoaderInfo: " + aError);
			}
		}
		
		protected function constructor_SetupSharedObject() : void
		{
			try
			{
				if ( this.iLoaderParamsObj != null && this.iLoaderParamsObj.hasOwnProperty("SOCid") )
				{
					try
					{
						this.iSOConfigGuid = this.iLoaderParamsObj.SOCid;
						this.iSOConfig = SharedObject.getLocal(this.iSOConfigGuid, "/");
						if ( this.iSOConfig != null )
						{
							// for (var prop in this.iSOConfig.data) {trace(prop+": "+this.iSOConfig.data[prop]);}						
							// merge config given by shared object's data
							this.iAppConfig.mergeInObjectProperty( this.iSOConfig.data, "config" );
						}
					}
					catch (aError:Error) 
					{
						trace(aError);
					}
				}
			}
			catch (aError:Error) 
			{
				trace("BaseDataInformerMC.constructor_SetupSharedObject: " + aError);
			}
		}
		
		protected function constructor_DataLoader() : void
		{
			this.iDataLoader = this.alloc_DataLoader();
			this.iDataLoader.HandlerOnBeginLoad = this.onBeginLoad
			this.iDataLoader.HandlerOnLoadSchema = this.onLoadSchema
			this.iDataLoader.HandlerOnLoadData = this.onLoadData;
			this.iDataLoader.HandlerOnError = this.onLoadFailed;
			this.iDataLoader.HandlerOnFinished = this.onLoadFinished;
			this.iDataLoader.HandlerOnTimer = this.onLoaderTimer;
trace( "BaseDataInformerMC::constructor_DataLoader: " + this.iDataLoader.ConnectionString );
		}
		
		protected function alloc_DataLoader() : DataLoaderRecurring
		{
			var aUrl:String = "http://194.100.32.249:2008/eForceMobile/Broker.aspx";
			if ( this.iAppConfig != null && this.iAppConfig.getTagIndex("brokerUrl") >= 0 )
			{
//trace(this.iAppConfig.toString());
				aUrl = this.iAppConfig.getValueText("brokerUrl");
			}
			if ( aUrl == "" )
			{
trace( "constructor_DataLoader: brokerUrl is empty" );
				aUrl = "http://localhost/eForceMobile/Broker.aspx";//"http://194.100.32.249:2008/eForceMobile/Broker.aspx";
			}
			return new DataLoaderRecurring( aUrl, this.iLoaderParamsObj, 0);
		}

		
		//----------------------------------------------------
		// protected (overridable) properties

		//----------------------------------------------------
		// protected (overridable) functions
		
		protected function ShowData() : void
		{
			// called e.g.on start and when iDATA object changed
		}
	
		
		protected function validate_AppConfig() : void
		{
			try
			{
				if ( this.iAppConfig != null )
				{
					var aNum:Number;
					var aStr:String;
					if ( this.iAppConfig.getTagIndex("delay") >= 0 )
					{
						if ( this.iAppConfig.getValueText("delay") == 'random' )
						{
							aNum = Math.round(5 + 5 * Math.random())/10;
							this.iAppConfig.replaceValueText( "delay", aNum.toString() );
						}
					}
					if ( this.iAppConfig.getTagIndex("frequency") >= 0 )
					{
						if ( this.iAppConfig.getValueText("frequency") == 'random' )
						{
							aNum = Math.round(10*(5 + 5 * Math.random()))/10;
							this.iAppConfig.replaceValueText( "frequency", aNum.toString() );
						}
					}
				}
			}
			catch (aError:Error) 
			{
				trace("BaseDataInformerMC.validate_AppConfig" + aError);
			}
//trace(this.iAppConfig);
		}
		
		protected function notify_AppConfig() : void
		{
			try
			{
				if (  this.iSOConfig != null )
				{
					var aConfig:String = this.iAppConfig.xml.toXMLString();
//trace(aConfig);
					aConfig = be.boulevart.as3.security.Base64.encode(aConfig);
//trace(aConfig);
					this.iSOConfig.data.config = aConfig;
					var aFlushResult:Object = this.iSOConfig.flush();
//trace("flushResult: " + aFlushResult);
				}
			}
			catch (aError:Error) 
			{
				trace("BaseDataInformerMC.notify_AppConfig" + aError);
			}
//trace(this.iAppConfig);
		}

		protected function constructCommandByPrefix( aPrefix:String ): String
		{
			var aQuery:String = "<command dialect='XSQL' type='" + (this.iAppConfig.getAttr(aPrefix) == "StoredProcedure" ? "StoredProcedure" : "Text") +"' execute='query'>"; 
			aQuery += "<![CDATA["+be.boulevart.as3.security.Base64.encode(this.iAppConfig.getValueText(aPrefix))+"]]>";
			
			var aArray:Array = this.iAppConfig.getArray(); // {t:<v.@t>, a:<v.@t>, v:<v.toString()}
			if ( aArray != null && aArray.length > 0 )
			{
				for( var i:int=0; i < aArray.length; i++ )
				{
					if ( aArray[i].t.length > (aPrefix.length+1) && aArray[i].t.substr(0,aPrefix.length+1) == (aPrefix + ".") )
					{
						aQuery += "<p n='" + aArray[i].t.substr(aPrefix.length+1) + "'><![CDATA[" + be.boulevart.as3.security.Base64.encode(aArray[i].v) + "]]></p>";
					}
				}
			}
		
			aQuery += "</command>";

			return aQuery;
		}

		protected function nextLoadData(): void
		{
			// create data loader
			if ( this.iDataLoader == null )
				this.constructor_DataLoader();

			if ( this.iQueryQueue != null && this.iQueryQueue.length > 0 )
			{
				var aXSQL:String = this.iQueryQueue[0] as String;
				this.iDataLoader.LoadRequest( aXSQL, true, true );
//trace( this.className + ".nextLoadData: " + aSqlQuery );
				//this.iDataLoader.LoadBySqlNoSchema( aSqlQuery, null, true, true );
			}
		}
		
		protected function onBeginLoad( aSender:Object )
		{
//trace( this.className + ".onLoadSchema: " + aXMLNode );
		}

		protected function onLoadSchema( aSender:Object, aXMLNode:XMLNode )
		{
//trace( this.className + ".onLoadSchema: " + aXMLNode );
		}
		
		protected function onLoadData( aSender:Object, aXML:XML )
		{
			this.iDATA = aXML;
//trace( this.className + ".onLoadData: " + aXML );
		}
		
		protected function onLoadFailed( aSender:Object, aMethodName:String, aError:* )
		{
trace( this.className + ".onLoadFailed: (" + aMethodName + ") " + aError );
		}
		
		protected function onLoadFinished(  aSender:Object )
		{
//trace( this.className + ".onLoadFinished" + " " + this.iAppConfig.frequency[0] + " " + this.iAppConfig.queryRecurring[0] );

			ShowData();
			
			if ( this.iFirstLoad && this.iAppConfig != null && this.iQueryQueue != null && this.iQueryQueue.length > 0 )
			{
				var aDelay:Number = parseFloat(this.iAppConfig.getValueText("delay"));
				this.iTimer = new Timer(aDelay * 1000);
				this.iTimer.addEventListener(TimerEvent.TIMER, onOwnTimerHandler);
				this.iTimer.start();
				//nextLoadData();
			}

			if ( this.iAppConfig != null
				 && this.iAppConfig.getTagIndex("frequency") >= 0
				 && this.iAppConfig.getTagIndex("queryRecurring") >= 0 )
			{
				var aNum:Number = parseFloat(this.iAppConfig.getValueText("frequency"));
				if ( aNum > 0 )
				{
					var aQry:String = this.constructCommandByPrefix( "queryRecurring" );
					if ( aQry != null && aQry.length > 0 )
					{
						this.iQueryQueue.push( aQry );
						this.iDataLoader.timer.delay = aNum * 1000;
						this.iDataLoader.timer.start();
					}
				}
			}
		}
		
		protected function onLoaderTimer( aSender:Object, aEvent:TimerEvent )
		{
//trace( "BaseDataInformerMC::onTimer" );
			nextLoadData();
			this.iDataLoader.timer.reset();
		}
		
		
		protected function onOwnTimerHandler(aEvent:TimerEvent):void 
		{
			nextLoadData();
			this.iTimer.reset();
			if ( this.iFirstLoad )
			{
				this.iFirstLoad = false;
			}
		}

		
		//----------------------------------------------------
		// public (overridable) properties

		public function get className():String 
		{
			return "ff.ui.BaseDataInformerMC";
		}
		

	}
	
}
