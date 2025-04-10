
using PayXpert.Models;

namespace PayXpert.Interfaces
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int employeeID);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void RemoveEmployee(int employeeID);
    }
}