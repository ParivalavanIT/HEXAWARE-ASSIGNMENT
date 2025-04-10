using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Models
{
    public class Employee
    {
        private int employeeID;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string gender;
        private string email;
        private string phoneNumber;
        private string address;
        private string position;
        private DateTime joiningDate;
        private DateTime? terminationDate;

        public Employee() { }

        public Employee(int employeeID, string firstName, string lastName, DateTime dateOfBirth, string gender,
                        string email, string phoneNumber, string address, string position, DateTime joiningDate,
                        DateTime? terminationDate = null)
        {
            this.employeeID = employeeID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.position = position;
            this.joiningDate = joiningDate;
            this.terminationDate = terminationDate;
        }

        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Address { get => address; set => address = value; }
        public string Position { get => position; set => position = value; }
        public DateTime JoiningDate { get => joiningDate; set => joiningDate = value; }
        public DateTime? TerminationDate { get => terminationDate; set => terminationDate = value; }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
