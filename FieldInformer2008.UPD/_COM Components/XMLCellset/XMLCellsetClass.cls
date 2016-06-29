VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "XMLCellsetClass"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit


'Public m_connPool As New MSXmlAnalysisSCLib.ADOConPool

Private conn As ADODB.Connection
Private m_connPool As New MSMDConnPool.ConnPool


Public Sub SetConnection(Connection As ADODB.Connection)
    Set conn = Connection
End Sub



Public Sub SetConnectionFromPool(ConnectionString As String)
    Set conn = m_connPool.GetConnectionFromPool(ConnectionString)
End Sub


Public Sub ReturnCurrentConnectionToPool()
    If conn Is Nothing Then Exit Sub
    
    m_connPool.ReturnConnectionToPool conn
End Sub


Public Sub DiscardCurrentConnection()
    ' just won't return as in MSMDCOnnPool specs
    Set conn = Nothing
End Sub






Public Function BuildCellsetXml(Mdx As String) As String


    ' propagate errors
    On Error GoTo 0
    
    Dim errNum As Long
    Dim errSource As String
    Dim errDescription As String
    
    Dim f As TextStream
    Dim fso As New FileSystemObject
        
    Dim cst As New ADOMD.Cellset
    
    Dim strOut As String
        
    
    'Set conn = m_connPool.GetConnection(ConnectionString)
    'Set conn = m_connPool.GetConnectionFromPool(ConnectionString)
    
    
    Set cst.ActiveConnection = conn
    
    cst.Open Mdx
    strOut = BuildDomXML(cst)
    cst.Close
    
    
    BuildCellsetXml = strOut
End Function



Public Function BuildCellset(Mdx As String) As String


    ' propagate errors
    On Error GoTo 0
    
    Dim errNum As Long
    Dim errSource As String
    Dim errDescription As String
    
    Dim f As TextStream
    Dim fso As New FileSystemObject
        
    Dim cst As New ADOMD.Cellset
    
    Dim strOut As String
        
    
    'Set conn = m_connPool.GetConnection(ConnectionString)
    'Set conn = m_connPool.GetConnectionFromPool(ConnectionString)
    
    
    Set cst.ActiveConnection = conn
    
    cst.Open Mdx
    strOut = BuildCellsetString(cst)
    cst.Close
    
    
    BuildCellset = strOut
End Function




Private Sub WriteLog(msg As String)
        Dim f As TextStream
        Dim fso As New FileSystemObject
        Set f = fso.OpenTextFile("c:\FIXMLCellset.log", ForAppending, True, TristateFalse)
        f.WriteLine Now() & " : " & msg
        f.Close
        Set f = Nothing
        Set fso = Nothing
End Sub



'Sub Main()
'
'
'    Dim dicCmdLineParams As New Dictionary
'    Dim varDicItem As Variant
'    Dim varDicItem1 As Variant
'    Dim i As Integer
'    Dim j As Integer
'
'    Dim strCommand As String
'    Dim iStartPosition As Integer
'    Dim iEndPosition As Integer
'    Dim iTempEndPosition As Integer
'
'
'    strCommand = Command()
'
'    '--- getting expected parameters from command line ---
'    dicCmdLineParams.Add "/outpath", ""
'    dicCmdLineParams.Add "/connstring", ""
'    dicCmdLineParams.Add "/mdx", ""
'
'    For Each varDicItem In dicCmdLineParams
'        iStartPosition = InStr(1, strCommand, varDicItem)
'        If iStartPosition <> 0 Then
'            iStartPosition = iStartPosition + Len(varDicItem)
'            iEndPosition = 0
'            For Each varDicItem1 In dicCmdLineParams
'                iTempEndPosition = InStr(iStartPosition, strCommand, varDicItem1)
'                If varDicItem <> varDicItem1 And iStartPosition < iTempEndPosition And (iTempEndPosition < iEndPosition Or iEndPosition = 0) Then
'                    iEndPosition = iTempEndPosition
'                End If
'            Next
'            If iEndPosition > iStartPosition Then
'                dicCmdLineParams.Item(varDicItem) = Trim(Mid(strCommand, iStartPosition, iEndPosition - iStartPosition))
'            Else
'                dicCmdLineParams.Item(varDicItem) = Trim(Mid(strCommand, iStartPosition, Len(strCommand) - iStartPosition + 1))
'            End If
'        End If
'    Next
'
'    '--------------------------------------------------------
'
'    Dim fiDac As New FF_FI_DataAccess.OlapQuerySystem
'    Set cst = New ADOMD.Cellset
'    MsgBox fiDac.Increment()
'    MsgBox fiDac.ConPool.PoolSize
'    Set cst = fiDac.GetCellset(dicCmdLineParams.Item("/mdx"), dicCmdLineParams.Item("/connstring"))
'    Dim myconn As Connection
'    Set myconn = cst.ActiveConnection
'    myconn.CommandTimeout = 1
'    BuildXML (dicCmdLineParams.Item("/outpath"))
'    cst.Close
'    Set cst = Nothing
'
'
'
'
'
'End Sub





