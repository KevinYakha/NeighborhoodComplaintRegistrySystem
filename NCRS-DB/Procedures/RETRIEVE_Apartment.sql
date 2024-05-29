CREATE PROCEDURE [dbo].[RETRIEVE_Apartment]
	@Nr INT
AS
	SELECT Nr, BuildingNr FROM Apartment
	WHERE Nr = @Nr
