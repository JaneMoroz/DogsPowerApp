CREATE TABLE [dbo].[ProfilePictures]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GroomerId] NVARCHAR(128) NOT NULL, 
    [Picture] VARBINARY(MAX) NOT NULL, 
    CONSTRAINT [FK_ProfilePictures_ToGroomers] FOREIGN KEY (GroomerId) REFERENCES Groomers(Id)
)
