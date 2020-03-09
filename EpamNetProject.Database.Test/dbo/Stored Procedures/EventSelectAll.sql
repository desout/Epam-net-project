CREATE PROCEDURE [dbo].[EventSelectAll]
AS
	SELECT [Id]
      ,[Name]
      ,[Description]
      ,[EventDate]
      ,[LayoutId]
	  ,[ImgUrl]
	FROM [dbo].[Events]