using PrismMVVM.Models;

namespace PrismMVVM.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IList<Employee> GetAllEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "John", LastName = "Doe", Age = 28, Salary = 1500, IsManager = false },
                new Employee {FirstName = "Paolo", LastName = "Rossi", Age = 45, Salary = 2500, IsManager = true },
                new Employee {FirstName = "Mark", LastName = "Serth", Age = 21, Salary = 1000, IsManager = false },
            };
        }
    }
}
