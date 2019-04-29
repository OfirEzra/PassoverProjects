CREATE TABLE Products(
Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
ProductName VARCHAR(50) NOT NULL UNIQUE,
SupplierId int NOT NULL,
Price float NOT NULL,
Amount int NOT NULL)
GO