CREATE PROCEDURE [dbo].[spProducts_GetAll]
AS
BEGIN
	SELECT [Id], [Name], [Price]
	FROM dbo.Products
END
