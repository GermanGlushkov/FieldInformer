Attribute VB_Name = "Main_module"

Option Explicit

Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessId As Long) As Long
Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Long, lpExitCode As Long) As Long
Private Const PROCESS_QUERY_INFORMATION = &H400
Private Const STILL_ACTIVE = &H103

Private Declare Function GetComputerName Lib "kernel32" Alias "GetComputerNameA" _
                         (ByVal lpBuffer As String, _
                          ByRef nSize As Long) As Long
Private Const MAX_COMPUTERNAME_LENGTH = 16    'Maximum length of computer network name (including NULL)
'--



Sub Main()

    
    
End Sub


Public Function RestoreDatabase(CabPath As String)

    Dim hProcess As Long
    Dim RetVal As Long
    
    Dim Servername As String
    Dim DataPath As String
    Dim RootDir As String
    Dim ExePath As String
    Dim CabFileName As String
    Dim DatabaseName As String
    Dim Execstr As String
    Dim fso As FileSystemObject


    Dim sBuff As String         'Buffer for name
    Dim lBuff As Long           'Length of name buffer

    sBuff = String$(MAX_COMPUTERNAME_LENGTH, 0)
    lBuff = Len(sBuff)

    If GetComputerName(sBuff, lBuff) Then
        Servername = Left$(sBuff, lBuff)
    Else
        Servername = ""
    End If


    RootDir = GetRegistryKey("SOFTWARE\Microsoft\OLAP Server\CurrentVersion", "RootDir")
    
    Execstr = """" & App.Path & "\MSMDARCH.EXE"" /r " & Servername & " """ & RootDir & "\"" """ & CabPath & """"
    'Shell Execstr, vbHide



    hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, False, Shell(Execstr, vbNormalFocus))
    Do
        GetExitCodeProcess hProcess, RetVal
        DoEvents: Sleep 100
    'Loop until the process is completed
    Loop While RetVal = STILL_ACTIVE


    Sleep 10000
    
    Set fso = Nothing
    
End Function


Public Function ArchiveDatabase(DatabaseName As String, CabPath As String)

    Dim hProcess As Long
    Dim RetVal As Long
    
    Dim Servername As String
    Dim DataPath As String
    Dim RootDir As String
    Dim ExePath As String
    Dim CabFileName As String
    Dim DatabaseName As String
    Dim Execstr As String
    Dim fso As FileSystemObject


    Dim sBuff As String         'Buffer for name
    Dim lBuff As Long           'Length of name buffer

    sBuff = String$(MAX_COMPUTERNAME_LENGTH, 0)
    lBuff = Len(sBuff)

    If GetComputerName(sBuff, lBuff) Then
        Servername = Left$(sBuff, lBuff)
    Else
        Servername = ""
    End If


    RootDir = GetRegistryKey("SOFTWARE\Microsoft\OLAP Server\CurrentVersion", "RootDir")
    
    Execstr = """" & App.Path & "\MSMDARCH.EXE"" /a " & Servername & " """ & RootDir & "\"" """ & DatabaseName & """ """ & CabPath & """"
    'Shell Execstr, vbHide



    hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, False, Shell(Execstr, vbNormalFocus))
    Do
        GetExitCodeProcess hProcess, RetVal
        DoEvents: Sleep 100
    'Loop until the process is completed
    Loop While RetVal = STILL_ACTIVE


    Sleep 10000
    
    Set fso = Nothing
    
End Function


