using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;

namespace PayRollManagementSystemAPI.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        public Task<Salary> CreateSalary(int id, Salary salary)
        {
            throw new NotImplementedException();
        }

        public Task<Salary> GetSalaryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Salary> GetSalaryByMonth(int id, DateTime month)
        {
            throw new NotImplementedException();
        }

        public Task<Salary> GetSalaryByYearById(int id, DateTime startDtae, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
