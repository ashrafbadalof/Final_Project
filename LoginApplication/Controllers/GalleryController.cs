using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
