using WebApplication1.Model;

namespace EmployeeManagementSystemEntityFramework.Model
{
    public class Employees
    {
        public int Id { get; set; }  // EF will make this the PK automatically
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}