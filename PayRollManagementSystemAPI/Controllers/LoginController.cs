using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AccountUser> _userManager;
        private readonly IPasswordHasher<AccountUser> _passwordHasher;
        private readonly SignInManager<AccountUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public LoginController(UserManager<AccountUser> userManager,
            IPasswordHasher<AccountUser> passwordHasher, SignInManager<AccountUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
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
        [Route("LoginUser")]
        //Login user function to login the user
        public async Task<IActionResult> LoginUser(LoginViewModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (user == null || !result)
            {

                return BadRequest();
            }

            else
            {
                return Json(user);
            }

        }
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if(changePasswordViewModel != null)
            {
                AccountUser user = await _userManager.FindByIdAsync(changePasswordViewModel.Id);
                if (user != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                    if (changePasswordResult.Succeeded) 
                    {
                        return Json(user);
                    }
                    else
                    {
                        return Json(changePasswordResult.Errors);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
