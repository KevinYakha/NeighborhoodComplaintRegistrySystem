CREATE TABLE [dbo].[Apartment]
(
	[Nr] INT NOT NULL PRIMARY KEY IDENTITY(100, 1), 
    [BuildingNr] INT NOT NULL, 
    CONSTRAINT [FK_Apartment_Building] FOREIGN KEY ([BuildingNr]) REFERENCES [Building]([Nr])
)
