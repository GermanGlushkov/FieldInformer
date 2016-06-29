VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "ConnPool"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Attribute VB_Description = "Connection Pool"
'++
'
' This code supports connection pooling for use by ASP pages.
' It was written for MS OLAP Services, but could work for any OLE DB provider.
'
' It is meant only as a temporary alternative to real OLE DB connection pooling.
' Connection pooling is not fully implemented and is untested in the first release.
' The main problem, as in this code as well, is that session state is not cleared.
' It is with this limitation that this code is presented.
'
' Synopsis:
'    To begin using:
'       set ApplicationObject = Application
'    To terminate:
'       set ApplicationObject = nothing
'    To get a connection:
'       Set con = GetConnectionFromPool( szConnectionString as string)
'    To return a connection:
'       ReturnConnectionToPool( con as ADODB.Connection )
'
' The connection string is used as an index into many possible pools.
' Properties that make a connection distinct include:
'    - DataSource       (server name)
'    - user id          (we use NT integrated security)
'    - initial catalog  (database)
' So you will need to ensure separation of pools for each combination of these values.
' Just ensure that the connection string includes the database,
' using "Initial Catalog=xxx".
' Because NT Integrated security is used, you should ensure that the UID is set.
' The provider ignores this value, but it can be used here to manage pools
' separately.
'
' We require that each ASP page have session state.
' This is so that the session can time out.
' You could disable with <%@ EnableSessionState=False %>.
'
' We create new connections on demand.
' We destroy the entire pool when the last session times out.
' This would normally occur after 20 minutes of no activity.
'
' A preferred approach would be to create an MTS (Microsft Transaction Services)
' object, which would encapsulate the connection.
' You would need to expose IObjectControl, which has the CanBePooled method.
' This might be a better approach but not so straightforward.
' The best approach is to use OLE DB connection pooling, but that
' has to be supported by the provider.
'
' On a practical level, it is expected that an application
' would have a limited number of connections.
' It is generally a good idea to limit the number
' of separate pools and elements in each pool.
' (i.e. having 10000 connections means something is wrong...)
' You can change these numbers at will.
'
' Internal notes:
' We want to allow users to simply return the connection to the pool,
' while maintaining separate pools.  The connection string we get
' back from ADO (the provider) is not necessarily the same as
' what we used for creation.  So we create a second index of
' returned connection strings, so that we can discover the original
' connection strings.
'
' Created 1-Apr-1999, Eric Jacobsen
'
'--

Option Explicit


Const MAX_NUM_POOLS = 100
Const MAX_POOL_SIZE = 50

Private m_rgcCon(MAX_NUM_POOLS) As Integer                      ' count of connections in a pool
Private m_rgCon(MAX_NUM_POOLS, MAX_POOL_SIZE) As ADODB.Connection   ' connection pools
Private m_colConnStr As New Collection
Private m_colConnStrAux As New Collection


Public Function MaxPoolSize() As Integer

    Dim i As Integer
    Dim max As Integer

    For i = 0 To UBound(m_rgcCon)
        If max < m_rgcCon(i) Then max = m_rgcCon(i)
    Next i
    
    MaxPoolSize = max
    
End Function




Public Function GetConnectionFromPool(szConnectionString As Variant) As ADODB.Connection
Attribute GetConnectionFromPool.VB_Description = "Get a connection from the pool, or create a new connection."
    Dim iPool As Integer

    '
    ' Get an existing connection from the pool
    ' or create a new one.
    '
    ' First get the pool number from the connection string.
    ' VB implements this as a red-black tree, should be fast.
    '    key = connection string (from caller)
    '    val = index into other arrays
    '
    ' We maintain an auxilliary index:
    '    key = connection string from ADODB.Connection.ConnectionString
    '    val = connection string from caller
    '
    Dim con As ADODB.Connection
    
    Do While (True)

        Set con = Nothing

        On Error Resume Next
        iPool = -1
        iPool = m_colConnStr.Item(szConnectionString)

        ' Pop element off stack.
        ' For last element, we remove the primary index,
        ' so that we don't have a pool any more.
        ' Leave the secondary index so we know how to get back.

        If (iPool >= 0) Then
            If (m_rgcCon(iPool) > 0) Then
                m_rgcCon(iPool) = m_rgcCon(iPool) - 1
                Set con = m_rgCon(iPool, m_rgcCon(iPool))
                Set m_rgCon(iPool, m_rgcCon(iPool)) = Nothing
            End If

            If (m_rgcCon(iPool) <= 0) Then
                On Error Resume Next
                m_colConnStr.Remove (szConnectionString)
            End If
        End If

        ' We did not find any connections in the pool
        ' associated with the given connection string
        ' Exit the loop, and create a new connection
        If (con Is Nothing) Then
            Exit Do
        End If

        ' We found a connection, now check if it is valid
        If (IsConnectionValid(con)) Then
            Set GetConnectionFromPool = con
            Exit Function
        End If

    Loop

    ' Create a new connection.
    ' Errors will propogate to caller.

    On Error GoTo 0
    Set con = New ADODB.Connection
    con.Open szConnectionString

    ' Create an auxilliary index so we know where to put it later.
    ' OK if key already exists.
    ' @todo: Should we verify that if exists it is same?

    On Error Resume Next
    m_colConnStrAux.Add szConnectionString, con.ConnectionString

    Set GetConnectionFromPool = con
