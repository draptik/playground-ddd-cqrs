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
    CONSTRAINT [FK_EventStreamId] FOREIGN KEY ([EventStreamId]) REFERENCES [EventStreams]([Id])
)
