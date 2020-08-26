IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [article] (
    [ArticleId] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NULL,
    CONSTRAINT [PK_article] PRIMARY KEY ([ArticleId])
);

GO

CREATE TABLE [tags] (
    [TagId] nvarchar(20) NOT NULL,
    [Content] ntext NULL,
    CONSTRAINT [PK_tags] PRIMARY KEY ([TagId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200826085744_InitWebDB', N'3.1.7');

GO

ALTER TABLE [article] ADD [Content] ntext NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200826091437_InitWebDB_V1', N'3.1.7');

GO

CREATE TABLE [articletag] (
    [ArticleTagId] int NOT NULL IDENTITY,
    [ArticleId] int NOT NULL,
    [TagId] nvarchar(20) NULL,
    CONSTRAINT [PK_articletag] PRIMARY KEY ([ArticleTagId]),
    CONSTRAINT [FK_articletag_article_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [article] ([ArticleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_articletag_tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [tags] ([TagId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_articletag_TagId] ON [articletag] ([TagId]);

GO

CREATE UNIQUE INDEX [IX_articletag_ArticleId_TagId] ON [articletag] ([ArticleId], [TagId]) WHERE [TagId] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200826092431_InitWebDB_V2', N'3.1.7');

GO

