Option Explicit On 

Imports System
Imports System.Windows.Forms
Imports System.ComponentModel
'======================================================================
'
'  File:      CellTypeBase.vb
'  Summary:   Base implementation of ICellType interface, used to 
'             identify data type, conversion, and editing 
'             capabilities for a given Cell object.
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

Public Class CellTypeBase
    Implements ICellType

#Region "== Variables =========================================================="
    ' Property values
    Private m_EditCell As Cell
    Private m_NullString As String = "#NUL"
    Private m_AllowNull As Boolean = True
    Private m_ErrorString As String = "#ERR"
    Private m_DefaultValue As Object = Nothing
    Private m_CellType As Type = Nothing
    Private m_StandardValues As ICollection
    Private m_StandardValuesExclusive As Boolean = False
    Private m_TypeConverter As TypeConverter = Nothing
    Private m_EnableEdit As Boolean = True
#End Region

#Region "== Events ============================================================="
    Public Event Validating As ValidatingCellEventHandler Implements ICellType.Validating
    Public Event Validated As CellEventHandler Implements ICellType.Validated
    Public Event ValidatingValue As ValidatingEventHandler Implements ICellType.ValidatingValue
#End Region

#Region "== Constructors ======================================================="
    Public Sub New()
        ' Do nothing
    End Sub

    Public Sub New(ByVal cellType As Type, ByVal defaultValue As Object)
        m_CellType = cellType
        If Not (m_CellType Is Nothing) Then
            TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(m_CellType)
            m_DefaultValue = defaultValue
        End If
    End Sub

    Public Sub New(ByVal cellType As Type, ByVal defaultValue As Object, ByVal typeConverter As TypeConverter)
        m_CellType = cellType
        m_TypeConverter = typeConverter
        m_DefaultValue = defaultValue
    End Sub
#End Region

#Region "== Properties ========================================================="
    Public Property NullString() As String Implements ICellType.NullString
        Get
            Return m_NullString
        End Get
        Set(ByVal value As String)
            m_NullString = value
        End Set
    End Property

    Public Property AllowNull() As Boolean
        Get
            Return m_AllowNull
        End Get
        Set(ByVal value As Boolean)
            m_AllowNull = value
        End Set
    End Property

    Public Property ErrorString() As String Implements ICellType.ErrorString
        Get
            Return m_ErrorString
        End Get
        Set(ByVal value As String)
            m_ErrorString = value
        End Set
    End Property

    Public Property DefaultValue() As Object
        Get
            Return m_DefaultValue
        End Get
        Set(ByVal value As Object)
            m_DefaultValue = value
        End Set
    End Property

    Public Property StandardValuesExclusive() As Boolean
        Get
            Return m_StandardValuesExclusive
        End Get
        Set(ByVal value As Boolean)
            m_StandardValuesExclusive = value
        End Set
    End Property

    Public Property CellType() As Type
        Get
            Return m_CellType
        End Get
        Set(ByVal value As Type)
            m_CellType = value
        End Set
    End Property

    Public Property TypeConverter() As TypeConverter
        Get
            Return m_TypeConverter
        End Get
        Set(ByVal value As TypeConverter)
            m_TypeConverter = value
        End Set
    End Property

    Public Overridable ReadOnly Property SupportStringConversion() As Boolean Implements ICellType.SupportStringConversion
        Get
            If m_StandardValuesExclusive Then
                Return False
            Else
                If Not (m_TypeConverter Is Nothing) Then
                    Return (m_TypeConverter.CanConvertFrom(GetType(String)) AndAlso m_TypeConverter.CanConvertTo(GetType(String)))
                Else
                    Return True
                End If
            End If
        End Get
    End Property

    Public Overridable Property StandardValues() As ICollection Implements ICellType.StandardValues
        Get
            Return m_StandardValues
        End Get
        Set(ByVal value As ICollection)
            m_StandardValues = value
        End Set
    End Property
#End Region

