using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Controllers
{
    public class OurStuffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
