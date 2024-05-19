using Microsoft.AspNetCore.Mvc;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class NewSaleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
