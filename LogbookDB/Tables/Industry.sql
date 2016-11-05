CREATE TABLE [dbo].[Industry]
(
	[IndustryId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DisplayName] NVARCHAR(255) NULL, 
    [DescriptionText] NVARCHAR(MAX) NULL
)
