using Microsoft.AspNetCore.Mvc;

namespace PayRollManagementSystemAPI.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
