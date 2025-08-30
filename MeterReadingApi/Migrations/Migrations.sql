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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250829155423_Initial'
)
BEGIN
    CREATE TABLE [Accounts] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250829155423_Initial'
)
BEGIN
    CREATE TABLE [MeterReadings] (
        [Id] int NOT NULL IDENTITY,
        [AccountId] int NOT NULL,
        [MeterReadingDateTime] datetime2 NOT NULL,
        [MeterReadValue] int NOT NULL,
        CONSTRAINT [PK_MeterReadings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MeterReadings_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250829155423_Initial'
)
BEGIN
    CREATE INDEX [IX_MeterReadings_AccountId] ON [MeterReadings] ([AccountId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250829155423_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250829155423_Initial', N'9.0.8');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250830085303_Add Account and Date Index to MeterReading'
)
BEGIN
    DROP INDEX [IX_MeterReadings_AccountId] ON [MeterReadings];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250830085303_Add Account and Date Index to MeterReading'
)
BEGIN
    CREATE UNIQUE INDEX [IX_MeterReadings_AccountId_MeterReadingDateTime] ON [MeterReadings] ([AccountId], [MeterReadingDateTime]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250830085303_Add Account and Date Index to MeterReading'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250830085303_Add Account and Date Index to MeterReading', N'9.0.8');
END;

COMMIT;
GO

