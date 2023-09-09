using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface IUserRepository
    {
        Task<AccountUser> CreateEmployee(AccountUser user);
        Task<string> UpdateEmployee(AccountUser user);
        Task<string> DeleteEmployee(string id);
        Task<AccountUser> CreateAdmin(AccountUser user);
        Task<string> UpdateAdmin(AccountUser user);
        Task<string> DeleteAdmin(string id);
        Task<List<AccountUser>> GetAllAdmins();
        Task<List<AccountUser>> GetAllEmployees();
        Task<AccountUser> GetAdminById(string id);
        Task<AccountUser>GetEmployeeById(string id);

        Task<List<SalaryViewModel>> GetSalaryById(string id);
        Task<List<SalaryViewModel>> GetSalaryByYearById(string id, DateTime year);
        Task<SalaryViewModel> GetSalaryByMonthById(string id, DateTime month);
    }
}
