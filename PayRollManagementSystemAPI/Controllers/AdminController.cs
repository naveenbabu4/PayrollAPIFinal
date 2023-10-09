using Microsoft.AspNetCore.Cors;
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
            return Json("Working after changes added");
        }
        //Inserting Employee into the database
        [HttpPost]
        [Route("CreateEmployee")] 
        public async Task<JsonResult> CreateEmployee([FromBody] EmployeeViewModel employee,int id)
        {
            if (!await _roleManager.RoleExistsAsync("employee"))
            {
                await _roleManager.CreateAsync(new IdentityRole("employee"));
                return Json(await CreateEmployeeFunc(employee,employee.AllowanceId));
            }
            else
            {
                return Json(await CreateEmployeeFunc(employee, employee.AllowanceId));
            }
        }
        //Inserting Employee Function which will be triggered when CreateEmployee Called
        private async Task<IActionResult> CreateEmployeeFunc(EmployeeViewModel employee,int id)
        {
            string obj = JsonConvert.SerializeObject(employee);
            AccountUser user = new AccountUser();
            user = JsonConvert.DeserializeObject<AccountUser>(obj);
            user.Package = await _allowanceRepository.GetAllowancesById(id);
            user.UserName = employee.Email.Split('@')[0];
            user.FullName = employee.FirstName +" "+ employee.LastName;
            var result = await _userManager.CreateAsync(user, employee.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "employee");
                user = await _userManager.FindByEmailAsync(user.Email);
                return Json(user);
            }
            else return null;
        }
        //Inserting Admin into the database
        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody]AdminViewModel admin) 
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
        private async Task<IActionResult> CreateAdminFunc(AdminViewModel admin)
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
        public async Task<IActionResult> AddClass([FromBody] AllowanceViewModel allowanceViewModel)
        {
            if (allowanceViewModel != null)
                return Json(await _allowanceRepository.CreateAllowance(allowanceViewModel));
            else
                return BadRequest();
        }

        // update class Method used when admin to update allowance and deduction
        [HttpPost]
        [Route("UpdateClass")]
        public async Task<IActionResult> UpdateClass([FromBody] AllowanceViewModel allowanceViewModel)
        {
            if (allowanceViewModel != null)
            {
                return Json(await _allowanceRepository.UpdateAllowance(allowanceViewModel));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetAllPendingLeaves")]
        public async Task<IActionResult> GetAllPendingLeaves()
        {
            List<DisplayLeaveModel> leaves = await _leaveRepository.GetAllPendingLeaves();
            return Json(leaves);
        }
        [HttpPost]
        [Route("ApproveLeave/{id}")]
        public async Task<IActionResult> ApproveLeave(int id)
        {
            if(id != null)
            {
                //leave.LeaveStatus = "approved";
                return Json(await _leaveRepository.UpdateLeaveStatus(id));
            }
            return null;
        }
        [HttpPost]
        [Route("RejectLeave/{id}")]
        public async Task<IActionResult> RejectLeave(int id)
        {
            if (id != null)
            {

                return Json(await _leaveRepository.UpdateRejectLeave(id));
            }
            return null;
        }
        [HttpGet]
        [Route("GenerateSalary/{id}")]
        public async Task<IActionResult> GenerateSalary(string id)
        {
            if(id!=null)
            {
                DateTime month = DateTime.Now;
                return Json(await _salaryRepository.GenerateSalary(id,month));
            }
            return BadRequest();
        }
        //Method to retrive all allowance and deductions
        [HttpGet]
        [Route("GetAllAllowances")]
        public async Task<IActionResult> GetAllAllowances()
        {
            List<AllowanceAndDeduction> allowDed = await _allowanceRepository.GetAllAllowances();
            return Json(allowDed);
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            List<AccountUser> employees = await _userRepository.GetAllEmployees();
            List<UserViewModel> employeeViewModels = new List<UserViewModel>();
            foreach (AccountUser user in employees)
            {
                string obj = JsonConvert.SerializeObject(user);
                UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(obj);
                employeeViewModels.Add(userViewModel);
            }
            return Json(employeeViewModels);
        }
        [HttpGet]
        [Route("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            List<AccountUser> admins = await _userRepository.GetAllAdmins();
            List<UserViewModel> adminViewModels = new List<UserViewModel>();
            foreach (AccountUser user in admins)
            {
                string obj = JsonConvert.SerializeObject(user);
                UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(obj);
                adminViewModels.Add(userViewModel);
            }
            return Json(adminViewModels);
        }
        [HttpGet]
        [Route("GetAdminById/{id}")]
        public async Task<IActionResult> GetAdminById(string id)
        {
            if (id != null)
            {
                AccountUser user = await _userRepository.GetAdminById(id);
                string obj = JsonConvert.SerializeObject(user);
                UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(obj);
                return Json(userViewModel);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetAllApprovedLeaves")]
        public async Task<IActionResult> GetAllApprovedLeaves()
        {
            List<DisplayLeaveModel> leaves = await _leaveRepository.GetAllApprovedLeaves();
            return Json(leaves);
        }
        [HttpGet]
        [Route("GetAllRejectedLeaves")]
        public async Task<IActionResult> GetAllRejectedLeaves()
        {
            List<DisplayLeaveModel> leaves = await _leaveRepository.GetAllRejectedLeaves();
            return Json(leaves);
        }
        [HttpPost]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody]UserViewModel employee)
        {
            if (employee != null) {
                AccountUser user = await _userManager.FindByIdAsync(employee.Id);
                if (user != null)
                {
                    user.FirstName = employee.FirstName;
                    user.LastName = employee.LastName;
                    user.Email = employee.Email;
                    user.Address = employee.Address;
                    user.PhoneNumber = employee.PhoneNumber;
                    user.Position = employee.Position;
                    user.JoiningDate = employee.JoiningDate; 
                    user.FullName = user.FirstName+ " "+user.LastName;
                    return Json(await _userRepository.UpdateEmployee(user));
                }
                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] UserViewModel employee)
        {
            if (employee != null)
            {
                AccountUser user = await _userManager.FindByIdAsync(employee.Id);
                if (user != null)
                {
                    user.FirstName = employee.FirstName;
                    user.LastName = employee.LastName;
                    user.Email = employee.Email;
                    user.Address = employee.Address;
                    user.PhoneNumber = employee.PhoneNumber;
                    user.Position = employee.Position;
                    user.JoiningDate = employee.JoiningDate;
                    user.FullName = user.FirstName + " " + user.LastName;
                    return Json(await _userRepository.UpdateAdmin(user));
                }
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
