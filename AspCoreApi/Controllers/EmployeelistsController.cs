using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspCoreApi.Models;

namespace AspCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeelistsController : ControllerBase
    {
        private readonly employeeContext _context;

        public EmployeelistsController(employeeContext context)
        {
            _context = context;
        }

        // GET: api/Employeelists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employeelist>>> GetEmployeelists()
        {
            return await _context.Employeelists.ToListAsync();
        }

        // GET: api/Employeelists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employeelist>> GetEmployeelist(int id)
        {
            var employeelist = await _context.Employeelists.FindAsync(id);

            if (employeelist == null)
            {
                return NotFound();
            }

            return employeelist;
        }

        // PUT: api/Employeelists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeelist(int id, Employeelist employeelist)
        {
            if (id != employeelist.Eid)
            {
                return BadRequest();
            }

            _context.Entry(employeelist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeelistExists(id))
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

        // POST: api/Employeelists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employeelist>> PostEmployeelist(Employeelist employeelist)
        {
            _context.Employeelists.Add(employeelist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeelistExists(employeelist.Eid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployeelist", new { id = employeelist.Eid }, employeelist);
        }

        // DELETE: api/Employeelists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeelist(int id)
        {
            var employeelist = await _context.Employeelists.FindAsync(id);
            if (employeelist == null)
            {
                return NotFound();
            }

            _context.Employeelists.Remove(employeelist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeelistExists(int id)
        {
            return _context.Employeelists.Any(e => e.Eid == id);
        }
    }
}
