using LoginApplication.DAL;
using LoginApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.Areas.AdminPanel.Controllers
{
    public class EmployeeController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public EmployeeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _dbContext.Employees.ToListAsync();
            return View(employees);
        }
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> Create(Employees employee) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.Employees.AnyAsync(x=>x.Name.ToLower().Equals(employee.Name.ToLower()));

            if (isExist)
            {
                ModelState.AddModelError("Name", "The name already exists");
                return View();
            }

            await _dbContext.Employees.AddAsync(employee);

            await _dbContext.SaveChangesAsync();
         
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null) return NotFound();
            
            _dbContext.Employees.Remove(employee);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id == null) return NotFound();

            var employee = await _dbContext.Employees.FindAsync(id);

            if(employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Employees employee) 
        {
            if (id == null) return NotFound();

            if(id != employee.Id) return BadRequest();

            var existEmployee = await _dbContext.Employees.FindAsync(id);

            var existName = await _dbContext.Employees.AnyAsync(x => x.Name.ToLower().Equals(employee.Name.ToLower()) && x.Id != id);

            if (existName)
            {
                ModelState.AddModelError("Name", "The category with this name is already exists");
                return View();
            }

            existEmployee.Name = employee.Name;
            existEmployee.Subject = employee.Subject;
            existEmployee.Description = employee.Description;
            existEmployee.ImageUrl = employee.ImageUrl;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
