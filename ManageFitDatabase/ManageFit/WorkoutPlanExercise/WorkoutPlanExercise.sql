CREATE TABLE [dbo].[WorkoutPlanExercise]
(
	[WorkoutPlansId] UNIQUEIDENTIFIER NOT NULL,
    [ExercisesId] UNIQUEIDENTIFIER NOT NULL,
    [Set] INTEGER,
    [Repetition] INTEGER,
    [Time] FLOAT,
    CONSTRAINT [PK_WorkoutPlanExerciese] PRIMARY KEY ([WorkoutPlansId], [ExercisesId]),
    CONSTRAINT [FK_WorkourPlans] FOREIGN KEY ([WorkoutPlansId]) REFERENCES [dbo].[WorkoutPlan] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ExerciesesId] FOREIGN KEY ([ExercisesId]) REFERENCES [dbo].[Exercise] ([Id]) ON DELETE CASCADE
)