Public Function BuildCellsetString(ByRef cst As ADOMD.Cellset) As String


    Dim i As Integer
    Dim j As Integer
    Dim mem As ADOMD.Member
    Dim Axis0 As ADOMD.Axis
    Dim Axis1 As ADOMD.Axis
    Dim curCell As ADOMD.Cell

    Dim strCat As New Catter
    Dim strOut As String

    Dim iAx0PosCount As Long
    Dim iAx1PosCount As Long
    Dim iAx0MemCount As Integer
    Dim iAx1MemCount As Integer





    Set Axis0 = cst.Axes(0)
    Set Axis1 = cst.Axes(1)

    iAx0PosCount = Axis0.Positions.Count
    iAx0MemCount = Axis0.DimensionCount
    iAx1PosCount = Axis1.Positions.Count
    iAx1MemCount = Axis1.DimensionCount


    strCat iAx0PosCount
    strCat Chr(9)
    strCat iAx0MemCount
    strCat Chr(13)
    strCat iAx1PosCount
    strCat Chr(9)
    strCat iAx1MemCount
    
    strCat Chr(13)

    For i = 0 To iAx0PosCount - 1
        For j = 0 To iAx0MemCount - 1

            Set mem = Axis0.Positions(i).Members(j)

            strCat mem.UniqueName
            strCat Chr(8)
            strCat mem.Caption
            strCat Chr(8)
            strCat mem.ChildCount
            strCat Chr(8)
            strCat mem.LevelDepth
            
            If j < iAx0MemCount - 1 Then
                strCat Chr(9)
            End If

        Next
        
        If i < iAx0PosCount - 1 Then
            strCat Chr(10)
        End If
            
    Next

    strCat Chr(13)

    For i = 0 To iAx1PosCount - 1
        For j = 0 To iAx1MemCount - 1

            Set mem = Axis1.Positions(i).Members(j)

            strCat mem.UniqueName
            strCat Chr(8)
            strCat mem.Caption
            strCat Chr(8)
            strCat mem.ChildCount
            strCat Chr(8)
            strCat mem.LevelDepth
            
            If j < iAx1MemCount - 1 Then
                strCat Chr(9)
            End If

        Next
        
        If i < iAx1PosCount - 1 Then
            strCat Chr(10)
        End If
            
    Next

    strCat Chr(13)
    
    
    For i = 0 To iAx0PosCount - 1
        For j = 0 To iAx1PosCount - 1

            Set curCell = cst(i, j)
           
            strCat sGetCellValue(curCell)
            strCat Chr(8)
            strCat sGetCellFormattedValue(curCell)
            
            If j < iAx1PosCount - 1 Then
                strCat Chr(9)
            End If

        Next j
        
        If i < iAx0PosCount - 1 Then
            strCat Chr(10)
        End If
        
    Next i

    strOut = strCat.Dump
    Set strCat = Nothing

    BuildCellsetString = strOut

End Function




