Option Explicit On 

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Windows.Forms

'======================================================================
'
'  File:      CellFormat.vb
'  Summary:   Encapsulated object for storing visual properties
'             associated with a given ICellType object.
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

<Serializable()> _
Public Class CellFormat
    Implements ICloneable

#Region "== Variables =========================================================="
    ' Property values
    Private m_BackColor As Color
    Private m_ForeColor As Color
    Private m_SelectionBackColor As Color
    Private m_SelectionForeColor As Color
    Private m_FocusBackColor As Color
    Private m_FocusForeColor As Color
    Private m_Font As Font
    Private m_StringFormat As StringFormat = StringFormat.GenericDefault
    Private m_Image As Image
    Private m_ImageAlignment As ContentAlignment
    Private m_ImageStretch As Boolean
    Private m_AlignTextToImage As Boolean
    Private m_Cursor As Cursor
    Private m_Border As CellBorder
    Private m_FocusBorder As CellBorder
    Private m_SelectionBorder As CellBorder
#End Region

#Region "== Events ============================================================="
    Public Event Change()
#End Region

#Region "== Constructors ======================================================="
    Public Sub New()
        MyBase.New()
        m_Cursor = Nothing
        m_BackColor = Color.FromKnownColor(KnownColor.Window)
        m_ForeColor = Color.FromKnownColor(KnownColor.WindowText)
        m_SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
        m_SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
        m_FocusBackColor = ControlPaint.LightLight(m_SelectionBackColor)
        m_FocusForeColor = m_ForeColor
        m_Font = Nothing
        m_Image = Nothing
        m_ImageAlignment = ContentAlignment.MiddleLeft
        m_ImageStretch = False
        m_AlignTextToImage = True
        m_StringFormat = StringFormat.GenericDefault.Clone
        m_Border = New CellBorder(New Border(Color.LightGray, 1), New Border(Color.LightGray, 1))
        m_FocusBorder = m_Border
        m_SelectionBorder = m_Border
    End Sub

    Public Sub New( _
        ByVal backColor As Color, _
        ByVal foreColor As Color, _
        ByVal selectionBackColor As Color, _
        ByVal selectionForeColor As Color, _
        ByVal focusBackColor As Color, _
        ByVal focusForeColor As Color, _
        ByVal font As Font, _
        ByVal stringFormat As StringFormat, _
        ByVal image As Image, _
        ByVal imageAlignment As ContentAlignment, _
        ByVal ImageStretch As Boolean, _
        ByVal alignTextToImage As Boolean, _
        ByVal cursor As Cursor, _
        ByVal border As CellBorder, _
        ByVal focusBorder As CellBorder, _
        ByVal selectionBorder As CellBorder)

        MyBase.New()
        m_BackColor = backColor
        m_ForeColor = foreColor
        m_SelectionBackColor = selectionBackColor
        m_SelectionForeColor = selectionForeColor
        m_FocusBackColor = focusBackColor
        m_FocusForeColor = focusForeColor
        m_Font = font
        m_StringFormat = stringFormat
        m_Image = image
        m_ImageAlignment = imageAlignment
        m_ImageStretch = ImageStretch
        m_AlignTextToImage = alignTextToImage
        m_Cursor = cursor
        m_Border = border
        m_FocusBorder = focusBorder
        m_SelectionBorder = selectionBorder

    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Shared ReadOnly DefaultFormat As New CellFormat()

    <System.ComponentModel.Browsable(False)> _
