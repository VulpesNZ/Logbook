CREATE PROCEDURE [dbo].[PopulateDefaultActivities]
	@UserId UNIQUEIDENTIFIER = '2E0975F1-0DF5-4F6D-BA6D-93768218CAA9'
AS

-- Create the default set of activities and fields for a new user, or recreate them for an existing user on request.

BEGIN TRAN

-- Populate Activities

MERGE dbo.Activity AS T
USING template.Activity S
ON T.Name = S.Name AND T.UserId = @UserId AND T.TemplateActivityId = S.ActivityId
WHEN NOT MATCHED BY TARGET AND NOT EXISTS (SELECT * FROM dbo.Activity WHERE UserId = @UserId AND Name = S.Name) THEN INSERT (UserId, Name, Description, ImageURL, Active, TemplateActivityId) VALUES (@UserId, Name, Description, ImageURL, 1, S.ActivityId)
WHEN MATCHED AND T.Active = 0 THEN UPDATE SET Active = 1;  -- don't create a new one, just reactivate the existing one

-- Populate Fields

MERGE dbo.Field AS T
USING template.Field S
ON T.UserId = @UserId AND T.TemplateFieldId = S.FieldId
WHEN NOT MATCHED BY TARGET AND EXISTS (SELECT * FROM Activity WHERE UserId = @UserId AND TemplateActivityId = S.ActivityId)
	THEN INSERT (ActivityId, UserId, Name, IsRequired, AllowFreeText, IsMultiSelect, SortOrder, Active, TemplateFieldId) 
	VALUES ((SELECT TOP 1 Activity.ActivityId FROM dbo.Activity WHERE UserId = @UserId AND Activity.TemplateActivityId = S.ActivityId)
			, @UserId, Name, IsRequired, AllowFreeText, IsMultiSelect, SortOrder, 1, S.FieldId)
WHEN MATCHED AND T.Active = 0 THEN UPDATE SET Active = 1;

-- Populate FieldOptions

MERGE dbo.FieldOption AS T
USING template.FieldOption S
ON T.TemplateFieldOptionId = S.FieldOptionId
WHEN NOT MATCHED BY TARGET AND EXISTS (SELECT * FROM Field WHERE UserId = @UserId AND TemplateFieldId = S.FieldId) 
	THEN INSERT (FieldId, Text, SortOrder, Active, TemplateFieldOptionId) 
	VALUES ((SELECT TOP 1 Field.FieldId FROM dbo.Field WHERE UserId = @UserId AND Field.TemplateFieldId = S.FieldId)
			, Text, SortOrder, Active, S.FieldOptionId)
WHEN MATCHED AND T.Active = 0 THEN UPDATE SET Active = 1;

RETURN 0
