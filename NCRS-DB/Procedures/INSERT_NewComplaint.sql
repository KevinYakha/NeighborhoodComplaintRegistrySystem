CREATE PROCEDURE [dbo].[INSERT_NewComplaint]
	@Issuer UNIQUEIDENTIFIER,
	@Location INT,
	@Description VARCHAR(MAX),
	@Status INT,
	@Category INT
AS
	INSERT INTO Complaint
	(Id, Issuer, Location, CreationDate, Description, Status, Category)
	OUTPUT Inserted.Id
	VALUES
	(NEWID(),
	(SELECT Id from Tenant WHERE Id = @Issuer),
	(SELECT Nr FROM Apartment WHERE Nr = @Location),
	SYSUTCDATETIME(), 
	@Description,
	@Status,
	@Category)
