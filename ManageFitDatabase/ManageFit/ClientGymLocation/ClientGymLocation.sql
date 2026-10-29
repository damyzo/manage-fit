CREATE TABLE [dbo].[ClientGymLocation]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[ClientId] UNIQUEIDENTIFIER NOT NULL,
	[GymLocationId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT PK_ClientGymLocation_Client_GymLocation PRIMARY KEY ([ClientId], [GymLocationId]),
	CONSTRAINT FK_ClientGymLocation_Client FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client]([Id]) ON DELETE CASCADE,
	CONSTRAINT FK_ClientGymLocation_GymLocation FOREIGN KEY ([GymLocationId]) REFERENCES [dbo].[GymLocation]([Id]) ON DELETE CASCADE
)