#Region "== Methods ============================================================"
    Protected Sub SetEditCell(ByVal cell As Cell)
        m_EditCell = cell
    End Sub

    Public Function IsErrorString(ByVal value As String) As Boolean Implements ICellType.IsErrorString
        Return (value = m_ErrorString)
    End Function

    Public Overridable Sub ClearCell(ByVal cell As Cell) Implements ICellType.ClearCell
        SetCellValue(cell, m_DefaultValue)
    End Sub

    Public Overridable Function SetCellValue(ByVal cell As Cell, ByVal newValue As Object) As Boolean Implements ICellType.SetCellValue
        Dim l_cancelEvent As ValidatingCellEventArgs = New ValidatingCellEventArgs(cell, newValue)
        OnValidating(l_cancelEvent)

        If l_cancelEvent.Cancel = False Then
            Dim l_PrevValue = cell.Value
            Try
                cell.Value = l_cancelEvent.NewValue
                OnValidated(New CellEventArgs(cell))
            Catch ex As Exception
                cell.Value = l_PrevValue
                l_cancelEvent.Cancel = True
            End Try
        End If
        Return (l_cancelEvent.Cancel = False)
    End Function

    Protected Overridable Sub OnValidated(ByVal e As CellEventArgs)
        RaiseEvent Validated(Me, e)
    End Sub

    Protected Overridable Sub OnValidating(ByVal e As ValidatingCellEventArgs)
        OnValidatingValue(e)
        RaiseEvent Validating(Me, e)
    End Sub

    Protected Overridable Sub OnValidatingValue(ByVal e As ValidatingEventArgs)
        If e.NewValue Is Nothing Then
            If AllowNull = False Then e.Cancel = True
        Else
            If m_CellType Is Nothing Then
            Else
                If (m_CellType.IsAssignableFrom(e.NewValue.GetType)) Then
                ElseIf (TypeOf (e.NewValue.GetType) Is String) AndAlso SupportStringConversion Then
                    Dim l_tmp As Object = StringToObject(CType(e.NewValue, String))
                    If l_tmp Is Nothing Then
                        If AllowNull = False Then e.Cancel = True
                    ElseIf l_tmp.Equals(m_CellType) Then
                        e.NewValue = l_tmp
                    Else
                        e.Cancel = True
                    End If
                Else
                    If Not (m_TypeConverter Is Nothing) Then
                        Dim l_tmp As Object = m_TypeConverter.ConvertFrom(e.NewValue)
                        If l_tmp Is Nothing Then
                            If AllowNull = False Then e.Cancel = True
                        ElseIf l_tmp.Equals(m_CellType) Then
                            e.NewValue = l_tmp
                        Else
                            e.Cancel = True
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        End If

        RaiseEvent ValidatingValue(Me, e)
    End Sub

    Public Function IsValidValue(ByVal value As Object) As Boolean Implements ICellType.IsValidValue
        Dim l_Valid As ValidatingEventArgs = New ValidatingEventArgs(value)
        OnValidatingValue(l_Valid)
        Return (l_Valid.Cancel = False)
    End Function

    Public Function GetEditedValue() As Object
        Throw New ApplicationException("No valid cell type found")
    End Function

    Public Shadows Function IsNullString(ByVal value As String) As Boolean Implements ICellType.IsNullString
        Return (value Is Nothing OrElse value = NullString)
    End Function

    Public Shadows Function StringToObject(ByVal value As String) As Object Implements ICellType.StringToObject
        If IsNullString(value) Then
            Return Nothing
        Else
            If Not (m_TypeConverter Is Nothing) Then
                Return m_TypeConverter.ConvertFromString(value)
            Else
                Return value
            End If
        End If
    End Function

    Public Overridable Function ObjectToString(ByVal value As Object) As String Implements ICellType.ObjectToString
        Try
            If value Is Nothing Then
                Return NullString
            Else
                If Not (m_TypeConverter Is Nothing) Then
                    Return m_TypeConverter.ConvertToString(value)
                Else
                    Return value.ToString
                End If
            End If
        Catch ex As Exception
            Return ErrorString
        End Try
    End Function

    Public Overridable Function GetStringRepresentation(ByVal value As Object) As String Implements ICellType.GetStringRepresentation
        If Not (value Is Nothing) Then
            If Not (m_TypeConverter Is Nothing) Then
                If m_TypeConverter.CanConvertTo(GetType(String)) Then
                    Return ObjectToString(value)
                Else
                    Return value.ToString
                End If
            Else
                Return value.ToString
            End If
        Else
            Return NullString
        End If
    End Function

    Public Overridable Function IsValidString(ByVal value As String) As Boolean Implements ICellType.IsValidString
        If Not (m_TypeConverter Is Nothing) Then
            If m_TypeConverter.IsValid(value) Then
                Try
                    Dim tmp As Object = StringToObject(value)
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Overridable Function ExportValue(ByVal value As Object) As String Implements ICellType.ExportValue
        Return ObjectToString(value)
    End Function

    Public Overridable Function ImportValue(ByVal strImport As String) As Object Implements ICellType.ImportValue
        Return StringToObject(strImport)
    End Function

#End Region
End Class
