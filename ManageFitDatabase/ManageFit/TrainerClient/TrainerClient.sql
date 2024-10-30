CREATE TABLE [dbo].[TrainerClient]
(
	[TrainerUid] UNIQUEIDENTIFIER NOT NULL,
	[ClientUid] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT PK_Trainer_Client PRIMARY KEY ([TrainerUid], [ClientUid]),
	CONSTRAINT FK_Traiener FOREIGN KEY ([TrainerUid]) REFERENCES [dbo].[Trainer]([Uid]) ON DELETE CASCADE,
	CONSTRAINT FK_Client FOREIGN KEY ([ClientUid]) REFERENCES [dbo].[Client]([Uid]) ON DELETE CASCADE
)
