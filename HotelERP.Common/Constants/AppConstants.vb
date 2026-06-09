Namespace Constants
    Public Class AppConstants
        ' Application Settings
        Public Const APP_NAME = "Hotel Mitra ERP"
        Public Const APP_VERSION = "1.0.0"
        Public Const APP_TITLE = "Hotel Mitra - Enterprise Resource Planning System"
        
        ' Database Settings
        Public Const DEFAULT_DB_NAME = "HotelMitra"
        Public Const DEFAULT_CONNECTION_TIMEOUT = 30
        
        ' Default Values
        Public Const DEFAULT_TIMEZONE = "UTC"
        Public Const DEFAULT_CURRENCY = "USD"
        
        ' File Paths
        Public Shared ReadOnly CONFIG_FOLDER = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HotelMitra")
        Public Shared ReadOnly CONFIG_FILE = IO.Path.Combine(CONFIG_FOLDER, "config.xml")
        Public Shared ReadOnly LOG_FOLDER = IO.Path.Combine(CONFIG_FOLDER, "Logs")
        Public Shared ReadOnly BACKUP_FOLDER = IO.Path.Combine(CONFIG_FOLDER, "Backups")
        
        ' Session Settings
        Public Const SESSION_TIMEOUT_MINUTES = 30
        Public Const MAX_LOGIN_ATTEMPTS = 3
        Public Const PASSWORD_MIN_LENGTH = 6
        
        ' Date Formats
        Public Const DATE_FORMAT = "yyyy-MM-dd"
        Public Const DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss"
        Public Const TIME_FORMAT = "HH:mm:ss"
        
        ' Page Sizes
        Public Const DEFAULT_PAGE_SIZE = 20
        Public Const MAX_PAGE_SIZE = 1000
        
        ' Status Constants
        Public Module RoomStatus
            Public Const AVAILABLE = "Available"
            Public Const OCCUPIED = "Occupied"
            Public Const MAINTENANCE = "Maintenance"
            Public Const DIRTY = "Dirty"
            Public Const RESERVED = "Reserved"
        End Module
        
        Public Module ReservationStatus
            Public Const CONFIRMED = "Confirmed"
            Public Const CANCELLED = "Cancelled"
            Public Const COMPLETED = "Completed"
            Public Const NO_SHOW = "NoShow"
        End Module
        
        Public Module BillStatus
            Public Const PENDING = "Pending"
            Public Const PARTIAL_PAID = "PartialPaid"
            Public Const PAID = "Paid"
            Public Const CANCELLED = "Cancelled"
        End Module
        
        Public Module TaskStatus
            Public Const PENDING = "Pending"
            Public Const IN_PROGRESS = "InProgress"
            Public Const COMPLETED = "Completed"
            Public Const ON_HOLD = "OnHold"
        End Module
        
        Public Module PaymentMethod
            Public Const CASH = "Cash"
            Public Const CREDIT_CARD = "CreditCard"
            Public Const DEBIT_CARD = "DebitCard"
            Public Const CHECK = "Check"
            Public Const ONLINE = "Online"
        End Module
        
    End Class
End Namespace