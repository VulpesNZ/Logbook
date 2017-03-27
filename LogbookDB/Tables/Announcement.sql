﻿CREATE TABLE [dbo].[Announcement]
(
	[AnnouncementId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Title] VARCHAR(200) NOT NULL, 
    [Body] VARCHAR(MAX) NOT NULL, 
    [PublishDate] DATE NOT NULL
)
