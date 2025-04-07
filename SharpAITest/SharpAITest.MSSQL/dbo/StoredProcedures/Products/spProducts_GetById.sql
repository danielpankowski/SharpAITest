CREATE PROCEDURE [dbo].[spProducts_GetById]
	@Id INT
AS
BEGIN
	SELECT [Id], [Name], [Price] 
	FROM dbo.Products 
	WHERE Id = @Id;
END
