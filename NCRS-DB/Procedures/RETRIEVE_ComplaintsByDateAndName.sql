CREATE PROCEDURE [dbo].[RETRIEVE_ComplaintsByDateAndName]
	@Date DATETIME,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50)
AS
	SELECT Id, Issuer, Location, CreationDate, Description, Status, Category FROM Complaint
	WHERE CAST(CreationDate AS DATE) = @Date AND
	Issuer = (SELECT Id FROM Tenant WHERE FirstName = @FirstName AND LastName = @LastName)
