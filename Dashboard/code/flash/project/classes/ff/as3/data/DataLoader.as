package ff.as3.data { 

	//common
	import flash.utils.ByteArray;
	import flash.utils.*;//CompressionAlgorithm;
	import flash.net.*;
    import flash.net.sendToURL;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
	import flash.xml.XMLDocument;
	import flash.xml.XMLNode;
	import flash.events.Event;
	import flash.events.ProgressEvent;
	import flash.events.HTTPStatusEvent;
	import flash.events.SecurityErrorEvent;
	import flash.events.IOErrorEvent;
	import flash.events.IEventDispatcher;

	//3rd party
	import be.boulevart.as3.security.Rijndael;
	import be.boulevart.as3.security.Base64;
	
	public class DataLoader
	{
	    //static var className:String = "ff..data.DataLoader";
		static var hexes:Array = new Array("0","1","2","3","4","5","6","7","8","9","a","b","c","d","e","f");
		
		protected var iURLLoader:URLLoader = null;
		
		protected var iConnectionString:String = "";
		protected var iExternalParams:Object = null;

		protected var iRijndael:Rijndael = null;
		protected var iKeyString:String = "1234567890123456";
		
		protected var iHandlerOnBeginLoad:Function = null;
		protected var iHandlerOnLoad:Function = null;
		protected var iHandlerOnLoadTable:Function = null;
		protected var iHandlerOnLoadSchema:Function = null;
		protected var iHandlerOnLoadData:Function = null;
		protected var iHandlerOnError:Function = null;
		protected var iHandlerOnFinished:Function = null;

		//----------------------------------------------------
		// public methods
		/*
		public function DataLoader()
		{
			var iURLLoader:URLLoader = new URLLoader();
			_configureListeners(iURLLoader);
		}
		*/
		public function DataLoader(aConnectionString:String, aExternalParams:Object)
		{
			this.iURLLoader = new URLLoader();
			this.iRijndael = new Rijndael(128, 256);

			_configureListeners(this.iURLLoader);
			this.iConnectionString = aConnectionString;
			this.iExternalParams = aExternalParams;
		}


		// 'Handmade:)' destructor
		public function CleanUp():void
		{
			if ( this.iURLLoader != null )
			{
				try
				{
					this.iURLLoader.close();
				}
				catch (aError:Error) 
				{
					trace(aError);
					if ( this.iHandlerOnError != null )
					{
						this.iHandlerOnError( this, this.className+".CleanUp", aError );
					}
				}
			}
			this.iURLLoader = null;
		}
		
		public function LoadNoSchema(aCommand:String, aCommandType:String, aCrypt:Boolean, aZip:Boolean ):void 
		{
			var aSchemaNeeded:Boolean = false;
			Load(aCommand, aCommandType, aSchemaNeeded, aCrypt, aZip );
		}
		
		public function LoadCryptZip(aCommand:String, aCommandType:String, aSchemaNeeded:Boolean):void 
		{
			var aSchemaNeeded:Boolean = aSchemaNeeded;
			var aCrypt:Boolean = true;
			var aZip:Boolean = true;
			Load(aCommand, aCommandType, aSchemaNeeded, aCrypt, aZip );
		}
		
		public function Load(aCommand:String, aCommandType:String, aSchemaNeeded:Boolean, aCrypt:Boolean, aZip:Boolean ):void 
		{
			var aRequest:String = "<command "; 
			if ( aSchemaNeeded )
				aRequest += " schema='1'";
			if ( aCommandType != null )
				aRequest += " type='" + aCommandType + "'";
			aRequest += " ><![CDATA[";
			aRequest += be.boulevart.as3.security.Base64.encode(aCommand);
			aRequest += "]]></command>";
			this.LoadRequest( aRequest, aCrypt, aZip );

		}		
		
		public function LoadRequest(aRequest:String, aCrypt:Boolean, aZip:Boolean ):void 
		{
			try 
			{
				if ( this.iHandlerOnBeginLoad != null )
				{
					this.iHandlerOnBeginLoad( this );
				}
				var aURLRequest:URLRequest = this.PrepareURLRequest(aRequest, aCrypt, aZip, this.ConnectionString, this.iKeyString, this.iRijndael);
				this.iURLLoader.load(aURLRequest);
			} 
			catch (aError:Error) 
			{
				trace(this.className+".Load: " + aError);
				if ( this.iHandlerOnError != null )
				{
					this.iHandlerOnError( this, this.className+".Load", aError );
				}
			}
		}		
		
		public function Cancel():void 
		{
			try 
			{
				if ( this.iURLLoader != null )
				{
					this.iURLLoader.close();
				}
			} 
			catch (aError:Error) 
			{
				trace(this.className+".Cancel: " + aError);
			}
		}		

		public function sendUpdate(aQuery:XML, aCrypt:Boolean, aZip:Boolean ):void 
		{
			/*
			var aRequest:String = "<command dialect='xsql' execute='NonQuery' transaction='1'"; 
			aRequest += " ><![CDATA[";
			aRequest += be.boulevart.as3.security.Base64.encode(aQuery);
			aRequest += "]]></command>";
			*/
			try 
			{
				var aURLRequest:URLRequest = PrepareURLRequest(aQuery, aCrypt, aZip, this.ConnectionString, this.iKeyString, this.iRijndael);
				sendToURL(aURLRequest);
			} 
			catch (aError:Error) 
			{
				trace(aError);
				if ( this.iHandlerOnError != null )
				{
					this.iHandlerOnError( this, this.className+".sendUpdate", aError );
				}
			}
		}		
		
		//----------------------------------------------------
		// public /*?static?*/ functions
		
		public function PrepareURLRequest(aReqIn:String, aCrypt:Boolean = true, aZip:Boolean = true, aUrl:String = null, aKeyString:String = null, aRijndael:Rijndael = null):URLRequest 
		{
			var aSize:int = aReqIn.length;
			
			if ( aUrl == null )
				aUrl = this.ConnectionString;

			var aURLRequest:URLRequest = new URLRequest(aUrl);
//trace(aUrl);			
			aURLRequest.method="POST";
			
			if ( aRijndael == null )
				aRijndael = this.iRijndael;
			
			if ( aKeyString == null )
				aKeyString = this.KeyString;
/* if not ie				
			if ( iExternalParams != null )
			{
				if ( iExternalParams.hasOwnProperty("ASPSESSID") )
				{
					var aHeaderASPSESSID:URLRequestHeader = new URLRequestHeader("ASPSESSID", iExternalParams.ASPSESSID);
					aURLRequest.requestHeaders.push(aHeaderASPSESSID);
				}
				if ( iExternalParams.hasOwnProperty("AUTHID") )
				{
					var aHeaderAUTHID:URLRequestHeader = new URLRequestHeader("AUTHID", iExternalParams.AUTHID);
					aURLRequest.requestHeaders.push(aHeaderAUTHID);
				}
			}
*/				
			
			var aURLVariables:URLVariables = new URLVariables();
			
			var aReqOut:String = "";
			if ( aZip )
			{
				var aOff:int = 0;
				var aLen:int = aReqIn.length > 32000 ? 32000 : (aReqIn.length - aOff);
				while ( aLen > 0 )
				{
					var aByteArray:ByteArray = new ByteArray();
					aByteArray.writeUTF(aReqIn.substr(aOff,aLen));
					aByteArray.deflate();
					aByteArray.position = 0;
					// 2 first bytes are length (low first)
					var aByte:int = aByteArray.length & 0xff;//low
					aReqOut += hexes[aByte >> 4] + hexes[aByte & 0xf];
					aByte = aByteArray.length >> 8;//high
					aReqOut += hexes[aByte >> 4] + hexes[aByte & 0xf];
					for (var i:Number = 0; i<aByteArray.length; i++) 
					{
						aByte = aByteArray.readByte();
						aByte = aByte & 0xff;
						aReqOut += hexes[aByte >> 4] + hexes[aByte & 0xf];
					}
					aOff += aLen;
					aLen = (aReqIn.length - aOff) > 32000 ? 32000 : (aReqIn.length - aOff);
				}
				// zero size at the and
				aReqOut += hexes[0] + hexes[0] + hexes[0] + hexes[0];
				
				if ( aCrypt )
					aReqOut = aRijndael.encryptHexToHex(aReqOut, aKeyString, "ECB");
			}
			else
			{
				if ( aCrypt )
					aReqOut = aRijndael.encryptStrToHex(aReqIn, aKeyString, "ECB");
				else
					aReqOut = aReqIn;
			}
//trace(aRequest.length);trace(aRequest);
			var aXMLDocument:XMLDocument = new XMLDocument();
			aXMLDocument.ignoreWhite=true;
			var aXMLNodeElt:XMLNode = aXMLDocument.createElement("r");
			var aXMLNodeText:XMLNode = aXMLDocument.createTextNode(aReqOut);
			aXMLNodeElt.appendChild(aXMLNodeText);
			if ( aCrypt )
				aXMLNodeElt.attributes["crypt"]="1";
			if ( aZip )
				aXMLNodeElt.attributes["zip"]="1";
			aXMLNodeElt.attributes["size"]= new String(aSize);
			aXMLDocument.firstChild = aXMLNodeElt;
			
			aURLVariables.xml = aXMLDocument;
			aURLRequest.data = aURLVariables;
			
			return aURLRequest;
		}		
		

		public function ProcessUrlResponse(aData:*):void 
		{
			try
			{
				var aResponse:String = this.ParseUrlResponse( aData, this.KeyString, this.iRijndael );
				
				var aXMLDocumentResponse:XMLDocument = new XMLDocument();
				aXMLDocumentResponse.ignoreWhite=true;
				aXMLDocumentResponse.parseXML( aResponse );			
				if ( iHandlerOnLoad != null )
				{
					iHandlerOnLoad( this, aXMLDocumentResponse );
				}
				else
				{
					var aXmlNodeDocElt:XMLNode = aXMLDocumentResponse.firstChild;
					var aXMLNodeElt:XMLNode = null;
					for each (aXMLNodeElt in aXmlNodeDocElt.childNodes)
					{
		//trace(aXMLNodeElt); 
						if ( iHandlerOnLoadTable != null )
						{
							iHandlerOnLoadTable( this, aXMLNodeElt );
						}
						else
						{
							var aXMLNode:XMLNode = null;
							for each (aXMLNode in aXMLNodeElt.childNodes)
							{
								if ( aXMLNode.nodeName == "s" ) // schema
								{
			//trace(aXMLNode); 
									if ( iHandlerOnLoadSchema != null )
									{
										iHandlerOnLoadSchema( this, aXMLNode );
									}
								}
							}
							for each (aXMLNode in aXMLNodeElt.childNodes)
							{
								if ( aXMLNode.nodeName == "d" ) // data
								{
									var aXML:XML = new XML(aXMLNode);
			//trace(aXML); 
									if ( iHandlerOnLoadData != null )
									{
										iHandlerOnLoadData( this, aXML );
									}
								}
							}
						}
					}
				}

			}
			catch (aError:Error) 
			{
				trace(aError);
				if ( this.iHandlerOnError != null )
				{
					try
					{
						this.iHandlerOnError( this, this.className+".ProcessUrlResponse", aError );
					}
					catch (aError:Error) 
					{
						trace(aError);
					}
				}
			}


			try
			{
				if ( this.iHandlerOnFinished != null )
				{
					this.iHandlerOnFinished( this );
				}
			}
			catch (aError:Error) 
			{
				trace(aError);
			}
		}
		
		public function ParseUrlResponse(aData:*, aKeyString:String, aRijndael:Rijndael):String 
		{
			var aStringResponse:String = "";
			
            //trace(this.className+"._completeHandler: ");// + event);
			var i:Number;
			
			if ( aRijndael == null )
				aRijndael = this.iRijndael;
			
			if ( aKeyString == null )
				aKeyString = this.KeyString;

			// response
			var aXMLDocumentResponse:XMLDocument=new XMLDocument();
			aXMLDocumentResponse.ignoreWhite=true;
			aXMLDocumentResponse.parseXML(aData);
			var aXmlOuter:XMLNode=aXMLDocumentResponse.firstChild.firstChild;
			
			var aCrypt:Boolean = (aXmlOuter.attributes.crypt != "0"); 
			var aZip:Boolean = (aXmlOuter.attributes.zip != "0"); 
			var aSize:int = new int(aXmlOuter.attributes.size); 

			aStringResponse = aXmlOuter.firstChild.nodeValue;
			trace(aStringResponse); 
			
			if ( aZip || aCrypt )
			{
				aStringResponse = Base64.decodeToHex( aStringResponse );
				var aArray:Array = null;
				if ( aCrypt )
					aArray = iRijndael.decryptHexToArray(aStringResponse, KeyString, "ECB");
				else
					aArray = this.hexToArray( aStringResponse );
				aStringResponse = "";
				if ( aZip )
				{
					var aOff:int = 0;
					var aLen:int = 0;
					var ba:ByteArray = new ByteArray();
					while ( aOff < aArray.length && aLen < aSize )
					{
						var aZipLen:int = (aArray[aOff] as int) + 256 * (aArray[aOff + 1] as int);
						if ( aZipLen == 0 )
							break;
						aOff += 2;
						for( i=0;i < aZipLen; i++)
							ba.writeByte(aArray[aOff + i]);
						ba.position = 0;
						ba.inflate();
						aStringResponse += ba.toString();
						aOff += aZipLen;
						ba.clear();
						aLen = aStringResponse.length;
					}
				}
				else
					aStringResponse = this.arrayToStr( aArray);
			}
			
			return aStringResponse;
		}


		//----------------------------------------------------
		// public properties
		public function get className():String 
		{
			return "ff.data.DataLoader";
		}
		
		public function get ConnectionString():String 
		{
			return this.iConnectionString;
		}
		public function set ConnectionString( aConnectionString:String )
		{
			this.iConnectionString = aConnectionString;
		}

		public function get KeyString():String 
		{
			return this.iKeyString;
		}
		public function set KeyString( aKeyString:String )
		{
			this.iKeyString = aKeyString;
		}

		//----------------------------------------------------
		// delegates
		public function set HandlerOnError( aHandler:Function )
		{
			this.iHandlerOnError = aHandler;
		}
		
		public function set HandlerOnBeginLoad( aHandler:Function )
		{
			this.iHandlerOnBeginLoad = aHandler;
		}
		
		public function set HandlerOnLoad( aHandler:Function )
		{
			this.iHandlerOnLoad = aHandler;
		}
	
		public function set HandlerOnLoadTable( aHandler:Function )
		{
			this.iHandlerOnLoadTable = aHandler;
		}
		
		public function set HandlerOnLoadSchema( aHandler:Function )
		{
			this.iHandlerOnLoadSchema = aHandler;
		}
		
		public function set HandlerOnLoadData( aHandler:Function )
		{
			this.iHandlerOnLoadData = aHandler;
		}
		
		public function set HandlerOnFinished( aHandler:Function )
		{
			this.iHandlerOnFinished = aHandler;
		}

		//----------------------------------------------------
		// protected helpers
		protected function _configureListeners(aDispatcher:IEventDispatcher):void 
		{
            aDispatcher.addEventListener(Event.COMPLETE, _completeHandler);
            aDispatcher.addEventListener(Event.OPEN, _openHandler);
            aDispatcher.addEventListener(ProgressEvent.PROGRESS, _progressHandler);
            aDispatcher.addEventListener(SecurityErrorEvent.SECURITY_ERROR, _securityErrorHandler);
            aDispatcher.addEventListener(HTTPStatusEvent.HTTP_STATUS, _httpStatusHandler);
            aDispatcher.addEventListener(IOErrorEvent.IO_ERROR, _ioErrorHandler);
        }

		protected function hexToArray(hex:String):Array {
			var codes:Array = new Array();
			for (var i:Number = (hex.substr(0, 2) == "0x") ? 2 : 0; i<hex.length; i+=2) {
				codes.push(parseInt(hex.substr(i, 2), 16));
			}
			return codes;
		}
		protected function charsToHex(chars:Array):String {
			var result:String = new String("");
			for (var i:Number = 0; i<chars.length; i++) {
				result += hexes[chars[i] >> 4] + hexes[chars[i] & 0xf];
			}
			return result;
		}
		protected function arrayToStr(chars:Array):String {
			var result:String = new String("");
			for (var i:Number = 0; i<chars.length; i++) {
				result += String.fromCharCode(chars[i]);
			}
			return result;
		}
		protected function strToChars(str:String):Array {
			var codes:Array = new Array();
			for (var i:Number = 0; i<str.length; i++) {
				codes.push(str.charCodeAt(i));
			}
			return codes;
		}

		//----------------------------------------------------
		// events handlers for agregated objects

		//URLLoader
        protected function _openHandler(event:Event):void 
		{
            //trace(this.className+"._openHandler: " + event);
        }

        protected function _progressHandler(event:ProgressEvent):void 
		{
            //trace(this.className+"._progressHandler loaded:" + event.bytesLoaded + " total: " + event.bytesTotal);
        }

        protected function _securityErrorHandler(event:SecurityErrorEvent):void 
		{
            trace(this.className+"._securityErrorHandler: " + event);
			if ( this.iHandlerOnError != null )
			{
				try
				{
					this.iHandlerOnError( this, this.className+"._securityErrorHandler", event.text );
				}
				catch (aError:Error) 
				{
					trace(aError);
				}
			}
        }

        protected function _httpStatusHandler(event:HTTPStatusEvent):void 
		{
            //trace(this.className+"._httpStatusHandler: " + event);
        }

        protected function _ioErrorHandler(event:IOErrorEvent):void 
		{
            trace(this.className+"._ioErrorHandler: " + event);
			if ( this.iHandlerOnError != null )
			{
				try
				{
					this.iHandlerOnError( this, this.className+"._ioErrorHandler", event.text );
				}
				catch (aError:Error) 
				{
					trace(aError);
				}
			}
        }

		protected function _completeHandler(event:Event):void 
		{
 //trace(className+"._completeHandler: ");// + event);
			// define a function that will execute after data has finished loading
			var aURLLoader:URLLoader = event.currentTarget as URLLoader;
//trace(aURLLoader.data);			
			this.ProcessUrlResponse( aURLLoader.data );
		}
	}
	
}
