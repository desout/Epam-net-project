CREATE PROCEDURE [dbo].[CreateSnapshot]
AS 
	DROP DATABASE IF EXISTS  [epam-net-project-db-Snapshot];
	CREATE DATABASE [epam-net-project-db-Snapshot] ON
 
	( NAME = 'epam-net-project-db', FILENAME = 'C:\backup\EpamNetProject_data_1800.ss' )
 
	--It is a Snapshot of the adventureworks2012 database
 
	AS SNAPSHOT OF [epam-net-project-db];
 
GO