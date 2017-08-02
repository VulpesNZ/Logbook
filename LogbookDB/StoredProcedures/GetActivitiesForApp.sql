CREATE PROCEDURE [dbo].[GetActivitiesForApp]
	@UserId UNIQUEIDENTIFIER
AS

SELECT		Activity.Name AS ActivityName, 
			Activity.ActivityId
FROM		Activity
WHERE		Activity.UserId = @UserId
