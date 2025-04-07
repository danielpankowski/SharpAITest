CREATE PROCEDURE [dbo].[spOrderProducts_GetByOrderId]
	@OrderId INT
AS
BEGIN
	SELECT 
		[op].[Id], [op].[OrderId], [op].[ProductId], [op].[Quantity], 
		[p].[Id], [p].[Name], [p].[Price]
	FROM dbo.OrderProducts op
	INNER JOIN dbo.Products p ON op.ProductId = p.Id
	WHERE OrderId = @OrderId;
END
