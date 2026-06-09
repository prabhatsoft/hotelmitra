Imports System.Data
Imports System.Data.SqlClient
Imports HotelERP.Data.Models

Namespace DataAccess
    Public Class UserRepository
        Public Function GetUserByUsername(username As String) As User
            Try
                Dim query = "SELECT UserID, Username, FullName, Email, PhoneNumber, RoleID, IsActive, CreatedDate, LastLoginDate, LastModifiedDate FROM Users WHERE Username = @Username"
                Dim param = New SqlParameter("@Username", username)
                Dim dt = SqlHelper.GetDataTable(query, New SqlParameter() {param})
                
                If dt.Rows.Count > 0 Then
                    Return MapDataRowToUser(dt.Rows(0))
                End If
                Return Nothing
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting user by username", ex)
                Throw
            End Try
        End Function
        
        Public Function GetUserByID(userID As Integer) As User
            Try
                Dim query = "SELECT UserID, Username, FullName, Email, PhoneNumber, RoleID, IsActive, CreatedDate, LastLoginDate, LastModifiedDate FROM Users WHERE UserID = @UserID"
                Dim param = New SqlParameter("@UserID", userID)
                Dim dt = SqlHelper.GetDataTable(query, New SqlParameter() {param})
                
                If dt.Rows.Count > 0 Then
                    Return MapDataRowToUser(dt.Rows(0))
                End If
                Return Nothing
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting user by ID", ex)
                Throw
            End Try
        End Function
        
        Public Function GetAllUsers() As List(Of User)
            Try
                Dim query = "SELECT UserID, Username, FullName, Email, PhoneNumber, RoleID, IsActive, CreatedDate, LastLoginDate, LastModifiedDate FROM Users WHERE IsActive = 1"
                Dim dt = SqlHelper.GetDataTable(query, Nothing)
                Dim users = New List(Of User)()
                
                For Each row As DataRow In dt.Rows
                    users.Add(MapDataRowToUser(row))
                Next
                
                Return users
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting all users", ex)
                Throw
            End Try
        End Function
        
        Public Function InsertUser(user As User, password As String) As Boolean
            Try
                Dim hashedPassword = Helpers.EncryptionHelper.EncryptPassword(password)
                Dim query = "INSERT INTO Users (Username, Password, FullName, Email, PhoneNumber, RoleID, IsActive) VALUES (@Username, @Password, @FullName, @Email, @PhoneNumber, @RoleID, @IsActive)"
                
                Dim parameters = New SqlParameter() {
                    New SqlParameter("@Username", user.Username),
                    New SqlParameter("@Password", hashedPassword),
                    New SqlParameter("@FullName", user.FullName),
                    New SqlParameter("@Email", If(user.Email, "")),
                    New SqlParameter("@PhoneNumber", If(user.PhoneNumber, "")),
                    New SqlParameter("@RoleID", user.RoleID),
                    New SqlParameter("@IsActive", user.IsActive)
                }
                
                Return SqlHelper.ExecuteNonQuery(query, parameters) > 0
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error inserting user", ex)
                Throw
            End Try
        End Function
        
        Public Function UpdateUserLastLogin(userID As Integer) As Boolean
            Try
                Dim query = "UPDATE Users SET LastLoginDate = @LastLoginDate WHERE UserID = @UserID"
                Dim parameters = New SqlParameter() {
                    New SqlParameter("@LastLoginDate", DateTime.Now),
                    New SqlParameter("@UserID", userID)
                }
                Return SqlHelper.ExecuteNonQuery(query, parameters) > 0
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error updating last login", ex)
                Throw
            End Try
        End Function
        
        Private Function MapDataRowToUser(row As DataRow) As User
            Return New User With {
                .UserID = CInt(row("UserID")),
                .Username = CStr(row("Username")),
                .FullName = CStr(row("FullName")),
                .Email = If(IsDBNull(row("Email")), "", CStr(row("Email"))),
                .PhoneNumber = If(IsDBNull(row("PhoneNumber")), "", CStr(row("PhoneNumber"))),
                .RoleID = CInt(row("RoleID")),
                .IsActive = CBool(row("IsActive")),
                .CreatedDate = CDate(row("CreatedDate")),
                .LastLoginDate = If(IsDBNull(row("LastLoginDate")), Nothing, CDate(row("LastLoginDate"))),
                .LastModifiedDate = CDate(row("LastModifiedDate"))
            }
        End Function
    End Class
End Namespace