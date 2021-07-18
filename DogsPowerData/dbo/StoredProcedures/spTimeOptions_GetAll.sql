CREATE PROCEDURE [dbo].[spTimeOptions_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [StartTime]
	FROM dbo.TimeOptions;
END

