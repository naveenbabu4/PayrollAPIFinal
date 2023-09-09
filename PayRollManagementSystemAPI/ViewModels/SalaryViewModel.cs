using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PayRollManagementSystemAPI.Models;

namespace PayRollManagementSystemAPI.ViewModels
{
    public class SalaryViewModel
    {

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? Month { get; set; }

        public DateTime? Year { get; set; }

        public decimal? BasicSalary { get; set; }

        public decimal? TotalAllowances { get; set; }

        public decimal? TotalDeductions { get; set; }

        public decimal? GrossSalary { get; set; }

        public decimal? NetSalary { get; set; }
    }
}
