using LoginApplication.DAL;
using LoginApplication.Entities;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Courses> courses = await _context.Courses.ToListAsync();
            List<Employees> employees = await _context.Employees.ToListAsync();

            HomeViewModel homeViewModel = new()
            {
                Employees = employees,
                Courses = courses 
            };

            return View(homeViewModel); 
        }
    }
}
