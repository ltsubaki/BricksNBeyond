--
-- File generated with SQLiteStudio v3.4.4 on Wed Apr 10 19:45:12 2024
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: __EFMigrationsHistory
CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20240405000456_Auth', '8.0.3');

-- Table: AspNetRoleClaims
CREATE TABLE "AspNetRoleClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY AUTOINCREMENT,
    "RoleId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

-- Table: AspNetRoles
CREATE TABLE "AspNetRoles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetRoles" PRIMARY KEY,
    "Name" TEXT NULL,
    "NormalizedName" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL
);

-- Table: AspNetUserClaims
CREATE TABLE "AspNetUserClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY AUTOINCREMENT,
    "UserId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

-- Table: AspNetUserLogins
CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
INSERT INTO AspNetUserLogins (LoginProvider, ProviderKey, ProviderDisplayName, UserId) VALUES ('Google', '111486366490597640517', 'Google', 'c1df01eb-b479-4043-8329-f1142a0688c6');

-- Table: AspNetUserRoles
CREATE TABLE "AspNetUserRoles" (
    "UserId" TEXT NOT NULL,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

-- Table: AspNetUsers
CREATE TABLE "AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "UserName" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "Email" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "AccessFailedCount" INTEGER NOT NULL
);
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES ('72ba7fef-ce6c-4c66-984b-fca4729e9184', 'ltsubaki@byu.edu', 'LTSUBAKI@BYU.EDU', 'ltsubaki@byu.edu', 'LTSUBAKI@BYU.EDU', 1, 'AQAAAAIAAYagAAAAEOSzBn457s8r6dLLfHSxmYiJq7XrEy6D8qZPpGEDK9cq5ZjFe0OAPJ3GT1+mODeBPA==', 'VKVLFDKF2K4PPM7IJ6MO6KPXRB4ZM5T6', 'eb1ec811-54fd-49d6-ab7e-c19f68dde955', NULL, 0, 0, NULL, 1, 0);
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES ('d2cfcf41-3c68-44b1-a850-5253315425ed', 'lilianrtsubaki@gmail.com', 'LILIANRTSUBAKI@GMAIL.COM', 'lilianrtsubaki@gmail.com', 'LILIANRTSUBAKI@GMAIL.COM', 1, 'AQAAAAIAAYagAAAAEJX6R5Vm4wKOqKzV4e4NoLNVZsNH8SHyOlSydPxlhGcS3xbMfKQj+XScNKJ2F0/Irg==', 'PBYTWSOGYDLG4Q6CX5AZX3QPGFBLEKFW', '4cae03bb-9e67-420d-a416-7cd9c8a2d7e3', NULL, 0, 1, '2024-04-10 06:19:58.4204365+00:00', 1, 0);
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES ('c1df01eb-b479-4043-8329-f1142a0688c6', 'laurlinatamyfourteen@gmail.com', 'LAURLINATAMYFOURTEEN@GMAIL.COM', 'laurlinatamyfourteen@gmail.com', 'LAURLINATAMYFOURTEEN@GMAIL.COM', 1, NULL, 'DFCHTGHIWVM36YMKW234HFNKOGZNZN5K', 'a6cbac08-b4e5-44de-9984-91b708fa447b', NULL, 0, 0, NULL, 1, 0);

-- Table: AspNetUserTokens
CREATE TABLE "AspNetUserTokens" (
    "UserId" TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
INSERT INTO AspNetUserTokens (UserId, LoginProvider, Name, Value) VALUES ('d2cfcf41-3c68-44b1-a850-5253315425ed', '[AspNetUserStore]', 'AuthenticatorKey', 'PC67X2ZXDSQV7B5CMRALWYY2VMBX7YSV');
INSERT INTO AspNetUserTokens (UserId, LoginProvider, Name, Value) VALUES ('d2cfcf41-3c68-44b1-a850-5253315425ed', '[AspNetUserStore]', 'RecoveryCodes', 'CY7T7-GT3M3;9T74H-BQX2C;3Q9PG-MXN6N;VWRFC-P8QQP;CKYDG-2MTKF;H64D4-NGKJN;TTHFT-FTRCT;7WBT2-4X8Q3;39TYN-TY454;MKN7G-TMT39');

-- Index: EmailIndex
CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

-- Index: IX_AspNetRoleClaims_RoleId
CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

-- Index: IX_AspNetUserClaims_UserId
CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

-- Index: IX_AspNetUserLogins_UserId
CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

-- Index: IX_AspNetUserRoles_RoleId
CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

-- Index: RoleNameIndex
CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");

-- Index: UserNameIndex
CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
