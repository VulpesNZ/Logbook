CREATE TABLE [template].[Field]
(
	[FieldId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
	[ActivityId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
	[IsRequired] BIT NOT NULL DEFAULT 0,
    [AllowFreeText] BIT NOT NULL DEFAULT 1,
    [IsMultiSelect] BIT NOT NULL DEFAULT 0, 
	[SortOrder] INT NOT NULL DEFAULT 0,
    [Active] BIT NOT NULL DEFAULT 1, 
	CONSTRAINT [FK_Field_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [template].[Activity]([ActivityId])
    
)
