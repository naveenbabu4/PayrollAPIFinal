using PayRollManagementSystemAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PayRollManagementSystemAPI.ViewModels
{
    public class LeaveViewModel
    {
        public string? UserId { get; set; }
        public int Id { get; set; }
        public string? LeaveType { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public TimeSpan TotalNoDays { get; set; }
        public string? LeaveStatus { get; set; }
        public string? Reason { get; set; }
        public string? Comments { get; set; }
    }
}
