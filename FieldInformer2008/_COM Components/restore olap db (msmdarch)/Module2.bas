Attribute VB_Name = "GetRegistry"
Option Explicit

Private Const KEY_ALL_ACCESS As Long = &H3F
Private Const HKEY_LOCAL_MACHINE As Long = &H80000002
Private Const REG_SZ As Long = 1
Private Const ERROR_SUCCESS As Long = 0

Private Declare Function RegOpenKeyEx _
    Lib "advapi32.dll" Alias "RegOpenKeyExA" _
   (ByVal hKey As Long, _
    ByVal lpSubKey As String, _
    ByVal ulOptions As Long, _
    ByVal samDesired As Long, _
    phkResult As Long) As Long

Private Declare Function RegCloseKey Lib "advapi32.dll" _
   (ByVal hKey As Long) As Long

Private Declare Function RegQueryValueEx Lib "advapi32.dll" _
   Alias "RegQueryValueExA" _
  (ByVal hKey As Long, ByVal lpszValueName As String, _
   ByVal lpdwRes As Long, lpType As Long, _
   lpData As Any, nSize As Long) As Long

'demo using button - move to sub as needed
Public Function GetRegistryKey(sKeyName As String, sValue As String) As String


   Dim hKey As Long
   Dim dwDataType As Long
   Dim dwDataSize As Long
   'Dim sKeyName As String
   'Dim sValue As String
   Dim sDataRet As String

  'open key
   'sKeyName = "SOFTWARE\McAfee\VirusScan"
   'sValue = "AlertConfigEXE"

   hKey = RegKeyOpen(HKEY_LOCAL_MACHINE, sKeyName)

   If hKey <> 0 Then

     'determine size and type of data to be read.
     'In this case it should be a string (REG_SZ) value.
      dwDataSize = RegGetStringSize(ByVal hKey, sValue, dwDataType)

      If dwDataSize > 0 Then

        'get the value for that key
         sDataRet = RegGetStringValue(hKey, sValue, dwDataSize)

        'if a value returned
         If Len(sDataRet) = 0 Then sDataRet = ""

      End If
   End If

  'clean up
   Call RegCloseKey(hKey)



    GetRegistryKey = sDataRet
    
    
End Function


Private Function RegKeyOpen(dwKeyType As Long, sKeyPath As String) As Long

   Dim hKey As Long

   If RegOpenKeyEx(dwKeyType, _
                   sKeyPath, 0&, _
                   KEY_ALL_ACCESS, hKey) = ERROR_SUCCESS Then

      RegKeyOpen = hKey

   End If

End Function


Private Function RegGetStringValue(ByVal hKey As Long, _
                                   ByVal sValue As String, _
                                   dwDataSize As Long) As String

   Dim sDataRet As String
   Dim dwDataRet As Long
   Dim pos As Long

  'get the value of the passed key
   sDataRet = Space$(dwDataSize)
   dwDataRet = Len(sDataRet)

   If RegQueryValueEx(hKey, _
                      sValue, _
                      ByVal 0&, _
                      dwDataSize, _
                      ByVal sDataRet, _
                      dwDataRet) = ERROR_SUCCESS Then

      If dwDataRet > 0 Then

         pos = InStr(sDataRet, Chr$(0))
         RegGetStringValue = Left$(sDataRet, pos - 1)

      End If
   End If

End Function


Private Function RegGetStringSize(ByVal hKey As Long, _
                                  ByVal sValue As String, _
                                  dwDataType As Long) As Long
   Dim dwDataSize As Long

   If RegQueryValueEx(hKey, _
                      sValue, _
                      0&, _
                      dwDataType, _
                      ByVal 0&, _
                      dwDataSize) = ERROR_SUCCESS Then



      If dwDataType = REG_SZ Then _
         RegGetStringSize = dwDataSize

   End If

End Function


