Option Explicit On 

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms
Imports Adomd = Microsoft.AnalysisServices.AdomdClient

'======================================================================
'
'  File:      Grid.vb
'  Summary:   User control that represents a grid in AdomdSampleGrid.
'             Primary control for AdomdSample.
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

Public Class Grid
    Inherits AdomdSampleGrid.ScrollableControl

#Region "== Constants =========================================================="
    Private Const DefaultHeight As Long = 20
    Private Const DefaultWidth As Long = 50
#End Region

#Region "== Variables =========================================================="
    Private m_Cols As Integer = 0
    Private m_Rows As Integer = 0
    Private m_FixedRows As Integer = 0
    Private m_FixedCols As Integer = 0
    Private m_Cells(,) As Cell
    Private m_RowsInfo As New RowInfoCollection
    Private m_ColsInfo As New ColInfoCollection
    Private m_bRedraw As Boolean = True
    Private m_bEnableRowColSpan As Boolean = True
    Private m_EnableMouseSelection As Boolean = True
    Private m_CellSet As Adomd.CellSet = Nothing
    Private m_AutoSizeMinHeight As Integer = 10
    Private m_AutoSizeMinWidth As Integer = 10
    Private mvblnAutoStretchColumnsToFitWidth As Boolean = False
    Private m_MouseDownCell As Cell = Nothing

#End Region

#Region "== Events ============================================================="
    Public Event SelectionChange As EventHandler
    Public Event CellFocusChange As CellEventHandler
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_ContainerControl.Grid = Me

        AddHandler m_ContainerControl.Paint, New PaintEventHandler(AddressOf BackContainer_Paint)
    End Sub

    'UserControl1 overrides dispose to clean up the component list.
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
    Friend WithEvents imageListCommon As System.Windows.Forms.ImageList
    Friend WithEvents m_ContainerControl As AdomdSampleGrid.ContainerControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.imageListCommon = New System.Windows.Forms.ImageList(Me.components)
        Me.m_ContainerControl = New AdomdSampleGrid.ContainerControl()
        Me.SuspendLayout()
        '
        'imageListCommon
        '
        Me.imageListCommon.ImageSize = New System.Drawing.Size(16, 16)
        Me.imageListCommon.TransparentColor = System.Drawing.Color.Transparent
        '
        'm_ContainerControl
        '
        Me.m_ContainerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_ContainerControl.Grid = Nothing
        Me.m_ContainerControl.Location = New System.Drawing.Point(0, 0)
        Me.m_ContainerControl.Name = "m_ContainerControl"
        Me.m_ContainerControl.TabIndex = 0
        Me.m_ContainerControl.ToolTipActive = True
        Me.m_ContainerControl.ToolTipText = ""
        '
        'Grid
        '
        Me.Controls.Add(Me.m_ContainerControl)
        Me.Name = "Grid"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "== Properties ========================================================="
    Public Property AutoSizeMinHeight() As Integer
        Get
            Return m_AutoSizeMinHeight
        End Get
        Set(ByVal value As Integer)
            m_AutoSizeMinHeight = value
        End Set
    End Property

    Public Property AutoSizeMinWidth() As Integer
        Get
            Return m_AutoSizeMinWidth
        End Get
        Set(ByVal value As Integer)
            m_AutoSizeMinWidth = value
        End Set
    End Property

    Public Property AutoStretchColumnsToFitWidth() As Boolean
        Get
            Return mvblnAutoStretchColumnsToFitWidth
        End Get
        Set(ByVal value As Boolean)
            mvblnAutoStretchColumnsToFitWidth = value
        End Set
    End Property

    Public ReadOnly Property ContainerControl() As ContainerControl
        Get
            Return m_ContainerControl
        End Get
    End Property

    Public Property Cols() As Integer
        Get
            Return m_Cols
        End Get
        Set(ByVal value As Integer)
            If m_Cols < value Then
                AddColumn(m_Cols, value - m_Cols)
            ElseIf m_Cols > value Then
                RemoveColumn(value, m_Cols - value)
            End If
        End Set
    End Property

    Public Property Rows() As Integer
        Get
            Return m_Rows
        End Get
        Set(ByVal value As Integer)
            If m_Rows < value Then
                AddRow(m_Rows, value - m_Rows)
            ElseIf m_Rows > value Then
                RemoveRow(value, m_Rows - value)
            End If
        End Set
    End Property

    Public Overridable Property FixedRows() As Integer
        Get
            Return m_FixedRows
        End Get
        Set(ByVal value As Integer)
            m_FixedRows = value
        End Set
    End Property

    Public Overridable Property FixedCols() As Integer
        Get
            Return m_FixedCols
        End Get
        Set(ByVal value As Integer)
            m_FixedCols = value
        End Set
    End Property

    Default Public Property Cell(ByVal rowIndex As Integer, ByVal colIndex As Integer) As Cell
        Get
            Return m_Cells(rowIndex, colIndex)
        End Get
        Set(ByVal value As Cell)
            InsertCell(rowIndex, colIndex, value)
        End Set
    End Property

    Public Property CellSet() As Adomd.CellSet
        Get
            Return m_CellSet
        End Get
        Set(ByVal value As Adomd.CellSet)
            If Not (m_CellSet Is value) Then
                m_CellSet = value
                LoadCellSet()
            End If
        End Set
    End Property

    Public Property GridScrollPosition() As Point
        Get
            Return CustomScrollPosition
        End Get
        Set(ByVal value As Point)
            CustomScrollPosition = value
        End Set
    End Property

    Public Property ToolTipActive() As Boolean
        Get
            Return m_ContainerControl.ToolTipActive
        End Get
        Set(ByVal value As Boolean)
            m_ContainerControl.ToolTipActive = value
        End Set
    End Property

    <Browsable(False)> _
