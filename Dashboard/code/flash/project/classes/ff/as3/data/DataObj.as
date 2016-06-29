package ff.as3.data 
{
	import flash.utils.describeType;
	import flash.utils.getQualifiedClassName;
	
	import ff.as3.data.AppConfig;
	
	public class DataObj extends Object
	{
		
		//constructor
		public function DataObj():void{
			
		}
		
		public function toXMLString():String
		{
			return ff.as3.data.AppConfig.toXMLString( this );
		}
		
		public function parseXML(aXml:String):void
		{
			ff.as3.data.AppConfig.parseXML( aXml, this );
		}
	}
}