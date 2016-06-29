VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "OlapSchema"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private strCube As String

Private conn As ADODB.Connection
Private m_connPool As New MSMDConnPool.ConnPool



Public Sub SetConnection(Connection As ADODB.Connection)
    Set conn = Connection
End Sub

Public Sub SetCube(CubeName As String)
    strCube = CubeName
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




Public Function GetSchemaMembers(MemUniqueName() As String) As String

    Dim i As Long
    Dim dsoMem As ADOMD.Member
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    
    Set xmlRoot = xmlDoc.createElement("OlapMemSet")
    xmlRoot.setAttribute "xmlns", "http://tempuri.org/OlapMemSet.xsd"
    xmlDoc.appendChild xmlRoot
    
    For i = 0 To UBound(MemUniqueName)
       
        On Error Resume Next
        Set dsoMem = dsoCube.GetSchemaObject(adObjectTypeMember, MemUniqueName(i))
        If Err.Number <> 0 Then
            Err.Clear
            GoTo continue
        End If
        Set xmlElement = xmlDoc.createElement("M")
        xmlElement.setAttribute "UN", MemUniqueName(i)
        xmlElement.setAttribute "N", dsoMem.Caption
        xmlElement.setAttribute "CC", dsoMem.ChildCount
        xmlElement.setAttribute "LD", dsoMem.LevelDepth
        xmlRoot.appendChild xmlElement
continue:
    Next i
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    
    GetSchemaMembers = xmlDoc.xml
    
End Function



Public Function GetLevelMembers(LevelUniqueName As String) As String

    Dim i As Long
    Dim dsoMem As ADOMD.Member
    Dim dsoLevel As ADOMD.Level
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    
    Set xmlRoot = xmlDoc.createElement("SCHEMA")
    xmlDoc.appendChild xmlRoot
    
    Set dsoLevel = dsoCube.GetSchemaObject(adObjectTypeLevel, LevelUniqueName)
    
    For i = 0 To dsoLevel.Members.Count - 1
       
        Set dsoMem = dsoLevel.Members(i)
        
        Set xmlElement = xmlDoc.createElement("M")
        xmlElement.setAttribute "UN", dsoMem.UniqueName
        xmlElement.setAttribute "N", dsoMem.Caption
        xmlElement.setAttribute "CC", dsoMem.ChildCount
        xmlElement.setAttribute "LD", dsoMem.LevelDepth
        xmlRoot.appendChild xmlElement
        
    Next i
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    Set dsoLevel = Nothing
    Set dsoMem = Nothing
    
    GetLevelMembers = xmlDoc.xml
    
End Function



Public Function GetMemChildren(MemUniqueName As String, ByVal IfLeafAddItself As Boolean) As String

    Dim i As Long
    Dim dsoMem As ADOMD.Member
    Dim dsoParentMem As ADOMD.Member
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    
    Set xmlRoot = xmlDoc.createElement("SCHEMA")
    xmlDoc.appendChild xmlRoot
    
    Set dsoParentMem = dsoCube.GetSchemaObject(adObjectTypeMember, MemUniqueName)
    
    For i = 0 To dsoParentMem.Children.Count - 1
       
        Set dsoMem = dsoParentMem.Children(i)
        
        Set xmlElement = xmlDoc.createElement("M")
        xmlElement.setAttribute "UN", dsoMem.UniqueName
        xmlElement.setAttribute "N", dsoMem.Caption
        xmlElement.setAttribute "CC", dsoMem.ChildCount
        xmlElement.setAttribute "LD", dsoMem.LevelDepth
        xmlRoot.appendChild xmlElement
        
    Next i
    
    If i = 0 And IfLeafAddItself = True Then
        Set xmlElement = xmlDoc.createElement("M")
        xmlElement.setAttribute "UN", dsoParentMem.UniqueName
        xmlElement.setAttribute "N", dsoParentMem.Caption
        xmlElement.setAttribute "CC", dsoParentMem.ChildCount
        xmlElement.setAttribute "LD", dsoParentMem.LevelDepth
        xmlRoot.appendChild xmlElement
    End If
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    Set dsoParentMem = Nothing
    Set dsoMem = Nothing
    
    GetMemChildren = xmlDoc.xml
    
End Function



