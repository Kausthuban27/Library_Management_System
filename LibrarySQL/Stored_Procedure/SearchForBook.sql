CREATE PROCEDURE [dbo].[SearchForBook]
	@searchTerms NVARCHAR(MAX)
AS
BEGIN
		DECLARE @sql NVARCHAR(MAX);
		DECLARE @terms TABLE (Term NVARCHAR(MAX));

		INSERT INTO @terms (Term)
		SELECT LTRIM(RTRIM(value)) FROM string_split(@searchTerms, ',');

		SET @sql = 'SELECT * FROM BookDetails WHERE 1=0';

		DECLARE @term NVARCHAR(MAX);
		DECLARE item CURSOR FOR SELECT Term FROM @terms;
		open item;

		FETCH next FROM item into @term
		WHILE @@FETCH_STATUS = 0
		BEGIN
				SET @sql = @sql + ' OR (Bookname LIKE ''%' + @term + '%'' OR BookAuthor LIKE ''%' + @term + '%'' OR BookPublisher LIKE ''%' + @term + '%'' OR Category LIKE ''%' + @term + '%'')';
				FETCH next FROM item into @term
		END;

		CLOSE item;
		DEALLOCATE item;

		EXEC (@sql);
END
RETURN 0
