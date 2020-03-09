CREATE PROCEDURE [dbo].[EventSelectById] @Id int
AS
	SELECT [Id]
      ,[Name]
      ,[Description]
      ,[EventDate]
      ,[LayoutId]
	  ,[ImgUrl]
	FROM [dbo].[Events]
	WHERE Id = @Id