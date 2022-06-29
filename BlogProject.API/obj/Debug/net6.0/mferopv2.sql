IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [OnModified] datetime2 NOT NULL,
    [OnModifiedUsername] nvarchar(max) NULL,
    [PhotoUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    [Email] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [ActivatedGuid] uniqueidentifier NOT NULL,
    [IsAdmin] bit NOT NULL,
    [OnCreated] datetime2 NOT NULL,
    [OnModified] datetime2 NOT NULL,
    [ModifiedUsername] nvarchar(max) NULL,
    [Photourl] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Notes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Text] nvarchar(max) NULL,
    [LikeCount] int NOT NULL,
    [OnCreated] datetime2 NOT NULL,
    [OnModified] datetime2 NOT NULL,
    [OnModifiedUsername] nvarchar(max) NULL,
    [UserId] int NULL,
    [CategoryId] int NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notes_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Notes_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NULL,
    [OnCreated] datetime2 NOT NULL,
    [OnModified] datetime2 NOT NULL,
    [OnModifiedUsername] nvarchar(max) NULL,
    [UserId] int NULL,
    [NoteId] int NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Notes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Likes] (
    [Id] int NOT NULL IDENTITY,
    [OnCreated] datetime2 NOT NULL,
    [OnModifiedUsername] nvarchar(max) NULL,
    [UserId] int NULL,
    [NoteId] int NULL,
    CONSTRAINT [PK_Likes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Likes_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Notes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Likes_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Photos] (
    [Id] int NOT NULL IDENTITY,
    [PhotoUrl] nvarchar(max) NULL,
    [OnCreated] datetime2 NOT NULL,
    [OnModified] datetime2 NOT NULL,
    [OnModifiedUsername] nvarchar(max) NULL,
    [NoteId] int NULL,
    CONSTRAINT [PK_Photos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Photos_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Notes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Comments_NoteId] ON [Comments] ([NoteId]);
GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
GO

CREATE INDEX [IX_Likes_NoteId] ON [Likes] ([NoteId]);
GO

CREATE INDEX [IX_Likes_UserId] ON [Likes] ([UserId]);
GO

CREATE INDEX [IX_Notes_CategoryId] ON [Notes] ([CategoryId]);
GO

CREATE INDEX [IX_Notes_UserId] ON [Notes] ([UserId]);
GO

CREATE INDEX [IX_Photos_NoteId] ON [Photos] ([NoteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190215090040_MyFirstMigration', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Photos] ADD [PublicId] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190304084555_publicidforphoto', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Photos]') AND [c].[name] = N'PublicId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Photos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Photos] ALTER COLUMN [PublicId] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190304104525_publicidforphoto2', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Categories] ADD [PublicId] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190327090352_category-photo', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Notes] ADD [Description] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190614083402_NoteDescription', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Notes] ADD [IsDraft] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190619083911_note-isdraft', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [Description] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190621114308_user-description', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Tags] (
    [Id] int NOT NULL IDENTITY,
    [OnCreated] datetime2 NOT NULL,
    [OnModifiedUsername] nvarchar(max) NULL,
    [Tags] nvarchar(max) NULL,
    [NoteId] int NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tags_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Notes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Tags_NoteId] ON [Tags] ([NoteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190701121457_TagTable', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190723145104_azureMigration', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Notes] ADD [MainPhotourl] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190725130217_mainphotofornote', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Categories].[Name]', N'Categoryname', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729125602_Categoryname', N'6.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220321212107_CreatingIdentityTables', N'6.0.3');
GO

COMMIT;
GO

