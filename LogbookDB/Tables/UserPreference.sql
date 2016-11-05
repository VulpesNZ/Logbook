CREATE TABLE [dbo].[UserPreference]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL , 
    [PreferenceId] UNIQUEIDENTIFIER NOT NULL, 
    [Value] NVARCHAR(255) NOT NULL, 
    PRIMARY KEY ([UserId], [PreferenceId]), 
    CONSTRAINT [FK_UserPreference_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserPreference_Preference] FOREIGN KEY ([PreferenceId]) REFERENCES [Preference]([PreferenceId])
)
