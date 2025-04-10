using PayXpert.Context;
using PayXpert.Models;
using PayXpert.Services;
using System;
using Microsoft.Data.SqlClient;

namespace PayXpert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to PayXpert Console Application!");

            // Initialize services
            EmployeeService employeeService = new EmployeeService();
            PayrollService payrollService = new PayrollService();
            TaxService taxService = new TaxService();
            FinancialRecordService financialRecordService = new FinancialRecordService();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. Employee Management");
                Console.WriteLine("2. Payroll Management");
                Console.WriteLine("3. Tax Management");
                Console.WriteLine("4. Financial Record Management");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EmployeeManagement(employeeService);
                        break;

                    case "2":
                        PayrollManagement(payrollService);
                        break;

                    case "3":
                        TaxManagement(taxService);
                        break;

                    case "4":
                        FinancialRecordManagement(financialRecordService);
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void EmployeeManagement(EmployeeService employeeService)
        {
            Console.WriteLine("\n--- Employee Management ---");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View All Employees");
            Console.WriteLine("3. Get Employee by ID");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Remove Employee");
            Console.WriteLine("6. Back to Main Menu");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmployee(employeeService);
                    break;

                case "2":
                    ViewAllEmployees(employeeService);
                    break;

                case "3":
                    GetEmployeeById(employeeService);
                    break;

                case "4":
                    UpdateEmployee(employeeService);
                    break;

                case "5":
                    RemoveEmployee(employeeService);
                    break;

                case "6":
                    Console.WriteLine("Returning to main menu...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void AddEmployee(EmployeeService employeeService)
        {
            Console.WriteLine("\n--- Add Employee ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Gender (Male/Female): ");
            string gender = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Position: ");
            string position = Console.ReadLine();

            Console.Write("Enter Joining Date (yyyy-MM-dd): ");
            DateTime joiningDate = DateTime.Parse(Console.ReadLine());

            var employee = new Employee
            {
                EmployeeID = employeeID,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                Position = position,
                JoiningDate = joiningDate
            };

            employeeService.AddEmployee(employee);
            Console.WriteLine("Employee added successfully.");
        }

        static void ViewAllEmployees(EmployeeService employeeService)
        {
            Console.WriteLine("\n--- All Employees ---");
            var employees = employeeService.GetAllEmployees();

            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
                return;
            }

            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.EmployeeID}, Name: {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
            }
        }

        static void GetEmployeeById(EmployeeService employeeService)
        {
            Console.WriteLine("\n--- Get Employee by ID ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            var employee = employeeService.GetEmployeeById(employeeID);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.WriteLine($"ID: {employee.EmployeeID}, Name: {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
        }

        static void UpdateEmployee(EmployeeService employeeService)
        {
            Console.WriteLine("\n--- Update Employee ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            var employee = employeeService.GetEmployeeById(employeeID);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.Write("Enter New First Name (leave blank to keep current): ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrEmpty(firstName)) employee.FirstName = firstName;

            Console.Write("Enter New Last Name (leave blank to keep current): ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(lastName)) employee.LastName = lastName;

            Console.Write("Enter New Position (leave blank to keep current): ");
            string position = Console.ReadLine();
            if (!string.IsNullOrEmpty(position)) employee.Position = position;

            employeeService.UpdateEmployee(employee);
            Console.WriteLine("Employee updated successfully.");
        }

        static void RemoveEmployee(EmployeeService employeeService)
        {
            Console.WriteLine("\n--- Remove Employee ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            employeeService.RemoveEmployee(employeeID);
            Console.WriteLine("Employee removed successfully.");
        }

        static void PayrollManagement(PayrollService payrollService)
        {
            Console.WriteLine("\n--- Payroll Management ---");
            Console.WriteLine("1. Generate Payroll");
            Console.WriteLine("2. Get Payroll by ID");
            Console.WriteLine("3. Get Payrolls for Employee");
            Console.WriteLine("4. Get Payrolls for Period");
            Console.WriteLine("5. Back to Main Menu");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GeneratePayroll(payrollService);
                    break;

                case "2":
                    GetPayrollById(payrollService);
                    break;

                case "3":
                    GetPayrollsForEmployee(payrollService);
                    break;

                case "4":
                    GetPayrollsForPeriod(payrollService);
                    break;

                case "5":
                    Console.WriteLine("Returning to main menu...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void GeneratePayroll(PayrollService payrollService)
        {
            Console.WriteLine("\n--- Generate Payroll ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter End Date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            payrollService.GeneratePayroll(employeeID, startDate, endDate);
            Console.WriteLine("Payroll generated successfully.");
        }

        static void GetPayrollById(PayrollService payrollService)
        {
            Console.WriteLine("\n--- Get Payroll by ID ---");
            Console.Write("Enter Payroll ID: ");
            int payrollID = int.Parse(Console.ReadLine());

            var payroll = payrollService.GetPayrollById(payrollID);

            if (payroll == null)
            {
                Console.WriteLine("Payroll not found.");
                return;
            }

            Console.WriteLine($"Payroll ID: {payroll.PayrollID}, Employee ID: {payroll.EmployeeID}, Net Salary: {payroll.NetSalary}");
        }

        static void GetPayrollsForEmployee(PayrollService payrollService)
        {
            Console.WriteLine("\n--- Get Payrolls for Employee ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            var payrolls = payrollService.GetPayrollsForEmployee(employeeID);

            if (payrolls.Count == 0)
            {
                Console.WriteLine("No payrolls found for this employee.");
                return;
            }

            foreach (var payroll in payrolls)
            {
                Console.WriteLine($"Payroll ID: {payroll.PayrollID}, Net Salary: {payroll.NetSalary}");
            }
        }

        static void GetPayrollsForPeriod(PayrollService payrollService)
        {
            Console.WriteLine("\n--- Get Payrolls for Period ---");
            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter End Date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            var payrolls = payrollService.GetPayrollsForPeriod(startDate, endDate);

            if (payrolls.Count == 0)
            {
                Console.WriteLine("No payrolls found for this period.");
                return;
            }

            foreach (var payroll in payrolls)
            {
                Console.WriteLine($"Payroll ID: {payroll.PayrollID}, Net Salary: {payroll.NetSalary}");
            }
        }

        static void TaxManagement(TaxService taxService)
        {
            Console.WriteLine("\n--- Tax Management ---");
            Console.WriteLine("1. Calculate Tax");
            Console.WriteLine("2. Get Tax by ID");
            Console.WriteLine("3. Get Taxes for Employee");
            Console.WriteLine("4. Get Taxes for Year");
            Console.WriteLine("5. Back to Main Menu");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CalculateTax(taxService);
                    break;

                case "2":
                    GetTaxById(taxService);
                    break;

                case "3":
                    GetTaxesForEmployee(taxService);
                    break;

                case "4":
                    GetTaxesForYear(taxService);
                    break;

                case "5":
                    Console.WriteLine("Returning to main menu...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void CalculateTax(TaxService taxService)
        {
            Console.WriteLine("\n--- Calculate Tax ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            Console.Write("Enter Tax Year: ");
            int taxYear = int.Parse(Console.ReadLine());

            taxService.CalculateTax(employeeID, taxYear);
            Console.WriteLine("Tax calculated successfully.");
        }

        static void GetTaxById(TaxService taxService)
        {
            Console.WriteLine("\n--- Get Tax by ID ---");
            Console.Write("Enter Tax ID: ");
            int taxID = int.Parse(Console.ReadLine());

            var tax = taxService.GetTaxById(taxID);

            if (tax == null)
            {
                Console.WriteLine("Tax not found.");
                return;
            }

            Console.WriteLine($"Tax ID: {tax.TaxID}, Employee ID: {tax.EmployeeID}, Tax Amount: {tax.TaxAmount}");
        }

        static void GetTaxesForEmployee(TaxService taxService)
        {
            Console.WriteLine("\n--- Get Taxes for Employee ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            var taxes = taxService.GetTaxesForEmployee(employeeID);

            if (taxes.Count == 0)
            {
                Console.WriteLine("No taxes found for this employee.");
                return;
            }

            foreach (var tax in taxes)
            {
                Console.WriteLine($"Tax ID: {tax.TaxID}, Tax Amount: {tax.TaxAmount}");
            }
        }

        static void GetTaxesForYear(TaxService taxService)
        {
            Console.WriteLine("\n--- Get Taxes for Year ---");
            Console.Write("Enter Tax Year: ");
            int taxYear = int.Parse(Console.ReadLine());

            var taxes = taxService.GetTaxesForYear(taxYear);

            if (taxes.Count == 0)
            {
                Console.WriteLine("No taxes found for this year.");
                return;
            }

            foreach (var tax in taxes)
            {
                Console.WriteLine($"Tax ID: {tax.TaxID}, Tax Amount: {tax.TaxAmount}");
            }
        }

        static void FinancialRecordManagement(FinancialRecordService financialRecordService)
        {
            Console.WriteLine("\n--- Financial Record Management ---");
            Console.WriteLine("1. Add Financial Record");
            Console.WriteLine("2. Get Financial Record by ID");
            Console.WriteLine("3. Get Financial Records for Employee");
            Console.WriteLine("4. Get Financial Records for Date");
            Console.WriteLine("5. Back to Main Menu");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddFinancialRecord(financialRecordService);
                    break;

                case "2":
                    GetFinancialRecordById(financialRecordService);
                    break;

                case "3":
                    GetFinancialRecordsForEmployee(financialRecordService);
                    break;

                case "4":
                    GetFinancialRecordsForDate(financialRecordService);
                    break;

                case "5":
                    Console.WriteLine("Returning to main menu...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void AddFinancialRecord(FinancialRecordService financialRecordService)
        {
            Console.WriteLine("\n--- Add Financial Record ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Record Type (e.g., Credit/Debit): ");
            string recordType = Console.ReadLine();

            financialRecordService.AddFinancialRecord(employeeID, description, amount, recordType);
            Console.WriteLine("Financial record added successfully.");
        }

        static void GetFinancialRecordById(FinancialRecordService financialRecordService)
        {
            Console.WriteLine("\n--- Get Financial Record by ID ---");
            Console.Write("Enter Record ID: ");
            int recordID = int.Parse(Console.ReadLine());

            var record = financialRecordService.GetFinancialRecordById(recordID);

            if (record == null)
            {
                Console.WriteLine("Financial record not found.");
                return;
            }

            Console.WriteLine($"Record ID: {record.RecordID}, Description: {record.Description}, Amount: {record.Amount}");
        }

        static void GetFinancialRecordsForEmployee(FinancialRecordService financialRecordService)
        {
            Console.WriteLine("\n--- Get Financial Records for Employee ---");
            Console.Write("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            var records = financialRecordService.GetFinancialRecordsForEmployee(employeeID);

            if (records.Count == 0)
            {
                Console.WriteLine("No financial records found for this employee.");
                return;
            }

            foreach (var record in records)
            {
                Console.WriteLine($"Record ID: {record.RecordID}, Description: {record.Description}, Amount: {record.Amount}");
            }
        }

        static void GetFinancialRecordsForDate(FinancialRecordService financialRecordService)
        {
            Console.WriteLine("\n--- Get Financial Records for Date ---");
            Console.Write("Enter Record Date (yyyy-MM-dd): ");
            DateTime recordDate = DateTime.Parse(Console.ReadLine());

            var records = financialRecordService.GetFinancialRecordsForDate(recordDate);

            if (records.Count == 0)
            {
                Console.WriteLine("No financial records found for this date.");
                return;
            }

            foreach (var record in records)
            {
                Console.WriteLine($"Record ID: {record.RecordID}, Description: {record.Description}, Amount: {record.Amount}");
            }
        }
    }
}