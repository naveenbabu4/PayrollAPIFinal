using PayRollManagementSystemAPI.Models;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface ISalaryRepository
    {
        Task<Salary> CreateSalary(int id,Salary salary);
        Task<Salary> GetSalaryById(int id);
        Task<Salary> GetSalaryByYearById(int id,DateTime startDtae,DateTime endDate);
        Task<Salary> GetSalaryByMonth(int id, DateTime month);

    }
}
