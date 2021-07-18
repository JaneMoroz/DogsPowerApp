CREATE TABLE [dbo].[Services]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ServiceName] NVARCHAR(50) NOT NULL, 
    [WeightId] INT NULL, 
    [Duration] TIME NOT NULL, 
    [Price] MONEY NOT NULL, 
    CONSTRAINT [FK_Services_ToWeights] FOREIGN KEY (WeightId) REFERENCES Weights(Id)
)
