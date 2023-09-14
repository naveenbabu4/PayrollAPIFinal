using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface IAllowanceRepository
    {
        Task<AllowanceAndDeduction> CreateAllowance(AllowanceViewModel allowanceAndDeduction);
        Task<int> UpdateAllowance(AllowanceViewModel allowanceAndDeduction);
        Task<int> DeleteAllowance(string id);
        Task<List<AllowanceAndDeduction>> GetAllAllowances();
        Task<AllowanceAndDeduction> GetAllowancesById(int id);
        Task<List<AllowanceAndDeduction>> GetAllowancesByClassName(string className);

    }
}