Public Property StringFormat() As StringFormat
        Get
            Return m_StringFormat
        End Get
        Set(ByVal value As StringFormat)
            m_StringFormat = value
            OnChange()
        End Set
    End Property

    Public Property Font() As Font
        Get
            Return m_Font
        End Get
        Set(ByVal value As Font)
            m_Font = value
            OnChange()
        End Set
    End Property

    Public Property BackColor() As Color
        Get
            Return m_BackColor
        End Get
        Set(ByVal value As Color)
            m_BackColor = value
            OnChange()
        End Set
    End Property

    Public Property ForeColor() As Color
        Get
            Return m_ForeColor
        End Get
        Set(ByVal value As Color)
            m_ForeColor = value
            OnChange()
        End Set
    End Property

    Public Property SelectionForeColor() As Color
        Get
            Return m_SelectionForeColor
        End Get
        Set(ByVal value As Color)
            m_SelectionForeColor = value
            OnChange()
        End Set
    End Property

    Public Property SelectionBackColor() As Color
        Get
            Return m_SelectionBackColor
        End Get
        Set(ByVal value As Color)
            m_SelectionBackColor = value
            OnChange()
        End Set
    End Property

    Public Property FocusForeColor() As Color
        Get
            Return m_FocusForeColor
        End Get
        Set(ByVal value As Color)
            m_FocusForeColor = value
            OnChange()
        End Set
    End Property

    Public Property FocusBackColor() As Color
        Get
            Return m_FocusBackColor
        End Get
        Set(ByVal value As Color)
            m_FocusBackColor = value
            OnChange()
        End Set
    End Property

    Public Property Border() As CellBorder
        Get
            Return m_Border
        End Get
        Set(ByVal value As CellBorder)
            m_Border = value
            OnChange()
        End Set
    End Property

    Public Property FocusBorder() As CellBorder
        Get
            Return m_FocusBorder
        End Get
        Set(ByVal value As CellBorder)
            m_FocusBorder = value
            OnChange()
        End Set
    End Property

    Public Property SelectionBorder() As CellBorder
        Get
            Return m_SelectionBorder
        End Get
        Set(ByVal value As CellBorder)
            m_SelectionBorder = value
            OnChange()
        End Set
    End Property

    Public Property WordWrap() As Boolean
        Get
            If ((StringFormat.FormatFlags And StringFormatFlags.NoWrap) = StringFormatFlags.NoWrap) Then
                Return False
            Else
                Return True
            End If
        End Get
        Set(ByVal value As Boolean)
            If (value AndAlso WordWrap = False) Then
                StringFormat.FormatFlags = StringFormat.FormatFlags Xor StringFormatFlags.NoWrap
            Else
                StringFormat.FormatFlags = StringFormat.FormatFlags Or StringFormatFlags.NoWrap
            End If
        End Set
    End Property

    Public Property TextAlignment() As ContentAlignment
        Get
            If (CommonRoutines.IsBottom(StringFormat) AndAlso CommonRoutines.IsLeft(StringFormat)) Then
                Return ContentAlignment.BottomLeft
            ElseIf (CommonRoutines.IsBottom(StringFormat) AndAlso CommonRoutines.IsRight(StringFormat)) Then
                Return ContentAlignment.BottomRight
            ElseIf (CommonRoutines.IsBottom(StringFormat) AndAlso CommonRoutines.IsCenter(StringFormat)) Then
                Return ContentAlignment.BottomCenter

            ElseIf (CommonRoutines.IsTop(StringFormat) AndAlso CommonRoutines.IsLeft(StringFormat)) Then
                Return ContentAlignment.TopLeft
            ElseIf (CommonRoutines.IsTop(StringFormat) AndAlso CommonRoutines.IsRight(StringFormat)) Then
                Return ContentAlignment.TopRight
            ElseIf (CommonRoutines.IsTop(StringFormat) AndAlso CommonRoutines.IsCenter(StringFormat)) Then
                Return ContentAlignment.TopCenter

            ElseIf (CommonRoutines.IsMiddle(StringFormat) AndAlso CommonRoutines.IsLeft(StringFormat)) Then
                Return ContentAlignment.MiddleLeft
            ElseIf (CommonRoutines.IsMiddle(StringFormat) AndAlso CommonRoutines.IsRight(StringFormat)) Then
                Return ContentAlignment.MiddleRight
            Else
                Return ContentAlignment.MiddleCenter
            End If
        End Get
        Set(ByVal value As ContentAlignment)
            If (CommonRoutines.IsBottom(value)) Then
                StringFormat.LineAlignment = StringAlignment.Far
            ElseIf (CommonRoutines.IsTop(value)) Then
                StringFormat.LineAlignment = StringAlignment.Near
            Else
                StringFormat.LineAlignment = StringAlignment.Center
            End If

            If (CommonRoutines.IsLeft(value)) Then
                StringFormat.Alignment = StringAlignment.Near
            ElseIf (CommonRoutines.IsRight(value)) Then
                StringFormat.Alignment = StringAlignment.Far
            Else
                StringFormat.Alignment = StringAlignment.Center
            End If
        End Set
    End Property
#End Region

#Region "== Methods ============================================================"
    Protected Overridable Sub OnChange()
        RaiseEvent Change()
    End Sub

    Public Overloads Function Clone() As Object Implements System.ICloneable.Clone
        Dim l_tmpImage As Image = Nothing
        If Not (m_Image Is Nothing) Then
            l_tmpImage = CommonRoutines.ImageClone(m_Image)
        End If
        Dim l_tmpFont As Font = Nothing
        If Not (m_Font Is Nothing) Then
            l_tmpFont = m_Font.Clone()
        End If
        Dim l_tmpStringFormat As StringFormat = Nothing
        If Not (m_StringFormat Is Nothing) Then
            l_tmpStringFormat = m_StringFormat.Clone
        End If

        Return New CellFormat(m_BackColor, _
            m_ForeColor, _
            m_SelectionBackColor, _
            m_SelectionForeColor, _
            m_FocusBackColor, _
            m_FocusForeColor, _
            l_tmpFont, _
            l_tmpStringFormat, _
            l_tmpImage, _
            m_ImageAlignment, _
            m_ImageStretch, _
            m_AlignTextToImage, _
            m_Cursor, _
            m_Border, _
            m_FocusBorder, _
            m_SelectionBorder)

    End Function

#End Region

End Class
