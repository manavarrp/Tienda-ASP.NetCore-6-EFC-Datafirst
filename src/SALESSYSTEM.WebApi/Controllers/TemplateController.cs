using Microsoft.AspNetCore.Mvc;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendPassword(string email, string password)
        {
            ViewData["Email"] = email;
            ViewData["Password"] = password;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";

            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Template", "SendPassword.cshtml")))
            {
                return Content("View not found.");
            }

            return View();
        }

        public IActionResult ResetPassword(string password)
        {
            ViewData["Password"] = password;
            return View();
        }
    }
}
