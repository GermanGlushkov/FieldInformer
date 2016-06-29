VERSION 5.00
Begin VB.Form TestForm 
   Caption         =   "Form1"
   ClientHeight    =   3945
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   9675
   LinkTopic       =   "Form1"
   ScaleHeight     =   3945
   ScaleWidth      =   9675
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command8 
      Caption         =   "Command8"
      Height          =   255
      Left            =   7440
      TabIndex        =   10
      Top             =   1680
      Width           =   975
   End
   Begin VB.CommandButton Command7 
      Caption         =   "Command7"
      Height          =   255
      Left            =   7440
      TabIndex        =   9
      Top             =   2400
      Width           =   975
   End
   Begin VB.CommandButton Command6 
      Caption         =   "Command6"
      Height          =   372
      Left            =   7320
      TabIndex        =   8
      Top             =   3360
      Width           =   972
   End
   Begin VB.TextBox Text3 
      Height          =   3012
      Left            =   4680
      MultiLine       =   -1  'True
      ScrollBars      =   3  'Both
      TabIndex        =   7
      Text            =   "TestForm.frx":0000
      Top             =   120
      Width           =   2532
   End
   Begin VB.CommandButton Command5 
      Caption         =   "Command5"
      Height          =   372
      Left            =   6120
      TabIndex        =   6
      Top             =   3360
      Width           =   972
   End
   Begin VB.CommandButton Command4 
      Caption         =   "Command4"
      Height          =   372
      Left            =   4800
      TabIndex        =   5
      Top             =   3360
      Width           =   1092
   End
   Begin VB.CommandButton Command3 
      Caption         =   "Command3"
      Height          =   372
      Left            =   3480
      TabIndex        =   4
      Top             =   3360
      Width           =   972
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Command2"
      Height          =   372
      Left            =   2160
      TabIndex        =   3
      Top             =   3360
      Width           =   852
   End
   Begin VB.TextBox Text2 
      Height          =   1932
      Left            =   240
      MultiLine       =   -1  'True
      ScrollBars      =   3  'Both
      TabIndex        =   2
      Text            =   "TestForm.frx":0006
      Top             =   1200
      Width           =   4332
   End
   Begin VB.TextBox Text1 
      Height          =   852
      Left            =   240
      MultiLine       =   -1  'True
      ScrollBars      =   3  'Both
      TabIndex        =   1
      Text            =   "TestForm.frx":000C
      Top             =   120
      Width           =   4332
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   372
      Left            =   480
      TabIndex        =   0
      Top             =   3360
      Width           =   1332
   End
End
Attribute VB_Name = "TestForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim obj As New FIXMLCellset.XMLCellsetClass
Dim conn As New ADODB.Connection


Private Sub Command1_Click()



Dim mdx As String




mdx = Text2

obj.SetConnectionFromPool "Data Source=localhost;Initial Catalog=foodmart 2000;Provider=MSOLAP;"
MsgBox obj.BuildCellset(mdx)
obj.ReturnCurrentConnectionToPool

End Sub

Private Sub Command2_Click()

    Dim mdx As String

    mdx = Text2
    
    obj.SetConnectionFromPool "Data Source=localhost;Initial Catalog=foodmart 2000;Provider=MSOLAP;"
    MsgBox obj.BuildCellsetXml(mdx)
    obj.ReturnCurrentConnectionToPool
    
End Sub

Private Sub Command3_Click()
    Dim obj As New FIXMLCellset.OlapSchema

    Dim OpenNodesUniqueNames(3) As String
    Dim HierUniqueNames(1) As String
    Dim xml As String
    
    obj.SetConnection conn
    obj.SetCube "Warehouse"
    Dim i As Integer
    
    OpenNodesUniqueNames(0) = "[Product].[All Products]"
    OpenNodesUniqueNames(1) = "[Product].[All Products].[Drink]"
    OpenNodesUniqueNames(2) = "[Product].[All Products].[Food]"
    
    xml = obj.GetSchema(OpenNodesUniqueNames)
    
    MsgBox xml