Public Function GetMemParentWithSiblings(HierUniqueName As String, MemUniqueName As String) As String

    Dim i As Long
    Dim dsoMem As ADOMD.Member
    Dim dsoGrandPa As ADOMD.Member
    Dim dsoParent As ADOMD.Member
    Dim dsoHier As ADOMD.Hierarchy
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    
    Set xmlRoot = xmlDoc.createElement("SCHEMA")
    xmlDoc.appendChild xmlRoot
    
    Set dsoMem = dsoCube.GetSchemaObject(adObjectTypeMember, MemUniqueName)
    
    If dsoMem.LevelDepth = 0 Then
        Set xmlElement = xmlDoc.createElement("M")
        xmlElement.setAttribute "UN", dsoMem.UniqueName
        xmlElement.setAttribute "N", dsoMem.Caption
        xmlElement.setAttribute "CC", dsoMem.ChildCount
        xmlElement.setAttribute "LD", dsoMem.LevelDepth
        xmlRoot.appendChild xmlElement
    ElseIf dsoMem.LevelDepth = 1 Then
        Set dsoHier = dsoCube.GetSchemaObject(adObjectTypeHierarchy, HierUniqueName)
        
        For i = 0 To dsoHier.Levels(0).Members.Count - 1
            Set dsoMem = dsoHier.Levels(0).Members(i)
            Set xmlElement = xmlDoc.createElement("M")
            xmlElement.setAttribute "UN", dsoMem.UniqueName
            xmlElement.setAttribute "N", dsoMem.Caption
            xmlElement.setAttribute "CC", dsoMem.ChildCount
            xmlElement.setAttribute "LD", dsoMem.LevelDepth
            xmlRoot.appendChild xmlElement
        Next i
    Else
        Set dsoGrandPa = dsoMem.Parent.Parent
        
        For i = 0 To dsoGrandPa.ChildCount - 1
            Set dsoParent = dsoGrandPa.Children(i)
            Set xmlElement = xmlDoc.createElement("M")
            xmlElement.setAttribute "UN", dsoParent.UniqueName
            xmlElement.setAttribute "N", dsoParent.Caption
            xmlElement.setAttribute "CC", dsoParent.ChildCount
            xmlElement.setAttribute "LD", dsoParent.LevelDepth
            xmlRoot.appendChild xmlElement
        Next i
    End If
    
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    Set dsoGrandPa = Nothing
    Set dsoParent = Nothing
    Set dsoMem = Nothing
    
    GetMemParentWithSiblings = xmlDoc.xml
    
End Function

Public Function GetMemParent(HierUniqueName As String, MemUniqueName As String) As String

    Dim dsoMem As ADOMD.Member
    Dim dsoParent As ADOMD.Member
    Dim dsoHier As ADOMD.Hierarchy
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    
    Set xmlRoot = xmlDoc.createElement("SCHEMA")
    xmlDoc.appendChild xmlRoot
    
    Set dsoMem = dsoCube.GetSchemaObject(adObjectTypeMember, MemUniqueName)
    
    If dsoMem.LevelDepth > 0 Then
        Set dsoParent = dsoMem.Parent
        
        Set xmlElement = xmlDoc.createElement("M")
        xmlElement.setAttribute "UN", dsoParent.UniqueName
        xmlElement.setAttribute "N", dsoParent.Caption
        xmlElement.setAttribute "CC", dsoParent.ChildCount
        xmlElement.setAttribute "LD", dsoParent.LevelDepth
        xmlRoot.appendChild xmlElement
    End If
    
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    Set dsoParent = Nothing
    Set dsoMem = Nothing
    
    GetMemParent = xmlDoc.xml
    
End Function


Public Function GetMemGrandParent(HierUniqueName As String, MemUniqueName As String) As String

    Dim dsoMem As ADOMD.Member
    Dim dsoGrandPa As ADOMD.Member
    Dim dsoHier As ADOMD.Hierarchy
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    
    Set xmlRoot = xmlDoc.createElement("SCHEMA")
    xmlDoc.appendChild xmlRoot
    
    Set dsoMem = dsoCube.GetSchemaObject(adObjectTypeMember, MemUniqueName)
    
    If dsoMem.LevelDepth > 1 Then
        Set dsoGrandPa = dsoMem.Parent.Parent
        
        Set xmlElement = xmlDoc.createElement("M")
        xmlElement.setAttribute "UN", dsoGrandPa.UniqueName
        xmlElement.setAttribute "N", dsoGrandPa.Caption
        xmlElement.setAttribute "CC", dsoGrandPa.ChildCount
        xmlElement.setAttribute "LD", dsoGrandPa.LevelDepth
        xmlRoot.appendChild xmlElement
    End If
    
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    Set dsoGrandPa = Nothing
    Set dsoMem = Nothing
    
    GetMemGrandParent = xmlDoc.xml
    
