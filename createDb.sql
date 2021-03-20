IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name='Games')
    CREATE DATABASE [Games]
GO
USE [Games]