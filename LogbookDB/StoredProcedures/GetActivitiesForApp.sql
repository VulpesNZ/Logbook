CREATE PROCEDURE [dbo].[GetActivitiesForApp]
	@UserId UNIQUEIDENTIFIER
AS

SELECT		Activity.Name AS ActivityName, 
			Activity.ActivityId, 
			Field.Name AS FieldName, 
			Field.FieldId, 
			Field.SortOrder AS FieldSortOrder, 
			FieldOption.Text AS FieldOptionText, 
			FieldOption.FieldOptionId, 
			FieldOption.SortOrder AS FieldOptionSortOrder
FROM		Activity
LEFT JOIN	Field ON Field.ActivityId = Activity.ActivityId
LEFT JOIN	FieldOption ON FieldOption.FieldId = Field.FieldId
WHERE		Activity.UserId = @UserId
