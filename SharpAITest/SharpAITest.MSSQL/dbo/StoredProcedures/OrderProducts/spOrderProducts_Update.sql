CREATE PROCEDURE [dbo].[spOrderProducts_Update]
	@Id INT,
	@Quantity INT
AS
BEGIN
	UPDATE dbo.OrderProducts
	SET Quantity = @Quantity
	OUTPUT [inserted].[Id], [inserted].[OrderId], [inserted].[ProductId], [inserted].[Quantity]
	WHERE Id = @Id;
END
