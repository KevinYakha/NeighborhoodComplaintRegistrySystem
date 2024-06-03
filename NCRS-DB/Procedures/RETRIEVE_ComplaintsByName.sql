CREATE PROCEDURE [dbo].[RETRIEVE_ComplaintsByName]
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50)
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint
	WHERE Issuer = (SELECT TOP (1) Id FROM Tenant WHERE FirstName = @FirstName AND LastName = @LastName)
