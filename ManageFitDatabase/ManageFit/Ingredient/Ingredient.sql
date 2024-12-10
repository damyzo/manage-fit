CREATE TABLE [dbo].[Ingredient]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(300) NOT NULL,
    [Calories] FLOAT,
    [Protein] FLOAT,
    [Fat] FLOAT,
    [Carbohydrate] FLOAT
)
