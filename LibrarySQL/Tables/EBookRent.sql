CREATE TABLE [dbo].[EBookRent]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Bookname] NVARCHAR(100) NOT NULL, 
    [IsRented] BIT NOT NULL, 
    [Category] NVARCHAR(100) NOT NULL,
    [RentAmount] DECIMAL(5) NOT NULL,
    [IssuedOn] DATETIME2(6) NOT NULL,
    [Username] NVARCHAR(20) NOT NULL

    CONSTRAINT [Fk_Username] FOREIGN KEY (Username) REFERENCES Student(Username)
)
