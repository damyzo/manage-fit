CREATE TABLE [dbo].[MealIngredient]
(
	[MealsId] UNIQUEIDENTIFIER NOT NULL,
	[IngredientsId] UNIQUEIDENTIFIER NOT NULL,
	[Grams] FLOAT, 
	[Liters] FLOAT,
    CONSTRAINT [PK_MealIngredient] PRIMARY KEY ([MealsId], [IngredientsId]),
    CONSTRAINT [FK_MealIngredient_Meals] FOREIGN KEY ([MealsId]) REFERENCES [dbo].[Meal] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MealIngredient_Ingredients] FOREIGN KEY ([IngredientsId]) REFERENCES [dbo].[Ingredient] ([Id]) ON DELETE CASCADE
)
