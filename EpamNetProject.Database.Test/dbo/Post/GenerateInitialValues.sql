-- INITIAL VALUES DATABASE
--- VENUES
INSERT INTO dbo.Venues (Name,Description,Address,Phone)
VALUES
('First Venue', 'Description', 'Address', '8-800-555-35-35'),
('Second Venue', 'Description', 'Address', '8-800-555-35-35'),
('Third Venue', 'Description', 'Address', '8-800-555-35-35'),
('Fourth Venue', 'Description', 'Address', '8-800-555-35-35'),
('Fifth Venue', 'Description', 'Address', '8-800-555-35-35')

--- LAYOUTS
INSERT INTO dbo.Layouts (Description, LayoutName, VenueId)
VALUES
('Description', '1 layout name', 1),
('Description', '2 layout name', 2)

--- Area
INSERT INTO dbo.Areas (Description, CoordX, CoordY, LayoutId, Price)
VALUES
('Description', 10,20,1, 10),
('Description', 10,20,2, 20),
('Description', 150,20,2, 30),
('Description', 150,20,1, 40)

--- SEATS
INSERT INTO dbo.Seats(Number,AreaId,Row)
VALUES
(1,1,1),
(2,1,1),
(3,1,1),
(4,1,1),
(5,1,1),
(6,1,1),
(7,1,1),
(8,1,1),
(9,1,1),
(10,1,1),
(1,2,1),
(2,2,1),
(3,2,1),
(4,2,1),
(5,2,1),
(6,2,1),
(7,2,1),
(8,2,1),
(9,2,1),
(10,2,1),
(1,1,2),
(2,1,2),
(3,1,2),
(4,1,2),
(5,1,2),
(6,1,2),
(7,1,2),
(8,1,2),
(9,1,2),
(10,1,2),
(1,2,2),
(2,2,2),
(3,2,2),
(4,2,2),
(5,2,2),
(6,2,2),
(7,2,2),
(8,2,2),
(9,2,2),
(10,2,2),
(1,3,1),
(2,3,1),
(3,3,1),
(4,3,1),
(5,3,1),
(6,3,1),
(7,3,1),
(8,3,1),
(9,3,1),
(10,3,1),
(1,4,1),
(2,4,1),
(3,4,1),
(4,4,1),
(5,4,1),
(6,4,1),
(7,4,1),
(8,4,1),
(9,4,1),
(10,4,1),
(1,3,2),
(2,3,2),
(3,3,2),
(4,3,2),
(5,3,2),
(6,3,2),
(7,3,2),
(8,3,2),
(9,3,2),
(10,3,2),
(1,4,2),
(2,4,2),
(3,4,2),
(4,4,2),
(5,4,2),
(6,4,2),
(7,4,2),
(8,4,2),
(9,4,2),
(10,4,2)

