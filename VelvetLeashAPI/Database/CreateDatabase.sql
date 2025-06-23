-- Velvet Leash Pet Care Database Creation Script
-- This script creates the database and all necessary tables for the Velvet Leash API

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'VelvetLeashDB')
BEGIN
    CREATE DATABASE VelvetLeashDB;
END
GO

USE VelvetLeashDB;
GO

-- Create Users table (extends ASP.NET Identity)
-- Note: ASP.NET Identity will create the base AspNetUsers table
-- We'll add our custom columns via Entity Framework migrations

-- Create Pets table
CREATE TABLE Pets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Type INT NOT NULL, -- PetType enum: 1=Dog, 2=Cat, 3=Bird, 4=Fish, 5=Rabbit, 6=Other
    Size INT NOT NULL, -- PetSize enum: 1=Small(0-15lbs), 2=Medium(16-40lbs), 3=Large(41-100lbs), 4=ExtraLarge(101+lbs)
    Age INT NOT NULL, -- PetAge enum: 1=Puppy(<1year), 2=Adult(1+years)
    GetAlongWithDogs BIT NOT NULL DEFAULT 0,
    GetAlongWithCats BIT NOT NULL DEFAULT 0,
    IsUnsureWithDogs BIT NOT NULL DEFAULT 0,
    IsUnsureWithCats BIT NOT NULL DEFAULT 0,
    SpecialInstructions NVARCHAR(1000) NULL,
    MedicalConditions NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UserId NVARCHAR(450) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

-- Create PetSitters table
CREATE TABLE PetSitters (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL UNIQUE,
    About NVARCHAR(1000) NULL,
    Skills NVARCHAR(500) NULL,
    HomeDetails NVARCHAR(500) NULL,
    Address NVARCHAR(200) NULL,
    City NVARCHAR(100) NULL,
    State NVARCHAR(50) NULL,
    ZipCode NVARCHAR(10) NULL,
    Latitude FLOAT NULL,
    Longitude FLOAT NULL,
    IsStarSitter BIT NOT NULL DEFAULT 0,
    IsAvailable BIT NOT NULL DEFAULT 1,
    HourlyRate DECIMAL(18,2) NULL,
    DailyRate DECIMAL(18,2) NULL,
    OvernightRate DECIMAL(18,2) NULL,
    AverageRating FLOAT NOT NULL DEFAULT 0.0,
    TotalReviews INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

-- Create PetSitterServices table
CREATE TABLE PetSitterServices (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PetSitterId INT NOT NULL,
    ServiceType INT NOT NULL, -- ServiceType enum: 1=Boarding, 2=DayCare, 3=Walking, 4=Sitting, 5=Grooming
    Price DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (PetSitterId) REFERENCES PetSitters(Id) ON DELETE CASCADE
);

-- Create Bookings table
CREATE TABLE Bookings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    PetSitterId INT NOT NULL,
    PetId INT NOT NULL,
    ServiceType INT NOT NULL, -- ServiceType enum
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    SpecialInstructions NVARCHAR(1000) NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Status INT NOT NULL DEFAULT 1, -- BookingStatus enum: 1=Pending, 2=Confirmed, 3=InProgress, 4=Completed, 5=Cancelled, 6=Declined
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (PetSitterId) REFERENCES PetSitters(Id),
    FOREIGN KEY (PetId) REFERENCES Pets(Id)
);

-- Create Reviews table
CREATE TABLE Reviews (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReviewerId NVARCHAR(450) NOT NULL,
    RevieweeId NVARCHAR(450) NOT NULL,
    BookingId INT NOT NULL,
    Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
    Comment NVARCHAR(1000) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (ReviewerId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (RevieweeId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (BookingId) REFERENCES Bookings(Id)
);

-- Create BookingMessages table
CREATE TABLE BookingMessages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT NOT NULL,
    SenderId NVARCHAR(450) NOT NULL,
    Message NVARCHAR(1000) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (BookingId) REFERENCES Bookings(Id) ON DELETE CASCADE,
    FOREIGN KEY (SenderId) REFERENCES AspNetUsers(Id)
);

-- Create UserNotificationSettings table
CREATE TABLE UserNotificationSettings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL UNIQUE,
    EmailNotifications BIT NOT NULL DEFAULT 1,
    MarketingEmails BIT NOT NULL DEFAULT 0,
    SmsNotifications BIT NOT NULL DEFAULT 1,
    MessageNotifications BIT NOT NULL DEFAULT 1,
    NewInquiries BIT NOT NULL DEFAULT 1,
    NewMessages BIT NOT NULL DEFAULT 1,
    BookingRequests BIT NOT NULL DEFAULT 1,
    BookingDeclined BIT NOT NULL DEFAULT 1,
    MmsSupport BIT NOT NULL DEFAULT 0,
    QuietHours BIT NOT NULL DEFAULT 0,
    MarketingSms BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

-- Create indexes for better performance
CREATE INDEX IX_Pets_UserId ON Pets(UserId);
CREATE INDEX IX_PetSitters_UserId ON PetSitters(UserId);
CREATE INDEX IX_PetSitters_Location ON PetSitters(Latitude, Longitude);
CREATE INDEX IX_PetSitters_Available ON PetSitters(IsAvailable);
CREATE INDEX IX_PetSitterServices_PetSitterId ON PetSitterServices(PetSitterId);
CREATE INDEX IX_Bookings_UserId ON Bookings(UserId);
CREATE INDEX IX_Bookings_PetSitterId ON Bookings(PetSitterId);
CREATE INDEX IX_Bookings_PetId ON Bookings(PetId);
CREATE INDEX IX_Bookings_Status ON Bookings(Status);
CREATE INDEX IX_Reviews_RevieweeId ON Reviews(RevieweeId);
CREATE INDEX IX_BookingMessages_BookingId ON Bookings(Id);
CREATE INDEX IX_UserNotificationSettings_UserId ON UserNotificationSettings(UserId);

-- Insert sample data for testing
-- Note: You'll need to run the application first to create ASP.NET Identity tables
-- Then you can insert sample users and related data

PRINT 'Database VelvetLeashDB created successfully!';
PRINT 'Tables created:';
PRINT '- Pets';
PRINT '- PetSitters';
PRINT '- PetSitterServices';
PRINT '- Bookings';
PRINT '- Reviews';
PRINT '- BookingMessages';
PRINT '- UserNotificationSettings';
PRINT '';
PRINT 'Note: ASP.NET Identity tables (AspNetUsers, AspNetRoles, etc.) will be created automatically when you run the application.';
PRINT 'Connection String: Server=(localdb)\\mssqllocaldb;Database=VelvetLeashDB;Trusted_Connection=true;MultipleActiveResultSets=true';