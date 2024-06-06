CREATE PROCEDURE [dbo].[RETRIEVE_ComplaintsByDateRange]
	@DateFrom DATETIME,
	@DateTo DATETIME
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint
	WHERE CAST(CreationDate AS DATE) BETWEEN @DateFrom AND @DateTo
