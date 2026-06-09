USE HotelMitra
GO

-- ==================================================
-- USERS AND AUTHENTICATION
-- ==================================================

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    RoleID INT NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastLoginDate DATETIME,
    LastModifiedDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(MAX),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE Permissions (
    PermissionID INT PRIMARY KEY IDENTITY(1,1),
    PermissionName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(MAX),
    ModuleName NVARCHAR(50),
    IsActive BIT DEFAULT 1
)
GO

CREATE TABLE RolePermissions (
    RolePermissionID INT PRIMARY KEY IDENTITY(1,1),
    RoleID INT NOT NULL,
    PermissionID INT NOT NULL,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
    FOREIGN KEY (PermissionID) REFERENCES Permissions(PermissionID),
    UNIQUE(RoleID, PermissionID)
)
GO

-- ==================================================
-- HOTEL CONFIGURATION
-- ==================================================

CREATE TABLE Hotels (
    HotelID INT PRIMARY KEY IDENTITY(1,1),
    HotelName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(MAX),
    City NVARCHAR(50),
    State NVARCHAR(50),
    PostalCode NVARCHAR(20),
    PhoneNumber NVARCHAR(20),
    Email NVARCHAR(100),
    WebsiteURL NVARCHAR(200),
    TaxID NVARCHAR(50),
    CurrencyCode NVARCHAR(3) DEFAULT 'USD',
    TimeZone NVARCHAR(50),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE RoomTypes (
    RoomTypeID INT PRIMARY KEY IDENTITY(1,1),
    HotelID INT NOT NULL,
    RoomTypeName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    MaxGuests INT DEFAULT 2,
    BaseRate DECIMAL(10,2) NOT NULL,
    Amenities NVARCHAR(MAX),
    ImageURL NVARCHAR(500),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID)
)
GO

CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY IDENTITY(1,1),
    HotelID INT NOT NULL,
    RoomNumber NVARCHAR(20) NOT NULL,
    RoomTypeID INT NOT NULL,
    Floor INT,
    Status NVARCHAR(20) DEFAULT 'Available', -- Available, Occupied, Maintenance, Dirty
    LastCleanedDate DATETIME,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID),
    FOREIGN KEY (RoomTypeID) REFERENCES RoomTypes(RoomTypeID),
    UNIQUE(HotelID, RoomNumber)
)
GO

-- ==================================================
-- GUESTS
-- ==================================================

CREATE TABLE Guests (
    GuestID INT PRIMARY KEY IDENTITY(1,1),
    HotelID INT NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    Address NVARCHAR(MAX),
    City NVARCHAR(50),
    State NVARCHAR(50),
    PostalCode NVARCHAR(20),
    Country NVARCHAR(50),
    IDType NVARCHAR(20), -- Passport, DrivingLicense, etc.
    IDNumber NVARCHAR(50),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    CompanyName NVARCHAR(100),
    CreditCardLast4 NVARCHAR(4),
    IsVIP BIT DEFAULT 0,
    PreferredLanguage NVARCHAR(20),
    SpecialRequests NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID)
)
GO

-- ==================================================
-- RESERVATIONS
-- ==================================================

CREATE TABLE Reservations (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
    HotelID INT NOT NULL,
    GuestID INT NOT NULL,
    RoomID INT NOT NULL,
    CheckInDate DATE NOT NULL,
    CheckOutDate DATE NOT NULL,
    NumberOfGuests INT DEFAULT 1,
    NumberOfChildren INT DEFAULT 0,
    ReservationStatus NVARCHAR(20) DEFAULT 'Confirmed', -- Confirmed, Cancelled, NoShow, Completed
    RoomRate DECIMAL(10,2) NOT NULL,
    DiscountPercentage DECIMAL(5,2) DEFAULT 0,
    DiscountAmount DECIMAL(10,2) DEFAULT 0,
    TotalAmount DECIMAL(10,2) NOT NULL,
    SpecialRequests NVARCHAR(MAX),
    CreatedByUserID INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedByUserID INT,
    LastModifiedDate DATETIME DEFAULT GETDATE(),
    CancellationDate DATETIME,
    CancellationReason NVARCHAR(MAX),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID),
    FOREIGN KEY (GuestID) REFERENCES Guests(GuestID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID),
    FOREIGN KEY (ModifiedByUserID) REFERENCES Users(UserID)
)
GO

-- ==================================================
-- CHECK-IN / CHECK-OUT
-- ==================================================

CREATE TABLE CheckInCheckOut (
    CheckInCheckOutID INT PRIMARY KEY IDENTITY(1,1),
    ReservationID INT NOT NULL,
    CheckInTime DATETIME,
    CheckOutTime DATETIME,
    CheckInByUserID INT,
    CheckOutByUserID INT,
    RoomConditionOnCheckIn NVARCHAR(MAX),
    RoomConditionOnCheckOut NVARCHAR(MAX),
    DamageNotes NVARCHAR(MAX),
    DamageAmount DECIMAL(10,2) DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (CheckInByUserID) REFERENCES Users(UserID),
    FOREIGN KEY (CheckOutByUserID) REFERENCES Users(UserID)
)
GO

