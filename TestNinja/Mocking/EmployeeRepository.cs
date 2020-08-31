namespace TestNinja.Mocking
{
    public interface IEmployeeRepository
    {
        void RemoveEmployee(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _db;
        public EmployeeRepository(EmployeeContext db)
        {
            _db = new EmployeeContext();
        }

        public void RemoveEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee ==  null) return;
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}