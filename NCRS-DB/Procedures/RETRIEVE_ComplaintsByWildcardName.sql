CREATE PROCEDURE [dbo].[RETRIEVE_ComplaintsByWildcardName]
	@Name NVARCHAR(50)
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint
	WHERE Issuer = (SELECT TOP (1) Id FROM Tenant
		WHERE LOWER(FirstName) LIKE '%' + LOWER(@Name) + '%'
			OR LOWER(LastName) LIKE '%' + LOWER(@Name) + '%')
