CREATE PROCEDURE [dbo].[spOrders_GetAll]
AS
BEGIN
	SELECT [Id], [TotalPrice]
	FROM dbo.Orders
END
