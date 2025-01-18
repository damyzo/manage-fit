CREATE TABLE [dbo].[TrainerClient]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[TrainerId] UNIQUEIDENTIFIER NOT NULL,
	[ClientId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT PK_TrainerClient_Trainer_Client PRIMARY KEY ([TrainerId], [ClientId]),
	CONSTRAINT FK_TrainerClient_Traiener FOREIGN KEY ([TrainerId]) REFERENCES [dbo].[Trainer]([Id]) ON DELETE CASCADE,
	CONSTRAINT FK_TrainerClient_Client FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client]([Id]) ON DELETE CASCADE
)
