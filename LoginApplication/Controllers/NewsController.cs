using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
