using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Areas.AdminPanel.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
