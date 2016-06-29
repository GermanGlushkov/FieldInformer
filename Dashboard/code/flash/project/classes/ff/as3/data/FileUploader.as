package ff.as3.data { 

	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
	import flash.net.sendToURL;
	
	import flash.net.FileFilter;
	import flash.net.FileReference;
	
	import flash.net.URLVariables;

	import flash.xml.XMLDocument;
	import flash.xml.XMLNode;

	import flash.display.*;
	import flash.events.*;
	import fl.controls.*;

	import be.boulevart.as3.security.*;
	import ff.as3.data.DataLoaderRecurring;

	import ff.as3.ui.FileUploaderUI;

	public class FileUploader extends ff.as3.data.DataLoaderRecurring	
	{
		protected var iFileRef:FileReference = null;

		protected var iHandlerOnFileReference_Select:Function = null;

		/**
		 * Constructor.
		 */

		public function FileUploader(aConnectionString:String, aExternalParams:Object, aDelayMilliseconds:Number = 0)
		{
			super(aConnectionString, aExternalParams, aDelayMilliseconds);
			
			this.iFileRef = new FileReference();
			this.iFileRef.addEventListener(Event.CANCEL, FileReference_cancelHandler);
			this.iFileRef.addEventListener(Event.COMPLETE, FileReference_completeHandler);
			this.iFileRef.addEventListener(HTTPStatusEvent.HTTP_STATUS, FileReference_httpStatusHandler);
			this.iFileRef.addEventListener(IOErrorEvent.IO_ERROR, FileReference_ioErrorHandler);
			this.iFileRef.addEventListener(Event.OPEN, FileReference_openHandler);
			this.iFileRef.addEventListener(ProgressEvent.PROGRESS, FileReference_progressHandler);
			this.iFileRef.addEventListener(SecurityErrorEvent.SECURITY_ERROR, FileReference_securityErrorHandler);
			this.iFileRef.addEventListener(Event.SELECT, FileReference_selectHandler);
			this.iFileRef.addEventListener(DataEvent.UPLOAD_COMPLETE_DATA, FileReference_uploadCompleteDataHandler);
		}

		//----------------------------------------------------
		// public properties
		
		override public function get className():String 
		{
			return "ff.data.FileUploader";
		}
		

		public function get FileRef():FileReference 
		{
			return this.iFileRef;
		}		
//		public function set FileReference(aFileReference:FileReference):FileReference 
//		{
//			return this.iFileRef = aFileReference;
//		}		
		
		
		//----------------------------------------------------
		// delegates
		
		public function set HandlerOnFileReference_Select( aHandler:Function )
		{
			this.iHandlerOnFileReference_Select = aHandler;
		}
		
		
		//----------------------------------------------------
		// public helpers

		override public function Cancel():void 
		{
			super.Cancel();
			try 
			{
				if ( this.iFileRef != null )
				{
					this.iFileRef.cancel();
				}
			} 
			catch (aError:Error) 
			{
				trace(this.className+".Cancel: " + aError);
			}
		}		

		public function Upload(aRequest:String, aCrypt:Boolean = true, aZip:Boolean = true) : void
		{
			try 
			{
				if ( this.iHandlerOnBeginLoad != null )
				{
					this.iHandlerOnBeginLoad( this );
				}
				var aURLRequest:URLRequest = this.PrepareURLRequest(aRequest, aCrypt, aZip, this.ConnectionString, this.iKeyString, this.iRijndael);
				this.iFileRef.upload(aURLRequest);
			} 
			catch (aError:Error) 
			{
				trace(aError);
				if ( this.iHandlerOnError != null )
				{
					this.iHandlerOnError( this, this.className+".Upload", aError );
				}
				else
					throw aError;
			}
		}
		
		
		//----------------------------------------------------
		// protected event's handlers

		protected function FileReference_cancelHandler(event:Event):void {
			trace(this.className + ".FileReference_cancelHandler: " + event);
		}
		
		protected function FileReference_completeHandler(event:Event):void {
			trace(this.className + ".FileReference_completeHandler: " + event);
		}
		
		protected function FileReference_uploadCompleteDataHandler(event:DataEvent):void 
		{
			trace(this.className + ".FileReference_uploadCompleteData: " + event.data);
			var aResponse:String = event.data.toString();
			var a1st:String = aResponse.charAt(0);
			while ( aResponse.length > 0 && a1st != "<" )
			{
				aResponse = aResponse.substring(1);
				if ( aResponse.length > 0 )
					a1st = aResponse.charAt(0);
			}
			this.ProcessUrlResponse( aResponse );
		}
		
		protected function FileReference_httpStatusHandler(event:HTTPStatusEvent):void {
			trace(this.className + ".FileReference_httpStatusHandler: " + event);
		}
		
		protected function FileReference_ioErrorHandler(event:IOErrorEvent):void {
			trace(this.className + ".FileReference_ioErrorHandler: " + event);
			if ( this.iHandlerOnError != null )
			{
				try
				{
					this.iHandlerOnError( this, this.className+".FileReference_ioErrorHandler", event.text );
				}
				catch (aError:Error) 
				{
					trace(aError);
				}
			}
		}
		
		protected function FileReference_openHandler(event:Event):void 
		{
			trace(this.className + ".FileReference_openHandler: " + event);
			//txtResult.text="openHandler: " + iUploadURL.data;
		}
		
		protected function FileReference_progressHandler(event:ProgressEvent):void 
		{
			var file:FileReference = FileReference(event.target);
			trace(this.className + ".FileReference_progressHandler name=" + file.name + " bytesLoaded=" + event.bytesLoaded + " bytesTotal=" + event.bytesTotal);
		}
		
		protected function FileReference_securityErrorHandler(event:SecurityErrorEvent):void 
		{
			trace(this.className + ".FileReference_securityErrorHandler: " + event);
			if ( this.iHandlerOnError != null )
			{
				try
				{
					this.iHandlerOnError( this, this.className+".FileReference_securityErrorHandler", event.text );
				}
				catch (aError:Error) 
				{
					trace(aError);
				}
			}
		}
		
		protected function FileReference_selectHandler(event:Event):void
		{
			var aFile:FileReference = FileReference(event.target);
			trace(this.className + ".FileReference_selectHandler: name=" + aFile.name );//" + URL=" + uploadURL.url);
			
			if ( this.iHandlerOnFileReference_Select != null )
				this.iHandlerOnFileReference_Select( this, aFile );
		}
		
	}
}

