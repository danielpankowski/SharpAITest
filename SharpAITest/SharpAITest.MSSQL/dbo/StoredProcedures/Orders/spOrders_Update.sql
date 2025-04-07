CREATE PROCEDURE [dbo].[spOrders_Update]
	@Id INT,
	@FullPrice MONEY
AS
BEGIN
	UPDATE dbo.Orders
	SET TotalPrice = @FullPrice
	OUTPUT [inserted].[Id], [inserted].[TotalPrice]
	WHERE Id = @Id;
END
