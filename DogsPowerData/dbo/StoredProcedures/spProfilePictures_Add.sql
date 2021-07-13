CREATE PROCEDURE [dbo].[spProfilePictures_Add]
	@GroomerId nvarchar(128),
	@Picture varbinary(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO dbo.ProfilePictures(GroomerId, Picture)
	VALUES(@GroomerId, @Picture)
END
