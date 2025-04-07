CREATE PROCEDURE [dbo].[spOrderProducts_Delete]
	@Id INT
AS
BEGIN
	DELETE FROM dbo.OrderProducts
	WHERE Id = @Id;
END