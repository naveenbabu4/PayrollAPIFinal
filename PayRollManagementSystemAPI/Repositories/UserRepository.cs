using Microsoft.EntityFrameworkCore;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;

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
    }
}
