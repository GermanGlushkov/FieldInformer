package be.boulevart.as3.security { 
	/**
	* Creates a new genuine unique identifier string.
	* @authors Sven Dens - http://www.svendens.be
	* @version 0.1
	*/
	import flash.system.*;
	
	public class GUID {
		/**
		* Variables
		* @exclude
		*/
		protected static var counter:Number = 0;
	
		/**
		* Creates a new Genuine Unique IDentifier. :)
		*/
		public static function create():String {
			var d:Date = new Date();
			var id1:Number = d.getTime();
			var id2:Number = Math.random()*Number.MAX_VALUE;
			var id3:String = "";
			try
			{
				if(flash.system.Capabilities.serverString != null) 
				{
					id3 = flash.system.Capabilities.serverString;
				}
			}
			catch(error:Error)
			{
			}
			return SHA1.calculate(id1+id3+id2+counter++);
		}
	}
}