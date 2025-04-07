CREATE PROCEDURE [dbo].[spProducts_Insert]
	@Name NVARCHAR(50),
	@Price MONEY
AS
BEGIN
	INSERT INTO dbo.Products (Name, Price)
	OUTPUT [inserted].[Id], [inserted].[Name], [inserted].[Price]
	VALUES (@Name, @Price);
END
