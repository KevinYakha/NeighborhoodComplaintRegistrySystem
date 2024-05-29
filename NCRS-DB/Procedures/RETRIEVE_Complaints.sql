CREATE PROCEDURE [dbo].[RETRIEVE_Complaints]
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint