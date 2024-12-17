CREATE TABLE [dbo].[NutritionPlanMeal]
(
	[NutritionPlanId] UNIQUEIDENTIFIER NOT NULL,
    [MealId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_NutritionPlanMeal] PRIMARY KEY ([NutritionPlanId], [MealId]),
    CONSTRAINT [FK_NutritionPlanMeal_NutritionPlanId] FOREIGN KEY ([NutritionPlanId]) REFERENCES [dbo].[NutritionPlan] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NutritionPlanMeal_MealId] FOREIGN KEY ([MealId]) REFERENCES [dbo].[Meal] ([Id]) ON DELETE CASCADE
)
