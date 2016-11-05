CREATE TABLE [dbo].[ActivityField]
(
	[ActivityId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FieldId] UNIQUEIDENTIFIER NOT NULL, 
    [DefaultValue] NVARCHAR(255) NOT NULL, 
    [DescriptionText] NVARCHAR(MAX) NULL, 
    [IsRequired] BIT NOT NULL, 
    [IsMultiSelect] BIT NOT NULL,
    CONSTRAINT [FK_ActivityField_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([ActivityId]), 
    CONSTRAINT [FK_ActivityField_Field] FOREIGN KEY ([FieldId]) REFERENCES [Field]([FieldId])
)
