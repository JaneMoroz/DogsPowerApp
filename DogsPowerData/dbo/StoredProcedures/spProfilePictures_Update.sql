CREATE PROCEDURE [dbo].[spProfilePictures_Update]
	@GroomerId nvarchar(128),
	@Picture varbinary(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE dbo.ProfilePictures
	SET Picture = @Picture
	WHERE GroomerId = @GroomerId
END
