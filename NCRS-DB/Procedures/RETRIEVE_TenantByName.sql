CREATE PROCEDURE [dbo].[RETRIEVE_TenantByName]
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50)
AS
	SELECT Id, FirstName, LastName, ApartmentNr FROM Tenant
	WHERE FirstName = @FirstName AND LastName = @LastName
