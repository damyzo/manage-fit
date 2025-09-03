CREATE TABLE [dbo].[WorkoutPlanClient]
(
	[WorkoutPlansId] UNIQUEIDENTIFIER NOT NULL,
    [ClientsId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_WorkoutPlanClient] PRIMARY KEY ([WorkoutPlansId], [ClientsId]),
    CONSTRAINT [FK_WorkoutPlanClient_WorkourPlans] FOREIGN KEY ([WorkoutPlansId]) REFERENCES [dbo].[WorkoutPlan] ([Id]),
    CONSTRAINT [FK_WorkoutPlanClient_Client] FOREIGN KEY ([ClientsId]) REFERENCES [dbo].[Client] ([Id]) ON DELETE CASCADE
)
