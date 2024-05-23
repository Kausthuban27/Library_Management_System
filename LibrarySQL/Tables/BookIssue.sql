CREATE TABLE [dbo].[BookIssue]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Bookname] NVARCHAR(100) NOT NULL, 
    [BookAuthor] NVARCHAR(100) NOT NULL, 
    [BookPublisher] NVARCHAR(100) NOT NULL, 
    [Category] NVARCHAR(100) NOT NULL, 
    [IssueDate] DATETIME2 NOT NULL,

)
