using Microsoft.AspNetCore.Mvc;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class BussinesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
