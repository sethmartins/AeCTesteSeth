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

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [Nome] int NOT NULL,
    [Usuario_] nvarchar(max) NOT NULL,
    [Senha] nvarchar(10) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Enderecos] (
    [Id] int NOT NULL IDENTITY,
    [Cep] nvarchar(max) NOT NULL,
    [Logradouro] int NOT NULL,
    [Complemento] nvarchar(max) NOT NULL,
    [Bairro] nvarchar(max) NOT NULL,
    [Cidade] nvarchar(max) NOT NULL,
    [Uf] nvarchar(max) NOT NULL,
    [Numero] nvarchar(max) NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Enderecos_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Enderecos_UsuarioId] ON [Enderecos] ([UsuarioId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240612041357_TesteAeC', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'Usuario_');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Usuarios] ALTER COLUMN [Usuario_] nvarchar(100) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'Nome');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Usuarios] ALTER COLUMN [Nome] nvarchar(50) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Uf');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Uf] nvarchar(2) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Numero');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Numero] nvarchar(10) NOT NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Complemento');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Complemento] nvarchar(50) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Cidade');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Cidade] nvarchar(50) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Cep');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Cep] nvarchar(10) NOT NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Bairro');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Bairro] nvarchar(50) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240613024657_TrocaTipoNomedoUsuario', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Logradouro');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Logradouro] nvarchar(100) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240613230025_mudatipologradouro', N'8.0.6');
GO

COMMIT;
GO

