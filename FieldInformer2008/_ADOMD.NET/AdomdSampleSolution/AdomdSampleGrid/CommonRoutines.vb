Option Explicit On 

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Windows.Forms
Imports System.Runtime.Serialization

'======================================================================
'
'  File:      CommonRoutines.vb
'  Summary:   Helper routines used by ICellType implementations.
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

Public Class CommonRoutines

#Region "== Methods ============================================================"
    Public Shared Function ImageClone(ByVal image As Image) As Image
        Dim l_BinForm As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim l_Stream As New System.IO.MemoryStream

        l_BinForm.Serialize(l_Stream, image)
        l_Stream.Seek(0, IO.SeekOrigin.Begin)

        Return CType(l_BinForm.Deserialize(l_Stream), Image)
    End Function

    Public Overloads Shared Function IsBottom(ByVal a As ContentAlignment) As Boolean

        Return ((a = ContentAlignment.BottomCenter) Or _
            (a = ContentAlignment.BottomLeft) Or _
            (a = ContentAlignment.BottomRight))

    End Function

    Public Overloads Shared Function IsBottom(ByVal a As StringFormat) As Boolean
        Return (a.LineAlignment = StringAlignment.Far)
    End Function

    Public Overloads Shared Function IsTop(ByVal a As ContentAlignment) As Boolean

        Return ((a = ContentAlignment.TopCenter) Or _
            (a = ContentAlignment.TopLeft) Or _
            (a = ContentAlignment.TopRight))

    End Function

    Public Overloads Shared Function IsTop(ByVal a As StringFormat) As Boolean
        Return (a.LineAlignment = StringAlignment.Near)
    End Function

    Public Overloads Shared Function IsMiddle(ByVal a As ContentAlignment) As Boolean

        Return ((a = ContentAlignment.MiddleCenter) Or _
            (a = ContentAlignment.MiddleLeft) Or _
            (a = ContentAlignment.MiddleRight))

    End Function

    Public Overloads Shared Function IsMiddle(ByVal a As StringFormat) As Boolean
        Return (a.LineAlignment = StringAlignment.Center)
    End Function

    Public Overloads Shared Function IsLeft(ByVal a As ContentAlignment) As Boolean

        Return ((a = ContentAlignment.BottomLeft) Or _
            (a = ContentAlignment.MiddleLeft) Or _
            (a = ContentAlignment.TopLeft))

    End Function

    Public Overloads Shared Function IsLeft(ByVal a As StringFormat) As Boolean
        Return (a.Alignment = StringAlignment.Near)
    End Function

    Public Overloads Shared Function IsRight(ByVal a As ContentAlignment) As Boolean

        Return ((a = ContentAlignment.BottomRight) Or _
            (a = ContentAlignment.MiddleRight) Or _
            (a = ContentAlignment.TopRight))

    End Function

    Public Overloads Shared Function IsRight(ByVal a As StringFormat) As Boolean
        Return (a.Alignment = StringAlignment.Far)
    End Function

    Public Overloads Shared Function IsCenter(ByVal a As ContentAlignment) As Boolean

        Return ((a = ContentAlignment.BottomCenter) Or _
            (a = ContentAlignment.MiddleCenter) Or _
            (a = ContentAlignment.TopCenter))

    End Function

    Public Overloads Shared Function IsCenter(ByVal a As StringFormat) As Boolean
        Return (a.Alignment = StringAlignment.Center)
    End Function

    Public Shared Function ContentToHorizontalAlignment(ByVal a As ContentAlignment) As HorizontalAlignment
        If IsLeft(a) Then
            Return HorizontalAlignment.Left
        ElseIf IsRight(a) Then
            Return HorizontalAlignment.Right
        Else
            Return HorizontalAlignment.Center
        End If
    End Function

    Public Overloads Shared Function CreateCellType(ByVal type As Type, ByVal defaultValue As Object) As ICellType
        Return CreateCellType(type, defaultValue, Not (type.IsValueType))
    End Function

    Public Overloads Shared Function CreateCellType(ByVal type As Type, ByVal defaultValue As Object, ByVal allowNull As Boolean) As ICellType
        Dim l_Converter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(type)
        If Not (l_Converter Is Nothing) Then
            Return CreateCellType(type, _
                defaultValue, _
                allowNull, _
                l_Converter.GetStandardValues, _
                l_Converter.GetStandardValuesExclusive, _
                l_Converter, _
                CType(System.ComponentModel.TypeDescriptor.GetEditor(type, GetType(System.Drawing.Design.UITypeEditor)), System.Drawing.Design.UITypeEditor))
        Else
            Return CreateCellType(type, _
                defaultValue, _
                allowNull, _
                Nothing, _
                False, _
                Nothing, _
                CType(System.ComponentModel.TypeDescriptor.GetEditor(type, GetType(System.Drawing.Design.UITypeEditor)), System.Drawing.Design.UITypeEditor))
        End If

    End Function

    Public Overloads Shared Function CreateCellType(ByVal type As Type, _
        ByVal defaultValue As Object, _
        ByVal allowNull As Boolean, _
        ByVal standardValues As System.Collections.ICollection, _
        ByVal standardValueExclusive As Boolean, _
        ByVal typeConverter As System.ComponentModel.TypeConverter, _
        ByVal uiTypeEditor As System.Drawing.Design.UITypeEditor) As ICellType

        Dim l_CellType As New CellTypeBase(type, defaultValue)

        Return l_CellType
    End Function

#End Region

End Class



