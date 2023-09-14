using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface ISalaryRepository
    {
       // Task<Salary> CreateSalary(string id, SalaryViewModel salary); 
        Task<SalaryViewModel> GenerateSalary(string id, DateTime month);

    }
}
