CREATE TABLE [dbo].[EventSeats](
	[Id] [int] IDENTITY(1,1),
	[Row] [int] NOT NULL,
	[EventAreaId] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[State] [int] NOT NULL,
	[UserId] NVARCHAR(128) NOT NULL,
 CONSTRAINT [PK_EventSeat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [FK_EventSeats_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventSeats]  WITH CHECK ADD  CONSTRAINT [FK_EventSeat_EventArea] FOREIGN KEY([EventAreaId])
REFERENCES [dbo].[EventAreas] ([Id])
GO

ALTER TABLE [dbo].[EventSeat] CHECK CONSTRAINT [FK_EventSeat_EventArea]