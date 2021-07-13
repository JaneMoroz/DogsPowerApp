CREATE PROCEDURE [dbo].[spWorkSchedule_GetById]
	@GroomerId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT w.Workday
	FROM dbo.WorkSchedule ws
	JOIN dbo.Workdays w ON ws.WorkdayId = w.Id
	WHERE GroomerId = @GroomerId
END
