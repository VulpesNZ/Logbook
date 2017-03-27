CREATE VIEW [dbo].[ActivityFieldOptionMapping]
	AS 

SELECT		Activity.UserId,
			Field.Name AS FieldName,
			FieldOption.Text AS OptionText,
			Activity.ActivityId,
			Field.FieldId,
			FieldOption.FieldOptionId,
			Field.SortOrder AS FieldSortOrder,
			FieldOption.SortOrder AS FieldOptionSortOrder,
			Field.AllowFreeText
FROM		Activity
JOIN		Field ON Field.ActivityId = Activity.ActivityId
LEFT JOIN	FieldOption ON FieldOption.FieldId = Field.FieldId
