-- ==========================================
-- CREATE DATABASE
-- ==========================================
CREATE DATABASE EmployeePayrollDB;
USE EmployeePayrollDB;

-- ==========================================
-- CREATE Employee TABLE
-- ==========================================
CREATE TABLE Employee (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10) CHECK (Gender IN ('Male', 'Female', 'Other')),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(15) UNIQUE NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Position NVARCHAR(50) NOT NULL,
    JoiningDate DATE NOT NULL,
    TerminationDate DATE NULL
);

-- ==========================================
-- CREATE Payroll TABLE
-- ==========================================
CREATE TABLE Payroll (
    PayrollID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT NOT NULL,
    PayPeriodStartDate DATE NOT NULL,
    PayPeriodEndDate DATE NOT NULL,
    BasicSalary DECIMAL(10,2) NOT NULL CHECK (BasicSalary >= 0),
    OvertimePay DECIMAL(10,2) DEFAULT 0 CHECK (OvertimePay >= 0),
    Deductions DECIMAL(10,2) DEFAULT 0 CHECK (Deductions >= 0),
    NetSalary AS (BasicSalary + OvertimePay - Deductions) PERSISTED, -- Computed Column
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ON DELETE CASCADE
);

-- ==========================================
-- CREATE Tax TABLE
-- ==========================================
CREATE TABLE Tax (
    TaxID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT NOT NULL,
    TaxYear INT NOT NULL CHECK (TaxYear >= 2000),
    TaxableIncome DECIMAL(10,2) NOT NULL CHECK (TaxableIncome >= 0),
    TaxAmount DECIMAL(10,2) NOT NULL CHECK (TaxAmount >= 0),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ON DELETE CASCADE
);

-- ==========================================
-- CREATE FinancialRecord TABLE
-- ==========================================
CREATE TABLE FinancialRecord (
    RecordID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT NOT NULL,
    RecordDate DATE NOT NULL DEFAULT GETDATE(),
    Description NVARCHAR(255) NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    RecordType NVARCHAR(50) CHECK (RecordType IN ('Income', 'Expense', 'Tax Payment')),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ON DELETE CASCADE
);

