-- Create the QuoteLibrary database if it does not exist
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'QuoteLibrary')
BEGIN
    CREATE DATABASE QuoteLibrary;
END

-- Switch to the QuoteLibrary database
USE QuoteLibrary;

-- Create the Types table
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'TypesQuotes') AND type in (N'U'))
BEGIN
    CREATE TABLE TypesQuotes (
        Id INT IDENTITY(1,1),                -- Auto-increment primary key
        Name NVARCHAR(50) NOT NULL,         -- Name of the type (Movie, Series, etc.)
		CreationDate SMALLDATETIME NOT NULL,      -- Date and time the TypesQuotes was created
        ModificationDate SMALLDATETIME NULL,      -- Date and time the TypesQuotes was last modified
        CONSTRAINT PK_Types PRIMARY KEY (Id) -- Primary key constraint with name
    );
END

-- Create the Authors table
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'Authors') AND type in (N'U'))
BEGIN
    CREATE TABLE Authors (
        Id INT IDENTITY(1,1),               -- Auto-increment primary key
        Name NVARCHAR(100) NOT NULL,        -- Name of the author
        BirthDate DATE NULL,                -- Birthdate of the author (optional)
        IdNationality INT NULL,				-- Nationality of the author (optional)
        PhotoUrl NVARCHAR(255) NULL,        -- URL or path of the author's photo (optional)
		CreationDate SMALLDATETIME NOT NULL,      -- Date and time the Authors was created
        ModificationDate SMALLDATETIME NULL,      -- Date and time the Authors was last modified
        CONSTRAINT PK_Authors PRIMARY KEY (Id) -- Primary key constraint with name
    );
END

-- Create the Quotes table
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'Quotes') AND type in (N'U'))
BEGIN
    CREATE TABLE Quotes (
        Id INT IDENTITY(1,1),                -- Auto-increment primary key
        Text NVARCHAR(500) NOT NULL,         -- Field to store the quote
        AuthorId INT NULL,                   -- Foreign key to be added later
        TypeId INT NULL,                     -- Foreign key to be added later
        CreationDate SMALLDATETIME NOT NULL,      -- Date and time the quote was created
        ModificationDate SMALLDATETIME NULL,      -- Date and time the quote was last modified
        CONSTRAINT PK_Quotes PRIMARY KEY (Id) -- Primary key constraint with name
    );
END

-- Add foreign key constraint for AuthorId in the Quotes table referencing the Authors table
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Quotes_Authors') AND parent_object_id = OBJECT_ID(N'Quotes'))
BEGIN
    ALTER TABLE Quotes
    ADD CONSTRAINT FK_Quotes_Authors FOREIGN KEY (AuthorId) REFERENCES Authors(Id)
    ON DELETE SET NULL;  -- If an author is deleted, set AuthorId in Quotes to NULL
END

-- Add foreign key constraint for TypeId in the Quotes table referencing the Types table
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Quotes_TypesQuotes') AND parent_object_id = OBJECT_ID(N'Quotes'))
BEGIN
    ALTER TABLE Quotes
    ADD CONSTRAINT FK_Quotes_TypesQuotes FOREIGN KEY (TypeId) REFERENCES TypesQuotes(Id)
    ON DELETE SET NULL;  -- If a type is deleted, set TypeId in Quotes to NULL
END

