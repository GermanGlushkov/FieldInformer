
'-------------- UPDATING DATA SOURCE IN OLAP DATABASE ------------------

Set dsoServer = CreateObject("DSO.Server")
dsoServer.Connect "localhost"
Set dsoDS = dsoServer.MDStores("DBSALESPP").DataSources(1)

strTemp=dsoDS.ConnectionString

intFirst=Instr(1, strTemp,  "Password=") + Len("Password=")
intSecond=Instr( intFirst , strTemp , ";")

If intSecond=0 Then 
	strTemp= Left(strTemp , intFirst-1) & "###SPP_PASS###"
Else
	strTemp= Left(strTemp , intFirst-1) & "###SPP_PASS###" & Right(strTemp , Len(strTemp)-intSecond+1)
End If

dsoDS.ConnectionString=strTemp

dsoDS.Update

Set dsoDS=Nothing
Set dsoServer = Nothing

'----------------------------------------------------------------------------------