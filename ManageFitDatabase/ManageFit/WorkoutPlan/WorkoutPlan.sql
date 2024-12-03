CREATE TABLE [dbo].[WorkoutPlan]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [TrainerId] UNIQUEIDENTIFIER NOT NULL,
    [ClientId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [FK_WorkoutPlanExerciese_TrainerId] FOREIGN KEY ([TrainerId]) REFERENCES [dbo].[Trainer] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_WorkoutPlanExerciese_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]) ON DELETE CASCADE
)
