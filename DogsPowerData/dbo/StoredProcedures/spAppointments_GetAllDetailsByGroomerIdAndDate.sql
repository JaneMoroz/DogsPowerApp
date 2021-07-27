CREATE PROCEDURE [dbo].[spAppointments_GetAllDetailsByGroomerIdAndDate]
	@GroomerId nvarchar(128),
	@Date datetime2(7)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT a.Id, s.Duration, tm.StartTime, c.FirstName, c.LastName, p.[Name], s.ServiceName, w.WeightName
	FROM dbo.Appointments a
	JOIN dbo.Customers c ON c.Id = a.CustomerId
	JOIN dbo.Pets p ON p.Id = a.PetId
	JOIN dbo.[Services] s ON s.Id = a.ServiceId
	JOIN dbo.TimeOptions tm ON tm.Id = a.TimeId
	JOIN dbo.Weights w ON w.Id = s.WeightId
	WHERE a.GroomerId = @GroomerId AND a.[Date] = @Date
END
