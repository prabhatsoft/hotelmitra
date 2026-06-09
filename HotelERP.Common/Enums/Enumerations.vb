Imports System.Runtime.Serialization

Namespace Enums
    <DataContract>
    Public Enum UserRoleEnum
        <EnumMember>
        Administrator = 1
        <EnumMember>
        Manager = 2
        <EnumMember>
        Receptionist = 3
        <EnumMember>
        Housekeeping = 4
        <EnumMember>
        Accounts = 5
        <EnumMember>
        Staff = 6
    End Enum
    
    <DataContract>
    Public Enum RoomStatusEnum
        <EnumMember>
        Available = 1
        <EnumMember>
        Occupied = 2
        <EnumMember>
        Maintenance = 3
        <EnumMember>
        Dirty = 4
        <EnumMember>
        Reserved = 5
    End Enum
    
    <DataContract>
    Public Enum ReservationStatusEnum
        <EnumMember>
        Confirmed = 1
        <EnumMember>
        Cancelled = 2
        <EnumMember>
        Completed = 3
        <EnumMember>
        NoShow = 4
    End Enum
    
    <DataContract>
    Public Enum BillStatusEnum
        <EnumMember>
        Pending = 1
        <EnumMember>
        PartialPaid = 2
        <EnumMember>
        Paid = 3
        <EnumMember>
        Cancelled = 4
    End Enum
    
    <DataContract>
    Public Enum TaskStatusEnum
        <EnumMember>
        Pending = 1
        <EnumMember>
        InProgress = 2
        <EnumMember>
        Completed = 3
        <EnumMember>
        OnHold = 4
    End Enum
    
    <DataContract>
    Public Enum PaymentMethodEnum
        <EnumMember>
        Cash = 1
        <EnumMember>
        CreditCard = 2
        <EnumMember>
        DebitCard = 3
        <EnumMember>
        Check = 4
        <EnumMember>
        Online = 5
    End Enum
    
    <DataContract>
    Public Enum OperationType
        <EnumMember>
        INSERT = 1
        <EnumMember>
        UPDATE = 2
        <EnumMember>
        DELETE = 3
    End Enum
End Namespace