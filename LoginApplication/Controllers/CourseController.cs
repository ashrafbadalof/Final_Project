using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace LoginApplication.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details ()
        {
            return View();
        }
    }
}
