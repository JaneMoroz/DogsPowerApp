CREATE PROCEDURE [dbo].[spGroomers_UpdateStatus]
	@Id nvarchar(128),
	@IsActive bit
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE dbo.Groomers
	SET IsActive = @IsActive
	WHERE Id = @Id
END
