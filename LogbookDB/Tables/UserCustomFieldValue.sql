CREATE TABLE [dbo].[UserCustomFieldValue]
(
	[CustomFieldId] UNIQUEIDENTIFIER NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL , 
    [ActivityId] UNIQUEIDENTIFIER NOT NULL, 
    [FieldId] UNIQUEIDENTIFIER NOT NULL, 
    [Value] NCHAR(255) NOT NULL, 
    PRIMARY KEY ([CustomFieldId]),
    CONSTRAINT [FK_UserCustomFieldValue_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserCustomFieldValue_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([ActivityId]), 
    CONSTRAINT [FK_UserCustomFieldValue_Field] FOREIGN KEY ([FieldId]) REFERENCES [Field]([FieldId])
)
