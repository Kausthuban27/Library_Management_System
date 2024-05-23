CREATE TABLE [dbo].[Librarian]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Lastname] NVARCHAR(50) NOT NULL, 
    [Username] NVARCHAR(20) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL,
    [Phone] NVARCHAR(10) NOT NULL,
    [DateOfJoining] DATETIME2(6) NOT NULL,
    CONSTRAINT CK_Librarian CHECK (LEN(Phone) = 10 AND Phone NOT LIKE '%[^0-9]%')
)
