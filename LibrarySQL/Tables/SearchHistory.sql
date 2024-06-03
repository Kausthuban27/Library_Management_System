CREATE TABLE [dbo].[SearchHistory]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [BookName] NVARCHAR(100) NULL, 
    [BookAuthor] NVARCHAR(100) NULL, 
    [BookPublisher] NVARCHAR(100) NULL,
    [Category] NVARCHAR(100) NULL
)

