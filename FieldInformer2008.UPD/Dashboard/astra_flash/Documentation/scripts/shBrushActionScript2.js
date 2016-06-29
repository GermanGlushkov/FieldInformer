dp.sh.Brushes.ActionScript2 = function() {
	var void_str = 'Void';
	var return_str = 'return';
	var public_str = 'public';
	var private_str = 'private';
	var core_constants = 'true false null undefined';
	var keywords = 'package add and break case catch class continue default delete do dynamic else eq extends finally for function ge get gt if ifFrameLoaded implements import in instanceof interface intrinsic le lt ne new not on onClipEvent or set static switch tellTarget this throw try typeof var void while with abstract enum export short byte long synchronized char debugger protected double volatile float throws transient goto';
	var core_str = 'Accessibility Accordion Alert Array Binding Boolean Button Camera CellRenderer CheckBox Collection Color ComboBox ComponentMixins ContextMenu ContextMenuItem CustomActions CustomFormatter CustomValidator DataGrid DataHolder DataProvider DataSet DataType Date DateChooser DateField Delta DeltaItem DeltaPacket DepthManager EndPoint Error FocusManager Form Function Iterator Key Label List Loader LoadVars LocalConnection Log Math Media Menu MenuBar Microphone Mouse MovieClip MovieClipLoader NetConnection NetStream Number NumericStepper Object PendingCall PopUpManager PrintJob ProgressBar RadioButton RDBMSResolver Screen ScrollPane Selection SharedObject Slide SOAPCall Sound Stage String StyleManager System TextArea TextField TextFormat TextInput TextSnapshot TransferObject Tree TreeDataProvider TypedValue UIComponent UIEventDispatcher UIObject Video WebService WebServiceConnector Window XML XMLConnector XUpdateResolver';

	this.regexList = [
		{ regex: dp.sh.RegexLib.DoubleQuotedString,								css: 'string' },			// strings
		{ regex: dp.sh.RegexLib.SingleQuotedString,								css: 'character' },			// strings
		{ regex: new RegExp('\\b([\\d]+(\\.[\\d]+)?|0x[a-f0-9]+)\\b', 'gi'),	css: 'number' },			// numbers
		{ regex: new RegExp(this.GetKeywords(keywords), 'gm'),					css: 'keyword' },			// ActionScript keywords
		{ regex: new RegExp(this.GetKeywords(void_str), 'gm'),					css: 'void' },				// Base Types 'Void'
		{ regex: new RegExp(this.GetKeywords(return_str), 'gm'),				css: 'return' },			// Keyword 'return'
		{ regex: new RegExp(this.GetKeywords(public_str), 'gm'),				css: 'public' },			// Keyword 'public'
		{ regex: new RegExp(this.GetKeywords(private_str), 'gm'),				css: 'private' },			// Keyword 'private'
		{ regex: new RegExp(this.GetKeywords(core_constants), 'gm'),			css: 'core_constants' },	// Constants true, false, [...]
		{ regex: new RegExp(this.GetKeywords(core_str), 'gm'),					css: 'core' },				// Core types from MM classes root
		{ regex: dp.sh.RegexLib.SingleLineCComments,							css: 'comments_single' },	// one line comments
		{ regex: new RegExp("/\\*[^\\*][\\s\\S]*?\\*/","gm"),					css: 'comments_multi' },	// multiline comments
		{ regex: new RegExp("/\\*\\*[\\s\\S]*?\\*/","gm"),						css: 'javadoc' }			// javadoc
	];

	var todo_regex = 	new RegExp("TODO\\b","gm");
	this.regexSubList = [
		{ css: 'javadoc_todo',		parent:"javadoc",			regex: todo_regex },						// JavaDoc TODO
		{ css: 'todo_multi',		parent:"comments_multi",	regex: todo_regex },						// Multiline TODO
		{ css: 'todo_single',		parent:"comments_single",	regex: todo_regex },						// Single line TODO
		{ css: 'javadoc_keyword',	parent:"javadoc",			regex: /@\w+\b/gm},							// JavaDoc keyword @word
		{ css: 'javadoc_link',		parent:"javadoc",			regex: /\{.*\}/g},							// JavaDoc link
		{ css: 'javadoc_html_tag',	parent:"javadoc",			regex: /&lt;(?:(?!&gt;).)*&gt;/gm}			// Javadoc HTML tags
	];
	this.CssClass = 'dp-as2';
	this.collapse = true;
}

dp.sh.Brushes.ActionScript2.prototype	= new dp.sh.Highlighter();
dp.sh.Brushes.ActionScript2.Aliases	= ['as2'];

if (dp.SyntaxHighlighter) {
	dp.SyntaxHighlighter.HighlightSubElements = function (elementName) {
		 var findSourceElements = function (topElement, className, out_array) {
		 	var element, result;
		 	for (var i=0, len = topElement.childNodes.length; i < len; i++) {
		 		 element = topElement.childNodes[i];
		 		 if (element.className == className) {
		 		 	  out_array.push(element);
		 		 } else if (element.childNodes.length) {
		 		 	  findSourceElements(element, className, out_array);
		 		 }
		 	}
		 	return out_array;
		 }
		 
		 var setSubclasses = function (element_list, css, parent, regex) {
		 	var found, replaceText;
		 	for (var i=0, len=element_list.length; i<len; i++) {
		 		var element = element_list[i];
		 		element.innerHTML = element.innerHTML.replace(regex, "<span class='"+css+"'>$&</span>");
		 	}
		 }
		 
		 var as2 = new dp.sh.Brushes.ActionScript2();
		 var sub_list = as2.regexSubList;

		 var sourceList = document.getElementsByName(elementName);
		 var element_list;
		 
		 for (var i=0, i_len=sub_list.length; i<i_len; i++) {
		 	var sub_item = sub_list[i];
		 	var parent = sub_item.parent;
		 	var css = sub_item.css;
		 	var regex = sub_item.regex;
			 for (var k=0, k_len=sourceList.length; k < k_len; k++) {
			 	  element_list = findSourceElements(sourceList[k].previousSibling, parent, []);
			 	  setSubclasses(element_list, css, parent, regex);
			 }
		 }
	}
} else {
	 // alert("Shit hapened");
}