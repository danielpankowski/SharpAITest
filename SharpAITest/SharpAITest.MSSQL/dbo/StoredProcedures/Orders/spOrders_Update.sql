CREATE PROCEDURE [dbo].[spOrders_Update]
	@Id INT,
	@TotalPrice MONEY
AS
BEGIN
	UPDATE dbo.Orders
	SET TotalPrice = @TotalPrice
	OUTPUT [inserted].[Id], [inserted].[TotalPrice]
	WHERE Id = @Id;
END
