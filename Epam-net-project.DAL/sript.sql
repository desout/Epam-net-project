DROP DATABASE IF EXISTS [epam-net-project-db]
GO
CREATE DATABASE [epam-net-project-db]
GO
USE [epam-net-project-db]
GO
DROP PROCEDURE IF EXISTS [dbo].[EventSelectById]
GO
DROP PROCEDURE IF EXISTS [dbo].[EventSelectAll]
GO
DROP PROCEDURE IF EXISTS [dbo].[EventInsert]
GO
DROP PROCEDURE IF EXISTS [dbo].[EventDeleteById]
GO
ALTER TABLE [dbo].[Seat] DROP CONSTRAINT IF EXISTS [FK_Seat_Area]
GO
ALTER TABLE [dbo].[Layout] DROP CONSTRAINT IF EXISTS [FK_Layout_Venue]
GO
ALTER TABLE [dbo].[EventSeat] DROP CONSTRAINT IF EXISTS [FK_EventSeat_EventArea]
GO
ALTER TABLE [dbo].[EventArea] DROP CONSTRAINT IF EXISTS [FK_EventArea_Event]
GO
ALTER TABLE [dbo].[Event] DROP CONSTRAINT IF EXISTS [FK_Event_Layout]
GO
ALTER TABLE [dbo].[Area] DROP CONSTRAINT IF EXISTS [FK_Area_Layout]
GO
DROP TABLE IF EXISTS [dbo].[Venue]
GO
DROP TABLE IF EXISTS [dbo].[Seat]
GO
DROP TABLE IF EXISTS [dbo].[Layout]
GO
DROP TABLE IF EXISTS [dbo].[EventSeat]
GO
DROP TABLE IF EXISTS [dbo].[EventArea]
GO
DROP TABLE IF EXISTS [dbo].[Event]
GO
DROP TABLE IF EXISTS [dbo].[Area]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[Id] [int] IDENTITY(1,1),
	[LayoutId] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[CoordX] [int] NOT NULL,
	[CoordY] [int] NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1),
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[EventDate] [DateTime] NOT NULL,
	[LayoutId] [int] NOT NULL,
 CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventArea](
	[Id] [int] IDENTITY(1,1),
	[EventId] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[CoordX] [int] NOT NULL,
	[CoordY] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_EventArea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventSeat](
	[Id] [int] IDENTITY(1,1),
	[Row] [int] NOT NULL,
	[EventAreaId] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_EventSeat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Layout](
	[Id] [int] IDENTITY(1,1),
	[VenueId] [int] NOT NULL,
	[LayoutName] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Layout] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seat](
	[Id] [int] IDENTITY(1,1),
	[AreaId] [int] NOT NULL,
	[Row] [int] NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_Seat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venue](
	[Id] [int] IDENTITY(1,1),
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Venue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Layout] FOREIGN KEY([LayoutId])
REFERENCES [dbo].[Layout] ([Id])
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_Layout]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Layout] FOREIGN KEY([LayoutId])
REFERENCES [dbo].[Layout] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Layout]
GO
ALTER TABLE [dbo].[EventArea]  WITH CHECK ADD  CONSTRAINT [FK_EventArea_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[EventArea] CHECK CONSTRAINT [FK_EventArea_Event]
GO
ALTER TABLE [dbo].[EventSeat]  WITH CHECK ADD  CONSTRAINT [FK_EventSeat_EventArea] FOREIGN KEY([EventAreaId])
REFERENCES [dbo].[EventArea] ([Id])
GO
ALTER TABLE [dbo].[EventSeat] CHECK CONSTRAINT [FK_EventSeat_EventArea]
GO
ALTER TABLE [dbo].[Layout]  WITH CHECK ADD  CONSTRAINT [FK_Layout_Venue] FOREIGN KEY([VenueId])
REFERENCES [dbo].[Venue] ([Id])
GO
ALTER TABLE [dbo].[Layout] CHECK CONSTRAINT [FK_Layout_Venue]
GO
ALTER TABLE [dbo].[Seat]  WITH CHECK ADD  CONSTRAINT [FK_Seat_Area] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([Id])
GO
ALTER TABLE [dbo].[Seat] CHECK CONSTRAINT [FK_Seat_Area]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventDeleteById] @Id int
AS
	DELETE FROM dbo.Event
	WHERE Id = @Id
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventSelectAll]
AS
	SELECT [Id]
      ,[Name]
      ,[Description]
      ,[EventDate]
      ,[LayoutId]
	FROM [dbo].[Event]
GO


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventSelectById] @Id int
AS
	SELECT [Id]
      ,[Name]
      ,[Description]
      ,[EventDate]
      ,[LayoutId]
	FROM [dbo].[Event]
	WHERE Id = @Id
GO

-- INITIAL VALUES DATABASE
--- VENUES
INSERT INTO dbo.Venue (Name,Description,Address,Phone)
VALUES
('First Venue', 'Description', 'Address', '8-800-555-35-35'),
('Second Venue', 'Description', 'Address', '8-800-555-35-35'),
('Third Venue', 'Description', 'Address', '8-800-555-35-35'),
('Fourth Venue', 'Description', 'Address', '8-800-555-35-35'),
('Fifth Venue', 'Description', 'Address', '8-800-555-35-35')

--- LAYOUTS
INSERT INTO dbo.Layout (Description, LayoutName, VenueId)
VALUES
('Description', '1 layout name', 1),
('Description', '2 layout name', 2),
('Description', '3 layout name', 3)

--- Area
INSERT INTO dbo.Area (Description, CoordX, CoordY, LayoutId)
VALUES
('Description', 10,20,1),
('Description', 20,30,2),
('Description', 30,40,2),
('Description', 40,50,1),
('Description', 50,60,3),
('Description', 60,70,3)

--- SEATS
INSERT INTO dbo.Seat(Number,AreaId,Row)
VALUES
(10,1,1),
(20,2,2),
(30,3,3),
(40,4,4),
(50,4,5),
(60,3,6),
(70,2,7),
(80,1,8),
(90,1,9),
(110,1,10)

--- EVENTS
INSERT INTO dbo.Event (Name,Description,EventDate,LayoutId)
VALUES 
('First Event', 'Description', DATEADD(day, (DATEDIFF(day, 0, GETDATE()) + 1), 0), 1),
('Second Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 2),0), 2),
('Third Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 3),0), 3),
('Fourth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 4),0), 1),
('Fifth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 5),0), 2),
('Sixth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 6),0), 3),
('Seventh Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 7),0), 3),
('Eighth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 8),0), 2),
('Ninth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 9),0), 1)