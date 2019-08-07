CREATE PROCEDURE [dbo].[EventInsert] @Name varchar(50), @Descr varchar(50), @EventDate DateTime, @LayoutId int
AS
	INSERT INTO [dbo].[Event]
           ([Name]
           ,[Description]
		   ,[EventDate]
           ,[LayoutId])
	OUTPUT inserted.Id
     VALUES
           (@Name, @Descr, @EventDate, @LayoutId)