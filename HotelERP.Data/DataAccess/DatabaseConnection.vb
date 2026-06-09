Imports System.Data
Imports System.Data.SqlClient

Namespace DataAccess
    Public Class DatabaseConnection
        Private Shared _connectionString As String = ""
        Private Shared _instance As DatabaseConnection = Nothing
        Private Shared _lock As Object = New Object()
        
        Private Sub New()
        End Sub
        
        Public Shared Function GetInstance() As DatabaseConnection
            If _instance Is Nothing Then
                SyncLock _lock
                    If _instance Is Nothing Then
                        _instance = New DatabaseConnection()
                    End If
                End SyncLock
            End If
            Return _instance
        End Function
        
        Public Shared Sub SetConnectionString(connString As String)
            _connectionString = connString
        End Sub
        
        Public Shared Function GetConnectionString() As String
            If String.IsNullOrEmpty(_connectionString) Then
                Throw New Exception("Connection string not configured")
            End If
            Return _connectionString
        End Function
        
        Public Shared Function GetConnection() As SqlConnection
            If String.IsNullOrEmpty(_connectionString) Then
                Throw New Exception("Connection string not configured")
            End If
            Return New SqlConnection(_connectionString)
        End Function
        
        Public Shared Function TestConnection() As Boolean
            Try
                Using conn = GetConnection()
                    conn.Open()
                    Dim cmd = New SqlCommand("SELECT 1", conn)
                    cmd.ExecuteScalar()
                    conn.Close()
                    Return True
                End Using
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Database connection test failed", ex)
                Return False
            End Try
        End Function
    End Class
    
    Public Class SqlHelper
        Public Shared Function ExecuteScalar(query As String, parameters As SqlParameter()) As Object
            Using conn = DatabaseConnection.GetConnection()
                Using cmd = New SqlCommand(query, conn)
                    If parameters IsNot Nothing Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    conn.Open()
                    Return cmd.ExecuteScalar()
                End Using
            End Using
        End Function
        
        Public Shared Function ExecuteNonQuery(query As String, parameters As SqlParameter()) As Integer
            Using conn = DatabaseConnection.GetConnection()
                Using cmd = New SqlCommand(query, conn)
                    cmd.CommandType = CommandType.Text
                    If parameters IsNot Nothing Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    conn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        End Function
        
        Public Shared Function ExecuteStoredProcedure(procName As String, parameters As SqlParameter()) As Integer
            Using conn = DatabaseConnection.GetConnection()
                Using cmd = New SqlCommand(procName, conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    If parameters IsNot Nothing Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    conn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        End Function
        
        Public Shared Function GetDataTable(query As String, parameters As SqlParameter()) As DataTable
            Dim dt = New DataTable()
            Using conn = DatabaseConnection.GetConnection()
                Using cmd = New SqlCommand(query, conn)
                    If parameters IsNot Nothing Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    cmd.CommandTimeout = 300
                    Dim adapter = New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
            Return dt
        End Function
        
        Public Shared Function GetDataSet(query As String, parameters As SqlParameter()) As DataSet
            Dim ds = New DataSet()
            Using conn = DatabaseConnection.GetConnection()
                Using cmd = New SqlCommand(query, conn)
                    If parameters IsNot Nothing Then
                        cmd.Parameters.AddRange(parameters)
                    End If
                    cmd.CommandTimeout = 300
                    Dim adapter = New SqlDataAdapter(cmd)
                    adapter.Fill(ds)
                End Using
            End Using
            Return ds
        End Function
    End Class
End Namespace