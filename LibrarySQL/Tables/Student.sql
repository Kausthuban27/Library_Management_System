CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Lastname] NVARCHAR(10) NOT NULL, 
    [Username] NVARCHAR(20) NOT NULL, 
    [Department] NVARCHAR(10) NOT NULL, 
    [Year] INT NOT NULL
)
