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
        public AccountUser? User { get; set; }
        public string? AllowanceOrDeductionType { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Amount { get; set; }
    }
}
