-- Create Database MyStoreDB
-- Run this script in SQL Server Management Studio

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MyStoreDB')
BEGIN
    CREATE DATABASE MyStoreDB;
END
GO

USE MyStoreDB;
GO

-- Create AccountMember table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AccountMember' AND xtype='U')
BEGIN
    CREATE TABLE AccountMember (
        MemberID INT PRIMARY KEY IDENTITY(1,1),
        MemberPassword VARCHAR(255) NOT NULL,
        FullName VARCHAR(100),
        EmailAddress VARCHAR(100),
        MemberRole VARCHAR(50)
    );
END
GO

-- Create Categories table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' AND xtype='U')
BEGIN
    CREATE TABLE Categories (
        CategoryID INT PRIMARY KEY IDENTITY(1,1),
        CategoryName VARCHAR(100) NOT NULL
    );
END
GO

-- Create Products table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' AND xtype='U')
BEGIN
    CREATE TABLE Products (
        ProductID INT PRIMARY KEY IDENTITY(1,1),
        ProductName VARCHAR(100) NOT NULL,
        CategoryID INT,
        UnitsInStock INT,
        UnitPrice DECIMAL(10, 2),
        FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
    );
END
GO

-- Seed data for AccountMember
INSERT INTO AccountMember (MemberPassword, FullName, EmailAddress, MemberRole)
VALUES 
    ('admin123', 'Admin User', 'admin@mystore.com', 'Admin'),
    ('user123', 'Regular User', 'user@mystore.com', 'User');
GO

-- Seed data for Categories
INSERT INTO Categories (CategoryName)
VALUES 
    ('Electronics'),
    ('Clothing'),
    ('Books'),
    ('Home & Garden');
GO

-- Seed data for Products
INSERT INTO Products (ProductName, CategoryID, UnitsInStock, UnitPrice)
VALUES 
    ('Laptop Dell XPS 13', 1, 10, 1299.99),
    ('iPhone 15 Pro', 1, 25, 999.99),
    ('Samsung TV 55"', 1, 15, 799.99),
    ('T-Shirt Cotton', 2, 100, 29.99),
    ('Jeans Levis 501', 2, 50, 79.99),
    ('Python Programming Book', 3, 30, 45.99),
    ('Clean Code Book', 3, 20, 39.99),
    ('Garden Chair Set', 4, 8, 199.99);
GO

PRINT 'Database MyStoreDB created and seeded successfully!';
