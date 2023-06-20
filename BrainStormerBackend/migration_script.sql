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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620141734_afterazure')
BEGIN
    CREATE TABLE [Projects] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [ProjectDescription] nvarchar(max) NOT NULL,
        [Visibility] bit NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620141734_afterazure')
BEGIN
    CREATE TABLE [Issues] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [ProjectId] int NOT NULL,
        CONSTRAINT [PK_Issues] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Issues_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620141734_afterazure')
BEGIN
    CREATE TABLE [BrainStorms] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Visibility] bit NOT NULL,
        [IssueId] int NOT NULL,
        [IsChosen] bit NOT NULL,
        CONSTRAINT [PK_BrainStorms] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BrainStorms_Issues_IssueId] FOREIGN KEY ([IssueId]) REFERENCES [Issues] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620141734_afterazure')
BEGIN
    CREATE TABLE [ActionSteps] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NOT NULL,
        [BrainStormId] int NOT NULL,
        CONSTRAINT [PK_ActionSteps] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ActionSteps_BrainStorms_BrainStormId] FOREIGN KEY ([BrainStormId]) REFERENCES [BrainStorms] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620141734_afterazure')
BEGIN
    CREATE INDEX [IX_ActionSteps_BrainStormId] ON [ActionSteps] ([BrainStormId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620141734_afterazure')
BEGIN
    CREATE INDEX [IX_BrainStorms_IssueId] ON [BrainStorms] ([IssueId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620141734_afterazure')
BEGIN
    CREATE INDEX [IX_Issues_ProjectId] ON [Issues] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620141734_afterazure')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230620141734_afterazure', N'7.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230620143241_mssql.azure_migration_196')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230620143241_mssql.azure_migration_196', N'7.0.5');
END;
GO

COMMIT;
GO

