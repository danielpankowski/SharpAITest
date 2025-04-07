CREATE PROCEDURE [dbo].[spOrderProducts_DeleteByOrderId]
	@OrderId INT
AS
BEGIN
	DELETE FROM dbo.OrderProducts
	WHERE OrderId = @OrderId;
END