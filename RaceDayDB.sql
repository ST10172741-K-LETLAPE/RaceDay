IF DB_ID('RaceDayDB') IS NOT NULL DROP DATABASE RaceDayDB;
GO
CREATE DATABASE RaceDayDB;
GO
USE RaceDayDB;
GO

CREATE TABLE [User] (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    Surname VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL,
    Phone VARCHAR(20) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CK_User_Role CHECK (Role IN ('Organiser', 'Participant'))
);
GO
CREATE TABLE Event (
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    OrganiserId INT NOT NULL,
    EventName VARCHAR(100) NOT NULL,
    EventDate DATE NOT NULL,
    Location VARCHAR(150) NOT NULL,
    Description VARCHAR(500) NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Active',
    CONSTRAINT FK_Event_User FOREIGN KEY (OrganiserId) REFERENCES [User](UserId),
    CONSTRAINT CK_Event_Status CHECK (Status IN ('Active', 'Cancelled', 'Completed'))
);
GO
CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    EventId INT NOT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    DistanceKm DECIMAL(5,2) NOT NULL,
    EntryFee DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Category_Event FOREIGN KEY (EventId) REFERENCES Event(EventId),
    CONSTRAINT UQ_Category_Event_Name UNIQUE (EventId, CategoryName)
);
GO
CREATE TABLE Enrolment (
    EnrolmentId INT IDENTITY(1,1) PRIMARY KEY,
    ParticipantId INT NOT NULL,
    CategoryId INT NOT NULL,
    EnrolmentDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    Status VARCHAR(20) NOT NULL DEFAULT 'Active',
    CONSTRAINT FK_Enrolment_Participant FOREIGN KEY (ParticipantId) REFERENCES [User](UserId),
    CONSTRAINT FK_Enrolment_Category FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId),
    CONSTRAINT UQ_Enrolment_Participant_Category UNIQUE (ParticipantId, CategoryId)
);
GO
CREATE TABLE Result (
    ResultId INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentId INT NOT NULL UNIQUE,
    FinishTime TIME NULL,
    Position INT NULL,
    ResultStatus VARCHAR(20) NULL,
    CONSTRAINT FK_Result_Enrolment FOREIGN KEY (EnrolmentId) REFERENCES Enrolment(EnrolmentId)
);
GO
CREATE TABLE EventRoute (
    RouteId INT IDENTITY(1,1) PRIMARY KEY,
    EventId INT NOT NULL UNIQUE,
    RouteName VARCHAR(100) NOT NULL,
    DistanceKm DECIMAL(5,2) NOT NULL,
    RouteDescription VARCHAR(500) NULL,
    CONSTRAINT FK_EventRoute_Event FOREIGN KEY (EventId) REFERENCES Event(EventId)
);
GO
CREATE TABLE Weather (
    WeatherId INT IDENTITY(1,1) PRIMARY KEY,
    EventId INT NOT NULL,
    Temperature DECIMAL(5,2) NULL,
    WeatherCondition VARCHAR(50) NULL,
    WindSpeed DECIMAL(5,2) NULL,
    RecordedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Weather_Event FOREIGN KEY (EventId) REFERENCES Event(EventId)
);
GO

INSERT INTO [User] (FirstName, Surname, Email, PasswordHash, Role, Phone) VALUES
('Thabo', 'Mokoena', 'thabo.mokoena@raceday.co.za', 'HASHED_PASSWORD_001', 'Organiser', '0821112233'),
('Lerato', 'Dlamini', 'lerato.dlamini@raceday.co.za', 'HASHED_PASSWORD_002', 'Organiser', '0832223344'),
('Sipho', 'Nkosi', 'sipho.nkosi@example.com', 'HASHED_PASSWORD_003', 'Participant', '0843334455'),
('Nomsa', 'Mthembu', 'nomsa.mthembu@example.com', 'HASHED_PASSWORD_004', 'Participant', '0854445566');
GO
INSERT INTO Event (OrganiserId, EventName, EventDate, Location, Description, Status) VALUES
(1, 'Johannesburg City Run', '2026-10-18', 'Johannesburg, Gauteng', 'Road running event.', 'Active'),
(2, 'Cape Town Community Cycle', '2026-11-08', 'Cape Town, Western Cape', 'Community cycling event.', 'Active'),
(1, 'Soweto Heritage Marathon', '2026-11-22', 'Soweto, Gauteng', 'Heritage marathon.', 'Active');
GO
INSERT INTO Category (EventId, CategoryName, DistanceKm, EntryFee) VALUES
(1, '5 KM Fun Run', 5.00, 80.00),
(1, '10 KM Road Race', 10.00, 150.00),
(1, '21 KM Half Marathon', 21.10, 250.00),
(2, '20 KM Cycle', 20.00, 180.00),
(2, '50 KM Cycle', 50.00, 300.00),
(3, '10 KM Run', 10.00, 150.00),
(3, '21 KM Half Marathon', 21.10, 250.00),
(3, '42 KM Marathon', 42.20, 400.00);
GO