CREATE PROCEDURE [dbo].[spAppointments_GetByGroomerIdAndDate]
	@GroomerId nvarchar(128),
	@Date datetime2(7)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT s.Duration, tm.StartTime
	FROM dbo.Appointments a
	JOIN dbo.[Services] s ON s.Id = a.ServiceId
	JOIN dbo.TimeOptions tm ON tm.Id = a.TimeId
	WHERE a.GroomerId = @GroomerId AND a.[Date] = @Date
END
