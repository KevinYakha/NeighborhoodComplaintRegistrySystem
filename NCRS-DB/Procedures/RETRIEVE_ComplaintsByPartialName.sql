CREATE PROCEDURE [dbo].[RETRIEVE_ComplaintsByPartialName]
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50)
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint
	WHERE Issuer = (SELECT TOP (1) Id FROM Tenant
		WHERE LOWER(FirstName) LIKE '%' + LOWER(@FirstName) + '%'
			AND LOWER(LastName) LIKE '%' + LOWER(@LastName) + '%')