Public Property ContainerControlCursor() As Cursor
        Get
            Return m_ContainerControl.Cursor
        End Get
        Set(ByVal value As Cursor)
            m_ContainerControl.Cursor = value
        End Set
    End Property


    <Browsable(False)> _
    Public Property ContainerControlToolTipText() As String
        Get
            Return m_ContainerControl.ToolTipText
        End Get
        Set(ByVal value As String)
            m_ContainerControl.ToolTipText = value
        End Set
    End Property

    Public Overridable Property Redraw() As Boolean
        Get
            Return m_bRedraw
        End Get
        Set(ByVal value As Boolean)
            m_bRedraw = value
            If m_bRedraw Then InvalidateCells()
        End Set
    End Property


    Public Property EnableRowColSpan() As Boolean
        Get
            Return m_bEnableRowColSpan
        End Get
        Set(ByVal value As Boolean)
            m_bEnableRowColSpan = value
        End Set
    End Property


#End Region

#Region "== Methods ============================================================"
    Public Overridable Sub AutoSizeColumn(ByVal colIndex As Integer, ByVal minWidth As Integer)
        Dim l_Graphics As Graphics = CreateGraphics()
        Dim l_Row As Integer = 0
        For l_Row = 0 To m_Rows - 1
            If Not (m_Cells(l_Row, colIndex) Is Nothing) Then
                Dim l_Size As Size = m_Cells(l_Row, colIndex).GetRequiredSize(l_Graphics)
                If l_Size.Width > minWidth Then minWidth = l_Size.Width
            End If
        Next

        SetColWidth(colIndex, minWidth)
    End Sub

    Public Overridable Sub AutoSizeRow(ByVal rowIndex As Integer, ByVal minHeight As Integer)
        Dim l_Graphics As Graphics = CreateGraphics()
        Dim l_Col As Integer = 0
        For l_Col = 0 To m_Cols - 1
            If Not (m_Cells(rowIndex, l_Col) Is Nothing) Then
                Dim l_Size As Size = m_Cells(rowIndex, l_Col).GetRequiredSize(l_Graphics)
                If l_Size.Height > minHeight Then minHeight = l_Size.Height
            End If
        Next

        SetRowHeight(rowIndex, minHeight)
    End Sub

    Public Overridable Overloads Sub AutoSizeAll(ByVal minHeight As Integer, ByVal minWidth As Integer)
        Dim l_Col As Integer = 0
        Dim l_Row As Integer = 0

        For l_Col = 0 To m_Cols - 1
            AutoSizeColumn(l_Col, minWidth)
        Next

        For l_Row = 0 To m_Rows - 1
            AutoSizeRow(l_Row, minHeight)
        Next

        If AutoStretchColumnsToFitWidth Then
            StretchColumnsToFitWidth()
        End If
    End Sub

    Public Overridable Overloads Sub AutoSizeAll()
        AutoSizeAll(AutoSizeMinHeight, AutoSizeMinWidth)
    End Sub

    Public Overridable Sub StretchColumnsToFitWidth()
        If Cols > 0 Then
            Dim l_CurrentPos = (GetColLeft(Cols - 1) + GetColWidth(Cols - 1) + 4)
            If CustomClientRectangle.Width > l_CurrentPos Then
                Dim l_DeltaPerCol = (CustomClientRectangle.Width - l_CurrentPos) / Cols
                Dim l_Counter As Integer
                For l_Counter = 0 To Cols - 1
                    SetColWidth(l_Counter, GetColWidth(l_Counter) + l_DeltaPerCol)
                Next
            End If
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        If AutoStretchColumnsToFitWidth Then
            AutoSizeAll()
            StretchColumnsToFitWidth()
        End If
    End Sub

    Public Sub SetGridSize(ByVal rowCount As Integer, ByVal colCount As Integer)
        Rows = rowCount
        Cols = colCount
    End Sub

    Public Overridable Sub RemoveCell(ByVal rowIndex As Integer, ByVal colIndex As Integer)
        Dim l_Temp As Cell = m_Cells(rowIndex, colIndex)

        If Not (l_Temp Is Nothing) Then
            l_Temp.UnbindToGrid()

            m_Cells(rowIndex, colIndex) = Nothing
        End If
    End Sub

    Public Overridable Sub InsertCell(ByVal rowIndex As Integer, ByVal colIndex As Integer, ByVal cell As Cell)
        RemoveCell(rowIndex, colIndex)
        m_Cells(rowIndex, colIndex) = cell

        If Not (cell Is Nothing) Then
            If Not (cell.Grid Is Nothing) Then
                Throw New ArgumentException("This cell is already linked to a grid.", "cell")
            Else
                cell.BindToGrid(Me, rowIndex, colIndex)
                If Redraw Then cell.InvokeInvalidate()
            End If
        End If
    End Sub

    Public Overridable Overloads Function CellAtPoint(ByVal relativePoint As Point) As Cell
        Return CellAtPoint(relativePoint, True)
    End Function

    Public Overridable Overloads Function CellAtPoint(ByVal relativePoint As Point, ByVal manageFixedCells As Boolean) As Cell
        Dim l_AbsolutePoint As New Point(relativePoint.X - GridScrollPosition.X, relativePoint.Y - GridScrollPosition.Y)
        Dim l_Temp As Cell
        Dim l_Row As Integer = 0
        Dim l_Col As Integer = 0
        Dim l_StartRow As Integer
        Dim l_EndRow As Integer
        Dim l_StartCol As Integer
        Dim l_EndCol As Integer

        If manageFixedCells Then
            Dim l_SearchFixedRows As New Point(l_AbsolutePoint.X, relativePoint.Y)
            For l_Row = 0 To m_FixedRows - 1
                For l_Col = 0 To m_Cols - 1
                    If Not (m_Cells(l_Row, l_Col) Is Nothing) Then
                        If (m_Cells(l_Row, l_Col).AbsoluteRectangle.Contains(l_SearchFixedRows)) Then
                            Return m_Cells(l_Row, l_Col)
                        End If
                    End If
                Next
            Next

            Dim l_SearchFixedCols As New Point(relativePoint.X, l_AbsolutePoint.Y)
            For l_Col = 0 To m_FixedCols - 1
                For l_Row = 0 To m_Rows - 1
                    If Not (m_Cells(l_Row, l_Col) Is Nothing) Then
                        If (m_Cells(l_Row, l_Col).AbsoluteRectangle.Contains(l_SearchFixedCols)) Then
                            Return m_Cells(l_Row, l_Col)
                        End If
                    End If
                Next
            Next
        End If

        GetCellRangeFromAbsoluteRectangle(New Rectangle(l_AbsolutePoint, New Size(1, 1)), l_StartRow, l_EndRow, l_StartCol, l_EndCol)

        For l_Row = l_StartRow To m_Rows - 1
            Dim l_RowInfo As RowInfo = m_RowsInfo(l_Row)
            If l_RowInfo.Top <= l_AbsolutePoint.Y Then
                Dim l_IsInRow As Boolean = (l_RowInfo.Top + l_RowInfo.Height >= l_AbsolutePoint.Y)

                For l_Col = l_StartCol To m_Cols - 1
                    Dim l_ColInfo As ColInfo = m_ColsInfo(l_Col)
                    If l_ColInfo.Left <= l_AbsolutePoint.X Then
                        l_Temp = m_Cells(l_Row, l_Col)
                        If Not (l_Temp Is Nothing) Then
                            If (l_Temp.ColSpan > 1 OrElse l_Temp.RowSpan > 1 OrElse _
                                l_IsInRow OrElse (l_ColInfo.Left + l_ColInfo.Width >= l_AbsolutePoint.X)) Then

                                Dim l_Rectangle As Rectangle = GetCellAbsoluteRectangle(l_Row, l_Col, l_Temp.RowSpan, l_Temp.ColSpan)
                                If (l_AbsolutePoint.Y <= l_Rectangle.Bottom) AndAlso (l_AbsolutePoint.X <= l_Rectangle.Right) Then
                                    Return l_Temp
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Next

        Return Nothing
    End Function

    Public Sub GetCellRangeFromAbsoluteRectangle(ByVal absoluteRectangle As Rectangle, ByRef startRow As Integer, ByRef endRow As Integer, ByRef startCol As Integer, ByRef endCol As Integer)
        If m_bEnableRowColSpan Then
            startRow = 0
            endRow = m_Rows - 1
            startCol = 0
            endCol = m_Cols - 1
        Else
            startRow = m_RowsInfo.GetRowAtPoint(absoluteRectangle.Y)
            If startRow = -1 Then startRow = m_Rows
            endRow = m_RowsInfo.GetRowAtPoint(absoluteRectangle.Bottom)
            If endRow = -1 Then endRow = m_Rows - 1
            startCol = m_ColsInfo.GetColAtPoint(absoluteRectangle.X)
            If startCol = -1 Then startCol = m_Cols
            endCol = m_ColsInfo.GetColAtPoint(absoluteRectangle.Right)
            If endCol = -1 Then endCol = m_Cols - 1
        End If
    End Sub

    Private Sub LoadCellSet()
        Dim r As Integer, c As Integer
        Dim r_Count As Integer, c_Count As Integer
        Dim r_Depth As Integer, c_Depth As Integer
        Dim r_Current As Integer, c_Current As Integer
        Dim r_Span As Integer, c_Span As Integer
        Dim r_ID As String, c_ID As String
        Dim r_PrevID As String, c_PrevID As String
        Dim cellProperty As Adomd.CellProperty
        Dim objTuple As Adomd.Tuple

        Dim c_Text As String

        ' Clear the grid first!
        Me.SetGridSize(0, 0)
        Me.InvalidateCells()

        If Not (m_CellSet Is Nothing) Then
            If m_CellSet.Axes.Count > 2 Then
                ' Display a message to the user indicating that
                ' the grid only supports up to 2 axes.
                MsgBox("The grid is unable to display cellsets with more than 2 axes.")
            Else
                Me.Redraw = False

                Select Case m_CellSet.Axes.Count
                    Case 0
                        r_Count = 1
                        r_Depth = 0
                        c_Count = 1
                        c_Depth = 0
                    Case 1
                        r_Count = 1
                        r_Depth = 0
                        c_Count = m_CellSet.Axes(0).Positions.Count
                        c_Depth = m_CellSet.Axes(0).Set.Hierarchies.Count
                    Case 2
                        r_Count = m_CellSet.Axes(1).Positions.Count
                        c_Count = m_CellSet.Axes(0).Positions.Count
                        r_Depth = m_CellSet.Axes(1).Set.Hierarchies.Count
                        c_Depth = m_CellSet.Axes(0).Set.Hierarchies.Count
                End Select

                Me.SetGridSize(r_Count + c_Depth, c_Count + r_Depth)
                Me.FixedCols = r_Depth
                Me.FixedRows = c_Depth

                ' Load column headers
                If c_Depth > 0 Then
                    For r = 0 To c_Depth - 1
                        c_PrevID = ""
                        c_Current = 0
                        For c = 0 To c_Count - 1
                            c_ID = ""
                            With m_CellSet.Axes(0).Set.Tuples(c)
                                For r_Span = 0 To r
                                    c_ID += .Members(r_Span).Name
                                Next
                            End With
                            If c_ID = c_PrevID Then
                                Me.Cell(r, c_Current + r_Depth).ColSpan += 1
                                Me.Cell(r, c + r_Depth).UnbindToGrid()
                            Else
                                c_PrevID = c_ID
                                c_Current = c
                                With Me.Cell(r, c_Current + r_Depth)
                                    .TextAlignment = ContentAlignment.MiddleCenter
                                    .Value = m_CellSet.Axes(0).Set.Tuples(c_Current).Members(r).Caption
                                    .BackColor = Color.FromKnownColor(KnownColor.Control)
                                    .SetAllBorderColor(Color.FromKnownColor(KnownColor.Black))
                                End With
                            End If
                        Next
                    Next
                End If

                If r_Depth > 0 Then
                    ' Load Row headers
                    For c = 0 To r_Depth - 1
                        r_PrevID = ""
                        r_Current = 0
                        For r = 0 To r_Count - 1
                            r_ID = ""
                            With m_CellSet.Axes(1).Set.Tuples(r)
                                For c_Span = 0 To c
                                    r_ID += .Members(c_Span).Name
                                Next
                            End With
                            If r_ID = r_PrevID Then
                                Me.Cell(r_Current + c_Depth, c).RowSpan += 1
                                Me.Cell(r + c_Depth, c).UnbindToGrid()
                            Else
                                r_PrevID = r_ID
                                r_Current = r
                                With Me.Cell(r_Current + c_Depth, c)
                                    .TextAlignment = ContentAlignment.MiddleLeft
                                    .Value = m_CellSet.Axes(1).Set.Tuples(r_Current).Members(c).Caption
                                    .BackColor = Color.FromKnownColor(KnownColor.Control)
                                    .SetAllBorderColor(Color.FromKnownColor(KnownColor.Black))
                                End With
                            End If
                        Next
                    Next
                End If

                ' Manage empty header cells
                For r = 0 To c_Depth - 1
                    For c = 0 To r_Depth - 1
                        With Me.Cell(r, c)
                            .BackColor = Color.FromKnownColor(KnownColor.Control)
                            .SetAllBorderColor(Color.FromKnownColor(KnownColor.Black))
                        End With
                    Next
                Next

                ' Load cells
                Select Case m_CellSet.Axes.Count
                    Case 0
                        SetCellFromCellSet(Me.Cell(c_Depth, r_Depth), m_CellSet.Cells(0))
                    Case 1
                        For c = 0 To c_Count - 1
                            SetCellFromCellSet(Me.Cell(c_Depth, c), m_CellSet.Cells(c))
                        Next
                    Case 2
                        For r = 0 To r_Count - 1
                            For c = 0 To c_Count - 1
                                SetCellFromCellSet(Me.Cell(r + c_Depth, c + r_Depth), m_CellSet.Cells(c, r))
                            Next
                        Next
                End Select

                For c = 0 To (c_Count + r_Depth) - 1
                    Me.AutoSizeColumn(c, 10)
                Next
                For r = 0 To (r_Count + c_Depth) - 1
                    Me.AutoSizeRow(r, 10)
                Next

                Me.Redraw = True
            End If
        End If
    End Sub

    Private Sub SetCellFromCellSet(ByRef gridCell As AdomdSampleGrid.Cell, ByRef cell As Adomd.Cell)
        Dim cellProperty As Adomd.CellProperty
        Dim objFont As Font
        Dim objCellFont As Font
        Dim strFontName As String
        Dim sngFontSize As Single
        Dim enuFontStyle As FontStyle = FontStyle.Regular

        If gridCell.Font Is Nothing Then
            objCellFont = gridCell.Grid.Font
        Else
            objCellFont = gridCell.Font
        End If

        gridCell.Value = cell.FormattedValue.ToString
        gridCell.TextAlignment = ContentAlignment.MiddleRight

        cellProperty = cell.CellProperties.Find("FORE_COLOR")
        If Not (cellProperty Is Nothing) AndAlso Not (cellProperty.Value Is Nothing) Then
            gridCell.ForeColor = ColorTranslator.FromWin32(cellProperty.Value)
            cellProperty = Nothing
        End If

        cellProperty = cell.CellProperties.Find("BACK_COLOR")
        If Not (cellProperty Is Nothing) AndAlso Not (cellProperty.Value Is Nothing) Then
            gridCell.BackColor = ColorTranslator.FromWin32(cellProperty.Value)
            cellProperty = Nothing
        End If

        cellProperty = cell.CellProperties.Find("FONT_NAME")
        If Not (cellProperty Is Nothing) AndAlso Not (cellProperty.Value Is Nothing) Then
            strFontName = cellProperty.Value
            cellProperty = Nothing
        Else
            strFontName = objCellFont.Name
        End If

        cellProperty = cell.CellProperties.Find("FONT_SIZE")
        If Not (cellProperty Is Nothing) AndAlso Not (cellProperty.Value Is Nothing) Then
            sngFontSize = CSng(cellProperty.Value)
            cellProperty = Nothing
        Else
            sngFontSize = CSng(objCellFont.Size)
        End If

        cellProperty = cell.CellProperties.Find("FONT_FLAGS")
        If Not (cellProperty Is Nothing) AndAlso Not (cellProperty.Value Is Nothing) Then
            If (cellProperty.Value And 1) = 1 Then
                enuFontStyle = enuFontStyle Or FontStyle.Bold
            End If
            If (cellProperty.Value And 2) = 2 Then
                enuFontStyle = enuFontStyle Or FontStyle.Underline
            End If
            If (cellProperty.Value And 4) = 4 Then
                enuFontStyle = enuFontStyle Or FontStyle.Italic
            End If
            If (cellProperty.Value And 8) = 8 Then
                enuFontStyle = enuFontStyle Or FontStyle.Strikeout
            End If
            cellProperty = Nothing
        Else
            enuFontStyle = objCellFont.Style
        End If

        objFont = New Font(strFontName, sngFontSize, enuFontStyle)
        If Not (objFont.Equals(objCellFont)) Then gridCell.Font = objFont

    End Sub

    Public Overloads Function GetCellDisplayRectangle(ByVal cell As Cell) As Rectangle
        Return GetCellDisplayRectangle(cell.RowSpan, cell.Col, cell.RowSpan, cell.ColSpan)
    End Function

    Public Overloads Function GetCellDisplayRectangle(ByVal rowIndex As Integer, ByVal colIndex As Integer, ByVal rowSpan As Integer, ByVal colSpan As Integer) As Rectangle
        Dim l_Temp As Rectangle = GetCellAbsoluteRectangle(rowIndex, colIndex, rowSpan, colSpan)
        Dim l_X As Integer = l_Temp.X
        Dim l_Y As Integer = l_Temp.Y
        Dim l_ScrollPos As Point = CustomScrollPosition
        If rowIndex >= m_FixedRows Then
            l_Y += l_ScrollPos.Y
        End If
        If colIndex >= m_FixedCols Then
            l_X += l_ScrollPos.X
        End If

        Return New Rectangle(l_X, l_Y, l_Temp.Width, l_Temp.Height)
    End Function

    Public Function GetCellAbsoluteRectangle(ByVal cell As Cell) As Rectangle
        Return GetCellAbsoluteRectangle(cell.Row, cell.Col, cell.RowSpan, cell.ColSpan)
    End Function

    Public Function GetCellAbsoluteRectangle(ByVal rowIndex As Integer, ByVal colIndex As Integer, ByVal rowSpan As Integer, ByVal colSpan As Integer) As Rectangle
        Dim l_Row As Integer, l_Col As Integer

        If (rowIndex >= 0 AndAlso rowIndex < m_Rows AndAlso colIndex >= 0 AndAlso colIndex < m_Cols) Then
            Dim l_ColInfo As ColInfo = m_ColsInfo(colIndex)
            Dim l_RowInfo As RowInfo = m_RowsInfo(rowIndex)
            Dim l_Width As Integer = l_ColInfo.Width
            Dim l_Height As Integer = l_RowInfo.Height
            If rowSpan > 1 Then
                For l_Row = rowIndex + 1 To Math.Min((rowIndex + rowSpan) - 1, Rows - 1)
                    l_Height += m_RowsInfo(l_Row).Height
                Next
            End If
            If colSpan > 1 Then
                For l_Col = colIndex + 1 To Math.Min((colIndex + colSpan) - 1, Cols - 1)
                    l_Width += m_ColsInfo(l_Col).Width
                Next
            End If

            Return New Rectangle(l_ColInfo.Left, l_RowInfo.Top, l_Width, l_Height)
        Else
            Return New Rectangle(0, 0, 0, 0)
        End If
    End Function

    Public Overridable Function GetColWidth(ByVal colIndex As Integer) As Integer
        Return m_ColsInfo(colIndex).Width
    End Function

    Public Overridable Sub SetColWidth(ByVal colIndex As Integer, ByVal value As Integer)
        Dim l_Col As Integer

        If value > 0 Then
            Dim l_Delta As Integer = m_ColsInfo(colIndex).Width - value
            If l_Delta <> 0 Then
                m_ColsInfo(colIndex).Width = value
                For l_Col = colIndex + 1 To m_Cols - 1
                    m_ColsInfo(l_Col).Left -= l_Delta
                Next
                RecalculateScrollBar()
                InvalidateCells()
            End If
        End If
    End Sub

    Public Overridable Function GetRowHeight(ByVal rowIndex As Integer) As Integer
        Return m_RowsInfo(rowIndex).Height
    End Function

    Public Overridable Sub SetRowHeight(ByVal rowIndex As Integer, ByVal value As Integer)
        Dim l_Row As Integer

        If value > 0 Then
            Dim l_Delta As Integer = m_RowsInfo(rowIndex).Height - value
            If l_Delta <> 0 Then
                m_RowsInfo(rowIndex).Height = value
                For l_Row = rowIndex + 1 To m_Rows - 1
                    m_RowsInfo(l_Row).Top -= l_Delta
                Next
                RecalculateScrollBar()
                InvalidateCells()
            End If
        End If
    End Sub

    Public Function IsCellVisible(ByVal cell As Cell) As Boolean
        Dim l_ClientRect As Rectangle = CustomClientRectangle
        Dim l_X As Integer, l_Y As Integer, l_Width As Integer, l_Height As Integer

        If (m_FixedRows = 0 OrElse cell.Row < m_FixedRows) Then
            l_Y = l_ClientRect.Y
            l_Height = l_ClientRect.Height
        Else
            Dim l_RowInfo As RowInfo = m_RowsInfo(m_FixedRows)
            l_Y = l_ClientRect.Y + l_RowInfo.Top
            l_Height = l_ClientRect.Height - l_RowInfo.Top
        End If

        If (m_FixedCols = 0 OrElse cell.Col < m_FixedCols) Then
            l_X = l_ClientRect.X
            l_Width = l_ClientRect.Width
        Else
            Dim l_ColInfo As ColInfo = m_ColsInfo(m_FixedCols)
            l_X = l_ClientRect.X + l_ColInfo.Left
            l_Width = l_ClientRect.Width - l_ColInfo.Left
        End If

        Return cell.IsInDisplayRegion(New Rectangle(l_X + 1, l_Y + 1, l_Width - 1, l_Height - 1))
    End Function

    Public Function ShowCell(ByVal cell As Cell) As Boolean
        If IsCellVisible(cell) Then
            Return True
        Else
            Dim l_CellRect As Rectangle = cell.DisplayRectangle
            Dim l_NewCustomScrollPosition As Point = New Point(CustomScrollPosition.X, CustomScrollPosition.Y)
            Dim l_ApplyScroll As Boolean = False
            Dim l_ClientRectangle As Rectangle = CustomClientRectangle

            If (l_CellRect.Location.X < GetColLeft(Math.Min(m_FixedCols, cell.Col))) Then
                l_NewCustomScrollPosition.X -= l_CellRect.Location.X - GetColLeft(Math.Min(m_FixedCols, cell.Col))
                l_ApplyScroll = True
            End If

            If (l_CellRect.Location.Y < GetRowTop(Math.Min(m_FixedRows, cell.Row))) Then
                l_NewCustomScrollPosition.Y -= l_CellRect.Location.Y - GetRowTop(Math.Min(m_FixedRows, cell.Row))
                l_ApplyScroll = True
            End If

            If (l_CellRect.Right > l_ClientRectangle.Width) Then
                l_NewCustomScrollPosition.X -= l_CellRect.Right - l_ClientRectangle.Width
                l_ApplyScroll = True
            End If

            If (l_CellRect.Bottom > l_ClientRectangle.Height) Then
                l_NewCustomScrollPosition.Y -= l_CellRect.Bottom - l_ClientRectangle.Height
                l_ApplyScroll = True
            End If

            If l_ApplyScroll Then
                CustomScrollPosition = l_NewCustomScrollPosition
                If (m_FixedRows > 0 OrElse m_FixedCols > 0) Then
                    InvalidateCells()
                End If
            End If

            Return False
        End If
    End Function

    Public Sub InvalidateCells()
        Invalidate(True)
    End Sub

    Public Sub InvalidateCell(ByVal cell As Cell)
        m_ContainerControl.Invalidate(cell.DisplayRectangle, False)
    End Sub

    Public Function GetColLeft(ByVal colIndex As Integer) As Integer
        Return m_ColsInfo(colIndex).Left
    End Function

    Public Function GetRowTop(ByVal rowIndex As Integer) As Integer
        Return m_RowsInfo(rowIndex).Top
    End Function

    Private Sub InnerChangeFocusCell(ByVal newCell As Cell)
        If (Not (newCell Is Nothing)) AndAlso (newCell.Grid Is Nothing) Then
            newCell = Nothing
        End If

        If Not (newCell Is Nothing) Then
            ShowCell(newCell)
        End If

    End Sub

    Public Function SetFocusOnContainerControl() As Boolean
        Return m_ContainerControl.Focus
    End Function

    Protected Overrides Sub OnMouseWheel(ByVal e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        Try
            If e.Delta >= 120 OrElse e.Delta <= -120 Then
                Dim t As Point = CustomScrollPosition
                Dim l_NewY As Long = t.Y + (SystemInformation.MouseWheelScrollLines * 6) * Math.Sign(e.Delta)

                If l_NewY > 0 Then l_NewY = 0
                If l_NewY < (-MyBase.MaximumVScroll) Then l_NewY = -MyBase.MaximumVScroll

                CustomScrollPosition = New Point(t.X, l_NewY)
            End If
        Catch ex As Exception
            ' Raise an exception here.
        End Try
    End Sub

    Public Overridable Overloads Sub AddRow(ByVal newRowPosition As Long, ByVal ParamArray cells() As Cell)
        AddRow(newRowPosition)
        If Not (cells Is Nothing) Then
            Dim l_Length As Long = Math.Min(Cols, cells.Length)
            Dim l_Counter As Long
            For l_Counter = 0 To (l_Length - 1)
                Me(newRowPosition, l_Counter) = cells(l_Counter)
            Next
        End If
    End Sub

    Public Overridable Overloads Sub AddRow(ByVal newRowPosition As Long)
        AddRow(newRowPosition, 1)
    End Sub

    Public Overridable Overloads Sub AddRow(ByVal newRowPosition As Long, ByVal newRowNumber As Long)
        If newRowNumber <= 0 Then
            Throw New ApplicationException("Invalid row number")
        Else
            RedimMatrix(m_Rows + newRowNumber, m_Cols)
            Dim r As Long, c As Long
            For r = newRowPosition To (newRowPosition + newRowNumber) - 1
                InnerRowAdded(r)
            Next

            For r = m_Rows - 1 To (newRowPosition + newRowNumber) Step -1
                For c = 0 To m_Cols - 1
                    Dim tmp As Cell = m_Cells(r - newRowNumber, c)
                    RemoveCell(r - newRowNumber, c)
                    InsertCell(r, c, tmp)
                Next
            Next

            For r = newRowPosition To (newRowPosition + newRowNumber) - 1
                OnRowAdded(New RowEventArgs(r))
            Next

            RecalculateScrollBar()
            InvalidateCells()
        End If
    End Sub

    Public Overridable Overloads Sub RemoveRow(ByVal rowToRemove As Long)
        RemoveRow(rowToRemove, 1)
    End Sub

    Public Overridable Overloads Sub RemoveRow(ByVal startRemoveRow As Long, ByVal rowToRemove As Long)
        Dim r As Long, c As Long

        For r = startRemoveRow + rowToRemove To m_Rows - 1
            For c = 0 To m_Cols - 1
                Dim tmp As Cell = m_Cells(r, c)
                RemoveCell(r, c)
                InsertCell(r - rowToRemove, c, tmp)
            Next
        Next

        RedimMatrix(m_Rows - rowToRemove, m_Cols)

        For r = (startRemoveRow + rowToRemove - 1) To startRemoveRow Step -1
            InnerOnRowRemoved(New RowEventArgs(startRemoveRow))
        Next

        RecalculateScrollBar()
        InvalidateCells()
    End Sub

    Public Overridable Overloads Sub AddColumn(ByVal newColPosition As Long)
        AddColumn(newColPosition, 1)
    End Sub

    Public Overridable Overloads Sub AddColumn(ByVal newColPosition As Long, ByVal newColNumber As Long)
        Dim r As Long, c As Long

        If newColNumber < 1 Then
            Throw New ApplicationException("Invalid column number")
        End If

        RedimMatrix(m_Rows, m_Cols + newColNumber)

        For c = newColPosition To (newColPosition + newColNumber) - 1
            InnerColumnAdded(c)
        Next

        For c = m_Cols - 1 To (newColPosition + newColNumber) Step -1
            For r = 0 To m_Rows - 1
                Dim tmp As Cell = m_Cells(r, c - newColNumber)
                RemoveCell(r, c - newColNumber)
                InsertCell(r, c, tmp)
            Next
        Next


        For c = newColPosition To (newColPosition + newColNumber) - 1
            OnColumnAdded(New ColumnEventArgs(c))
        Next

        RecalculateScrollBar()
        InvalidateCells()
    End Sub

    Public Overridable Overloads Sub RemoveColumn(ByVal columnToRemove As Long)
        RemoveColumn(columnToRemove, 1)
    End Sub

    Public Overridable Overloads Sub RemoveColumn(ByVal startRemoveColumn As Long, ByVal columnToRemove As Long)
        Dim r As Long, c As Long

        For c = (startRemoveColumn + columnToRemove) To m_Cols - 1
            For r = 0 To m_Rows - 1
                Dim tmp As Cell = m_Cells(r, c)
                RemoveCell(r, c)
                InsertCell(r, c - columnToRemove, tmp)
            Next
        Next

        RedimMatrix(m_Rows, m_Cols - columnToRemove)

        For c = (startRemoveColumn + columnToRemove - 1) To startRemoveColumn Step -1
            InnerOnColumnRemoved(New ColumnEventArgs(startRemoveColumn))
        Next

        RecalculateScrollBar()
        InvalidateCells()
    End Sub

    Private Sub InnerRowAdded(ByVal row As Long)
        Dim l_Top As Long = 0
        Dim r As Long

        If row > 0 Then
            l_Top = m_RowsInfo(row - 1).Top
            l_Top += m_RowsInfo(row - 1).Height
        End If

        m_RowsInfo.Add(row, New RowInfo(DefaultHeight, l_Top))

        For r = row + 1 To m_RowsInfo.Count - 1
            m_RowsInfo(r).Top += m_RowsInfo(row).Height
        Next
    End Sub

    Protected Overridable Sub OnRowAdded(ByVal e As RowEventArgs)
        ' Do nothing.
    End Sub

    Private Sub InnerOnRowRemoved(ByVal e As RowEventArgs)
        Dim l_Oldtop As Long = m_RowsInfo(e.Row).Top
        Dim r As Long

        For r = e.Row + 1 To m_RowsInfo.Count - 1
            m_RowsInfo(r).Top = l_Oldtop
            l_Oldtop = l_Oldtop + m_RowsInfo(r).Height
        Next

        m_RowsInfo.RemoveAt(e.Row)
        OnRowRemoved(e)
    End Sub

    Protected Overridable Sub OnRowRemoved(ByVal e As RowEventArgs)
        ' Do nothing.
    End Sub

    Private Sub InnerColumnAdded(ByVal col As Long)
        Dim l_Left As Long = 0
        Dim c As Long

        If col > 0 Then
            l_Left = m_ColsInfo(col - 1).Left
            l_Left += m_ColsInfo(col - 1).Width
        End If

        m_ColsInfo.Add(col, New ColInfo(DefaultWidth, l_Left))

        For c = col + 1 To m_ColsInfo.Count - 1
            m_ColsInfo(c).Left += m_ColsInfo(col).Width
        Next
    End Sub

    Protected Overridable Sub OnColumnAdded(ByVal e As ColumnEventArgs)
        ' Do nothing.
    End Sub

    Private Sub InnerOnColumnRemoved(ByVal e As ColumnEventArgs)
        Dim l_OldLeft As Long = m_ColsInfo(e.Column).Left
        Dim c As Long

        For c = e.Column + 1 To m_ColsInfo.Count - 1
            m_ColsInfo(c).Left = l_OldLeft
            l_OldLeft = l_OldLeft + m_ColsInfo(c).Width
        Next

        m_ColsInfo.RemoveAt(e.Column)
        OnColumnRemoved(e)
    End Sub

    Protected Overridable Sub OnColumnRemoved(ByVal e As ColumnEventArgs)
        ' Do nothing
    End Sub

    Private Sub RedimMatrix(ByVal rows As Long, ByVal cols As Long)
        If m_Cells Is Nothing Then
            ReDim m_Cells(rows, cols)
            Dim r As Long, c As Long
            For r = 0 To (rows - 1)
                For c = 0 To (cols - 1)
                    m_Cells(r, c) = New Cell()
                    m_Cells(r, c).BindToGrid(Me, r, c)
                Next
            Next
        Else
            If m_Rows <> rows OrElse m_Cols <> cols Then
                Dim l_tmp(,) As Cell = m_Cells
                Dim l_MinRows As Long = Math.Min(l_tmp.GetLength(0), rows)
                Dim l_MinCols As Long = Math.Min(l_tmp.GetLength(1), cols)
                Dim i As Long, j As Long

                For i = l_MinRows To (l_tmp.GetLength(0) - 1)
                    For j = 0 To (l_tmp.GetLength(1) - 1)
                        RemoveCell(i, j)
                    Next
                Next

                For i = 0 To (l_MinRows - 1)
                    For j = l_MinCols To (l_tmp.GetLength(1) - 1)
                        RemoveCell(i, j)
                    Next
                Next

                ReDim m_Cells(rows, cols)

                For i = 0 To (l_MinRows - 1)
                    For j = 0 To (l_MinCols - 1)
                        m_Cells(i, j) = l_tmp(i, j)
                    Next
                Next

                Dim r As Long, c As Long
                For r = 0 To (m_Cells.GetLength(0) - 1)
                    For c = 0 To (m_Cells.GetLength(1) - 1)
                        If (m_Cells(r, c) Is Nothing) Then
                            m_Cells(r, c) = New Cell()
                        End If
                        m_Cells(r, c).BindToGrid(Me, r, c)
                    Next
                Next
            End If
        End If

        m_Rows = m_Cells.GetLength(0) - 1
        m_Cols = m_Cells.GetLength(1) - 1
    End Sub

    Protected Sub RecalculateScrollBar()
        Dim l_MaxY As Integer = 0
        Dim l_MaxX As Integer = 0
        Dim r As Long, c As Long

        For r = 0 To Rows - 1
            l_MaxY += GetRowHeight(r)
        Next

        For c = 0 To Cols - 1
            l_MaxX += GetColWidth(c)
        Next

        CustomScrollArea = New Size(l_MaxX, l_MaxY)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub BackContainer_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
        Dim l_ScrollPoint As Point = CustomScrollPosition
        Dim l_AbsoluteDrawRect As Rectangle = New Rectangle(e.ClipRectangle.X - l_ScrollPoint.X, e.ClipRectangle.Y - l_ScrollPoint.Y, e.ClipRectangle.Width, e.ClipRectangle.Height)

        Dim l_EndCol As Long
        Dim l_StartCol As Long
        Dim l_EndRow As Long
        Dim l_StartRow As Long

        GetCellRangeFromAbsoluteRectangle(l_AbsoluteDrawRect, l_StartRow, l_EndRow, l_StartCol, l_EndCol)

        If m_FixedRows > l_StartRow Then
            l_StartRow = m_FixedRows
        End If

        If m_FixedCols > l_StartCol Then
            l_StartCol = m_FixedCols
        End If

        Dim r As Long, c As Long

        For r = l_StartRow To l_EndRow
            If (m_RowsInfo(r).Top + l_ScrollPoint.Y) > e.ClipRectangle.Bottom Then
                Exit For
            Else
                For c = l_StartCol To l_EndCol
                    If (m_ColsInfo(c).Left + l_ScrollPoint.X) > e.ClipRectangle.Right Then
                        Exit For
                    Else
                        If Not (m_Cells(r, c) Is Nothing) Then
                            m_Cells(r, c).InvokePaint(e, l_AbsoluteDrawRect, True)
                        End If
                    End If
                Next
            End If
        Next

        Dim l_SafeFixedRows As Long = m_FixedRows

        If l_SafeFixedRows > m_Rows Then
            l_SafeFixedRows = m_Rows
        End If

        For r = 0 To l_SafeFixedRows - 1
            If (m_RowsInfo(r).Top + l_ScrollPoint.Y) > e.ClipRectangle.Bottom Then
                Exit For
            Else
                For c = 0 To m_Cols - 1
                    If Not (m_Cells(r, c) Is Nothing) Then
                        m_Cells(r, c).InvokePaint(e, l_AbsoluteDrawRect, False)
                    End If
                Next
            End If
        Next

        Dim l_SafeFixedCols As Long = m_FixedCols

        If l_SafeFixedRows > m_Cols Then
            l_SafeFixedRows = m_Cols
        End If

        For c = 0 To l_SafeFixedCols - 1
            For r = 0 To m_Rows - 1
                If (m_RowsInfo(r).Top + l_ScrollPoint.Y) > e.ClipRectangle.Bottom Then
                    Exit For
                Else
                    If Not (m_Cells(r, c) Is Nothing) Then
                        m_Cells(r, c).InvokePaint(e, l_AbsoluteDrawRect, False)
                    End If
                End If
            Next
        Next

        For r = 0 To l_SafeFixedRows - 1
            For c = 0 To l_SafeFixedCols - 1
                If Not (m_Cells(r, c) Is Nothing) Then
                    m_Cells(r, c).InvokePaint(e, l_AbsoluteDrawRect, False)
                End If
            Next
        Next
    End Sub

    Public Sub MoveColumn(ByVal oldCol As Long, ByVal newCol As Long)
        If oldCol = newCol Then
            Exit Sub
        End If

        Dim tmp() As Cell = New Cell(Rows) {}

        Dim r As Long, c As Long

        For r = 0 To Rows - 1
            tmp(r) = Me(r, oldCol)
        Next

        RemoveColumn(oldCol)
        AddColumn(newCol)
        For r = 0 To Rows - 1
            Me(r, newCol) = tmp(r)
        Next
    End Sub

    Public Sub MoveRow(ByVal oldRow As Long, ByVal newRow As Long)
        If oldRow = newRow Then
            Exit Sub
        End If

        Dim tmp() As Cell = New Cell(Cols) {}

        Dim r As Long, c As Long
        For c = 0 To Cols - 1
            tmp(c) = Me(oldRow, c)
        Next

        RemoveRow(oldRow)
        AddRow(newRow)

        For c = 0 To Cols - 1
            Me(newRow, c) = tmp(c)
        Next
    End Sub
#End Region

End Class
