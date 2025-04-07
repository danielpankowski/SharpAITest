CREATE PROCEDURE [dbo].[spOrderProducts_InsertSet]
	@OrderProducts dbo.OrderProductUDT READONLY
AS
BEGIN
	INSERT INTO dbo.OrderProducts
	OUTPUT [inserted].[Id], [inserted].[OrderId], [inserted].[ProductId], [inserted].[Quantity]
	SELECT OrderId, ProductId, Quantity
	FROM @OrderProducts;
END