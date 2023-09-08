using PayRollManagementSystemAPI.Models;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface ILeaveRepository
    {
        Task<Leave> Create(int id,Leave leave);
        Task<List<Leave>> GetAllLeavesById(int id);
        Task<List<Leave>> GetAllLeavesByYearById(int id,DateTime startYear,DateTime endYear);
        Task<List<Leave>> GetAllLeavesByMonthById(int id, DateTime month);
        Task<List<Leave>> GetAllPendingLeaves();
        Task<List<Leave>> GetAllApprovedLeaves();
    }
}
