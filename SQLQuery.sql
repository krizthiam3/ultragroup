USE [master]
GO

/****** Object:  Database [ultragroup]    Script Date: 9/07/2024 2:15:06 p. m. ******/
CREATE DATABASE [ultragroup]
GO


USE [ultragroup]
GO


-- CUSTOMERS

CREATE TABLE Customers (
		Id uniqueidentifier ,
		[DocumentType] varchar(2),
		[Document] varchar(20),
		[Name] varchar(100),
		[LastName] varchar(100),
		[Gender] varchar(1),		
	    [Email] varchar(100),
	    [PhoneNumber] varchar(10),
		[Address_Country] varchar(3),
	    [Address_City] varchar(100),
	    [Address_State] varchar(100),
	    [Address_ZipCode] varchar(100),
	    [Active] Bit
);


-- HOTEL


CREATE TABLE Hotels (
		[Id] uniqueidentifier ,
	    [Code] varchar(20),
	    [Name] varchar(100),
		[Description] varchar(200),
		[PhoneNumber] varchar(10),
		[Address_Country] varchar(3),
		[Address_City] varchar(100),
		[Address_State] varchar(100),
		[Address_ZipCode] varchar(100),
	    [Active] Bit
);





-- HABITACIONES


CREATE TABLE Rooms (
		[Id] uniqueidentifier ,
	    [Code] varchar(20),
	    [Name] varchar(100),
		[TypeId] varchar(20),
		[HotelId] varchar(20),
		[Occupancy] int,
		[UbicationFloor] int,
		[Price] decimal(10, 2),
		[Taxes] decimal(10, 2),
	    [Active] Bit
);


CREATE TABLE RoomTypes (
		[Id] uniqueidentifier ,
	    [Code] varchar(20),
	    [Name] varchar(100),
	    [Active] Bit
);

CREATE TABLE Booking (
		[Id] uniqueidentifier ,
	    [Code] varchar(20),
	    [CheckInDate] date,
	    [CheckOutDate] date,
		[RoomId] varchar(20),
		[CustomerId] varchar(20),
		[EmergencyContactFullName] varchar(100),
		[EmergencyContactPhoneNumber] varchar(20),
	    [Active] Bit
);