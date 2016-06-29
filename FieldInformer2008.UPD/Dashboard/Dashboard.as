package
{
	import flash.geom.*;
	import fl.containers.*;
	import flash.display.*;
	import flash.net.*;
	import flash.events.*;
	import flash.display.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.*;
	import flash.ui.*;
	import fl.controls.*;
	import fl.events.*;
	
	import com.yahoo.astra.fl.controls.Menu; 
	import com.yahoo.astra.fl.events.MenuEvent;
	
	public class Dashboard extends MovieClip
	{
		
		var _serviceUrl:String=""; //"http://www.fieldforce.com/WebServices/DashboardService.aspx";
		var _userId:String=""; //"15394";
		
		
		var _xmlLoader:URLLoader = new URLLoader();
		var _xmlSaver:URLLoader = new URLLoader();
		
		var _loadReq:URLRequest=null;
		var _saveReq:URLRequest=null;
		
		var _mnu:Menu=null;
		var _mnuIntervalId:int;
		var _mnuTween:Tween;
		
		function Dashboard()
		{					
			//loaderComplete(null);
			loaderInfo.addEventListener(Event.COMPLETE, loaderComplete);			
		
			contextMenu=new ContextMenu();
			contextMenu.hideBuiltInItems();			
			
			stage.addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
			txtGaugePanel.addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);			

			_xmlLoader.addEventListener(Event.COMPLETE, onLoadInitXML);
			_xmlSaver.addEventListener(Event.COMPLETE, onSaveNewGauge);			
			txtGaugePanel.visible=false;									
			
		}
		
		function loaderComplete(evt:Event)
		{
			_userId=loaderInfo.parameters.userId;
			_serviceUrl=loaderInfo.parameters.serviceUrl;			
			if(_serviceUrl==null || _serviceUrl=="")
				_serviceUrl="http://213.180.3.218/eforce.net/WebServices/DashboardService.aspx";
			if(_userId==null || _userId=="")
				_userId="15394";
			
		  	_loadReq = new URLRequest(_serviceUrl);		
			_loadReq.method=URLRequestMethod.POST;
			_loadReq.data = "<COMMAND TYPE='GetUserGauges' USERID='" + _userId + "'/>";
			_xmlLoader.load(_loadReq);
		}


		function onMouseDown(evt:MouseEvent)
		{	
			if((evt.target==stage || evt.target==txtGaugePanel) && _saveReq==null)
			{
				txtGaugePanel.visible=false;
				showMenu();
			}
			else if(_mnu==null || !_mnu.hitTestPoint(evt.stageX, evt.stageY))
				hideMenu();			
		}
		
		
		function showMenu()
		{			
			if(_mnu==null)
			{
				var mnuItems:XML =   
				<root>  
					<menuitem label="New TMeter" />  
					<menuitem label="New DGauge" />  
					<menuitem label="New Chart" />  
				</root>  
				_mnu = Menu.createMenu(stage, mnuItems);
				_mnu.addEventListener(MenuEvent.ITEM_CLICK, onMenuClick); 
				_mnu.width=100;
			}
			
			var x:int=stage.mouseX-_mnu.width-1;
			if(x<0)
				x=0;										
			var y:int=stage.mouseY-1;
			_mnu.show(x, y);
			stage.focus=null;
			
			
			if(_mnuTween!=null)
				_mnuTween.stop();
			_mnu.alpha=1;
			
			clearInterval(_mnuIntervalId);
			_mnuIntervalId=setInterval(hideMenuInact, 3000);
		}
		
		function hideMenuInact()
		{					
			clearInterval(_mnuIntervalId);
			_mnuIntervalId=setInterval(hideMenu, 2100);
						
			_mnuTween = new Tween(_mnu, "alpha", Strong.easeOut, _mnu.alpha, 0, 2, true);
			_mnuTween.start();						
		}
		
		function hideMenu()
		{			
			if(_mnu!=null)
				_mnu.visible=false;
			//if(_mnu!=null && _mnu.parent!=null)
			//	stage.removeChild(_mnu);
		}
				
		function onLoadInitXML (e:Event)
		{	
			try
			{
				var gauges:int=0;
				var xml:XML = new XML(e.target.data);
				trace(e.target.data);
				var gaugesList:XMLList = xml..GAUGE;
				for each (var gaugeEl:XML in gaugesList) 
				{
					var gc:GaugeContainer = new GaugeContainer(_serviceUrl, _userId, gaugeEl);
					addChildAt(gc, numChildren);
					gc.load();
					gauges++;
				}				
				
				// if there's nothing exempt panel
				if(gauges==0)
				{
					txtGaugePanel.visible=true;
					var t:Tween = new Tween(txtGaugePanel, "alpha", Strong.easeOut, 1, 0, 4, true);
					var t1:Tween = new Tween(txtGaugePanel, "x", Strong.easeIn, 1, stage.width, 2, true);
					//t.start();						
					//t1.start();
				}
			}
			catch(exc:Error)
			{
				trace("GetUserGauges xml load failed");
				trace(exc.message);
				txtGaugePanel.text=exc.message;
			}
			
		}
		
		function onMenuClick(evt:MenuEvent)
		{
			var xml:XML=null;
			if(evt.label=="New TMeter")
			{
				xml=
				<COMMAND TYPE='SaveUserGaugeConfig'>
					<GAUGE NAME='TMeter' TYPE='TMeter' WIDTH='110' HEIGHT='300' />
				</COMMAND>;
			}
			else if(evt.label=="New DGauge")
			{
				xml=
				<COMMAND TYPE='SaveUserGaugeConfig'>
					<GAUGE NAME='DGauge' TYPE='DGauge' WIDTH='194' HEIGHT='202' />
				</COMMAND>;
			}
			else if(evt.label=="New Chart")
			{
				xml=
				<COMMAND TYPE='SaveUserGaugeConfig'>
					<GAUGE NAME='Chart' TYPE='Chart' WIDTH='300' HEIGHT='300' />
				</COMMAND>;
			}
			
			if(xml!=null)
			{
				xml.@USERID=_userId;
				xml.GAUGE.@X=evt.target.x+evt.target.width;
				xml.GAUGE.@Y=evt.target.y;
				xml.GAUGE.@ID=GUID.create();
				xml.GAUGE.@VISIBLE=1;
				xml.GAUGE.@REFRESH=60*60;
					
				_saveReq = new URLRequest(_serviceUrl);		
				_saveReq.method=URLRequestMethod.POST;
				_saveReq.data = xml.toXMLString();
				_xmlSaver.load(_saveReq);
			}
			
			hideMenu();
		}
		
		
		
		function onSaveNewGauge (e:Event)
		{				
			try
			{					
				if(_saveReq!=null && (e.target.data==null || e.target.data==""))
				{
					var saveXml:XML=new XML(_saveReq.data);
					var gc:GaugeContainer = new GaugeContainer(_serviceUrl, _userId, saveXml.GAUGE[0]);
					addChild(gc);
					gc.load();
					_saveReq=null;
					txtGaugePanel.visible=false;
				}
				
			}
			catch(exc:Error)
			{
				trace(exc.message);
			}
			
		}

	}
}