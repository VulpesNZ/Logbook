DECLARE @FieldOption TABLE (FieldOptionId UNIQUEIDENTIFIER, FieldId UNIQUEIDENTIFIER, Text VARCHAR(255), SortOrder INT, Active INT)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0CBE2281-8906-4009-8F2B-29841F711D14', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'No Cloud', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('EE004AA5-E038-4338-BF47-3613DEE1F2F8', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Light Cloud', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('19DE38A2-1DB1-4A29-89C4-5C6BD2DBCA81', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Overcast', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('7D7EBA86-C9E7-450F-99FA-3DBC8AF943EB', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'No Wind', 10, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('E1BCE03E-D67C-4A39-AC4D-858D0F3A436A', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Light Wind', 11, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('92FE8D66-38BA-492B-9938-198C0699EE00', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Moderate Wind', 12, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('FCDB6389-ECF9-4C9F-BB3D-3DAE2784B66B', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Strong Wind', 13, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('5B649E18-4DD0-48D7-8C20-9ADC30EEA5AE', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Extreme Wind', 14, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('9F1E0B79-A43D-4D44-BBCF-DD8AEAAE87DE', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'No Rain', 20, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('E76CBF61-3174-4BBA-888B-93F51C4BB4E3', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Light Rain', 21, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('3912BA22-8205-4737-85AA-A7FD455E10FB', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Moderate Rain', 22, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('A6AAAFD1-E9F6-4B19-A563-FC9110360256', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', 'Heavy Rain', 23, 1)

MERGE template.FieldOption AS Target
USING @FieldOption AS Source
ON Target.FieldOptionId = Source.FieldOptionId
WHEN NOT MATCHED BY TARGET 
	THEN INSERT (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES (FieldOptionId, FieldId, Text, SortOrder, Active)
WHEN MATCHED
	THEN UPDATE SET FieldOptionId = Source.FieldOptionId, FieldId = Source.FieldId, Text = Source.Text, SortOrder = Source.SortOrder, Active = Source.Active
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;