namespace PayRollManagementSystemAPI.ViewModels
{
    public class DisplayLeaveModel
    {
        public int Id { get; set; }
        public string? LeaveType { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public TimeSpan TotalNoDays { get; set; }
        public string? LeaveStatus { get; set; }
        public string? Reason { get; set; }
        public string? Comments { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }   
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
