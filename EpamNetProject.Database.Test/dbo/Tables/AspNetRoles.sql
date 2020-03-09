/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 15-Mar-17 10:27:06 PM ******/

GO

CREATE TABLE [dbo].[AspNetRoles](

    [Id] [nvarchar](128) NOT NULL,

    [Name] [nvarchar](256) NOT NULL,

[Discriminator] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED

(

    [Id] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]



GO
