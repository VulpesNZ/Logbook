CREATE TABLE [dbo].[LogbookEntryFieldOptionCustom]
(
	[LogbookEntryId] UNIQUEIDENTIFIER NOT NULL, 
    [FieldId] UNIQUEIDENTIFIER NOT NULL, 
    [CustomValue] VARCHAR(MAX) NOT NULL ,
    CONSTRAINT [FK_LogbookEntryFieldOptionCustom_LogbookEntryId] FOREIGN KEY ([LogbookEntryId]) REFERENCES [LogbookEntry]([LogbookEntryId]), 
    CONSTRAINT [FK_LogbookEntryFieldOptionCustom_FieldId] FOREIGN KEY ([FieldId]) REFERENCES [Field]([FieldId]),
	CONSTRAINT [PK_LogbookEntryFieldOptionCustom] PRIMARY KEY ([LogbookEntryId], [FieldId])
)
