CREATE PROCEDURE [dbo].[RETRIEVE_Complaint]
	@Id UNIQUEIDENTIFIER
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category
	FROM Complaint WHERE Id = @Id
RETURN 0
