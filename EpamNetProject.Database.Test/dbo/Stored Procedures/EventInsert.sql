CREATE PROCEDURE [dbo].[EventInsert] @Name varchar(50), @Descr varchar(50), @EventDate DateTime, @LayoutId int
AS
	DECLARE @OutputId int

	INSERT INTO [dbo].[Events]
           ([Name]
           ,[Description]
		   ,[EventDate]
           ,[LayoutId])
	OUTPUT inserted.Id 
    VALUES
          (@Name, @Descr, @EventDate, @LayoutId)
	SELECT @OutputId

	INSERT INTO dbo.EventAreas(EventId,Description, CoordX,CoordY,Price) 
	SELECT @OutputId, Description, CoordX, CoordY, 0
	FROM dbo.Areas
	WHERE LayoutId = @LayoutId

	INSERT INTO dbo.EventSeats(EventAreaId, Row, Number, State)
	SELECT AreaId, Row, Number, 0
	FROM dbo.Seats
	INNER JOIN dbo.Areas ON LayoutId = @LayoutId
	RETURN @OutputId

