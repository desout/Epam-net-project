CREATE TABLE [dbo].[EventAreas](
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
ALTER TABLE [dbo].[EventAreas]  WITH CHECK ADD  CONSTRAINT [FK_EventArea_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([Id])
GO

ALTER TABLE [dbo].[EventAreas] CHECK CONSTRAINT [FK_EventArea_Event]