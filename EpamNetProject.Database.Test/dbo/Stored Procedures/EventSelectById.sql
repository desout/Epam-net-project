CREATE PROCEDURE [dbo].[EventSelectById] @Id int
AS
	SELECT [Id]
      ,[Name]
      ,[Description]
      ,[EventDate]
      ,[LayoutId]
	FROM [dbo].[Events]
	WHERE Id = @Id