End Function


Public Function GetMainSchema() As String

    Dim i_dim, i_hier, i_level As Long
    Dim dsoDim As ADOMD.Dimension
    Dim dsoHier As ADOMD.Hierarchy
    Dim dsoLevel As ADOMD.Level
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    Set xmlRoot = xmlDoc.createElement("OlapMainSchema")
    xmlDoc.appendChild xmlRoot
    
    For i_dim = 0 To dsoCube.Dimensions.Count - 1
       
        Set dsoDim = dsoCube.Dimensions(i_dim)
        Set xmlElement = xmlDoc.createElement("D")
        xmlElement.setAttribute "UN", dsoDim.UniqueName
        xmlElement.setAttribute "N", dsoDim.Name
        xmlRoot.appendChild xmlElement
        Set xmlElementParent = xmlElement
        
        For i_hier = 0 To dsoDim.Hierarchies.Count - 1
       
            Set dsoHier = dsoDim.Hierarchies(i_hier)
            Set xmlElement = xmlDoc.createElement("H")
            xmlElement.setAttribute "UN", dsoHier.UniqueName
            xmlElement.setAttribute "N", dsoHier.Name
            xmlElementParent.appendChild xmlElement
            Set xmlElementParent = xmlElement
    
            For i_level = 0 To dsoHier.Levels.Count - 1
               
                Set dsoLevel = dsoHier.Levels(i_level)
                Set xmlElement = xmlDoc.createElement("L")
                xmlElement.setAttribute "UN", dsoLevel.UniqueName
                xmlElement.setAttribute "N", dsoLevel.Name
                xmlElement.setAttribute "LD", dsoLevel.Depth
                xmlElementParent.appendChild xmlElement
                 
            Next i_level
                      
        Next i_hier
        
    Next i_dim
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    
    GetMainSchema = xmlDoc.xml
    
End Function



