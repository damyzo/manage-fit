CREATE TABLE [dbo].[Ingredient]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(300) NOT NULL,
    [Calories] FLOAT,
    [Protein] FLOAT,
    [Fat] FLOAT,
    [Carbohydrate] FLOAT,
	[TrainerId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_Ingredient_TrainerId] FOREIGN KEY ([TrainerId]) REFERENCES [dbo].[Trainer] ([Id]) ON DELETE CASCADE
)
