# Hotel Mitra - Hotel ERP System

A comprehensive VB.NET-based Hotel Management System with SQL Server database support, multi-user capabilities, and offline synchronization.

## Features

✅ **Reservations Management** - Complete booking system with cancellations and modifications
✅ **Guest Management** - Check-in, Check-out, and Guest profiles
✅ **Housekeeping** - Room status tracking and maintenance logs
✅ **Billing & Invoicing** - Guest billing, charges, and payment tracking
✅ **Inventory Management** - Stock control for supplies and amenities
✅ **Staff Management** - Employee records and shift management
✅ **Reports & Analytics** - Occupancy, revenue, and performance reports
✅ **Multi-user Support** - Concurrent access with role-based permissions
✅ **Offline Sync** - Works offline with automatic sync when online
✅ **User Authentication** - Secure login and access control

## Technology Stack

- **Language**: VB.NET (.NET Framework 4.7.2+)
- **Database**: SQL Server 2016+
- **UI Framework**: Windows Forms / WPF
- **Sync Engine**: Custom offline/online synchronization

## Project Structure

```
hotel-erp-vbnet/
├── Database/
│   ├── Schema/
│   ├── Scripts/
│   └── StoredProcedures/
├── HotelERP.Application/
│   ├── UI/
│   ├── Forms/
│   ├── Reports/
│   └── Resources/
├── HotelERP.Business/
│   ├── Managers/
│   ├── Services/
│   └── Utilities/
├── HotelERP.Data/
│   ├── DataAccess/
│   ├── Models/
│   └── Context/
└── HotelERP.Common/
    ├── Constants/
    ├── Enums/
    └── Helpers/
```

## Getting Started

1. Create SQL Server database using scripts in `Database/` folder
2. Update connection string in configuration
3. Build the solution in Visual Studio
4. Run the application

## Documentation

See `DOCUMENTATION.md` for detailed setup and usage instructions.