End Function


Public Sub ReturnConnectionToPool(con As Variant)
Attribute ReturnConnectionToPool.VB_Description = "Return a connection to the pool."
    Dim szConnectionString As String
    Dim iPool As Integer

    '
    ' Return a connection to the pool.
    '
    ' First discover which pool this connection belongs to.
    ' Primary index is original connection string from user.
    ' Secondary index is connection string from ADODB.
    '

    If (con Is Nothing) Then
        Exit Sub
    End If
    
    On Error Resume Next
    szConnectionString = m_colConnStrAux.Item(con.ConnectionString)

    ' We should always have this mapping.
    ' We need it to find our connection.
    ' Note that the secondary index is never deleted.
    
    If (Len(szConnectionString) = 0) Then
        Debug.Print "Error: Could not map connection string."
        Set con = Nothing
        Exit Sub
    End If

    On Error Resume Next
    iPool = -1
    iPool = m_colConnStr.Item(szConnectionString)
    On Error GoTo 0

    If (iPool < 0) Then
        ' Allocate a new pool
        For iPool = 0 To UBound(m_rgcCon)
            If (m_rgcCon(iPool) = 0) Then
                ' create a mapping between the pool and the connection string
                On Error Resume Next
                m_colConnStr.Add iPool, szConnectionString
                
                ' add the connection to the pool
                Set m_rgCon(iPool, 0) = con
                
                ' increase the count of connections in this pool
                m_rgcCon(iPool) = 1
                
                GoTo ClearReferenceAndReturn
            End If
        Next iPool

        ' No empty pools, just dispose of the connection
        Debug.Print "No empty pools."
        GoTo ClearReferenceAndReturn
    Else
        ' Put in an existing pool.
        If (m_rgcCon(iPool) < MAX_POOL_SIZE) Then
            Set m_rgCon(iPool, m_rgcCon(iPool)) = con
            m_rgcCon(iPool) = m_rgcCon(iPool) + 1
        End If
    End If

ClearReferenceAndReturn:

    Set con = Nothing

End Sub


Public Sub ClearConnectionPool()
Attribute ClearConnectionPool.VB_Description = "Clear the connection pool."
    Dim con As ADODB.Connection
    Dim iPool As Integer

    '
    ' Clear all connections from pool.
    ' Note that disconnecting might take a while.
    '

    While (m_colConnStr.Count)
        m_colConnStr.Remove 1
    Wend
    While (m_colConnStrAux.Count)
        m_colConnStrAux.Remove 1
    Wend

    For iPool = 0 To UBound(m_rgcCon)
        Do While (True)
            If (m_rgcCon(iPool) = 0) Then
                Exit Do
            End If

            If (m_rgcCon(iPool)) Then
                Set con = m_rgCon(iPool, m_rgcCon(iPool))
                Set m_rgCon(iPool, m_rgcCon(iPool)) = Nothing
                m_rgcCon(iPool) = m_rgcCon(iPool) - 1
            End If

            Set con = Nothing
        Loop
    Next iPool

End Sub


Private Function IsConnectionValid(con As ADODB.Connection)

    '
    ' Test the connection.
    ' There might have been some error that causes it to no longer be valid.
    ' Note that users of the pool should not return an invalid object.
    ' (There wouldn't be any value in doing so...)
    ' One way is to get the CATALOGS schema rowset, it should be small and cheap.
    '
    ' Fortunately there is a property used by OLE DB connection pooling
    ' that we can use.  It is supported in version 1 of Plato.
    ' It checks for internal zombie state, and some other things.
    '
    ' From OLEDB.H:
    ' DBPROP_RESETDATASOURCE    = 0xf7L
    ' #define DBPROPVAL_RD_RESETALL     0xffffffffL
    '

    IsConnectionValid = False

    On Error GoTo ErrorTrap
    con.Properties("Reset Datasource") = -1

    IsConnectionValid = True

ErrorTrap:

End Function


Private Sub Class_Initialize()
    ' Nothing for us to do
    Debug.Print "init"
End Sub

Private Sub Class_Terminate()
    ' Note that these get cleaned up when we go away.
    ClearConnectionPool
End Sub


