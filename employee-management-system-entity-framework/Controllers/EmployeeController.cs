using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace employee_management_system_entity_framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
