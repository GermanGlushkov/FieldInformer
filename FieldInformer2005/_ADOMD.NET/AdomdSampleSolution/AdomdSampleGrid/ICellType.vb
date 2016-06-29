Option Explicit On 

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms

'======================================================================
'
'  File:      ICellType.vb
'  Summary:   Interface that describes required properties and methods
'             for cell types within AdomdSampleGrid.
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

Public Interface ICellType

#Region "== Events ============================================================="
    Event Validating As ValidatingCellEventHandler
    Event ValidatingValue As ValidatingEventHandler
    Event Validated As CellEventHandler
#End Region

#Region "== Properties ========================================================="
    ReadOnly Property SupportStringConversion() As Boolean
    Property NullString() As String
    Property ErrorString() As String
    Property StandardValues() As ICollection
#End Region

#Region "== Methods ============================================================"
    Sub ClearCell(ByVal cell As Cell)
    Function SetCellValue(ByVal cell As Cell, ByVal value As Object) As Boolean
    Function IsValidValue(ByVal value As Object) As Boolean
    Function IsValidString(ByVal value As String) As Boolean
    Function StringToObject(ByVal value As String) As Object
    Function ObjectToString(ByVal value As Object) As String
    Function GetStringRepresentation(ByVal value As Object) As String
    Function IsNullString(ByVal value As String) As Boolean
    Function IsErrorString(ByVal value As String) As Boolean
    Function ExportValue(ByVal value As Object) As String
    Function ImportValue(ByVal value As String) As Object
#End Region
End Interface
