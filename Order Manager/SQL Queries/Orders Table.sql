CREATE TABLE Orders(
Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
ClientId int NOT NULL,
FOREIGN KEY (ClientId) REFERENCES Clients(Id),
ItemId int NOT NULL,
FOREIGN KEY (ItemId) REFERENCES Products(Id),
Amount int NOT NULL,
Price float NOT NULL)
GO