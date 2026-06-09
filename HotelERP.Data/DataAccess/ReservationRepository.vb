Imports System.Data
Imports System.Data.SqlClient
Imports HotelERP.Data.Models

Namespace DataAccess
    Public Class ReservationRepository
        
        Public Function GetReservationByID(reservationID As Integer) As Reservation
            Try
                Dim query = "SELECT * FROM Reservations WHERE ReservationID = @ReservationID"
                Dim param = New SqlParameter("@ReservationID", reservationID)
                Dim dt = SqlHelper.GetDataTable(query, New SqlParameter() {param})
                
                If dt.Rows.Count > 0 Then
                    Return MapDataRowToReservation(dt.Rows(0))
                End If
                Return Nothing
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting reservation", ex)
                Throw
            End Try
        End Function
        
        Public Function GetReservationsByGuestID(guestID As Integer) As List(Of Reservation)
            Try
                Dim query = "SELECT * FROM Reservations WHERE GuestID = @GuestID ORDER BY CheckInDate DESC"
                Dim param = New SqlParameter("@GuestID", guestID)
                Dim dt = SqlHelper.GetDataTable(query, New SqlParameter() {param})
                Dim reservations = New List(Of Reservation)()
                
                For Each row As DataRow In dt.Rows
                    reservations.Add(MapDataRowToReservation(row))
                Next
                Return reservations
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting guest reservations", ex)
                Throw
            End Try
        End Function
        
        Public Function GetUpcomingReservations(hotelID As Integer, days As Integer) As List(Of Reservation)
            Try
                Dim query = "SELECT * FROM Reservations WHERE HotelID = @HotelID AND CheckInDate <= DATEADD(day, @Days, CAST(GETDATE() AS DATE)) AND ReservationStatus = 'Confirmed' ORDER BY CheckInDate"
                Dim parameters = New SqlParameter() {
                    New SqlParameter("@HotelID", hotelID),
                    New SqlParameter("@Days", days)
                }
                Dim dt = SqlHelper.GetDataTable(query, parameters)
                Dim reservations = New List(Of Reservation)()
                
                For Each row As DataRow In dt.Rows
                    reservations.Add(MapDataRowToReservation(row))
                Next
                Return reservations
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting upcoming reservations", ex)
                Throw
            End Try
        End Function
        
        Public Function InsertReservation(reservation As Reservation) As Integer
            Try
                Dim query = "INSERT INTO Reservations (HotelID, GuestID, RoomID, CheckInDate, CheckOutDate, NumberOfGuests, NumberOfChildren, ReservationStatus, RoomRate, DiscountPercentage, DiscountAmount, TotalAmount, SpecialRequests, CreatedByUserID) VALUES (@HotelID, @GuestID, @RoomID, @CheckInDate, @CheckOutDate, @NumberOfGuests, @NumberOfChildren, @ReservationStatus, @RoomRate, @DiscountPercentage, @DiscountAmount, @TotalAmount, @SpecialRequests, @CreatedByUserID); SELECT CAST(SCOPE_IDENTITY() AS INT)"
                
                Dim parameters = New SqlParameter() {
                    New SqlParameter("@HotelID", reservation.HotelID),
                    New SqlParameter("@GuestID", reservation.GuestID),
                    New SqlParameter("@RoomID", reservation.RoomID),
                    New SqlParameter("@CheckInDate", reservation.CheckInDate),
                    New SqlParameter("@CheckOutDate", reservation.CheckOutDate),
                    New SqlParameter("@NumberOfGuests", reservation.NumberOfGuests),
                    New SqlParameter("@NumberOfChildren", reservation.NumberOfChildren),
                    New SqlParameter("@ReservationStatus", reservation.ReservationStatus),
                    New SqlParameter("@RoomRate", reservation.RoomRate),
                    New SqlParameter("@DiscountPercentage", reservation.DiscountPercentage),
                    New SqlParameter("@DiscountAmount", reservation.DiscountAmount),
                    New SqlParameter("@TotalAmount", reservation.TotalAmount),
                    New SqlParameter("@SpecialRequests", If(reservation.SpecialRequests, "")),
                    New SqlParameter("@CreatedByUserID", reservation.CreatedByUserID)
                }
                
                Return CInt(SqlHelper.ExecuteScalar(query, parameters))
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error inserting reservation", ex)
                Throw
            End Try
        End Function
        
        Public Function UpdateReservation(reservation As Reservation) As Boolean
            Try
                Dim query = "UPDATE Reservations SET CheckInDate = @CheckInDate, CheckOutDate = @CheckOutDate, NumberOfGuests = @NumberOfGuests, ReservationStatus = @ReservationStatus, RoomRate = @RoomRate, TotalAmount = @TotalAmount, SpecialRequests = @SpecialRequests, LastModifiedDate = GETDATE() WHERE ReservationID = @ReservationID"
                
                Dim parameters = New SqlParameter() {
                    New SqlParameter("@CheckInDate", reservation.CheckInDate),
                    New SqlParameter("@CheckOutDate", reservation.CheckOutDate),
                    New SqlParameter("@NumberOfGuests", reservation.NumberOfGuests),
                    New SqlParameter("@ReservationStatus", reservation.ReservationStatus),
                    New SqlParameter("@RoomRate", reservation.RoomRate),
                    New SqlParameter("@TotalAmount", reservation.TotalAmount),
                    New SqlParameter("@SpecialRequests", If(reservation.SpecialRequests, "")),
                    New SqlParameter("@ReservationID", reservation.ReservationID)
                }
                
                Return SqlHelper.ExecuteNonQuery(query, parameters) > 0
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error updating reservation", ex)
                Throw
            End Try
        End Function
        
        Public Function CancelReservation(reservationID As Integer, reason As String) As Boolean
            Try
                Dim query = "UPDATE Reservations SET ReservationStatus = 'Cancelled', CancellationDate = GETDATE(), CancellationReason = @Reason, LastModifiedDate = GETDATE() WHERE ReservationID = @ReservationID"
                Dim parameters = New SqlParameter() {
                    New SqlParameter("@ReservationID", reservationID),
                    New SqlParameter("@Reason", reason)
                }
                Return SqlHelper.ExecuteNonQuery(query, parameters) > 0
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error cancelling reservation", ex)
                Throw
            End Try
        End Function
        
        Private Function MapDataRowToReservation(row As DataRow) As Reservation
            Return New Reservation With {
                .ReservationID = CInt(row("ReservationID")),
                .HotelID = CInt(row("HotelID")),
                .GuestID = CInt(row("GuestID")),
                .RoomID = CInt(row("RoomID")),
                .CheckInDate = CDate(row("CheckInDate")),
                .CheckOutDate = CDate(row("CheckOutDate")),
                .NumberOfGuests = CInt(row("NumberOfGuests")),
                .NumberOfChildren = CInt(row("NumberOfChildren")),
                .ReservationStatus = CStr(row("ReservationStatus")),
                .RoomRate = CDec(row("RoomRate")),
                .DiscountPercentage = CDec(row("DiscountPercentage")),
                .DiscountAmount = CDec(row("DiscountAmount")),
                .TotalAmount = CDec(row("TotalAmount")),
                .SpecialRequests = If(IsDBNull(row("SpecialRequests")), "", CStr(row("SpecialRequests"))),
                .CreatedByUserID = CInt(row("CreatedByUserID")),
                .CreatedDate = CDate(row("CreatedDate")),
                .LastModifiedDate = CDate(row("LastModifiedDate")),
                .CancellationDate = If(IsDBNull(row("CancellationDate")), Nothing, CDate(row("CancellationDate"))),
                .CancellationReason = If(IsDBNull(row("CancellationReason")), "", CStr(row("CancellationReason")))
            }
        End Function
    End Class
End Namespace