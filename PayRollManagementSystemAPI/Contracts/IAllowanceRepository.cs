using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface IAllowanceRepository
    {
        Task<AllowanceViewModel> CreateAllowance(AllowanceViewModel allowanceAndDeduction);
        Task<int> UpdateAllowance(AllowanceViewModel allowanceAndDeduction);
        Task<int> DeleteAllowance(string id);
        Task<List<AllowanceAndDeduction>> GetAllAllowancesNames();
        Task<List<AllowanceAndDeduction>> GetAllAllowancesById(string id);
        Task<List<AllowanceAndDeduction>> GetAllowancesByClassName(string className);

    }
}
