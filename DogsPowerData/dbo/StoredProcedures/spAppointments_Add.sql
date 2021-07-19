CREATE PROCEDURE [dbo].[spAppointments_Add]
	@CustomerId int,
	@GroomerId nvarchar(128),
	@PetId int,
	@ServiceName nvarchar(50),
	@WeightName nvarchar(50),
	@Date datetime2(7),
	@StartTime time(7)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Appointments(CustomerId, GroomerId, PetId, ServiceId, [Date], TimeId)
	VALUES (@CustomerId, @GroomerId, @PetId, 
			(SELECT Id FROM dbo.[Services] WHERE ServiceName = @ServiceName AND WeightId = (SELECT [Id] FROM dbo.Weights WHERE WeightName = @WeightName)), 
			@Date, 
			(SELECT Id FROM dbo.TimeOptions WHERE StartTime = @StartTime));
END
