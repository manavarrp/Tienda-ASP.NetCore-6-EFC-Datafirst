using Microsoft.AspNetCore.Mvc;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
