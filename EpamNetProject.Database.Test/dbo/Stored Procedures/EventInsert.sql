CREATE PROCEDURE [dbo].[EventInsert] @Name varchar(50), @Descr varchar(50), @EventDate DateTime, @LayoutId int, @ImgUrl varchar(50)
AS
	declare @ID table (Id int)
	declare @OutputId int

	INSERT INTO [dbo].[Events]
           ([Name]
           ,[Description]
		   ,[EventDate]
           ,[LayoutId]
		   ,[ImgUrl])
	OUTPUT inserted.Id INTO @ID
    VALUES
          (@Name, @Descr, @EventDate, @LayoutId, @ImgUrl)
	
	SELECT @OutputId = (SELECT TOP 1 Id FROM @ID)

	INSERT INTO dbo.EventAreas(EventId,Description, CoordX,CoordY,Price) 
	SELECT @OutputId, Description, CoordX, CoordY, 0
	FROM dbo.Areas
	WHERE LayoutId = @LayoutId

	INSERT INTO dbo.EventSeats(EventAreaId, Row, Number, State)
	SELECT AreaId, Row, Number, 0
	FROM dbo.Seats
	INNER JOIN dbo.Areas ON LayoutId = @LayoutId
	SELECT @OutputId

