CREATE PROCEDURE [dbo].[RETRIEVE_Tenants]
AS
	SELECT Id, FirstName, LastName, ApartmentNr FROM Tenant