Public Function BuildDomXML(ByRef cst As ADOMD.Cellset) As String


    Dim i As Long
    Dim j As Long
    Dim mem As ADOMD.Member
    Dim Axis0 As ADOMD.Axis
    Dim Axis1 As ADOMD.Axis
    Dim curCell As ADOMD.Cell

    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    'Dim strCat As New Catter
    
    Dim iAx0PosCount As Long
    Dim iAx1PosCount As Long
    Dim iAx0MemCount As Long
    Dim iAx1MemCount As Long
    
    
       


    Set Axis0 = cst.Axes(0)
    Set Axis1 = cst.Axes(1)
    
    iAx0PosCount = Axis0.Positions.Count
    iAx0MemCount = Axis0.DimensionCount
    iAx1PosCount = Axis1.Positions.Count
    iAx1MemCount = Axis1.DimensionCount


    'strCat "<OlapCellsetData xmlns=""http://tempuri.org/OlapCellsetData.xsd"">"
    Set xmlRoot = xmlDoc.createElement("OlapCellsetData")
    xmlRoot.setAttribute "xmlns", "http://tempuri.org/OlapCellsetData.xsd"
    xmlDoc.appendChild xmlRoot
    
    
    Set xmlElementParent = xmlDoc.createElement("CellsetMetaData")
    xmlRoot.appendChild xmlElementParent
    
    Set xmlElement = xmlDoc.createElement("Ax0PosCount")
    xmlElement.Text = iAx0PosCount
    xmlElementParent.appendChild xmlElement
    
    Set xmlElement = xmlDoc.createElement("Ax0MemCount")
    xmlElement.Text = iAx0MemCount
    xmlElementParent.appendChild xmlElement
    
    Set xmlElement = xmlDoc.createElement("Ax1PosCount")
    xmlElement.Text = iAx1PosCount
    xmlElementParent.appendChild xmlElement
    
    Set xmlElement = xmlDoc.createElement("Ax1MemCount")
    xmlElement.Text = iAx1MemCount
    xmlElementParent.appendChild xmlElement
    
    

    
    
    For i = 0 To iAx0PosCount - 1
        For j = 0 To iAx0MemCount - 1
            
            Set mem = Axis0.Positions(i).Members(j)
            
            Set xmlElementParent = xmlDoc.createElement("Ax0Mem")
            xmlRoot.appendChild xmlElementParent
    
    
            Set xmlElement = xmlDoc.createElement("Pos")
            xmlElement.Text = i
            xmlElementParent.appendChild xmlElement
    
            Set xmlElement = xmlDoc.createElement("MPos")
            xmlElement.Text = j
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("UName")
            xmlElement.Text = mem.UniqueName    'XMLEscape(mem.UniqueName)
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("Name")
            xmlElement.Text = mem.Caption   'XMLEscape(mem.Caption)
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("CCnt")
            xmlElement.Text = mem.ChildCount
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("Lvl")
            xmlElement.Text = mem.LevelDepth
            xmlElementParent.appendChild xmlElement
            
        Next
    Next
    
    
    For i = 0 To iAx1PosCount - 1
        For j = 0 To iAx1MemCount - 1
            
            Set mem = Axis1.Positions(i).Members(j)

            Set xmlElementParent = xmlDoc.createElement("Ax1Mem")
            xmlRoot.appendChild xmlElementParent
    
    
            Set xmlElement = xmlDoc.createElement("Pos")
            xmlElement.Text = i
            xmlElementParent.appendChild xmlElement
    
            Set xmlElement = xmlDoc.createElement("MPos")
            xmlElement.Text = j
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("UName")
            xmlElement.Text = mem.UniqueName    'XMLEscape(mem.UniqueName)
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("Name")
            xmlElement.Text = mem.Caption   '(mem.Caption)
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("CCnt")
            xmlElement.Text = mem.ChildCount
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("Lvl")
            xmlElement.Text = mem.LevelDepth
            xmlElementParent.appendChild xmlElement
            
        Next
    Next
    
    
    
    
    For i = 0 To iAx0PosCount - 1
        For j = 0 To iAx1PosCount - 1
            
            Set curCell = cst(i, j)
            
            Set xmlElementParent = xmlDoc.createElement("Cl")
            xmlRoot.appendChild xmlElementParent
    
    
            Set xmlElement = xmlDoc.createElement("Ax0")
            xmlElement.Text = i
            xmlElementParent.appendChild xmlElement
    
            Set xmlElement = xmlDoc.createElement("Ax1")
            xmlElement.Text = j
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("Val")
            xmlElement.Text = sGetCellValue(curCell)
            xmlElementParent.appendChild xmlElement
            
            Set xmlElement = xmlDoc.createElement("FVal")
            xmlElement.Text = sGetCellFormattedValue(curCell)
            xmlElementParent.appendChild xmlElement
            
        Next j
    Next i

    BuildDomXML = xmlDoc.xml
    Set xmlDoc = Nothing
    Set xmlElementParent = Nothing
    Set xmlElement = Nothing
    
    
End Function




Private Function XMLEscape(strIn As String) As String
    Dim strOut As String
    strOut = Replace(strIn, "&", "&amp;")
    strOut = Replace(strOut, "'", "&apos;")
    strOut = Replace(strOut, ">", "&gt;")
    strOut = Replace(strOut, "<", "&lt;")
    strOut = Replace(strOut, """", "%22;")
    XMLEscape = strOut
End Function






Private Function sGetCellValue(cl As ADOMD.Cell) As String
    On Error GoTo sGetCellValueErr
   
    sGetCellValue = cl.Value & ""
    On Error GoTo 0
    
    Exit Function
sGetCellValueErr:
    sGetCellValue = "#ERR" '* Value to display in a cell for which an error occured
    On Error GoTo 0
End Function

Private Function sGetCellFormattedValue(cl As ADOMD.Cell) As String
    On Error GoTo sGetCellValueErr
   
    sGetCellFormattedValue = cl.FormattedValue & ""
    On Error GoTo 0
    
    Exit Function
sGetCellValueErr:
    sGetCellFormattedValue = "#ERR" '* Value to display in a cell for which an error occured
    On Error GoTo 0
End Function