-- ==================================================
-- HOUSEKEEPING
-- ==================================================

CREATE TABLE HousekeepingTasks (
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    HotelID INT NOT NULL,
    RoomID INT NOT NULL,
    TaskType NVARCHAR(50), -- Cleaning, Maintenance, Repair
    Description NVARCHAR(MAX),
    Priority NVARCHAR(20), -- Low, Medium, High, Urgent
    Status NVARCHAR(20) DEFAULT 'Pending', -- Pending, InProgress, Completed, OnHold
    AssignedToStaffID INT,
    CreatedByUserID INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    StartedDate DATETIME,
    CompletedDate DATETIME,
    Notes NVARCHAR(MAX),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
)
GO

CREATE TABLE RoomMaintenanceLogs (
    MaintenanceID INT PRIMARY KEY IDENTITY(1,1),
    RoomID INT NOT NULL,
    MaintenanceDate DATETIME DEFAULT GETDATE(),
    Description NVARCHAR(MAX),
    Category NVARCHAR(50),
    CompletedDate DATETIME,
    Cost DECIMAL(10,2),
    Notes NVARCHAR(MAX),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
)
GO

-- ==================================================
-- BILLING AND PAYMENTS
-- ==================================================

CREATE TABLE BillingItems (
    BillingItemID INT PRIMARY KEY IDENTITY(1,1),
    BillingItemName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    UnitPrice DECIMAL(10,2) NOT NULL,
    Category NVARCHAR(50),
    IsActive BIT DEFAULT 1
)
GO

CREATE TABLE Bills (
    BillID INT PRIMARY KEY IDENTITY(1,1),
    ReservationID INT NOT NULL,
    GuestID INT NOT NULL,
    BillDate DATETIME DEFAULT GETDATE(),
    CheckOutDate DATE,
    RoomCharges DECIMAL(10,2) DEFAULT 0,
    ServiceCharges DECIMAL(10,2) DEFAULT 0,
    TaxAmount DECIMAL(10,2) DEFAULT 0,
    DamageCharges DECIMAL(10,2) DEFAULT 0,
    OtherCharges DECIMAL(10,2) DEFAULT 0,
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaidAmount DECIMAL(10,2) DEFAULT 0,
    DueAmount DECIMAL(10,2) NOT NULL,
    BillStatus NVARCHAR(20) DEFAULT 'Pending', -- Pending, PartialPaid, Paid
    Notes NVARCHAR(MAX),
    CreatedByUserID INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (GuestID) REFERENCES Guests(GuestID),
    FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
)
GO

CREATE TABLE BillDetails (
    BillDetailID INT PRIMARY KEY IDENTITY(1,1),
    BillID INT NOT NULL,
    BillingItemID INT NOT NULL,
    Quantity DECIMAL(10,2) DEFAULT 1,
    UnitPrice DECIMAL(10,2) NOT NULL,
    TotalPrice DECIMAL(10,2) NOT NULL,
    Description NVARCHAR(MAX),
    FOREIGN KEY (BillID) REFERENCES Bills(BillID),
    FOREIGN KEY (BillingItemID) REFERENCES BillingItems(BillingItemID)
)
GO

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    BillID INT NOT NULL,
    PaymentDate DATETIME DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50), -- Cash, CreditCard, DebitCard, Check, Online
    AmountPaid DECIMAL(10,2) NOT NULL,
    ReferenceNumber NVARCHAR(50),
    Notes NVARCHAR(MAX),
    CreatedByUserID INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (BillID) REFERENCES Bills(BillID),
    FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
)
GO

-- ==================================================
-- INVENTORY AND SUPPLIES
-- ==================================================

CREATE TABLE InventoryItems (
    ItemID INT PRIMARY KEY IDENTITY(1,1),
    HotelID INT NOT NULL,
    ItemName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Category NVARCHAR(50),
    UnitOfMeasure NVARCHAR(20),
    ReorderLevel INT DEFAULT 10,
    UnitCost DECIMAL(10,2) NOT NULL,
    Supplier NVARCHAR(100),
    SupplierContact NVARCHAR(50),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID)
)
GO

CREATE TABLE InventoryStock (
    StockID INT PRIMARY KEY IDENTITY(1,1),
    ItemID INT NOT NULL,
    QuantityInStock INT DEFAULT 0,
    QuantityReserved INT DEFAULT 0,
    LastCountDate DATETIME,
    LastUpdatedDate DATETIME DEFAULT GETDATE(),
    Location NVARCHAR(100),
    FOREIGN KEY (ItemID) REFERENCES InventoryItems(ItemID)
)
GO

