DECLARE @Field TABLE (FieldId UNIQUEIDENTIFIER, ActivityId UNIQUEIDENTIFIER, Name VARCHAR(200), IsRequired BIT, IsMultiSelect BIT, AllowFreeText BIT, SortOrder INT)

-- Sea Kayaking

INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('13A19761-A26B-416B-AB83-6DA6D7A7F9E5', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Duration', 1, 0, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('ED949CB6-5DE2-4A4F-B67A-A526DC8E9014', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Location', 1, 0, 1, 2)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('6F54C80F-1D28-4121-B4B6-C1869466E4F0', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Route', 1, 0, 1, 3)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('9A438E88-0748-4D75-BA88-D1C01FED031E', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Role', 1, 1, 1, 4)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('67C38008-CA39-4A9C-AAAE-0287D9A84EFB', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Wind', 1, 0, 1, 5)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('47589792-E642-4E5E-BA25-45EF5F04CD7D', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Rain', 1, 0, 1, 6)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('CC3F67CB-5415-4997-94A3-88C3CB4CE2D1', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Visibility', 1, 0, 1, 7)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('DB2CB21A-316C-4357-8265-E603B9C60AAA', '294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Group Size', 1, 0, 1, 8)

-- White-water Kayaking

INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('AB25C3BD-8820-A08F-4B07-63D2B605FEE0', '82A1E5E0-2513-4DE4-9901-1BFD9782359E', 'Duration', 1, 0, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('9ADB78DE-324E-DA88-4FE8-0B433211680C', '82A1E5E0-2513-4DE4-9901-1BFD9782359E', 'Location', 1, 0, 1, 2)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('99902257-E95A-7B8F-4509-39ED8ADF876F', '82A1E5E0-2513-4DE4-9901-1BFD9782359E', 'Route', 1, 0, 1, 3)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('ED117A40-E0AA-2998-4CC2-D6F192EA0F64', '82A1E5E0-2513-4DE4-9901-1BFD9782359E', 'Role', 1, 1, 1, 4)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('7E9ACB9C-E36B-769F-4311-C1CB88ED319A', '82A1E5E0-2513-4DE4-9901-1BFD9782359E', 'Rain', 1, 0, 1, 5)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('6D8DB025-07EC-ACA7-4A6D-005FC9DF8BB5', '82A1E5E0-2513-4DE4-9901-1BFD9782359E', 'River Flow', 1, 0, 1, 6)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('FAC5EFDF-1D92-D2A0-4475-3BC6365896AD', '82A1E5E0-2513-4DE4-9901-1BFD9782359E', 'Group Size', 1, 0, 1, 7)

-- White-water Rafting

INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('19631301-955B-C285-4AB8-D0B10F57653A', 'FF0B3D69-41C2-4282-B263-DE456DDF927B', 'Duration', 1, 0, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('599F4B2A-D25C-3692-48D3-4E1B8766E0FE', 'FF0B3D69-41C2-4282-B263-DE456DDF927B', 'Location', 1, 0, 1, 2)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('BCD08083-7829-32BE-400E-22DC901BA78C', 'FF0B3D69-41C2-4282-B263-DE456DDF927B', 'Route', 1, 0, 1, 3)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('7F60A5D7-AF6F-E585-4BE5-C7E0D2ED7906', 'FF0B3D69-41C2-4282-B263-DE456DDF927B', 'Role', 1, 1, 1, 4)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('19938EE0-2616-6A97-47DA-16A80A1F55FC', 'FF0B3D69-41C2-4282-B263-DE456DDF927B', 'Rain', 1, 0, 1, 5)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('0D425965-5C63-4292-4C6D-5414A915A2BD', 'FF0B3D69-41C2-4282-B263-DE456DDF927B', 'River Flow', 1, 0, 1, 6)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('EAA9D0AA-3385-D0B4-4F1F-74496A2E5CE6', 'FF0B3D69-41C2-4282-B263-DE456DDF927B', 'Passengers', 1, 0, 1, 7)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('1CF5D9AD-059D-1BB8-488C-68599C958958', 'FF0B3D69-41C2-4282-B263-DE456DDF927B', 'Total Group Size', 1, 0, 1, 8)

-- Tramping (Sub-Alpine)

INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('D8AA0989-DC1A-F78F-4A23-5092EEAF8EF9', '9A98187D-A835-4C77-B902-35C66CF3128E', 'Duration', 1, 0, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('0F85369C-0123-4185-480D-C9EE5367098F', '9A98187D-A835-4C77-B902-35C66CF3128E', 'Location', 1, 0, 1, 2)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('AC0A9CC7-1D1F-069C-413A-32855F4BF5E0', '9A98187D-A835-4C77-B902-35C66CF3128E', 'Route', 1, 0, 1, 3)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('7CFDC1F2-CD6E-9CA7-46BC-1F21D96D8094', '9A98187D-A835-4C77-B902-35C66CF3128E', 'Role', 1, 1, 1, 4)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('95B7C227-9E0B-69B0-4C7A-3B2CD921B0FE', '9A98187D-A835-4C77-B902-35C66CF3128E', 'Wind', 1, 0, 1, 5)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('FE223550-656C-F4A8-43EC-7A79CE89514F', '9A98187D-A835-4C77-B902-35C66CF3128E', 'Rain', 1, 0, 1, 6)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('AE1B847D-8173-ADAE-40CC-F0AFEF70F807', '9A98187D-A835-4C77-B902-35C66CF3128E', 'Visibility', 1, 0, 1, 7)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('7F4D83D8-59D9-C5BB-413D-6B99029A4A5E', '9A98187D-A835-4C77-B902-35C66CF3128E', 'Group Size', 1, 0, 1, 8)

-- Alpine

INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('9F8553FD-5053-D7AE-4BE7-5589A576F42C', '3162E951-75F0-4A40-B072-C50CA7922888', 'Duration', 1, 0, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('F5B33FAA-1A56-42AB-4865-B4D474B633E2', '3162E951-75F0-4A40-B072-C50CA7922888', 'Location', 1, 0, 1, 2)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('B8F5FCD9-74DF-57A6-4A43-473E03BB8DF3', '3162E951-75F0-4A40-B072-C50CA7922888', 'Route', 1, 0, 1, 3)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('726424F3-7225-3A85-4AC5-699F67D01213', '3162E951-75F0-4A40-B072-C50CA7922888', 'Role', 1, 1, 1, 4)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('143D81DB-82C7-2B89-4D0D-9FFD8B75D755', '3162E951-75F0-4A40-B072-C50CA7922888', 'Wind', 1, 0, 1, 5)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('4DB918E8-FF2D-0B85-4DCA-0D3A0153F971', '3162E951-75F0-4A40-B072-C50CA7922888', 'Rain/Snow', 1, 0, 1, 6)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('0193CCC1-2A13-6687-4DC1-77B584930585', '3162E951-75F0-4A40-B072-C50CA7922888', 'Visibility', 1, 0, 1, 7)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('92751C34-9315-E690-4521-1023225F6FCC', '3162E951-75F0-4A40-B072-C50CA7922888', 'Group Size', 1, 0, 1, 8)

-- Rock Climbing

INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('313F3E2A-1568-7B9F-45B7-0E34D5462005', 'E2F4895E-4AD7-41F4-AEE9-A8BA099F0767', 'Duration', 1, 0, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('E206E5F5-6AEF-DF9D-4A17-0009AB872391', 'E2F4895E-4AD7-41F4-AEE9-A8BA099F0767', 'Location', 1, 0, 1, 2)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('40D90C0C-3B26-DF91-4747-453C8C3F01CA', 'E2F4895E-4AD7-41F4-AEE9-A8BA099F0767', 'Type', 1, 0, 1, 3)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('C93E6ED6-EB55-008A-4305-958E824DBDF8', 'E2F4895E-4AD7-41F4-AEE9-A8BA099F0767', 'No. of Climbs', 1, 0, 1, 4)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('67A11B67-2305-BBAE-4A9D-35B349FD001F', 'E2F4895E-4AD7-41F4-AEE9-A8BA099F0767', 'Role', 1, 1, 1, 5)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('37EDFE2E-CF7F-B5B3-4E0E-ABF984CADFE1', 'E2F4895E-4AD7-41F4-AEE9-A8BA099F0767', 'Rain', 1, 0, 1, 6)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('BA99C3B1-A154-0B82-4F78-DEFF8539A315', 'E2F4895E-4AD7-41F4-AEE9-A8BA099F0767', 'Group Size', 1, 0, 1, 7)

-- Mountain Biking

INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('D5998ECA-1380-9A8D-4718-862663E89087', 'CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Duration', 1, 0, 1, 1)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('33D31938-7A61-E98A-436A-52218BADFF46', 'CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Location', 1, 0, 1, 2)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('094FE037-7A7C-93AC-4344-8B2BF2A22BD9', 'CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Route', 1, 0, 1, 3)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('A803AD46-3D60-A3B9-4922-1FCCFA66FCB1', 'CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Role', 1, 1, 1, 4)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('C8C04CAC-4243-9D95-4666-65DAB5E48A39', 'CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Rain', 1, 0, 1, 5)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('5E9E8BC6-87E7-49A6-46D8-F00FE4941423', 'CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Track Type', 1, 0, 1, 6)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('4FBF05FB-7E50-E191-41A6-5FFBC93738CE', 'CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Track Conditions', 1, 0, 1, 7)
INSERT INTO @Field (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText, SortOrder) VALUES ('2A6365F3-78FA-E3B2-4F51-515CF2F4DB84', 'CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Group Size', 1, 0, 1, 8)



MERGE template.Field AS Target
USING @Field AS Source
ON Target.FieldId = Source.FieldId
WHEN NOT MATCHED BY TARGET 
	THEN INSERT (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText) VALUES (FieldId, ActivityId, Name, IsRequired, IsMultiSelect, AllowFreeText)
WHEN MATCHED
	THEN UPDATE SET ActivityId = Source.ActivityId, Name = Source.Name, IsRequired = Source.IsRequired, IsMultiSelect = Source.IsMultiSelect, AllowFreeText = Source.AllowFreeText
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;