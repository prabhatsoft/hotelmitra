Namespace Models
    Public Class User
        Public Property UserID As Integer
        Public Property Username As String
        Public Property FullName As String
        Public Property Email As String
        Public Property PhoneNumber As String
        Public Property RoleID As Integer
        Public Property RoleName As String
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime
        Public Property LastLoginDate As DateTime
        Public Property LastModifiedDate As DateTime
    End Class
    
    Public Class Guest
        Public Property GuestID As Integer
        Public Property HotelID As Integer
        Public Property FirstName As String
        Public Property LastName As String
        Public Property Email As String
        Public Property PhoneNumber As String
        Public Property Address As String
        Public Property City As String
        Public Property State As String
        Public Property PostalCode As String
        Public Property Country As String
        Public Property IDType As String
        Public Property IDNumber As String
        Public Property DateOfBirth As Date
        Public Property Gender As String
        Public Property CompanyName As String
        Public Property IsVIP As Boolean
        Public Property CreatedDate As DateTime
        Public Property LastModifiedDate As DateTime
    End Class
    
    Public Class Room
        Public Property RoomID As Integer
        Public Property HotelID As Integer
        Public Property RoomNumber As String
        Public Property RoomTypeID As Integer
        Public Property RoomTypeName As String
        Public Property Floor As Integer
        Public Property Status As String
        Public Property LastCleanedDate As DateTime
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime
    End Class
    
    Public Class RoomType
        Public Property RoomTypeID As Integer
        Public Property HotelID As Integer
        Public Property RoomTypeName As String
        Public Property Description As String
        Public Property MaxGuests As Integer
        Public Property BaseRate As Decimal
        Public Property Amenities As String
        Public Property ImageURL As String
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime
    End Class
    
    Public Class Reservation
        Public Property ReservationID As Integer
        Public Property HotelID As Integer
        Public Property GuestID As Integer
        Public Property RoomID As Integer
        Public Property CheckInDate As Date
        Public Property CheckOutDate As Date
        Public Property NumberOfGuests As Integer
        Public Property NumberOfChildren As Integer
        Public Property ReservationStatus As String
        Public Property RoomRate As Decimal
        Public Property DiscountPercentage As Decimal
        Public Property DiscountAmount As Decimal
        Public Property TotalAmount As Decimal
        Public Property SpecialRequests As String
        Public Property CreatedByUserID As Integer
        Public Property CreatedDate As DateTime
        Public Property LastModifiedDate As DateTime
        Public Property CancellationDate As DateTime
        Public Property CancellationReason As String
    End Class
    
    Public Class Bill
        Public Property BillID As Integer
        Public Property ReservationID As Integer
        Public Property GuestID As Integer
        Public Property BillDate As DateTime
        Public Property CheckOutDate As Date
        Public Property RoomCharges As Decimal
        Public Property ServiceCharges As Decimal
        Public Property TaxAmount As Decimal
        Public Property DamageCharges As Decimal
        Public Property OtherCharges As Decimal
        Public Property TotalAmount As Decimal
        Public Property PaidAmount As Decimal
        Public Property DueAmount As Decimal
        Public Property BillStatus As String
        Public Property Notes As String
        Public Property CreatedByUserID As Integer
        Public Property CreatedDate As DateTime
    End Class
    
    Public Class Payment
        Public Property PaymentID As Integer
        Public Property BillID As Integer
        Public Property PaymentDate As DateTime
        Public Property PaymentMethod As String
        Public Property AmountPaid As Decimal
        Public Property ReferenceNumber As String
        Public Property Notes As String
        Public Property CreatedByUserID As Integer
        Public Property CreatedDate As DateTime
    End Class
    
    Public Class InventoryItem
        Public Property ItemID As Integer
        Public Property HotelID As Integer
        Public Property ItemName As String
        Public Property Description As String
        Public Property Category As String
        Public Property UnitOfMeasure As String
        Public Property ReorderLevel As Integer
        Public Property UnitCost As Decimal
        Public Property Supplier As String
        Public Property SupplierContact As String
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime
    End Class
    
    Public Class InventoryStock
        Public Property StockID As Integer
        Public Property ItemID As Integer
        Public Property QuantityInStock As Integer
        Public Property QuantityReserved As Integer
        Public Property LastCountDate As DateTime
        Public Property LastUpdatedDate As DateTime
        Public Property Location As String
    End Class
    
    Public Class Staff
        Public Property StaffID As Integer
        Public Property HotelID As Integer
        Public Property FirstName As String
        Public Property LastName As String
        Public Property EmployeeID As String
        Public Property Email As String
        Public Property PhoneNumber As String
        Public Property DepartmentID As Integer
        Public Property DepartmentName As String
        Public Property Position As String
        Public Property JoinDate As Date
        Public Property SalaryType As String
        Public Property SalaryAmount As Decimal
        Public Property IsActive As Boolean
        Public Property CreatedDate As DateTime
        Public Property LastModifiedDate As DateTime
    End Class
    
    Public Class HousekeepingTask
        Public Property TaskID As Integer
        Public Property HotelID As Integer
        Public Property RoomID As Integer
        Public Property TaskType As String
        Public Property Description As String
        Public Property Priority As String
        Public Property Status As String
        Public Property AssignedToStaffID As Integer
        Public Property CreatedByUserID As Integer
        Public Property CreatedDate As DateTime
        Public Property CompletedDate As DateTime
        Public Property Notes As String
    End Class
End Namespace