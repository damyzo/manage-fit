﻿CREATE TABLE [dbo].[Meal]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(300),
    [Description] NVARCHAR(500),
	[TrainerId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_Meal_TrainerId] FOREIGN KEY ([TrainerId]) REFERENCES [dbo].[Trainer] ([Id])
)
