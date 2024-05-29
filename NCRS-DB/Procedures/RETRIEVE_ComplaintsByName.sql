CREATE PROCEDURE [dbo].[RETRIEVE_ComplaintsByName]
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50)
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint
	WHERE Issuer = (SELECT TOP (1) Id FROM Tenant WHERE FirstName = @FirstName AND LastName = @LastName)
