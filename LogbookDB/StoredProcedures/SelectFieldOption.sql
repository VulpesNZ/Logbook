CREATE PROCEDURE [dbo].[SelectFieldOption]
	@LogbookEntryId UNIQUEIDENTIFIER,
	@FieldOptionId UNIQUEIDENTIFIER,
	@Selected BIT
AS
	MERGE LogbookEntryFieldOption AS TARGET
	USING (SELECT @LogbookEntryId AS LogbookEntryId, @FieldOptionId AS FieldOptionId, @Selected AS Selected) AS SOURCE
	ON TARGET.LogbookEntryId = SOURCE.LogbookEntryId AND TARGET.FieldOptionId = SOURCE.FieldOptionId
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (LogbookEntryId, FieldOptionId, Selected) VALUES (SOURCE.LogbookEntryId, SOURCE.FieldOptionId, SOURCE.Selected)
	WHEN MATCHED THEN 
		UPDATE SET Selected = SOURCE.Selected
	;

RETURN 0
