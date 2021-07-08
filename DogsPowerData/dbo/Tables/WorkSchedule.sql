CREATE TABLE [dbo].[WorkSchedule]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GroomerId] NVARCHAR(128) NOT NULL, 
    [WorkdayId] INT NOT NULL, 
    CONSTRAINT [FK_WorkSchedule_ToGroomers] FOREIGN KEY (GroomerId) REFERENCES Groomers(Id), 
    CONSTRAINT [FK_WorkSchedule_ToWorkdays] FOREIGN KEY (WorkdayId) REFERENCES Workdays(Id)
)
