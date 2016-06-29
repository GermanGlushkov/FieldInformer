package
{
	import flash.geom.*;
	import fl.containers.*;
	import fl.core.*;
	import flash.display.*;
	import flash.net.*;
	import flash.events.*;
	import flash.display.*;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import flash.utils.*;
	import fl.controls.DataGrid;
	import fl.data.*;
	import fl.controls.dataGridClasses.*;
	import fl.events.*;
    import fl.controls.ComboBox;
    import fl.controls.CheckBox;
		
	public class ChartPropertyPanel extends ModalSprite
	{
		var _serviceUrl:String=""; //"http://localhost/FieldINformer2008/UI.Web/WebServices/DashboardService.aspx";
		var _userId:String=""; //15394";
		var _gaugeId:String=""; //"c4e158c4-1c7a-4795-9cff-7a1a544c44a9";
		
		var _xmlLoader:URLLoader = new URLLoader();
		var _xmlSaver:URLLoader = new URLLoader();
		var _xml:XML=null;
		
		var _seriesXml:XML=null;
		var _categoriesXml:XML=null;
		
		var _dynQuery:DynamicMultiQueryControl=null;
		
		function ChartPropertyPanel(serviceUrl:String, userId:String, gaugeId:String)
		{						
			_serviceUrl=serviceUrl;
			_userId=userId;
			_gaugeId=gaugeId;
			
			btnOk.addEventListener(MouseEvent.CLICK, onOk);
			btnCancel.addEventListener(MouseEvent.CLICK, onCancel);
			btnSelect.addEventListener(MouseEvent.CLICK, onSelectClick);
			btnPivot.addEventListener(MouseEvent.CLICK, onPivotClick);
			
			_xmlLoader.addEventListener(Event.COMPLETE, onLoadXML);
			_xmlSaver.addEventListener(Event.COMPLETE, onSaveXML);						
			
			// dataprovider for combo
			var dp = new DataProvider();
			dp.addItem( { label: "Bar" } );
			dp.addItem( { label: "Stacked Bar" } );
			dp.addItem( { label: "Line" } );
			
			_gridSeries.columns=["CAPTION", "CHARTTYPE"];						
			var ceSer:ComboBoxCellEditor=new ComboBoxCellEditor(_gridSeries);			
			ceSer.addEventListener(Event.CHANGE, onComboChange);
			ceSer.dataProvider = dp;
			_gridSeries.getColumnAt(1).width = 80;
			_gridSeries.getColumnAt(1).itemEditor = ceSer;
			_gridSeries.addEventListener(DataGridEvent.ITEM_EDIT_END, onEndEdit, false, -100); //priority important!

			_gridCategories.columns=["CAPTION"];
			var ceCat:ComboBoxCellEditor=new ComboBoxCellEditor(_gridCategories);			
			_gridCategories.addEventListener(DataGridEvent.ITEM_EDIT_END, onEndEdit, false, -100); //priority important!
			
			executeUrlRequest();
		}
		
		override public function get width():Number
		{
			return 350;
		}
		
		override public function get height():Number
		{
			return 400;
		}
		
		function onComboChange(evt:Event)
		{
			var cmb:ComboBoxCellEditor=evt.target as ComboBoxCellEditor;									
			_seriesXml..HEADER[cmb.owner.editedItemPosition.rowIndex].@CHARTTYPE=cmb.selectedLabel;
			if(cmb.selectedLabel=="Bar")
			{
				for each(var hdrXml:XML in _seriesXml..HEADER.(@CHARTTYPE=="Stacked Bar"))
					hdrXml.@CHARTTYPE=cmb.selectedLabel;
			}
			else if(cmb.selectedLabel=="Stacked Bar")
			{
				for each(var hdrXml:XML in _seriesXml..HEADER.(@CHARTTYPE=="Bar"))
					hdrXml.@CHARTTYPE=cmb.selectedLabel;
			}
			cmb.owner.selectedIndex=-1;
			cmb.owner.dataProvider=new DataProvider(_seriesXml);
			cmb.owner.invalidate();
		}
		
		function onEndEdit(evt:DataGridEvent)
		{
			var dg:DataGrid=evt.target as DataGrid;									
			var item:Object=dg.getItemAt(uint(evt.rowIndex));
			
			// revert if empty
			if(item.CAPTION==null || item.CAPTION.replace(/^\s+|\s+$/gs, "")=="") // this is FUCKING  TRIM!!!
				item.CAPTION=_seriesXml..HEADER[evt.rowIndex].@CAPTION;
			else
			{
				//assign to xml
				if(dg==_gridSeries)
					_seriesXml..HEADER[evt.rowIndex].@CAPTION=item.CAPTION;
				else if(dg==_gridCategories)
					_categoriesXml..HEADER[evt.rowIndex].@CAPTION=item.CAPTION;
			}
			
			dg.selectedIndex=-1;
		}		
		
		function onSelectClick(evt:MouseEvent)
		{
			if(_dynQuery==null)
			{
				_dynQuery=new DynamicMultiQueryControl();
				_dynQuery.addEventListener("QueryChanged", onQueryXmlChanged);
			}
			var valuesXml:XML=null;
			if(_xml.GAUGE[0].VALUES.length()>0)
				valuesXml=_xml.GAUGE[0].VALUES[0];
			_dynQuery.init(_serviceUrl, _userId, valuesXml);
			_dynQuery.show(stage);
		}
		
		function onPivotClick(evt:MouseEvent)
		{
			for each(var hdrXml:XML in _xml..HEADER)
			{
				if(hdrXml.@TYPE=="SERIES")
					hdrXml.@TYPE="CATEGORIES";
				else
					hdrXml.@TYPE="SERIES";
			}
			
			refreshControls();
		}
		
		function onQueryXmlChanged(evt:Event)
		{
			var valuesXml:XML=_dynQuery.getValuesXml();
			_xml.GAUGE[0].VALUES=valuesXml;
			refreshControls();
		}		
		
		
		public function refreshControls()
		{			
			txtName.text=_xml..GAUGE[0].@NAME;
			chkShowLegend.selected=(_xml..GAUGE[0].VALUES.@LEGEND=="1");				
			dynRefresh.initStatic(String(Number(_xml..GAUGE[0].@REFRESH)/60), 0, NaN);						
			
			// add chart types if needed
			for each(var hdrXml:XML in _xml..HEADERS[0].HEADER)
			{
				if(hdrXml.@CHARTTYPE==null || hdrXml.@CHARTTYPE=="" || hdrXml.@CHARTTYPE==undefined)
					hdrXml.@CHARTTYPE="Bar";
			}
			
			//_gridSeries.headerHeight=0;			
			var seriesXmlList:XMLList=_xml..HEADERS[0].HEADER.(@TYPE=="SERIES");			
			_seriesXml=<LIST></LIST>;
			_seriesXml.appendChild(seriesXmlList);						
			_gridSeries.dataProvider=new DataProvider(_seriesXml);			
			
			//_gridCategories.headerHeight=0;
			var catXmlList:XMLList=_xml..HEADERS[0].HEADER.(@TYPE=="CATEGORIES");
			_categoriesXml=<LIST></LIST>;
			_categoriesXml.appendChild(catXmlList);			
			_gridCategories.dataProvider=new DataProvider(_categoriesXml);
			
			
			/*
			_gridSeries.columns=["CAPTION"];
			for each(var headerXml:XML in _xml..HEADER)
			{
				trace(headerXml.@CAPTION);
				if(headerXml.@TYPE=="SERIES")
					_gridSeries.addItem({CAPTION: headerXml.@CAPTION});
				if(headerXml.@TYPE=="CATEGORIES")
					_gridCategories.addItem({CAPTION: headerXml.@CAPTION});
			}
			*/
			
		}
		
		
		function executeUrlRequest()
		{
			// xml request
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;
			req.data = "<COMMAND TYPE='GetUserGaugeConfig' USERID='" + _userId + "'>" + 
				"<GAUGE ID='" + _gaugeId + "' QUERYDEF='1' QUERYRESULT='1'/>" + 
			"</COMMAND>";
		
			_xmlLoader.load(req);
		}
		
		function onLoadXML (e:Event)
		{	
			try
			{
				_xml = new XML(e.target.data);				
				refreshControls();			
			}
			catch(exc:Error)
			{
				trace(exc.message);
			}
			
		}
		
		
		function saveConfig()
		{
			// xml request
			var req:URLRequest = new URLRequest(_serviceUrl);		
			req.method=URLRequestMethod.POST;

			var reqData:XML=<COMMAND TYPE='SaveUserGaugeConfig'><GAUGE><VALUES/></GAUGE></COMMAND>;
			reqData.GAUGE[0]=_xml.GAUGE[0];
			reqData.@USERID=_userId;
			reqData.GAUGE[0].@ID=_gaugeId;
			reqData.GAUGE[0].@NAME=txtName.text;
			reqData.GAUGE[0].@REFRESH=String(dynRefresh.getStaticValue()*60);
			
			reqData.GAUGE[0].VALUES[0].@LEGEND="0";
			if (chkShowLegend.selected)
				reqData.GAUGE[0].VALUES[0].@LEGEND="1";
			
			trace(reqData);
			
			if(_seriesXml!=null)
			{
				reqData.GAUGE[0].VALUES.HEADERS=<HEADERS/>;
				reqData.GAUGE[0].VALUES.HEADERS.appendChild(_seriesXml.HEADER);
				reqData.GAUGE[0].VALUES.HEADERS.appendChild(_categoriesXml.HEADER);
			}						
			
			req.data=reqData.toXMLString();			
			//trace("REQUEST", req.data);
			_xmlSaver.load(req);
		}		
		
		function onSaveXML (e:Event)
		{				
			dispatchEvent(new Event("ConfigSaved", true));
			this.hide();
		}
		
		function onOk(evt:MouseEvent)
		{
			saveConfig();
		}
		
		function onCancel(evt:MouseEvent)
		{
			this.hide();
		}
	}
}