using LoginApplication.DAL;
using LoginApplication.Entities;
using LoginApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginApplication.Areas.AdminPanel.Controllers
{
    public class CourseController : AdminController
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CourseController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _dbContext.Courses.ToListAsync();
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> Create(CourseViewModel courseViewModel) /// hazirlasin alem deyecek bir birine // elasan dostum eyw reis
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.Courses.AnyAsync(x => x.Name.ToLower().Equals(courseViewModel.Name.ToLower()));

            if (isExist)
            {
                ModelState.AddModelError("Name", "The name already exists");
                return View();
            }

            if (courseViewModel.Image == null)
            {
                ModelState.AddModelError("Image", "Image input can not be empty ");
                return View();
            }

            string file = $"{Guid.NewGuid()}-{courseViewModel.Image.FileName}";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "course", file);


            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await courseViewModel.Image.CopyToAsync(stream);
            }

            Courses course = new()
            {
                Name = courseViewModel.Name,
                Description = courseViewModel.Description,
                ImageUrl = file,
                Price = courseViewModel.Price,
            };


            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var course = await _dbContext.Courses.FindAsync(id);

            if (course == null) return NotFound();

            _dbContext.Courses.Remove(course);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)//one min okay bayram burdasan
        {
            if (id == null) return NotFound();
            var course = await _dbContext.Courses.FindAsync(id);

            if (course == null) return NotFound();

            CourseViewModel courseViewModel = new()
            {
                Description = course.Description,
                Price = course.Price,
                Name = course.Name,
            };

            return View(courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CourseViewModel courseViewModel)  
        {
            if (id == null) return NotFound();

            var existCourse = await _dbContext.Courses.FindAsync(id);
            if (existCourse is null) return NotFound();

            if (courseViewModel.Image != null)
            {
                var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "course", existCourse.ImageUrl);

                if (System.IO.File.Exists(oldPath))
                { System.IO.File.Delete(oldPath); }

                string file = $"{Guid.NewGuid()}-{courseViewModel.Image.FileName}";
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "course", file);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await courseViewModel.Image.CopyToAsync(stream);
                }

                existCourse.ImageUrl = file;
            }

            existCourse.Name = courseViewModel.Name;
            existCourse.Price = courseViewModel.Price;
            existCourse.Description = courseViewModel.Description;


            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
