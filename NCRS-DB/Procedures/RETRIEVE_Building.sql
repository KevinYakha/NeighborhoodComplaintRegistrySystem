CREATE PROCEDURE [dbo].[RETRIEVE_Building]
	@Nr INT
AS
	SELECT Nr, Address FROM Building
	WHERE Nr = @Nr