CREATE PROCEDURE [dbo].[spProducts_Update]
	@Id INT,
	@Name NVARCHAR(50),
	@Price MONEY
AS
BEGIN
	UPDATE dbo.Products
	SET Name = @Name,
		Price = @Price
	OUTPUT [inserted].[Id], [inserted].[Name], [inserted].[Price]
	WHERE Id = @Id;
END
