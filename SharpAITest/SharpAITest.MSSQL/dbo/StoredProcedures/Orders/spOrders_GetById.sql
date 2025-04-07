CREATE PROCEDURE [dbo].[spOrders_GetById]
	@Id INT
AS
BEGIN
	SELECT [Id], [TotalPrice] 
	FROM dbo.Orders 
	WHERE Id = @Id;
END
