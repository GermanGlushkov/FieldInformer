




'************ CREATE PACKAGE *************

strServer="."
strSaPass="###SA_PASS###"
strSppPass="###SPP_PASS###"
strSppIniPath="###SPP_INI_PATH###"  
strOlapServer="###OLAP_SERVER###"
strHideHierarchies="###HIDE_HIERARCHIES###"

'For i=0 to Wscript.Arguments.Count-1
'	If Left(Wscript.Arguments(i),5)="/sqls" Then
'		strServer=Mid(Wscript.Arguments(i),6)
'	ElseIf Left(Wscript.Arguments(i),4)="/sap" Then
'		strSaPass=Mid(Wscript.Arguments(i),5)
'	ElseIf Left(Wscript.Arguments(i),4)="/spp" Then
'		strSppPass=Mid(Wscript.Arguments(i),5)	
'	ElseIf Left(Wscript.Arguments(i),5)="/olap" Then
'		strOlapServer=Mid(Wscript.Arguments(i),6)
'	ElseIf Left(Wscript.Arguments(i),5)="/inip" Then
'		strSppIniPath=Mid(Wscript.Arguments(i),6)
'	End if		
'Next

'strOlapServer=Trim(strOlapServer)
'If strOlapServer="localhost" or strOlapServer="(local)" or strOlapServer="." or strOlapServer="" Then
	'strOlapServer="localhost"
'End IF



Call Main()

'***************************************





Private Sub Main()
	set goPackage = CreateObject("DTS.Package")

	goPackage.Name = "FieldInformer process  job"
	goPackage.WriteCompletionStatusToNTEventLog = False
	goPackage.FailOnError = False
	goPackage.PackagePriorityClass = 2
	goPackage.MaxConcurrentSteps = 4
	goPackage.LineageOptions = 0
	goPackage.UseTransaction = True
	goPackage.TransactionIsolationLevel = 4096
	goPackage.AutoCommitTransaction = True
	


'---------------------------------------------------------------------------
' create package steps information
'---------------------------------------------------------------------------

'Dim oStep as DTS.Step2
'Dim oPrecConstraint as DTS.PrecedenceConstraint

'------------- a new step defined below

Set oStep = goPackage.Steps.New

	oStep.Name = "DTSStep_DTSActiveScriptTask_1"
	oStep.Description = "CREATE MEASURES"
	oStep.ExecutionStatus = 1
	oStep.TaskName = "DTSTask_DTSActiveScriptTask_1"
	oStep.CommitSuccess = False
	oStep.RollbackFailure = False
	oStep.ScriptLanguage = "VBScript"
	oStep.AddGlobalVariables = True
	oStep.RelativePriority = 3
	oStep.CloseConnection = False
	oStep.ExecuteInMainThread = True   '!!!!!!!!!!!!!!!!!!!!!!!!!! important !!!!!!!!!!!!!!!!!!!!!
	oStep.IsPackageDSORowset = False
	oStep.JoinTransactionIfPresent = False
	oStep.DisableStep = False
	
goPackage.Steps.Add oStep
Set oStep = Nothing

'------------- a new step defined below

Set oStep = goPackage.Steps.New

	oStep.Name = "DTSStep_DTSActiveScriptTask_2"
	oStep.Description = "REBUILD CUBES + REPAIR REPORTS"
	oStep.ExecutionStatus = 1
	oStep.TaskName = "DTSTask_DTSActiveScriptTask_2"
	oStep.CommitSuccess = False
	oStep.RollbackFailure = False
	oStep.ScriptLanguage = "VBScript"
	oStep.AddGlobalVariables = True
	oStep.RelativePriority = 3
	oStep.CloseConnection = False
	oStep.ExecuteInMainThread = True   '!!!!!!!!!!!!!!!!!!!!!!!!!! important !!!!!!!!!!!!!!!!!!!!!
	oStep.IsPackageDSORowset = False
	oStep.JoinTransactionIfPresent = False
	
goPackage.Steps.Add oStep
Set oStep = Nothing

'------------- a precedence constraint for steps defined below

Set oStep = goPackage.Steps("DTSStep_DTSActiveScriptTask_2")
Set oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSActiveScriptTask_1")
	oPrecConstraint.StepName = "DTSStep_DTSActiveScriptTask_1"
	oPrecConstraint.PrecedenceBasis = 1
	oPrecConstraint.Value = 0
	
oStep.precedenceConstraints.Add oPrecConstraint
Set oPrecConstraint = Nothing

'---------------------------------------------------------------------------
' create package tasks information
'---------------------------------------------------------------------------

'------------- call Task_Sub1 for task DTSTask_DTSActiveScriptTask_1 (CREATE MEASURES)
Call Task_Sub1( goPackage	)

'------------- call Task_Sub2 for task DTSTask_DTSActiveScriptTask_2 (REBUILD CUBES + REPAIR REPORTS)
Call Task_Sub2( goPackage	)

'---------------------------------------------------------------------------
' Save or execute package
'---------------------------------------------------------------------------

on error resume next
goPackage.RemoveFromSQLServer strServer, "sa", strSaPass, , , , goPackage.Name
if err.number<>0 then err.clear
on error goto 0

goPackage.SaveToSQLServer strServer, "sa", strSaPass 



goPackage.Uninitialize
'to save a package instead of executing it, comment out the executing package line above and uncomment the saving package line
set goPackage = Nothing

set goPackageOld = Nothing

End Sub







'------------- define Task_Sub1 for task DTSTask_DTSActiveScriptTask_1 (CREATE MEASURES)
Public Sub Task_Sub1(ByVal goPackage)


