CREATE PROCEDURE [dbo].[spProducts_Delete]
	@Id INT
AS
BEGIN
	DELETE FROM dbo.Products
	WHERE Id = @Id;
END