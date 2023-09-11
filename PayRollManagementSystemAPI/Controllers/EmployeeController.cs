using Microsoft.AspNetCore.Mvc;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Controllers
{
    [Route("EmployeeController/")]
    public class EmployeeController : Controller
    {
        private readonly ILeaveRepository _leaveRepository;
        public EmployeeController(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Json("Working");
        }
        [HttpPost]
        [Route("ApplyLeave")]
        public async Task<IActionResult> AppplyLeave(string id,LeaveViewModel leaveViewModel)
        {
            if(id!=null && leaveViewModel !=null)
            {
                return Json(await _leaveRepository.Create(id, leaveViewModel));
            }
            return null;
        }
    }
}
