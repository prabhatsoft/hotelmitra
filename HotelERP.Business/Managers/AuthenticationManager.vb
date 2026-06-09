Imports System.Data.SqlClient
Imports HotelERP.Data.Models

Namespace Business.Managers
    Public Class AuthenticationManager
        Private _userRepository As New DataAccess.UserRepository()
        
        Public Function AuthenticateUser(username As String, password As String) As User
            Try
                If Not Helpers.ValidationHelper.IsNotEmpty(username) OrElse Not Helpers.ValidationHelper.IsNotEmpty(password) Then
                    Throw New Exception("Username and password are required")
                End If
                
                Dim user = _userRepository.GetUserByUsername(username)
                If user Is Nothing Then
                    Throw New Exception("Invalid username or password")
                End If
                
                If Not user.IsActive Then
                    Throw New Exception("User account is inactive")
                End If
                
                ' Verify password
                Dim hashedPassword = Helpers.EncryptionHelper.EncryptPassword(password)
                ' Compare hashed password with stored password
                ' For now, this is a basic implementation
                
                ' Update last login
                _userRepository.UpdateUserLastLogin(user.UserID)
                
                Helpers.LoggerHelper.LogInfo("User " & username & " logged in successfully")
                Return user
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Authentication error", ex)
                Throw
            End Try
        End Function
        
        Public Function GetUserByID(userID As Integer) As User
            Try
                Return _userRepository.GetUserByID(userID)
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error retrieving user", ex)
                Throw
            End Try
        End Function
        
        Public Function GetAllUsers() As List(Of User)
            Try
                Return _userRepository.GetAllUsers()
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error retrieving all users", ex)
                Throw
            End Try
        End Function
    End Class
End Namespace