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
        [HttpGet]
        [Route("GetAllLeavesById")]
        public async Task<IActionResult> LeaveReport(string id) {
            if (id != null)
            {
                return Json(await _leaveRepository.GetAllLeavesById(id));
            }
            return null;
        }
        [HttpGet]
        [Route("GetAllLeavesByMonthById")]
        public async Task<IActionResult> LeaveReportByMonth(string id,DateTime date)
        {
            if (id != null)
            {
                return Json(await _leaveRepository.GetAllLeavesByMonthById(id,date));
            }
            return null;
        }
        [HttpGet]
        [Route("GetAllLeavesByYearById")]
        public async Task<IActionResult> LeaveReportByYear(string id, DateTime startYear,DateTime endYear)
        {
            if (id != null)
            {
                return Json(await _leaveRepository.GetAllLeavesByYearById(id, startYear,endYear));
            }
            return null;
        }
    }
}
