CREATE TABLE [dbo].[ReturnBook]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Bookname] NVARCHAR(100) NOT NULL, 
    [IsFineApplicable] BIT NOT NULL, 
    [FineAmount] DECIMAL(4,2) NOT NULL
)
