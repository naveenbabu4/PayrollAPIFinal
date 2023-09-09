using Microsoft.EntityFrameworkCore;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;
using PayRollManagementSystemAPI.Repositories;

using System;
using Newtonsoft.Json;

namespace PayRollManagementSystemAPI.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        //Creating constructor and initializing db
        private PayRollManagementSystemDbContext _db;
        private readonly ILeaveRepository _leaveObjRepository;

        public SalaryRepository(PayRollManagementSystemDbContext db, ILeaveRepository leaveRepository)
        {
            _db = db;
            _leaveObjRepository = leaveRepository;
        }
        // Method to create a salary
        public async Task<Salary> CreateSalary(string id, SalaryViewModel salary)
        {
            string obj = JsonConvert.SerializeObject(salary);
            Salary salaryObj= JsonConvert.DeserializeObject<Salary>(obj);
            salaryObj.allowanceAndDeduction = JsonConvert.DeserializeObject<AllowanceAndDeduction>(obj);
            //Getting allowances and deduction based on particular class(A4,A5) from AllowanceAndDeduction table
            AllowanceAndDeduction allowDed = await _db.AllowanceAndDeduction.FirstOrDefaultAsync(x => x.ClassName == salaryObj.allowanceAndDeduction.ClassName);
            //Getting if any Leaves are there in particular month for that employee
           // List<Leave> noOfLeaves = await _leaveObjRepository.GetAllLeavesByMonthById(id, salaryObj.Month);
            if (allowDed != null && _db != null)
            {
                
                //Adding allowances of an employee to TotalAllowances attribute of Salary Table
                salaryObj.TotalAllowances = allowDed.DAAllowance + allowDed.HRAllowance + allowDed.WashingAllowance + allowDed.MedicalAllowance + allowDed.TravelAllowance;
                salaryObj.GrossSalary = allowDed.BasicSalary + salaryObj.TotalAllowances;
                salaryObj.TotalDeductions = (allowDed.BasicSalary * (decimal)0.12) + 200;//where 200 is PROFESSION TAX which is standard and pf is 12% of Basic Salary 
                //salary.allowanceAndDeduction = allowDed;
                //if (noOfLeaves.Count() > 0 || noOfLeaves == null) 
                //{
                //    var perEachDay = (salaryObj.GrossSalary / 30); // price to deduct for paid leaves for each day
                //    var totalLeaveDeduction = noOfLeaves.Count() * perEachDay;
                //    salaryObj.TotalDeductions += totalLeaveDeduction;   
                //}
                salaryObj.NetSalary = salaryObj.GrossSalary - salaryObj.TotalDeductions;
                salaryObj.allowanceAndDeduction = allowDed;
                await _db.Salary.AddAsync(salaryObj);
                await _db.SaveChangesAsync();
                return salaryObj;
            }            
            return null;
        }
       

    }
}
