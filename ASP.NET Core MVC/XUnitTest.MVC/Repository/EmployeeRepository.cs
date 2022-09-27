using XUnitTest.MVC.Models;

namespace XUnitTest.MVC.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees?.ToList() ?? new();
        }

        public Employee GetEmployee(Guid id)
        {
            return _context.Employees?.SingleOrDefault(e => e.Id.Equals(id)) ?? default!;
        }

        public void CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
        }
    }
}