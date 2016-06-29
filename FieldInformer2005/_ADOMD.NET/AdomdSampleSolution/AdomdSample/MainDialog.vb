Option Explicit On 
Imports Microsoft.AnalysisServices.AdomdClient

'======================================================================
'
'  File:      MainDialog.vb
'  Summary:   Main dialog for AdomdSample.
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

Public Class MainDialog
    Inherits System.Windows.Forms.Form

#Region "== Enumerations ======================================================="
    ' Used to refer to images stored in the ImageList control assigned to the
    ' Metadata Browser pane.
    Private Enum MetadataImages
        Folder_Closed = 0
        Folder_Opened
        Server_Unknown
        Server_Connected
        Server_Disconnected
        Database
        DataSource
        DataSourceView
        Cube
        VirtualCube
        Dimension
        MeasureGroup
        Measure
        Hierarchy
        LevelAll
        Level1
        Level2
        Level3
        Level4
        Level5
        Level6
        Level7
        Level8
        Level9
        Level10
        Level11
        Level12
        Level13
        Level14
        Level15
        Level16
        Member
        Member_Formula
        NamedSet
    End Enum
#End Region

#Region "== Variables =========================================================="
    ' Flag to handle connection state for the form.
    Private m_IsConnected As Boolean = False
    ' Contains the active CellSet object for the form.
    Private m_CellSet As CellSet
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents sbrMessage As System.Windows.Forms.StatusBarPanel
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents pnlResults As System.Windows.Forms.Panel
    Friend WithEvents sptResults As System.Windows.Forms.Splitter
    Friend WithEvents pnlMetaData As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlQuery As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents lblMetaData As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents imlMetadata As System.Windows.Forms.ImageList
    Friend WithEvents tvwSyntax As System.Windows.Forms.TreeView
    Friend WithEvents pnlSBrowser As System.Windows.Forms.Panel
    Friend WithEvents pnlMDBrowser As System.Windows.Forms.Panel
    Friend WithEvents sbrMain As System.Windows.Forms.StatusBar
    Friend WithEvents chkUseSchemaRowsets As System.Windows.Forms.CheckBox
    Friend WithEvents tvwMetadata As System.Windows.Forms.TreeView
    Friend WithEvents imlToolbar As System.Windows.Forms.ImageList
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileConnect As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileDisconnect As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuQueryRun As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileBar1 As System.Windows.Forms.MenuItem
    Friend WithEvents tbbConnect As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbSeparator2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbQueryRun As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbrMain As System.Windows.Forms.ToolBar
    Friend WithEvents mnuMain As System.Windows.Forms.MainMenu
    Friend WithEvents pnlCurrentDatabase As System.Windows.Forms.Panel
    Friend WithEvents lblCurrentDatabase As System.Windows.Forms.Label
    Friend WithEvents rtfQuery As System.Windows.Forms.RichTextBox
    Friend WithEvents grdResults As Microsoft.Samples.SqlServer.AdomdSample.AdomdSampleGrid.Grid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MainDialog))
        Me.sbrMain = New System.Windows.Forms.StatusBar()
        Me.sbrMessage = New System.Windows.Forms.StatusBarPanel()
        Me.mnuMain = New System.Windows.Forms.MainMenu()
        Me.mnuFile = New System.Windows.Forms.MenuItem()
        Me.mnuFileConnect = New System.Windows.Forms.MenuItem()
        Me.mnuFileDisconnect = New System.Windows.Forms.MenuItem()
        Me.mnuFileBar1 = New System.Windows.Forms.MenuItem()
        Me.mnuFileExit = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.mnuQueryRun = New System.Windows.Forms.MenuItem()
        Me.pnlResults = New System.Windows.Forms.Panel()
        Me.grdResults = New Microsoft.Samples.SqlServer.AdomdSample.AdomdSampleGrid.Grid()
        Me.sptResults = New System.Windows.Forms.Splitter()
        Me.tbrMain = New System.Windows.Forms.ToolBar()
        Me.tbbConnect = New System.Windows.Forms.ToolBarButton()
        Me.tbbSeparator2 = New System.Windows.Forms.ToolBarButton()
        Me.tbbQueryRun = New System.Windows.Forms.ToolBarButton()
        Me.imlToolbar = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMetaData = New System.Windows.Forms.Panel()
        Me.pnlSBrowser = New System.Windows.Forms.Panel()
        Me.tvwSyntax = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlMDBrowser = New System.Windows.Forms.Panel()
        Me.tvwMetadata = New System.Windows.Forms.TreeView()
        Me.imlMetadata = New System.Windows.Forms.ImageList(Me.components)
        Me.chkUseSchemaRowsets = New System.Windows.Forms.CheckBox()
        Me.lblMetaData = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlQuery = New System.Windows.Forms.Panel()
        Me.rtfQuery = New System.Windows.Forms.RichTextBox()
        Me.pnlCurrentDatabase = New System.Windows.Forms.Panel()
        Me.lblCurrentDatabase = New System.Windows.Forms.Label()
        CType(Me.sbrMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlResults.SuspendLayout()
        Me.pnlMetaData.SuspendLayout()
        Me.pnlSBrowser.SuspendLayout()
        Me.pnlMDBrowser.SuspendLayout()
        Me.pnlQuery.SuspendLayout()
        Me.pnlCurrentDatabase.SuspendLayout()
        Me.SuspendLayout()
        '
        'sbrMain
        '
        Me.sbrMain.Location = New System.Drawing.Point(0, 457)
        Me.sbrMain.Name = "sbrMain"
        Me.sbrMain.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbrMessage})
        Me.sbrMain.ShowPanels = True
        Me.sbrMain.Size = New System.Drawing.Size(824, 24)
        Me.sbrMain.TabIndex = 0
        '
        'sbrMessage
        '
        Me.sbrMessage.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbrMessage.Text = "Ready"
        Me.sbrMessage.Width = 808
        '
        'mnuMain
        '
        Me.mnuMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.MenuItem3})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileConnect, Me.mnuFileDisconnect, Me.mnuFileBar1, Me.mnuFileExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuFileConnect
        '
        Me.mnuFileConnect.Index = 0
        Me.mnuFileConnect.Text = "Connect..."
        '
        'mnuFileDisconnect
        '
        Me.mnuFileDisconnect.Index = 1
        Me.mnuFileDisconnect.Text = "Disconnect"
        '
        'mnuFileBar1
        '
        Me.mnuFileBar1.Index = 2
        Me.mnuFileBar1.Text = "-"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Index = 3
        Me.mnuFileExit.Text = "Exit"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuQueryRun})
        Me.MenuItem3.Text = "&Query"
        '
        'mnuQueryRun
        '
        Me.mnuQueryRun.Index = 0
        Me.mnuQueryRun.Text = "Run current query"
        '
        'pnlResults
        '
        Me.pnlResults.Controls.AddRange(New System.Windows.Forms.Control() {Me.grdResults})
        Me.pnlResults.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlResults.Location = New System.Drawing.Point(0, 337)
        Me.pnlResults.Name = "pnlResults"
        Me.pnlResults.Size = New System.Drawing.Size(824, 120)
        Me.pnlResults.TabIndex = 1
        '
        'grdResults
        '
        Me.grdResults.AutoSizeMinHeight = 10
        Me.grdResults.AutoSizeMinWidth = 10
        Me.grdResults.AutoStretchColumnsToFitWidth = False
        Me.grdResults.CellSet = Nothing
        Me.grdResults.Cols = 0
        Me.grdResults.ContainerControlCursor = System.Windows.Forms.Cursors.Default
        Me.grdResults.ContainerControlToolTipText = ""
        Me.grdResults.CustomScrollArea = New System.Drawing.Size(0, 0)
        Me.grdResults.CustomScrollPosition = New System.Drawing.Point(0, 0)
        Me.grdResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdResults.EnableRowColSpan = True
        Me.grdResults.FixedCols = 0
        Me.grdResults.FixedRows = 0
        Me.grdResults.GridScrollPosition = New System.Drawing.Point(0, 0)
        Me.grdResults.Name = "grdResults"
        Me.grdResults.Redraw = True
        Me.grdResults.Rows = 0
        Me.grdResults.Size = New System.Drawing.Size(824, 120)
        Me.grdResults.TabIndex = 1
        Me.grdResults.ToolTipActive = True
        '
        'sptResults
        '
        Me.sptResults.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.sptResults.Location = New System.Drawing.Point(0, 329)
        Me.sptResults.Name = "sptResults"
        Me.sptResults.Size = New System.Drawing.Size(824, 8)
        Me.sptResults.TabIndex = 2
        Me.sptResults.TabStop = False
        '
        'tbrMain
        '
        Me.tbrMain.AutoSize = False
        Me.tbrMain.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbConnect, Me.tbbSeparator2, Me.tbbQueryRun})
        Me.tbrMain.ButtonSize = New System.Drawing.Size(21, 19)
        Me.tbrMain.DropDownArrows = True
        Me.tbrMain.ImageList = Me.imlToolbar
        Me.tbrMain.Name = "tbrMain"
        Me.tbrMain.ShowToolTips = True
        Me.tbrMain.Size = New System.Drawing.Size(824, 29)
        Me.tbrMain.TabIndex = 3
        '
        'tbbConnect
        '
        Me.tbbConnect.ImageIndex = 0
        Me.tbbConnect.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        '
        'tbbSeparator2
        '
        Me.tbbSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tbbQueryRun
        '
        Me.tbbQueryRun.ImageIndex = 1
        '
        'imlToolbar
        '
        Me.imlToolbar.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.imlToolbar.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlToolbar.ImageStream = CType(resources.GetObject("imlToolbar.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlToolbar.TransparentColor = System.Drawing.Color.Magenta
        '
        'pnlMetaData
        '
        Me.pnlMetaData.Controls.AddRange(New System.Windows.Forms.Control() {Me.pnlSBrowser, Me.Splitter2, Me.pnlMDBrowser})
        Me.pnlMetaData.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMetaData.Location = New System.Drawing.Point(0, 29)
        Me.pnlMetaData.Name = "pnlMetaData"
        Me.pnlMetaData.Size = New System.Drawing.Size(200, 300)
        Me.pnlMetaData.TabIndex = 6
        '
        'pnlSBrowser
        '
        Me.pnlSBrowser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSBrowser.Controls.AddRange(New System.Windows.Forms.Control() {Me.tvwSyntax, Me.Label1})
        Me.pnlSBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSBrowser.Location = New System.Drawing.Point(0, 176)
        Me.pnlSBrowser.Name = "pnlSBrowser"
        Me.pnlSBrowser.Size = New System.Drawing.Size(200, 124)
        Me.pnlSBrowser.TabIndex = 2
        '
        'tvwSyntax
        '
        Me.tvwSyntax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tvwSyntax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwSyntax.ImageIndex = -1
        Me.tvwSyntax.Location = New System.Drawing.Point(0, 16)
        Me.tvwSyntax.Name = "tvwSyntax"
        Me.tvwSyntax.SelectedImageIndex = -1
        Me.tvwSyntax.Size = New System.Drawing.Size(196, 104)
        Me.tvwSyntax.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(196, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Syntax Browser"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 168)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(200, 8)
        Me.Splitter2.TabIndex = 1
        Me.Splitter2.TabStop = False
        '
        'pnlMDBrowser
        '
        Me.pnlMDBrowser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMDBrowser.Controls.AddRange(New System.Windows.Forms.Control() {Me.tvwMetadata, Me.chkUseSchemaRowsets, Me.lblMetaData})
        Me.pnlMDBrowser.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMDBrowser.Name = "pnlMDBrowser"
        Me.pnlMDBrowser.Size = New System.Drawing.Size(200, 168)
        Me.pnlMDBrowser.TabIndex = 0
        '
        'tvwMetadata
        '
        Me.tvwMetadata.AllowDrop = True
        Me.tvwMetadata.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tvwMetadata.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwMetadata.ImageList = Me.imlMetadata
        Me.tvwMetadata.Location = New System.Drawing.Point(0, 16)
        Me.tvwMetadata.Name = "tvwMetadata"
        Me.tvwMetadata.Nodes.AddRange(New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("(Disconnected)", 4, 4)})
        Me.tvwMetadata.Size = New System.Drawing.Size(196, 132)
        Me.tvwMetadata.TabIndex = 3
        '
        'imlMetadata
        '
        Me.imlMetadata.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imlMetadata.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlMetadata.ImageStream = CType(resources.GetObject("imlMetadata.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlMetadata.TransparentColor = System.Drawing.Color.Transparent
        '
        'chkUseSchemaRowsets
        '
        Me.chkUseSchemaRowsets.Checked = True
        Me.chkUseSchemaRowsets.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseSchemaRowsets.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.chkUseSchemaRowsets.Location = New System.Drawing.Point(0, 148)
        Me.chkUseSchemaRowsets.Name = "chkUseSchemaRowsets"
        Me.chkUseSchemaRowsets.Size = New System.Drawing.Size(196, 16)
        Me.chkUseSchemaRowsets.TabIndex = 2
        Me.chkUseSchemaRowsets.Text = "Use Schema Rowsets"
        '
        'lblMetaData
        '
        Me.lblMetaData.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMetaData.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblMetaData.Name = "lblMetaData"
        Me.lblMetaData.Size = New System.Drawing.Size(196, 16)
        Me.lblMetaData.TabIndex = 0
        Me.lblMetaData.Text = "Meta Data Browser"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(200, 29)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(8, 300)
        Me.Splitter1.TabIndex = 7
        Me.Splitter1.TabStop = False
        '
        'pnlQuery
        '
        Me.pnlQuery.Controls.AddRange(New System.Windows.Forms.Control() {Me.rtfQuery, Me.pnlCurrentDatabase})
        Me.pnlQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlQuery.Location = New System.Drawing.Point(208, 29)
        Me.pnlQuery.Name = "pnlQuery"
        Me.pnlQuery.Size = New System.Drawing.Size(616, 300)
        Me.pnlQuery.TabIndex = 8
        '
        'rtfQuery
        '
        Me.rtfQuery.AcceptsTab = True
        Me.rtfQuery.AllowDrop = True
        Me.rtfQuery.DetectUrls = False
        Me.rtfQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtfQuery.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtfQuery.HideSelection = False
        Me.rtfQuery.Location = New System.Drawing.Point(0, 24)
        Me.rtfQuery.Name = "rtfQuery"
        Me.rtfQuery.Size = New System.Drawing.Size(616, 276)
        Me.rtfQuery.TabIndex = 6
        Me.rtfQuery.Text = ""
        '
        'pnlCurrentDatabase
        '
        Me.pnlCurrentDatabase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlCurrentDatabase.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblCurrentDatabase})
        Me.pnlCurrentDatabase.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCurrentDatabase.Name = "pnlCurrentDatabase"
        Me.pnlCurrentDatabase.Size = New System.Drawing.Size(616, 24)
        Me.pnlCurrentDatabase.TabIndex = 5
        '
        'lblCurrentDatabase
        '
        Me.lblCurrentDatabase.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblCurrentDatabase.Location = New System.Drawing.Point(6, 6)
        Me.lblCurrentDatabase.Name = "lblCurrentDatabase"
        Me.lblCurrentDatabase.Size = New System.Drawing.Size(599, 12)
        Me.lblCurrentDatabase.TabIndex = 0
        Me.lblCurrentDatabase.Text = "Current Database: "
        Me.lblCurrentDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainDialog
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(824, 481)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.pnlQuery, Me.Splitter1, Me.pnlMetaData, Me.tbrMain, Me.sptResults, Me.pnlResults, Me.sbrMain})
        Me.Menu = Me.mnuMain
        Me.Name = "MainDialog"
        Me.Text = "ADOMD.NET Sample Application"
        CType(Me.sbrMessage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlResults.ResumeLayout(False)
        Me.pnlMetaData.ResumeLayout(False)
        Me.pnlSBrowser.ResumeLayout(False)
        Me.pnlMDBrowser.ResumeLayout(False)
        Me.pnlQuery.ResumeLayout(False)
        Me.pnlCurrentDatabase.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "== Properties ========================================================="
    Public Property UseSchemaRowsets() As Boolean
        ' If UseSchemaRowsets is set to True, the various GetSchemaRowset_xxx
        ' functions defined in the AdomdHelper class are used to retrieve
        ' meta data.
        ' If UseSchemaRowsets is set to False, the ADOMD.NET object model
        ' is used to retrieve meta data.
        Get
            UseSchemaRowsets = chkUseSchemaRowsets.Checked
        End Get
        Set(ByVal value As Boolean)
            If chkUseSchemaRowsets.Checked <> value Then
                chkUseSchemaRowsets.Checked = value
            End If
        End Set
    End Property
#End Region

#Region "== Methods ============================================================"
    Public Sub UpdateStatus(Optional ByVal message As String = "")
        ' Helper subroutine to manage display of messages in the status bar.
        With Me
            .sbrMessage.Text = message
            If m_Helper.IsConnected(m_Connection) AndAlso _
                m_Connection.Database <> "" Then
                .lblCurrentDatabase.Text = "Current Database: " & _
                    m_Connection.Database
            Else
                .lblCurrentDatabase.Text = "Current Database: " & _
                    "<None>"
            End If
            .Refresh()
        End With
    End Sub

    Public Sub Connect()
        Dim frmCD As New ConnectionDialog()

        Me.Cursor = Cursors.WaitCursor
        Me.UpdateStatus("Connecting...")
        frmCD.ShowDialog(Me)

        If m_Helper.IsConnected(m_Connection) Then
            tbbConnect.Pushed = True
            pnlMetaData.Enabled = True
            pnlResults.Enabled = True
            tbbQueryRun.Enabled = True

            LoadMetadataBrowser()
            LoadSyntaxBrowser()

            Me.UpdateStatus("Connection string: " & m_Connection.ConnectionString)
        Else
            Disconnect()
        End If

        Me.Cursor = Cursors.Default

    End Sub

    Public Sub Disconnect()
        ' Helper routine to disconnect the active AdomdConnection object and
        ' to update the form accordingly.

        Me.Cursor = Cursors.WaitCursor
        Me.UpdateStatus("Disconnecting...")

        m_Helper.Disconnect(m_Connection)

        If Not m_Helper.IsConnected(m_Connection) Then
            tbbConnect.Pushed = False

            ClearMetadataBrowser()
            ClearSyntaxBrowser()

            pnlMetaData.Enabled = False
            pnlResults.Enabled = False
            tbbQueryRun.Enabled = False
        End If

        Me.UpdateStatus("Disconnected")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Show()
        Me.UpdateStatus("Disconnected")
        Me.Connect()
    End Sub

    Private Sub tvwMetadata_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwMetadata.MouseDown
        ' Drag & drop routine for moving unique names of objects shown in the 
        ' Metadata Browser to the Query Pane.
        If (e.Button And MouseButtons.Left) Then
            tvwMetadata.SelectedNode = tvwMetadata.GetNodeAt(New Point(e.X, e.Y))
            If Not (tvwMetadata.SelectedNode Is Nothing) AndAlso (tvwMetadata.SelectedNode.Tag <> "") Then
                tvwMetadata.DoDragDrop(tvwMetadata.SelectedNode.Tag, DragDropEffects.Copy Or DragDropEffects.None)
            End If
        End If
    End Sub

    Private Sub tvwMetadata_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwMetadata.AfterSelect
        ' Retrieve the database node for the selected item, and change the
        ' active database accordingly.
        Dim l_Node As TreeNode

        If Not (e.Node Is Nothing) Then
            Me.Cursor = Cursors.WaitCursor

            l_Node = GetDatabaseNode(e.Node)

            If Not l_Node Is Nothing AndAlso _
                l_Node.Text <> m_Connection.Database Then

                ' Attempt to change the active database.
                Try
                    m_Connection.ChangeDatabase(l_Node.Text)
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "changing the active database")
                End Try
            End If

            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ClearMetadataBrowser()
        ' Helper routine to clear the Metadata Browser when disconnected.
        Dim l_Node As TreeNode

        tvwMetadata.Nodes.Clear()
        l_Node = New TreeNode("Disconnected", MetadataImages.Server_Disconnected, MetadataImages.Server_Disconnected)
        tvwMetadata.Nodes.Add(l_Node)
    End Sub

    Private Sub LoadMetadataBrowser()
        ' The metadata browser pane only loads on demand. As the user clicks each 
        ' collection node, if the node has not yet been loaded it will then
        ' call the appropriate LoadxxxxMetadata routine to retrieve meta data
        ' and load child nodes.
        Dim l_Node As TreeNode
        Dim l_ChildNode As TreeNode

        Me.Cursor = Cursors.WaitCursor
        Me.UpdateStatus("Loading metadata...")

        l_Node = LoadServerMetadata()
        If m_Helper.IsConnected(m_Connection) Then
            ' More information can be shown through schema rowsets.
            ' The first child node is always the Databases node.
            LoadDatabaseMetadata(l_Node.Nodes(0))
        End If

        Me.UpdateStatus()
        Me.Cursor = Cursors.Default
    End Sub

    Private Function LoadServerMetadata() As TreeNode
        Dim l_Node As New TreeNode()
        Dim l_ChildNode As TreeNode
        Dim intIndex As Integer

        Me.tvwMetadata.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            ' Add a "connected" node and start loading databases.
            Try
                With l_Node
                    .Text = m_Connection.ConnectionString
                    .Tag = ""
                    .ImageIndex = MetadataImages.Server_Connected
                    .SelectedImageIndex = MetadataImages.Server_Connected

                    ' Add Databases node.
                    intIndex = .Nodes.Add(New TreeNode("Databases", _
                        MetadataImages.Folder_Closed, _
                        MetadataImages.Folder_Opened))

                End With
            Catch ex As Exception
                ' Display the error.
                m_Helper.DisplayException(ex, "retrieving meta data")
            End Try
        Else
            ' Add a "disconnected" node.
            With l_Node
                .Text = "(Disconnected)"
                .Tag = ""
                .ImageIndex = MetadataImages.Server_Disconnected
                .SelectedImageIndex = MetadataImages.Server_Disconnected
            End With
        End If

        Me.tvwMetadata.Nodes.Add(l_Node)

        LoadServerMetadata = l_Node
    End Function

    Private Sub LoadDatabaseMetadata(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode

        parentNode.Nodes.Clear()

        ' Currently, there is no way through the ADOMD.NET object model
        ' to retrieve a list of databases available to a given
        ' AdomdConnection - you must use the DBSCHEMA_CATALOGS
        ' schema rowset to list databases.
        If m_Helper.IsConnected(m_Connection) Then
            Try
                ' Retrieve the DBSCHEMA_CATALOGS schema rowset.
                Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Catalogs( _
                    m_Connection)

                ' Iterate through the schema rowset, adding each database found 
                ' to the treeview.
                If Not (objTable Is Nothing) Then
                    Dim l_Row As DataRow

                    For Each l_Row In objTable.Rows
                        l_Node = New TreeNode()
                        With l_Node
                            .Text = l_Row("CATALOG_NAME")
                            .Tag = l_Row("CATALOG_NAME")
                            .ImageIndex = MetadataImages.Database
                            .SelectedImageIndex = MetadataImages.Database

                            ' Add placeholder child nodes.
                            AddNodeWithEmpty(l_Node, "Cubes")

                        End With
                        parentNode.Nodes.Add(l_Node)
                    Next
                End If
            Catch ex As Exception
                ' Display the error.
                m_Helper.DisplayException(ex, "retrieving meta data")
            End Try
        End If
    End Sub

    Private Sub LoadCubeMetadata(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode
        Dim objCube As CubeDef

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            If Me.UseSchemaRowsets Then
                Try
                    ' Retrive the MDSCHEMA_CUBES schema rowset, filtered for the
                    ' appropriate database.
                    Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Cubes( _
                        m_Connection, _
                        GetDatabaseNode(parentNode).Text)

                    ' Iterate through the schema rowset, adding each database found 
                    ' to the treeview.
                    If Not (objTable Is Nothing) Then
                        Dim l_Row As DataRow
                        For Each l_Row In objTable.Rows

                            l_Node = New TreeNode()
                            With l_Node
                                .Text = l_Row("CUBE_NAME")
                                .Tag = "[" & l_Row("CUBE_NAME") & "]"
                                If l_Row("CUBE_TYPE") = "VIRTUAL CUBE" Then
                                    .ImageIndex = MetadataImages.VirtualCube
                                    .SelectedImageIndex = MetadataImages.VirtualCube
                                Else
                                    .ImageIndex = MetadataImages.Cube
                                    .SelectedImageIndex = MetadataImages.Cube
                                End If
                            End With

                            AddNodeWithEmpty(l_Node, "Dimensions")
                            AddNodeWithEmpty(l_Node, "Measures")
                            AddNodeWithEmpty(l_Node, "Named Sets")

                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            Else
                Try
                    ' First, switch the AdomdConnection object to the 
                    ' correct database.
                    m_Connection.ChangeDatabase(GetDatabaseNode(parentNode).Tag)

                    ' Next, iterate through the Cubes collection of the 
                    ' AdomdConnection object.
                    For Each objCube In m_Connection.Cubes
                        l_Node = New TreeNode()
                        With l_Node
                            .Text = objCube.Caption
                            .Tag = "[" & objCube.Name & "]"
                            .ImageIndex = MetadataImages.Cube
                            .SelectedImageIndex = MetadataImages.Cube
                            AddNodeWithEmpty(l_Node, "Dimensions")
                            AddNodeWithEmpty(l_Node, "Measures")
                            AddNodeWithEmpty(l_Node, "Named Sets")
                        End With

                        parentNode.Nodes.Add(l_Node)
                    Next
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            End If
        End If
    End Sub


    Private Sub LoadMeasureMetadata(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode, objCube As CubeDef
        objCube = m_Connection.Cubes(GetCubeNode(parentNode).Text)

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            If Me.UseSchemaRowsets Then
                Try
                    ' Retrieve the MDSCHEMA_MEASURES schema rowset for the specified cube.
                    Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Measures(m_Connection, _
                        GetDatabaseNode(parentNode).Text, _
                        Nothing, _
                        GetCubeNode(parentNode).Text, _
                        Nothing, _
                        Nothing)

                    If Not (objTable Is Nothing) Then
                        Dim l_Row As DataRow
                        For Each l_Row In objTable.Rows
                            l_Node = New TreeNode()
                            With l_Node
                                .Text = l_Row("MEASURE_NAME")
                                .Tag = l_Row("MEASURE_UNIQUE_NAME")
                                .ImageIndex = MetadataImages.Measure
                                .SelectedImageIndex = MetadataImages.Measure
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            Else
                Try
                    Dim objMeasure As Measure

                    objCube = m_Connection.Cubes(GetCubeNode(parentNode).Text)

                    For Each objMeasure In objCube.Measures
                        l_Node = New TreeNode()
                        With l_Node
                            .Text = objMeasure.Caption
                            .Tag = objMeasure.UniqueName
                            .ImageIndex = MetadataImages.Measure
                            .SelectedImageIndex = MetadataImages.Measure
                        End With
                        parentNode.Nodes.Add(l_Node)
                    Next
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            End If
        End If
    End Sub

    Private Sub LoadDimensionMetadata(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            If Me.UseSchemaRowsets Then
                Try

                    Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Dimensions(m_Connection, _
                        GetDatabaseNode(parentNode).Text, _
                        Nothing, _
                        GetCubeNode(parentNode).Text, _
                        Nothing, _
                        Nothing)
                    If Not (objTable Is Nothing) Then
                        Dim l_Row As DataRow

                        For Each l_Row In objTable.Rows
                            l_Node = New TreeNode()
                            With l_Node
                                .Text = l_Row("DIMENSION_NAME")
                                .Tag = "[" & l_Row("DIMENSION_NAME") & "]"
                                .ImageIndex = MetadataImages.Dimension
                                .SelectedImageIndex = MetadataImages.Dimension
                                AddNodeWithEmpty(l_Node, "Hierarchies")
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            Else
                Try
                    Dim objDim As Dimension, objCube As CubeDef
                    objCube = m_Connection.Cubes(GetCubeNode(parentNode).Text)
                    For Each objDim In objCube.Dimensions
                        l_Node = New TreeNode()
                        With l_Node
                            .Text = objDim.Caption
                            .Tag = objDim.UniqueName
                            .ImageIndex = MetadataImages.Dimension
                            .SelectedImageIndex = MetadataImages.Dimension
                            AddNodeWithEmpty(l_Node, "Hierarchies")
                        End With
                        parentNode.Nodes.Add(l_Node)
                    Next
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            End If
        End If
    End Sub

    Private Sub LoadHierarchyMetadata(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            If Me.UseSchemaRowsets Then
                Try
                    Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Hierarchies(m_Connection, _
                        GetDatabaseNode(parentNode).Text, _
                        Nothing, _
                        GetCubeNode(parentNode).Text, _
                        GetDimensionNode(parentNode).Tag, _
                        Nothing, _
                        Nothing)

                    If Not (objTable Is Nothing) Then
                        Dim l_Row As DataRow
                        For Each l_Row In objTable.Rows
                            l_Node = New TreeNode()
                            With l_Node
                                If System.Convert.IsDBNull(l_Row("HIERARCHY_CAPTION")) Then
                                    .Text = GetDimensionNode(parentNode).Text
                                Else
                                    .Text = l_Row("HIERARCHY_CAPTION")
                                End If
                                .Tag = l_Row("HIERARCHY_UNIQUE_NAME")
                                .ImageIndex = MetadataImages.Hierarchy
                                .SelectedImageIndex = MetadataImages.Hierarchy
                                AddNodeWithEmpty(l_Node, "Levels")
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            Else
                Try
                    Dim objDimension As Dimension, objHierarchy As Hierarchy
                    objDimension = m_Connection.Cubes(GetCubeNode(parentNode).Text).Dimensions(GetDimensionNode(parentNode).Text)

                    If Not (objDimension Is Nothing) Then
                        For Each objHierarchy In objDimension.Hierarchies
                            l_Node = New TreeNode()
                            With l_Node
                                .Text = objHierarchy.Caption
                                .Tag = objHierarchy.UniqueName
                                .ImageIndex = MetadataImages.Hierarchy
                                .SelectedImageIndex = MetadataImages.Hierarchy
                                AddNodeWithEmpty(l_Node, "Levels")
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            End If
        End If
    End Sub

    Private Sub LoadLevelMetadata(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode
        Dim intLevelNumber As Integer
        Dim intNoAllLevel As Integer = 1

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            If Me.UseSchemaRowsets Then
                Try

                    Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Levels(m_Connection, _
                        GetDatabaseNode(parentNode).Text, _
                        Nothing, _
                        GetCubeNode(parentNode).Text, _
                        GetDimensionNode(parentNode).Tag, _
                        parentNode.Parent.Tag, _
                        Nothing, _
                        Nothing)

                    If Not (objTable Is Nothing) Then
                        Dim l_Row As DataRow
                        For Each l_Row In objTable.Rows
                            If (l_Row("LEVEL_TYPE") And LevelTypeEnum.All) Then
                                intNoAllLevel = 0
                                Exit For
                            End If
                        Next

                        For Each l_Row In objTable.Rows
                            l_Node = New TreeNode()
                            With l_Node
                                .Text = l_Row("LEVEL_CAPTION")
                                .Tag = l_Row("LEVEL_UNIQUE_NAME")
                                intLevelNumber = System.Convert.ToInt32(l_Row("LEVEL_NUMBER"))
                                If intLevelNumber < 17 Then
                                    .ImageIndex = MetadataImages.LevelAll + intLevelNumber + intNoAllLevel
                                    .SelectedImageIndex = MetadataImages.LevelAll + intLevelNumber + intNoAllLevel
                                Else
                                    .ImageIndex = MetadataImages.Level16
                                    .SelectedImageIndex = MetadataImages.Level16
                                End If
                                AddNodeWithEmpty(l_Node, "Members")
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            Else
                Try
                    Dim objLevel As Level, objHierarchy As Hierarchy
                    objHierarchy = m_Connection.Cubes(GetCubeNode(parentNode).Text). _
                        Dimensions(GetDimensionNode(parentNode).Text). _
                        Hierarchies(GetHierarchyNode(parentNode).Text)

                    If Not (objHierarchy Is Nothing) Then
                        For Each objLevel In objHierarchy.Levels
                            If (objLevel.LevelType And LevelTypeEnum.All) Then
                                intNoAllLevel = 0
                            End If
                        Next

                        For Each objLevel In objHierarchy.Levels
                            l_Node = New TreeNode()
                            With l_Node
                                .Text = objLevel.Caption
                                .Tag = objLevel.UniqueName
                                If objLevel.LevelNumber < 17 Then
                                    .ImageIndex = MetadataImages.LevelAll + objLevel.LevelNumber + intNoAllLevel
                                    .SelectedImageIndex = MetadataImages.LevelAll + objLevel.LevelNumber + intNoAllLevel
                                Else
                                    .ImageIndex = MetadataImages.Level16
                                    .SelectedImageIndex = MetadataImages.Level16
                                End If
                                AddNodeWithEmpty(l_Node, "Members")
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            End If
        End If
    End Sub

    Private Sub LoadMemberMetadata(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            If Me.UseSchemaRowsets Then
                Try
                    Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Members(m_Connection, _
                        GetDatabaseNode(parentNode).Text, _
                        Nothing, _
                        GetCubeNode(parentNode).Text, _
                        GetDimensionNode(parentNode).Tag, _
                        GetHierarchyNode(parentNode).Tag, _
                        parentNode.Parent.Tag, _
                        Nothing, _
                        Nothing, _
                        Nothing, _
                        Nothing, _
                        Nothing, _
                        Nothing)

                    If Not (objTable Is Nothing) Then
                        Dim l_Row As DataRow
                        For Each l_Row In objTable.Rows
                            l_Node = New TreeNode()
                            With l_Node
                                .Text = l_Row("MEMBER_CAPTION")
                                .Tag = l_Row("MEMBER_UNIQUE_NAME")
                                .ImageIndex = MetadataImages.Member
                                .SelectedImageIndex = MetadataImages.Member
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            Else
                Try
                    Dim objMemberCollection As MemberCollection, _
                        objMember As Member
                    objMemberCollection = m_Connection.Cubes(GetCubeNode(parentNode).Text). _
                        Dimensions(GetDimensionNode(parentNode).Text). _
                        Hierarchies(GetHierarchyNode(parentNode).Text). _
                        Levels(parentNode.Parent.Text).GetMembers()

                    If Not (objMemberCollection Is Nothing) Then
                        For Each objMember In objMemberCollection
                            l_Node = New TreeNode()
                            With l_Node
                                .Text = objMember.Caption
                                .Tag = objMember.UniqueName
                                If objMember.Type = MemberTypeEnum.Formula Then
                                    .ImageIndex = MetadataImages.Member_Formula
                                    .SelectedImageIndex = MetadataImages.Member_Formula
                                Else
                                    .ImageIndex = MetadataImages.Member
                                    .SelectedImageIndex = MetadataImages.Member
                                End If
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            End If
        End If
    End Sub


    Private Sub LoadNamedSetMetadata(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode
        Dim objCube As CubeDef

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            If Me.UseSchemaRowsets Then
                Try
                    ' Retrieve the schema rowset.
                    Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Sets(m_Connection, _
                        GetDatabaseNode(parentNode).Text, _
                        Nothing, _
                        GetCubeNode(parentNode).Text)

                    If Not (objTable Is Nothing) Then
                        Dim l_Row As DataRow
                        For Each l_Row In objTable.Rows

                            l_Node = New TreeNode()
                            With l_Node
                                .Text = l_Row("SET_NAME")
                                .Tag = "[" & l_Row("SET_NAME") & "]"
                                .ImageIndex = MetadataImages.NamedSet
                                .SelectedImageIndex = MetadataImages.NamedSet
                            End With
                            parentNode.Nodes.Add(l_Node)
                        Next
                    End If
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            Else
                Try
                    Dim objNamedSet As NamedSet
                    objCube = m_Connection.Cubes(GetCubeNode(parentNode).Text)
                    For Each objNamedSet In objCube.NamedSets
                        l_Node = New TreeNode()
                        With l_Node
                            .Text = objNamedSet.Name
                            .Tag = "[" & objNamedSet.Name & "]"
                            .ImageIndex = MetadataImages.NamedSet
                            .SelectedImageIndex = MetadataImages.NamedSet
                        End With
                        parentNode.Nodes.Add(l_Node)
                    Next
                Catch ex As Exception
                    ' Display the error.
                    m_Helper.DisplayException(ex, "retrieving meta data")
                End Try
            End If
        End If
    End Sub

    Private Function HasEmptyNode(ByRef nodeToCheck As TreeNode) As Boolean
        HasEmptyNode = ((nodeToCheck.Nodes.Count = 1) AndAlso (nodeToCheck.Nodes(0).Text = "Loading..."))
    End Function

    Private Sub AddNodeWithEmpty(ByVal parentNode As TreeNode, ByVal nodeText As String, Optional ByVal imageIndex As Integer = MetadataImages.Folder_Closed, Optional ByVal selectedImageIndex As Integer = MetadataImages.Folder_Opened)
        Dim l_Node As New TreeNode(nodeText, imageIndex, selectedImageIndex)
        l_Node.Nodes.Add(New TreeNode("Loading..."))
        parentNode.Nodes.Add(l_Node)
    End Sub

    Private Sub tvwMetadata_BeforeExpand(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwMetadata.BeforeExpand
        Dim l_Node As TreeNode = e.Node

        Me.Cursor = Cursors.WaitCursor

        If HasEmptyNode(l_Node) Then
            Select Case l_Node.Text
                Case "Databases"
                    LoadDatabaseMetadata(l_Node)
                Case "Cubes"
                    LoadCubeMetadata(l_Node)
                Case "Dimensions"
                    LoadDimensionMetadata(l_Node)
                Case "Measures"
                    LoadMeasureMetadata(l_Node)
                Case "Hierarchies"
                    LoadHierarchyMetadata(l_Node)
                Case "Members"
                    LoadMemberMetadata(l_Node)
                Case "Levels"
                    LoadLevelMetadata(l_Node)
                Case "Named Sets"
                    LoadNamedSetMetadata(l_Node)
                Case Else
                    ' Do nothing
            End Select
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function GetDatabaseNode(ByVal node As TreeNode) As TreeNode
        Dim l_Node As TreeNode
        ' If a collection, get its parent to identify location.
        If node.ImageIndex = MetadataImages.Folder_Closed Then
            l_Node = node.Parent
        Else
            l_Node = node
        End If

        Select Case l_Node.ImageIndex
            Case MetadataImages.Folder_Closed
                ' A collection node
                GetDatabaseNode = Nothing
            Case MetadataImages.Server_Connected
                ' This should never happen...
                GetDatabaseNode = Nothing
            Case MetadataImages.Database
                GetDatabaseNode = l_Node
            Case Else
                ' Just walk back up the tree recursively until the
                ' node is located.
                GetDatabaseNode = GetDatabaseNode(l_Node.Parent)
        End Select
    End Function

    Private Function GetCubeNode(ByVal node As TreeNode) As TreeNode
        Dim l_Node As TreeNode
        ' If a collection, get its parent to identify location.
        If node.ImageIndex = MetadataImages.Folder_Closed Then
            l_Node = node.Parent
        Else
            l_Node = node
        End If

        Select Case l_Node.ImageIndex
            Case MetadataImages.Folder_Closed
                ' A collection node
                GetCubeNode = Nothing
            Case MetadataImages.Server_Connected, MetadataImages.Database
                GetCubeNode = Nothing
            Case MetadataImages.Cube, MetadataImages.VirtualCube
                GetCubeNode = l_Node
            Case Else
                ' Just walk back up the tree recursively until the
                ' node is located.
                GetCubeNode = GetCubeNode(l_Node.Parent)
        End Select
    End Function

    Private Function GetDimensionNode(ByVal node As TreeNode) As TreeNode
        Dim l_Node As TreeNode
        ' If a collection, get its parent to identify location.
        If node.ImageIndex = MetadataImages.Folder_Closed Then
            l_Node = node.Parent
        Else
            l_Node = node
        End If

        Select Case l_Node.ImageIndex
            Case MetadataImages.Folder_Closed
                ' A collection node
                GetDimensionNode = Nothing
            Case MetadataImages.Server_Connected, _
                MetadataImages.Database, _
                MetadataImages.Cube, _
                MetadataImages.VirtualCube

                GetDimensionNode = Nothing
            Case MetadataImages.Dimension
                GetDimensionNode = l_Node
            Case Else
                ' Just walk back up the tree recursively until the
                ' node is located.
                GetDimensionNode = GetDimensionNode(l_Node.Parent)
        End Select
    End Function

    Private Function GetHierarchyNode(ByVal node As TreeNode) As TreeNode
        Dim l_Node As TreeNode
        ' If a collection, get its parent to identify location.
        If node.ImageIndex = MetadataImages.Folder_Closed Then
            l_Node = node.Parent
        Else
            l_Node = node
        End If

        Select Case l_Node.ImageIndex
            Case MetadataImages.Folder_Closed
                ' A collection node
                GetHierarchyNode = Nothing
            Case MetadataImages.Server_Connected, _
                MetadataImages.Database, _
                MetadataImages.Cube, _
                MetadataImages.VirtualCube, _
                MetadataImages.Dimension

                GetHierarchyNode = Nothing
            Case MetadataImages.Hierarchy
                GetHierarchyNode = l_Node
            Case Else
                ' Just walk back up the tree recursively until the
                ' node is located.
                GetHierarchyNode = GetHierarchyNode(l_Node.Parent)
        End Select
    End Function

    Private Sub tvwSyntax_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwSyntax.MouseDown
        Dim l_Text As String

        ' Parse the syntax information out of the Tag property of the node
        ' and start the drag & drop operation.
        If (e.Button And MouseButtons.Left) Then
            tvwSyntax.SelectedNode = tvwSyntax.GetNodeAt(New Point(e.X, e.Y))
            If Not (tvwSyntax.SelectedNode Is Nothing) AndAlso (tvwSyntax.SelectedNode.Tag <> "") Then
                l_Text = tvwSyntax.SelectedNode.Tag
                l_Text = l_Text.Substring(l_Text.IndexOf(vbTab) + 1)
                tvwSyntax.DoDragDrop(l_Text, DragDropEffects.Copy Or DragDropEffects.None)
            End If
        End If
    End Sub

    Private Sub ClearSyntaxBrowser()
        Dim l_Node As TreeNode

        tvwSyntax.Nodes.Clear()
        l_Node = New TreeNode("Disconnected")
        tvwSyntax.Nodes.Add(l_Node)

    End Sub

    Private Sub LoadSyntaxBrowser()
        ' The metadata browser pane only loads on demand.
        Dim l_Node As TreeNode
        Dim l_ChildNode As TreeNode

        Me.UpdateStatus("Loading syntax information...")
        Me.Cursor = Cursors.WaitCursor

        tvwSyntax.Nodes.Clear()

        l_Node = New TreeNode("Syntax")
        LoadSyntax(l_Node)
        tvwSyntax.Nodes.Add(l_Node)

        Me.Cursor = Cursors.Default
        Me.UpdateStatus()
    End Sub

    Private Sub LoadSyntax(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode

        ' Add functions
        l_Node = New TreeNode("Functions")
        LoadFunctionSyntax(l_Node)
        parentNode.Nodes.Add(l_Node)

        ' Add keywords
        l_Node = New TreeNode("Keywords")
        LoadKeywordSyntax(l_Node)
        parentNode.Nodes.Add(l_Node)

    End Sub

    Private Sub LoadKeywordSyntax(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode

        Dim l_Exists As Boolean

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            Dim l_Row As DataRow

            ' Retrieve the schema rowset.
            Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Keywords( _
                m_Connection)

            ' Add keywords into the treeview.
            For Each l_Row In objTable.Rows
                l_Node = New TreeNode()
                With l_Node
                    .Text = l_Row("Keyword")
                    .Tag = l_Row("Keyword")
                End With
                parentNode.Nodes.Add(l_Node)
            Next
        End If

    End Sub

    Private Sub LoadFunctionSyntax(ByVal parentNode As TreeNode)
        Dim l_Node As TreeNode
        Dim objCategoryNode As TreeNode

        Dim l_Exists As Boolean

        parentNode.Nodes.Clear()

        If m_Helper.IsConnected(m_Connection) Then
            Dim l_Row As DataRow

            ' Retrieve the schema rowset.
            Dim objTable As DataTable = m_Helper.GetSchemaDataSet_Functions( _
                m_Connection)

            ' First, loop through and extract categories from the schema rowset.
            For Each l_Row In objTable.Rows
                l_Exists = False
                For Each l_Node In parentNode.Nodes
                    If l_Node.Text = l_Row("INTERFACE_NAME") Then
                        l_Exists = True
                        Exit For
                    End If
                Next

                If l_Exists = False Then
                    l_Node = New TreeNode(l_Row("INTERFACE_NAME"))
                    parentNode.Nodes.Add(l_Node)
                End If
            Next

            ' Add each function definition, including syntax, into
            ' the treeview.
            For Each objCategoryNode In parentNode.Nodes
                For Each l_Row In objTable.Rows
                    If l_Row("INTERFACE_NAME") = objCategoryNode.Text Then
                        l_Node = New TreeNode()
                        With l_Node
                            .Text = l_Row("CAPTION")
                            .Tag = l_Row("DESCRIPTION") & vbTab & l_Row("FUNCTION_NAME")
                            If Not (l_Row("PARAMETER_LIST") = "(none)" Or System.Convert.IsDBNull(l_Row("PARAMETER_LIST"))) Then
                                .Tag = .Tag & "(" & l_Row("PARAMETER_LIST") & ")"
                            End If
                        End With
                        objCategoryNode.Nodes.Add(l_Node)
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub rtfQuery_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles rtfQuery.DragDrop
        ' Handle drag & drop events from the Metadata Browser or Syntax Browser panes
        ' to the Query Pane.

        Dim strText As String
        Dim intStart As Integer
        Dim strSelection As String

        If e.Data.GetDataPresent(DataFormats.Text) Then
            strText = e.Data.GetData(DataFormats.Text).ToString
            intStart = rtfQuery.SelectionStart
            strSelection = rtfQuery.Text.Substring(intStart)
            rtfQuery.Text = rtfQuery.Text.Substring(0, intStart)

            rtfQuery.Text = rtfQuery.Text & strText & strSelection
        End If
    End Sub

    Private Sub rtfQuery_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles rtfQuery.DragEnter
        ' Handle drag & drop events from the Metadata Browser or Syntax Browser panes
        ' to the Query Pane.
        If e.Data.GetDataPresent(DataFormats.Text) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub mnuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
        ' Clean up outstanding object references, then end the application.
        If Not (m_CellSet Is Nothing) Then
            m_CellSet = Nothing
        End If
        If Not (m_Connection Is Nothing) Then
            m_Helper.Disconnect(m_Connection, True)
        End If
        If Not (m_Helper Is Nothing) Then
            m_Helper = Nothing
        End If
        Application.Exit()
    End Sub

    Private Sub mnuFileConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileConnect.Click
        ' Connect to a specified data source.
        Me.Connect()
    End Sub

    Private Sub mnuFileDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileDisconnect.Click
        ' Disconnect from the current data source.
        Me.Disconnect()
    End Sub

    Private Sub mnuQueryRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQueryRun.Click
        ' Clear grid
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.UpdateStatus("Resetting grid...")

            m_CellSet = Nothing
            grdResults.CellSet = m_CellSet

            ' Retrieve data
            Me.UpdateStatus("Executing query...")
            m_CellSet = m_Helper.RunQuery(m_Connection, rtfQuery.Text)
            Me.UpdateStatus("Loading cellset into grid...")

            ' Provide grid with the returned cellset.
            grdResults.CellSet = m_CellSet
        Catch ex As Exception
            MsgBox("An error occurred while running the query:" & vbCrLf & _
                ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.UpdateStatus()
        End Try
    End Sub

    Private Sub tbrMain_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbrMain.ButtonClick
        ' Handle the toolbar buttons.
        Select Case True
            ' If connected, then disconnect, otherwise connect.
        Case e.Button Is tbbConnect
                If m_Helper.IsConnected(m_Connection) Then
                    Me.Disconnect()
                Else
                    Me.Connect()
                End If
                ' Run the query.
            Case e.Button Is tbbQueryRun
                mnuQueryRun_Click(sender, New System.EventArgs())
        End Select
    End Sub

    Private Sub chkUseSchemaRowsets_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUseSchemaRowsets.CheckedChanged
        If m_Helper.IsConnected(m_Connection) Then
            ClearMetadataBrowser()
            LoadMetadataBrowser()
        End If
    End Sub
#End Region

End Class

