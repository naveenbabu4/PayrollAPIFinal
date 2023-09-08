using Microsoft.EntityFrameworkCore;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;
using System;

namespace PayRollManagementSystemAPI.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        //Creating constructor and initializing db
        private PayRollManagementSystemDbContext _db;
        public SalaryRepository(PayRollManagementSystemDbContext db)
        {
            _db = db;
        }
        // Method to create a salary
        public async Task<Salary> CreateSalary(string id, Salary salary)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                salary.User = user;
                if (_db.Salary != null)
                {
                    await _db.Salary.AddAsync(salary);
                    await _db.SaveChangesAsync();
                    return salary;
                }
            }
            return null;
        }
        // Method To Get Salary Of An Particular Employee By His Id
        public async Task<List<SalaryViewModel>> GetSalaryById(string id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null && _db != null)
            {
                List<SalaryViewModel> salaries = await (from u in _db.Users
                                                join s in _db.Salary on
                                                u.Id equals s.User.Id
                                                select new SalaryViewModel
                                                {
                                                    FirstName = u.FirstName,
                                                    LastName = u.LastName,
                                                    Email = u.Email,
                                                    PhoneNumber = u.PhoneNumber,
                                                    Month = s.Month,
                                                    Year = s.Year,
                                                    BasicSalary = s.BasicSalary,
                                                    TotalAllowances = s.TotalAllowances,
                                                    TotalDeductions = s.TotalDeductions,
                                                    GrossSalary = s.GrossSalary,
                                                    NetSalary = s.NetSalary

                                                }).ToListAsync();

                return salaries;
            }
            return null;
        }
        //Method To Get Salary Of An Particular Employee By His Id By Month
        public async Task<SalaryViewModel> GetSalaryByMonthById(string id, DateTime month)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var mon = month.ToString("MMM");
            var year = month.ToString("yyyy");
            if(_db != null && user != null)
            {
                SalaryViewModel salary = await (from u in _db.Users join s in _db.Salary on
                                                 u.Id equals s.User.Id where s.Month.ToString("MMM") == mon && 
                                                s.Year.ToString("yyyy") == year
                                                select new SalaryViewModel
                                                {
                                                    FirstName = u.FirstName,
                                                    LastName = u.LastName,
                                                    Email = u.Email,
                                                    PhoneNumber = u.PhoneNumber,
                                                    Month = s.Month,
                                                    Year = s.Year,
                                                    BasicSalary = s.BasicSalary,
                                                    TotalAllowances = s.TotalAllowances,
                                                    TotalDeductions = s.TotalDeductions,
                                                    GrossSalary = s.GrossSalary,
                                                    NetSalary = s.NetSalary
                                                }).FirstOrDefaultAsync();
                return salary;
            }
            return null;
        }
        //Method To Get Salary Of An Particular Employee By His Id By Year
        public async Task<List<SalaryViewModel>> GetSalaryByYearById(string id, DateTime year)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var ye = year.ToString("yyyy");
            if (_db != null && user != null)
            {
                List<SalaryViewModel> salaries = await (from u in _db.Users
                                                join s in _db.Salary on
                                                 u.Id equals s.User.Id
                                                where s.Year.ToString("yyyy") == ye
                                                select new SalaryViewModel
                                                {
                                                    FirstName = u.FirstName,
                                                    LastName = u.LastName,
                                                    Email = u.Email,
                                                    PhoneNumber = u.PhoneNumber,
                                                    Month = s.Month,
                                                    Year = s.Year,
                                                    BasicSalary = s.BasicSalary,
                                                    TotalAllowances = s.TotalAllowances,
                                                    TotalDeductions = s.TotalDeductions,
                                                    GrossSalary = s.GrossSalary,
                                                    NetSalary = s.NetSalary
                                                }).ToListAsync();
                return salaries;
            }
            return null;
        }
    }
}
