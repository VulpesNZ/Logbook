CREATE TABLE [dbo].[Logbook]
(
	[LogbookId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [IndustryId] UNIQUEIDENTIFIER NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NULL, 
    [CreateDate] DATETIME NULL DEFAULT GETDATE(), 
    [UpdateDate] DATETIME NULL DEFAULT GETDATE(), 
    [Status] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NULL,	
    [DefaultActivityId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [FK_Logbook_IndustryId] FOREIGN KEY ([IndustryId]) REFERENCES [Industry]([IndustryId]),  
    CONSTRAINT [FK_Logbook_CreatedByUser] FOREIGN KEY ([CreatedBy]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_Logbook_UpdatedByUser] FOREIGN KEY ([UpdatedBy]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_Logbook_DefaultActivityId] FOREIGN KEY ([DefaultActivityId]) REFERENCES [Activity]([ActivityId])
)
