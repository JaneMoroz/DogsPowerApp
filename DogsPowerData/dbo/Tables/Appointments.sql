CREATE TABLE [dbo].[Appointments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [GroomerId] NVARCHAR(128) NOT NULL, 
    [PetId] INT NOT NULL, 
    [ServiceId] INT NOT NULL, 
    [Date] DATETIME2 NOT NULL, 
    [TimeId] INT NOT NULL, 
    CONSTRAINT [FK_Appointments_ToCustomers] FOREIGN KEY (CustomerId) REFERENCES Customers(Id), 
    CONSTRAINT [FK_Appointments_ToGroomers] FOREIGN KEY (GroomerId) REFERENCES Groomers(Id), 
    CONSTRAINT [FK_Appointments_ToPets] FOREIGN KEY (PetId) REFERENCES Pets(Id), 
    CONSTRAINT [FK_Appointments_ToTimeOptions] FOREIGN KEY (TimeId) REFERENCES TimeOptions(Id)
)
