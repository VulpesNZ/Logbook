CREATE PROCEDURE [dbo].[GetActivitiesForApp]
	@UserId UNIQUEIDENTIFIER
AS

SELECT		Activity.ActivityId, Activity.Name, ROW_NUMBER() OVER (ORDER BY	SUM(CASE WHEN LogbookEntry.ActivityId IS NULL THEN 0 ELSE 1 END) DESC, Name ASC) AS SortOrder
FROM		Activity
LEFT JOIN	LogbookEntry ON LogbookEntry.ActivityId = Activity.ActivityId
WHERE		Activity.UserId = @UserId
GROUP BY	Activity.ActivityId, Activity.Name
ORDER BY	SUM(CASE WHEN LogbookEntry.ActivityId IS NULL THEN 0 ELSE 1 END) DESC, Name ASC