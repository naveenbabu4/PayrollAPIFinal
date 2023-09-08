using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;

namespace PayRollManagementSystemAPI.Repositories
{
    public class AllowanceRepository : IAllowanceRepository
    {
        public Task<AllowanceAndDeduction> CreateAllowance(AllowanceAndDeduction allowanceAndDeduction)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAllowance(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AllowanceAndDeduction>> GetAllAllowancesById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AllowanceAndDeduction>> GetAllAllowancesNames()
        {
            throw new NotImplementedException();
        }

        public Task<List<AllowanceAndDeduction>> GetAllowancesByClassName(string className)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAllowance(AllowanceAndDeduction allowanceAndDeduction)
        {
            throw new NotImplementedException();
        }
    }
}
