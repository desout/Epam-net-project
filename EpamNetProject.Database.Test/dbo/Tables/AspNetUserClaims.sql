GO

CREATE TABLE [dbo].[AspNetUserClaims](

    [Id] [int] IDENTITY(1,1) NOT NULL,

    [UserId] [nvarchar](128) NOT NULL,

    [ClaimType] [nvarchar](max) NULL,

    [ClaimValue] [nvarchar](max) NULL,

CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED

(

    [Id] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



GO