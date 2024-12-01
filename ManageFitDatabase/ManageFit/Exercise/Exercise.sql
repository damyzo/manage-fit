CREATE TABLE [dbo].[Exercise]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(300),
    [Description] NVARCHAR(500),
    [VideoUrl] NVARCHAR(MAX),
    [TrainerId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Exercise_Traiener FOREIGN KEY ([TrainerId]) REFERENCES [dbo].[Trainer]([Id]) ON DELETE CASCADE
)

