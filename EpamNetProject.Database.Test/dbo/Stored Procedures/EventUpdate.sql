CREATE PROCEDURE [dbo].[EventUpdate]
	@Name varchar(50), @Descr varchar(50), @EventDate DateTime, @LayoutId int, @Id int
AS
	UPDATE [dbo].[Event]
	   SET [Name] = @Name
		  ,[Description] = @Descr
		  ,[EventDate] = @EventDate
		  ,[LayoutId] = @LayoutId
	 WHERE @Id = Id


