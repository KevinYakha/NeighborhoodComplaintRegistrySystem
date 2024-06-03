CREATE TABLE [dbo].[Complaint]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Issuer] UNIQUEIDENTIFIER NOT NULL, 
    [Location] INT NOT NULL, 
    [CreationDate] DATETIME NOT NULL, 
    [Description] NVARCHAR(500) NOT NULL, 
    [Status] INT NOT NULL, 
    [Category] INT NOT NULL, 
    CONSTRAINT [FK_Complaint_Issuer] FOREIGN KEY ([Issuer]) REFERENCES [Tenant]([Id]), 
    CONSTRAINT [FK_Complaint_Location] FOREIGN KEY ([Location]) REFERENCES [Apartment]([Nr])
)
