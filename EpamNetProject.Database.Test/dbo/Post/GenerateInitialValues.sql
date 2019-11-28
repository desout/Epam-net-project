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
('Description', '2 layout name', 2),
('Description', '3 layout name', 3)

--- Area
INSERT INTO dbo.Areas (Description, CoordX, CoordY, LayoutId, Price)
VALUES
('Description', 10,20,1, 10),
('Description', 20,30,2, 20),
('Description', 30,40,2, 30),
('Description', 40,50,1, 40),
('Description', 50,60,3, 50),
('Description', 60,70,3, 60)

--- SEATS
INSERT INTO dbo.Seats(Number,AreaId,Row)
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
INSERT INTO dbo.Events (Name,Description,EventDate,LayoutId, ImgUrl)
VALUES 
('First Event', 'Description', DATEADD(day, (DATEDIFF(day, 0, GETDATE()) + 1), 0), 1, ''),
('Second Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 2),0), 2, ''),
('Third Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 3),0), 3, ''),
('Fourth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 4),0), 1, ''),
('Fifth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 5),0), 2, ''),
('Sixth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 6),0), 3, ''),
('Seventh Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 7),0), 3, ''),
('Eighth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 8),0), 2, ''),
('Ninth Event', 'Description', DATEADD(day,(DATEDIFF(day, 0, GETDATE()) + 9),0), 1, '')


--- EventArea
INSERT INTO dbo.EventAreas(Description, CoordX, CoordY, EventId, Price)
VALUES
('Description', 10,20,1, 10),
('Description', 240,50,1, 40),
('Description', 10,20,4, 10),
('Description', 40,50,4, 40),
('Description', 10,20,9, 10),
('Description', 40,50,9, 40),
('Description', 20,30,2, 20),
('Description', 30,40,2, 30),
('Description', 20,30,5, 20),
('Description', 30,40,5, 30),
('Description', 20,30,8, 20),
('Description', 30,40,8, 30),
('Description', 50,60,3, 50),
('Description', 60,70,3, 60),
('Description', 50,60,6, 50),
('Description', 60,70,6, 60),
('Description', 50,60,7, 50),
('Description', 60,70,7, 60)

--- EventSEATS
INSERT INTO dbo.EventSeats(Number,EventAreaId,Row,State,UserId)
VALUES
(10,1,1,0,null),
(20,2,2,0,null),
(30,3,3,0,null),
(40,4,4,0,null),
(50,4,5,0,null),
(60,3,6,0,null),
(70,2,7,0,null),
(80,1,8,0,null),
(90,1,9,0,null),
(110,1,10,0,null),
(11,1,1,0,null),
(21,2,2,0,null),
(31,3,3,0,null),
(41,4,4,0,null),
(51,4,5,0,null),
(61,3,6,0,null),
(71,2,7,0,null),
(81,1,8,0,null),
(91,1,9,0,null),
(111,1,10,0,null),
(12,1,1,0,null),
(22,2,2,0,null),
(32,3,3,0,null),
(42,4,4,0,null),
(52,4,5,0,null),
(62,3,6,0,null),
(72,2,7,0,null),
(82,1,8,0,null),
(92,1,9,0,null),
(112,1,10,0,null)
