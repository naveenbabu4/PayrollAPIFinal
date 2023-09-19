using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;

namespace PayRollManagementSystemAPI.Contracts
{
    public interface ILeaveRepository
    {
        Task<Leave> Create(string id,LeaveViewModel leave);
        Task<List<Leave>> GetAllLeavesById(string id);
        Task<List<Leave>> GetAllLeavesByYearById(string id,DateTime startYear,DateTime endYear);
        Task<List<Leave>> GetAllLeavesByMonthById(string id, DateTime month);
        Task<List<DisplayLeaveModel>> GetAllPendingLeaves();
        Task<List<DisplayLeaveModel>> GetAllApprovedLeaves();
        Task<List<DisplayLeaveModel>> GetAllRejectedLeaves();
        Task<Leave> UpdateLeaveStatus(int id);
        Task<Leave> UpdateRejectLeave(int id);
    }
}
