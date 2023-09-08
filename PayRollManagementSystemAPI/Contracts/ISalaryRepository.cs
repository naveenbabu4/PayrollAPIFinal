using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface ISalaryRepository
    {
        Task<Salary> CreateSalary(string id, Salary salary);
        Task<List<SalaryViewModel>> GetSalaryById(string id);
        Task<List<SalaryViewModel>> GetSalaryByYearById(string id, DateTime year);
        Task<SalaryViewModel> GetSalaryByMonthById(string id, DateTime month);

    }
}
