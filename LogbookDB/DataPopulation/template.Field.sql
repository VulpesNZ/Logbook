DECLARE @Field TABLE (FieldId UNIQUEIDENTIFIER, ActivityId UNIQUEIDENTIFIER, Name VARCHAR(200), IsRequired BIT, IsMultiSelect BIT, AllowFreeText BIT)

INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('13A19761-A26B-416B-AB83-6DA6D7A7F9E5', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Duration', 1, 0, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('ED949CB6-5DE2-4A4F-B67A-A526DC8E9014', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Location', 1, 0, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('6F54C80F-1D28-4121-B4B6-C1869466E4F0', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Route', 1, 0, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('9A438E88-0748-4D75-BA88-D1C01FED031E', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Role', 1, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('67C38008-CA39-4A9C-AAAE-0287D9A84EFB', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Wind', 1, 0, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('47589792-E642-4E5E-BA25-45EF5F04CD7D', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Rain', 1, 0, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('CC3F67CB-5415-4997-94A3-88C3CB4CE2D1', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Visibility', 1, 0, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('DB2CB21A-316C-4357-8265-E603B9C60AAA', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Size of Group', 1, 0, 1)

MERGE template.Field AS Target
USING @Field AS Source
ON Target.FieldId = Source.FieldId
WHEN NOT MATCHED BY TARGET 
	THEN INSERT (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText)
WHEN MATCHED
	THEN UPDATE SET ActivityId = Source.ActivityId, Name = Source.Name, IsRequired = Source.IsRequired, IsMultiSelect = Source.IsMultiSelect, AllowFreeText = Source.AllowFreeText
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;