CREATE PROCEDURE [dbo].[GetReportDataForActivity]
	@ActivityId UNIQUEIDENTIFIER,
	@StartDate DATETIME,
	@EndDate DATETIME
AS
	SET NOCOUNT ON

	CREATE TABLE #ReportData (
		ActivityId UNIQUEIDENTIFIER,
		LogbookEntryId UNIQUEIDENTIFIER,
		[Activity] VARCHAR(200),
		[Date] DATETIME,
		Notes VARCHAR(200)
	)

	INSERT INTO #ReportData
	SELECT		Activity.ActivityId, LogbookEntryId, Activity.Name, EntryDate, Notes
	FROM		LogbookEntry
	JOIN		Activity ON Activity.ActivityId = LogbookEntry.ActivityId
	WHERE		Activity.ActivityId = @ActivityId 
	AND			Status = 'STATUS/ACTIVE'
	AND			EntryDate BETWEEN @StartDate AND @EndDate

	CREATE TABLE #UpdateStatements (
		Statement VARCHAR(512)
	)

	DECLARE @FieldCursor AS CURSOR;
	DECLARE @FieldId UNIQUEIDENTIFIER;
	DECLARE @FieldName VARCHAR(200);

	SET @FieldCursor = CURSOR FOR
	 SELECT FieldId, Name 
	 FROM Field
	 WHERE Field.ActivityId = @ActivityId
	 ORDER BY Field.SortOrder

	 OPEN @FieldCursor;
	 FETCH NEXT FROM @FieldCursor INTO @FieldId, @FieldName

	 WHILE @@FETCH_STATUS = 0
	BEGIN

	DECLARE @sql NVARCHAR(512) = 'ALTER TABLE #ReportData ADD [' + @FieldName + '] VARCHAR(200) NULL;'

	 exec sp_executesql @sql

	 FETCH NEXT FROM @FieldCursor INTO @FieldId, @FieldName
	END

	CLOSE @FieldCursor
	DEALLOCATE @FieldCursor

	INSERT INTO #UpdateStatements
	SELECT		'UPDATE #ReportData SET [' + Name + '] = ''' + Text + ''' WHERE LogbookEntryId = ''' + CONVERT(VARCHAR(36), LogbookEntryId) + ''''
	FROM
	( 

		SELECT		LogbookEntry.LogbookEntryId, Field.Name, FieldOption.Text
		FROM		Logbook
		JOIN		LogbookEntry ON LogbookEntry.LogbookId = Logbook.LogbookId
		JOIN		LogbookEntryFieldOption ON LogbookEntryFieldOption.LogbookEntryId = LogbookEntry.LogbookEntryId
		JOIN		FieldOption ON FieldOption.FieldOptionId = LogbookEntryFieldOption.FieldOptionId
		JOIN		Field ON Field.FieldId = FieldOption.FieldId
		WHERE		LogbookEntryFieldOption.Selected = 1
		AND			LogbookEntry.ActivityId = @ActivityId
		AND			EntryDate BETWEEN @StartDate AND @EndDate

		UNION		

		SELECT		LogbookEntry.LogbookEntryId, Field.Name, LogbookEntryFieldOptionCustom.CustomValue
		FROM		Logbook
		JOIN		LogbookEntry ON LogbookEntry.LogbookId = Logbook.LogbookId
		JOIN		LogbookEntryFieldOptionCustom ON LogbookEntryFieldOptionCustom.LogbookEntryId = LogbookEntry.LogbookEntryId
		JOIN		Field ON Field.FieldId = LogbookEntryFieldOptionCustom.FieldId
		AND			LogbookEntry.ActivityId = @ActivityId
		AND			EntryDate BETWEEN @StartDate AND @EndDate

	) Fields

	DECLARE @StatementCursor AS CURSOR;
	DECLARE @Statement NVARCHAR(512);

	SET @StatementCursor = CURSOR FOR
	 SELECT *
	 FROM #UpdateStatements

	 OPEN @StatementCursor;
	 FETCH NEXT FROM @StatementCursor INTO @Statement

	 WHILE @@FETCH_STATUS = 0
	BEGIN

	 exec sp_executesql @Statement

	 FETCH NEXT FROM @StatementCursor INTO @Statement

	END

	CLOSE @StatementCursor
	DEALLOCATE @StatementCursor

	SELECT		*
	FROM		#ReportData
	ORDER BY	[Date] DESC

	DROP TABLE #ReportData
	DROP TABLE #UpdateStatements

RETURN 0
