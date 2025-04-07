CREATE PROCEDURE [dbo].[spOrderProducts_InsertSet]
	@OrderProducts dbo.CreateOrderProductUDT READONLY
AS
BEGIN
	INSERT INTO dbo.OrderProducts (OrderId, ProductId, Quantity)
	OUTPUT [inserted].[Id], [inserted].[OrderId], [inserted].[ProductId], [inserted].[Quantity]
	SELECT OrderId, ProductId, Quantity
	FROM @OrderProducts;
END