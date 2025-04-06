-- Insert the 20 main nationalities into the Nationalities table with validation
-- **************************************************************************************
-- Nationalities
-- **************************************************************************************
USE QuoteLibrary;
GO

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'American')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('American');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'British')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('British');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Canadian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Canadian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Chinese')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Chinese');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'French')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('French');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'German')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('German');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Indian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Indian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Italian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Italian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Japanese')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Japanese');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Mexican')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Mexican');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Russian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Russian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'South Korean')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('South Korean');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Brazilian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Brazilian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Argentinian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Argentinian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Spanish')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Spanish');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Australian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Australian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Egyptian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Egyptian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'South African')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('South African');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Nigerian')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Nigerian');
END

IF NOT EXISTS (SELECT 1 FROM Nationalities WHERE Name = 'Turkish')
BEGIN
    INSERT INTO Nationalities (Name) VALUES ('Turkish');
END
