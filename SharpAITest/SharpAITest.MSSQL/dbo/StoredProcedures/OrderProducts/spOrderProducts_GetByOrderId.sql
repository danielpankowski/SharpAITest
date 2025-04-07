CREATE PROCEDURE [dbo].[spOrderProducts_GetByOrderId]
	@OrderId INT
AS
BEGIN
	SELECT [Id], [OrderId], [ProductId], [Quantity]
	FROM dbo.OrderProducts
	WHERE OrderId = @OrderId;
END
