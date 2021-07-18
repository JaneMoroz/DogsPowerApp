CREATE PROCEDURE [dbo].[spWorkSchedule_FindAvailableGroomersByWorkday]
	@Workday nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ws.[GroomerId]
	FROM dbo.WorkSchedule ws
	JOIN dbo.Groomers g ON ws.GroomerId = g.Id
	WHERE ws.WorkdayId = (SELECT [Id] FROM dbo.Workdays WHERE Workday = @Workday) AND g.IsActive = 1
END