--- EVENTS
INSERT INTO dbo.Events (Name,Description,EventDate,LayoutId, ImgUrl)
VALUES 
('First Event', 'Description', DATEADD(day, (DATEDIFF(day, 0, GETDATE()) + 1), 0), 1, ''),
('Second Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 2),0), 2, ''),
('Third Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 3),0), 1, ''),
('Fourth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 4),0), 1, ''),
('Fifth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 5),0), 2, ''),
('Eighth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 8),0), 2, ''),
('Ninth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 9),0), 1, '')


--- EventArea
INSERT INTO dbo.EventAreas(Description, CoordX, CoordY, EventId, Price)
VALUES
('Description', 10,20,1, 10),
('Description', 10,20,2, 20),
('Description', 150,20,2, 30),
('Description', 150,20,1, 40)


--users
INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[Email]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEndDateUtc]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[UserName])
     VALUES
           ('3c13f18e-754e-4b28-8e8c-0363357d5241','1015036@mail.ru', 0, 'AMQurvcuXuvEanMox0DJ6LHX64udnKpuegVVgcuDv2/vW/9q+2Sl4gTcBf9zWSE5wA==', null,null,0,0,null,0,0,'desout1'),
		   ('25724293-ca00-4b8a-ae86-74d55f291be6','3809766@mail.ru', 0, 'AB8MRHpvKyoXXovRGUCzcCO3iPxaEtTMC6FYrZYDw2jQTHRoztGUXuhS9Zct6KVgdA==', null,null,0,0,null,0,0,'desout')
--roles
INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[Discriminator])
     VALUES
           ('16e28c50-d069-4b16-8c28-a0c6e8cdf003','Admin','UserRole'),
		   ('cbb7d71e-29f7-4a9f-95b1-453572cc2185','Manager','UserRole'),
		   ('f57201c0-774e-4ec7-a0aa-0f8158cfffc4','User','UserRole')

--claims
INSERT INTO [dbo].[AspNetUserClaims]
           ([UserId]
           ,[ClaimType]
           ,[ClaimValue])
     VALUES
           ('25724293-ca00-4b8a-ae86-74d55f291be6','http://schemas.microsoft.com/ws/2008/06/identity/claims/role','Admin'),
		   ('3c13f18e-754e-4b28-8e8c-0363357d5241','http://schemas.microsoft.com/ws/2008/06/identity/claims/role','User')
--profiles
INSERT INTO [dbo].[UserProfiles]
           ([TimeZone]
           ,[Language]
           ,[FirstName]
           ,[Surname]
           ,[UserId]
           ,[Balance]
           ,[BasketTime])
     VALUES
           ('UTC-11','en','Andrei','Anelkin','25724293-ca00-4b8a-ae86-74d55f291be6',100000,null),
		    ('UTC-11','en','Andrei1','Anelkin1','3c13f18e-754e-4b28-8e8c-0363357d5241',0,null)

--- EventSEATS
INSERT INTO dbo.EventSeats(Number,EventAreaId,Row,State,UserId)
VALUES
(1,1,1,2,'25724293-ca00-4b8a-ae86-74d55f291be6'),
(2,1,1,0,null),
(3,1,1,0,null),
(4,1,1,0,null),
(5,1,1,0,null),
(6,1,1,0,null),
(7,1,1,0,null),
(8,1,1,0,null),
(9,1,1,0,null),
(10,1,1,0,null),
(1,2,1,0,null),
(2,2,1,0,null),
(3,2,1,0,null),
(4,2,1,0,null),
(5,2,1,0,null),
(6,2,1,0,null),
(7,2,1,0,null),
(8,2,1,0,null),
(9,2,1,0,null),
(10,2,1,0,null),
(1,1,2,0,null),
(2,1,2,0,null),
(3,1,2,0,null),
(4,1,2,0,null),
(5,1,2,0,null),
(6,1,2,0,null),
(7,1,2,0,null),
(8,1,2,0,null),
(9,1,2,0,null),
(10,1,2,0,null),
(1,2,2,0,null),
(2,2,2,0,null),
(3,2,2,0,null),
(4,2,2,0,null),
(5,2,2,0,null),
(6,2,2,0,null),
(7,2,2,0,null),
(8,2,2,0,null),
(9,2,2,0,null),
(10,2,2,0,null),
(1,3,1,0,null),
(2,3,1,0,null),
(3,3,1,0,null),
(4,3,1,0,null),
(5,3,1,0,null),
(6,3,1,0,null),
(7,3,1,0,null),
(8,3,1,0,null),
(9,3,1,0,null),
(10,3,1,0,null),
(1,4,1,0,null),
(2,4,1,0,null),
(3,4,1,0,null),
(4,4,1,0,null),
(5,4,1,0,null),
(6,4,1,0,null),
(7,4,1,0,null),
(8,4,1,0,null),
(9,4,1,0,null),
(10,4,1,0,null),
(1,3,2,0,null),
(2,3,2,0,null),
(3,3,2,0,null),
(4,3,2,0,null),
(5,3,2,0,null),
(6,3,2,0,null),
(7,3,2,0,null),
(8,3,2,0,null),
(9,3,2,0,null),
(10,3,2,0,null),
(1,4,2,0,null),
(2,4,2,0,null),
(3,4,2,0,null),
(4,4,2,0,null),
(5,4,2,0,null),
(6,4,2,0,null),
(7,4,2,0,null),
(8,4,2,0,null),
(9,4,2,0,null),
(10,4,2,0,null)