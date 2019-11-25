CREATE TABLE [dbo].[UserProfiles]
(
	[Id] INT IDENTITY(1,1),
    [TimeZone] NVARCHAR(50) NOT NULL, 
    [Language] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [Surname] NVARCHAR(50) NOT NULL, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [Balance] DECIMAL NOT NULL, 
	[ReserveDate] DateTime,
    CONSTRAINT [FK_UserProfile_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
)
