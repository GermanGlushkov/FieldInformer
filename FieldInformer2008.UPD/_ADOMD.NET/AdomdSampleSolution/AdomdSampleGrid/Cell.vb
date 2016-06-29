Option Explicit On 

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms

'======================================================================
'
'  File:      Cell.vb
'  Summary:   Base implementation of ICellType interface.
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

Public Class Cell

#Region "== Constants =========================================================="
    Private Const c_MaxStringWidth As Integer = 4096
#End Region

#Region "== Variables =========================================================="
    Private m_SharedFormat As CellFormat
    Private m_Grid As Grid
    Private m_Row As Integer = -1
    Private m_Col As Integer = -1
    Private m_RowSpan As Integer = 1
    Private m_ColSpan As Integer = 1
    Private m_Value As Object = Nothing
    Private m_Tag As Object = Nothing
    Private m_CellType As ICellType = Nothing
    Private m_OldCursor As Cursor = Nothing
    Private m_OldToolTipText As String = Nothing
    Private m_ToolTipText As String = ""
#End Region

#Region "== Events ============================================================="
    Public Event Click As EventHandler
    Public Event DoubleClick As EventHandler
    Public Event Enter As EventHandler
    Public Event GridBind As EventHandler
    Public Event GridUnbind As EventHandler
    Public Event KeyDown As KeyEventHandler
    Public Event KeyPress As KeyPressEventHandler
    Public Event KeyUp As KeyEventHandler
    Public Event Leave As EventHandler
    Public Event MouseDown As MouseEventHandler
    Public Event MouseEnter As EventHandler
    Public Event MouseLeave As EventHandler
    Public Event MouseMove As MouseEventHandler
    Public Event MouseUp As MouseEventHandler
    Public Event SelectionChange As EventHandler
    Public Event ValueChanged As EventHandler
#End Region

#Region "== Constructors ======================================================="
    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(ByVal value As Object)
        Me.New(value, Nothing)
    End Sub

    Public Sub New(ByVal CellType As ICellType)
        Me.New(Nothing, CellType)
    End Sub

    Public Sub New(ByVal value As Object, ByVal CellType As ICellType)
        Me.New(value, CellType, Nothing)
    End Sub

    Public Sub New(ByVal type As Type, ByVal startValue As Object, ByVal defaultValue As Object)
        MyBase.New()
        m_CellType = CommonRoutines.CreateCellType(type, defaultValue)
        m_SharedFormat = CellFormat.DefaultFormat
        m_Value = startValue
    End Sub

    Public Sub New(ByVal type As Type, ByVal startValue As Object, ByVal defaultValue As Object, ByVal allowNull As Boolean)
        MyBase.New()
        m_CellType = CommonRoutines.CreateCellType(type, defaultValue, allowNull)
        m_SharedFormat = CellFormat.DefaultFormat
        m_Value = startValue
    End Sub

    Public Sub New(ByVal cellValue As Object, ByVal CellType As ICellType, ByVal cellFormat As CellFormat)
        MyBase.New()
        m_CellType = CellType

        If CellFormat Is Nothing Then
            m_SharedFormat = CellFormat.DefaultFormat
        Else
            m_SharedFormat = CellFormat
        End If

        m_Value = cellValue
    End Sub
#End Region

