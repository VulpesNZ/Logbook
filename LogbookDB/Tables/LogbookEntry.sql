CREATE TABLE [dbo].[LogbookEntry]
(
	[LogbookEntryId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [LogbookId] UNIQUEIDENTIFIER NULL, 
    [ActivityId] UNIQUEIDENTIFIER NULL,
    [CreatedBy] UNIQUEIDENTIFIER NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NULL, 
    [CreateDate] DATETIME NULL DEFAULT GETDATE(), 
    [UpdateDate] DATETIME NULL DEFAULT GETDATE(), 
    [Status] NVARCHAR(50) NULL,
    [EntryDate] DATETIME NULL, 
    [Notes] TEXT NULL, 
    CONSTRAINT [FK_LogbookEntry_CreatedByUser] FOREIGN KEY ([CreatedBy]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_LogbookEntry_UpdatedByUser] FOREIGN KEY ([UpdatedBy]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_LogbookEntry_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([ActivityId])
)
