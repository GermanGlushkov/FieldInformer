Option Explicit On 

'======================================================================
'
'  File:      ScrollableControl.vb
'  Summary:   User control that encapsulates horizontal and vertical
'             scroll bar support for a user-specified client area.
'             AdomdSampleGrid uses this control as the base class
'             for the Grid control.
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

Public Class ScrollableControl
    Inherits System.Windows.Forms.UserControl

#Region "== Variables =========================================================="
    Private WithEvents m_VScroll As VScrollBar = Nothing
    Private WithEvents m_HScroll As HScrollBar = Nothing
    Private WithEvents m_BottomRightPanel As Panel = Nothing

    Private m_CustomScrollArea As New Size(0, 0)
    Private m_OldVScrollValue As Integer = 0
    Private m_OldHScrollValue As Integer = 0
    Private m_NotScrollableControls As New ArrayList

#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        MyBase.AutoScroll = False
        AddHandler ControlRemoved, AddressOf Controls_Removed

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

#Region "== Events ============================================================="
    Public Event VScrollPositionChanged As ScrollPositionChangedEventHandler
    Public Event HScrollPositionChanged As ScrollPositionChangedEventHandler

#End Region

#Region "== Properties ========================================================="
    <System.ComponentModel.Browsable(False)> _
    Public Overrides Property AutoScroll() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
            If value Then
                Throw New ApplicationException("The AutoScroll property is not supported.")
                MyBase.AutoScroll = False
            End If
        End Set
    End Property


    <System.ComponentModel.Browsable(False)> _
    Public Overridable Property CustomScrollArea() As Size
        Get
            Return m_CustomScrollArea
        End Get
        Set(ByVal value As Size)
            m_CustomScrollArea = value
            RecalculateCustomScrollBars()
        End Set
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overridable Property CustomScrollPosition() As Point
        Get
            Dim l_X As Integer = 0
            If Not (m_HScroll Is Nothing) Then l_X = -m_HScroll.Value
            Dim l_Y As Integer = 0
            If Not (m_VScroll Is Nothing) Then l_Y = -m_VScroll.Value
            Return New Point(l_X, l_Y)
        End Get
        Set(ByVal value As Point)
            If Not (m_HScroll Is Nothing) Then m_HScroll.Value = -value.X
            If Not (m_VScroll Is Nothing) Then m_VScroll.Value = -value.Y
        End Set
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overridable ReadOnly Property CustomClientRectangle() As Rectangle
        Get
            Dim l_ScrollH As Integer = 0
            If Not (m_HScroll Is Nothing) Then l_ScrollH = m_HScroll.Height
            Dim l_ScrollV As Integer = 0
            If Not (m_VScroll Is Nothing) Then l_ScrollV = m_VScroll.Width
            If (Not (m_HScroll Is Nothing)) OrElse (Not (m_VScroll Is Nothing)) Then
                Return New Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - l_ScrollV, ClientRectangle.Height - l_ScrollH)
            Else
                Return ClientRectangle
            End If
        End Get
    End Property

    <System.ComponentModel.Browsable(False)> _
Public Overridable ReadOnly Property MaximumVScroll() As Integer
        Get
            If m_VScroll Is Nothing Then
                Return 0
            Else
                Return m_VScroll.Maximum
            End If
        End Get
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overridable ReadOnly Property MinimumVScroll() As Integer
        Get
            Return 0
        End Get
    End Property

    <System.ComponentModel.Browsable(False)> _
       Public Overridable ReadOnly Property MinimumHScroll() As Integer
        Get
            Return 0
        End Get
    End Property

    <System.ComponentModel.Browsable(False)> _
    Public Overridable ReadOnly Property MaximumHScroll() As Integer
        Get
            If m_HScroll Is Nothing Then
                Return 0
            Else
                Return m_HScroll.Maximum
            End If
        End Get
    End Property

#End Region

