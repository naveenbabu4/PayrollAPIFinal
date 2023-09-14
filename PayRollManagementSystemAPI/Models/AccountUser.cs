

using Microsoft.AspNetCore.Identity;

namespace PayRollManagementSystemAPI.Models
{
    public class AccountUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public string? Position { get; set; }
        public AllowanceAndDeduction? Package { get; set;}
       // public Salary? Salary { get; set; }
    }
}
