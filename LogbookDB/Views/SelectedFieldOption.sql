CREATE VIEW [dbo].[SelectedFieldOption] AS 
	
SELECT		LogbookEntry.LogbookEntryId, Field.FieldId, FieldOption.FieldOptionId, Field.Name, FieldOption.Text
FROM		LogbookEntryFieldOption
JOIN		FieldOption ON FieldOption.FieldOptionId = LogbookEntryFieldOption.FieldOptionId
JOIN		Field ON Field.FieldId = FieldOption.FieldId
JOIN		LogbookEntry ON LogbookEntry.LogbookEntryId = LogbookEntryFieldOption.LogbookEntryId
WHERE		Selected = 1

UNION		

SELECT		LogbookEntry.LogbookEntryId, Field.FieldId, NULL, Field.Name, CustomValue
FROM		LogbookEntryFieldOptionCustom
JOIN		Field ON Field.FieldId = LogbookEntryFieldOptionCustom.FieldId
JOIN		LogbookEntry ON LogbookEntry.LogbookEntryId = LogbookEntryFieldOptionCustom.LogbookEntryId