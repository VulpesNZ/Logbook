﻿CREATE TABLE [dbo].[User]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Email] NVARCHAR(255) NOT NULL, 
    [PasswordHash] BINARY(32) NOT NULL, 
    [PasswordSalt] BINARY(32) NOT NULL, 
    [Name] NVARCHAR(255) NULL, 
    [Location] NVARCHAR(255) NULL, 
    [Status] NVARCHAR(255) NOT NULL,
	CONSTRAINT AK_User_Email UNIQUE (Email)
)
