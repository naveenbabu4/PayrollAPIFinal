using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace PayRollManagementSystemAPI.Models
{
    public class AllowanceAndDeduction
    {
        [Key]
        public int Id { get; set; }
        public string? ClassName { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,6)")]
        public decimal BasicSalary { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal HRAllowance { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal DAAllowance { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal TravelAllowance { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal MedicalAllowance { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal WashingAllowance { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal LeaveDeduction { get; set; }
    }
}
