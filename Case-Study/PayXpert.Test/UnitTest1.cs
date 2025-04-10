using NUnit.Framework;
using PayXpert.Models;
using PayXpert.Services;
using System;
using System.Collections.Generic;

namespace PayXpert.Tests
{
    [TestFixture]
    public class PayrollSystemTests
    {
        private EmployeeService _employeeService;
        private PayrollService _payrollService;
        private TaxService _taxService;
        private FinancialRecordService _financialRecordService;

        [SetUp]
        public void Setup()
        {
            // Initialize services for testing
            _employeeService = new EmployeeService();
            _payrollService = new PayrollService();
            _taxService = new TaxService();
            _financialRecordService = new FinancialRecordService();
        }

        #region EmployeeService Tests

        [Test]
        public void AddEmployee_ShouldAddEmployeeSuccessfully()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeID = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 5, 15),
                Gender = "Male",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Street, City",
                Position = "Developer",
                JoiningDate = new DateTime(2022, 1, 1)
            };

            // Act
            _employeeService.AddEmployee(employee);

            // Assert
            var addedEmployee = _employeeService.GetEmployeeById(1);
            Assert.IsNotNull(addedEmployee);
            Assert.AreEqual("John", addedEmployee.FirstName);
            Assert.AreEqual("Doe", addedEmployee.LastName);
        }

        [Test]
        public void GetAllEmployees_ShouldReturnAllEmployees()
        {
            // Arrange
            var employee1 = new Employee
            {
                EmployeeID = 2,
                FirstName = "Jane",
                LastName = "Doe",
                DateOfBirth = new DateTime(1992, 8, 20),
                Gender = "Female",
                Email = "jane.doe@example.com",
                PhoneNumber = "0987654321",
                Address = "456 Avenue, City",
                Position = "Manager",
                JoiningDate = new DateTime(2021, 6, 1)
            };
            _employeeService.AddEmployee(employee1);

            // Act
            var employees = _employeeService.GetAllEmployees();

            // Assert
            Assert.IsNotEmpty(employees);
            Assert.IsTrue(employees.Exists(e => e.EmployeeID == 2));
        }

        [Test]
        public void GetEmployeeById_ShouldReturnNullForInvalidID()
        {
            // Arrange
            int invalidEmployeeID = 9999;

            // Act
            var employee = _employeeService.GetEmployeeById(invalidEmployeeID);

            // Assert
            Assert.IsNull(employee);
        }

        [Test]
        public void UpdateEmployee_ShouldUpdateEmployeeSuccessfully()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeID = 3,
                FirstName = "Alice",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 3, 10),
                Gender = "Female",
                Email = "alice.smith@example.com",
                PhoneNumber = "5555555555",
                Address = "789 Lane, City",
                Position = "Analyst",
                JoiningDate = new DateTime(2020, 2, 1)
            };
            _employeeService.AddEmployee(employee);

            // Act
            employee.Position = "Senior Analyst";
            _employeeService.UpdateEmployee(employee);

            // Assert
            var updatedEmployee = _employeeService.GetEmployeeById(3);
            Assert.AreEqual("Senior Analyst", updatedEmployee.Position);
        }

        [Test]
        public void RemoveEmployee_ShouldRemoveEmployeeSuccessfully()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeID = 4,
                FirstName = "Bob",
                LastName = "Brown",
                DateOfBirth = new DateTime(1988, 7, 25),
                Gender = "Male",
                Email = "bob.brown@example.com",
                PhoneNumber = "1111111111",
                Address = "321 Road, City",
                Position = "Engineer",
                JoiningDate = new DateTime(2019, 5, 1)
            };
            _employeeService.AddEmployee(employee);

            // Act
            _employeeService.RemoveEmployee(4);
            var removedEmployee = _employeeService.GetEmployeeById(4);

            // Assert
            Assert.IsNull(removedEmployee);
        }

        #endregion

        #region PayrollService Tests

        [Test]
        public void GeneratePayroll_ShouldThrowNotImplementedException()
        {
            // Arrange
            int employeeID = 1;
            DateTime startDate = new DateTime(2023, 4, 1);
            DateTime endDate = new DateTime(2023, 4, 30);

            // Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _payrollService.GeneratePayroll(employeeID, startDate, endDate);
            });
        }

        [Test]
        public void GetPayrollById_ShouldReturnNullForInvalidID()
        {
            // Arrange
            int invalidPayrollID = 9999;

            // Act
            var payroll = _payrollService.GetPayrollById(invalidPayrollID);

            // Assert
            Assert.IsNull(payroll);
        }

        [Test]
        public void GetPayrollsForEmployee_ShouldReturnEmptyListForNoRecords()
        {
            // Arrange
            int employeeID = 9999;

            // Act
            var payrolls = _payrollService.GetPayrollsForEmployee(employeeID);

            // Assert
            Assert.IsEmpty(payrolls);
        }

        [Test]
        public void GetPayrollsForPeriod_ShouldReturnEmptyListForNoRecords()
        {
            // Arrange
            DateTime startDate = new DateTime(2023, 1, 1);
            DateTime endDate = new DateTime(2023, 12, 31);

            // Act
            var payrolls = _payrollService.GetPayrollsForPeriod(startDate, endDate);

            // Assert
            Assert.IsEmpty(payrolls);
        }

        #endregion

        #region TaxService Tests

        [Test]
        public void CalculateTax_ShouldThrowNotImplementedException()
        {
            // Arrange
            int employeeID = 1;
            int taxYear = 2023;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _taxService.CalculateTax(employeeID, taxYear);
            });
        }

        [Test]
        public void GetTaxById_ShouldReturnNullForInvalidID()
        {
            // Arrange
            int invalidTaxID = 9999;

            // Act
            var tax = _taxService.GetTaxById(invalidTaxID);

            // Assert
            Assert.IsNull(tax);
        }

        [Test]
        public void GetTaxesForEmployee_ShouldReturnEmptyListForNoRecords()
        {
            // Arrange
            int employeeID = 9999;

            // Act
            var taxes = _taxService.GetTaxesForEmployee(employeeID);

            // Assert
            Assert.IsEmpty(taxes);
        }

        [Test]
        public void GetTaxesForYear_ShouldReturnEmptyListForNoRecords()
        {
            // Arrange
            int taxYear = 2023;

            // Act
            var taxes = _taxService.GetTaxesForYear(taxYear);

            // Assert
            Assert.IsEmpty(taxes);
        }

        #endregion

        #region FinancialRecordService Tests

        [Test]
        public void AddFinancialRecord_ShouldAddRecordSuccessfully()
        {
            // Arrange
            int employeeID = 1;
            string description = "Bonus Payment";
            decimal amount = 5000;
            string recordType = "Credit";

            // Act
            _financialRecordService.AddFinancialRecord(employeeID, description, amount, recordType);

            // Assert
            var records = _financialRecordService.GetFinancialRecordsForEmployee(employeeID);
            Assert.IsNotEmpty(records);
            Assert.IsTrue(records.Exists(r => r.Description == description && r.Amount == amount));
        }

        [Test]
        public void GetFinancialRecordById_ShouldReturnNullForInvalidID()
        {
            // Arrange
            int invalidRecordID = 9999;

            // Act
            var record = _financialRecordService.GetFinancialRecordById(invalidRecordID);

            // Assert
            Assert.IsNull(record);
        }

        [Test]
        public void GetFinancialRecordsForEmployee_ShouldReturnEmptyListForNoRecords()
        {
            // Arrange
            int employeeID = 9999;

            // Act
            var records = _financialRecordService.GetFinancialRecordsForEmployee(employeeID);

            // Assert
            Assert.IsEmpty(records);
        }

        [Test]
        public void GetFinancialRecordsForDate_ShouldReturnEmptyListForNoRecords()
        {
            // Arrange
            DateTime recordDate = new DateTime(2023, 1, 1);

            // Act
            var records = _financialRecordService.GetFinancialRecordsForDate(recordDate);

            // Assert
            Assert.IsEmpty(records);
        }

        #endregion
    }
}