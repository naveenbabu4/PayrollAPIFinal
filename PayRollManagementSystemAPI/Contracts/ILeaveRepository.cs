using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface ILeaveRepository
    {
        Task<LeaveViewModel> Create(string id,LeaveViewModel leave);
        Task<List<Leave>> GetAllLeavesById(string id);
        Task<List<Leave>> GetAllLeavesByYearById(string id,DateTime startYear,DateTime endYear);
        Task<List<Leave>> GetAllLeavesByMonthById(string id, DateTime month);
        Task<List<Leave>> GetAllPendingLeaves();
        Task<List<Leave>> GetAllApprovedLeaves();
        Task<List<Leave>> GetAllRejectedLeaves();
        Task<Leave> UpdateLeaveStatus(LeaveViewModel leave);
    }
}