#Region "== Methods ============================================================"
    Protected Overridable Sub RemoveHScrollBar()
        If Not (m_HScroll Is Nothing) Then
            Try
                RemoveHandler m_HScroll.ValueChanged, New EventHandler(AddressOf HScrollChangeHandler)
                Controls.Remove(m_HScroll)
            Catch ex As Exception
                Throw ex
            Finally
                m_HScroll.Dispose()
                m_HScroll = Nothing
            End Try
        End If
    End Sub

    Protected Overridable Sub RemoveVScrollBar()
        If Not (m_VScroll Is Nothing) Then
            Try
                RemoveHandler m_VScroll.ValueChanged, New EventHandler(AddressOf VScrollChangeHandler)
                Controls.Remove(m_VScroll)
            Catch ex As Exception
                Throw ex
            Finally
                m_VScroll.Dispose()
                m_VScroll = Nothing
            End Try
        End If
    End Sub

    Protected Overridable Sub RecalculateHScrollBar()
        If Not (m_HScroll Is Nothing) Then
            Dim l_WidthVScroll As Integer = 0
            If Not (m_VScroll Is Nothing) Then
                l_WidthVScroll = m_VScroll.Width
            End If

            With m_HScroll
                .Location = New Point(0, ClientRectangle.Height - m_HScroll.Height)
                .Width = ClientRectangle.Width - l_WidthVScroll
                .Minimum = 0
                .Maximum = Math.Max(0, m_CustomScrollArea.Width)
                .LargeChange = Math.Max(5, ClientRectangle.Width - l_WidthVScroll)
                .SmallChange = m_HScroll.LargeChange / 5
                .BringToFront()
            End With
        End If
    End Sub

    Protected Overridable Sub RecalculateVScrollBar()
        If Not (m_VScroll Is Nothing) Then
            Dim l_HeightHScroll As Integer = 0
            If Not (m_HScroll Is Nothing) Then
                l_HeightHScroll = m_HScroll.Height
            End If

            With m_VScroll
                .Location = New Point(ClientRectangle.Width - m_VScroll.Width, 0)
                .Height = ClientRectangle.Height - l_HeightHScroll
                .Minimum = 0
                .Maximum = Math.Max(0, m_CustomScrollArea.Height)
                .LargeChange = Math.Max(5, ClientRectangle.Height - l_HeightHScroll)
                .SmallChange = m_VScroll.LargeChange / 5
                .BringToFront()
            End With
        End If
    End Sub

    Public Overridable Sub RecalculateCustomScrollBars()
        Dim l_Client As Rectangle = ClientRectangle

        If (l_Client.Height < m_CustomScrollArea.Height) AndAlso (l_Client.Width < m_CustomScrollArea.Width) Then
            If m_VScroll Is Nothing Then
                m_VScroll = New VScrollBar
                AddHandler m_VScroll.ValueChanged, New EventHandler(AddressOf VScrollChangeHandler)
                Controls.Add(m_VScroll)
            End If

            If m_HScroll Is Nothing Then
                m_HScroll = New HScrollBar
                AddHandler m_HScroll.ValueChanged, New EventHandler(AddressOf HScrollChangeHandler)
                Controls.Add(m_HScroll)
            End If
        ElseIf (l_Client.Height < m_CustomScrollArea.Height) Then
            If m_VScroll Is Nothing Then
                m_VScroll = New VScrollBar
                AddHandler m_VScroll.ValueChanged, New EventHandler(AddressOf VScrollChangeHandler)
                Controls.Add(m_VScroll)
            End If

            m_OldHScrollValue = 0
            RemoveHScrollBar()

            If Not (m_BottomRightPanel Is Nothing) Then
                Try
                    Controls.Remove(m_BottomRightPanel)
                Catch ex As Exception
                    Throw ex
                Finally
                    m_BottomRightPanel.Dispose()
                    m_BottomRightPanel = Nothing
                End Try
            End If
        ElseIf (l_Client.Width < m_CustomScrollArea.Width) Then
            If m_HScroll Is Nothing Then
                m_HScroll = New HScrollBar
                AddHandler m_HScroll.ValueChanged, New EventHandler(AddressOf HScrollChangeHandler)
                Controls.Add(m_HScroll)
            End If

            m_OldVScrollValue = 0
            RemoveVScrollBar()

            If Not (m_BottomRightPanel Is Nothing) Then
                Try
                    Controls.Remove(m_BottomRightPanel)
                Catch ex As Exception
                    Throw ex
                Finally
                    m_BottomRightPanel.Dispose()
                    m_BottomRightPanel = Nothing
                End Try
            End If
        Else
            m_OldHScrollValue = 0
            m_OldVScrollValue = 0
            RemoveVScrollBar()
            RemoveHScrollBar()

            If Not (m_BottomRightPanel Is Nothing) Then
                Try
                    Controls.Remove(m_BottomRightPanel)
                Catch ex As Exception
                    Throw ex
                Finally
                    m_BottomRightPanel.Dispose()
                    m_BottomRightPanel = Nothing
                End Try
            End If
        End If

        RecalculateVScrollBar()
        RecalculateHScrollBar()

        If (Not (m_HScroll Is Nothing)) AndAlso (Not (m_VScroll Is Nothing)) Then
            If m_BottomRightPanel Is Nothing Then
                m_BottomRightPanel = New Panel
                m_BottomRightPanel.BackColor = Color.FromKnownColor(KnownColor.Control)
                Controls.Add(m_BottomRightPanel)
            End If
            With m_BottomRightPanel
                .Location = New Point(m_HScroll.Right, m_VScroll.Bottom)
                .Size = New Size(m_VScroll.Width, m_HScroll.Height)
                .BringToFront()
            End With
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        RecalculateCustomScrollBars()
    End Sub

    Private Sub VScrollChangeHandler(ByVal sender As Object, ByVal e As EventArgs)
        OnVScrollPositionChanged(New ScrollPositionChangedEventArgs(-m_VScroll.Value, -m_OldVScrollValue))
        Invalidate(True)
        m_OldVScrollValue = m_VScroll.Value
    End Sub

    Private Sub HScrollChangeHandler(ByVal sender As Object, ByVal e As EventArgs)
        OnHScrollPositionChanged(New ScrollPositionChangedEventArgs(-m_HScroll.Value, -m_OldHScrollValue))
        Invalidate(True)
        m_OldHScrollValue = m_HScroll.Value
    End Sub

    Protected Overridable Sub OnVScrollPositionChanged(ByVal e As ScrollPositionChangedEventArgs)
        Dim l_Control As Control

        For Each l_Control In Controls
            If (Not (l_Control Is m_HScroll)) AndAlso _
                (Not (l_Control Is m_VScroll)) AndAlso _
                (Not (l_Control Is m_BottomRightPanel)) AndAlso _
                m_NotScrollableControls.Contains(l_Control) = False AndAlso _
                l_Control.Dock = DockStyle.None Then

                l_Control.Top -= e.Delta
            End If
        Next

        RaiseEvent VScrollPositionChanged(Me, e)
    End Sub

    Protected Overridable Sub OnHScrollPositionChanged(ByVal e As ScrollPositionChangedEventArgs)
        Dim l_Control As Control

        For Each l_Control In Controls
            If (Not (l_Control Is m_HScroll)) AndAlso _
                (Not (l_Control Is m_VScroll)) AndAlso _
                (Not (l_Control Is m_BottomRightPanel)) AndAlso _
                m_NotScrollableControls.Contains(l_Control) = False AndAlso _
                l_Control.Dock = DockStyle.None Then

                l_Control.Left -= e.Delta
            End If
        Next

        RaiseEvent HScrollPositionChanged(Me, e)
    End Sub

    Public Sub AddNotScrollableControls(ByVal control As Control)
        m_NotScrollableControls.Add(control)
    End Sub

    Public Sub RemoveNotScrollableControls(ByVal control As Control)
        If m_NotScrollableControls.Contains(control) Then m_NotScrollableControls.Remove(control)
    End Sub

    Private Sub Controls_Removed(ByVal sender As Object, ByVal e As ControlEventArgs)
        RemoveNotScrollableControls(e.Control)
    End Sub

    Public Overridable Sub CustomScrollPageDown()
        If Not (m_VScroll Is Nothing) Then m_VScroll.Value = Math.Min(m_VScroll.Value + m_VScroll.LargeChange, m_VScroll.Maximum)
    End Sub

    Public Overridable Sub CustomScrollPageUp()
        If Not (m_VScroll Is Nothing) Then m_VScroll.Value = Math.Max(m_VScroll.Value - m_VScroll.LargeChange, m_VScroll.Minimum)
    End Sub

    Public Overridable Sub CustomScrollPageRight()
        If Not (m_HScroll Is Nothing) Then m_HScroll.Value = Math.Min(m_HScroll.Value + m_HScroll.LargeChange, m_HScroll.Maximum)
    End Sub

    Public Overridable Sub CustomScrollPageLeft()
        If Not (m_HScroll Is Nothing) Then m_HScroll.Value = Math.Max(m_HScroll.Value - m_HScroll.LargeChange, m_HScroll.Minimum)
    End Sub

    Public Overridable Sub CustomScrollLineDown()
        If Not (m_VScroll Is Nothing) Then m_VScroll.Value = Math.Min(m_VScroll.Value + m_VScroll.SmallChange, m_VScroll.Maximum)
    End Sub

    Public Overridable Sub CustomScrollLineUp()
        If Not (m_VScroll Is Nothing) Then m_VScroll.Value = Math.Max(m_VScroll.Value - m_VScroll.SmallChange, m_VScroll.Minimum)
    End Sub

    Public Overridable Sub CustomScrollLineRight()
        If Not (m_HScroll Is Nothing) Then m_HScroll.Value = Math.Min(m_HScroll.Value + m_HScroll.SmallChange, m_HScroll.Maximum)
    End Sub

    Public Overridable Sub CustomScrollLineLeft()
        If Not (m_HScroll Is Nothing) Then m_HScroll.Value = Math.Max(m_HScroll.Value - m_HScroll.SmallChange, m_HScroll.Minimum)
    End Sub
#End Region

End Class
