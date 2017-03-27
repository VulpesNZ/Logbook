CREATE TABLE [dbo].[LogbookEntryFieldOption]
(
	[LogbookEntryId] UNIQUEIDENTIFIER NOT NULL, 
    [FieldOptionId] UNIQUEIDENTIFIER NOT NULL, 
    [Selected] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_LogbookEntryFieldOption_LogbookEntryId] FOREIGN KEY ([LogbookEntryId]) REFERENCES [LogbookEntry]([LogbookEntryId]), 
    CONSTRAINT [FK_LogbookEntryFieldOption_FieldOptionId] FOREIGN KEY ([FieldOptionId]) REFERENCES [FieldOption]([FieldOptionId]),
	CONSTRAINT [PK_LogbookEntryFieldOption] PRIMARY KEY ([LogbookEntryId], [FieldOptionId])
)
