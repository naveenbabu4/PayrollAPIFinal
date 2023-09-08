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
        private readonly UserManager<AccountUser> _userManager;
        private readonly IPasswordHasher<AccountUser> _passwordHasher;
        private readonly SignInManager<AccountUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(IUserRepository userRepository,UserManager<AccountUser> userManager,
            IPasswordHasher<AccountUser> passwordHasher,SignInManager<AccountUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Json("Working");
        }
        [HttpPost]
        [Route("CreateEmployee")] //Inserting Employee
        public async Task<JsonResult> CreateEmployee(UserViewModel employee)
        {
            if (!await _roleManager.RoleExistsAsync("employee"))
            {
                await _roleManager.CreateAsync(new IdentityRole("employee"));
                return Json(await CreateEmployeeFunc(employee));
            }
            else
            {
                return Json(await CreateEmployeeFunc(employee));
            }
        }
        //Inserting Employee Function
        private async Task<ActionResult> CreateEmployeeFunc(UserViewModel employee)
        {
            string obj = JsonConvert.SerializeObject(employee);
            AccountUser user = new AccountUser();
            user = JsonConvert.DeserializeObject<AccountUser>(obj);
            user.UserName = employee.Email.Split('@')[0];
            var result = await _userManager.CreateAsync(user, employee.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "employee");
                return Json(user);
                //return new
                //{
                //    token = JWTTokenGenerator(user, userModel.Role.Trim().ToLower()),
                //    userId = user.Id,
                //    role = userModel.Role.ToLower()
                //};
            }
            else return null;
        }
        [HttpPost]
        [Route("CreateAdmin")]
        //Inserting Admin
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
        //Inserting Admin Function
        private async Task<ActionResult> CreateAdminFunc(UserViewModel admin)
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
                //return new
                //{
                //    token = JWTTokenGenerator(user, userModel.Role.Trim().ToLower()),
                //    userId = user.Id,
                //    role = userModel.Role.ToLower()
                //};
            }
            else return null;
        }
        [HttpPost]
        [Route("LoginUser")]
        //Login user function
        public async Task<ActionResult> LoginUser(LoginViewModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (user == null || !result)
            {

                return null;
            }

            else
            {
                return Json(user);
            }

        }
    }
}
