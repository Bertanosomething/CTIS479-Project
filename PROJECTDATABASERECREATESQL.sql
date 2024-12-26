ALTER TABLE [dbo].[BookGenres] DROP CONSTRAINT [FK_BookGenres_Books];
ALTER TABLE [dbo].[Books] DROP CONSTRAINT [FK_Books_Authors];
ALTER TABLE [dbo].[Books] DROP CONSTRAINT [FK_Books_Publishers];

-- Drop the existing BookGenres table
DROP TABLE IF EXISTS [dbo].[BookGenres];

-- Drop the Books table
DROP TABLE IF EXISTS [dbo].[Books];

-- Drop the Genres table if needed
DROP TABLE IF EXISTS [dbo].[Genres];

-- Recreate the Books table
CREATE TABLE [dbo].[Books] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)   NOT NULL,
    [Price]       DECIMAL (10, 2) NULL,
    [Quantity]    INT             NULL,
    [AuthorId]    INT             NOT NULL,
    [PublisherId] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Books_Authors] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([Id]),
    CONSTRAINT [FK_Books_Publishers] FOREIGN KEY ([PublisherId]) REFERENCES [dbo].[Publishers] ([Id]),
    CHECK ([Quantity]>=(0))
);

-- Recreate the Genres table
CREATE TABLE [dbo].[Genres] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Recreate the BookGenres table with the new ID column
CREATE TABLE [dbo].[BookGenres] (
    [Id]        INT IDENTITY(1,1) NOT NULL, -- Add the Id column as primary key
    [BookId]    INT NOT NULL,
    [GenreId]   INT NOT NULL,
    CONSTRAINT [PK_BookGenres] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BookGenres_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookGenres_Genres] FOREIGN KEY ([GenreId]) REFERENCES [dbo].[Genres] ([Id]) ON DELETE CASCADE
);