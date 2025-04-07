CREATE PROCEDURE [dbo].[spOrders_Delete]
	@Id INT
AS
BEGIN
	DELETE FROM dbo.Orders
	WHERE Id = @Id;
END
