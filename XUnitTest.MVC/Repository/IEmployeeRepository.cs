using XUnitTest.MVC.Models;

namespace XUnitTest.MVC.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetEmployee(Guid id);
        void CreateEmployee(Employee employee);
    }
}