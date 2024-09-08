CREATE DATABASE PasswordManagerDB;
GO
USE PasswordManagerDB;
GO
CREATE TABLE Passwords (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Category NVARCHAR(50) NULL,
    App NVARCHAR(100) NOT NULL,
    UserName NVARCHAR(100) NOT NULL,
    EncryptedPassword NVARCHAR(256) NOT NULL
);
GO
INSERT INTO Passwords (Category, App, UserName, EncryptedPassword)
VALUES ('work', 'outlook', 'testuser@mytest.com', 'TXlQYXNzd29yZEAxMjM=');

INSERT INTO Passwords (Category, App, UserName, EncryptedPassword)
VALUES ('school', 'messenger', 'testuser@mytest.com', 'TmV3UGFzc3dvcmRAMTIz');

