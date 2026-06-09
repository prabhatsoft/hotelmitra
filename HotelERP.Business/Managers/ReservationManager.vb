Imports HotelERP.Data.Models

Namespace Business.Managers
    Public Class ReservationManager
        Private _reservationRepository As New DataAccess.ReservationRepository()
        
        Public Function GetReservationByID(reservationID As Integer) As Reservation
            Try
                If reservationID <= 0 Then
                    Throw New Exception("Invalid reservation ID")
                End If
                Return _reservationRepository.GetReservationByID(reservationID)
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting reservation", ex)
                Throw
            End Try
        End Function
        
        Public Function GetGuestReservations(guestID As Integer) As List(Of Reservation)
            Try
                If guestID <= 0 Then
                    Throw New Exception("Invalid guest ID")
                End If
                Return _reservationRepository.GetReservationsByGuestID(guestID)
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting guest reservations", ex)
                Throw
            End Try
        End Function
        
        Public Function CreateReservation(reservation As Reservation) As Integer
            Try
                ' Validate reservation data
                If reservation.GuestID <= 0 OrElse reservation.RoomID <= 0 Then
                    Throw New Exception("Invalid guest or room")
                End If
                
                If reservation.CheckInDate >= reservation.CheckOutDate Then
                    Throw New Exception("Check-out date must be after check-in date")
                End If
                
                If reservation.TotalAmount <= 0 Then
                    Throw New Exception("Total amount must be greater than zero")
                End If
                
                Dim reservationID = _reservationRepository.InsertReservation(reservation)
                Helpers.LoggerHelper.LogInfo("Reservation " & reservationID & " created successfully")
                Return reservationID
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error creating reservation", ex)
                Throw
            End Try
        End Function
        
        Public Function UpdateReservation(reservation As Reservation) As Boolean
            Try
                If reservation.ReservationID <= 0 Then
                    Throw New Exception("Invalid reservation ID")
                End If
                
                If reservation.CheckInDate >= reservation.CheckOutDate Then
                    Throw New Exception("Check-out date must be after check-in date")
                End If
                
                Dim result = _reservationRepository.UpdateReservation(reservation)
                If result Then
                    Helpers.LoggerHelper.LogInfo("Reservation " & reservation.ReservationID & " updated successfully")
                End If
                Return result
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error updating reservation", ex)
                Throw
            End Try
        End Function
        
        Public Function CancelReservation(reservationID As Integer, reason As String) As Boolean
            Try
                If reservationID <= 0 Then
                    Throw New Exception("Invalid reservation ID")
                End If
                
                Dim result = _reservationRepository.CancelReservation(reservationID, reason)
                If result Then
                    Helpers.LoggerHelper.LogInfo("Reservation " & reservationID & " cancelled with reason: " & reason)
                End If
                Return result
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error cancelling reservation", ex)
                Throw
            End Try
        End Function
        
        Public Function GetUpcomingReservations(hotelID As Integer, days As Integer) As List(Of Reservation)
            Try
                If hotelID <= 0 Then
                    Throw New Exception("Invalid hotel ID")
                End If
                Return _reservationRepository.GetUpcomingReservations(hotelID, days)
            Catch ex As Exception
                Helpers.LoggerHelper.LogError("Error getting upcoming reservations", ex)
                Throw
            End Try
        End Function
        
        Public Function CalculateStayDuration(checkInDate As Date, checkOutDate As Date) As Integer
            Return Helpers.DateHelper.CalculateDays(checkInDate, checkOutDate)
        End Function
        
        Public Function CalculateReservationAmount(roomRate As Decimal, nights As Integer, discountPercentage As Decimal) As Decimal
            Dim baseAmount = roomRate * nights
            Dim discountAmount = (baseAmount * discountPercentage) / 100
            Return baseAmount - discountAmount
        End Function
    End Class
End Namespace