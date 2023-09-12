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



        public async Task<List<AllowanceAndDeduction>> GetAllAllowancesById(string id)
        {
            //// Assuming _db is your DbContext instance
            //var allowances = await _db.AllowanceAndDeduction
            //    .Where(ad => ad.User.Id == id) // Filter by user ID
            //    .ToListAsync();

            return null;
            //return allowances;
        }



        public async Task<List<AllowanceAndDeduction>> GetAllAllowancesNames()
        {
            var allowances = await _db.AllowanceAndDeduction.ToListAsync();
            return allowances;
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
            //var existingAllowance = await _db.AllowanceAndDeduction.FindAsync(allowanceAndDeduction.Id);

            //if (existingAllowance == null)
            //{

            //    return 0;
            //}

            //// Update the properties of the existing entity with the new values
            //existingAllowance.User = allowanceAndDeduction.User;
            //existingAllowance.AllowanceOrDeductionType = allowanceAndDeduction.AllowanceOrDeductionType;
            //existingAllowance.Amount = allowanceAndDeduction.Amount;

            //// Mark the entity as modified
            //_db.Entry(existingAllowance).State = EntityState.Modified;

            //await _db.SaveChangesAsync();

            return 0;
        }

    }


}



