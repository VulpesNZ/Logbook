CREATE TABLE [dbo].[DefaultActivityField]
(
	[ActivityId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FieldId] UNIQUEIDENTIFIER NOT NULL, 
    [DefaultValue] NVARCHAR(255) NOT NULL, 
    [DescriptionText] NVARCHAR(MAX) NULL, 
    [IsRequired] BIT NOT NULL, 
    [IsMultiSelect] BIT NOT NULL,
    CONSTRAINT [FK_DefaultActivityField_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [DefaultActivity]([ActivityId]), 
    CONSTRAINT [FK_DefaultActivityField_Field] FOREIGN KEY ([FieldId]) REFERENCES [Field]([FieldId])
)
