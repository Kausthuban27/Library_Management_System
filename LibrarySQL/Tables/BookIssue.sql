CREATE TABLE [dbo].[BookIssue]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Bookname] NVARCHAR(100) NOT NULL, 
    [BookAuthor] NVARCHAR(100) NOT NULL, 
    [BookPublisher] NVARCHAR(100) NOT NULL, 
    [Category] NVARCHAR(100) NOT NULL, 
    [IssueDate] DATETIME2 NOT NULL, 
    [Username] NVARCHAR(20) NOT NULL

    CONSTRAINT [Fk_BookIssue_Username] FOREIGN KEY (Username) REFERENCES Student(Username)
)
