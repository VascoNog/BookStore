BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130195406_AddBooks'
)
BEGIN
    CREATE TABLE [Books] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Isbn] nvarchar(max) NULL,
        [Category] nvarchar(max) NULL,
        [Publisher] nvarchar(max) NULL,
        [PublishedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Books] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130195406_AddBooks'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250130195406_AddBooks', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130203635_DataAnotations'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'Title');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var + '];');
    EXEC(N'UPDATE [Books] SET [Title] = N'''' WHERE [Title] IS NULL');
    ALTER TABLE [Books] ALTER COLUMN [Title] nvarchar(255) NOT NULL;
    ALTER TABLE [Books] ADD DEFAULT N'' FOR [Title];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130203635_DataAnotations'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'Isbn');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var1 + '];');
    EXEC(N'UPDATE [Books] SET [Isbn] = N'''' WHERE [Isbn] IS NULL');
    ALTER TABLE [Books] ALTER COLUMN [Isbn] nvarchar(20) NOT NULL;
    ALTER TABLE [Books] ADD DEFAULT N'' FOR [Isbn];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130203635_DataAnotations'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250130203635_DataAnotations', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130204306_ISBNVARCHAR'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'Isbn');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Books] ALTER COLUMN [Isbn] VARCHAR(20) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250130204306_ISBNVARCHAR'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250130204306_ISBNVARCHAR', N'9.0.1');
END;

COMMIT;
GO