#Region "== Properties ========================================================="

    Public Property BackColor() As Color
        Get
            Return Format.BackColor
        End Get
        Set(ByVal value As Color)
            If Not (Format.BackColor.Equals(value)) Then
                Dim l_Temp As CellFormat = Format.Clone()
                l_Temp.BackColor = value
                Format = l_Temp
            End If
        End Set
    End Property
    Public Property Border() As CellBorder
        Get
            Return Format.Border
        End Get
        Set(ByVal value As CellBorder)
            If Not (Format.Border.Equals(value)) Then
                Dim l_Temp As CellFormat = CType(Format.Clone(), CellFormat)
                l_Temp.Border = value
                Format = l_Temp
            End If
        End Set
    End Property
    Public ReadOnly Property Col() As Integer
        Get
            Return m_Col
        End Get
    End Property
    Public Property ColSpan() As Integer
        Get
            Return m_ColSpan
        End Get
        Set(ByVal value As Integer)
            m_ColSpan = value
            InvokeInvalidate()
        End Set
    End Property
    Public Property StringFormat() As StringFormat
        Get
            Return Format.StringFormat
        End Get
        Set(ByVal value As StringFormat)
            If Not (Format.StringFormat Is value) Then
                Dim l_Temp As CellFormat = CType(Format.Clone(), CellFormat)
                l_Temp.StringFormat = value
                Format = l_Temp
            End If
        End Set
    End Property
    Public Property TextAlignment() As ContentAlignment
        Get
            Return Format.TextAlignment
        End Get
        Set(ByVal value As ContentAlignment)
            If (Format.TextAlignment <> value) Then
                Dim l_Temp As CellFormat = CType(Format.Clone(), CellFormat)
                l_Temp.TextAlignment = value
                Format = l_Temp
            End If
        End Set
    End Property
    Public Property Font() As Font
        Get
            Return Format.Font
        End Get
        Set(ByVal value As Font)
            If Not (Format.Font Is value) Then
                Dim l_Temp As CellFormat = CType(Format.Clone(), CellFormat)
                l_Temp.Font = value
                Format = l_Temp
            End If
        End Set
    End Property
    Public Property ForeColor() As Color
        Get
            Return Format.ForeColor
        End Get
        Set(ByVal value As Color)
            If Not (Format.ForeColor.Equals(value)) Then
                Dim l_Temp As CellFormat = CType(Format.Clone(), CellFormat)
                l_Temp.ForeColor = value
                Format = l_Temp
            End If
        End Set
    End Property
    Public Overridable ReadOnly Property Grid() As Grid
        Get
            Return m_Grid
        End Get
    End Property
    Public ReadOnly Property Row() As Integer
        Get
            Return m_Row
        End Get
    End Property
    Public Property RowSpan() As Integer
        Get
            Return m_RowSpan
        End Get
        Set(ByVal value As Integer)
            m_RowSpan = value
            InvokeInvalidate()
        End Set
    End Property
    Public Overridable ReadOnly Property DisplayText() As String
        Get
            Try
                Dim l_Temp As Object = Value
                If (m_CellType Is Nothing) Then
                    If l_Temp Is Nothing Then
                        Return ""
                    Else
                        Return l_Temp.ToString
                    End If
                Else
                    If m_CellType.SupportStringConversion Then
                        Return m_CellType.ObjectToString(l_Temp)
                    Else
                        Return m_CellType.GetStringRepresentation(l_Temp)
                    End If
                End If
            Catch ex As Exception
                Return "Error:" & ex.Message
            End Try
        End Get
    End Property
    Public Overridable Property Tag() As Object
        Get
            Return m_Tag
        End Get
        Set(ByVal value As Object)
            m_Tag = value
        End Set
    End Property
    <Browsable(False)> Public ReadOnly Property AbsoluteRectangle() As Rectangle
        Get
            If Not (m_Grid Is Nothing) Then
                Return m_Grid.GetCellAbsoluteRectangle(m_Row, m_Col, m_RowSpan, m_ColSpan)
            Else
                Return New Rectangle(0, 0, 0, 0)
            End If
        End Get
    End Property
    <Browsable(False)> Public ReadOnly Property DisplayRectangle() As Rectangle
        Get
            If Not (m_Grid Is Nothing) Then
                Return m_Grid.GetCellDisplayRectangle(m_Row, m_Col, m_RowSpan, m_ColSpan)
            Else
                Return New Rectangle(0, 0, 0, 0)
            End If
        End Get
    End Property
    Public Property CellType() As ICellType
        Get
            Return m_CellType
        End Get
        Set(ByVal value As ICellType)
            m_CellType = value
        End Set
    End Property
    Public Overridable Property ToolTipText() As String
        Get
            Return m_ToolTipText
        End Get
        Set(ByVal value As String)
            m_ToolTipText = value
        End Set
    End Property
    Public Overridable Property Value() As Object
        Get
            Return m_Value
        End Get
        Set(ByVal value As Object)
            If Not (m_Value Is value) Then
                m_Value = value
                OnValueChanged(EventArgs.Empty)
                InvokeInvalidate()
            End If
        End Set
    End Property
    <Browsable(False)> Public Overridable Property Format() As CellFormat
        Get
            Return m_SharedFormat
        End Get
        Set(ByVal value As CellFormat)
            If (value Is Nothing) Then
                Throw New ArgumentNullException("Format")
            Else
                m_SharedFormat = value
                InvokeInvalidate()
            End If
        End Set
    End Property
    Public Property WordWrap() As Boolean
        Get
            Return Format.WordWrap
        End Get
        Set(ByVal value As Boolean)
            If Not (Format.WordWrap <> value) Then
                Dim l_Temp As CellFormat = CType(Format.Clone(), CellFormat)
                l_Temp.WordWrap = value
                Format = l_Temp
            End If
        End Set
    End Property

