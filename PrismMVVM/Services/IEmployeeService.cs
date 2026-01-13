using PrismMVVM.Models;

namespace PrismMVVM.Services
{
    public interface IEmployeeService
    {
        IList<Employee> GetAllEmployees();
    }
}
