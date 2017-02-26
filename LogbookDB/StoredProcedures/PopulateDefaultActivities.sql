CREATE PROCEDURE [dbo].[PopulateDefaultActivities]
	@UserId UNIQUEIDENTIFIER = '2E0975F1-0DF5-4F6D-BA6D-93768218CAA9'
AS

BEGIN TRAN

-- Populate Activities

MERGE dbo.Activity AS T
USING template.Activity S
ON T.Name = S.Name AND T.UserId = @UserId
WHEN NOT MATCHED BY TARGET THEN INSERT (UserId, Name, Description, ImageURL, Active) VALUES (@UserId, Name, Description, ImageURL, 1)
WHEN MATCHED THEN UPDATE SET Active = 1
WHEN NOT MATCHED BY SOURCE AND UserId = @UserId THEN UPDATE SET Active = 0;

-- Populate Fields

MERGE dbo.Field AS T
USING template.Field S
ON T.ActivityId = S.ActivityId AND T.Name = S.Name AND T.UserId = @UserId
WHEN NOT MATCHED BY TARGET THEN INSERT (ActivityId, UserId, Name, IsRequired, AllowFreeText, IsMultiSelect, SortOrder, Active) 
-- TODO:  Hacky as shit
	VALUES ((SELECT TOP 1 Activity.ActivityId FROM dbo.Activity JOIN template.Activity tp ON tp.Name = Activity.Name AND UserId = @UserId)
			, @UserId, Name, IsRequired, AllowFreeText, IsMultiSelect, SortOrder, 1)
WHEN MATCHED THEN UPDATE SET Active = 1
WHEN NOT MATCHED BY SOURCE AND UserId = @UserId THEN UPDATE SET Active = 0;


RETURN 0
