using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;
using System.Xml.Linq;

namespace PayRollManagementSystemAPI.Repositories
{
    public class AllowanceRepository : IAllowanceRepository
    {
        private readonly PayRollManagementSystemDbContext _db;
        public AllowanceRepository(PayRollManagementSystemDbContext db)
        {
            _db = db;
        }

        public async Task<AllowanceAndDeduction> CreateAllowance(AllowanceViewModel allowanceAndDeduction)
        {
            string obj = JsonConvert.SerializeObject(allowanceAndDeduction);
            AllowanceAndDeduction allowanceAndDeduction1 = JsonConvert.DeserializeObject<AllowanceAndDeduction>(obj);
            if (_db != null)
            {
                await _db.AllowanceAndDeduction.AddAsync(allowanceAndDeduction1);
                await _db.SaveChangesAsync();
            }
            return allowanceAndDeduction1;

        }



        public async Task<int> DeleteAllowance(string id)
        {
            var allowance = await _db.AllowanceAndDeduction.FindAsync(id);
            if (allowance != null)
            {
                _db.AllowanceAndDeduction.Remove(allowance);
                await _db.SaveChangesAsync();

                return allowance.Id;
            }

            return 0;


        }



        public async Task<AllowanceAndDeduction> GetAllowancesById(int id)
        {
            AllowanceAndDeduction allowDed = await _db.AllowanceAndDeduction.FirstOrDefaultAsync(x => x.Id == id);
            return allowDed;            
        }



        public async Task<List<AllowanceAndDeduction>> GetAllAllowances()
        {
            List<AllowanceAndDeduction> allowancesAndDed = await _db.AllowanceAndDeduction.ToListAsync();
            return allowancesAndDed;
        }

        public async Task<List<AllowanceAndDeduction>> GetAllowancesByClassName(string name)
        {
            //var allowances = await _db.AllowanceAndDeduction
            //.Where(ad => ad.User.FullName == name)
            //.ToListAsync();

            return  null;

        }


        public async Task<int> UpdateAllowance(AllowanceViewModel allowanceAndDeduction)
        {
           // string obj = JsonConvert.SerializeObject(allowanceAndDeduction);
            //AllowanceAndDeduction allowanceAnd= JsonConvert.DeserializeObject<AllowanceAndDeduction>(obj);
            AllowanceAndDeduction allow = await _db.AllowanceAndDeduction.FirstOrDefaultAsync(x => x.Id == allowanceAndDeduction.Id);
            //    salaryObj.allowanceAndDeduction = JsonConvert.DeserializeObject<AllowanceAndDeduction>(obj);
            if (allow != null)
            {
                string obj = JsonConvert.SerializeObject(allowanceAndDeduction);
                AllowanceAndDeduction allowanceAnd= JsonConvert.DeserializeObject<AllowanceAndDeduction>(obj);
                allow.DAAllowance = allowanceAnd.DAAllowance;
                allow.TravelAllowance =allowanceAnd.TravelAllowance;
                allow.HRAllowance = allowanceAnd.HRAllowance;
                allow.BasicSalary = allowanceAnd.BasicSalary;
                allow.MedicalAllowance = allowanceAnd.MedicalAllowance;
                allow.ClassName = allowanceAnd.ClassName;
                allow.LeaveDeduction = allowanceAnd.LeaveDeduction;
                allow.WashingAllowance= allowanceAnd.WashingAllowance;
                _db.AllowanceAndDeduction.Update(allow);
                await _db.SaveChangesAsync();
                return allowanceAnd.Id;
            }
            return 0;
        }

    }


}



