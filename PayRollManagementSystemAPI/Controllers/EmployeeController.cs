using Microsoft.AspNetCore.Mvc;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.ViewModels;
using Newtonsoft.Json;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.NewFolder;
using Microsoft.AspNetCore.Cors;

namespace PayRollManagementSystemAPI.Controllers
{
    [Route("EmployeeController/")]
   
    public class EmployeeController : Controller
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IUserRepository _userRepository;
        public EmployeeController(ILeaveRepository leaveRepository, IUserRepository userRepository)
        {
            _leaveRepository = leaveRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Json("Working");
        }
        [HttpPost]
        [Route("ApplyLeave")]
        public async Task<IActionResult> AppplyLeave([FromBody] LeaveViewModel leaveViewModel)
        {
            if (leaveViewModel != null)
            {
                return Json(await _leaveRepository.Create(leaveViewModel.UserId, leaveViewModel));
            }
            return null;

        }
        [HttpGet]
        [Route("GetAllLeavesById/{id}")]
        public async Task<IActionResult> LeaveReport(string id)
        {
            if (id != null)
            {
                return Json(await _leaveRepository.GetAllLeavesById(id));
            }
            return null;
        }
        [HttpGet]
        [Route("GetAllLeavesByMonthById")]
        public async Task<IActionResult> LeaveReportByMonth(string id, DateTime date)
        {
            if (id != null)
            {
                return Json(await _leaveRepository.GetAllLeavesByMonthById(id, date));
            }
            return null;
        }
        [HttpGet]
        [Route("GetAllLeavesByYearById")]
        public async Task<IActionResult> LeaveReportByYear(string id, DateTime startYear, DateTime endYear)
        {
            if (id != null)
            {
                return Json(await _leaveRepository.GetAllLeavesByYearById(id, startYear, endYear));
            }
            return null;
        }
        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            if (id != null)
            {
                AccountUser user = await _userRepository.GetEmployeeById(id);
                string obj = JsonConvert.SerializeObject(user);
                UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(obj);
                return Json(userViewModel);
            }
            return BadRequest();
        }
    }
}