Set oTask = goPackage.Tasks.New("DTSActiveScriptTask")
Set oCustomTask1 = oTask.CustomTask

	oCustomTask1.Name = "DTSTask_DTSActiveScriptTask_1"
	oCustomTask1.Description = "CREATE MEASURES"
	oCustomTask1.ActiveXScript = "'**********************************************************************" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'  Visual Basic ActiveX Script" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'************************************************************************" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Function Main()" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'##########################" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "strOLAP_SERVER=""" & strOlapServer & """" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "strOLAP_DB=""DBSALESPP""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "strOLAP_CUBE=""VIRTUAL""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "strSALESPP_DSN=""DBSALESPP""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "strSALESPP_UID=""spp""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "strSALESPP_PWD=""" & strSppPass & """" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "strSalesppIniPath=""" & strSppIniPath & """" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "strHideDimensions=""" & strHideHierarchies & """" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'##########################" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set dsoServer = CreateObject(""DSO.Server"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "dsoServer.Connect strOLAP_SERVER" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "dsoServer.Timeout=14400 ' 4 hours for each object process" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "dsoServer.Update ' Update the Analysis server" & vbCrLf	
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "    " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "   " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set dsoDB = dsoServer.MDStores(strOLAP_DB)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set dsoDS = dsoDB.DataSources(1)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set cnn  = CreateObject(""ADODB.Connection"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set rst = CreateObject(""ADODB.Recordset"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "cnn.ConnectionTimeout=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "cnn.CommandTimeout=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "cnn.Open ""DSN="" & strSALESPP_DSN & "";User ID="" & strSALESPP_UID & "";Password="" & strSALESPP_PWD & "";"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- MAKE ALL MEASURES INVISIBLE -----------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	For Each msr in  dsoDB.MDStores(""VIRTUAL"").Measures" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		msr.IsVisible=False" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'----------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'If instr(1 , msr.Name , ""Wrk"")>0 then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			'msr.IsVisible=True" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'end if" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'----------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	Next" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'-----------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'------------------------DELETE ALL CALC MEMBERS------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	For Each cmd in  dsoDB.MDStores(""VIRTUAL"").Commands" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'msgbox cmd.Statement" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		dsoDB.MDStores(""VIRTUAL"").Commands.Remove(cmd.Name)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	Next	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'-----------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strStatement(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strMember(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strFormat(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strSolveOrder(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strVisible(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strLevel1(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strLevel2(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strDescription(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Dim strParentDimension(500)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "n=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'------------------------------------ STORE MEASURES -------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Store Distinct Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Store Hierarchy""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Salesforce].CurrentMember IS [Salesforce].[Salesforce].&[0] OR [Store].[Salesforce].CurrentMember IS [Store].[Salesforce].[Salesforce].&[0] , NULL , ValidMeasure([Measures].[StoreDistinctCount]) ) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Store Turnover""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Store Hierarchy""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" IIF ( [Salesforce].CurrentMember.Level.Ordinal>0 or [Store].[Salesforce].CurrentMember.Level.Ordinal>0 ,  IIF(ValidMeasure([Measures].[StoreSalesforceTurnover])=0 , NULL ,  ValidMeasure([Measures].[StoreSalesforceTurnover])) , IIF(ValidMeasure([Measures].[StoreTurnover])=0 , NULL ,  ValidMeasure([Measures].[StoreTurnover])) ) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- CALENDAR  MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'----------------------constructing member statement ----------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'strCalendarStatement="" ValidMeasure([Measures].[GlobWrkdaySum]) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'For Each hier in cdf.Dimensions(""Salesforce"").Hierarchies(0).levels(1).Members" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "				'strCalendarStatement= ""IIF ( [Salesforce].["" & hier.Name & ""].CurrentMember.Level.Ordinal=0 ,  [Measures].[WrkDaySum]  ,  ""  & strCalendarStatement  & ""  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "				'strCalendarStatement= ""IIF ( [Store].[Salesforce].["" & hier.Name & ""].CurrentMember.Level.Ordinal=0 ,  ([Measures].[WrkDaySum] , StrToMember(""""[Salesforce].["" & hier.Name  & ""].["""" +  [Store].[Salesforce].["" & hier.Name & ""].CurrentMember.Name  + """"]""""))  ,  ""  & strCalendarStatement  & ""  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "				" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'Next	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'strCalendarStatement=REPLACE(strCalendarStatement , ""'"" , ""''"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'-------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SalesCallCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Store Distinct Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SalesCallStoreDistCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Length Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SalesCallLenSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Length Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SalesCallLenSum])/ValidMeasure([Measures].[SalesCallCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Length Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SalesCallLenMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Length Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SalesCallLenMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Travel Time Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SalesCallLenDaySum])-ValidMeasure([Measures].[SalesCallLenSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Travel Time Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""(ValidMeasure([Measures].[SalesCallLenDaySum])-ValidMeasure([Measures].[SalesCallLenSum]))/ValidMeasure([Measures].[SalesCallDayCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Working Days Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" IIF ( [Salesforce].CurrentMember.Level.Ordinal=2 or [Store].[Salesforce].CurrentMember.Level.Ordinal=2 ,  ValidMeasure([Measures].[GlobWrkdaySum]) + ValidMeasure([Measures].[WrkDayException]) , ValidMeasure([Measures].[GlobWrkdaySum])  ) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Field Days Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" IIF ( [Salesforce].CurrentMember.Level.Ordinal=2 or [Store].[Salesforce].CurrentMember.Level.Ordinal=2 ,  ValidMeasure([Measures].[GlobWrkdaySum]) + ValidMeasure([Measures].[FieldDayException]) , ValidMeasure([Measures].[GlobWrkdaySum])  ) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Sales Call Frequency""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Sales Calls and Calendar""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" IIF( [Measures].[Working Days Count]>0  , ValidMeasure([Measures].[SalesCallCount])/[Measures].[Working Days Count] , NULL ) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End IF" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- DPM and SELECTION MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSemiadditiveStatement=""IIF( [Time].[Monthly].CurrentMember.Level.Ordinal=3 , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" Sum( Except({ PeriodsToDate([Time].[Monthly].[(All)] , Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Year)   )  , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" PeriodsToDate([Time].[Monthly].[Year] , Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Month) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" PeriodsToDate([Time].[Monthly].[Month] , Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Date ) ) } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Year)  ,   Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Month )     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure(( [Measures].[DpmMeasuredSum]  , [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" IIF( [Time].[Monthly].CurrentMember.Level.Ordinal=2 ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" Sum( Except({ PeriodsToDate([Time].[Monthly].[(All)] , Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Year)   )  ,"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" PeriodsToDate([Time].[Monthly].[Year] , Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Month) )  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" {  Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Year)   }  ) ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure(( [Measures].[DpmMeasuredSum]  , [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))  )  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" IIF( [Time].[Monthly].CurrentMember.Level.Ordinal=1 ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" Sum( { PeriodsToDate([Time].[Monthly].[(All)] , Ancestor([Time].[Monthly].CurrentMember , [Time].[Monthly].Year)  ) }  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure(( [Measures].[DpmMeasuredSum] , [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]) )   )  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""IIF( [Time].[Weekly].CurrentMember.Level.Ordinal=3 , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" Sum( Except({ PeriodsToDate([Time].[Weekly].[(All)] , Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Year)   )  , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" PeriodsToDate([Time].[Weekly].[Year] , Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Week) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" PeriodsToDate([Time].[Weekly].[Week] , Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Date ) ) } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""{  Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Year)  ,   Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Week )     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" ValidMeasure(( [Measures].[DpmMeasuredSum]  , [Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]))    )  , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" IIF( [Time].[Weekly].CurrentMember.Level.Ordinal=2 ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" Sum( Except({ PeriodsToDate([Time].[Weekly].[(All)] , Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Year)   )  ,"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" PeriodsToDate([Time].[Weekly].[Year] , Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Week) )  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" {  Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Year)   }  ) ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" ValidMeasure(( [Measures].[DpmMeasuredSum]  , [Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]))  )  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" IIF( [Time].[Weekly].CurrentMember.Level.Ordinal=1 ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" Sum( { PeriodsToDate([Time].[Weekly].[(All)] , Ancestor([Time].[Weekly].CurrentMember , [Time].[Weekly].Year)  ) }  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"" ValidMeasure(( [Measures].[DpmMeasuredSum] , [Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]) )   )  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "						"" IIF( [Time].[Salesperiod].CurrentMember.Level.Ordinal=2 ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "						"" Sum( Except({ PeriodsToDate([Time].[Salesperiod].[(All)] , Ancestor([Time].[Salesperiod].CurrentMember , [Time].[Salesperiod].Salesperiod)   )  ,"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "						"" PeriodsToDate([Time].[Salesperiod].[Salesperiod] , Ancestor([Time].[Salesperiod].CurrentMember , [Time].[Salesperiod].Date) )  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "						"" {  Ancestor([Time].[Salesperiod].CurrentMember , [Time].[Salesperiod].Salesperiod)   }  ) ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "						"" ValidMeasure(( [Measures].[DpmMeasuredSum]  , [Time].[Monthly].[All Time Monthly] , [Time].[Weekly].[All Time Weekly]))  )  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "						"" IIF( [Time].[Salesperiod].CurrentMember.Level.Ordinal=1 ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "						"" Sum( { PeriodsToDate([Time].[Salesperiod].[(All)] , Ancestor([Time].[Salesperiod].CurrentMember , [Time].[Salesperiod].Salesperiod)  ) }  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "						"" ValidMeasure(( [Measures].[DpmMeasuredSum] , [Time].[Monthly].[All Time Monthly] , [Time].[Weekly].[All Time Weekly]) )   )  ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "									"" ValidMeasure(( [Measures].[DpmMeasuredSum]  , [Time].[Monthly].[All Time Monthly]  , [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod])) "" & _   " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"")   )   )             ) ) )			) )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure( [Measures].[DpmCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Measured Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strSemiadditiveStatement" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM BSel Measured Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DpmBSelMeasuredSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""ValidCrossjoinCount""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure( [Measures].[CrossjoinCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Coverage Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DpmCoverSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Coverage Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DpmCoverSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Distribution Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[DPM Measured Count]=0, NULL, [Measures].[Coverage Semiadd]/[Measures].[DPM Measured Count] )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Full Distribution Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""[Measures].[Coverage Semiadd]/[Measures].[ValidCrossjoinCount] """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'--------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Selection Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""SelectionSemiaddSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Selection Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SelectionSemiaddSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Base Selection Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""BaseSelectionSemiaddSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Base Selection Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[BaseSelectionSemiaddSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Coverage With Sel Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DpmSelCoverSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Coverage With Sel Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DpmSelCoverSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Cover With Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[DPM Measured Count]=0, NULL, [Measures].[Coverage With Sel Semiadd]/[Measures].[DPM Measured Count] )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Full Cover With Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[Selection Semiadd]=0, NULL, [Measures].[Coverage With Sel Semiadd]/[Measures].[Selection Semiadd] )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Coverage With Base Sel Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DpmBSelCoverSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Coverage With Base Sel Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DpmBSelCoverSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Cover With Base Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[DPM BSel Measured Count]=0, NULL, [Measures].[Coverage With Base Sel Semiadd]/[Measures].[DPM BSel Measured Count]) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Inv Cover With Base Sel Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""[Measures].[DPM BSel Measured Count]-[Measures].[Coverage With Base Sel Semiadd]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Inv Cover With Base Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF( [Measures].[Cover With Base Sel Semiadd %]=NULL , NULL , 1-[Measures].[Cover With Base Sel Semiadd %] )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Full Cover With Base Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[Base Selection Semiadd]=0, NULL, [Measures].[Coverage With Base Sel Semiadd]/[Measures].[Base Selection Semiadd]) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Suppl Cover With Base Sel Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""[Measures].[Coverage Semiadd]-[Measures].[Coverage With Base Sel Semiadd]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""5""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Suppl Cover With Base Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF( ([Measures].[DPM Measured Count]-[Measures].[DPM BSel Measured Count])=0 , NULL , [Measures].[Suppl Cover With Base Sel Semiadd]/([Measures].[DPM Measured Count]-[Measures].[DPM BSel Measured Count]) )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""ValidDelInDistrSemiaddSum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DelInDistrSemiaddSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""ValidDelInSelDistrSemiaddSum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DelInSelDistrSemiaddSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""ValidDelInBSelDistrSemiaddSum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DelInBSelDistrSemiaddSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Est Delivery Distribution Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""[Measures].[ValidDelInDistrSemiaddSum]/[Measures].[ValidCrossjoinCount]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Est Delivery Cover With Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[Selection Semiadd]=0, NULL, [Measures].[ValidDelInSelDistrSemiaddSum]/[Measures].[Selection Semiadd]) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Est Delivery Cover With Base Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[Base Selection Semiadd]=0, NULL, [Measures].[ValidDelInBSelDistrSemiaddSum]/[Measures].[Base Selection Semiadd] )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""ValidOrdInDistrSemiaddSum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""OrdInDistrSemiaddSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""ValidOrdInSelDistrSemiaddSum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""OrdInSelDistrSemiaddSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""ValidOrdInBSelDistrSemiaddSum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strVisible(n)=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""OrdInBSelDistrSemiaddSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ###""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Est Order Distribution Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""[Measures].[ValidOrdInDistrSemiaddSum]/[Measures].[ValidCrossjoinCount]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Est Order Coverage With Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[Selection Semiadd]=0, NULL, [Measures].[ValidOrdInSelDistrSemiaddSum]/[Measures].[Selection Semiadd] )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Est Order Coverage With Base Sel Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[Base Selection Semiadd]=0, NULL,[Measures].[ValidOrdInBSelDistrSemiaddSum]/[Measures].[Base Selection Semiadd] )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Listing Selection Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""[Measures].[Selection Semiadd]/[Measures].[ValidCrossjoinCount]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Listing Base Selection Semiadd %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Distribution and Coverage""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""[Measures].[Base Selection Semiadd]/[Measures].[ValidCrossjoinCount]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Average Stock Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Stock""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DpmAvgStockSum"" )" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Average Stock Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Stock""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DpmAvgStockSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Movement Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DpmMovementSum"" )" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Movement Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DpmMovementSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Facing Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DpmFacingSum"" )" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Facing Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DpmFacingSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Channel Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=Replace(strSemiadditiveStatement , ""DpmMeasuredSum"" , ""DpmChannelSum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Channel Time Diff""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DpmChannelSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Stock Turn Days Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Stock""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[Average Stock Semiadd]=0 , NULL , 12*[Measures].[DPM Movement Semiadd]/[Measures].[Average Stock Semiadd])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""5""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Stock Cover Days Semiadd""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Stock""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Measures].[Average Stock Semiadd]=0 , NULL , 360/[Measures].[Stock Turn Days Semiadd])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Consumer Price Net Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DpmPriceNetSum])/ValidMeasure([Measures].[DpmCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Consumer Price Net Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DpmPriceNetMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Consumer Price Net Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DpmPriceNetMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Consumer Price Gross Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DpmPriceGrossSum])/ValidMeasure([Measures].[DpmCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Consumer Price Gross Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DpmPriceGrossMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""DPM Consumer Price Gross Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DpmPriceGrossMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- PLANOGRAM  MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Facing Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnFacingSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Facing Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnFacingSum])/ValidMEasure([Measures].[PlnCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Facing Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnFacingMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Facing Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnFacingMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Channel Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnChannelSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Channel Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnChannelSum])/ValidMEasure([Measures].[PlnCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Channel Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnChannelMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Channel Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnChannelMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Cons Pkg Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnConsPkgSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Cons Pkg Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnConsPkgSum])/ValidMEasure([Measures].[PlnCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Cons Pkg Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnConsPkgMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Planogram Cons Pkg Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Facings""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMEasure([Measures].[PlnConsPkgMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- POS  MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PosCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Cons Pkg Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PosConsPkgSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Cons Pkg Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PosConsPkgSum])/ValidMeasure([Measures].[PosCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Cons Pkg Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PosConsPkgMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Cons Pkg Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PosConsPkgMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Money Net Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosMoneyNetSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Money Net Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosMoneyNetSum])/ValidMeasure([Measures].[PosCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Money Net Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosMoneyNetMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Money Net Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosMoneyNetMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Money Gross Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosMoneyGrossSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Money Gross Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosMoneyGrossSum])/ValidMeasure([Measures].[PosCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Money Gross Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosMoneyGrossMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Movement Money Gross Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosMoneyGrossMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Price Net Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosPriceNetSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Price Net Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosPriceNetSum])/ValidMeasure([Measures].[PosCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Price Net Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosPriceNetMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Price Net Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosPriceNetMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Price Gross Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosPriceGrossSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Price Gross Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosPriceGrossSum])/ValidMeasure([Measures].[PosCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Price Gross Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosPriceGrossMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Price Gross Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[PosPriceGrossMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Weight Net Sum"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n) " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)="""" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PosWeightNetSum])"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Weight Cpg Gross Sum"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n) " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)="""" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PosWeightCpgGrossSum])"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""POS Weight Case Gross Sum"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n) " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Movement"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)="""" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PosWeightCaseGrossSum])"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf	
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- DELIVERY  MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Store Distinct Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleStoreDistCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Purchase Price Net Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DelePriceNetSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Purchase Price Net Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DelePriceNetSum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Purchase Price Net Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DelePriceNetMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Purchase Price Net Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DelePriceNetMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Purchase Price Gross Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DelePriceGrossSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Purchase Price Gross Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DelePriceGrossSum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Purchase Price Gross Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DelePriceGrossMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Purchase Price Gross Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DelePriceGrossMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetarySum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetarySum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary (Prodprice) Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryProdPrSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary (Prodprice) Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryProdPrSum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary (Prodprice) Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryProdPrMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary (Prodprice) Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryProdPrMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary (Pricelist) Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryPriceListSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary (Pricelist) Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryPriceListSum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary (Pricelist) Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryPriceListMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Monetary (Pricelist) Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[DeleMonetaryPriceListMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Cons Pkg Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleConsPkgSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Cons Pkg Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleConsPkgSum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Cons Pkg Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleConsPkgMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Cons Pkg Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleConsPkgMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Case Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleCaseSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Case Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleCaseSum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Case Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleCaseMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Case Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleCaseMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Pallet Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DelePalletSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Pallet Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DelePalletSum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Pallet Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DelePalletMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Pallet Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DelePalletMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Unit Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleUnitSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Unit Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleUnitSum])/ValidMeasure([Measures].[DeleCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Unit Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleUnitMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Unit Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleUnitMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Weight Net Sum"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n) " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)="""" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleWeightNetSum])"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Weight Cpg Gross Sum"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n) " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)="""" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleWeightCpgGrossSum])"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Delivery Weight Case Gross Sum"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n) " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Deliveries"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)="""" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[DeleWeightCaseGrossSum])"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1 " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- ORDER  MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Cons Pkg Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdConsPkgSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Cons Pkg Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdConsPkgSum])/ValidMeasure([Measures].[OrdCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Cons Pkg Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdConsPkgMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Cons Pkg Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdConsPkgMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Case Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdCaseSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Case Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdCaseSum])/ValidMeasure([Measures].[OrdCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Case Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdCaseMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Case Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdCaseMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Pallet Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdPalletSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Pallet Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdPalletSum])/ValidMeasure([Measures].[OrdCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Pallet Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdPalletMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Pallet Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdPalletMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Unit Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdUnitSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Unit Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdUnitSum])/ValidMeasure([Measures].[OrdCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Unit Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdUnitMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Unit Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[OrdUnitMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Monetary Net Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[OrdMonetaryNoTaxSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Monetary Net Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[OrdMonetaryNoTaxSum])/ValidMeasure([Measures].[OrdCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Monetary Net Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[OrdMonetaryNoTaxMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Monetary Net Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[OrdMonetaryNoTaxMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Monetary Gross Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[OrdMonetaryTaxSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Monetary Gross Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[OrdMonetaryTaxSum])/ValidMeasure([Measures].[OrdCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Monetary Gross Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[OrdMonetaryTaxMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Order Monetary Gross Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Orders""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*ValidMeasure([Measures].[OrdMonetaryTaxMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- SURVEY  MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Survey Answer Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Surveys""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SurveyAnswerCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Survey Answer Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Surveys""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SurveyAnswerSum])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Survey Answer Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Surveys""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SurveyAnswerSum])/ValidMeasure([Measures].[SurveyAnswerCount])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Survey Answer Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Surveys""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SurveyAnswerMin])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Survey Answer Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Surveys""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[SurveyAnswerMax])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- MSA  MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Count""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[MsaCount]) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Money Net Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrMonetaryNetSum])  , ValidMeasure([Measures].[MsaMonetaryNetSum]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Money Net Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrMonetaryNetSum])/ValidMeasure([Measures].[MsaDistrCount])  , ValidMeasure([Measures].[MsaMonetaryNetSum])"
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "/ValidMeasure([Measures].[MsaCount])  )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Money Net Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrMonetaryNetMin])  , ValidMeasure([Measures].[MsaMonetaryNetMin]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Money Net Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrMonetaryNetMax])  , ValidMeasure([Measures].[MsaMonetaryNetMax]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Money Gross Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrMonetaryGrossSum])  , ValidMeasure([Measures].[MsaMonetaryGrossSum]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Money Gross Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrMonetaryGrossSum])/ValidMeasure([Measures].[MsaDistrCount])  , ValidMeasure([Measures].[MsaMonetaryGrossS"
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "um])/ValidMeasure([Measures].[MsaCount])  )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Money Gross Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrMonetaryGrossMin])  , ValidMeasure([Measures].[MsaMonetaryGrossMin]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA Money Gross Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*( IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrMonetaryGrossMax])  , ValidMeasure([Measures].[MsaMonetaryGrossMax]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA VAT Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrTaxSum])  , ValidMeasure([Measures].[MsaTaxSum]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA VAT Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrTaxSum])/ValidMeasure([Measures].[MsaDistrCount])  , ValidMeasure([Measures].[MsaTaxSum])/ValidMeasure([M"
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "easures].[MsaCount])  )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA VAT Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrTaxMin])  , ValidMeasure([Measures].[MsaTaxMin]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""MSA VAT Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""MSA""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[CurrencyRate])*(IIF ([MSA].[Date].CurrentMember IS [MSA].[Date].[MSA Date].&[V] , ValidMeasure([Measures].[MsaDistrTaxMax])  , ValidMeasure([Measures].[MsaTaxMax]) )  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------- TARGET MEASURES -----------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'----------------------constructing member statement ----------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement="" ValidMeasure([Measures].[TargProdGrpVolSum])+ValidMeasure([Measures].[TargProductVolSum]) """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	For Each dimen  in  dsoDB.MDStores(""VIRTUAL"").Dimensions" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Left(dimen.Name , 8)=""Product."" Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "				dim_name=Right(dimen.Name , len(dimen.Name)-8)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "				If dimen.Name<>""Product.Supplier"" and dimen.Name<>""Product.Supplier Builtin"" Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "					strTargetStatement= ""IIF( ["" & Replace(dimen.Name , ""."" , ""].["") & ""].CurrentMember.Level.Ordinal=1 ,ValidMeasure( (StrToMember(""""[SrcProductGroups].[Pgrpname].&["" & dim_name & ""].&["""" + "" & ""["" &  Replace(dimen.Name , ""."" , ""].["") & ""]"""
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & " & "".CurrentMember.Name + """"]"""") , [Measures].[TargProdGrpVolSum])  )+ ValidMeasure([Measures].[TargProductVolSum]),  ""  & strTargetStatement  & ""  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "					strTargetStatement=""IIF( ["" & Replace(dimen.Name , ""."" , ""].["") & ""].CurrentMember.Level.Ordinal>1 , ValidMeasure([Measures].[TargProductVolSum]),  ""  & strTargetStatement  & ""  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "				Else" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "					strTargetStatement=""IIF( ["" & Replace(dimen.Name , ""."" , ""].["") & ""].CurrentMember.Level.Ordinal>0 , ValidMeasure([Measures].[TargProductVolSum]),  ""  & strTargetStatement  & ""  )""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "				End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	Next" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=REPLACE(strTargetStatement , ""'"" , ""''"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'-------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Volume Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement , ""VolSum"" , ""MoneySum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Money Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement & ""*ValidMeasure([Measures].[CurrencyRate])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement ,  ""MoneySum"" , ""Num1Sum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Numeric1 Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement ,  ""Num1Sum"" , ""Num2Sum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Numeric2 Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement ,  ""Num2Sum"" , ""Num3Sum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Numeric3 Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement ,  ""Num3Sum"" , ""Num4Sum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Numeric4 Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement ,  ""Num4Sum"" , ""Money1Sum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Monetary1 Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement & ""*ValidMeasure([Measures].[CurrencyRate])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement ,  ""Money1Sum"" , ""Money2Sum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Monetary2 Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement & ""*ValidMeasure([Measures].[CurrencyRate])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement ,  ""Money2Sum"" , ""Money3Sum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Monetary3 Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement & ""*ValidMeasure([Measures].[CurrencyRate])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strTargetStatement=Replace(strTargetStatement ,  ""Money3Sum"" , ""Money4Sum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Target Monetary4 Sum""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Targets""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=strTargetStatement & ""*ValidMeasure([Measures].[CurrencyRate])""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'------------------------------------ PRICELIST MEASURES -------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Purchase Price Net Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" ( Sum( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PricePurchNetSum])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"")"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""/"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Sum("" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceCount])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") ) * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Purchase Price Gross Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" ( Sum( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PricePurchGrossSum])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"")"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""/"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Sum("" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceCount])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") ) * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Purchase Price Net Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""  MIN ( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PricePurchNetMin])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Purchase Price Gross Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""  MIN ( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PricePurchGrossMin])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Purchase Price Net Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""  MAX ( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PricePurchNetMax])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Purchase Price Gross Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""  MAX ( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PricePurchGrossMax])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Consumer Price Net Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" ( Sum( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceConsNetSum])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"")"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""/"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Sum("" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceCount])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") ) * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Consumer Price Gross Avg""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" ( Sum( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceConsGrossSum])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"")"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""/"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Sum("" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceCount])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") ) * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Consumer Price Net Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""  MIN ( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceConsNetMin])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Consumer Price Gross Min""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""  MIN ( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceConsGrossMin])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Consumer Price Net Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""  MAX ( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceConsNetMax])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Pricelist Consumer Price Gross Max""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Prices""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""  MAX ( "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""Filter([SrcPricelistSelrange].Members(0).Children ,   Not IsNull(ValidMeasure( [Measures].[PriceSelrangeCount]) ) ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			""ValidMeasure([Measures].[PriceConsGrossMax])"" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			"") * ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'------------------------------------ MARGIN + ROI MEASURES -------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Margin""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""Margin""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""ValidMeasure([Measures].[PriceMarginSum])*[Measures].[DPM Movement Semiadd]*ValidMeasure([Measures].[CurrencyRate]) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""ROI""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDescription(n)=strMember(n)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel1(n)=""ROI""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strLevel2(n)=""""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""12*[Measures].[Margin] "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'------------------------------------ TIME MEMBERS -------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "If True=True Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strNow=now()" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strYear=Year(strNow)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMonth=Month(strNow)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(strMonth)=1 Then strMonth=""0"" & strMonth" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMonth=strYear & strMonth" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strWeek=DatePart(""ww"", strNow , 2)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(strWeek)=1 Then strWeek=""0"" & strWeek" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strWeek=strYear & strWeek" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDay=Day(strNow)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(strDay)=1 Then strDay=""0"" & strDay" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strDay=strMonth & strDay	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonth_Now=DATEADD(""m"" , -1 , now() )" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonth_Year=Year(str_PrevMonth_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonth_Month=Month(str_PrevMonth_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(str_PrevMonth_Month)=1 Then str_PrevMonth_Month=""0"" & str_PrevMonth_Month" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonth_Month=str_PrevMonth_Year & str_PrevMonth_Month" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'str_PrevMonth_Day=Day(str_PrevMonth_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'If Len(str_PrevMonth_Day)=1 Then str_PrevMonth_Day=""0"" & str_PrevMonth_Day" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'str_PrevMonth_Day=str_PrevMonth_Month & str_PrevMonth_Day	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeek_Now=DATEADD(""ww"" , -1 , now() )" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeek_Year=Year(str_PrevWeek_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeek_Week=DATEPART(""ww"" , str_PrevWeek_Now , 2)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(str_PrevWeek_Week)=1 Then str_PrevWeek_Week=""0"" & str_PrevWeek_Week" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeek_Week=str_PrevWeek_Year & str_PrevWeek_Week" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'str_PrevWeek_Day=Day(str_PrevWeek_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'If Len(str_PrevWeek_Day)=1 Then str_PrevWeek_Day=""0"" & str_PrevWeek_Day" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'str_PrevWeek_Day=str_PrevWeek_Week & str_PrevWeek_Day	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevSalesperiod_Now=strDay" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	rst.Open ""select salenum from olap_date where date='"" & str_PrevSalesperiod_Now & ""' "" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		str_PrevSalesperiod_Salesperiod=rst.Fields(""salenum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	if str_PrevSalesperiod_Salesperiod=""000000"" then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Open ""select Isnull(max(date), '00000000') as prev_salesperiod_day from olap_date where salenum!='000000' and date<='"" & str_PrevSalesperiod_Now & ""' "" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			str_PrevSalesperiod_Day=rst.Fields(""prev_salesperiod_day"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	else" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Open ""select Isnull(max(date), '00000000') as prev_salesperiod_day from olap_date where date<(select min(date) from olap_date where salenum='"" & str_PrevSalesperiod_Salesperiod & ""'  ) "" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			str_PrevSalesperiod_Day=rst.Fields(""prev_salesperiod_day"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	end if" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	if str_PrevSalesperiod_Day=""00000000"" then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Open ""select min(date) as min_date from olap_date"" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			str_PrevSalesperiod_Day=rst.Fields(""min_date"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	end if" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	rst.Open ""select salenum from olap_date where date='"" & str_PrevSalesperiod_Day & ""'  "" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		str_PrevSalesperiod_Salesperiod=rst.Fields(""salenum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevSalesperiod_Year=Left(str_PrevSalesperiod_Day, 4)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevSalesperiod_Month=Left(str_PrevSalesperiod_Day , 6)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonthPrevYear_Now=DATEADD(""yyyy"" , -1 , now() )" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonthPrevYear_Now=DATEADD(""m"" , -1 , str_PrevMonthPrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonthPrevYear_Year=Year(str_PrevMonthPrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonthPrevYear_Month=Month(str_PrevMonthPrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(str_PrevMonthPrevYear_Month)=1 Then str_PrevMonthPrevYear_Month=""0"" & str_PrevMonthPrevYear_Month" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevMonthPrevYear_Month=str_PrevMonthPrevYear_Year & str_PrevMonthPrevYear_Month" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'str_PrevMonthPrevYear_Day=Day(str_PrevMonthPrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'If Len(str_PrevMonthPrevYear_Day)=1 Then str_PrevMonthPrevYear_Day=""0"" & str_PrevMonthPrevYear_Day" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'str_PrevMonthPrevYear_Day=str_PrevMonthPrevYear_Month & str_PrevMonthPrevYear_Day	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeekPrevYear_Now=DATEADD(""yyyy"" , -1 , now() )" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeekPrevYear_Now=DATEADD(""ww"" , -1 , str_PrevWeekPrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeekPrevYear_Year=Year(str_PrevWeekPrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeekPrevYear_Week=DATEPART(""ww"" , str_PrevWeekPrevYear_Now , 2)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(str_PrevWeekPrevYear_Week)=1 Then str_PrevWeekPrevYear_Week=""0"" & str_PrevWeekPrevYear_Week" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevWeekPrevYear_Week=str_PrevWeekPrevYear_Year & str_PrevWeekPrevYear_Week" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'str_PrevWeekPrevYear_Day=Day(str_PrevWeekPrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		'If Len(str_PrevWeekPrevYear_Day)=1 Then str_PrevWeekPrevYear_Day=""0"" & str_PrevWeekPrevYear_Day" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	'str_PrevWeekPrevYear_Day=str_PrevWeekPrevYear_Week & str_PrevWeekPrevYear_Day	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevYear_Now=DATEADD(""yyyy"" , -1 , now() )" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevYear_Year=Year(str_PrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevYear_Month=Month(str_PrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(str_PrevYear_Month)=1 Then str_PrevYear_Month=""0"" & str_PrevYear_Month" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevYear_Month=str_PrevYear_Year & str_PrevYear_Month" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevYear_Week=DATEPART(""ww"" , str_PrevYear_Now , 2)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(str_PrevYear_Week)=1 Then str_PrevYear_Week=""0"" & str_PrevYear_Week" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevYear_Week=str_PrevYear_Year & str_PrevYear_Week" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevYear_Day=Day(str_PrevYear_Now)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(str_PrevYear_Day)=1 Then str_PrevYear_Day=""0"" & str_PrevYear_Day" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevYear_Day=str_PrevYear_Month & str_PrevYear_Day	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevSalesperiodPrevYear_Now=str_PrevYear_Day" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	rst.Open ""select salenum from olap_date where date='"" & str_PrevSalesperiodPrevYear_Now & ""' "" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		str_PrevSalesperiodPrevYear_Salesperiod=rst.Fields(""salenum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	if str_PrevSalesperiodPrevYear_Salesperiod=""000000"" then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Open ""select Isnull(max(date), '00000000') as prev_salesperiod_day from olap_date where salenum!='000000' and date<='"" & str_PrevSalesperiodPrevYear_Now & ""' "" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			str_PrevSalesperiodPrevYear_Day=rst.Fields(""prev_salesperiod_day"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	else" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Open ""select Isnull(max(date), '00000000') as prev_salesperiod_day from olap_date where date<(select min(date) from olap_date where salenum='"" & str_PrevSalesperiodPrevYear_Salesperiod & ""'  ) "" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			str_PrevSalesperiodPrevYear_Day=rst.Fields(""prev_salesperiod_day"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	end if" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	if str_PrevSalesperiodPrevYear_Day=""00000000"" then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Open ""select min(date) as min_date from olap_date"" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			str_PrevSalesperiodPrevYear_Day=rst.Fields(""min_date"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	end if" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	rst.Open ""select salenum from olap_date where date='"" & str_PrevSalesperiodPrevYear_Day & ""'   "" , cnn" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		str_PrevSalesperiodPrevYear_Salesperiod=rst.Fields(""salenum"")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	rst.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevSalesperiodPrevYear_Year=Left(str_PrevSalesperiodPrevYear_Day, 4)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	str_PrevSalesperiodPrevYear_Month=Left(str_PrevSalesperiodPrevYear_Day , 6)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly Year-To-Date""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate( Except({ "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Month].&["" & strYear & ""01]:[Time].[Monthly].[Month].&["" & strMonth & ""] }, "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Date].&["" & strMonth & ""01]:[Time].[Monthly].[Date].&["" & strDay & ""]}  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  [Time].[Monthly].[Month].&["" & strMonth & ""]     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure((  [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly Year-To-DatePrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""Aggregate( Except({ "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Month].&["" & str_PrevYear_Year & ""01]:[Time].[Monthly].[Month].&["" & str_PrevYear_Month & ""] }, "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Date].&["" & str_PrevYear_Month & ""01]:[Time].[Monthly].[Date].&["" & str_PrevYear_Day & ""]}  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  [Time].[Monthly].[Month].&["" & str_PrevYear_Month & ""]     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure((  [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly Year-To-Date/Year-To-Date PrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Time].[Monthly].[All Time Monthly].[Monthly Year-To-DatePrevYear] =0 , NULL , [Time].[Monthly].[All Time Monthly].[Monthly Year-To-Date]/[Time].[Monthly].[All Time Monthly].[Monthly Year-To-DatePrevYear] ) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly Year-To-PrevMonth""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate(  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Month].&["" & str_PrevMonth_Year & ""01]:[Time].[Monthly].[Month].&["" & str_PrevMonth_Month & ""] }  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""  , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure(([Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly Year-To-PrevMonthPrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate(  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Month].&["" & str_PrevMonthPrevYear_Year & ""01]:[Time].[Monthly].[Month].&["" & str_PrevMonthPrevYear_Month & ""] }  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""  , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure(([Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly Year-To-PrevMonth/Year-To-PrevMonthPrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Time].[Monthly].[All Time Monthly].[Monthly Year-To-PrevMonthPrevYear] =0 , NULL , [Time].[Monthly].[All Time Monthly].[Monthly Year-To-PrevMonth]/[Time].[Monthly].[All Time Monthly].[Monthly Year-To-PrevMonthPrevYear] ) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly PrevMonth""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=	"" ValidMeasure(( [Time].[Monthly].[Month].&["" & str_PrevMonth_Month & ""]   , [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly PrevMonthPrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=	"" ValidMeasure(( [Time].[Monthly].[Month].&["" & str_PrevMonthPrevYear_Month & ""]   , [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Monthly].[All Time Monthly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Monthly PrevMonth/PrevMonthPrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Time].[Monthly].[All Time Monthly].[Monthly PrevMonthPrevYear]=0 , NULL , [Time].[Monthly].[All Time Monthly].[Monthly PrevMonth]/[Time].[Monthly].[All Time Monthly].[Monthly PrevMonthPrevYear] ) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly Year-To-Date""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate( Except({ "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Weekly].[Week].&["" & strYear & ""01]:[Time].[Weekly].[Week].&["" & strWeek & ""] }, "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Weekly].[Week].&["" & strWeek & ""].FirstChild:[Time].[Weekly].[Date].&["" & strDay & ""]}  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  [Time].[Weekly].[Week].&["" & strWeek & ""]     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure((  [Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly Year-To-DatePrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""Aggregate( Except({ "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Weekly].[Week].&["" & str_PrevYear_Year & ""01]:[Time].[Weekly].[Week].&["" & str_PrevYear_Week & ""] }, "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Weekly].[Week].&["" & str_PrevYear_Week & ""].FirstChild:[Time].[Weekly].[Date].&["" & str_PrevYear_Day & ""]}  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  [Time].[Weekly].[Week].&["" & str_PrevYear_Week & ""]     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure((  [Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]))    )   "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly Year-To-Date/Year-To-Date PrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Time].[Weekly].[All Time Weekly].[Weekly Year-To-DatePrevYear]=0 , NULL , [Time].[Weekly].[All Time Weekly].[Weekly Year-To-Date]/[Time].[Weekly].[All Time Weekly].[Weekly Year-To-DatePrevYear] )"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly Year-To-PrevWeek""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate(  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Weekly].[Week].&["" & str_PrevWeek_Year & ""01]:[Time].[Weekly].[Week].&["" & str_PrevWeek_Week & ""] }  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""  , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure(([Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly Year-To-PrevWeekPrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate(  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Weekly].[Week].&["" & str_PrevWeekPrevYear_Year & ""01]:[Time].[Weekly].[Week].&["" & str_PrevWeekPrevYear_Week & ""] }  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""  , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure(([Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly Year-To-PrevWeek/Year-To-PrevWeekPrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Time].[Weekly].[All Time Weekly].[Weekly Year-To-PrevWeekPrevYear]=0 , NULL , [Time].[Weekly].[All Time Weekly].[Weekly Year-To-PrevWeek]/[Time].[Weekly].[All Time Weekly].[Weekly Year-To-PrevWeekPrevYear] )"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly PrevWeek""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=	"" ValidMeasure(( [Time].[Weekly].[Week].&["" & str_PrevWeek_Week & ""]  , [Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]))  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly PrevWeekPrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=	"" ValidMeasure(( [Time].[Weekly].[Week].&["" & str_PrevWeekPrevYear_Week & ""]   , [Time].[Monthly].[All Time Monthly] , [Time].[Salesperiod].[All Salesperiod]))  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Weekly].[All Time Weekly]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Weekly PrevWeek/PrevWeekPrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Time].[Weekly].[All Time Weekly].[Weekly PrevWeekPrevYear]=0 , NULL , [Time].[Weekly].[All Time Weekly].[Weekly PrevWeek]/[Time].[Weekly].[All Time Weekly].[Weekly PrevWeekPrevYear] )"" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod Year-To-Date""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate( Except({ "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Month].&["" & strYear & ""01]:[Time].[Monthly].[Month].&["" & strMonth & ""] }, "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Date].&["" & strMonth & ""01]:[Time].[Monthly].[Date].&["" & strDay & ""]}  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  [Time].[Monthly].[Month].&["" & strMonth & ""]     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure((  [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod Year-To-DatePrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""Aggregate( Except({ "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Month].&["" & str_PrevYear_Year & ""01]:[Time].[Monthly].[Month].&["" & str_PrevYear_Month & ""] }, "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Date].&["" & str_PrevYear_Month & ""01]:[Time].[Monthly].[Date].&["" & str_PrevYear_Day & ""]}  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  [Time].[Monthly].[Month].&["" & str_PrevYear_Month & ""]     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure((  [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod Year-To-Date/Year-To-DatePrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Time].[Salesperiod].[All Salesperiod].[Salesperiod Year-To-DatePrevYear] =0 , NULL , [Time].[Salesperiod].[All Salesperiod].[Salesperiod Year-To-Date]/[Time].[Salesperiod].[All Salesperiod].[Salesperiod Year-To-DatePrevYear] ) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod Year-To-PrevSalesperiod""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate( Except({ "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Month].&["" & str_PrevSalesperiod_Year & ""01]:[Time].[Monthly].[Month].&["" & str_PrevSalesperiod_Month & ""] }, "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Date].&["" & str_PrevSalesperiod_Month & ""01]:[Time].[Monthly].[Date].&["" & str_PrevSalesperiod_Day & ""]}  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  [Time].[Monthly].[Month].&["" & str_PrevSalesperiod_Month & ""]     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure((  [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod Year-To-PrevSalesperiodPrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)="" Aggregate( Except({ "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Month].&["" & str_PrevSalesperiodPrevYear_Year & ""01]:[Time].[Monthly].[Month].&["" & str_PrevSalesperiodPrevYear_Month & ""] }, "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" { [Time].[Monthly].[Date].&["" & str_PrevSalesperiodPrevYear_Month & ""01]:[Time].[Monthly].[Date].&["" & str_PrevSalesperiodPrevYear_Day & ""]}  } ,  "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	""{  [Time].[Monthly].[Month].&["" & str_PrevSalesperiodPrevYear_Month & ""]     }  ) , "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	"" ValidMeasure((  [Time].[Weekly].[All Time Weekly] , [Time].[Salesperiod].[All Salesperiod]))    )  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod Year-To-PrevSalesperiod/Year-To-PrevSalesperiodPrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF([Time].[Salesperiod].[All Salesperiod].[Salesperiod Year-To-PrevSalesperiodPrevYear] =0 , NULL , [Time].[Salesperiod].[All Salesperiod].[Salesperiod Year-To-PrevSalesperiod]/[Time].[Salesperiod].[All Salesperiod].[Salesperiod Year-To-PrevSal"
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "esperiodPrevYear] ) "" " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod PrevSalesperiod""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=	"" ValidMeasure(([Time].[Salesperiod].[Salesperiod].&["" & str_PrevSalesperiod_Salesperiod & ""]   , [Time].[Weekly].[All Time Weekly] , [Time].[Monthly].[All Time Monthly]))  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod PrevSalesperiodPrevYear""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=	"" ValidMeasure(( [Time].[Salesperiod].[Salesperiod].&["" & str_PrevSalesperiodPrevYear_Salesperiod & ""]    , [Time].[Weekly].[All Time Weekly] , [Time].[Monthly].[All Time Monthly]))  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""### ### ### ### ##0.00""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""-10""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strParentDimension(n)=""[Time].[Salesperiod].[All Salesperiod]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strMember(n)=""Salesperiod PrevSalesperiod/PrevSalesperiodPrevYear %""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strStatement(n)=""IIF( [Time].[Salesperiod].[All Salesperiod].[Salesperiod PrevSalesperiodPrevYear] =0 , NULL , [Time].[Salesperiod].[All Salesperiod].[Salesperiod PrevSalesperiod]/[Time].[Salesperiod].[All Salesperiod].[Salesperiod PrevSalesperiodPrevYear] )  """
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & " " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strFormat(n)=""Percent""	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	strSolveOrder(n)=""20""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	n=n+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'----------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'------------------ ADDING CALC MEMS TO  VIRTUAL -------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "i=0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Do While Len(Cstr(strMember(i)))>0" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If dsoDB.MDStores(""VIRTUAL"").Commands.Find(strMember(i)) Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			Set dsoCalculatedMember = dsoDB.MDStores(""VIRTUAL"").Commands(strMember(i))" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		Else" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			Set dsoCalculatedMember = dsoDB.MDStores(""VIRTUAL"").Commands.AddNew(strMember(i))" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		dsoCalculatedMember.CommandType = 1 'cmdCreateMember" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(strVisible(i))=0 Then " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			strVisible(i)=""1""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		ElseIf strVisible(i)=0 Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			strVisible(i)=""0""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If Len(strParentDimension(i))=0 Then " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			strParentDimension(i)=""[Measures]""" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		dsoCalculatedMember.Statement = ""CREATE MEMBER [VIRTUAL]."" & strParentDimension(i) & "".["" & strMember(i) & ""] AS '"" & strStatement(i) & ""' "" & _" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "					       "" , SOLVE_ORDER="" & strSolveOrder(i) & "" ,  FORMAT_STRING='"" & strFormat(i) & ""'  , VISIBLE='"" & strVisible(i) & ""'  """ & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "							       " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		i=i+1" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "										       " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Loop" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'--------------------------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'------------------ HIDE HIERARCHIES  IN  VIRTUAL -------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "arrHideDimensions=Split(strHideDimensions , "","")" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "For i = dsoDB.MDStores(""VIRTUAL"").Dimensions.Count To 1 Step -1   " & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	For j=0 To Ubound(arrHideDimensions)" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		If TRIM(arrHideDimensions(j))=dsoDB.MDStores(""VIRTUAL"").Dimensions(i).Name Then" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "			dsoDB.MDStores(""VIRTUAL"").Dimensions(i).IsVisible=False" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "		End If" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	Next" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Next" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'------------------ UPDATE VIRTUAL -------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "dsoDB.MDStores(""VIRTUAL"").Update" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'--------------------------------------------------------" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'*********************************************************************************************************************************************************************************************" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'*********************************************************************************************************************************************************************************************" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "'*********************************************************************************************************************************************************************************************" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "cnn.Close" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set cnn = Nothing" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set rst = Nothing" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set dsoServer = Nothing" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set dsoDB = Nothing" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set dsoDS = Nothing" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "Set strcat = Nothing" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "	Main = DTSTaskExecResult_Success" & vbCrLf
	oCustomTask1.ActiveXScript = oCustomTask1.ActiveXScript & "End Function"
	oCustomTask1.FunctionName = "Main"
	oCustomTask1.ScriptLanguage = "VBScript"
	oCustomTask1.AddGlobalVariables = True
	
goPackage.Tasks.Add oTask
Set oCustomTask1 = Nothing
Set oTask = Nothing

End Sub

'------------- define Task_Sub2 for task DTSTask_DTSActiveScriptTask_2 (REBUILD CUBES + REPAIR REPORTS)
Public Sub Task_Sub2(ByVal goPackage)

Set oTask = goPackage.Tasks.New("DTSActiveScriptTask")
Set oCustomTask2 = oTask.CustomTask

	oCustomTask2.Name = "DTSTask_DTSActiveScriptTask_2"
	oCustomTask2.Description = "REBUILD CUBES + REPAIR REPORTS"
	oCustomTask2.ActiveXScript = "'**********************************************************************" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'  Visual Basic ActiveX Script" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'************************************************************************" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'##########################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strOLAP_SERVER=""" & strOlapServer & """" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strOLAP_DB=""DBSALESPP""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strOLAP_CUBE=""VIRTUAL""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strSALESPP_DSN=""DBSALESPP""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strSALESPP_UID=""spp""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strSALESPP_PWD=""" & strSppPass & """" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strSalesppIniPath=""" & strSppIniPath & """" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'##########################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'#########   enums   ##########" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "processDefault=0" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "processFull=1" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "processRefreshData=2" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Function Main()" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set dsoServer = CreateObject(""DSO.Server"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "dsoServer.Timeout=14400	 ' 4 hours for each object process" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "dsoServer.Connect strOLAP_SERVER" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "    " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set dsoDB = dsoServer.MDStores(strOLAP_DB)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set dsoDS = dsoDB.DataSources(1)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set cnn  = CreateObject(""ADODB.Connection"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set rst = CreateObject(""ADODB.Recordset"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.CursorType=1		'adOpenKeyset" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.LockType=3		'adLockOptimistic" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "cnn.ConnectionTimeout=0" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "cnn.CommandTimeout=0" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "cnn.Open ""DSN="" & strSALESPP_DSN & "";User ID="" & strSALESPP_UID & "";Password="" & strSALESPP_PWD & "";"" " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'##################################### GETTING SALCTYPE FROM SALESPP.INI ######################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Open ""  SELECT  [KEY] , ATTR_VAL  FROM OLAP_SALESCALL_HIERARCHY WHERE ATTR='SALCTYPE'  "" , cnn " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "IF rst.EOF=True And rst.BOF=True Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	rst.Close" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Else" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Do Until rst.EOF" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		strSalctypeKey= rst.Fields(""KEY"") " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If Not(IsNull(strSalctypeKey)) and Not(IsEmpty(strSalctypeKey)) and IsNumeric(strSalctypeKey) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			strSalctypeKey=Cstr(CInt(strSalctypeKey)+1)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		strSalctypeVal=ReadIni( strSalesppIniPath, ""SalesCall_Type"", strSalctypeKey )" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If strSalctypeVal=""Not Found"" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			rst.Fields(""ATTR_VAL"")=""Sales Call Type - "" & strSalctypeKey & "" Not Found""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		Else" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			rst.Fields(""ATTR_VAL"")=""Sales Call Type - "" & strSalctypeVal" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		rst.MoveNext" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Loop" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	rst.UpdateBatch" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	rst.Close" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'##################################### GETTING CURRENCY FROM SALESPP.INI ######################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strDEFAULT=ReadIni( strSalesppIniPath, ""Defaults"", ""SaveEuro"" ) " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "If Left(strDEFAULT,9)=""Not Found"" or Left(strDEFAULT,1)=""0"" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	strDEFAULT=ReadIni( strSalesppIniPath, ""Defaults"", ""Curr"" ) " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	If strDEFAULT=""Not Found"" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		strDEFAULT=""N/A""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Else" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	strDEFAULT=""EURO""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Open ""DELETE FROM OLAP_CURRENCY"" , cnn " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Open ""INSERT INTO OLAP_CURRENCY(CURRENCY , RATE_TO_EURO , RATE_TO_DEFAULT ,  [DEFAULT] ) "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'EURO'  AS CURRENCY, 1  AS RATE_TO_EURO ,  1  AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'FIM'  AS CURRENCY, 5.94573  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'BEF'  AS CURRENCY, 40.3399  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'LUF'  AS CURRENCY,40.3399  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'DEM'  AS CURRENCY, 1.95583  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'ESP'  AS CURRENCY, 166.386  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'FRF'  AS CURRENCY, 6.55957  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'IEP'  AS CURRENCY, 0.787564  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'ITL'  AS CURRENCY, 1936.27  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'NLG'  AS CURRENCY, 2.20371  AS RATE_TO_EURO , 1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'ATS'  AS CURRENCY, 13.7603  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" UNION "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT 'PTE'  AS CURRENCY, 200.482  AS RATE_TO_EURO ,  1 AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] "" , cnn" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.open  ""IF  NOT EXISTS(SELECT * FROM OLAP_CURRENCY WHERE CURRENCY= '"" & strDEFAULT & ""') "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		""INSERT INTO OLAP_CURRENCY(CURRENCY , RATE_TO_EURO , RATE_TO_DEFAULT ,  [DEFAULT] ) "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		"" SELECT '"" & strDEFAULT & ""'  AS CURRENCY, 0  AS RATE_TO_EURO ,  1  AS RATE_TO_DEFAULT , '"" & strDEFAULT & ""' AS [DEFAULT] ""  , cnn" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Open ""UPDATE OLAP_CURRENCY SET RATE_TO_DEFAULT=( CASE DEFAULT_CURRENCY.RATE_TO_EURO WHEN 0 THEN 0 ELSE OLAP_CURRENCY.RATE_TO_EURO/DEFAULT_CURRENCY.RATE_TO_EURO END ) "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	"" FROM OLAP_CURRENCY , (SELECT * FROM OLAP_CURRENCY WHERE CURRENCY=[DEFAULT]) DEFAULT_CURRENCY ""  & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	"" WHERE OLAP_CURRENCY.CURRENCY!=DEFAULT_CURRENCY.CURRENCY  ""  , cnn" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'################################################################################################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'##################################### GETTING SALUNCALL FROM SALESPP.INI ######################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strVALIDCALL=ReadIni( strSalesppIniPath, ""FieldInformer"", ""VALIDCALL"" ) " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "On Error Resume Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Open ""EXEC proc_create_SALESCALL_VIEW ' "" &  Replace(strVALIDCALL , ""'"" , ""''"") & "" '  "" , cnn " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "On Error Goto 0" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'################################################################################################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'----------------------------- DIMENSIONS TO ADD TO REPORTS -------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Dim arrDimsToAdd()" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "countDimsToAdd=0" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Open ""select DISTINCT DIM_TYPE + '.' + DIM_NAME as DIM_NAME  from OLAP_UPD_DIM where UPD_FLAG='I'  "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" and "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" ( "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" (OLAP_UPD_DIM.DIM_TYPE='Store' and exists(SELECT * FROM  v_select_store_groups WHERE v_select_store_groups.STORE_GROUP_NAME=OLAP_UPD_DIM.DIM_NAME) ) "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" or "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" (OLAP_UPD_DIM.DIM_TYPE='Product' and exists(SELECT * FROM  v_select_product_groups WHERE v_select_product_groups.PRODUCT_GROUP_NAME=OLAP_UPD_DIM.DIM_NAME) ) "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" ) "" ,cnn" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "If rst.EOF=True and rst.BOF=True then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	blnAdd=0" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Else" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	blnAdd=1" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	ReDim arrDimsToAdd(rst.RecordCount-1)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Do Until rst.EOF" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		arrDimsToAdd(countDimsToAdd)=rst.Fields(""DIM_NAME"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		countDimsToAdd=countDimsToAdd+1" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		rst.MoveNext" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Loop" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strDimsToAdd=""$~$"" & Join(arrDimsToAdd , ""$~$"" ) & ""$~$""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Close" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'-----------------------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'----------------------------- DIMENSIONS TO REMOVE FROM REPORTS -------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Dim arrDimsToRemove()" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "countDimsToRemove=0" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Open ""select DISTINCT DIM_TYPE + '.' + DIM_NAME as DIM_NAME  from OLAP_UPD_DIM where UPD_FLAG='D'  "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" and "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" ( "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" (OLAP_UPD_DIM.DIM_TYPE='Store' and NOT exists(SELECT * FROM  v_select_store_groups WHERE v_select_store_groups.STORE_GROUP_NAME=OLAP_UPD_DIM.DIM_NAME) ) "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" or "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" (OLAP_UPD_DIM.DIM_TYPE='Product' and NOT exists(SELECT * FROM  v_select_product_groups WHERE v_select_product_groups.PRODUCT_GROUP_NAME=OLAP_UPD_DIM.DIM_NAME) ) "" & _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					"" ) "" ,cnn" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "If rst.EOF=True and rst.BOF=True then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	blnRemove=0" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Else" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	blnRemove=1" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	ReDim arrDimsToRemove(rst.RecordCount-1)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Do Until rst.EOF" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		arrDimsToRemove(countDimsToRemove)=rst.Fields(""DIM_NAME"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		countDimsToRemove=countDimsToRemove+1" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		rst.MoveNext" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Loop" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strDimsToRemove=""$~$"" & Join(arrDimsToRemove , ""$~$"" ) & ""$~$""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Close" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'-----------------------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'#################### REMOVING FROM CUBES ######################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "If blnRemove=1 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "For n=0 to ubound(arrDimsToRemove)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "             strDim =arrDimsToRemove(n)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "            If dsoDB.Dimensions.Find(strDim) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'--------- remove shared dims from virtual cubes --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				  	If   dsoDB.MDStores(i).SubClassType=1 and  dsoDB.MDStores(i).Dimensions.Find(strDim) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						dsoDB.MDStores(i).Dimensions.Remove (strDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					end if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'--------- remove shared dims from normal cubes --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				  	If   dsoDB.MDStores(i).SubClassType=0 and  dsoDB.MDStores(i).Dimensions.Find(strDim) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						dsoDB.MDStores(i).Dimensions.Remove (strDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					end if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'--------- remove shared dims from DB --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				dsoDB.Dimensions.Remove (strDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				dsoDB.Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "            End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	      If Left(strDim,7)=""Product"" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		'---------------- remove member properties ------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.Dimensions.Item(""SrcProduct"").Levels(""Product"").MemberProperties.Find(Replace(strDim, ""."" , "" "")) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.Dimensions.Item(""SrcProduct"").Levels(""Product"").MemberProperties.Remove(Replace(strDim, ""."" , "" "") )" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.Dimensions.Item(""SrcProduct"").Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	      ElseIf Left(strDim,13)=""Store.COMTEXT"" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		'---------------- remove member properties ------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.Dimensions.Item(""SrcStore"").Levels(""Store"").MemberProperties.Find(Replace(strDim, ""."" , "" "")) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.Dimensions.Item(""SrcStore"").Levels(""Store"").MemberProperties.Remove(Replace(strDim, ""."" , "" ""))" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.Dimensions.Item(""SrcStore"").Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	     End If		" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & " " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'--------- place Expand Product Groups as last dimension in virtual cubes --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "    	 If dsoDB.MDStores(i).SubClassType=1 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.MDStores(i).Dimensions.Find(""Expand Compound Products"") Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "       			dsoDB.MDStores(i).Dimensions.Remove(""Expand Compound Products"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		dsoDB.MDStores(i).Dimensions.AddNew(""Expand Compound Products"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		'dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & " 	end if	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'-----------------------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	 dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "dsoDB.Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'#################### ADDING TO CUBES ######################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "If blnAdd=1 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "For n=0 to ubound(arrDimsToAdd)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "             strNewDim =arrDimsToAdd(n)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "            If dsoDB.Dimensions.Find(strNewDim) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'--------- remove shared dims from virtual cubes --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				  	If   dsoDB.MDStores(i).SubClassType=1 and  dsoDB.MDStores(i).Dimensions.Find(strNewDim) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						dsoDB.MDStores(i).Dimensions.Remove (strNewDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					end if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'--------- remove shared dims from normal cubes --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				  	If   dsoDB.MDStores(i).SubClassType=0 and  dsoDB.MDStores(i).Dimensions.Find(strNewDim) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						dsoDB.MDStores(i).Dimensions.Remove (strNewDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					end if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'--------- remove shared dims from DB --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				dsoDB.Dimensions.Remove (strNewDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				dsoDB.Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "            End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	If Left(strNewDim,7)=""Product"" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		'################################### member properties ###################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.Dimensions.Item(""SrcProduct"").Levels(""Product"").MemberProperties.Find(Replace(strNewDim, ""."" , "" "")) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.Dimensions.Item(""SrcProduct"").Levels(""Product"").MemberProperties.Remove(Replace(strNewDim, ""."" , "" "") )" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.Dimensions.Item(""SrcProduct"").Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		Set new_prop=dsoDB.Dimensions.Item(""SrcProduct"").Levels(""Product"").MemberProperties.AddNew(Replace(strNewDim, ""."" , "" ""))" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		new_prop.ColumnType=129   'adChar" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		new_prop.ColumnSize=30" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		new_prop.SourceColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_PRODUCT"" & Chr(34) & ""."" & Chr(34) & ""GRP@#@"" & Mid(strNewDim , 9) & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		dsoDB.Dimensions.Item(""SrcProduct"").Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		'#######################################################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoDim = dsoDB.Dimensions.AddNew(strNewDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoDim.DataSource = dsoDS   'Dimension DataSource" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                '----------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					dsoDim.DependsOnDimension=""SrcProduct""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					dsoDim.IsVirtual=True" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoDim.AllowSiblingsWithSameName = True" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                'dsoDim.IsChanging = True" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                '----------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                'dsoDim.FromClause = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_PRODUCT"" & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                'dsoDim.JoinClause = """"" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	        " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoLev = dsoDim.Levels.AddNew(""(All)"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.LevelType = 1 'levAll" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberKeyColumn = ""All "" & Replace(strNewDim, ""."" , "" "")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	        " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoLev = dsoDim.Levels.AddNew(Mid(strNewDim , 9))" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberKeyColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_PRODUCT"" & Chr(34) & ""."" & Chr(34) & ""GRP@#@"" & Mid(strNewDim , 9) & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberNameColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_PRODUCT"" & Chr(34) & ""."" & Chr(34) & ""GRP@#@"" & Mid(strNewDim , 9) & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.ColumnSize = 30" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.ColumnType = 129  'adChar" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.EstimatedSize = 50" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	        " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoLev = dsoDim.Levels.AddNew(""Product"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberKeyColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_PRODUCT"" & Chr(34) & ""."" & Chr(34) & ""PRODSERN"" & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberNameColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_PRODUCT"" & Chr(34) & ""."" & Chr(34) & ""PRODNAME"" & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.ColumnSize = 15" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.ColumnType =  129  'adChar" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.EstimatedSize = 500" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                '----------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.AreMemberKeysUnique = True" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                '----------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoDim.Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoDim.Process processFull" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "         " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	ElseIf Left(strNewDim,13)=""Store.COMTEXT"" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		'################################### member properties ###################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.Dimensions.Item(""SrcStore"").Levels(""Store"").MemberProperties.Find(Replace(strNewDim, ""."" , "" "")) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.Dimensions.Item(""SrcStore"").Levels(""Store"").MemberProperties.Remove(Replace(strNewDim, ""."" , "" ""))" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.Dimensions.Item(""SrcStore"").Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		Set new_prop=dsoDB.Dimensions.Item(""SrcStore"").Levels(""Store"").MemberProperties.AddNew(Replace(strNewDim, ""."" , "" ""))" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		new_prop.ColumnType=129   'adChar" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		new_prop.ColumnSize=30" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		new_prop.SourceColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_STORE"" & Chr(34) & ""."" & Chr(34) & ""STOREGRP@#@"" & Mid(strNewDim ,  7) & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		dsoDB.Dimensions.Item(""SrcStore"").Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		'#######################################################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoDim = dsoDB.Dimensions.AddNew(strNewDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoDim.DataSource = dsoDS   'Dimension DataSource" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                '----------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					dsoDim.DependsOnDimension=""SrcStore""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					dsoDim.IsVirtual=True" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoDim.AllowSiblingsWithSameName = True" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                'dsoDim.IsChanging = True" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                '----------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	               ' dsoDim.FromClause = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_STORE"" & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                'dsoDim.JoinClause = """"" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	        " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoLev = dsoDim.Levels.AddNew(""(All)"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.LevelType = 1 'levAll" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberKeyColumn = ""All "" & Replace(strNewDim, ""."" , "" "")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	        " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoLev = dsoDim.Levels.AddNew(Mid(strNewDim ,  7))" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberKeyColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_STORE"" & Chr(34) & ""."" & Chr(34) & ""STOREGRP@#@"" & Mid(strNewDim ,  7) & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberNameColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_STORE"" & Chr(34) & ""."" & Chr(34) & ""STOREGRP@#@"" & Mid(strNewDim , 7) & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.ColumnSize = 30" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.ColumnType = 129  'adChar" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.EstimatedSize = 50" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	        " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                Set dsoLev = dsoDim.Levels.AddNew(""Store"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberKeyColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_STORE"" & Chr(34) & ""."" & Chr(34) & ""COMSERNO"" & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.MemberNameColumn = Chr(34) & ""spp"" & Chr(34) & ""."" & Chr(34) & ""OLAP_STORE"" & Chr(34) & ""."" & Chr(34) & ""COMNAME"" & Chr(34)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.ColumnSize = 15" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.ColumnType =  129  'adChar" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.EstimatedSize = 10000" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                '----------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoLev.AreMemberKeysUnique = True" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                '----------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoDim.Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	                dsoDim.Process processFull" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'--------- add shared dims to normal cubes --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "    	If dsoDB.MDStores(i).SubClassType=0  Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					if Left(strNewDim,7)=""Product"" and  dsoDB.MDStores(i).Dimensions.Find(""SrcProduct"") Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "										" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "       					 		dsoDB.MDStores(i).Dimensions.AddNew (strNewDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "								'dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "								" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					Elseif Left(strNewDim,13)=""Store.COMTEXT""  and  dsoDB.MDStores(i).Dimensions.Find(""SrcStore"")  Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "											" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						 		dsoDB.MDStores(i).Dimensions.AddNew (strNewDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "								'dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "							" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					End  if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & " 		End if	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & " 		" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'--------- add shared dims to virtual cubes --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "    		 If dsoDB.MDStores(i).SubClassType=1 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "       			 dsoDB.MDStores(i).Dimensions.AddNew (strNewDim)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				 'dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & " 		end if	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & " " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'--------- place Expand Product Groups as last dimension in virtual cubes --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "    	 If dsoDB.MDStores(i).SubClassType=1 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.MDStores(i).Dimensions.Find(""Expand Compound Products"") Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "       			dsoDB.MDStores(i).Dimensions.Remove(""Expand Compound Products"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		dsoDB.MDStores(i).Dimensions.AddNew(""Expand Compound Products"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		'dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & " 	end if	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'-----------------------------------------------------------------------------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	 dsoDB.MDStores(i).Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "dsoDB.Update" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'######################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'--------- process shared dims  --------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "For j = dsoDB.Dimensions.Count To 1 Step -1   " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	If Instr(dsoDB.Dimensions(j).Name , ""^"")=0 then dsoDB.Dimensions(j).Process processFull" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'------------------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "IF (blnRemove=1 Or blnAdd=1)  Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'--------- process normal cubes ----------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.MDStores(i).SubClassType=0 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			dsoDB.MDStores(i).Process processFull" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'-------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'--------- process virtual cubes ----------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.MDStores(i).SubClassType=1 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		 dsoDB.MDStores(i).Process processDefault" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Else" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'--------- process normal cubes ----------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.MDStores(i).SubClassType=0 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			If (	dsoDB.MDStores(i).Name=""BASE_SELECTION_SEMIADD"" or _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						dsoDB.MDStores(i).Name=""SELECTION_SEMIADD"" or _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						dsoDB.MDStores(i).Name=""PRICELIST"" or _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "						dsoDB.MDStores(i).Name=""PRICELIST_SELRANGE"" _" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				'reprocess these cubes only on weekend (or if not processed)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				If DatePart(""w"", now() ,2)=6 or dsoDB.MDStores(i).State=0 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "					dsoDB.MDStores(i).Process processFull" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				End If	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			Else" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "				dsoDB.MDStores(i).Process processFull" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "			End If	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'-------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'--------- process virtual cubes ----------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	For i = 1 To dsoDB.MDStores.Count" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		If dsoDB.MDStores(i).SubClassType=1 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		 dsoDB.MDStores(i).Process processDefault" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "		End If " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	next" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	'------------------------------------------" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End if" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'##############- remove  information  from OLAP_UPD_DIM ##############" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "cnn.Close" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "cnn.Open ""DSN="" & strSALESPP_DSN & "";User ID="" & strSALESPP_UID & "";Password="" & strSALESPP_PWD & "";"" " & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "rst.Open ""delete from OLAP_UPD_DIM"" , cnn" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "cnn.Close" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set cnn = Nothing" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set rst = Nothing" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "dsoServer.CloseServer ' Close the connection to the Analysis server" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set dsoServer = Nothing" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set dsoDB = Nothing" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set dsoDS = Nothing" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set strcat = Nothing" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	Main = DTSTaskExecResult_Success" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End Function" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "'########################################################" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Function ReadIni( file, section, item )" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "ReadIni = """"" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "strResult=""""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "Set FileSysObj = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "If FileSysObj.FileExists( file ) Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "  Set ini = FileSysObj.OpenTextFile( file, 1, False)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "  Do While ini.AtEndOfStream = False" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "    line = Trim(ini.ReadLine)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "   If line = ""["" & section & ""]"" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "    line = Trim(ini.ReadLine)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "        Do While Left( line, 1) <> ""[""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "          If InStr( line, item & ""="" ) = 1 Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "            strResult = mid( line, Len( item ) + 2 )" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "            Exit Do" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "          End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "          If ini.AtEndOfStream Then Exit Do" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "            line = Trim(ini.ReadLine)" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "        Loop" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "      Exit Do" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "    End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "   Loop" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "  ini.Close" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "If strResult="""" Then" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "	strResult=""Not Found""" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End If" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "ReadIni=strResult" & vbCrLf
	oCustomTask2.ActiveXScript = oCustomTask2.ActiveXScript & "End Function" & vbCrLf
	oCustomTask2.FunctionName = "Main"
	oCustomTask2.ScriptLanguage = "VBScript"
	oCustomTask2.AddGlobalVariables = True
	
goPackage.Tasks.Add oTask
Set oCustomTask2 = Nothing
Set oTask = Nothing

End Sub

