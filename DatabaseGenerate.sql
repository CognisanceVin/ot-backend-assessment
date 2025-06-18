IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'OT_Assessment_DB')
BEGIN
    CREATE DATABASE [OT_Assessment_DB];
END
GO

USE [OT_Assessment_DB];
GO