
-- Employee Table
CREATE TABLE Employee (
    EmployeeID INT PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    Position NVARCHAR(50) NOT NULL,
    JoiningDate DATE NOT NULL,
    TerminationDate DATE NULL
);

-- Payroll Table
CREATE TABLE Payroll (
    PayrollID INT PRIMARY KEY,
    EmployeeID INT NOT NULL,
    PayPeriodStartDate DATE NOT NULL,
    PayPeriodEndDate DATE NOT NULL,
    BasicSalary DECIMAL(18, 2) NOT NULL,
    OvertimePay DECIMAL(18, 2) NOT NULL,
    Deductions DECIMAL(18, 2) NOT NULL,
    NetSalary AS (BasicSalary + OvertimePay - Deductions) PERSISTED -- Computed column
);

-- Tax Table
CREATE TABLE Tax (
    TaxID INT PRIMARY KEY,
    EmployeeID INT NOT NULL,
    TaxYear INT NOT NULL,
    TaxableIncome DECIMAL(18, 2) NOT NULL,
    TaxAmount DECIMAL(18, 2) NOT NULL
);

-- FinancialRecord Table
CREATE TABLE FinancialRecord (
    RecordID INT PRIMARY KEY,
    EmployeeID INT NOT NULL,
    RecordDate DATE NOT NULL,
    Description NVARCHAR(200) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    RecordType NVARCHAR(50) NOT NULL
);

-- Step 3: Insert Sample Data

-- Insert into Employee Table
INSERT INTO dbo.Employee (EmployeeID, FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate)
VALUES
(1, 'Sabareesh', 'Kumar', '1990-05-12', 'Male', 'sabareesh.kumar@example.com', '91 9876543210', '123 Chennai, Tamil Nadu', 'Developer', '2022-02-15'),
(2, 'Santhosh', 'Ramesh', '1985-08-20', 'Male', 'santhosh.ramesh@example.com', '91 9786543210', '456 Coimbatore, Tamil Nadu', 'Manager', '2021-09-01'),
(3, 'Kabilan', 'Suresh', '1992-11-08', 'Male', 'kabilan.suresh@example.com', '91 9987654321', '789 Madurai, Tamil Nadu', 'Analyst', '2023-03-10'),
(4, 'Giri', 'Venkat', '1987-03-15', 'Male', 'giri.venkat@example.com', '91 9678901234', '321 Trichy, Tamil Nadu', 'Team Lead', '2020-06-20');

-- Insert into Payroll Table
INSERT INTO dbo.Payroll (PayrollID, EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions)
VALUES
(1, 1, '2023-04-01', '2023-04-30', 60000.00, 2000.00, 5000.00),
(2, 2, '2023-04-01', '2023-04-30', 75000.00, 0.00, 6000.00),
(3, 3, '2023-04-01', '2023-04-30', 50000.00, 1500.00, 4500.00),
(4, 4, '2023-04-01', '2023-04-30', 80000.00, 1000.00, 7000.00);

-- Insert into Tax Table
INSERT INTO dbo.Tax (TaxID, EmployeeID, TaxYear, TaxableIncome, TaxAmount)
VALUES
(1, 1, 2023, 700000.00, 140000.00),
(2, 2, 2023, 850000.00, 170000.00),
(3, 3, 2023, 600000.00, 120000.00),
(4, 4, 2023, 900000.00, 180000.00);

-- Insert into FinancialRecord Table
INSERT INTO dbo.FinancialRecord (RecordID, EmployeeID, RecordDate, Description, Amount, RecordType)
VALUES
(1, 1, '2023-04-15', 'Salary Payment', 57000.00, 'Credit'),
(2, 2, '2023-04-15', 'Salary Payment', 69000.00, 'Credit'),
(3, 3, '2023-04-15', 'Salary Payment', 47000.00, 'Credit'),
(4, 4, '2023-04-15', 'Salary Payment', 74000.00, 'Credit'),
(5, 1, '2023-04-20', 'Bonus', 5000.00, 'Credit'),
(6, 2, '2023-04-20', 'Loan Repayment', -2000.00, 'Debit');