﻿DECLARE @FieldOption TABLE (FieldOptionId UNIQUEIDENTIFIER, FieldId UNIQUEIDENTIFIER, Text VARCHAR(255), SortOrder INT, Active INT)
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
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('A3330C82-FAA7-43DC-9322-A0EC103B6DA0', '47589792-E642-4E5E-BA25-45EF5F04CD7D', 'Light Rain', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('253054DF-B03B-53A1-4F01-918E01FC386C', '47589792-E642-4E5E-BA25-45EF5F04CD7D', 'Heavy Rain', 4, 1)
-- Sea Kayaking - Visibility
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0C0FDCDA-F89E-4921-AB06-86D67AAAFDFE', 'CC3F67CB-5415-4997-94A3-88C3CB4CE2D1', 'Clear', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('E4F715CA-D976-46DD-9098-5E9D0EA4C71E', 'CC3F67CB-5415-4997-94A3-88C3CB4CE2D1', 'Reduced', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('6FF24060-D04A-4A2F-8AD4-CE5FD73E8766', 'CC3F67CB-5415-4997-94A3-88C3CB4CE2D1', 'Limited', 3, 1)



-- White-water Kayaking - Duration
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('8B6E70B6-7826-0085-489E-F8F2681660F1', 'AB25C3BD-8820-A08F-4B07-63D2B605FEE0', '< 1 hr', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('AE9DC1D7-B924-F99A-4900-9DEB6AD48534', 'AB25C3BD-8820-A08F-4B07-63D2B605FEE0', '1-4 hrs (half day)', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('8BE7AB75-AE52-4BB0-412D-CA801D36A432', 'AB25C3BD-8820-A08F-4B07-63D2B605FEE0', '5-8 hrs (full day)', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('125EA0B5-5CB2-E79E-4E5D-CC7340498807', 'AB25C3BD-8820-A08F-4B07-63D2B605FEE0', '> 8 hrs', 4, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('8769C3DB-7874-0690-4051-7470B0247F80', 'AB25C3BD-8820-A08F-4B07-63D2B605FEE0', 'Multi-day', 5, 1)
-- White-water Kayaking - Role
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('8F9A76F2-B0F5-38AA-4A0D-743878E6EC96', 'ED117A40-E0AA-2998-4CC2-D6F192EA0F64', 'Personal', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('54F40DEB-7E9D-F494-41A8-428642C43F3C', 'ED117A40-E0AA-2998-4CC2-D6F192EA0F64', 'Guide', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('18F3B053-D69A-ADB8-4F95-82C4C0A6AAB9', 'ED117A40-E0AA-2998-4CC2-D6F192EA0F64', 'Teacher/Instructor', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('41C4F66A-614D-01A2-4680-512ADC372B25', 'ED117A40-E0AA-2998-4CC2-D6F192EA0F64', 'Assessor', 4, 1)
-- White-water Kayaking - Rain
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('E5C78B9E-8F50-D5A1-475B-13F68840F3C6', '7E9ACB9C-E36B-769F-4311-C1CB88ED319A', 'None', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('D557E36B-7C42-79AB-48EC-FC999D5F6A41', '7E9ACB9C-E36B-769F-4311-C1CB88ED319A', 'Showers', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('2DEA8AB1-AA1C-8884-4968-B2ED64469585', '7E9ACB9C-E36B-769F-4311-C1CB88ED319A', 'Light Rain', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('FEBCB167-B5C4-32BB-4509-377A9445293A', '7E9ACB9C-E36B-769F-4311-C1CB88ED319A', 'Heavy Rain', 4, 1)




-- White-water Rafting - Duration
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0B4899A3-4734-7DA2-4B13-DC32CAC37FD7', '19631301-955B-C285-4AB8-D0B10F57653A', '< 1 hr', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('01293BB8-DF0E-52AD-4935-9C0732A282A8', '19631301-955B-C285-4AB8-D0B10F57653A', '1-4 hrs (half day)', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('DC516F2F-0CAD-19A0-4F2A-968F7B063ED7', '19631301-955B-C285-4AB8-D0B10F57653A', '5-8 hrs (full day)', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('FB09C70F-8A62-AFA0-4479-FC30219ADFF5', '19631301-955B-C285-4AB8-D0B10F57653A', '> 8 hrs', 4, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('7C9F000D-101A-76AA-46E3-A52C53E06256', '19631301-955B-C285-4AB8-D0B10F57653A', 'Multi-day', 5, 1)
-- White-water Rafting - Role
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('19E3383C-79E3-CCAA-4693-86B62A3FA797', '7F60A5D7-AF6F-E585-4BE5-C7E0D2ED7906', 'Personal', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0383D729-E4BD-A792-489B-CDE6ECE64BC6', '7F60A5D7-AF6F-E585-4BE5-C7E0D2ED7906', 'Guide', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('63E80E79-02A3-ECA0-46A1-6B2581FCD759', '7F60A5D7-AF6F-E585-4BE5-C7E0D2ED7906', 'Teacher/Instructor', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('4E869638-41E8-ADBE-4D7D-807B9F3B8DA7', '7F60A5D7-AF6F-E585-4BE5-C7E0D2ED7906', 'Assessor', 4, 1)
-- White-water Rafting - Rain
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('64E26253-70EA-48B9-468B-5D2166AFE5D4', '19938EE0-2616-6A97-47DA-16A80A1F55FC', 'None', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('E6F738A9-5BBA-AEAA-4AF4-0DDBA0A7851F', '19938EE0-2616-6A97-47DA-16A80A1F55FC', 'Showers', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('06ADB445-242C-BBAF-4232-DF38D33E3592', '19938EE0-2616-6A97-47DA-16A80A1F55FC', 'Light Rain', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('B0CF3AF7-78F5-4391-4503-2CFCDB30C301', '19938EE0-2616-6A97-47DA-16A80A1F55FC', 'Heavy Rain', 4, 1)


-- Tramping (Sub-Alpine) - Duration
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('4DF38475-A647-4E9C-4AB1-3609C320A632', 'D8AA0989-DC1A-F78F-4A23-5092EEAF8EF9', '< 1 hr', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('56F86346-6F7D-7F97-4DE2-644C71EF0251', 'D8AA0989-DC1A-F78F-4A23-5092EEAF8EF9', '1-4 hrs (half day)', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('986525AF-7B8B-9CA1-4BC5-98F66A300987', 'D8AA0989-DC1A-F78F-4A23-5092EEAF8EF9', '5-8 hrs (full day)', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('14A4A512-D2B3-5189-4770-A0AA900ED940', 'D8AA0989-DC1A-F78F-4A23-5092EEAF8EF9', '> 8 hrs', 4, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('D6127CA9-8389-079E-44E6-6ACC6584534B', 'D8AA0989-DC1A-F78F-4A23-5092EEAF8EF9', 'Multi-day', 5, 1)
-- Tramping (Sub-Alpine) - Role
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('A862EE47-F9BC-0AB4-4CA3-41F9308E90EF', '7CFDC1F2-CD6E-9CA7-46BC-1F21D96D8094', 'Personal', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('7C78BDEE-6D41-2D84-4FE6-D65EA767E914', '7CFDC1F2-CD6E-9CA7-46BC-1F21D96D8094', 'Guide', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('A319E139-A561-17A4-4D7A-68FDF699DFB1', '7CFDC1F2-CD6E-9CA7-46BC-1F21D96D8094', 'Teacher/Instructor', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('597D3239-8B25-66A0-4CB4-CE750A32AE77', '7CFDC1F2-CD6E-9CA7-46BC-1F21D96D8094', 'Assessor', 4, 1)
-- Tramping (Sub-Alpine) - Wind
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('8B07C704-F064-1EBA-412B-60F4921C2545', '95B7C227-9E0B-69B0-4C7A-3B2CD921B0FE', '0-20 km/h', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0DB70BC4-D41D-6191-49E7-33837B8B831F', '95B7C227-9E0B-69B0-4C7A-3B2CD921B0FE', '20-40 km/h', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('ED396FA0-FF3C-8DAB-4396-C95B7B463FAA', '95B7C227-9E0B-69B0-4C7A-3B2CD921B0FE', '40-60 km/h', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('1763FD5A-BFC0-839F-4B67-38946321EC85', '95B7C227-9E0B-69B0-4C7A-3B2CD921B0FE', '> 60 km/h', 4, 1)
-- Tramping (Sub-Alpine) - Rain
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('5FAEE3DA-F110-DDB2-41DC-084F505F4736', 'FE223550-656C-F4A8-43EC-7A79CE89514F', 'None', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('153111B7-E39D-8181-43B4-5C3BA5C3E8BA', 'FE223550-656C-F4A8-43EC-7A79CE89514F', 'Showers', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('4A9D4D7F-26D3-1B86-44AA-9F6BD0EFD56B', 'FE223550-656C-F4A8-43EC-7A79CE89514F', 'Light Rain', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('3383613F-C703-C99B-413D-F973DEDF2729', 'FE223550-656C-F4A8-43EC-7A79CE89514F', 'Heavy Rain', 4, 1)
-- Tramping (Sub-Alpine) - Visibility
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('65B8C9C8-9ECE-B582-40BA-F847199102E2', 'AE1B847D-8173-ADAE-40CC-F0AFEF70F807', 'Clear', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('F679C964-DC09-FBBB-4A97-412EDCFF974C', 'AE1B847D-8173-ADAE-40CC-F0AFEF70F807', 'Reduced', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('88128E0C-AB24-0BAF-45F3-7F0D67879D1C', 'AE1B847D-8173-ADAE-40CC-F0AFEF70F807', 'Limited', 3, 1)

-- Alpine - Duration
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('B614AD26-4620-1AAA-4398-91422DE20F8C', '9F8553FD-5053-D7AE-4BE7-5589A576F42C', '< 1 hr', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('460C9717-AD5D-6CBC-4A45-F5E29C3EF092', '9F8553FD-5053-D7AE-4BE7-5589A576F42C', '1-4 hrs (half day)', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('C26798EF-5E08-BDA1-4357-2899C6184868', '9F8553FD-5053-D7AE-4BE7-5589A576F42C', '5-8 hrs (full day)', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0F45361E-C8A3-BCAC-4F48-ED17C2303E1B', '9F8553FD-5053-D7AE-4BE7-5589A576F42C', '> 8 hrs', 4, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('D35CB85A-0054-07A5-4F97-2EFAB04EA735', '9F8553FD-5053-D7AE-4BE7-5589A576F42C', 'Multi-day', 5, 1)
-- Alpine - Role
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('EEC9D169-6DFA-D8B9-40CD-FFBA8B234BDA', '726424F3-7225-3A85-4AC5-699F67D01213', 'Personal', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('9D1B28A9-5B7E-1E86-4428-75DB32F5308D', '726424F3-7225-3A85-4AC5-699F67D01213', 'Guide', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('9B290348-BA6C-9CBF-4B10-0D958F7AA307', '726424F3-7225-3A85-4AC5-699F67D01213', 'Teacher/Instructor', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('856D1616-2B46-5A8C-4495-BA4ABD5E2CE5', '726424F3-7225-3A85-4AC5-699F67D01213', 'Assessor', 4, 1)
-- Alpine - Wind
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('5BE2DA9B-F444-33A9-4524-AB017B086D82', '143D81DB-82C7-2B89-4D0D-9FFD8B75D755', '0-20 km/h', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('7DEE9210-545B-A8BC-4308-7FFAA16C5E89', '143D81DB-82C7-2B89-4D0D-9FFD8B75D755', '20-40 km/h', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('20DF641C-183B-2683-4B8F-3DE801B8CB21', '143D81DB-82C7-2B89-4D0D-9FFD8B75D755', '40-60 km/h', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('8F5C4A45-E3BE-EB90-4E51-1B2AA6244111', '143D81DB-82C7-2B89-4D0D-9FFD8B75D755', '> 60 km/h', 4, 1)
-- Alpine - Rain/Show
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('DF4820D0-123D-7486-4FA2-828FF2C31145', '4DB918E8-FF2D-0B85-4DCA-0D3A0153F971', 'None', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('8A250CCC-FDF4-71A7-4B8B-267BA4F94AC0', '4DB918E8-FF2D-0B85-4DCA-0D3A0153F971', 'Showers', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('B9893A9D-0FE2-369F-4B15-7F1E5FF693F4', '4DB918E8-FF2D-0B85-4DCA-0D3A0153F971', 'Light Rain', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('B8CB497D-8115-FEBD-49A0-7748475D58F3', '4DB918E8-FF2D-0B85-4DCA-0D3A0153F971', 'Heavy Rain', 4, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('020F7F2A-CAD3-F495-4240-47E2F3F72478', '4DB918E8-FF2D-0B85-4DCA-0D3A0153F971', 'Light Snow', 5, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('AC526310-A091-998E-445E-92DF8262094E', '4DB918E8-FF2D-0B85-4DCA-0D3A0153F971', 'Heavy Snow', 6, 1)
-- Alpine - Visibility
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('952E0060-E950-B892-4B17-62B067A07695', '0193CCC1-2A13-6687-4DC1-77B584930585', 'Clear', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('7A1FA46C-4758-7C97-40A6-F31B8C5037AD', '0193CCC1-2A13-6687-4DC1-77B584930585', 'Reduced', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('F643626A-ECBD-4D86-4A17-6ECA6B24983B', '0193CCC1-2A13-6687-4DC1-77B584930585', 'Limited', 3, 1)



-- Rock Climbing - Duration
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('35209461-37F2-FCB9-40D7-2078C0E2B9C1', '313F3E2A-1568-7B9F-45B7-0E34D5462005', '< 1 hr', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('1253D43B-24F3-918D-45D3-9E24473AEF49', '313F3E2A-1568-7B9F-45B7-0E34D5462005', '1-4 hrs (half day)', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('97A05B5A-6421-6483-4045-85F8F139C278', '313F3E2A-1568-7B9F-45B7-0E34D5462005', '5-8 hrs (full day)', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('5EB58372-1B12-5CB0-4389-6DA35FD6ADFE', '313F3E2A-1568-7B9F-45B7-0E34D5462005', '> 8 hrs', 4, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('8D2F0632-3FF8-A1A0-4188-D3D307C669FD', '313F3E2A-1568-7B9F-45B7-0E34D5462005', 'Multi-day', 5, 1)
-- Rock Climbing - Type
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('F1460352-F06F-82AB-4EEE-6BF31674A0C2', '40D90C0C-3B26-DF91-4747-453C8C3F01CA', 'Indoor - Top Rope', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('3D9CACA3-8CAA-F3B3-41D2-F933A1A90A24', '40D90C0C-3B26-DF91-4747-453C8C3F01CA', 'Indoor - Lead', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('90C1E808-101A-20B3-450A-0D1FED42CF38', '40D90C0C-3B26-DF91-4747-453C8C3F01CA', 'Outdoor - Top Rope', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('683CE46F-4DC8-F2B1-4F16-AAEB8E76D8E9', '40D90C0C-3B26-DF91-4747-453C8C3F01CA', 'Outdoor - Lead', 4, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('CE36BE6E-F8DD-FF80-42D3-5CA6EFBBEC1A', '40D90C0C-3B26-DF91-4747-453C8C3F01CA', 'Outdoor - Trad Lead', 5, 1)
-- Rock Climbing - Role
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('C2109EFF-532E-DDB0-4BBD-86DE779B5971', '67A11B67-2305-BBAE-4A9D-35B349FD001F', 'Personal', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('DA44BF99-BF24-C7AD-4968-72A6C7C8C318', '67A11B67-2305-BBAE-4A9D-35B349FD001F', 'Guide', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('E2220DE5-912E-3A99-40A9-A46ED682AECE', '67A11B67-2305-BBAE-4A9D-35B349FD001F', 'Teacher/Instructor', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('09CD0EDD-2629-E2B1-49D6-B4597E1BD3B2', '67A11B67-2305-BBAE-4A9D-35B349FD001F', 'Assessor', 4, 1)
-- Rock Climbing - Rain
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('BF592973-AF71-FBB2-43E6-658493F0B978', '19938EE0-2616-6A97-47DA-16A80A1F55FC', 'None', 1, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('38B81177-F8FB-EFA5-479B-0B7278FA68DD', '19938EE0-2616-6A97-47DA-16A80A1F55FC', 'Showers', 2, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('9814AAFF-7F60-3DA5-4FAC-5BE57228312B', '19938EE0-2616-6A97-47DA-16A80A1F55FC', 'Light Rain', 3, 1)
INSERT INTO @FieldOption (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES ('0E464D90-973B-F19A-43B4-674FB5E027C1', '19938EE0-2616-6A97-47DA-16A80A1F55FC', 'Heavy Rain', 4, 1)







MERGE template.FieldOption AS Target
USING @FieldOption AS Source
ON Target.FieldOptionId = Source.FieldOptionId
WHEN NOT MATCHED BY TARGET 
	THEN INSERT (FieldOptionId, FieldId, Text, SortOrder, Active) VALUES (FieldOptionId, FieldId, Text, SortOrder, Active)
WHEN MATCHED
	THEN UPDATE SET FieldOptionId = Source.FieldOptionId, FieldId = Source.FieldId, Text = Source.Text, SortOrder = Source.SortOrder, Active = Source.Active
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;