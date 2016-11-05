CREATE TABLE [dbo].[Preference]
(
	[PreferenceId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DisplayName] NVARCHAR(255) NOT NULL, 
    [DefaultValue] NVARCHAR(255) NOT NULL
)
