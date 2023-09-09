using Microsoft.AspNetCore.Mvc;

namespace PayRollManagementSystemAPI.Controllers
{
    [Route("EmployeeController/")]
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Json("Working");
        }
        [HttpPost]
        [Route("ApplyLeave")]
        public Task<IActionResult> AppplyLeave(string id)
        {

        }
    }
}
