CREATE TABLE [dbo].[Tenant]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [ApartmentNr] INT NOT NULL, 
    CONSTRAINT [FK_Tenant_Apartment] FOREIGN KEY ([ApartmentNr]) REFERENCES [Apartment]([Nr])
)
