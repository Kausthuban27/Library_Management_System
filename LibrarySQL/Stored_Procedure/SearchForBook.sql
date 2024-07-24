CREATE PROCEDURE [dbo].[SearchForBook]
	@searchTerms NVARCHAR(MAX)
AS
BEGIN
		DECLARE @terms TABLE (Term NVARCHAR(MAX));

        INSERT INTO @terms (Term)
        SELECT LTRIM(RTRIM(value)) 
        FROM string_split(@searchTerms, ',');

        SELECT DISTINCT bd.*
        FROM BookDetails bd
        INNER JOIN @terms t
        ON bd.Bookname LIKE '%' + t.Term + '%'
           OR bd.BookAuthor LIKE '%' + t.Term + '%'
           OR bd.BookPublisher LIKE '%' + t.Term + '%'
           OR bd.Category LIKE '%' + t.Term + '%';
END
RETURN 0
