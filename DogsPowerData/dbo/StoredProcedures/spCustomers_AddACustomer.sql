CREATE PROCEDURE [dbo].[spCustomers_AddACustomer]
    @Id int output,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@PhoneNumber nvarchar(50),
	@Email nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	IF not exists (SELECT 1 FROM dbo.Customers WHERE PhoneNumber = @PhoneNumber)
	BEGIN
		INSERT INTO dbo.Customers(FirstName, LastName, PhoneNumber, Email)
		VALUES (@FirstName, @LastName, @PhoneNumber, @Email);
	END

	SELECT TOP 1 [Id], [FirstName], [LastName], [PhoneNumber], [Email]
	FROM dbo.Customers
	WHERE PhoneNumber = @PhoneNumber;

	SELECT @Id = SCOPE_IDENTITY();
END
