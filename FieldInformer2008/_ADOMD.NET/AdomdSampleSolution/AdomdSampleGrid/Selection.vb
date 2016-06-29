Option Explicit On 

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Windows.Forms
Imports System.Runtime.Serialization

'======================================================================
'
'  File:      Selection.vb
'  Summary:   ICollection implementation of a collection of Cell objects
'             that represent selected cells on a Grid control.
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

Public Class Selection
    Implements ICollection

#Region "== Variables =========================================================="
    Private m_List As New ArrayList
    Private m_Grid As Grid
    Private m_SelMode As GridSelectionMode = Enums.GridSelectionMode.Cell
#End Region

#Region "== Events ============================================================="
    Public Event SelectionChange As EventHandler
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal grid As Grid)
        m_Grid = grid
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public ReadOnly Property Grid() As Grid
        Get
            Return m_Grid
        End Get
    End Property

    Default Public ReadOnly Property Item(ByVal index As Integer) As Cell
        Get
            Return CType(m_List.Item(index), Cell)
        End Get
    End Property

    Public Property GridSelectionMode() As GridSelectionMode
        Get
            Return m_SelMode
        End Get
        Set(ByVal value As GridSelectionMode)
            m_SelMode = value
        End Set
    End Property

    Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
        Get
            Return m_List.IsSynchronized
        End Get
    End Property

    Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
        Get
            Return m_List.Count
        End Get
    End Property

    Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
        Get
            Return m_List.SyncRoot
        End Get
    End Property
#End Region

#Region "== Methods ============================================================"
    Public Sub Clear(ByVal selectedCell As Cell)
        Dim l_Count As Integer = Count
        Dim l_IndexToDel As Integer = 0
        Dim l_ClearApply As Boolean = False
        Dim i As Integer

        For i = 0 To l_Count - 1
            If Not (Me.Item(l_IndexToDel) Is selectedCell) Then
                RemoveAt(l_IndexToDel)
                l_ClearApply = True
            Else
                l_IndexToDel += 1
            End If
        Next

        If l_ClearApply Then
            OnSelectionChange(EventArgs.Empty)
        End If
    End Sub

    Public Function Contains(ByVal cell As Cell) As Boolean
        Return m_List.Contains(cell)
    End Function


    Public Overridable Overloads Sub CopyTo(ByVal array As Selection, ByVal index As Integer)
        m_List.CopyTo(CType(array, ICollection), index)
    End Sub

    Public Function Add(ByVal cell As Cell) As Integer
        Dim l_RetIndex As Integer
        If (Contains(cell) = False) Then
            Dim l_Index As Integer = m_List.Add(cell)
            cell.InvokeInvalidate()
            l_RetIndex = l_Index

            OnSelectionChange(EventArgs.Empty)
        Else
            l_RetIndex = IndexOf(cell)
        End If

        Return l_RetIndex
    End Function

    Public Sub Remove(ByVal cell As Cell)
        If (Contains(cell)) Then
            m_List.Remove(cell)
            cell.InvokeInvalidate()

            OnSelectionChange(EventArgs.Empty)
        End If
    End Sub

    Public Sub RemoveAt(ByVal index As Integer)
        Remove(Me.Item(index))
    End Sub

    Public Sub Clear()
        Dim l_Temp(Count) As Cell
        Dim l_Counter As Integer

        m_List.CopyTo(l_Temp, 0)
        m_List.Clear()
        For l_Counter = 0 To l_Temp.Length - 1
            l_Temp(l_Counter).InvokeInvalidate()
            OnSelectionChange(EventArgs.Empty)
        Next
    End Sub

    Public Function IndexOf(ByVal cell As Cell) As Integer
        Return m_List.IndexOf(cell)
    End Function

    Protected Overridable Sub OnSelectionChange(ByVal e As EventArgs)
        RaiseEvent SelectionChange(Me, e)
    End Sub

    Public Sub GetSelectionCorner(ByRef row1 As Integer, ByRef col1 As Integer, ByRef row2 As Integer, ByRef col2 As Integer)
        row1 = Integer.MaxValue
        col1 = Integer.MaxValue
        row2 = Integer.MaxValue
        col2 = Integer.MaxValue

        Dim l_Cell As Cell
        For Each l_Cell In Me
            If row1 > l_Cell.Row Then row1 = l_Cell.Row
            If col1 > l_Cell.Col Then col1 = l_Cell.Col
            If row2 < l_Cell.Row Then row2 = l_Cell.Row
            If col2 < l_Cell.Col Then col2 = l_Cell.Col
        Next
    End Sub

    Public Sub ClearValues()
        Try
            Dim l_Cell As Cell
            For Each l_Cell In Me
                If Not (l_Cell.CellType Is Nothing) Then l_Cell.CellType.ClearCell(l_Cell)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, , "Clear Error")
            Throw ex
        End Try
    End Sub

    Public Overloads Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
        m_List.CopyTo(array, index)
    End Sub

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return m_List.GetEnumerator
    End Function
#End Region

End Class

