using Microsoft.AspNetCore.Mvc;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class SalesReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
