/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/




USE [NCRS-DB];
--------------------------------------------------------------------------------------
-- Building Table
INSERT INTO [dbo].[Building] (Address) VALUES ('123 Maple Street');
INSERT INTO [dbo].[Building] (Address) VALUES ('456 Oak Avenue');
INSERT INTO [dbo].[Building] (Address) VALUES ('789 Pine Lane');
INSERT INTO [dbo].[Building] (Address) VALUES ('101 Elm Boulevard');
INSERT INTO [dbo].[Building] (Address) VALUES ('202 Cedar Drive');
INSERT INTO [dbo].[Building] (Address) VALUES ('303 Birch Road');




--------------------------------------------------------------------------------------
-- Apartment Table
-- Building 1
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (1);

-- Building 2
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (2);

-- Building 3
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (3);

-- Building 4
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (4);

-- Building 5
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (5);

-- Building 6
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);
INSERT INTO [dbo].[Apartment] (BuildingNr) VALUES (6);




--------------------------------------------------------------------------------------
-- Tenant Table
-- Building 1, Apartments 101-104
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'John', 'Doe', 100);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Jane', 'Smith', 100);
-- Apartment 102 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Emily', 'Johnson', 101);
-- Apartment 104 is empty

-- Building 1, Apartments 201-204
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Michael', 'Brown', 103);
-- Apartment 202 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Jessica', 'Davis', 104);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Chris', 'Wilson', 104);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Amanda', 'Garcia', 106);

-- Building 1, Apartments 301-304
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'David', 'Martinez', 107);
-- Apartment 302 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Laura', 'Hernandez', 108);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'James', 'Lopez', 110);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Sarah', 'Gonzalez', 111);

-- Building 2, Apartments 101-104
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Brian', 'Clark', 112);
-- Apartment 102 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Nancy', 'Lewis', 113);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Steve', 'Walker', 115);
-- Apartment 104 is empty

-- Building 2, Apartments 201-204
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Anna', 'Hall', 116);
-- Apartment 202 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Linda', 'Allen', 118);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Kevin', 'Young', 119);
-- Apartment 204 is empty

-- Building 2, Apartments 301-304
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Sandra', 'King', 121);
-- Apartment 302 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Gary', 'Wright', 123);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Lisa', 'Scott', 124);
-- Apartment 304 is empty

-- Building 3, Apartments 101-104
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Mark', 'Green', 125);
-- Apartment 102 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Cynthia', 'Baker', 127);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Paul', 'Adams', 129);
-- Apartment 104 is empty

-- Building 3, Apartments 201-204
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Diana', 'Nelson', 130);
-- Apartment 202 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Adam', 'Carter', 132);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Rebecca', 'Mitchell', 134);
-- Apartment 204 is empty

-- Building 3, Apartments 301-304
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'George', 'Perez', 135);
-- Apartment 302 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Deborah', 'Roberts', 136);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Ronald', 'Turner', 138);
-- Apartment 304 is empty

-- Building 4, Apartments 101-104
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Karen', 'Phillips', 140);
-- Apartment 102 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Edward', 'Campbell', 142);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Patricia', 'Parker', 143);
-- Apartment 104 is empty

-- Building 4, Apartments 201-204
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Betty', 'Evans', 145);
-- Apartment 202 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Frank', 'Edwards', 146);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Alice', 'Collins', 148);
-- Apartment 204 is empty

-- Building 4, Apartments 301-304
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Henry', 'Stewart', 150);
-- Apartment 302 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Marie', 'Sanchez', 152);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Charles', 'Morris', 154);
-- Apartment 304 is empty

-- Building 5, Apartments 101-104
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Angela', 'Rogers', 155);
-- Apartment 102 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Raymond', 'Reed', 156);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Megan', 'Cruz', 158);
-- Apartment 104 is empty

-- Building 5, Apartments 201-204
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'John', 'Ross', 159);
-- Apartment 202 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Lisa', 'Sanders', 161);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'James', 'Collins', 162);
-- Apartment 204 is empty

-- Building 5, Apartments 301-304
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Jennifer', 'Price', 164);
-- Apartment 302 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Karen', 'Bailey', 165);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Donald', 'Murphy', 167);
-- Apartment 304 is empty

-- Building 6, Apartments 101-104
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Daniel', 'Rivera', 168);
-- Apartment 102 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Matthew', 'Kim', 170);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Emily', 'Hughes', 171);
-- Apartment 104 is empty

-- Building 6, Apartments 201-204
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Brian', 'Patterson', 172);
-- Apartment 202 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Laura', 'Peterson', 174);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Chris', 'Rogers', 175);
-- Apartment 204 is empty

-- Building 6, Apartments 301-304
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Paul', 'Cox', 176);
-- Apartment 302 is empty
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Sarah', 'Howard', 178);
INSERT INTO [dbo].[Tenant] (Id, FirstName, LastName, ApartmentNr) VALUES (NEWID(), 'Steven', 'Ward', 179);
-- Apartment 304 is empty




--------------------------------------------------------------------------------------
-- Complaint Table
-- Complaint 1: Noise
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='John' AND LastName='Doe'), 100, GETDATE(), 'Loud music during the night', 1, 0);

-- Complaint 2: TenantConflict
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Nancy' AND LastName='Lewis'), 102, GETDATE(), 'Dispute over shared spaces', 0, 1);

-- Complaint 3: MaintenanceRequest
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Emily' AND LastName='Johnson'), 101, GETDATE(), 'Broken faucet in the kitchen', 1, 2);

-- Complaint 4: SafetyConcerns
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Amanda' AND LastName='Garcia'), 107, GETDATE(), 'Lack of proper lighting in the parking area', 2, 3);

-- Complaint 5: PrivacyViolation
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Michael' AND LastName='Brown'), 103, GETDATE(), 'Unauthorized entry into apartment', 1, 4);

-- Complaint 6: RecyclingViolation
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Jessica' AND LastName='Davis'), 101, GETDATE(), 'Incorrect recycling practices by a neighbor', 0, 5);

-- Complaint 7: ParkingViolation
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Chris' AND LastName='Wilson'), 104, GETDATE(), 'Blocked parking space', 1, 6);

-- Complaint 8: PetPolicy
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='David' AND LastName='Martinez'), 108, GETDATE(), 'Pets not on leash in common areas', 2, 7);

-- Complaint 9: PestIssues
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Laura' AND LastName='Hernandez'), 108, GETDATE(), 'Presence of cockroaches in the apartment', 1, 8);

-- Complaint 10: TenantConflict
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='James' AND LastName='Lopez'), 111, GETDATE(), 'Argument over noise levels', 0, 1);

-- Complaint 11: Noise
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Sarah' AND LastName='Gonzalez'), 111, GETDATE(), 'Frequent loud parties', 1, 0);

-- Complaint 12: MaintenanceRequest
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Brian' AND LastName='Clark'), 112, GETDATE(), 'Heating system not working', 1, 2);

-- Complaint 13: PrivacyViolation
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Linda' AND LastName='Allen'), 118, GETDATE(), 'Neighbor peeping into windows', 1, 4);

-- Complaint 14: PetPolicy
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Sandra' AND LastName='King'), 121, GETDATE(), 'Excessive barking from pets', 2, 7);

-- Complaint 15: SafetyConcerns
INSERT INTO [dbo].[Complaint] (Id, Issuer, Location, CreationDate, Description, Status, Category) VALUES 
(NEWID(), (SELECT TOP (1) Id FROM [dbo].[Tenant] WHERE FirstName='Gary' AND LastName='Wright'), 123, GETDATE(), 'Broken street lights outside building', 0, 3);

