CREATE PROCEDURE [dbo].[UPDATE_Complaint]
	@Id UNIQUEIDENTIFIER,
	@Description NVARCHAR(500),
	@Status INT
AS
	UPDATE Complaint
	SET Description = @Description, Status = @Status
	WHERE Id = @Id
