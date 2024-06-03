CREATE TABLE [dbo].[Tenant]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [ApartmentNr] INT NOT NULL, 
    CONSTRAINT [FK_Tenant_Apartment] FOREIGN KEY ([ApartmentNr]) REFERENCES [Apartment]([Nr])
)
