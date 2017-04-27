DECLARE @Activity TABLE (ActivityId UNIQUEIDENTIFIER, Name VARCHAR(200), Description TEXT, ImageUrl VARCHAR(200))
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('570D0E31-90F1-4FD3-A937-01F8CAECB773', 'Abseiling', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('82A1E5E0-2513-4DE4-9901-1BFD9782359E', 'White Water Kayaking', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('9A98187D-A835-4C77-B902-35C66CF3128E', 'Tramping (Sub-Alpine)', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('8FD4A9B2-3267-46B4-9B29-5D45B589D832', 'Skiing', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('CEEA2119-DF13-4A0C-B0BD-714F8B50F89B', 'Mountain Biking', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('3FC2DCFB-46FD-43BC-8110-8AC3538439E6', 'Caving', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('563A9A6D-319C-416A-ABE2-9603E660A19D', 'Canoeing', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('E2F4895E-4AD7-41F4-AEE9-A8BA099F0767', 'Rock Climbing', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('294A5142-0CD7-4C91-A8FF-B17E3E790CC7', 'Sea Kayaking', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('3162E951-75F0-4A40-B072-C50CA7922888', 'Alpine', NULL,NULL)
INSERT INTO @Activity (ActivityId, Name, Description, ImageUrl) VALUES ('FF0B3D69-41C2-4282-B263-DE456DDF927B', 'Rafting', NULL,NULL)

MERGE template.Activity AS Target
USING @Activity AS Source
ON Target.ActivityId = Source.ActivityId
WHEN NOT MATCHED BY TARGET 
	THEN INSERT (ActivityId, Name, Description, ImageUrl) VALUES (Source.ActivityId, Source.Name, Source.Description, Source.ImageUrl)
WHEN MATCHED
	THEN UPDATE SET Name = Source.Name, Description = Source.Description, ImageUrl = Source.ImageUrl
WHEN NOT MATCHED BY SOURCE
	THEN DELETE;