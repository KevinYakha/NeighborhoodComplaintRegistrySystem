CREATE PROCEDURE [dbo].[RETRIEVE_ComplaintsByDate]
	@Date DATETIME
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint
	WHERE CAST(CreationDate AS DATE) = @Date
