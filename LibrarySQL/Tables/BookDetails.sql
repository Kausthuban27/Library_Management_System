CREATE TABLE [dbo].[BookDetails]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Bookname] NVARCHAR(100) NOT NULL, 
    [BookAuthor] NVARCHAR(100) NOT NULL, 
    [BookPublisher] NVARCHAR(100) NOT NULL, 
    [Category] NVARCHAR(100) NOT NULL,
)
