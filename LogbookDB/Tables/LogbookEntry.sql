CREATE TABLE [dbo].[LogbookEntry]
(
	[LogbookEntryId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [LogbookId] UNIQUEIDENTIFIER NULL, 
    [ActivityId] UNIQUEIDENTIFIER NULL,
    [CreatedBy] UNIQUEIDENTIFIER NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NULL, 
    [CreatedDate] DATETIME NULL, 
    [UpdateDate] DATETIME NULL, 
    [Status] NVARCHAR(50) NULL,
    CONSTRAINT [FK_LogbookEntry_CreatedByUser] FOREIGN KEY ([CreatedBy]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_LogbookEntry_UpdatedByUser] FOREIGN KEY ([UpdatedBy]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_LogbookEntry_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([ActivityId])
)
