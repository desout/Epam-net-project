CREATE PROCEDURE [dbo].[EventInsert] @Name varchar(50), @Descr varchar(50), @EventDate DateTime, @LayoutId int
AS
	DECLARE @OutputId int

	INSERT INTO [dbo].[Event]
           ([Name]
           ,[Description]
		   ,[EventDate]
           ,[LayoutId])
	OUTPUT inserted.Id 
    VALUES
          (@Name, @Descr, @EventDate, @LayoutId)
	SELECT @OutputId

	INSERT INTO dbo.EventArea(EventId,Description, CoordX,CoordY,Price) 
	SELECT @OutputId, Description, CoordX, CoordY, 0
	FROM dbo.Area
	WHERE LayoutId = @LayoutId

	INSERT INTO dbo.EventSeat(EventAreaId, Row, Number, State)
	SELECT AreaId, Row, Number, 0
	FROM dbo.Seat
	INNER JOIN dbo.Area ON LayoutId = @LayoutId
	RETURN @OutputId

