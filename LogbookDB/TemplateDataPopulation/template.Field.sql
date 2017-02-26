DECLARE @Field TABLE (FieldId UNIQUEIDENTIFIER, ActivityId UNIQUEIDENTIFIER, Name VARCHAR(200), IsRequired BIT, IsMultiSelect BIT, AllowFreeText BIT)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('67C38008-CA39-4A9C-AAAE-0287D9A84EFB', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Weather', 1, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('47EE4F02-5617-406E-8AF8-BD9F55B05767', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Sea Conditions', 1, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('9A438E88-0748-4D75-BA88-D1C01FED031E', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Role', 1, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES ('DB2CB21A-316C-4357-8265-E603B9C60AAA', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Number of Clients', 1, 0, 1)

MERGE template.Field AS Target
USING @Field AS Source
ON Target.FieldId = Source.FieldId
WHEN NOT MATCHED BY TARGET 
	THEN INSERT (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText)
WHEN MATCHED
	THEN UPDATE SET ActivityId = Source.ActivityId, Name = Source.Name, IsRequired = Source.IsRequired, IsMultiSelect = Source.IsMultiSelect, AllowFreeText = Source.AllowFreeText
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;