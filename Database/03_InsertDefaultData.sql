USE HotelMitra
GO

-- ==================================================
-- INSERT DEFAULT ROLES
-- ==================================================

INSERT INTO Roles (RoleName, Description, IsActive) VALUES
('Administrator', 'System administrator with full access', 1),
('Manager', 'Hotel manager with management access', 1),
('Receptionist', 'Front desk receptionist', 1),
('Housekeeping', 'Housekeeping staff', 1),
('Accounts', 'Accounting department staff', 1),
('Staff', 'General staff member', 1)
GO

-- ==================================================
-- INSERT PERMISSIONS
-- ==================================================

INSERT INTO Permissions (PermissionName, Description, ModuleName, IsActive) VALUES
('ViewReservations', 'View all reservations', 'Reservations', 1),
('CreateReservation', 'Create new reservation', 'Reservations', 1),
('ModifyReservation', 'Modify reservation details', 'Reservations', 1),
('CancelReservation', 'Cancel reservation', 'Reservations', 1),
('ViewGuests', 'View guest information', 'Guests', 1),
('CreateGuest', 'Create new guest record', 'Guests', 1),
('ModifyGuest', 'Modify guest information', 'Guests', 1),
('CheckInGuest', 'Check-in guest', 'CheckIn', 1),
('CheckOutGuest', 'Check-out guest', 'CheckOut', 1),
('ViewBills', 'View bills and invoices', 'Billing', 1),
('CreateBill', 'Create new bill', 'Billing', 1),
('ProcessPayment', 'Process payment', 'Billing', 1),
('ViewInventory', 'View inventory', 'Inventory', 1),
('ManageInventory', 'Manage inventory stock', 'Inventory', 1),
('ViewReports', 'View reports', 'Reports', 1),
('GenerateReports', 'Generate custom reports', 'Reports', 1),
('ManageUsers', 'Manage user accounts', 'Users', 1),
('ViewAuditLog', 'View audit logs', 'Audit', 1),
('ManageRooms', 'Manage room information', 'Rooms', 1),
('ManageHousekeeping', 'Manage housekeeping tasks', 'Housekeeping', 1)
GO

-- ==================================================
-- ASSIGN PERMISSIONS TO ROLES
-- ==================================================

-- Administrator has all permissions
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 1, PermissionID FROM Permissions WHERE IsActive = 1
GO

-- Manager permissions
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 2, PermissionID FROM Permissions 
WHERE PermissionName IN ('ViewReservations', 'CreateReservation', 'ModifyReservation', 'ViewGuests', 
'CreateGuest', 'ModifyGuest', 'ViewBills', 'CreateBill', 'ProcessPayment', 'ViewInventory', 
'ManageInventory', 'ViewReports', 'GenerateReports', 'ManageRooms', 'ManageHousekeeping', 'CheckInGuest', 'CheckOutGuest')
GO

-- Receptionist permissions
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 3, PermissionID FROM Permissions 
WHERE PermissionName IN ('ViewReservations', 'CreateReservation', 'ModifyReservation', 'ViewGuests', 
'CreateGuest', 'ViewBills', 'CreateBill', 'CheckInGuest', 'CheckOutGuest')
GO

-- Housekeeping permissions
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 4, PermissionID FROM Permissions 
WHERE PermissionName IN ('ViewReservations', 'ManageHousekeeping', 'ManageRooms')
GO

-- Accounts permissions
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 5, PermissionID FROM Permissions 
WHERE PermissionName IN ('ViewBills', 'CreateBill', 'ProcessPayment', 'ViewReports', 'GenerateReports')
GO

-- ==================================================
-- INSERT DEFAULT ADMIN USER
-- ==================================================

INSERT INTO Users (Username, Password, FullName, Email, PhoneNumber, RoleID, IsActive)
VALUES ('admin', 'admin@123', 'System Administrator', 'admin@hotelmitra.com', '+1-800-HOTEL', 1, 1)
GO

-- ==================================================
-- INSERT DEFAULT HOTEL
-- ==================================================

INSERT INTO Hotels (HotelName, Address, City, State, PostalCode, PhoneNumber, Email, TimeZone, CurrencyCode, IsActive)
VALUES ('Hotel Mitra - Main', '123 Hotel Street', 'New York', 'NY', '10001', '+1-800-HOTEL', 'info@hotelmitra.com', 'EST', 'USD', 1)
GO

-- ==================================================
-- INSERT ROOM TYPES
-- ==================================================

DECLARE @HotelID INT = 1

INSERT INTO RoomTypes (HotelID, RoomTypeName, Description, MaxGuests, BaseRate, Amenities, IsActive)
VALUES 
(@HotelID, 'Standard Room', 'Single bedroom with basic amenities', 2, 100.00, 'WiFi, TV, Air Conditioning, Private Bathroom', 1),
(@HotelID, 'Deluxe Room', 'Premium single room with better amenities', 2, 150.00, 'WiFi, Smart TV, Air Conditioning, Private Bathroom, Mini Bar', 1),
(@HotelID, 'Suite', 'Two bedroom suite with premium amenities', 4, 250.00, 'WiFi, Smart TV, AC, Jacuzzi, Mini Bar, Work Desk, Living Area', 1),
(@HotelID, 'Family Room', 'Multi-room accommodation for families', 6, 200.00, 'WiFi, Multiple TVs, AC, Family Bathroom, Kitchen Amenities', 1)
GO