Public Function GetHierarchyTree(HierUniqueNames() As String, OpenNodesUniqueNames() As String) As String
    
    Dim i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10 As Long
    Dim dsoHier As ADOMD.Hierarchy
    Dim dsoMem As ADOMD.Member
    Dim dsoMemParent As ADOMD.Member
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementParent As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    
    Set xmlRoot = xmlDoc.createElement("OlapHierTreeSet")
    xmlDoc.appendChild xmlRoot
    
    For i0 = LBound(HierUniqueNames) To UBound(HierUniqueNames)
       
        Set dsoHier = dsoCube.GetSchemaObject(adObjectTypeHierarchy, HierUniqueNames(i0))
        Set xmlElement = xmlDoc.createElement("H")
        xmlElement.setAttribute "UN", dsoHier.UniqueName
        xmlRoot.appendChild xmlElement
        Set xmlElementParent = xmlElement

        For i1 = 0 To dsoHier.Levels(0).Members.Count - 1
        
             Set dsoMem = dsoHier.Levels(0).Members(i1)
             Set xmlElement = xmlDoc.createElement("M")
             xmlElement.setAttribute "UN", dsoMem.UniqueName
             xmlElement.setAttribute "N", dsoMem.Caption
             xmlElement.setAttribute "CC", dsoMem.ChildCount
             xmlElement.setAttribute "LD", dsoMem.LevelDepth
             xmlElementParent.appendChild xmlElement
             If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i1
             xmlElement.setAttribute "O", "1"
             Set xmlElementParent = xmlElement
             Set dsoMemParent = dsoMem
             
             For i2 = 0 To dsoMemParent.ChildCount - 1
            
                 Set dsoMem = dsoMemParent.Children(i2)
                 Set xmlElement = xmlDoc.createElement("M")
                 xmlElement.setAttribute "UN", dsoMem.UniqueName
                 xmlElement.setAttribute "N", dsoMem.Caption
                 xmlElement.setAttribute "CC", dsoMem.ChildCount
                 xmlElement.setAttribute "LD", dsoMem.LevelDepth
                 xmlElementParent.appendChild xmlElement
                 If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i2
                 xmlElement.setAttribute "O", "1"
                 Set xmlElementParent = xmlElement
                 Set dsoMemParent = dsoMem
                 
                 For i3 = 0 To dsoMemParent.ChildCount - 1
                
                     Set dsoMem = dsoMemParent.Children(i3)
                     Set xmlElement = xmlDoc.createElement("M")
                     xmlElement.setAttribute "UN", dsoMem.UniqueName
                     xmlElement.setAttribute "N", dsoMem.Caption
                     xmlElement.setAttribute "CC", dsoMem.ChildCount
                     xmlElement.setAttribute "LD", dsoMem.LevelDepth
                     xmlElementParent.appendChild xmlElement
                     If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i3
                     xmlElement.setAttribute "O", "1"
                     Set xmlElementParent = xmlElement
                     Set dsoMemParent = dsoMem
                    
                     For i4 = 0 To dsoMemParent.ChildCount - 1
                    
                         Set dsoMem = dsoMemParent.Children(i4)
                         Set xmlElement = xmlDoc.createElement("M")
                         xmlElement.setAttribute "UN", dsoMem.UniqueName
                         xmlElement.setAttribute "N", dsoMem.Caption
                         xmlElement.setAttribute "CC", dsoMem.ChildCount
                         xmlElement.setAttribute "LD", dsoMem.LevelDepth
                         xmlElementParent.appendChild xmlElement
                         If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i4
                         xmlElement.setAttribute "O", "1"
                         Set xmlElementParent = xmlElement
                         Set dsoMemParent = dsoMem
                        
                         For i5 = 0 To dsoMemParent.ChildCount - 1
                        
                             Set dsoMem = dsoMemParent.Children(i5)
                             Set xmlElement = xmlDoc.createElement("M")
                             xmlElement.setAttribute "UN", dsoMem.UniqueName
                             xmlElement.setAttribute "N", dsoMem.Caption
                             xmlElement.setAttribute "CC", dsoMem.ChildCount
                             xmlElement.setAttribute "LD", dsoMem.LevelDepth
                             xmlElementParent.appendChild xmlElement
                             If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i5
                             xmlElement.setAttribute "O", "1"
                             Set xmlElementParent = xmlElement
                             Set dsoMemParent = dsoMem
                            
                             For i6 = 0 To dsoMemParent.ChildCount - 1
                            
                                 Set dsoMem = dsoMemParent.Children(i6)
                                 Set xmlElement = xmlDoc.createElement("M")
                                 xmlElement.setAttribute "UN", dsoMem.UniqueName
                                 xmlElement.setAttribute "N", dsoMem.Caption
                                 xmlElement.setAttribute "CC", dsoMem.ChildCount
                                 xmlElement.setAttribute "LD", dsoMem.LevelDepth
                                 xmlElementParent.appendChild xmlElement
                                 If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i6
                                 xmlElement.setAttribute "O", "1"
                                 Set xmlElementParent = xmlElement
                                 Set dsoMemParent = dsoMem
                                
                                 For i7 = 0 To dsoMemParent.ChildCount - 1
                                
                                     Set dsoMem = dsoMemParent.Children(i7)
                                     Set xmlElement = xmlDoc.createElement("M")
                                     xmlElement.setAttribute "UN", dsoMem.UniqueName
                                     xmlElement.setAttribute "N", dsoMem.Caption
                                     xmlElement.setAttribute "CC", dsoMem.ChildCount
                                     xmlElement.setAttribute "LD", dsoMem.LevelDepth
                                     xmlElementParent.appendChild xmlElement
                                     If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i7
                                     xmlElement.setAttribute "O", "1"
                                     Set xmlElementParent = xmlElement
                                     Set dsoMemParent = dsoMem
                                    
                                     For i8 = 0 To dsoMemParent.ChildCount - 1
                                    
                                         Set dsoMem = dsoMemParent.Children(i8)
                                         Set xmlElement = xmlDoc.createElement("M")
                                         xmlElement.setAttribute "UN", dsoMem.UniqueName
                                         xmlElement.setAttribute "N", dsoMem.Caption
                                         xmlElement.setAttribute "CC", dsoMem.ChildCount
                                         xmlElement.setAttribute "LD", dsoMem.LevelDepth
                                         xmlElementParent.appendChild xmlElement
                                         If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i8
                                         xmlElement.setAttribute "O", "1"
                                         Set xmlElementParent = xmlElement
                                         Set dsoMemParent = dsoMem
                                        
                                         For i9 = 0 To dsoMemParent.ChildCount - 1
                                        
                                             Set dsoMem = dsoMemParent.Children(i9)
                                             Set xmlElement = xmlDoc.createElement("M")
                                             xmlElement.setAttribute "UN", dsoMem.UniqueName
                                             xmlElement.setAttribute "N", dsoMem.Caption
                                             xmlElement.setAttribute "CC", dsoMem.ChildCount
                                             xmlElement.setAttribute "LD", dsoMem.LevelDepth
                                             xmlElementParent.appendChild xmlElement
                                             If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i9
                                             xmlElement.setAttribute "O", "1"
                                             Set xmlElementParent = xmlElement
                                             Set dsoMemParent = dsoMem
                                            
                                             For i10 = 0 To dsoMemParent.ChildCount - 1
                                            
                                                 Set dsoMem = dsoMemParent.Children(i10)
                                                 Set xmlElement = xmlDoc.createElement("M")
                                                 xmlElement.setAttribute "UN", dsoMem.UniqueName
                                                 xmlElement.setAttribute "N", dsoMem.Caption
                                                 xmlElement.setAttribute "CC", dsoMem.ChildCount
                                                 xmlElement.setAttribute "LD", dsoMem.LevelDepth
                                                 xmlElementParent.appendChild xmlElement
                                                 'If ArrExists(OpenNodesUniqueNames, dsoMem.UniqueName) = False Then GoTo next_i10
                                                 'xmlElement.setAttribute "O", "1"
                                                 'Set xmlElementParent = xmlElement
                                                 'Set dsoMemParent = dsoMem
