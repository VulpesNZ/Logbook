DECLARE @FieldOption TABLE (FieldOptionId UNIQUEIDENTIFIER, FieldId UNIQUEIDENTIFIER, Text VARCHAR(255), SortOrder INT, Active INT)
-- Sea Kayaking - Duration
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0CBE2281-8906-4009-8F2B-29841F711D14', '13A19761-A26B-416B-AB83-6DA6D7A7F9E5', '< 1 hr', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('64811085-E3B0-44FA-AFA8-CF2783FA314C', '13A19761-A26B-416B-AB83-6DA6D7A7F9E5', '1-4 hrs (half day)', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('6FFBCEBF-6FD0-4ADE-9C40-B8CCD03C1149', '13A19761-A26B-416B-AB83-6DA6D7A7F9E5', '5-8 hrs (full day)', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('FF662EBD-B6BA-4B19-A92D-08C84E16D6E7', '13A19761-A26B-416B-AB83-6DA6D7A7F9E5', '> 8 hrs', 4, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('330E86BF-B124-41C2-8A32-5140E17D2942', '13A19761-A26B-416B-AB83-6DA6D7A7F9E5', 'Multi-day', 5, 1)
-- Sea Kayaking - Role
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('9B355A37-63A2-4FB8-808F-702E9BA4509D', '9A438E88-0748-4D75-BA88-D1C01FED031E', 'Personal', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('E42205CF-B3E2-4B86-93FF-104812162492', '9A438E88-0748-4D75-BA88-D1C01FED031E', 'Guide', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('2D151FB0-E9F6-4200-8FC4-C6DBE3EDC620', '9A438E88-0748-4D75-BA88-D1C01FED031E', 'Teacher/Instructor', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('4E2FEC21-128E-40FA-963E-52AF511F064C', '9A438E88-0748-4D75-BA88-D1C01FED031E', 'Assessor', 4, 1)
-- Sea Kayaking - Wind
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('274C0DC7-E2D6-4016-9C89-21235033C47B', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', '0-10 kts', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('21354DFD-C432-4CA2-8483-7916B64162CA', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', '10-20 kts', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('CA3766FC-A694-4C37-8FF0-3F4220D19EB6', '67C38008-CA39-4A9C-AAAE-0287D9A84EFB', '>20 kts', 3, 1)
-- Sea Kayaking - Rain
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('3B7D851E-07FF-4CE7-A44D-3B45E792B1EE', '47589792-E642-4E5E-BA25-45EF5F04CD7D', 'None', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('808BF871-4E4C-45B3-A25D-483D276638C3', '47589792-E642-4E5E-BA25-45EF5F04CD7D', 'Showers', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('A3330C82-FAA7-43DC-9322-A0EC103B6DA0', '47589792-E642-4E5E-BA25-45EF5F04CD7D', 'Full Rain', 3, 1)
-- Sea Kayaking - Visibility
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0C0FDCDA-F89E-4921-AB06-86D67AAAFDFE', 'CC3F67CB-5415-4997-94A3-88C3CB4CE2D1', 'Clear', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('E4F715CA-D976-46DD-9098-5E9D0EA4C71E', 'CC3F67CB-5415-4997-94A3-88C3CB4CE2D1', 'Reduced', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('6FF24060-D04A-4A2F-8AD4-CE5FD73E8766', 'CC3F67CB-5415-4997-94A3-88C3CB4CE2D1', 'Limited', 3, 1)


MERGE template.FieldOption AS Target
USING @FieldOption AS Source
ON Target.FieldOptionId = Source.FieldOptionId
WHEN NOT MATCHED BY TARGET 
	THEN INSERT (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES (FieldOptionId, FieldId, Text, SortOrder, Active)
WHEN MATCHED
	THEN UPDATE SET FieldOptionId = Source.FieldOptionId, FieldId = Source.FieldId, Text = Source.Text, SortOrder = Source.SortOrder, Active = Source.Active
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;