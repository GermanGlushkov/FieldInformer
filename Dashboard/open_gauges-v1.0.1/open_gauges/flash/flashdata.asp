<%@ LANGUAGE = VBScript%>
<%
'Get the value sent over by Flash; the function you wish to execute or data you wish to display.
' This can be perl, javascript, whatever. I think between the PHP examples and this it should be
' fairly clear.

Dim strFunc
strFunc = Request.QueryString("funcName")

If strFunc = "pgauge_data" Then
	Dim live_value
	live_value = "60"
	Response.Write("value="+live_value)
End If
%>

