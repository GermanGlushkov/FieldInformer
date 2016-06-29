Option Explicit On 

'======================================================================
'
'  File:      RowInfo.vb
'  Summary:   Stores information about a single row of ICellType 
'             objects in the grid.
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

Public Class RowInfo

#Region "== Variables =========================================================="
    Public Height As Integer
    Public Top As Integer
#End Region

#Region "== Constructors ======================================================="
    Public Sub New(ByVal h As Integer, ByVal t As Integer)
        Height = h
        Top = t
    End Sub
#End Region
End Class
