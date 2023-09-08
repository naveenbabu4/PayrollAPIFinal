﻿using PayRollManagementSystemAPI.Models;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface IAllowanceRepository
    {
        Task<AllowanceAndDeduction> CreateAllowance(AllowanceAndDeduction allowanceAndDeduction);
        Task<int> UpdateAllowance(AllowanceAndDeduction allowanceAndDeduction);
        Task<int> DeleteAllowance(string id);
        Task<List<AllowanceAndDeduction>> GetAllAllowancesNames();
        Task<List<AllowanceAndDeduction>> GetAllAllowancesById(string id);
        Task<List<AllowanceAndDeduction>> GetAllowancesByClassName(string className);

    }
}
