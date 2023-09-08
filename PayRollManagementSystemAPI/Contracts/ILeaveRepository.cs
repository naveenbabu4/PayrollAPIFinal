using PayRollManagementSystemAPI.Models;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface ILeaveRepository
    {
        Task<Leave> Create(string id,Leave leave);
        Task<List<Leave>> GetAllLeavesById(string id);
        Task<List<Leave>> GetAllLeavesByYearById(string id,DateTime startYear,DateTime endYear);
        Task<List<Leave>> GetAllLeavesByMonthById(string id, DateTime month);
        Task<List<Leave>> GetAllPendingLeaves();
        Task<List<Leave>> GetAllApprovedLeaves();
    }
}
