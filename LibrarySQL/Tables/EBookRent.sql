CREATE TABLE [dbo].[EBookRent]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Bookname] NCHAR(10) NOT NULL, 
    [IsRented] NCHAR(10) NOT NULL, 
    [Category] NVARCHAR(100) NOT NULL,
    [RentAmount] NCHAR(10) NOT NULL
)
