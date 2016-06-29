Option Explicit On 

Imports Microsoft.AnalysisServices.AdomdClient
Imports System.Data

'======================================================================
'
'  File:      AdomdHelper.vb
'  Summary:   Encapsulates the most commonly used ADOMD.NET functions
'             and classes.
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

Public Class AdomdHelper

#Region "== Enumerations ======================================================="
    Public Enum Versions
        Server
        Provider
        Client
    End Enum

#End Region

#Region "== Methods ============================================================"
    Public Function IsConnected(ByRef connection As AdomdConnection) As Boolean
        ' Helper function to check the instantiation and state of a given 
        ' AdomdConnection object.
        Return (Not (connection Is Nothing)) AndAlso _
            (connection.State <> ConnectionState.Broken) AndAlso _
            (connection.State <> ConnectionState.Closed)
    End Function

    Public Sub Connect(ByRef connection As AdomdConnection, Optional ByVal connectionString As String = "")
        ' Attempts to connect an given AdomdConnection object to a data source, optionally with
        ' a supplied connection string.

        If connectionString = "" Then
            ' Not enough information supplied to establish a connection.
            Throw New ArgumentNullException("connectionString", "The connection string is not valid.")
        End If

        ' Ensure an AdomdConnection object exists and that its
        ' ConnectionString property is set.
        If connection Is Nothing Then
            connection = New AdomdConnection(connectionString)
        Else
            Me.Disconnect(connection)
            connection.ConnectionString = connectionString
        End If

        ' Attempt to establish a connection.
        Try
            connection.Open()
        Catch ex As Exception
            ' Display the error.
            m_Helper.DisplayException(ex, "connecting")
            Throw ex
        End Try
    End Sub

    Public Sub Disconnect(ByRef connection As AdomdConnection, Optional ByVal destroyConnection As Boolean = False)
        ' A helper subroutine to safely disconnect an AdomdConnection object, optionally destroying
        ' the object if needed.
        Try
            If Not (connection Is Nothing) Then
                ' Attempt to close the connection.
                If connection.State <> ConnectionState.Closed Then connection.Close()

                ' Attempt to destroy the AdomdConnection object.
                If destroyConnection Then
                    connection.Dispose()
                    connection = Nothing
                End If
            End If
        Catch ex As System.Exception
            ' Display the error.
            m_Helper.DisplayException(ex, "disconnecting")
            Throw ex
        End Try
    End Sub

    Public Function RunQuery(ByRef connection As AdomdConnection, ByVal queryString As String) As CellSet
        Dim objCommand As AdomdCommand, objCellSet As CellSet

        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        ElseIf (queryString = "") Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentNullException("queryString")
        Else
            ' Create a new AdomdCommand object for the specified connection.
            objCommand = connection.CreateCommand()
            ' Supply the command text for the AdomdCommand object.
            objCommand.CommandText = queryString

            ' Attempt to run the command and retrieve a cellset.
            objCellSet = objCommand.ExecuteCellSet()

            ' Return the retrieved cellset.
            Return objCellSet

            ' Note that this subroutine just retrieves the cellset. For
            ' more information on traversing a CellSet object and
            ' loading it into a grid, see the LoadCellset method of
            ' the Grid object in the AdomdSampleGrid project.
        End If
    End Function

    Public Function GetSchemaDataSet_Actions(ByRef connection As AdomdConnection, _
        ByVal cubeName As String, _
        ByVal coordinate As String, _
        ByVal coordinateType As String, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal actionName As String = Nothing, _
        Optional ByVal actionType As String = Nothing, _
        Optional ByVal invocation As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_ACTIONS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName, _
            actionName, _
            actionType, _
            coordinate, _
            coordinateType, _
            invocation}

        ' Attempt to retrieve the MDSCHEMA_ACTIONS schema rowset.
        objTable = connection.GetSchemaDataSet(AdomdSchemaGuid.Actions, strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Catalogs(ByRef connection As AdomdConnection) As DataTable
        ' Helper function to retrieve the DBSCHEMA_CATALOGS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Attempt to retrieve the DBSCHEMA_CATALOGS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Catalogs, _
            Nothing).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Cubes(ByRef connection As AdomdConnection, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal cubeName As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_CUBES schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName}

        ' Attempt to retrieve the MDSCHEMA_CUBES schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Cubes, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Dimensions(ByRef connection As AdomdConnection, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal cubeName As String = Nothing, _
        Optional ByVal dimensionName As String = Nothing, _
        Optional ByVal dimensionUniqueName As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_DIMENSIONS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName, _
            dimensionName, _
            dimensionUniqueName}

        ' Attempt to retrieve the MDSCHEMA_DIMENSIONS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Dimensions, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Functions(ByRef connection As AdomdConnection, _
        Optional ByVal libraryName As String = Nothing, _
        Optional ByVal interfaceName As String = Nothing, _
        Optional ByVal functionName As String = Nothing, _
        Optional ByVal origin As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_FUNCTIONS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            libraryName, _
            interfaceName, _
            functionName, _
            origin}

        ' Attempt to retrieve the MDSCHEMA_FUNCTIONS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Functions, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Hierarchies(ByRef connection As AdomdConnection, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal cubeName As String = Nothing, _
        Optional ByVal dimensionUniqueName As String = Nothing, _
        Optional ByVal hierarchyName As String = Nothing, _
        Optional ByVal hierarchyUniqueName As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_HIERARCHIES schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName, _
            dimensionUniqueName, _
            hierarchyName, _
            hierarchyUniqueName}

        ' Attempt to retrieve the MDSCHEMA_HIERARCHIES schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Hierarchies, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Keywords(ByRef connection As AdomdConnection, _
        Optional ByVal parameter1 As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_KEYWORDS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            parameter1}

        ' Attempt to retrieve the MDSCHEMA_KEYWORDS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Keywords, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Levels(ByRef connection As AdomdConnection, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal cubeName As String = Nothing, _
        Optional ByVal dimensionUniqueName As String = Nothing, _
        Optional ByVal hierarchyUniqueName As String = Nothing, _
        Optional ByVal levelName As String = Nothing, _
        Optional ByVal levelUniqueName As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_LEVELS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName, _
            dimensionUniqueName, _
            hierarchyUniqueName, _
            levelName, _
            levelUniqueName}

        ' Attempt to retrieve the MDSCHEMA_LEVELS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Levels, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Measures(ByRef connection As AdomdConnection, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal cubeName As String = Nothing, _
        Optional ByVal measureName As String = Nothing, _
        Optional ByVal measureUniqueName As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_MEASURES schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName, _
            measureName, _
            measureUniqueName}

        ' Attempt to retrieve the MDSCHEMA_MEASURES schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Measures, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Members(ByRef connection As AdomdConnection, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal cubeName As String = Nothing, _
        Optional ByVal dimensionUniqueName As String = Nothing, _
        Optional ByVal hierarchyUniqueName As String = Nothing, _
        Optional ByVal levelUniqueName As String = Nothing, _
        Optional ByVal levelNumber As String = Nothing, _
        Optional ByVal memberName As String = Nothing, _
        Optional ByVal memberUniqueName As String = Nothing, _
        Optional ByVal memberCaption As String = Nothing, _
        Optional ByVal memberType As String = Nothing, _
        Optional ByVal treeOperator As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_MEMBERS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName, _
            dimensionUniqueName, _
            hierarchyUniqueName, _
            levelUniqueName, _
            levelNumber, _
            memberName, _
            memberUniqueName, _
            memberCaption, _
            memberType, _
            treeOperator}

        ' Attempt to retrieve the MDSCHEMA_MEMBERS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Members, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_MemberProperties(ByRef connection As AdomdConnection, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal cubeName As String = Nothing, _
        Optional ByVal dimensionUniqueName As String = Nothing, _
        Optional ByVal hierarchyUniqueName As String = Nothing, _
        Optional ByVal levelUniqueName As String = Nothing, _
        Optional ByVal memberUniqueName As String = Nothing, _
        Optional ByVal propertyName As String = Nothing, _
        Optional ByVal propertyType As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_PROPERTIES schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName, _
            dimensionUniqueName, _
            hierarchyUniqueName, _
            levelUniqueName, _
            memberUniqueName, _
            propertyName, _
            propertyType}

        ' Attempt to retrieve the MDSCHEMA_PROPERTIES schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.MemberProperties, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_MiningColumns(ByRef connection As AdomdConnection, _
        Optional ByVal modelCatalog As String = Nothing, _
        Optional ByVal modelSchema As String = Nothing, _
        Optional ByVal modelName As String = Nothing, _
        Optional ByVal columnName As String = Nothing) As DataTable

        ' Helper function to retrieve the DMSCHEMA_MINING_COLUMNS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            modelCatalog, _
            modelSchema, _
            modelName, _
            columnName}

        ' Attempt to retrieve the DMSCHEMA_MINING_COLUMNS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.MiningColumns, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_MiningModelContent(ByRef connection As AdomdConnection, _
        Optional ByVal modelCatalog As String = Nothing, _
        Optional ByVal modelSchema As String = Nothing, _
        Optional ByVal modelName As String = Nothing, _
        Optional ByVal attributeName As String = Nothing, _
        Optional ByVal nodeName As String = Nothing, _
        Optional ByVal nodeUniqueName As String = Nothing, _
        Optional ByVal nodeType As String = Nothing, _
        Optional ByVal nodeGuid As String = Nothing, _
        Optional ByVal nodeCaption As String = Nothing, _
        Optional ByVal treeOperator As String = Nothing) As DataTable

        ' Helper function to retrieve the DMSCHEMA_MINING_MODEL_CONTENT schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            modelCatalog, _
            modelSchema, _
            modelName, _
            attributeName, _
            nodeName, _
            nodeUniqueName, _
            nodeType, _
            nodeGuid, _
            nodeCaption, _
            treeOperator}

        ' Attempt to retrieve the DMSCHEMA_MINING_MODEL_CONTENT schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.MiningModelContent, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_MiningModelContentPmml(ByRef connection As AdomdConnection, _
        Optional ByVal modelCatalog As String = Nothing, _
        Optional ByVal modelSchema As String = Nothing, _
        Optional ByVal modelName As String = Nothing, _
        Optional ByVal modelType As String = Nothing) As DataTable

        ' Helper function to retrieve the DMSCHEMA_MINING_MODEL_CONTENT_PMML schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            modelCatalog, _
            modelSchema, _
            modelName, _
            modelType}

        ' Attempt to retrieve the DMSCHEMA_MINING_MODEL_CONTENT_PMML schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.MiningModelContentPmml, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_MiningModels(ByRef connection As AdomdConnection, _
        Optional ByVal modelCatalog As String = Nothing, _
        Optional ByVal modelSchema As String = Nothing, _
        Optional ByVal modelName As String = Nothing, _
        Optional ByVal modelType As String = Nothing, _
        Optional ByVal serviceName As String = Nothing, _
        Optional ByVal serviceTypeID As String = Nothing) As DataTable

        ' Helper function to retrieve the DMSCHEMA_MINING_MODELS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            modelCatalog, _
            modelSchema, _
            modelName, _
            modelType, _
            serviceName, _
            serviceTypeID}

        ' Attempt to retrieve the DMSCHEMA_MINING_MODELS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.MiningModels, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_MiningServiceParameters(ByRef connection As AdomdConnection, _
        Optional ByVal serviceName As String = Nothing, _
        Optional ByVal parameterName As String = Nothing) As DataTable

        ' Helper function to retrieve the DMSCHEMA_MINING_MODELS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            serviceName, _
            parameterName}

        ' Attempt to retrieve the DMSCHEMA_MINING_MODELS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.MiningServiceParameters, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_MiningServices(ByRef connection As AdomdConnection, _
        Optional ByVal serviceName As String = Nothing, _
        Optional ByVal serviceTypeID As String = Nothing) As DataTable

        ' Helper function to retrieve the DMSCHEMA_MINING_MODELS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            serviceName, _
            serviceTypeID}

        ' Attempt to retrieve the DMSCHEMA_MINING_MODELS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.MiningServices, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Function GetSchemaDataSet_Sets(ByRef connection As AdomdConnection, _
        Optional ByVal catalogName As String = Nothing, _
        Optional ByVal schemaName As String = Nothing, _
        Optional ByVal cubeName As String = Nothing, _
        Optional ByVal setName As String = Nothing, _
        Optional ByVal scope As String = Nothing) As DataTable

        ' Helper function to retrieve the MDSCHEMA_SETS schema rowset from an 
        ' existing AdomdConnection.

        Dim objTable As DataTable

        ' Check if a valid connection was provided.
        If Me.IsConnected(connection) = False Then
            ' A valid AdomdConnection object was not supplied.
            Throw New ArgumentException("A valid, active AdomdConnection object must be supplied.", _
                "connection")
        End If

        ' Construct the restrictions array.
        Dim strRestrictions() As String = { _
            catalogName, _
            schemaName, _
            cubeName, _
            setName, _
            scope}

        ' Attempt to retrieve the MDSCHEMA_SETS schema rowset.
        objTable = connection.GetSchemaDataSet( _
            AdomdSchemaGuid.Sets, _
            strRestrictions).Tables(0)

        Return objTable
    End Function

    Public Sub DisplayException(ByVal ex As Exception, Optional ByVal action As String = "")
        If TypeOf ex Is AdomdConnectionException Then
            ' The connection could not be opened or was disconnected.
            ' This error can occur at any time, if the provider is 
            ' disconnected from the server.
            MsgBox("The connection could not be opened or was disconnected.")
        ElseIf TypeOf ex Is AdomdErrorResponseException Then
            ' A response is received from a provider which indicates an error.
            If action = "" Then
                MsgBox("An error was received from the provider:" & vbCrLf & ex.Message)
            Else
                MsgBox("An error was received from the provider while " & _
                    action & ":" & vbCrLf & ex.Message)
            End If
        ElseIf TypeOf ex Is AdomdUnknownResponseException Then
            ' A response has been returned from the provider that 
            ' was not understood.
            MsgBox("The provider returned an unknown response:" & vbCrLf & ex.Message)
        ElseIf TypeOf ex Is AdomdCacheExpiredException Then
            ' A cached version of an Adomd.NET object is no longer valid.
            ' This error is typically raised when reviewing meta data.
            MsgBox("The cached metadata for one or more objects needs to be refreshed.")
        ElseIf TypeOf ex Is AdomdException Then
            ' Any other error raised by ADOMD.NET.
            MsgBox("ADOMD.NET encountered the following error:" & vbCrLf & ex.Message)
        ElseIf TypeOf ex Is Exception Then
            ' Any other error.
            If action = "" Then
                MsgBox("The following error occurred:" & vbCrLf & ex.Message)
            Else
                MsgBox("The following error occurred while " & _
                    action & ":" & vbCrLf & ex.Message)
            End If
        End If
    End Sub
#End Region

End Class
