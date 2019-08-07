CREATE PROCEDURE [dbo].[EventDeleteById] @Id int
AS
	DELETE FROM dbo.Event
	WHERE Id = @Id