-- ==================================================
-- INSERT ROOMS
-- ==================================================

DECLARE @HotelID INT = 1

INSERT INTO Rooms (HotelID, RoomNumber, RoomTypeID, Floor, Status, IsActive)
SELECT @HotelID, '101', 1, 1, 'Available', 1 UNION ALL
SELECT @HotelID, '102', 1, 1, 'Available', 1 UNION ALL
SELECT @HotelID, '103', 1, 1, 'Available', 1 UNION ALL
SELECT @HotelID, '104', 2, 1, 'Available', 1 UNION ALL
SELECT @HotelID, '105', 2, 1, 'Available', 1 UNION ALL
SELECT @HotelID, '201', 1, 2, 'Available', 1 UNION ALL
SELECT @HotelID, '202', 1, 2, 'Available', 1 UNION ALL
SELECT @HotelID, '203', 3, 2, 'Available', 1 UNION ALL
SELECT @HotelID, '204', 4, 2, 'Available', 1 UNION ALL
SELECT @HotelID, '301', 1, 3, 'Available', 1
GO

-- ==================================================
-- INSERT DEPARTMENTS
-- ==================================================

DECLARE @HotelID INT = 1

INSERT INTO Departments (HotelID, DepartmentName, Description, IsActive)
VALUES
(@HotelID, 'Front Desk', 'Reception and reservations', 1),
(@HotelID, 'Housekeeping', 'Room cleaning and maintenance', 1),
(@HotelID, 'Accounts', 'Billing and payments', 1),
(@HotelID, 'Maintenance', 'Property maintenance', 1),
(@HotelID, 'Food & Beverage', 'Restaurant and bar services', 1)
GO

-- ==================================================
-- INSERT SAMPLE STAFF
-- ==================================================

DECLARE @HotelID INT = 1

INSERT INTO Staff (HotelID, FirstName, LastName, EmployeeID, Email, DepartmentID, Position, JoinDate, SalaryType, SalaryAmount, IsActive)
VALUES
(@HotelID, 'John', 'Smith', 'EMP001', 'john.smith@hotelmitra.com', 1, 'Receptionist', '2023-01-15', 'Monthly', 2000.00, 1),
(@HotelID, 'Maria', 'Garcia', 'EMP002', 'maria.garcia@hotelmitra.com', 2, 'Housekeeper', '2023-02-20', 'Monthly', 1800.00, 1),
(@HotelID, 'Robert', 'Johnson', 'EMP003', 'robert.johnson@hotelmitra.com', 3, 'Accountant', '2023-03-10', 'Monthly', 2500.00, 1)
GO

-- ==================================================
-- INSERT BILLING ITEMS
-- ==================================================

INSERT INTO BillingItems (BillingItemName, Description, UnitPrice, Category, IsActive)
VALUES
('Room Charges', 'Nightly room charge', 0, 'Room', 1),
('Room Service', 'In-room dining service', 15.00, 'Service', 1),
('Laundry Service', 'Laundry and ironing', 10.00, 'Service', 1),
('Spa Treatment', 'Spa and wellness', 50.00, 'Amenity', 1),
('Mini Bar', 'Mini bar beverages', 5.00, 'Beverage', 1),
('Parking', 'Overnight parking', 20.00, 'Facility', 1),
('Extra Bed', 'Additional bed', 25.00, 'Room', 1),
('Breakfast', 'Complimentary breakfast', 15.00, 'Meal', 1)
GO

-- ==================================================
-- INSERT INVENTORY ITEMS
-- ==================================================

DECLARE @HotelID INT = 1

INSERT INTO InventoryItems (HotelID, ItemName, Description, Category, UnitOfMeasure, ReorderLevel, UnitCost, Supplier, IsActive)
VALUES
(@HotelID, 'Bed Sheets', 'Queen size bed sheets', 'Linens', 'pcs', 50, 20.00, 'Linen Supplier Co', 1),
(@HotelID, 'Towels', 'Bath towels', 'Linens', 'pcs', 100, 8.00, 'Linen Supplier Co', 1),
(@HotelID, 'Soap Dispenser', 'Room soap dispensers', 'Amenities', 'pcs', 20, 5.00, 'Amenity Plus', 1),
(@HotelID, 'Toilet Paper', 'Bathroom toilet paper', 'Supplies', 'rolls', 200, 2.00, 'Paper Supplies Inc', 1),
(@HotelID, 'Cleaning Solution', 'General cleaning solution', 'Cleaning', 'liters', 30, 15.00, 'Clean-All Corp', 1),
(@HotelID, 'Light Bulbs', 'LED light bulbs', 'Maintenance', 'pcs', 50, 5.00, 'Electrical Supplies', 1)
GO

-- Initialize stock
INSERT INTO InventoryStock (ItemID, QuantityInStock, Location)
SELECT ItemID, 100, 'Store Room' FROM InventoryItems
GO

PRINT 'Default data inserted successfully.'
GO
