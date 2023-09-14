using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayRollManagementSystemAPI.ViewModels
{
    public class AllowanceViewModel
    {
        public int Id { get; set; }
        public string? ClassName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HRAllowance { get; set; }
        public decimal DAAllowance { get; set; }
        public decimal TravelAllowance { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal WashingAllowance { get; set; }
        public decimal LeaveDeduction { get; set; }
    }
}
