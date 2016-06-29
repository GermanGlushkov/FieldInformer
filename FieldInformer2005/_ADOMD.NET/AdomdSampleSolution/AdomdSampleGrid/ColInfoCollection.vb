Option Explicit On 

'======================================================================
'
'  File:      ColInfoCollection.vb
'  Summary:   ICollection implementation of ColInfo collection.
'             IComparer implementation for comparing leftmost
'             ColInfo objects.
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

Public Class ColInfoCollection
    Implements ICollection

#Region "== Variables =========================================================="
    Private m_List As New ArrayList
    Private m_Comparer As New ColInfoLeftComparer
#End Region

#Region "== Properties ========================================================="
    Default Public ReadOnly Property Item(ByVal p As Integer) As ColInfo
        Get
            Return CType(m_List.Item(p), ColInfo)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer Implements ICollection.Count
        Get
            Return m_List.Count
        End Get
    End Property

    Public ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
        Get
            Return m_List.IsSynchronized
        End Get
    End Property

    Public ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
        Get
            Return m_List.SyncRoot
        End Get
    End Property
#End Region

#Region "== Methods ============================================================"
    Public Function GetColAtPoint(ByVal x As Integer) As Integer
        Dim l_Find As New ColInfo(0, x)
        Dim l_Found As Integer = m_List.BinarySearch(l_Find, m_Comparer)
        If l_Found > 0 Then
            Return l_Found
        Else
            l_Found = Not l_Found
            If l_Found <= 0 Then
                Return -1
            ElseIf l_Found <= m_List.Count Then
                Return l_Found - 1
            Else
                Return -1
            End If
        End If
    End Function

    Public Function Add(ByVal p As ColInfo) As Integer
        Return m_List.Add(p)
    End Function

    Public Function Add(ByVal index As Integer, ByVal p As ColInfo) As Integer
        m_List.Insert(index, p)
        Return index
    End Function

    Public Sub RemoveAt(ByVal index As Integer)
        m_List.RemoveAt(index)
    End Sub

    Public Overridable Overloads Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
        m_List.CopyTo(array, index)
    End Sub

    Public Overridable Overloads Sub CopyTo(ByVal array As ColInfoCollection, ByVal index As Integer)
        m_List.CopyTo(CType(array, ICollection), index)
    End Sub

    Public Overridable Function GetEnumerator() As IEnumerator Implements ICollection.GetEnumerator
        Return m_List.GetEnumerator
    End Function
#End Region

End Class

Public Class ColInfoLeftComparer
    Implements IComparer

#Region "== Methods ============================================================"
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Return CType(x, ColInfo).Left.CompareTo(CType(y, ColInfo).Left)
    End Function
#End Region

End Class
