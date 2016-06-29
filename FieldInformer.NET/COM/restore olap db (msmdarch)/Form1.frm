VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   2544
   ClientLeft      =   48
   ClientTop       =   288
   ClientWidth     =   3744
   LinkTopic       =   "Form1"
   ScaleHeight     =   2544
   ScaleWidth      =   3744
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   372
      Left            =   960
      TabIndex        =   0
      Top             =   960
      Width           =   1452
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessId As Long) As Long
Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Long, lpExitCode As Long) As Long
Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

Private Const PROCESS_QUERY_INFORMATION = &H400
Private Const STILL_ACTIVE = &H103



Private Sub Command1_Click()

    Dim Servername As String
    Dim DataPath As String
    Dim TempPath As String
    Dim ExePath As String
    Dim CabFileName As String
    Dim DatabaseName As String
    Dim LogFileName As String
    Dim Execstr As String
    
    Dim hProcess As Long
    Dim RetVal As Long
    
    Servername = "GER-LATITUDE"
    DataPath = """C:\Program Files\Microsoft Analysis Services\Data\"""
    TempPath = """C:\Program Files\Microsoft Analysis Services\Data\"""
    ExePath = """C:\Program Files\Microsoft Analysis Services\Bin\"
    DatabaseName = ""                         ' leave this blank when restoring
    CabFileName = """C:\Program Files\Microsoft Analysis Services\Data\DBSALESPP.cab"""  ' Backup File
    LogFileName = """D:\SetupFiles\Wise\FieldInformer 2.0\DBSALESPP.log"""  ' LogFile
    Execstr = ExePath & "MSMDARCH.EXE"" /r " & Servername & " " & DataPath & " " & DatabaseName & " " & CabFileName & " " & LogFileName & " " & TempPath
    
    
    hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, False, Shell(Execstr, vbNormalFocus))

    Do
        GetExitCodeProcess hProcess, RetVal

        DoEvents
        Sleep 100

    'Loop until the process is completed
    Loop While RetVal = STILL_ACTIVE

    
End Sub

