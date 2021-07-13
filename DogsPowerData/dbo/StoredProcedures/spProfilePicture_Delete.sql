CREATE PROCEDURE [dbo].[spProfilePicture_Delete]
	@GroomerId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.ProfilePictures
	WHERE GroomerId = @GroomerId
END
