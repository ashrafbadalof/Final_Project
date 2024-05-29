using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}