next_i10:
                                            Next i10
next_i9:
                                        Next i9
next_i8:
                                    Next i8
next_i7:
                                Next i7
next_i6:
                            Next i6
next_i5:
                        Next i5
next_i4:
                    Next i4
next_i3:
                Next i3
next_i2:
             Next i2
next_i1:
         Next i1
         
    Next i0
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    
    GetHierarchyTree = xmlDoc.xml
End Function




Private Function ArrExists(Arr() As String, Item As String) As Boolean
    Dim i As Long
    Dim lowBound As Long
    
    ' against not dimensioned array
    On Error Resume Next
    lowBound = LBound(Arr)
    If Err.Number <> 0 Then
        Err.Clear
        On Error GoTo 0
        ArrExists = False
        Exit Function
    End If
    
    For i = lowBound To UBound(Arr)
        If Arr(i) = Item Then
            ArrExists = True
            Exit Function
        End If
    Next i
    ArrExists = False
End Function






Private Function GetSchema(openNodeDict As Dictionary) As String
    
    Dim i_dim, i_hier, i_level, i As Long
    Dim dsoDim As ADOMD.Dimension
    Dim dsoHier As ADOMD.Hierarchy
    Dim dsoLevel As ADOMD.Level
    Dim dsoMem As ADOMD.Member
    Dim dsoMemParent As ADOMD.Member
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim openNodesDoc As New MSXML2.DOMDocument
    
    Dim xmlDoc As New MSXML2.DOMDocument
    Dim xmlElement As MSXML2.IXMLDOMElement
    Dim xmlElementDim As MSXML2.IXMLDOMElement
    Dim xmlElementHier As MSXML2.IXMLDOMElement
    Dim xmlElementLev As MSXML2.IXMLDOMElement
    Dim xmlDefMem As MSXML2.IXMLDOMElement
    Dim xmlRoot As MSXML2.IXMLDOMElement
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    Set xmlRoot = xmlDoc.createElement("SCHEMA")
    xmlDoc.appendChild xmlRoot
    
    For i_dim = 0 To dsoCube.Dimensions.Count - 1
           
        Set dsoDim = dsoCube.Dimensions(i_dim)
        Set xmlElementDim = xmlDoc.createElement("D")
        xmlElementDim.setAttribute "UN", dsoDim.UniqueName
        xmlElementDim.setAttribute "N", dsoDim.Name
        If openNodeDict.Exists(dsoDim.UniqueName) = True Then
            xmlElementDim.setAttribute "O", "1"
        End If
        xmlRoot.appendChild xmlElementDim
            
        For i_hier = 0 To dsoDim.Hierarchies.Count - 1
           
            Set dsoHier = dsoDim.Hierarchies(i_hier)
            Set xmlElementHier = xmlDoc.createElement("H")
            xmlElementHier.setAttribute "UN", dsoHier.UniqueName
            xmlElementHier.setAttribute "N", dsoHier.Name
            xmlElementDim.appendChild xmlElementHier
            
            ' ---------- all levels are right under hierarchy -----
            For i_level = 0 To dsoHier.Levels.Count - 1
                Set dsoLevel = dsoHier.Levels(i_level)
                Set xmlElementLev = xmlDoc.createElement("L")
                xmlElementLev.setAttribute "UN", dsoLevel.UniqueName
                xmlElementLev.setAttribute "N", dsoLevel.Name
                xmlElementLev.setAttribute "LD", dsoLevel.Depth
                xmlElementHier.appendChild xmlElementLev
                
                '------ default member of highest level------
                If dsoLevel.Depth = 0 Then
                    Set dsoMem = dsoLevel.Members(0)
                    Set xmlDefMem = xmlDoc.createElement("DM")
                    xmlDefMem.setAttribute "UN", dsoMem.UniqueName
                    xmlDefMem.setAttribute "N", dsoMem.Name
                    xmlDefMem.setAttribute "CC", dsoMem.ChildCount
                    xmlDefMem.setAttribute "LD", dsoMem.LevelDepth
                    xmlElementLev.appendChild xmlDefMem
                End If
            
            Next i_level

        
            If openNodeDict.Exists(dsoHier.UniqueName) = False Then GoTo next_i_hier
            xmlElementHier.setAttribute "O", "1"
            
    
            For i = 0 To dsoHier.Levels(0).Members.Count - 1
                 AppendXmlSchemaMember xmlElementHier, xmlDoc, dsoHier.Levels(0).Members(i), openNodeDict
            Next i
            
