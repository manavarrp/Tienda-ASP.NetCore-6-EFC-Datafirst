using Microsoft.AspNetCore.Mvc;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
