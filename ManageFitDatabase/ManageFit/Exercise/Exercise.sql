CREATE TABLE [dbo].[Exercise]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(300),
    [Description] NVARCHAR(500),
    [VideoUrl] NVARCHAR(MAX),
)
