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
INSERT INTO dbo.Areas (Description, CoordX, CoordY, LayoutId)
VALUES
('Description', 10,20,1),
('Description', 20,30,2),
('Description', 30,40,2),
('Description', 40,50,1),
('Description', 50,60,3),
('Description', 60,70,3)

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
INSERT INTO dbo.Events (Name,Description,EventDate,LayoutId)
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