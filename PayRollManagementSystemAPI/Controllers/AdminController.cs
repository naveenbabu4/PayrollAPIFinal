using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.NewFolder;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Controllers
{
    [Route("AdminController/")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAllowanceRepository _allowanceRepository;
        private readonly ISalaryRepository _salaryRepository;
        private readonly ILeaveRepository _leaveRepository;
        private readonly UserManager<AccountUser> _userManager;
        private readonly IPasswordHasher<AccountUser> _passwordHasher;
        private readonly SignInManager<AccountUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(IUserRepository userRepository,UserManager<AccountUser> userManager,
            IPasswordHasher<AccountUser> passwordHasher,SignInManager<AccountUser> signInManager,
            RoleManager<IdentityRole> roleManager,IAllowanceRepository allowanceRepository,
            ISalaryRepository salaryRepository,ILeaveRepository leaveRepository )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _allowanceRepository = allowanceRepository;
            _salaryRepository = salaryRepository;
            _leaveRepository = leaveRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Json("Working");
        }
        //Inserting Employee into the database
        [HttpPost]
        [Route("CreateEmployee")] 
        public async Task<JsonResult> CreateEmployee(UserViewModel employee,SalaryViewModel salaryViewModel)
        {
            if (!await _roleManager.RoleExistsAsync("employee"))
            {
                await _roleManager.CreateAsync(new IdentityRole("employee"));
                return Json(await CreateEmployeeFunc(employee,salaryViewModel));
            }
            else
            {
                return Json(await CreateEmployeeFunc(employee, salaryViewModel));
            }
        }
        //Inserting Employee Function which will be triggered when CreateEmployee Called
        private async Task<IActionResult> CreateEmployeeFunc(UserViewModel employee,SalaryViewModel salaryViewModel)
        {
            string obj = JsonConvert.SerializeObject(employee);
            AccountUser user = new AccountUser();
            user = JsonConvert.DeserializeObject<AccountUser>(obj);
            user.UserName = employee.Email.Split('@')[0];
            var result = await _userManager.CreateAsync(user, employee.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "employee");
                user = await _userManager.FindByEmailAsync(user.Email);
                return Json(user,await CreateSalary(user.Id,salaryViewModel));
            }
            else return null;
        }
        //Inserting Admin into the database
        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin(UserViewModel admin) 
        {

            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                return Json(await CreateAdminFunc(admin));
            }
            else
            {
                return Json(await CreateAdminFunc(admin));
            }

        }
        //Inserting Admin Function which will be triggered when CreateEmployee Called
        private async Task<IActionResult> CreateAdminFunc(UserViewModel admin)
        {
            string obj = JsonConvert.SerializeObject(admin);
            AccountUser user = new AccountUser();
            user = JsonConvert.DeserializeObject<AccountUser>(obj);
            user.UserName = admin.Email.Split('@')[0];
            var result = await _userManager.CreateAsync(user, admin.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "admin");
                return Json(user);
            }
            else return null;
        }
        
        //Add Class method is used to call when admin is creating a new class
        [HttpPost]
        [Route("AddClass")]
        public async Task<IActionResult> AddClass(AllowanceViewModel allowanceViewModel)
        {
            if (allowanceViewModel != null)
                return Json(await _allowanceRepository.CreateAllowance(allowanceViewModel));
            else
                return BadRequest();
        }
        //Create salary method is used for create a salary for particular employee
        private async Task<IActionResult> CreateSalary(string userId,SalaryViewModel salaryViewModel)
        {
            if (salaryViewModel != null)
                return Json(await _salaryRepository.CreateSalary(userId,salaryViewModel));
            else
                return BadRequest();
        }
        [HttpGet]
        [Route("GetAllPendingLeaves")]
        public async Task<IActionResult> GetAllPendingLeaves()
        {
            List<Leave> leaves = await _leaveRepository.GetAllPendingLeaves();
            return Json(leaves);
        }
        [HttpPost]
        [Route("ApproveLeave")]
        public async Task<IActionResult> ApproveLeave(LeaveViewModel leave)
        {
            if(leave != null)
            {
                leave.LeaveStatus = "approved";
                return Json(await _leaveRepository.UpdateLeaveStatus(leave));
            }
            return null;
        }
        [HttpPost]
        [Route("RejectLeave")]
        public async Task<IActionResult> RejectLeave(LeaveViewModel leave)
        {
            if (leave != null)
            {
                leave.LeaveStatus = "rejected";
                return Json(await _leaveRepository.UpdateLeaveStatus(leave));
            }
            return null;
        }
        [HttpPost]
        [Route("GenerateSalary")]
        public async Task<IActionResult> GenerateSalary(string id,SalaryViewModel salaryViewModel)
        {
            if(id!=null && salaryViewModel != null)
            {
                return Json(await _salaryRepository.GenerateSalary(id,salaryViewModel));
            }
            return BadRequest();
        }

    }
}
