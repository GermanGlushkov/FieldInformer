Option Explicit On 

'======================================================================
'
'  File:      Enums.vb
'  Summary:   Enumerations used throughout AdomdSampleGrid.
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

Public Module Enums

#Region "== Enumerations ======================================================="
    Public Enum GridSelectionMode
        Cell = 1
        Row = 2
        Col = 3
    End Enum

    Public Enum GridSortMode
        None = 0
        Ascending = 1
        Descending = 2
    End Enum

    Public Enum CommonBorderStyle
        Normal = 1
        Raised = 2
        Inset = 3
    End Enum

    <Flags()> _
    Public Enum CellResizeModes
        None = 0
        Height = 1
        Width = 2
        Both = 3
    End Enum

    <Flags()> _
    Public Enum ExportHtmlModes
        None = 0
        HtmlAndBody = 1
        GridBackColor = 2
        CellBackColor = 4
        CellBorder = 8
        CellForeColor = 16
        CellImages = 32
        [Default] = (HtmlAndBody Or GridBackColor Or CellBackColor Or CellBorder Or CellForeColor Or CellImages)
    End Enum
#End Region

End Module