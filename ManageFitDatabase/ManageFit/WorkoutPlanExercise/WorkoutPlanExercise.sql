CREATE TABLE [dbo].[WorkoutPlanExercise]
(
	[WorkoutPlansId] UNIQUEIDENTIFIER NOT NULL,
    [ExercisesId] UNIQUEIDENTIFIER NOT NULL,
    [Set] INTEGER,
    [Repetition] INTEGER,
    [Time] FLOAT,
    CONSTRAINT [PK_WorkoutPlanExerciese] PRIMARY KEY ([WorkoutPlansId], [ExercisesId]),
    CONSTRAINT [FK_WorkoutPlanExerciese_WorkourPlans] FOREIGN KEY ([WorkoutPlansId]) REFERENCES [dbo].[WorkoutPlan] ([Id]),
    CONSTRAINT [FK_WorkoutPlanExerciese_ExerciesesId] FOREIGN KEY ([ExercisesId]) REFERENCES [dbo].[Exercise] ([Id]) ON DELETE CASCADE
)
