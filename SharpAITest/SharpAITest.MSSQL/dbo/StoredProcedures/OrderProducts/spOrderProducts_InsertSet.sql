CREATE PROCEDURE [dbo].[spOrderProducts_InsertSet]
	@OrderProducts dbo.CreateOrderProductUDT READONLY
AS
BEGIN
    DECLARE @InsertedRows TABLE
    (
        Id INT,
		OrderId INT,
		ProductId INT,
		Quantity INT
	);

	INSERT INTO dbo.OrderProducts (OrderId, ProductId, Quantity)
	OUTPUT [inserted].[Id], [inserted].[OrderId], [inserted].[ProductId], [inserted].[Quantity]
	INTO @InsertedRows
	SELECT OrderId, ProductId, Quantity
	FROM @OrderProducts;

	SELECT
		[ir].[Id], [ir].[OrderId], [ir].[ProductId], [ir].[Quantity], 
		[p].[Id], [p].[Name], [p].[Price]
	FROM @InsertedRows ir
	INNER JOIN dbo.Products p ON ir.ProductId = p.Id
END