CREATE PROCEDURE [dbo].[spGroomers_GetAllGroomers]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [FirstName], [LastName], [Username], [Email], [IsActive]
	FROM dbo.Groomers;
END
