CREATE PROCEDURE [dbo].[spPets_AddAPet]
	@CustomerId int,
	@PetName nvarchar(50),
	@WeightName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	IF exists (SELECT 1 FROM dbo.Pets WHERE CustomerId = @CustomerId AND [Name] = @PetName)
	BEGIN
		UPDATE dbo.Pets
		SET WeightId = (Select [Id] FROM dbo.Weights WHERE WeightName = @WeightName)
		WHERE CustomerId = @CustomerId AND [Name] = @PetName;
	END

	IF not exists (SELECT 1 FROM dbo.Pets WHERE CustomerId = @CustomerId AND [Name] = @PetName)
	BEGIN
		INSERT INTO dbo.Pets(CustomerId, [Name], WeightId)
		VALUES (@CustomerId, @PetName, (Select [Id] 
										FROM dbo.Weights 
										WHERE WeightName = @WeightName));
	END

	SELECT TOP 1 [Id]
	FROM dbo.Pets
	WHERE CustomerId = @CustomerId AND [Name] = @PetName;
END