CREATE TABLE InventoryTransactions (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    ItemID INT NOT NULL,
    TransactionType NVARCHAR(20), -- IN, OUT, ADJUSTMENT, DAMAGE
    Quantity INT NOT NULL,
    TransactionDate DATETIME DEFAULT GETDATE(),
    Reason NVARCHAR(MAX),
    CreatedByUserID INT NOT NULL,
    Notes NVARCHAR(MAX),
    FOREIGN KEY (ItemID) REFERENCES InventoryItems(ItemID),
    FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID)
)
GO

-- ==================================================
-- STAFF MANAGEMENT
-- ==================================================

CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    HotelID INT NOT NULL,
    DepartmentName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX),
    ManagerID INT,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID)
)
GO

CREATE TABLE Staff (
    StaffID INT PRIMARY KEY IDENTITY(1,1),
    HotelID INT NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    EmployeeID NVARCHAR(50) UNIQUE NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    DepartmentID INT NOT NULL,
    Position NVARCHAR(50),
    JoinDate DATE NOT NULL,
    SalaryType NVARCHAR(20), -- Monthly, Hourly, Daily
    SalaryAmount DECIMAL(10,2),
    Address NVARCHAR(MAX),
    City NVARCHAR(50),
    State NVARCHAR(50),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    IDType NVARCHAR(20),
    IDNumber NVARCHAR(50),
    EmergencyContactName NVARCHAR(100),
    EmergencyContactPhone NVARCHAR(20),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastModifiedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (HotelID) REFERENCES Hotels(HotelID),
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
)
GO

CREATE TABLE StaffShifts (
    ShiftID INT PRIMARY KEY IDENTITY(1,1),
    StaffID INT NOT NULL,
    ShiftDate DATE NOT NULL,
    ShiftType NVARCHAR(20), -- Morning, Afternoon, Night, Full
    StartTime TIME,
    EndTime TIME,
    IsPresent BIT DEFAULT 1,
    CheckInTime TIME,
    CheckOutTime TIME,
    OvertimeHours DECIMAL(5,2) DEFAULT 0,
    Notes NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StaffID) REFERENCES Staff(StaffID)
)
GO

-- ==================================================
-- REPORTS AND AUDIT
-- ==================================================

CREATE TABLE AuditLog (
    AuditID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    TableName NVARCHAR(100),
    Operation NVARCHAR(20), -- INSERT, UPDATE, DELETE
    OldValues NVARCHAR(MAX),
    NewValues NVARCHAR(MAX),
    AuditDate DATETIME DEFAULT GETDATE(),
    IPAddress NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
)
GO

CREATE TABLE SystemLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    Action NVARCHAR(100),
    Details NVARCHAR(MAX),
    Severity NVARCHAR(20), -- Info, Warning, Error
    LogDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
)
GO

-- ==================================================
-- SYNCHRONIZATION TABLES (For Offline Sync)
-- ==================================================

CREATE TABLE SyncLog (
    SyncID INT PRIMARY KEY IDENTITY(1,1),
    TableName NVARCHAR(100),
    RecordID INT,
    Operation NVARCHAR(20), -- INSERT, UPDATE, DELETE
    IsSynced BIT DEFAULT 0,
    SyncDate DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE(),
    DeviceID NVARCHAR(100)
)
GO

-- ==================================================
-- CREATE INDEXES
-- ==================================================

CREATE INDEX IX_Users_Username ON Users(Username)
CREATE INDEX IX_Guests_Email ON Guests(Email)
CREATE INDEX IX_Guests_PhoneNumber ON Guests(PhoneNumber)
CREATE INDEX IX_Reservations_GuestID ON Reservations(GuestID)
CREATE INDEX IX_Reservations_CheckInDate ON Reservations(CheckInDate)
CREATE INDEX IX_Reservations_CheckOutDate ON Reservations(CheckOutDate)
CREATE INDEX IX_Reservations_Status ON Reservations(ReservationStatus)
CREATE INDEX IX_Bills_ReservationID ON Bills(ReservationID)
CREATE INDEX IX_Bills_GuestID ON Bills(GuestID)
CREATE INDEX IX_Bills_Status ON Bills(BillStatus)
CREATE INDEX IX_Payments_BillID ON Payments(BillID)
CREATE INDEX IX_InventoryStock_ItemID ON InventoryStock(ItemID)
CREATE INDEX IX_Staff_EmployeeID ON Staff(EmployeeID)
CREATE INDEX IX_StaffShifts_ShiftDate ON StaffShifts(ShiftDate)
CREATE INDEX IX_AuditLog_UserID ON AuditLog(UserID)
CREATE INDEX IX_AuditLog_AuditDate ON AuditLog(AuditDate)

PRINT 'All tables created successfully.'
GO
