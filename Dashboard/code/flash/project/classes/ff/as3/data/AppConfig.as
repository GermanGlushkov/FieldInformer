package ff.as3.data { 

	import flash.xml.*;
	
	import flash.net.SharedObject;
    import flash.net.SharedObjectFlushStatus;

	import flash.utils.describeType;
	import flash.utils.getQualifiedClassName;
	import flash.utils.getDefinitionByName;
	
	import fl.data.DataProvider;

	import be.boulevart.as3.security.*;
	
	import ff.as3.data.*;
	import ff.as3.ui.*;

	public class AppConfig
	{
		// agregated xml
		
		protected var iXML:XML = <config/>;
		
		//----------------------------------------------------
		// public methods
		public function AppConfig(aToMerge:XML = null)
		{
			if ( aToMerge != null )
				this.mergeIn( aToMerge );
            return;
		}

		//----------------------------------------------------
		// public properties
		public function get className():String 
		{
			return "ff.data.AppConfig";
		}
		
		public function get xml():XML 
		{
			return this.iXML;
		}
		
		/*
		public function set xml( aXML:XML )
		{
			this.iXML = aXML;
		}
		*/

		
		//----------------------------------------------------
		// public helpers
		
		public function mergeIn( aToMerge:XML ): void
		{
//trace( this.xml.toXMLString() );			
//trace( aToMerge.toXMLString() );			
			try
			{
				if ( aToMerge != null )
				{
					var aXMLList:XMLList = aToMerge.child("v");
					for ( var a:Number = 0; aXMLList != null && a < aXMLList.length(); a++ )
					{
						if ( aXMLList[a].hasOwnProperty("@t") )
						{
							var aTag:String = aXMLList[a].@t;
							if ( aTag.length > 0 )
							{
								var aAtt:String = null;
								if ( aXMLList[a].hasOwnProperty("@a") )
									aAtt = aXMLList[a].@a;
								try
								{
									this.replaceValueNode( aTag, aXMLList[a], aAtt );
								}
								catch ( aError:Error ) 
								{
									trace( "AppConfig.mergeIn: " + aError );
								}
							}
						}
					}
				}
			}
			catch ( aError:Error ) 
			{
				trace( "AppConfig.mergeIn: " + aError );
			}
//trace( this.xml.toXMLString() );			
		}
		
		public function mergeInString( aConfig:String, aBase64:Boolean = true ): void
		{
			try
			{
				var aXMLDoc:XMLDocument=new XMLDocument();
				aXMLDoc.ignoreWhite=true;
				if ( aBase64 )
					aConfig = be.boulevart.as3.security.Base64.decode( aConfig );
				aXMLDoc.parseXML( aConfig );
				var aToMerge:XML = new XML( aXMLDoc );
				this.mergeIn( aToMerge );
			}
			catch ( aError:Error ) 
			{
				trace( "AppConfig.mergeInString: " + aError );
			}
		}
		
		public function mergeInObjectProperty( aObj:Object, aProp:String, aBase64:Boolean = true ): void
		{
			try
			{
				if ( aObj != null )
				{
					if ( aObj.hasOwnProperty(aProp) )
					{
						for(var aKeyStr:String in aObj)
						{
							//trace(aKeyStr + ": " + aObj[aKeyStr] );
							if ( aProp == aKeyStr )
							{
								this.mergeInString(aObj[aKeyStr], aBase64);
								break;
							}
						}
					}
				}
			}
			catch ( aError:Error ) 
			{
				trace( "AppConfig.mergeInObjectProperty: " + aError );
			}
		}

		public function getTagIndex( aTag:String, aAttRegExp:RegExp = null ): int
		{
			var aRet_getTagIndex:int = -1;
			if ( this.iXML != null )
			{
				var aXMLList:XMLList = this.iXML.child("v");
				for ( var a:Number = 0; aXMLList != null && a < aXMLList.length(); a++ )
				{
					if ( aXMLList[a].hasOwnProperty("@t") && ( aXMLList[a].@t == aTag ) )
					{
						var aWanted:Boolean = (aAttRegExp == null);
						var aAtt:String = null;
						if ( !aWanted && aXMLList[a].hasOwnProperty("@a") )
						{
							try
							{
								aAtt = aXMLList[a].@a;
								var aRes:Object = aAttRegExp.exec(aAtt);
								aWanted = ( aRes != null ) ;
							}
							catch (aError:Error) 
							{
								trace(aError);
							}
						}
						if ( aWanted )
							aRet_getTagIndex = aXMLList[a].childIndex();
						break;
					}
				}
			}
			return aRet_getTagIndex;
		}

		public function getValueText( aTag:String, aAttRegExp:RegExp = null ): String
		{
			var aRet_getValue:String = null;
			var aV:XML = this.getNode( aTag, aAttRegExp );
			if ( aV != null )
			{
				aRet_getValue = aV.text();
			}
			return aRet_getValue;
		}

		public function getNode( aTag:String, aAttRegExp:RegExp = null ): XML
		{
			var aRet_getTagIndex:int = this.getTagIndex( aTag, aAttRegExp );
			if ( aRet_getTagIndex > -1 )
			{
				return this.iXML.elements()[aRet_getTagIndex];
			}
			return null;
		}

		public function getAttr( aTag:String, aAttRegExp:RegExp = null ): String
		{
			var aRet_getValue:String = null;
			var aRet_getTagIndex:int = this.getTagIndex( aTag, aAttRegExp );
			if ( aRet_getTagIndex > -1 )
			{
				if ( this.iXML.elements()[aRet_getTagIndex].hasOwnProperty("@a") )
					aRet_getValue = this.iXML.elements()[aRet_getTagIndex].@a;
			}
			return aRet_getValue;
		}
		
		public function replaceValueNode( aTag:String, aVal:*, aAtt:String = null/*, aAsXML:Boolean = false*/ ): void
		{
			var aRet_getTagIndex:int = 0;
			var aV:XML = new XML(aVal);
			aV.@t = aTag;
			if ( aAtt != null )
				aV.@a = aAtt;
			aRet_getTagIndex = this.getTagIndex( aTag );
			if ( aRet_getTagIndex > -1 )
			{
				if ( aAtt == null )
				{
					if ( this.iXML.elements()[aRet_getTagIndex].hasOwnProperty("@a") )
						aV.@a = this.iXML.elements()[aRet_getTagIndex].@a;
				}
				this.iXML.replace( aRet_getTagIndex, aV );
			}
			else
				this.iXML.appendChild( aV );
//trace( aV.toXMLString() ); trace( this.iXML.toXMLString() );				
		}
		
		public function replaceValueText( aTag:String, aVal:String, aAtt:String = null ): void
		{
			var aV:XML = this.getNode(aTag);
			if ( aV != null )
			{
				var aVx:XMLList = aV.elements("*");
				if ( aVx != null && aVx.length() )
				{
					aV.replace("*", AppConfig.cdata(aVal));
					aV.appendChild(	aVx );	
				}
				else
				{
					aVx = aV.children();
					if ( aVx != null && aVx.length() )
						aV.replace("*", AppConfig.cdata(aVal));
					else
						aV.appendChild(	AppConfig.cdata(aVal) );	
				}
			}
			else
			{
				aV = <v>{cdata(aVal)}</v>;
				aV.@t = aTag;
				this.iXML.appendChild( aV );
			}

			if ( aAtt != null )
				aV.@a = aAtt;
//trace( aV.toXMLString() ); trace( this.iXML.toXMLString() );				
		}
		
		public function getPairs( aArray:Array, aAttRegExp:RegExp = null ): void
		{
			for ( var a:Number = 0; this.iXML != null && a < this.iXML.elements().length(); a++ )
			{
				var aWanted:Boolean = (aAttRegExp == null);
				var aAtt:String = null;
				if ( !aWanted && this.iXML.elements()[a].hasOwnProperty("@a") )
				{
					try
					{
						aAtt = this.iXML.elements()[a].@a;
						var aRes:Object = aAttRegExp.exec(aAtt);
						aWanted = ( aRes != null ) ;
					}
					catch (aError:Error) 
					{
						trace(aError);
					}
				}
				if ( aWanted && this.iXML.elements()[a].hasOwnProperty("@t") )
				{
					aArray.push( {Tag: this.iXML.elements()[a].@t, Val: this.iXML.elements()[a].text() } );
				}
			}
		}
		
		public function getArray( aAttRegExp:RegExp = null, aShallow:Boolean = true ): Array
		{
			var aArray:Array = null;
			
			if ( this.iXML != null )
			{
				aArray = new Array();
				for ( var a:Number = 0; a < this.iXML.elements().length(); a++ )
				{
					var aWanted:Boolean = (aAttRegExp == null);
					var aAtt:String = null;
					if ( !aWanted && this.iXML.elements()[a].hasOwnProperty("@a") )
					{
						try
						{
							aAtt = this.iXML.elements()[a].@a;
							var aRes:Object = aAttRegExp.exec(aAtt);
							aWanted = ( aRes != null ) ;
						}
						catch (aError:Error) 
						{
							trace(aError);
						}
					}
					if ( aWanted && this.iXML.elements()[a].hasOwnProperty("@t") )
					{
						var aObj:Object = {};
						var aAttrs:XMLList = this.iXML.elements()[a].attributes();
						for each (var aAttr:XML in aAttrs) 
						{
							aObj[aAttr.localName()] = aAttr.toString();
						}
						if ( aShallow )
						{
							aObj["v"] = this.iXML.elements()[a].text();
						}
						else
							aObj["v"] = this.iXML.elements()[a].toString();
						aArray.push( aObj );
					}
				}
			}
			return aArray;
		}

		static public function cdata(aCDATAval:*):XML
		{    
			var aRet_cdata:XML = new XML("<![CDATA[" + aCDATAval + "]]>");    
			return aRet_cdata;
		}
		
		
		//----------------------------------------------------
		// public static helpers
		static public function toXMLString( aObj:Object ):String
		{
			var x:XML = describeType(aObj);
			var xl:XMLList = x..variable;
			var xmlStr:String = "";
			
			xmlStr ="<o t=\""+ getQualifiedClassName(aObj) +"\">";
			
			for each(var n:XML in xl){
				if ( aObj[n.@name.toString()] != null )
				{
					xmlStr += "<v n=\"" +  n.@name.toString() + "\" t=\"" + n.@type.toString() + "\">";
					xmlStr += "<![CDATA[" + aObj[n.@name.toString()].toString() + "]]>";
					xmlStr += "</v>";			
				}
				else
					xmlStr += "<v n=\"" +  n.@name.toString() + "\" t=\"" + n.@type.toString() + "\" />";
			}
			
			xmlStr += "</o>";
			
			return xmlStr;
		}
		
		static public function parseXML(x:XML, aObj:Object, aStrict:Boolean = true):void
		{
			try{
				var xl:XMLList = x..v;
				if(xl.length() == 0){
					throw("DataObj Error: XML does not contain any variable nodes.");
				}else{
					for each(var n:XML in xl){
						var varNameStr:String = n.@n;
						var varTypeStr:String = n.@t;
						var varValueStr:String = n.toString();
						
						
						if(aObj.hasOwnProperty(varNameStr) == true || !aStrict){
							try{
								aObj[varNameStr] = varValueStr;
							}catch(e:Error){
								switch(varTypeStr){
									case "Array":
										aObj[varNameStr] = varValueStr.split(",");
									break;
									
									default:
                      					throw("DataObj Error: Conversion of " + varValueStr + " to " + varTypeStr + " failed for variable " + varNameStr + ". \r" + e.message);									
									break;
								}
								
							}
							
						}else{
							throw("DataObj Error: XML contains a variable node that is not a class variable.");
						}
					}
				}
			}catch(e:Error){
				throw(e.message);
			}
		}
		
		static public function parseXmlString(s:String, aObj:Object, aStrict:Boolean = true):void
		{
			try
			{
				var x:XML = new XML(s);
				AppConfig.parseXML(x,aObj,aStrict);
			}
			catch(e:Error)
			{
				throw(e.message);
			}
		}
		
		static public function parseXmlString2Obj(s:String):Object
		{
			var aObj:Object = null;
			try
			{
				var aStrict:Boolean = false;
				
				var x:XML = new XML(s);
				
				try
				{
					var varObjStr:String = x..o[0].@t;
					if ( varObjStr != null )
					{
						x = new XML(x..o[0]);
						var aClassRef:Class = getDefinitionByName(varObjStr) as Class;
						if ( aClassRef != null )
						{
							//trace( classRef ); 
							aObj = new aClassRef();
							aStrict = true;
						}
					}
				}
				catch(e:Error)
				{
					trace( "AppConfig::parseXML2Obj: " + e.message);
				}
				
				if ( aObj == null )
					aObj = new Object;
				
				AppConfig.parseXML(x,aObj,aStrict);
			}
			catch(e:Error)
			{
				throw(e.message);
			}
			return aObj;
		}

		static public function getFuncByName( aObj:Object, aName:String ) :Function
		{
			var aFunc:Function = null;
			try
			{
				var x:XML = describeType(aObj);
				var xl:XMLList = x..method;
				if(xl.length() != 0)
				{
					for each(var n:XML in xl)
					{
						if ( n.@name == aName )
						{
							aFunc = aObj[aName] as Function;
							break;
						}
					}
				}
			}
			catch(e:Error)
			{
				trace( "AppConfig::getFuncByName: " + e.message);
				aFunc = null;
			}
			
			return aFunc;
		}

		static public function date2Str( aDate:Date = null ) :String
		{
			var aDateStr:String = null;
			if ( aDate == null )
				aDate = new Date();
			try
			{
				var aD:Number = aDate.date;
				var aM:Number = aDate.month;
				aM += 1;
				var aY:Number = aDate.fullYear;
				aDateStr = aY + (( aM < 10 ) ? "0" : "") + aM + (( aD < 10 ) ? "0" : "") + aD;
			}
			catch(e:Error)
			{
				trace( "AppConfig::date2Str: " + e.message);
			}
			
			return aDateStr;
		}

		static public function str2Date( aDateStr:String = null, aLast:Boolean = false ) :Date
		{
			var aDate:Date = new Date();
			try
			{
				if ( aDateStr != null )
				{
					aDate = new Date(new int(aDateStr.substr(0,4)),  new int(aDateStr.substr(4,2)) - 1, new int(aDateStr.substr(6,2)));
					if ( aLast )
						aDate.setHours(23, 59, 59, 999);
				}
			}
			catch(e:Error)
			{
				trace( "AppConfig::str2Date: " + e.message);
			}
			
			return aDate;
		}

		static public function serializeDataProviderAsXml( aDataProvider:DataProvider ) : XML
		{
			var aXML:XML = new XML("<d />");
			try
			{
				if ( aDataProvider != null )
				{
					var aRow:XML = null;
					for ( var aR:int = 0; aR < aDataProvider.length; aR++ )
					{
						aRow = new XML("<r />");
						var aObj:Object = aDataProvider.getItemAt( aR );
						for(var aC:String in aObj)
						{
							aRow.appendChild( new XML("<" + aC + "><![CDATA[" + aObj[aC].toString() + "]]></" + aC + ">" ) );
						}
						aXML.appendChild( aRow );
					}
				}
			}
			catch(e:Error)
			{
				trace( "AppConfig::serializeDataProviderAsXml: " + e.message);
			}
			
			return aXML;
		}

		static public function parseCSV(aRawData:String, aColumnDelimiter:String = ";", aQualifier:String = "", aRowDelimiter:String = null ):Array 
		{
			var ii:Number, columnArray:Array, rowObject:Object;
			var returnArray:Array = new Array();
			if ( aRowDelimiter == null )
				aRowDelimiter = (aRawData.indexOf('\r\n') > -1) ? '\r\n' : (aRawData.indexOf('\r') > -1) ? '\r' : '\n';
			//var columnDelimiter:String = aQualifier + ',' + aQualifier;
			var columnDelimiter:String = aQualifier + aColumnDelimiter + aQualifier;
			var rowsArray:Array = aRawData.split(aRowDelimiter);
			/*
			if(!columns.length) {
				columns = removeQualifier(rowsArray.shift().toString(),
				aQualifier).split(columnDelimiter);
			}
			*/
			for(var i:Number = 0; i < rowsArray.length; i++) {
				columnArray = removeQualifier(rowsArray[i].toString(),aQualifier).split(columnDelimiter);
				//if(columnArray.length == columns.length) {
					rowObject = new Object();
					for(ii = (columnArray.length - 1); ii >= 0; ii--) {
						rowObject[ii/*columns[ii]*/] = columnArray[ii];
					}
					//returnArray.push(rowObject);
					returnArray.push(columnArray);
				//}
			}
			return returnArray;
		}

		static private function removeQualifier(aOrig:String, aQualifier:String):String 
		{
			var aModif:String = aOrig;
			if(aModif.charAt(0) == aQualifier) {
				aModif = aModif.substring(1);
			}
			if(aModif.charAt(aModif.length - 1) == aQualifier) {
				aModif = aModif.substring(0, (aModif.length - 1));
			}
			return aModif;
		}

	}
}
