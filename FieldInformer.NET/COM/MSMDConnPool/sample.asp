<% @LANGUAGE="VBSCRIPT" %>
<% Option Explicit %>
<%
'
' This sample just executes a trivial query.
'
%>


<HTML>
<HEAD> 
<TITLE>Simple sample.</TITLE>
</HEAD>


<% 

    Dim szConnectionString
    Dim szQuery

	response.write "<Center>"
	response.write Now()
	response.write " </Center>" & vbcrlf

	response.write "<Center>"
	response.write "Number of active sessions = " & Application("m_cSessions")
	response.write " </Center>" & vbcrlf

    ' this is the connection string: replace localhost with your server name 
    ' if the OLAP server is running on a different machine
    szConnectionString = "provider=msolap;data source=localhost;Initial Catalog=DBSALESPP_MASTERFOODS"
    szQuery = "WITH SET colset0 AS '{[Time].[Weekly].[All Time Weekly]}' SET colset01_with_calc AS '{[Time].[Weekly].[All Time Weekly]}' SET colset1 AS '{[Currency].[All Currency]}' SET colset1_with_calc AS '{[Currency].[All Currency]}' SET rowset0 AS '{[Target].[All Target]}' SET rowset0_with_calc AS '{[Target].[All Target]}' SET rowset1 AS '{[Time].[Monthly].[All Time Monthly]}' SET rowset1_with_calc AS '{[Time].[Monthly].[All Time Monthly]}' SELECT NON EMPTY {{rowset0_with_calc }*{rowset1_with_calc }} ON ROWS , NON EMPTY {{colset01_with_calc }*{colset1_with_calc }} ON COLUMNS from VIRTUAL WHERE ([Expand Compound Products].[All Expand Compound Products],[Measures].[Sales Call Count],[MSA].[Attributes].[All MSA Attributes],[MSA].[Date].[All MSA Date],[Order].[Attributes].[All Order Attributes],[Order].[Date].[All Order Date],[Planogram Fixture].[All Planogram Fixture],[Pricelist].[All Pricelist],[Product].[BISBrand].[All Product.BISBrand],[Product].[Brand].[All Product.Brand],[Sales Call].[All Sales Call],[Salesforce].[All Salesforce],[Store].[Call Address].[All Call Address],[Store].[Central Chain].[All Central Chain],[Store].[Chain].[All Chain],[Store].[Class].[All Store Class],[Store].[Postal Code].[All Store Postal Code],[Store].[Salesforce].[All Salesforce],[Store].[Type].[All Store Type],[Survey Question].[All Survey Question],[Time].[Salesperiod].[All Salesperiod],[Wholesaler].[All Wholesaler]) "

    execute_and_show_query szConnectionString, szQuery

%>


<!-- Subroutines -->
<%

Sub execute_and_show_query(szConnectionString, szQuery)
    Dim con
    Dim cst
	Dim txtOut
	Dim iCell
	Dim cCell
	Dim m_ConnPool

	set con = nothing
	set cst = nothing

	response.write "Inside execute_and_show_query "

	' -- old way
    'Set con = Server.CreateObject("ADODB.Connection")
    'con.Open szConnectionString

	' -- new way
    set m_ConnPool = Application.StaticObjects("m_ConnPool")
    set con = m_ConnPool.GetConnectionFromPool(szConnectionString)	

	set cst = server.CreateObject("ADOMD.CellSet")   
	
    set cst.ActiveConnection = con

    cst.Open szQuery
    
    txtOut = Now & vbCrLf
	response.write txtOut
    ' Stupid code -- just print a portion of the data.
    ' It is also stupid because it will keep expanding the bstr.
    
    cCell = cst.Axes(0).Positions.Count
    For iCell = 0 To cCell - 1
        txtOut = cst.Axes(0).Positions(iCell).Members(0).Caption & " "
        txtOut = txtOut & cst.Item(iCell).FormattedValue & vbCrLf
        response.write txtOut
    Next

    m_ConnPool.ReturnConnectionToPool con

End Sub
    
%>


</html>
