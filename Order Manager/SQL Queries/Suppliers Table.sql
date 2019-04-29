CREATE TABLE Suppliers(
Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
Username varchar(50) NOT NULL UNIQUE,
Password text NOT NULL,
CompanyName text NOT NULL)
GO