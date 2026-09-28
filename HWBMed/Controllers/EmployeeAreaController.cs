using Microsoft.AspNetCore.Mvc;

namespace HWBMed.Controllers
{
    public class EmployeeAreaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
