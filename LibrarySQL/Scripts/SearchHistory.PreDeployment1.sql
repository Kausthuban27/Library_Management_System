/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

ALTER TABLE [dbo].[SearchHistory]
ADD CONSTRAINT FK_SearchHistory_BookName FOREIGN KEY (BookName)
REFERENCES [dbo].[BookDetails](BookName);

ALTER TABLE [dbo].[SearchHistory]
ADD CONSTRAINT FK_SearchHistory_BookAuthor FOREIGN KEY (BookAuthor)
REFERENCES [dbo].[BookDetails](BookAuthor);

ALTER TABLE [dbo].[SearchHistory]
ADD CONSTRAINT FK_SearchHistory_BookPublisher FOREIGN KEY (BookPublisher)
REFERENCES [dbo].[BookDetails](BookPublisher);

ALTER TABLE [dbo].[SearchHistory]
ADD CONSTRAINT FK_SearchHistory_Category FOREIGN KEY (Category)
REFERENCES [dbo].[BookDetails](Category);