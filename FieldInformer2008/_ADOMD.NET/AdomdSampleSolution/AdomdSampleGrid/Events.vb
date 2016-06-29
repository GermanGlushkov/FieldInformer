Option Explicit On 

'======================================================================
'
'  File:      Events.vb
'  Summary:   Delegate and event argument definitions used 
'             throughout AdomdSampleGrid.
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

Public Delegate Sub ValidatingEventHandler(ByVal sender As Object, ByVal e As ValidatingEventArgs)

Public Delegate Sub ValidatingCellEventHandler(ByVal sender As Object, ByVal e As ValidatingCellEventArgs)

Public Delegate Sub CellEventHandler(ByVal sender As Object, ByVal e As CellEventArgs)

Public Delegate Sub ScrollPositionChangedEventHandler(ByVal sender As Object, ByVal e As ScrollPositionChangedEventArgs)

Public Class ScrollPositionChangedEventArgs
    Inherits EventArgs

#Region "== Variables =========================================================="
    Private m_NewValue As Integer
    Private m_OldValue As Integer
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal newValue As Integer, ByVal oldValue As Integer)
        m_NewValue = newValue
        m_OldValue = oldValue
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public ReadOnly Property NewValue() As Integer
        Get
            Return m_NewValue
        End Get
    End Property

    Public ReadOnly Property OldValue() As Integer
        Get
            Return m_OldValue
        End Get
    End Property

    Public ReadOnly Property Delta() As Integer
        Get
            Return (m_OldValue - m_NewValue)
        End Get
    End Property
#End Region

End Class

Public Class RowEventArgs
    Inherits EventArgs

#Region "== Variables =========================================================="
    Private m_Row As Integer
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal row As Integer)
        m_Row = row
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Property Row() As Integer
        Get
            Return m_Row
        End Get
        Set(ByVal value As Integer)
            m_Row = value
        End Set
    End Property
#End Region

End Class

Public Class ColumnEventArgs
    Inherits EventArgs

#Region "== Variables =========================================================="
    Private m_Column As Integer
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal column As Integer)
        m_Column = column
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Property Column() As Integer
        Get
            Return m_Column
        End Get
        Set(ByVal value As Integer)
            m_Column = value
        End Set
    End Property
#End Region

End Class

Public Class ValidatingEventArgs
    Inherits System.ComponentModel.CancelEventArgs

#Region "== Variables =========================================================="
    Private m_NewValue As Object
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal newValue As Object)
        m_NewValue = newValue
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Property NewValue() As Object
        Get
            Return m_NewValue
        End Get
        Set(ByVal value As Object)
            m_NewValue = value
        End Set
    End Property
#End Region

End Class

Public Class ValidatingCellEventArgs
    Inherits ValidatingEventArgs

#Region "== Variables =========================================================="
    Private m_Cell As Cell
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal cell As Cell, ByVal newValue As Object)
        MyBase.New(newValue)
        m_Cell = cell
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public ReadOnly Property Cell() As Cell
        Get
            Return m_Cell
        End Get
    End Property
#End Region

End Class

Public Class CellEventArgs
    Inherits EventArgs

#Region "== Variables =========================================================="
    Private m_Cell As Cell
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal cell As Cell)
        m_Cell = cell
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Property Cell() As Cell
        Get
            Return m_Cell
        End Get
        Set(ByVal value As Cell)
            m_Cell = value
        End Set
    End Property

#End Region

End Class

Public Class CellArrayEventArgs
    Inherits EventArgs

#Region "== Variables =========================================================="
    Private m_Cells() As Cell
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal cell() As Cell)
        m_Cells = cell
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Property Cells() As Cell()
        Get
            Return m_Cells
        End Get
        Set(ByVal value As Cell())
            m_Cells = value
        End Set
    End Property
#End Region

End Class
