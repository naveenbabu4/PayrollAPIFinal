using Microsoft.EntityFrameworkCore;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        //Creating constructor and initializing db
        private PayRollManagementSystemDbContext _db;
        public UserRepository(PayRollManagementSystemDbContext db)
        {
            _db = db;
        }
        // Method to create admin
       
        public async Task<AccountUser> CreateAdmin(AccountUser user)
        {
            if (_db !=null)
            {
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user;
            }
            return null;
        }
        
        // Method To Create An Employee
        public async Task<AccountUser> CreateEmployee(AccountUser user)
        {
            if( _db !=null)
            {
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user;
            }
            return null;
        }        
        //Method to Delete an Admin
        public async Task<string> DeleteAdmin(string id)
        {
            if(_db != null)
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            return null;
        }
        //Method to delete an Employee
        public async Task<string> DeleteEmployee(string id)
        {
            if(_db != null)
            {
                var user = await _db.Users.FirstOrDefaultAsync(x=> x.Id == id);
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            return null;
        }
        // Method To Get All Admins
        public async Task<List<AccountUser>> GetAllAdmins()
        {
            if (_db != null)
            {
                return await (from user in _db.Users
                              join userrole in _db.UserRoles
                              on user.Id equals userrole.UserId
                              join role in _db.Roles on userrole.RoleId equals role.Id
                              where role.Name == "admin" select user).ToListAsync();                             
                              
            }
            return null;
        }
        // Method To Get All Employees
        public async Task<List<AccountUser>> GetAllEmployees()
        {
            if (_db != null)
            {
                return await (from user in _db.Users
                              join userrole in _db.UserRoles
                              on user.Id equals userrole.UserId
                              join role in _db.Roles on userrole.RoleId equals role.Id
                              where role.Name == "employee"
                              select user).ToListAsync();

            }
            return null;
        }
        // Method To Get An Admin By Id
        public async Task<AccountUser> GetAdminById(string id)
        {
            if (_db != null)
            {
                var user = (from u in _db.Users where u.Id == id select u).FirstOrDefault();
                return user;
            }
            return null;
        }
        // Method To Get An Employee By Id
        public async Task<AccountUser> GetEmployeeById(string id)
        {
            if (_db != null)
            {
                var user = (from u in _db.Users where u.Id == id select u).FirstOrDefault();
                return user;
            }
            return null;
        }
        //updating admin details
        public async Task<string> UpdateAdmin(AccountUser user)
        {
            
            if (user != null)
            {
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            return null;
        }
        // updating employee details
        public async Task<string> UpdateEmployee(AccountUser user)
        {
            if (user != null)
            {
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            return null;
        }

        // Method To Get Salary Of An Particular Employee By His Id
        public async Task<List<SalaryViewModel>> GetSalaryById(string id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null && _db != null)
            {
                List<SalaryViewModel> salaries = await (/*from u in _db.Users where u.Id == id */
                                                        from s in _db.Salary where s.User.Id == id
                                                        select new SalaryViewModel
                                                        {
                                                            FirstName = user.FirstName,
                                                            LastName = user.LastName,
                                                            Email = user.Email,
                                                            PhoneNumber = user.PhoneNumber,
                                                            Month = s.Month,
                                                            Year =s.Year,
                                                            BasicSalary = s.allowanceAndDeduction.BasicSalary,
                                                            TotalAllowances = s.TotalAllowances,
                                                            TotalDeductions = s.TotalDeductions,
                                                            GrossSalary = s.GrossSalary,
                                                            NetSalary = s.NetSalary

                                                        }).ToListAsync();

                return salaries;
            }
            return null;
        }
        //Method To Get Salary Of An Particular Employee By His Id By Month
        public async Task<SalaryViewModel> GetSalaryByMonthById(string id, DateTime month)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var mon = month.ToString("MMM");
            var year = month.ToString("yyyy");
            if (_db != null && user != null)
            {
                SalaryViewModel salary = await (from s in _db.Salary
                                                      where s.User.Id == id && s.Month.ToString("MMM") == mon
                                                      select new SalaryViewModel
                                                {
                                                    FirstName = user.FirstName,
                                                    LastName = user.LastName,
                                                    Email = user.Email,
                                                    PhoneNumber = user.PhoneNumber,
                                                    Month = s.Month,
                                                    Year = s.Year,
                                                    BasicSalary = s.allowanceAndDeduction.BasicSalary,
                                                    TotalAllowances = s.TotalAllowances,
                                                    TotalDeductions = s.TotalDeductions,
                                                    GrossSalary = s.GrossSalary,
                                                    NetSalary = s.NetSalary
                                                }).FirstOrDefaultAsync();
                return salary;
            }
            return null;
        }
        //Method To Get Salary Of An Particular Employee By His Id By Year
        public async Task<List<SalaryViewModel>> GetSalaryByYearById(string id, DateTime year)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var ye = year.ToString("yyyy");
            if (_db != null && user != null)
            {
                List<SalaryViewModel> salaries = await (from s in _db.Salary where s.User.Id == id && s.Year.ToString("YYYY") == ye                                                         
                                                        select new SalaryViewModel
                                                        {
                                                            FirstName = user.FirstName,
                                                            LastName = user.LastName,
                                                            Email = user.Email,
                                                            PhoneNumber = user.PhoneNumber,
                                                            Month = s.Month,
                                                            Year = s.Year,
                                                            BasicSalary = s.allowanceAndDeduction.BasicSalary,
                                                            TotalAllowances = s.TotalAllowances,
                                                            TotalDeductions = s.TotalDeductions,
                                                            GrossSalary = s.GrossSalary,
                                                            NetSalary = s.NetSalary
                                                        }).ToListAsync();
                return salaries;
            }
            return null;
        }
    }
}
