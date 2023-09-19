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
        private readonly ILeaveRepository _leaveRepository;

        public SalaryRepository(PayRollManagementSystemDbContext db, ILeaveRepository leaveRepository)
        {
            _db = db;
            _leaveRepository = leaveRepository;
        }
        // Method to create a salary
        //public async Task<Salary> CreateSalary(string id, SalaryViewModel salary)
        //{
        //    string obj = JsonConvert.SerializeObject(salary);
        //    Salary salaryObj= JsonConvert.DeserializeObject<Salary>(obj);
        //    salaryObj.allowanceAndDeduction = JsonConvert.DeserializeObject<AllowanceAndDeduction>(obj);
        //    AccountUser user = await  _db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        //    salaryObj.User = user;
        //    //Getting allowances and deduction based on particular class(A4,A5) from AllowanceAndDeduction table
        //    AllowanceAndDeduction allowDed = await _db.AllowanceAndDeduction.FirstOrDefaultAsync(x => x.ClassName == salaryObj.allowanceAndDeduction.ClassName);
        //    //Getting if any Leaves are there in particular month for that employee
        //   // List<Leave> noOfLeaves = await _leaveObjRepository.GetAllLeavesByMonthById(id, salaryObj.Month);
        //    if (allowDed != null && _db != null)
        //    {
                
        //        ////Adding allowances of an employee to TotalAllowances attribute of Salary Table
        //        //salaryObj.TotalAllowances = allowDed.WashingAllowance + allowDed.MedicalAllowance + allowDed.TravelAllowance;
        //        ////salaryObj.GrossSalary = allowDed.BasicSalary + salaryObj.TotalAllowances;
        //        //salaryObj.TotalDeductions = (allowDed.BasicSalary * (decimal)0.12) + 200;//where 200 is PROFESSION TAX which is standard and pf is 12% of Basic Salary 
        //        ////salary.allowanceAndDeduction = allowDed;
        //        ////if (noOfLeaves.Count() > 0 || noOfLeaves == null) 
        //        ////{
        //        ////    var perEachDay = (salaryObj.GrossSalary / 30); // price to deduct for paid leaves for each day
        //        ////    var totalLeaveDeduction = noOfLeaves.Count() * perEachDay;
        //        ////    salaryObj.TotalDeductions += totalLeaveDeduction;   
        //        ////}
        //        //salaryObj.NetSalary = salaryObj.GrossSalary - salaryObj.TotalDeductions;
        //        salaryObj.allowanceAndDeduction = allowDed;
        //        await _db.Salary.AddAsync(salaryObj);
        //        await _db.SaveChangesAsync();
        //        return salaryObj;
        //    }            
        //    return null;
        //}

        public async Task<SalaryViewModel> GenerateSalary(string id,DateTime month)
        {
            //string obj = JsonConvert.SerializeObject(salary);
           // Salary salaryObj = JsonConvert.DeserializeObject<Salary>(obj);
            //salaryObj.allowanceAndDeduction = JsonConvert.DeserializeObject<AllowanceAndDeduction>(obj);
            //AccountUser user = JsonConvert.DeserializeObject<AccountUser>(obj);
            AccountUser user = await _db.Users.Include(x=> x.Package).Where(x => x.Id == id).FirstOrDefaultAsync();
            //Getting allowances and deduction based on particular class(A4,A5) from AllowanceAndDeduction table
            //AllowanceAndDeduction allowDed = await _db.AllowanceAndDeduction.FirstOrDefaultAsync(x => x.ClassName == salaryObj.allowanceAndDeduction.ClassName);
            //Getting Salary by salary id
            //Salary sal = await _db.Salary.Include(x=> x.allowanceAndDeduction).FirstOrDefaultAsync(x => x.Id == salary.SalaryId);
            //sal.allowanceAndDeduction.DAAllowance = salaryObj.allowanceAndDeduction.DAAllowance;
            //sal.allowanceAndDeduction.HRAllowance = salaryObj.allowanceAndDeduction.HRAllowance;
            //Getting if any Leaves are there in particular month for that employee
            Salary sal = new Salary();
            List<Leave> noOfLeaves = await _leaveRepository.GetAllLeavesByMonthById(id,month);
            if (user != null && _db != null)
            {
                sal.User = user;//added this line
                sal.allowanceAndDeduction = user.Package;
                //Adding allowances of an employee to TotalAllowances attribute of Salary Table
                sal.TotalAllowances = user.Package.HRAllowance + user.Package.DAAllowance + user.Package.WashingAllowance+user.Package.TravelAllowance+user.Package.MedicalAllowance;
                sal.GrossSalary = user.Package.BasicSalary + sal.TotalAllowances;
                sal.TotalDeductions = (user.Package.BasicSalary * (decimal)0.12) + 200;//where 200 is PROFESSION TAX which is standard and pf is 12% of Basic Salary 
                //salary.allowanceAndDeduction = allowDed;
                //if (noOfLeaves.Count() > 0 || noOfLeaves == null)
                //{
                //    var perEachDay = (sal.GrossSalary / 30); // price to deduct for paid leaves for each day
                //    var totalLeaveDeduction = noOfLeaves.Count() * perEachDay;
                //    sal.TotalDeductions += totalLeaveDeduction;
                //}
                int totalLeaves = 0;
                foreach(Leave leave in noOfLeaves)
                {
                    totalLeaves += leave.TotalNoOfDays;
                }
                var perEachDayLeaveCost = (sal.GrossSalary / 30); // price to deduct for paid leaves for each day
                var totalLeaveDeduction = totalLeaves * perEachDayLeaveCost;
                sal.TotalDeductions += totalLeaveDeduction;
                sal.NetSalary = sal.GrossSalary - sal.TotalDeductions;
                sal.Month = month;
                sal.Year = month;
                _db.Salary.AddAsync(sal);
                await _db.SaveChangesAsync();
                string obj1 = JsonConvert.SerializeObject(sal);
                //SalaryViewModel viewModel = JsonConvert.DeserializeObject<SalaryViewModel>(obj1);
                //string obj2 = JsonConvert.SerializeObject(sal);
                SalaryViewModel viewModel = JsonConvert.DeserializeObject<SalaryViewModel>(obj1);
                viewModel.UserId = user.Id;
                viewModel.FirstName = user.FirstName;
                viewModel.LastName = user.LastName;
                viewModel.Email = user.Email;
                viewModel.PhoneNumber = user.PhoneNumber;
                viewModel.BasicSalary = user.Package.BasicSalary;
                viewModel.HRAllowance = user.Package.HRAllowance;
                viewModel.DAAllowance = user.Package.DAAllowance;
                viewModel.WashingAllowance = user.Package.WashingAllowance;
                viewModel.TravelAllowance = user.Package.TravelAllowance;
                viewModel.MedicalAllowance = user.Package.MedicalAllowance;
                viewModel.Month = month;
                viewModel.LeaveDeduction = totalLeaves;
                return viewModel;
            }
            return null;
        }
    }
}
