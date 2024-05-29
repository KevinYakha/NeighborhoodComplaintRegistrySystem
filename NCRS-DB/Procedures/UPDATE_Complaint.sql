CREATE PROCEDURE [dbo].[UPDATE_Complaint]
	@Id UNIQUEIDENTIFIER,
	@Description VARCHAR(MAX),
	@Status INT
AS
	UPDATE Complaint
	SET Description = @Description, Status = @Status
	WHERE Id = @Id
