CREATE PROCEDURE [dbo].[spPets_AddAPet]
	@Id int output,
	@CustomerId int,
	@PetName nvarchar(50),
	@WeightName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	IF not exists (SELECT 1 FROM dbo.Pets WHERE CustomerId = @CustomerId AND [Name] = @PetName)
	BEGIN
		INSERT INTO dbo.Pets(CustomerId, [Name], WeightId)
		VALUES (@CustomerId, @PetName, (Select [Id] 
										FROM dbo.Weights 
										WHERE WeightName = @WeightName));
	END

	SELECT TOP 1 [Id], [CustomerId], [Name], [WeightId]
	FROM dbo.Pets
	WHERE CustomerId = @CustomerId AND [Name] = @PetName;

	SELECT @Id = SCOPE_IDENTITY();
END
