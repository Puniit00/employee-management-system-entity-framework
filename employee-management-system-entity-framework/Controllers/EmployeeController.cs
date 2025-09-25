using EmployeeManagementSystemEntityFramework.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

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

        [Route("Employees")]
        [HttpGet]
        public async Task<ActionResult<Employees>> GetAllEmployees()
        {
            IEnumerable<Employees> employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [Route("Employee/{id}")]
        [HttpGet]
        public async Task<ActionResult<Employees>> GetEmployeeById(int id)
        {
            Employees? employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [Route("AddEmployee")]
        [HttpPost]
        public async Task<ActionResult<Employees>> AddEmployee(Employees employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [Route("UpdateEmployee/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(int id, Employees employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [Route("DeleteEmployee/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            Employees? employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return NoContent();
        }

        [Route("GetNumberofEmployeeRecordsPerGoal")]
        [HttpGet]
        public async Task<IActionResult> GetNumberofEmployeeRecordsPerGoalAsync()
        {
            var result = await (from g in _context.Goals
                                .AsNoTracking()
                               join e in _context.Employees.AsNoTracking() on g.EmployeeId equals e.Id
                               group g by new { g.EmployeeId, e.Name } into grp
                               select new
                               {
                                   grp.Key.EmployeeId,
                                   EmployeeName = grp.Key.Name,
                                   GoalCount = grp.Count()
                               }).ToListAsync();
            return Ok(result);
        }
    }
}
