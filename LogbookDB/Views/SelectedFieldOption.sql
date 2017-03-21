CREATE VIEW [dbo].[SelectedFieldOption] AS 
	
SELECT		TOP 100 PERCENT LogbookEntry.LogbookEntryId, Field.Name, FieldOption.Text
FROM		LogbookEntryFieldOption
JOIN		FieldOption ON FieldOption.FieldOptionId = LogbookEntryFieldOption.FieldOptionId
JOIN		Field ON Field.FieldId = FieldOption.FieldId
JOIN		LogbookEntry ON LogbookEntry.LogbookEntryId = LogbookEntryFieldOption.LogbookEntryId
WHERE		Selected = 1
ORDER BY	Field.SortOrder, FieldOption.SortOrder