#End Region

#Region "== Methods ============================================================"
    Public Sub SetAllBorderColor(ByVal color As Color)
        Format = CType(Format.Clone(), CellFormat)
        Format.Border = Format.Border.SetColor(color)
    End Sub

    Public Sub SetAllBorderWidth(ByVal width As Integer)
        Format = CType(Format.Clone(), CellFormat)
        Format.Border = Format.Border.SetWidth(width)
    End Sub

    Public Overridable Sub BindToGrid(ByVal grid As Grid, ByVal row As Integer, ByVal col As Integer)
        m_Grid = grid
        m_Row = row
        m_Col = col

        OnGridBind(EventArgs.Empty)
    End Sub

    Public Overridable Sub UnbindToGrid()
        If Not (m_Grid Is Nothing) Then
            OnGridUnbind(EventArgs.Empty)
        End If

        m_Grid = Nothing
        m_Row = -1
        m_Col = -1
    End Sub

    Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
        RaiseEvent ValueChanged(Me, e)
    End Sub

    Public Overrides Function ToString() As String
        Return Me.DisplayText
    End Function

    Public Overridable Function IsInDisplayRegion(ByVal rectangleToCheck As Rectangle) As Boolean
        Return DisplayRectangle.IntersectsWith(rectangleToCheck)
    End Function

    Public Overridable Function IsInAbsoluteRegion(ByVal rectangleToCheck As Rectangle) As Boolean
        Return AbsoluteRectangle.IntersectsWith(rectangleToCheck)
    End Function

    Protected Overridable Function GetCellFont() As Font
        Dim l_Font As Font = Format.Font

        If (l_Font Is Nothing) Then
            If (m_Grid Is Nothing) Then
                l_Font = New Font(FontFamily.GenericSansSerif, 8.25)
            Else
                l_Font = Grid.Font
            End If
        End If

        Return l_Font
    End Function

    Public Overridable Function GetRequiredSize(ByVal g As Graphics) As Size
        Dim l_Font As Font = GetCellFont()
        Dim l_ReqSize As SizeF
        Dim lvstrText As String = DisplayText

        If lvstrText <> "" Then
            l_ReqSize = g.MeasureString(lvstrText, l_Font, c_MaxStringWidth, Format.StringFormat)
            l_ReqSize.Width += 2
            l_ReqSize.Height += 2
        Else
            l_ReqSize = New SizeF(0, 0)
        End If


        l_ReqSize.Width += Format.Border.Left.Width + Format.Border.Right.Width
        l_ReqSize.Height += Format.Border.Top.Width + Format.Border.Bottom.Width

        l_ReqSize.Width = l_ReqSize.Width / ColSpan
        l_ReqSize.Height = l_ReqSize.Height / RowSpan

        Return l_ReqSize.ToSize
    End Function

    Public Overridable Sub InvokeMouseDown(ByVal e As MouseEventArgs)
        RaiseEvent MouseDown(Me, e)
    End Sub

    Public Overridable Sub InvokeMouseUp(ByVal e As MouseEventArgs)
        RaiseEvent MouseUp(Me, e)
    End Sub

    Public Overridable Sub InvokeMouseMove(ByVal e As MouseEventArgs)
        RaiseEvent MouseMove(Me, e)
    End Sub

    Protected Overridable Sub ApplyToolTipText()
        If ToolTipText <> "" Then
            m_OldToolTipText = m_Grid.ContainerControlToolTipText
            m_Grid.ContainerControlToolTipText = ToolTipText
        End If
    End Sub

    Protected Overridable Sub ResetToolTipText()
        If m_OldToolTipText <> "" Then
            m_Grid.ContainerControlToolTipText = m_OldToolTipText
            m_OldToolTipText = Nothing
        End If
    End Sub

    Public Overridable Sub InvokeMouseEnter(ByVal e As EventArgs)
        ApplyToolTipText()
        RaiseEvent MouseEnter(Me, e)
    End Sub

    Public Overridable Sub InvokeMouseLeave(ByVal e As EventArgs)
        ResetToolTipText()
        RaiseEvent MouseLeave(Me, e)
    End Sub

    Public Overridable Sub InvokeKeyUp(ByVal e As KeyEventArgs)
        RaiseEvent KeyUp(Me, e)
    End Sub

    Public Overridable Sub InvokeKeyPress(ByVal e As KeyPressEventArgs)
        RaiseEvent KeyPress(Me, e)
    End Sub

    Public Overridable Sub InvokeKeyDown(ByVal e As KeyEventArgs)
        RaiseEvent KeyDown(Me, e)
    End Sub


    Public Overridable Sub InvokeDoubleClick()
        RaiseEvent DoubleClick(Me, EventArgs.Empty)
    End Sub

    Public Overridable Sub InvokeClick()
        RaiseEvent Click(Me, EventArgs.Empty)
    End Sub

    Protected Overridable Sub OnGridBind(ByVal e As EventArgs)
        RaiseEvent GridBind(Me, e)
    End Sub

    Protected Overridable Sub OnGridUnbind(ByVal e As EventArgs)
        RaiseEvent GridUnbind(Me, e)
    End Sub

    Protected Sub OnSelectionChange(ByVal e As EventArgs)
        RaiseEvent SelectionChange(Me, e)
    End Sub

    Public Overridable Sub InvokeInvalidate()
        If Not (m_Grid Is Nothing) Then
            m_Grid.InvalidateCell(Me)
        End If
    End Sub

    Public Overridable Sub InvokePaint(ByVal e As PaintEventArgs, ByVal absoluteRectangle As Rectangle, ByVal checkIfIsRegion As Boolean)
        If m_Grid Is Nothing Then Exit Sub

        If (checkIfIsRegion = False Or IsInAbsoluteRegion(absoluteRectangle)) Then
            Dim g As Graphics = e.Graphics
            Dim l_DisplayRectangle As RectangleF = New RectangleF(DisplayRectangle.X, DisplayRectangle.Y, DisplayRectangle.Width, DisplayRectangle.Height)

            Dim l_PreviousClip As Region = g.Clip
            g.Clip = New Region(l_DisplayRectangle)

            Dim l_CurrentBackColor As Color
            Dim l_CurrentForeColor As Color
            Dim l_CurrentBorder As CellBorder

            l_CurrentBackColor = Format.BackColor
            l_CurrentForeColor = Format.ForeColor
            l_CurrentBorder = Format.Border

            Dim br As New SolidBrush(l_CurrentBackColor)
            g.FillRectangle(br, l_DisplayRectangle)
            br = Nothing

            PaintBorders(g, Rectangle.Truncate(l_DisplayRectangle), l_CurrentBorder)
            Dim l_CurrentFont As Font = GetCellFont()

            PaintContents(g, _
                l_DisplayRectangle, _
                DisplayText, _
                Format.StringFormat, _
                l_CurrentBorder.Left, _
                l_CurrentBorder.Top, _
                l_CurrentBorder.Right, _
                l_CurrentBorder.Bottom, _
                l_CurrentForeColor, _
                l_CurrentFont)

            g.Clip = l_PreviousClip
        End If
    End Sub

    Public Shared Sub PaintContents(ByVal g As Graphics, _
    ByVal displayRectangle As RectangleF, _
    ByVal textToPaint As String, _
    ByVal textFormat As StringFormat, _
    ByVal leftBorder As Border, _
    ByVal topBorder As Border, _
    ByVal rightBorder As Border, _
    ByVal bottomBorder As Border, _
    ByVal textColor As Color, _
    ByVal textFont As Font)

        Dim l_CellRectNoBorder As RectangleF = displayRectangle
        l_CellRectNoBorder.Y += topBorder.Width
        l_CellRectNoBorder.X += leftBorder.Width
        l_CellRectNoBorder.Width -= leftBorder.Width
        l_CellRectNoBorder.Width -= rightBorder.Width
        l_CellRectNoBorder.Height -= topBorder.Width
        l_CellRectNoBorder.Height -= bottomBorder.Width

        If textToPaint <> "" Then
            Dim textBrush As New SolidBrush(textColor)
            Dim l_RectDrawText As RectangleF = l_CellRectNoBorder

            If (l_CellRectNoBorder.Width > 0) AndAlso (l_CellRectNoBorder.Height > 0) Then
                g.DrawString(textToPaint, textFont, textBrush, l_RectDrawText, textFormat)
            End If

            textBrush = Nothing
        End If
    End Sub

    Public Shared Sub PaintBorders(ByVal g As Graphics, ByVal displayRectangle As Rectangle, ByVal border As CellBorder)
        ControlPaint.DrawBorder(g, displayRectangle, _
            border.Left.Color, _
            CInt(border.Left.Width), _
            ButtonBorderStyle.Solid, _
            border.Top.Color, _
            CInt(border.Top.Width), _
            ButtonBorderStyle.Solid, _
            border.Right.Color, _
            CInt(border.Right.Width), _
            ButtonBorderStyle.Solid, _
            border.Bottom.Color, _
            CInt(border.Bottom.Width), _
            ButtonBorderStyle.Solid)
    End Sub

    Public Shared Sub FormatBorder(ByVal cell As Cell, ByVal style As CommonBorderStyle, ByVal width As Single, _
        ByVal shadowColor As Color, ByVal lightColor As Color)

        Dim l_Prop As CellFormat = CType(cell.Format.Clone(), CellFormat)
        Dim l_Border As New CellBorder(New Border(Color.White))

        If (style = Enums.CommonBorderStyle.Inset) Then
            With l_Border
                .Top = New Border(shadowColor, width)
                .Left = New Border(shadowColor, width)
                .Bottom = New Border(lightColor, width)
                .Right = New Border(lightColor, width)
            End With
        ElseIf (style = Enums.CommonBorderStyle.Raised) Then
            With l_Border
                .Top = New Border(lightColor, width)
                .Left = New Border(lightColor, width)
                .Bottom = New Border(shadowColor, width)
                .Right = New Border(shadowColor, width)
            End With
        Else
            With l_Border
                .Top = New Border(shadowColor, width)
                .Left = New Border(shadowColor, width)
                .Bottom = New Border(shadowColor, width)
                .Right = New Border(shadowColor, width)
            End With
        End If

        l_Prop.Border = l_Border
        cell.Format = l_Prop
    End Sub

    Protected Shared Function GetObjAlignment(ByVal align As ContentAlignment, _
        ByVal clientLeft As Integer, _
        ByVal clientTop As Integer, _
        ByVal clientWidth As Integer, _
        ByVal clientHeight As Integer, _
        ByVal objWidth As Single, _
        ByVal objHeight As Single) As PointF

        Dim l_PointF As New PointF(CType(clientLeft, Single), CType(clientTop, Single))
        If CommonRoutines.IsTop(align) Then
            l_PointF.Y = CType(clientTop, Single)
        ElseIf CommonRoutines.IsBottom(align) Then
            l_PointF.Y = CType(clientTop, Single) + CType(clientHeight, Single) - objHeight
        Else
            l_PointF.Y = CType(clientTop, Single) + (CType(clientHeight, Single) / 2.0) - (objHeight / 2)
        End If

        If CommonRoutines.IsCenter(align) Then
            l_PointF.X = CType(clientLeft, Single) + (CType(clientWidth, Single) / 2.0) - (objWidth / 2)
        ElseIf CommonRoutines.IsRight(align) Then
            l_PointF.X = CType(clientLeft, Single) + CType(clientWidth, Single) - objWidth
        End If

        Return l_PointF
    End Function
#End Region

End Class
