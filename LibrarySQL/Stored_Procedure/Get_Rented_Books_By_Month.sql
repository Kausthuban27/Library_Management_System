CREATE PROCEDURE [dbo].[Get_Rented_Books_By_Month]
	@param1 DateTime
AS
	SELECT * FROM EBookRent WHERE MONTH(IssuedOn) = MONTH(@param1) AND YEAR(IssuedOn) = YEAR(@param1);
RETURN 0
