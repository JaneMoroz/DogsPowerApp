CREATE PROCEDURE [dbo].[spGroomers_GetAllGroomersAllDetails]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT gr.[Id], [FirstName], [LastName], [Username], [Email], [IsActive], w.Workday, pp.Picture
	FROM dbo.Groomers gr
	LEFT JOIN dbo.WorkSchedule ws on gr.Id = ws.GroomerId
	LEFT JOIN dbo.Workdays w on ws.WorkdayId = w.Id
	LEFT JOIN dbo.ProfilePictures pp on gr.Id = pp.GroomerId
END
