CREATE PROCEDURE [dbo].[RETRIEVE_ComplaintsByDateRangeAndName]
	@DateFrom DATETIME,
	@DateTo DATETIME,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50)
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint
	WHERE CAST(CreationDate AS DATE) BETWEEN @DateFrom AND @DateTo AND
	Issuer = (SELECT Id FROM Tenant WHERE FirstName = @FirstName AND LastName = @LastName)
