﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayRollManagementSystemAPI.Models
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Month { get; set; }
        [Required]
        public DateTime Year { get; set; }
        public AccountUser? User { get; set; }
        public AllowanceAndDeduction? allowanceAndDeduction { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal TotalAllowances { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal TotalDeductions { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal GrossSalary { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal NetSalary { get; set; }
    }
}
