using Microsoft.AspNetCore.Mvc;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class SalesHistory : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