next_i_hier:
        Next i_hier
    
    Next i_dim
    
    Set dsoCat = Nothing
    Set dsoCube = Nothing
    
    GetSchema = xmlDoc.xml
End Function


Private Sub AppendXmlSchemaMember(xmlParent As MSXML2.IXMLDOMElement, xmlDoc As MSXML2.DOMDocument, dsoMem As ADOMD.Member, openNodeDict As Dictionary)

Dim xmlElement As MSXML2.IXMLDOMElement
Dim dsoChild As ADOMD.Member

Set xmlElement = xmlDoc.createElement("M")
xmlElement.setAttribute "UN", dsoMem.UniqueName
xmlElement.setAttribute "N", dsoMem.Caption
xmlElement.setAttribute "CC", dsoMem.ChildCount
xmlElement.setAttribute "LD", dsoMem.LevelDepth
xmlParent.appendChild xmlElement

'children
If openNodeDict.Exists(dsoMem.UniqueName) = True Then
    xmlElement.setAttribute "O", "1"
    For Each dsoChild In dsoMem.Children
        AppendXmlSchemaMember xmlElement, xmlDoc, dsoChild, openNodeDict
    Next
End If

End Sub


Public Function GetReportSchemaXml(OpenNodesXml As String) As String

    Dim openNodeDict As New Dictionary
    Dim dom As New MSXML2.DOMDocument
    Dim onEl As MSXML2.IXMLDOMElement
    Dim loadSuccess As Boolean
    Dim openNodeUN As String
    
    dom.async = False
    loadSuccess = dom.loadXML(OpenNodesXml)
    If loadSuccess = False Then Err.Raise 1000, "ReadReportSchema", "LoadXML failed"
    
    '------ open nodes ------
    For Each onEl In dom.getElementsByTagName("ON")
    
        openNodeUN = onEl.getAttribute("UN")
        ' -- not checking against schema , just adding to dictionary
        openNodeDict.Add openNodeUN, False
    Next
        
    '------ schema ---------
    GetReportSchemaXml = GetSchema(openNodeDict)
    
    
    Set dom = Nothing
    
