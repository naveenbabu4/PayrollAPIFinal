using System.ComponentModel.DataAnnotations;

namespace PayRollManagementSystemAPI.Models
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }
        public AccountUser? User { get; set; }
        [Required]
        public string? LeaveType { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public int TotalNoOfDays { get; set; }  
        public string? LeaveStatus { get; set; }
        public string? Reason { get; set; }
        public string? Comments { get; set; }
    }
}
