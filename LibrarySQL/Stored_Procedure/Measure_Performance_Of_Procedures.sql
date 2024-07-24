CREATE PROCEDURE [dbo].[Measure_Performance_Of_Procedures]
	@searchTerms NVARCHAR(MAX)
AS
BEGIN
	DECLARE @startTime DATETIME2;
	DECLARE @endTime DATETIME2;
	DECLARE @TotalTimeOfLoop INT;
	DECLARE @TotalTimeOfJoin INT;

	--Performance of Looping Stored Procedure--
	SET @startTime = SYSDATETIME();
	EXEC [dbo].[SearchForBookLoop] @searchTerms;
	SET @endTime = SYSDATETIME();
	SET @TotalTimeOfLoop = DATEDIFF(MILLISECOND, @startTime, @endTime) ;

	--Performance of Joins Stored Procedure--
	SET @startTime = SYSDATETIME();
	EXEC [dbo].[SearchForBook] @searchTerms;
	SET @endTime = SYSDATETIME();
	SET @TotalTimeOfJoin = DATEDIFF(MILLISECOND, @startTime, @endTime);

	SELECT 'SearchForBookLoop' AS Procedurename, @TotalTimeOfLoop AS ExecutionTime
	UNION ALL
	SELECT 'SearchForBook' AS Procedurename, @TotalTimeOfJoin AS ExecutionTime;

END
RETURN 0
