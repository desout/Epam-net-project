CREATE PROCEDURE [dbo].[EventDeleteById] @Id int
AS
	DELETE FROM dbo.Events
	WHERE Id = @Id