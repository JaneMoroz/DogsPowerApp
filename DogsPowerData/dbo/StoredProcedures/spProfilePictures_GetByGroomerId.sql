CREATE PROCEDURE [dbo].[spProfilePictures_GetByGroomerId]
	@GroomerId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id]
	FROM dbo.ProfilePictures
	WHERE GroomerId = @GroomerId
END
