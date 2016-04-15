CREATE TABLE [dbo].[EventStreams]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Type] NVARCHAR(255) NOT NULL, 
    [Version] INT NOT NULL
)


CREATE TABLE [dbo].[Events]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [EventType] NVARCHAR(255) NOT NULL, 
    [Version] INT NOT NULL, 
    [Payload] NVARCHAR(MAX) NOT NULL, 
    [EventStreamId] UNIQUEIDENTIFIER NOT NULL, 
    [TimeStampUtc] DATETIME2 NOT NULL,
    CONSTRAINT [FK_EventStreamId] FOREIGN KEY ([EventStreamId]) REFERENCES [EventStreams]([Id])
)

CREATE TABLE [dbo].[Snapshots]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [EventStreamId] UNIQUEIDENTIFIER NOT NULL,
    [Payload] NVARCHAR(MAX) NOT NULL,
    [CreatedUtc] DATETIME2 NOT NULL,
    CONSTRAINT [FK_Snapshot_EventStreamId] FOREIGN KEY ([EventStreamId]) REFERENCES [EventStreams]([Id])
)