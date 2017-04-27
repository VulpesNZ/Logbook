CREATE PROCEDURE [dbo].[SetFieldCustomText]
	@LogbookEntryId UNIQUEIDENTIFIER,
	@FieldId UNIQUEIDENTIFIER,
	@CustomText VARCHAR(MAX)
AS
	MERGE LogbookEntryFieldOptionCustom AS TARGET
	USING (SELECT @LogbookEntryId AS LogbookEntryId, @FieldId AS FieldId, @CustomText AS CustomText) AS SOURCE
	ON TARGET.LogbookEntryId = SOURCE.LogbookEntryId AND TARGET.FieldId = SOURCE.FieldId
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (LogbookEntryId, FieldId, CustomValue) VALUES (SOURCE.LogbookEntryId, SOURCE.FieldId, SOURCE.CustomText)
	WHEN MATCHED THEN 
		UPDATE SET CustomValue = SOURCE.CustomText
	;
RETURN 0
