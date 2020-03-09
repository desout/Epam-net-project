CREATE PROCEDURE [dbo].[EventUpdate]
	@Name varchar(50), @Descr varchar(50), @EventDate DateTime, @LayoutId int, @Id int, @ImgUrl varchar(MAX)
AS
	UPDATE [dbo].[Events]
	   SET [Name] = @Name
		  ,[Description] = @Descr
		  ,[EventDate] = @EventDate
		  ,[LayoutId] = @LayoutId
		  ,[ImgUrl] = @ImgUrl
	 WHERE @Id = Id
	 RETURN @Id