End Sub

Private Sub Command4_Click()

    Dim obj As New FIXMLCellset.OlapSchema
    Dim xml As String
    
    obj.SetConnection conn
    obj.SetCube "Warehouse"
    Dim i As Integer
    
    xml = obj.GetMainSchema()
    
    MsgBox xml
End Sub

Private Sub Command5_Click()
    Dim obj As New FIXMLCellset.OlapSchema
    Dim xml As String
    
    obj.SetConnection conn
    obj.SetCube "VIRTUAL_MODEL"
    Dim i As Integer
    
    xml = "<R N='Some Report' D='Descr'> " & _
   " <H UN='[Measures]' AX='1' AXO='0'> " & _
     "   <M UN='[Measures].[Unit Sales]' C='0' VI='1'/>" & _
      "  <M UN='[Measures].[Store Sales1]' C='0' VI='1'/>" & _
        "<M UN='[Measures].[**SUM**]' C='1' VI='1' T='VIS_AGR'/>" & _
        "<OTM UN='[Measures].[**SUM**]' />" & _
        "<ON UN='[Measures]'/>" & _
   " </H>" & _
   " <H UN='[Time]' AX='2' AXO='0'>" & _
      "  <M UN='[Time].[1997]' C='0' V='1'/>" & _
    "</H>" & _
    "<H UN='[Product]' AX='2' AXO='0'>" & _
        "<M UN='[Product].[All Products].[Food]' C='0' V='1'/>" & _
        "<ON UN='[Product]'/>" & _
        "<ON UN='[Product].[All Products]'/>" & _
        "<ON UN='[Product].[All Products].[Drink]'/>" & _
        "<ON UN='[Product].[All Products].[Food]'/>" & _
    "</H>" & _
" </R> "

    xml = Text3
    MsgBox xml
    Dim resultXml As String
    Dim schemaXml As String
    obj.ReadReportSchema xml, resultXml, schemaXml
    
    Text2 = schemaXml
    Text1 = resultXml
    MsgBox "done"
End Sub

Private Sub Command6_Click()
    Dim obj As New FIXMLCellset.OlapSchema
    Dim xml As String
    
    obj.SetConnection conn
    obj.SetCube "Warehouse"
    Dim i As Integer
    
    Dim unArr(2) As String
    
    unArr(0) = "[Product].[All Products].[Drink].[Beverages].[Drinks1]"
    unArr(1) = "[Product].[All Products].[Drink].[Beverages].[Drinks]"
    unArr(2) = "[Product].[All Products].[Drink].[Beverages].[Drinks]"
    
    xml = obj.GetSchemaMembers(unArr)
    
    MsgBox xml
End Sub

Private Sub Command7_Click()

    Dim obj As New FIXMLCellset.OlapSchema
    Dim xml As String
    
    obj.SetConnectionFromPool "Data Source=localhost;Initial Catalog=foodmart 2000;Provider=MSOLAP;"
    obj.SetCube "Warehouse"
    Dim i As Integer
    
    xml = obj.GetMemParentWithSiblings("[Product]", "[Product].[All Products].[Drink].[Beverages].[Drinks]")
    
    obj.ReturnCurrentConnectionToPool
    
    MsgBox xml
    
End Sub

Private Sub Command8_Click()
    Dim str As String
    str = CDbl("444.000,5")
    MsgBox str
End Sub

Private Sub Form_Load()


Text1 = "Data Source=localhost;Initial Catalog=foodmart 2000;Provider=MSOLAP;"
Text2 = "select " & _
    "{ [Measures].[Units Shipped], [Measures].[Units Ordered] } on columns," & _
    " NON EMPTY [Store].[Store Name].members on rows " & _
"from Warehouse"

conn.Open Text1


End Sub
