CREATE TABLE [dbo].[Events](
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
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Event_Layout] FOREIGN KEY([LayoutId])
REFERENCES [dbo].[Layouts] ([Id])
GO

ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Layout]