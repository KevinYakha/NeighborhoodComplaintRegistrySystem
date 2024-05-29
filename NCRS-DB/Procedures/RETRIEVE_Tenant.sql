CREATE PROCEDURE [dbo].[RETRIEVE_Tenant]
	@Id UNIQUEIDENTIFIER
AS
	SELECT Id, FirstName, LastName, ApartmentNr FROM Tenant
	WHERE Id = @Id
