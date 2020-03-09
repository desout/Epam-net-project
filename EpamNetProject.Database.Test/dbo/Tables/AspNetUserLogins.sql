GO

CREATE TABLE [dbo].[AspNetUserLogins](

    [LoginProvider] [nvarchar](128) NOT NULL,

    [ProviderKey] [nvarchar](128) NOT NULL,

    [UserId] [nvarchar](128) NOT NULL,

CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED

(

    [LoginProvider] ASC,

    [ProviderKey] ASC,

    [UserId] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]



GO