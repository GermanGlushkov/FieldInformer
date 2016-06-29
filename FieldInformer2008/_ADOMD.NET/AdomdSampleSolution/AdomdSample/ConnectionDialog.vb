Option Explicit On 
Imports Microsoft.AnalysisServices.AdomdClient
Imports System.Text.RegularExpressions

'======================================================================
'
'  File:      ConnectionDialog.vb
'  Summary:   Dialog used to connect to an XML for Analysis 
'             data source.
'  Date:	  02/24/2004
'
'----------------------------------------------------------------------
'
'  This file is part of the Adomd.NET Software Development Kit.
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

Public Class ConnectionDialog
    Inherits System.Windows.Forms.Form

#Region "== Constants =========================================================="
    ' Connection string constants.
    Private Const CS_DATASOURCE = "Data Source"
    Private Const CS_NAMEVALUE As String = "{0}={1};"
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
    Friend WithEvents lblDataSource As System.Windows.Forms.Label
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents txtDataSource As System.Windows.Forms.TextBox
    Friend WithEvents epvConnectionDialog As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ConnectionDialog))
        Me.lblDataSource = New System.Windows.Forms.Label
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.txtDataSource = New System.Windows.Forms.TextBox
        Me.epvConnectionDialog = New System.Windows.Forms.ErrorProvider
        Me.SuspendLayout()
        '
        'lblDataSource
        '
        Me.lblDataSource.AccessibleDescription = resources.GetString("lblDataSource.AccessibleDescription")
        Me.lblDataSource.AccessibleName = resources.GetString("lblDataSource.AccessibleName")
        Me.lblDataSource.Anchor = CType(resources.GetObject("lblDataSource.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.lblDataSource.AutoSize = CType(resources.GetObject("lblDataSource.AutoSize"), Boolean)
        Me.lblDataSource.Dock = CType(resources.GetObject("lblDataSource.Dock"), System.Windows.Forms.DockStyle)
        Me.lblDataSource.Enabled = CType(resources.GetObject("lblDataSource.Enabled"), Boolean)
        Me.epvConnectionDialog.SetError(Me.lblDataSource, resources.GetString("lblDataSource.Error"))
        Me.lblDataSource.Font = CType(resources.GetObject("lblDataSource.Font"), System.Drawing.Font)
        Me.epvConnectionDialog.SetIconAlignment(Me.lblDataSource, CType(resources.GetObject("lblDataSource.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.epvConnectionDialog.SetIconPadding(Me.lblDataSource, CType(resources.GetObject("lblDataSource.IconPadding"), Integer))
        Me.lblDataSource.Image = CType(resources.GetObject("lblDataSource.Image"), System.Drawing.Image)
        Me.lblDataSource.ImageAlign = CType(resources.GetObject("lblDataSource.ImageAlign"), System.Drawing.ContentAlignment)
        Me.lblDataSource.ImageIndex = CType(resources.GetObject("lblDataSource.ImageIndex"), Integer)
        Me.lblDataSource.ImeMode = CType(resources.GetObject("lblDataSource.ImeMode"), System.Windows.Forms.ImeMode)
        Me.lblDataSource.Location = CType(resources.GetObject("lblDataSource.Location"), System.Drawing.Point)
        Me.lblDataSource.Name = "lblDataSource"
        Me.lblDataSource.RightToLeft = CType(resources.GetObject("lblDataSource.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.lblDataSource.Size = CType(resources.GetObject("lblDataSource.Size"), System.Drawing.Size)
        Me.lblDataSource.TabIndex = CType(resources.GetObject("lblDataSource.TabIndex"), Integer)
        Me.lblDataSource.Text = resources.GetString("lblDataSource.Text")
        Me.lblDataSource.TextAlign = CType(resources.GetObject("lblDataSource.TextAlign"), System.Drawing.ContentAlignment)
        Me.lblDataSource.Visible = CType(resources.GetObject("lblDataSource.Visible"), Boolean)
        '
        'cmdOK
        '
        Me.cmdOK.AccessibleDescription = resources.GetString("cmdOK.AccessibleDescription")
        Me.cmdOK.AccessibleName = resources.GetString("cmdOK.AccessibleName")
        Me.cmdOK.Anchor = CType(resources.GetObject("cmdOK.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.BackgroundImage = CType(resources.GetObject("cmdOK.BackgroundImage"), System.Drawing.Image)
        Me.cmdOK.Dock = CType(resources.GetObject("cmdOK.Dock"), System.Windows.Forms.DockStyle)
        Me.cmdOK.Enabled = CType(resources.GetObject("cmdOK.Enabled"), Boolean)
        Me.epvConnectionDialog.SetError(Me.cmdOK, resources.GetString("cmdOK.Error"))
        Me.cmdOK.FlatStyle = CType(resources.GetObject("cmdOK.FlatStyle"), System.Windows.Forms.FlatStyle)
        Me.cmdOK.Font = CType(resources.GetObject("cmdOK.Font"), System.Drawing.Font)
        Me.epvConnectionDialog.SetIconAlignment(Me.cmdOK, CType(resources.GetObject("cmdOK.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.epvConnectionDialog.SetIconPadding(Me.cmdOK, CType(resources.GetObject("cmdOK.IconPadding"), Integer))
        Me.cmdOK.Image = CType(resources.GetObject("cmdOK.Image"), System.Drawing.Image)
        Me.cmdOK.ImageAlign = CType(resources.GetObject("cmdOK.ImageAlign"), System.Drawing.ContentAlignment)
        Me.cmdOK.ImageIndex = CType(resources.GetObject("cmdOK.ImageIndex"), Integer)
        Me.cmdOK.ImeMode = CType(resources.GetObject("cmdOK.ImeMode"), System.Windows.Forms.ImeMode)
        Me.cmdOK.Location = CType(resources.GetObject("cmdOK.Location"), System.Drawing.Point)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = CType(resources.GetObject("cmdOK.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.cmdOK.Size = CType(resources.GetObject("cmdOK.Size"), System.Drawing.Size)
        Me.cmdOK.TabIndex = CType(resources.GetObject("cmdOK.TabIndex"), Integer)
        Me.cmdOK.Text = resources.GetString("cmdOK.Text")
        Me.cmdOK.TextAlign = CType(resources.GetObject("cmdOK.TextAlign"), System.Drawing.ContentAlignment)
        Me.cmdOK.Visible = CType(resources.GetObject("cmdOK.Visible"), Boolean)
        '
        'cmdCancel
        '
        Me.cmdCancel.AccessibleDescription = resources.GetString("cmdCancel.AccessibleDescription")
        Me.cmdCancel.AccessibleName = resources.GetString("cmdCancel.AccessibleName")
        Me.cmdCancel.Anchor = CType(resources.GetObject("cmdCancel.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.BackgroundImage = CType(resources.GetObject("cmdCancel.BackgroundImage"), System.Drawing.Image)
        Me.cmdCancel.CausesValidation = False
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Dock = CType(resources.GetObject("cmdCancel.Dock"), System.Windows.Forms.DockStyle)
        Me.cmdCancel.Enabled = CType(resources.GetObject("cmdCancel.Enabled"), Boolean)
        Me.epvConnectionDialog.SetError(Me.cmdCancel, resources.GetString("cmdCancel.Error"))
        Me.cmdCancel.FlatStyle = CType(resources.GetObject("cmdCancel.FlatStyle"), System.Windows.Forms.FlatStyle)
        Me.cmdCancel.Font = CType(resources.GetObject("cmdCancel.Font"), System.Drawing.Font)
        Me.epvConnectionDialog.SetIconAlignment(Me.cmdCancel, CType(resources.GetObject("cmdCancel.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.epvConnectionDialog.SetIconPadding(Me.cmdCancel, CType(resources.GetObject("cmdCancel.IconPadding"), Integer))
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.ImageAlign = CType(resources.GetObject("cmdCancel.ImageAlign"), System.Drawing.ContentAlignment)
        Me.cmdCancel.ImageIndex = CType(resources.GetObject("cmdCancel.ImageIndex"), Integer)
        Me.cmdCancel.ImeMode = CType(resources.GetObject("cmdCancel.ImeMode"), System.Windows.Forms.ImeMode)
        Me.cmdCancel.Location = CType(resources.GetObject("cmdCancel.Location"), System.Drawing.Point)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = CType(resources.GetObject("cmdCancel.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.cmdCancel.Size = CType(resources.GetObject("cmdCancel.Size"), System.Drawing.Size)
        Me.cmdCancel.TabIndex = CType(resources.GetObject("cmdCancel.TabIndex"), Integer)
        Me.cmdCancel.Text = resources.GetString("cmdCancel.Text")
        Me.cmdCancel.TextAlign = CType(resources.GetObject("cmdCancel.TextAlign"), System.Drawing.ContentAlignment)
        Me.cmdCancel.Visible = CType(resources.GetObject("cmdCancel.Visible"), Boolean)
        '
        'txtDataSource
        '
        Me.txtDataSource.AccessibleDescription = resources.GetString("txtDataSource.AccessibleDescription")
        Me.txtDataSource.AccessibleName = resources.GetString("txtDataSource.AccessibleName")
        Me.txtDataSource.Anchor = CType(resources.GetObject("txtDataSource.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.txtDataSource.AutoSize = CType(resources.GetObject("txtDataSource.AutoSize"), Boolean)
        Me.txtDataSource.BackgroundImage = CType(resources.GetObject("txtDataSource.BackgroundImage"), System.Drawing.Image)
        Me.txtDataSource.Dock = CType(resources.GetObject("txtDataSource.Dock"), System.Windows.Forms.DockStyle)
        Me.txtDataSource.Enabled = CType(resources.GetObject("txtDataSource.Enabled"), Boolean)
        Me.epvConnectionDialog.SetError(Me.txtDataSource, resources.GetString("txtDataSource.Error"))
        Me.txtDataSource.Font = CType(resources.GetObject("txtDataSource.Font"), System.Drawing.Font)
        Me.epvConnectionDialog.SetIconAlignment(Me.txtDataSource, CType(resources.GetObject("txtDataSource.IconAlignment"), System.Windows.Forms.ErrorIconAlignment))
        Me.epvConnectionDialog.SetIconPadding(Me.txtDataSource, CType(resources.GetObject("txtDataSource.IconPadding"), Integer))
        Me.txtDataSource.ImeMode = CType(resources.GetObject("txtDataSource.ImeMode"), System.Windows.Forms.ImeMode)
        Me.txtDataSource.Location = CType(resources.GetObject("txtDataSource.Location"), System.Drawing.Point)
        Me.txtDataSource.MaxLength = CType(resources.GetObject("txtDataSource.MaxLength"), Integer)
        Me.txtDataSource.Multiline = CType(resources.GetObject("txtDataSource.Multiline"), Boolean)
        Me.txtDataSource.Name = "txtDataSource"
        Me.txtDataSource.PasswordChar = CType(resources.GetObject("txtDataSource.PasswordChar"), Char)
        Me.txtDataSource.RightToLeft = CType(resources.GetObject("txtDataSource.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.txtDataSource.ScrollBars = CType(resources.GetObject("txtDataSource.ScrollBars"), System.Windows.Forms.ScrollBars)
        Me.txtDataSource.Size = CType(resources.GetObject("txtDataSource.Size"), System.Drawing.Size)
        Me.txtDataSource.TabIndex = CType(resources.GetObject("txtDataSource.TabIndex"), Integer)
        Me.txtDataSource.Text = resources.GetString("txtDataSource.Text")
        Me.txtDataSource.TextAlign = CType(resources.GetObject("txtDataSource.TextAlign"), System.Windows.Forms.HorizontalAlignment)
        Me.txtDataSource.Visible = CType(resources.GetObject("txtDataSource.Visible"), Boolean)
        Me.txtDataSource.WordWrap = CType(resources.GetObject("txtDataSource.WordWrap"), Boolean)
        '
        'epvConnectionDialog
        '
        Me.epvConnectionDialog.ContainerControl = Me
        Me.epvConnectionDialog.Icon = CType(resources.GetObject("epvConnectionDialog.Icon"), System.Drawing.Icon)
        '
        'ConnectionDialog
        '
        Me.AcceptButton = Me.cmdOK
        Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
        Me.AccessibleName = resources.GetString("$this.AccessibleName")
        Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
        Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
        Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
        Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtDataSource)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lblDataSource)
        Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
        Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
        Me.MaximizeBox = False
        Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
        Me.MinimizeBox = False
        Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
        Me.Name = "ConnectionDialog"
        Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.ShowInTaskbar = False
        Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
        Me.Text = resources.GetString("$this.Text")
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "== Methods ============================================================"
    Private Sub ConnectionDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check the existence of an instance of the AdomdHelper class.
        If m_Helper Is Nothing Then
            ' This form cannot load without an instance of the AdomdHelper class.
            Throw New Exception("Unable to initialize an instance of the AdomdHelper class.")
        End If

        ' Disconnect the existing AdomdConnection object, if applicable.
        If m_Helper.IsConnected(m_Connection) Then m_Helper.Disconnect(m_Connection)
    End Sub


    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        ' First, check the Data Source field.
        Dim l_OK As Boolean = True
        Dim l_Counter As Integer
        Dim l_Message As String
        Dim l_ConnectionString As String
        Dim l_dsnName As String = ""
        Dim l_dsnIndex As Integer = -1

        Me.Cursor = Cursors.WaitCursor

        ' Check required fields.
        l_ConnectionString += String.Format(CS_NAMEVALUE, CS_DATASOURCE, txtDataSource.Text)

        If l_OK Then
            Try
                ' Attempt to connect to a database at this time.
                m_Helper.Connect(m_Connection, l_ConnectionString)

                ' Report that a connection has successfully occured.
                If m_Helper.IsConnected(m_Connection) Then
                    Me.DialogResult = DialogResult.OK
                    Me.Cursor = Cursors.Default

                    ' Hide the form.
                    Me.Hide()
                Else
                    MsgBox("A connection could not be obtained to the specified database.")
                End If
            Catch ex As Exception
                MsgBox("An error occurred while attempting to connect to the specified database:" & vbCrLf & ex.ToString)
            End Try
        Else
            MsgBox("The following issues must be resolved before a connection can be made:" & vbCrLf & l_Message)
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        ' Report that a connection did not successfully occur.
        Me.DialogResult = DialogResult.Cancel

        ' Hide the form.
        Me.Hide()
    End Sub

    Private Sub cboDataSource_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        ' Validate the data source provided in txtDataSource.  The data source must meet 
        ' the following requirements:
        ' - It must be a valid file, HTTP, or HTTPS URI.
        ' - If a valid file URI, it must have an extension of ".cub" and it must exist.
        ' - If a valid HTTP or HTTPS URI, it must be well-formed.

        Try
            If sender Is cmdOK Then
                If txtDataSource.Text = "" Then
                    e.Cancel = True
                    Me.epvConnectionDialog.SetIconAlignment(txtDataSource, ErrorIconAlignment.MiddleLeft)
                    Me.epvConnectionDialog.SetError(txtDataSource, "You must provide a valid data source.")
                Else
                    Dim objURI As New Uri(txtDataSource.Text)

                    ' Check scheme and host names to ensure well-formed URI
                    If objURI.CheckSchemeName(objURI.Scheme) Then
                        If objURI.Scheme = objURI.UriSchemeFile Then
                            ' A potentially valid local cube file reference.
                            Dim lvobjFI As New System.IO.FileInfo(objURI.LocalPath)
                            If lvobjFI.Extension <> ".cub" Then
                                e.Cancel = True
                                Me.epvConnectionDialog.SetIconAlignment(txtDataSource, ErrorIconAlignment.MiddleLeft)
                                Me.epvConnectionDialog.SetError(txtDataSource, _
                                    "You must provide a valid local cube (.cub) file, HTTP, or HTTPS reference for the data source.")
                            ElseIf lvobjFI.Exists = False Then
                                e.Cancel = True
                                Me.epvConnectionDialog.SetIconAlignment(txtDataSource, ErrorIconAlignment.MiddleLeft)
                                Me.epvConnectionDialog.SetError(txtDataSource, _
                                    "The specified local cube file cannot be found.")
                            End If
                        Else
                            ' Only http and https connections are allowed.
                            If objURI.Scheme <> objURI.UriSchemeHttp And _
                                objURI.Scheme <> objURI.UriSchemeHttps Then

                                e.Cancel = True
                                Me.epvConnectionDialog.SetIconAlignment(txtDataSource, ErrorIconAlignment.MiddleLeft)
                                Me.epvConnectionDialog.SetError(txtDataSource, _
                                    "You must provide a valid local cube (.cub) file, HTTP, or HTTPS reference for the data source.")
                            End If
                        End If
                    Else
                        e.Cancel = True
                        Me.epvConnectionDialog.SetIconAlignment(txtDataSource, ErrorIconAlignment.MiddleLeft)
                        Me.epvConnectionDialog.SetError(txtDataSource, _
                            "You must provide a valid local cube (.cub) file, HTTP, or HTTPS reference for the data source.")
                    End If
                End If
            End If
        Catch ex As Exception
            e.Cancel = True
            Me.epvConnectionDialog.SetIconAlignment(txtDataSource, ErrorIconAlignment.MiddleLeft)
            Me.epvConnectionDialog.SetError(txtDataSource, _
                ex.Message)
        End Try
    End Sub

#End Region

End Class
