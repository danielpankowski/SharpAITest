CREATE PROCEDURE [dbo].[spOrderProducts_UpdateSet]
	@UpdateOrderProductUDT dbo.UpdateOrderProductUDT READONLY
AS
BEGIN
	UPDATE op
	SET Quantity = uop.Quantity
	OUTPUT [inserted].[Id], [inserted].[OrderId], [inserted].[ProductId], [inserted].[Quantity]
	FROM dbo.OrderProducts op
	INNER JOIN @UpdateOrderProductUDT uop ON op.Id = uop.Id;
END