End Function




Public Function GetReportXml(InReportXml As String) As String

    Dim dsoDim As ADOMD.Dimension
    Dim dsoHier As ADOMD.Hierarchy
    Dim dsoLevel As ADOMD.Level
    Dim dsoMem As ADOMD.Member
    Dim dsoCube As ADOMD.CubeDef
    Dim dsoCat As New ADOMD.Catalog
    
    Dim dimValid As Boolean
    Dim hierValid As Boolean
    Dim memValid As Boolean
    Dim dimUN As String
    Dim hierUN As String
    Dim levelUN As String
    Dim memUN As String
    Dim memCalc As String
    Dim openNodeUN As String
    Dim orderMemberUN As String
    
    Dim openNodeDict As New Dictionary
    
    Dim dom As New MSXML2.DOMDocument
    Dim dimEl As MSXML2.IXMLDOMElement
    Dim hierEl As MSXML2.IXMLDOMElement
    Dim levelEl As MSXML2.IXMLDOMElement
    Dim memEl As MSXML2.IXMLDOMElement
    
    Dim loadSuccess As Boolean
    
    
    dom.async = False
    loadSuccess = dom.loadXML(InReportXml)
    If loadSuccess = False Then Err.Raise 1000, "ReadReportSchema", "LoadXML failed"
    
    Set dsoCat.ActiveConnection = conn
    Set dsoCube = dsoCat.CubeDefs(strCube)
    
    
        
    '------ hierarchies ------
    For Each hierEl In dom.getElementsByTagName("H")

    hierValid = True
    hierUN = hierEl.getAttribute("UN")
    On Error Resume Next
    Set dsoHier = dsoCube.GetSchemaObject(adObjectTypeHierarchy, hierUN)
    If Err.Number <> 0 Then
        Err.Clear
        hierValid = False
        hierEl.setAttribute "E", "1"
    Else
        On Error GoTo 0
        hierValid = True
    End If
    On Error GoTo 0
    
    
    If hierValid Then
        
        '------ levels ------
        For Each levelEl In hierEl.getElementsByTagName("L")

            levelUN = levelEl.getAttribute("UN")
            On Error Resume Next
            Set dsoLevel = dsoCube.GetSchemaObject(adObjectTypeLevel, levelUN)
            If Err.Number <> 0 Then
                Err.Clear
                levelEl.setAttribute "E", "1"
            Else
                On Error GoTo 0
            End If
            On Error GoTo 0
        Next
        
        '------ members ------
        For Each memEl In hierEl.getElementsByTagName("M")

            memUN = memEl.getAttribute("UN")
            On Error Resume Next
            memCalc = memEl.getAttribute("C")
            If Err.Number <> 0 Then
                'it's invalid use of null
                Err.Clear
                memCalc = "0"
            End If
            On Error GoTo 0
            
            ' not checking calculated members
            If memCalc <> "1" Then
                On Error Resume Next
                Set dsoMem = dsoCube.GetSchemaObject(adObjectTypeMember, memUN)
                If Err.Number <> 0 Then
                    Err.Clear
                    memEl.setAttribute "E", "1"
                Else
                    On Error GoTo 0
                    memEl.setAttribute "N", dsoMem.Name
                    memEl.setAttribute "CC", dsoMem.ChildCount
                    memEl.setAttribute "LD", dsoMem.LevelDepth
                End If
            End If
            On Error GoTo 0
        Next
        
        '------ order tuple member ------
        For Each memEl In hierEl.getElementsByTagName("OTM")

            orderMemberUN = memEl.getAttribute("UN")
            On Error Resume Next
            Set dsoMem = dsoCube.GetSchemaObject(adObjectTypeMember, orderMemberUN)
            If Err.Number <> 0 Then
                Err.Clear
                memEl.setAttribute "E", "1"
            Else
                On Error GoTo 0
            End If
            On Error GoTo 0
        Next
    
    End If
    
Next
        
    
    GetReportXml = dom.xml
    
    
    Set dom = Nothing
    
End Function
