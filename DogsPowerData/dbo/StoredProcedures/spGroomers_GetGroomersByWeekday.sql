CREATE PROCEDURE [dbo].[spGroomers_GetGroomersByWeekday]
	@Weekday nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT gr.[Id], gr.[FirstName], gr.[LastName], pp.Picture
	FROM dbo.Groomers gr
	LEFT JOIN dbo.ProfilePictures pp ON gr.Id = pp.GroomerId
	LEFT JOIN dbo.WorkSchedule ws ON ws.GroomerId = gr.Id
	WHERE ws.WorkdayId = (SELECT [Id] FROM dbo.Workdays WHERE Workday = @Weekday) AND gr.IsActive = 1
END
