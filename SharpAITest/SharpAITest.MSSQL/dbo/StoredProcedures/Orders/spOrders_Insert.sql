CREATE PROCEDURE [dbo].[spOrders_Insert]
	@TotalPrice MONEY
AS
BEGIN
	INSERT INTO dbo.Orders (TotalPrice)
	OUTPUT [inserted].[Id], [inserted].[TotalPrice]
	VALUES (@TotalPrice);
END
