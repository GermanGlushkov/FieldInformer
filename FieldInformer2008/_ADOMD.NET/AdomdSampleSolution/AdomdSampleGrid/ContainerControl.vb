Option Explicit On 

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms

'======================================================================
'
'  File:      ContainerControl.vb
'  Summary:   User control that serves as the container for edit
'             controls attached to an ICellType implementation.
'             This control allows edit controls to be dynamically
'             associated with a grid at runtime, and traps grid-
'             specific input keys before the edit control receives
'             them.
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
Public Class ContainerControl
    Inherits System.Windows.Forms.UserControl

#Region "== Variables =========================================================="
    Private m_Grid As Grid
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        toolTip.SetToolTip(Me, "")

        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.DoubleBuffer, True)

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private WithEvents toolTip As System.Windows.Forms.ToolTip

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
        '
        'ContainerControl
        '
        Me.Name = "ContainerControl"

    End Sub

#End Region

#Region "== Properties ========================================================="
    Public Property Grid() As Grid
        Get
            Return m_Grid
        End Get
        Set(ByVal value As Grid)
            m_Grid = value
        End Set
    End Property

    Public Property ToolTipText() As String
        Get
            Return toolTip.GetToolTip(Me)
        End Get
        Set(ByVal value As String)
            toolTip.SetToolTip(Me, value)
        End Set
    End Property

    Public Property ToolTipActive() As Boolean
        Get
            Return toolTip.Active
        End Get
        Set(ByVal value As Boolean)
            toolTip.Active = value
        End Set
    End Property
#End Region

#Region "== Methods ============================================================"
    Protected Overrides Function IsInputKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Tab
                Return False
            Case (Keys.Tab Or Keys.Shift)
                Return True
            Case Else
                Return MyBase.IsInputKey(keyData)
        End Select
    End Function
#End Region

End Class
