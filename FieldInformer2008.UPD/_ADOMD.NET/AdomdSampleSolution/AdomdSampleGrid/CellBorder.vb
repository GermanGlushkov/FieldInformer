Option Explicit On 

'======================================================================
'
'  File:      CellBorder.vb
'  Summary:   Structure used to represent border settings for a cell.
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

Public Structure Border

#Region "== Variables =========================================================="
    Private mvsngWidth As Single
    Private m_Color As Color
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal color As Color)
        m_Color = color
        mvsngWidth = 1
    End Sub

    Public Sub New(ByVal color As Color, ByVal width As Single)
        m_Color = color
        mvsngWidth = width
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Property Width() As Single
        Get
            Return mvsngWidth
        End Get
        Set(ByVal value As Single)
            mvsngWidth = value
        End Set
    End Property

    Public Property Color() As Color
        Get
            Return m_Color
        End Get
        Set(ByVal value As Color)
            m_Color = value
        End Set
    End Property
#End Region

#Region "== Methods ============================================================"
    Public Overrides Function ToString() As String
        Return m_Color.ToString + ", Width=" & mvsngWidth.ToString
    End Function

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If (obj Is Nothing) Then
            Return False
        ElseIf (obj.GetType Is Me.GetType) Then
            Return False
        End If

        If obj.Width = mvsngWidth AndAlso CType(obj.Color, Color).Equals(m_Color) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return m_Color.GetHashCode
    End Function
#End Region

End Structure

Public Structure CellBorder

#Region "== Variables =========================================================="
    Private m_Top As Border
    Private m_Bottom As Border
    Private m_Left As Border
    Private m_Right As Border
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal border As Border)
        m_Top = border
        m_Bottom = border
        m_Left = border
        m_Right = border
    End Sub

    Public Sub New(ByVal right As Border, ByVal bottom As Border)
        m_Right = right
        m_Bottom = bottom
        m_Top = New Border(Color.White, 0)
        m_Left = New Border(Color.White, 0)
    End Sub

    Public Sub New(ByVal top As Border, ByVal bottom As Border, ByVal left As Border, ByVal right As Border)
        m_Top = top
        m_Bottom = bottom
        m_Left = left
        m_Right = right
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Property Top() As Border
        Get
            Return m_Top
        End Get
        Set(ByVal value As Border)
            m_Top = value
        End Set
    End Property

    Public Property Bottom() As Border
        Get
            Return m_Bottom
        End Get
        Set(ByVal value As Border)
            m_Bottom = value
        End Set
    End Property

    Public Property Left() As Border
        Get
            Return m_Left
        End Get
        Set(ByVal value As Border)
            m_Left = value
        End Set
    End Property

    Public Property Right() As Border
        Get
            Return m_Right
        End Get
        Set(ByVal value As Border)
            m_Right = value
        End Set
    End Property
#End Region

#Region "== Methods ============================================================"
    Public Overrides Function ToString() As String
        Return "Top:" & m_Top.ToString & " Bottom:" & m_Bottom.ToString & " Left:" & m_Left.ToString & " Right:" & m_Right.ToString
    End Function

    Public Function SetColor(ByVal color As Color) As CellBorder
        Dim l_Return As CellBorder = Me

        With l_Return
            .Top = New Border(color, m_Top.Width)
            .Bottom = New Border(color, m_Bottom.Width)
            .Left = New Border(color, m_Left.Width)
            .Right = New Border(color, m_Right.Width)
        End With

        Return l_Return
    End Function

    Public Function SetWidth(ByVal width As Integer) As CellBorder
        Dim l_Return As CellBorder = Me

        With l_Return
            .Top = New Border(m_Top.Color, width)
            .Bottom = New Border(m_Bottom.Color, width)
            .Left = New Border(m_Left.Color, width)
            .Right = New Border(m_Right.Color, width)
        End With

        Return l_Return
    End Function

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If (obj Is Nothing) Then
            Return False
        ElseIf (obj.GetType Is Me.GetType) Then
            Return False
        End If

        If m_Left.Equals(obj.Left) AndAlso _
            m_Right.Equals(obj.Right) AndAlso _
            m_Top.Equals(obj.Top) AndAlso _
            m_Bottom.Equals(obj.Bottom) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Overrides Function GetHashCode() As Integer
        Return m_Left.GetHashCode
    End Function
#End Region

End Structure
