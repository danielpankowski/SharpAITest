CREATE TABLE [dbo].[OrderProducts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OrderId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT 0 
)
