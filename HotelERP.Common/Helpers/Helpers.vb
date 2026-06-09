Namespace Helpers
    Public Class ValidationHelper
        Public Shared Function IsValidEmail(email As String) As Boolean
            Try
                Dim addr = New System.Net.Mail.MailAddress(email)
                Return addr.Address = email
            Catch
                Return False
            End Try
        End Function
        
        Public Shared Function IsValidPhoneNumber(phone As String) As Boolean
            If String.IsNullOrWhiteSpace(phone) Then Return False
            Dim cleanPhone = System.Text.RegularExpressions.Regex.Replace(phone, "[^0-9]", "")
            Return cleanPhone.Length >= 10 AndAlso cleanPhone.Length <= 15
        End Function
        
        Public Shared Function IsValidPassword(password As String) As Boolean
            If String.IsNullOrWhiteSpace(password) Then Return False
            Return password.Length >= Constants.AppConstants.PASSWORD_MIN_LENGTH
        End Function
        
        Public Shared Function IsNotEmpty(value As String) As Boolean
            Return Not String.IsNullOrWhiteSpace(value)
        End Function
        
        Public Shared Function IsValidDate(dateValue As String) As Boolean
            Try
                DateTime.ParseExact(dateValue, Constants.AppConstants.DATE_FORMAT, Nothing)
                Return True
            Catch
                Return False
            End Try
        End Function
        
        Public Shared Function IsValidDecimal(value As String) As Boolean
            Try
                Dim result As Decimal
                Return Decimal.TryParse(value, result)
            Catch
                Return False
            End Try
        End Function
    End Class
    
    Public Class EncryptionHelper
        Public Shared Function EncryptPassword(password As String) As String
            Try
                Dim hasher = New System.Security.Cryptography.SHA256Managed()
                Dim data = System.Text.Encoding.UTF8.GetBytes(password)
                Dim hash = hasher.ComputeHash(data)
                Return Convert.ToBase64String(hash)
            Catch ex As Exception
                Throw New Exception("Password encryption failed: " & ex.Message)
            End Try
        End Function
    End Class
    
    Public Class DateHelper
        Public Shared Function GetCurrentDate() As Date
            Return Date.Now.Date
        End Function
        
        Public Shared Function GetCurrentDateTime() As DateTime
            Return DateTime.Now
        End Function
        
        Public Shared Function FormatDate(dateValue As Date) As String
            Return dateValue.ToString(Constants.AppConstants.DATE_FORMAT)
        End Function
        
        Public Shared Function FormatDateTime(dateTimeValue As DateTime) As String
            Return dateTimeValue.ToString(Constants.AppConstants.DATETIME_FORMAT)
        End Function
        
        Public Shared Function CalculateDays(startDate As Date, endDate As Date) As Integer
            Return CInt((endDate - startDate).TotalDays)
        End Function
    End Class
    
    Public Class LoggerHelper
        Private Shared _logFolder As String = Constants.AppConstants.LOG_FOLDER
        
        Public Shared Sub LogInfo(message As String)
            LogMessage("INFO", message)
        End Sub
        
        Public Shared Sub LogWarning(message As String)
            LogMessage("WARNING", message)
        End Sub
        
        Public Shared Sub LogError(message As String, ex As Exception)
            Dim fullMessage = message & vbCrLf & "Exception: " & ex.Message & vbCrLf & "StackTrace: " & ex.StackTrace
            LogMessage("ERROR", fullMessage)
        End Sub
        
        Private Shared Sub LogMessage(level As String, message As String)
            Try
                If Not IO.Directory.Exists(_logFolder) Then
                    IO.Directory.CreateDirectory(_logFolder)
                End If
                
                Dim logFile = IO.Path.Combine(_logFolder, Date.Now.ToString("yyyy-MM-dd") & ".log")
                Dim logEntry = String.Format("[{0}] [{1}] {2}", DateTime.Now.ToString("HH:mm:ss"), level, message)
                
                IO.File.AppendAllText(logFile, logEntry & vbCrLf)
            Catch
                ' Silently fail to avoid breaking application
            End Try
        End Sub
    End Class
End Namespace