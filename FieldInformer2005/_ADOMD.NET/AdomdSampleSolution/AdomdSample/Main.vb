Option Explicit On 

Imports System.IO
Imports System.Xml
Imports System.Windows.Forms
Imports Microsoft.AnalysisServices.AdomdClient

'======================================================================
'
'  File:      AdomdSampleRoutines.vb
'  Summary:   Main routine for starting sample.
'  Date:	  02/24/2004
'
'----------------------------------------------------------------------
'
'  This file is part of the ADOMD.NET Software Development Kit.
'  Copyright (C) 2003 Microsoft Corporation.  All rights reserved.
'
'This source code is intended only as a supplement to Microsoft
'Development Tools and/or on-line documentation.  See these other
'materials for detailed information regarding Microsoft code samples.
'
'THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'
'======================================================================

Module AdomdSampleRoutines

#Region "== Variables =========================================================="
    Public m_Connection As AdomdConnection
    Public m_Helper As New AdomdHelper
    Public m_MainDialog As MainDialog
#End Region

#Region "== Methods ============================================================"
    Public Sub Main()
        Try
            ' Load and display the main dialog.
            m_MainDialog = New MainDialog
            m_MainDialog.ShowDialog()
        Catch ex As Exception
            ' Do nothing.
            MsgBox("An unexpected error occurred:" & vbCrLf & ex.Message)
        Finally
            ' Clean
            m_MainDialog = Nothing
            If m_Helper.IsConnected(m_Connection) Then
                m_Connection.Close()
            End If
            m_Connection = Nothing
        End Try
    End Sub
#End Region

End Module
