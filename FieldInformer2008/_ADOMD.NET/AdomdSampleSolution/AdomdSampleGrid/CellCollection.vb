Option Explicit On 

'======================================================================
'
'  File:      CellCollection.vb
'  Summary:   CollectionBase implementation of cell collection.
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
Public Class CellCollection
    Inherits CollectionBase

#Region "== Properties ========================================================="
    Default Public ReadOnly Property Item(ByVal index As Integer) As Cell
        Get
            Return CType(MyBase.InnerList(index), Cell)
        End Get
    End Property
#End Region

#Region "== Methods ============================================================"
    Public Overridable Overloads Sub Insert(ByVal index As Integer, ByVal value As Cell)
        MyBase.InnerList.Insert(index, value)
    End Sub

    Public Overridable Overloads Sub CopyTo(ByVal array As CellCollection, ByVal index As Integer)
        MyBase.InnerList.CopyTo(CType(array, ICollection), index)
    End Sub

    Public Overridable Overloads Function IndexOf(ByVal value As Cell) As Integer
        Return MyBase.InnerList.IndexOf(value)
    End Function

    Public Function Add(ByVal cell As Cell) As Integer
        Return MyBase.InnerList.Add(cell)
    End Function

    Public Sub Remove(ByVal cell As Cell)
        MyBase.InnerList.Remove(cell)
    End Sub

    Public Function Contains(ByVal cell As Cell) As Boolean
        Dim l_Cell As cell

        For Each l_Cell In MyBase.InnerList
            If l_Cell.Equals(cell) Then
                Return True
            End If
        Next

        Return False
    End Function
#End Region

End Class
