CREATE PROCEDURE [dbo].[EventSelectAll]
AS
	SELECT [Id]
      ,[Name]
      ,[Description]
      ,[EventDate]
      ,[LayoutId]
	FROM [dbo].[Event]