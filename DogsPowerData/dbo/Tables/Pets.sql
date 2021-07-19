CREATE TABLE [dbo].[Pets]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [WeightId] INT NULL, 
    CONSTRAINT [FK_Pets_ToCustomers] FOREIGN KEY (CustomerId) REFERENCES Customers(Id), 
    CONSTRAINT [FK_Pets_ToWeight] FOREIGN KEY (WeightId) REFERENCES Weights(Id)
)
