using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace employee_management_system_entity_framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeGoalsController(ApplicationDbContext context) : ControllerBase
    {
        [Route("EmployeeGoals")]
        [HttpGet]
        public async Task<ActionResult<Goals>> GetEmployeeGoalsById(int id)
        {
            IEnumerable<Goals> goals = await (from g in context.Goals
                                             join e in context.Employees
                                             on g.EmployeeId equals e.Id
                                             where g.EmployeeId == id
                                             select g)
                                 .ToListAsync();
            return Ok(goals);

        }

        [Route("AddEmployee")]
        [HttpPost]
        public async Task<ActionResult<bool>> AddEmployee(Goals goal)
        {
            int? employeeId = await context.Employees.Where(e=>e.Id == goal.EmployeeId).Select(e=>e.Id).FirstOrDefaultAsync();

            if (employeeId != null)
            {
                await context.Goals.AddAsync(goal);
                context.SaveChanges();
                return Ok(true);
            }

            return Ok(false);
        }
    }
}
