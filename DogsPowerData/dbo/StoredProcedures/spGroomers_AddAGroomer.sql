CREATE PROCEDURE [dbo].[spGroomers_AddAGroomer]
	@Id nvarchar(128),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Username nvarchar(50),
	@Email nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Groomers([Id], FirstName, LastName, Username, Email)
	VALUES (@Id, @FirstName, @LastName, @Username, @Email)
END
