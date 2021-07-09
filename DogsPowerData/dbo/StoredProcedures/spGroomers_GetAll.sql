CREATE PROCEDURE [dbo].[spGroomers_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT gr.[Id], [FirstName], [LastName], [Username], [Email], [IsActive], w.Workday
	FROM dbo.Groomers gr
	JOIN dbo.WorkSchedule ws ON gr.Id = ws.GroomerId
	JOIN dbo.Workdays w ON ws.WorkdayId = w.Id
END
