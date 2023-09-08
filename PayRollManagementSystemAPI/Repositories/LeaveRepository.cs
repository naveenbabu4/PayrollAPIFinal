using Microsoft.EntityFrameworkCore;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayRollManagementSystemAPI.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly PayRollManagementSystemDbContext _db;
        public LeaveRepository(PayRollManagementSystemDbContext db)
        {
            _db = db;
        }
        public async Task<Leave> Create(int id, Leave leave)
        {
           // First, you may want to validate the input or perform any necessary checks here.

            // Set the user Id for the leave object.
            leave.Id = id;

            // Add the leave object to the database context.
            _db.Leave.Add(leave);

            // Save the changes to the database.
            await _db.SaveChangesAsync();

            // Return the created leave object with any modifications or generated values.
            return leave;
            throw new NotImplementedException();

        }

        public async Task<List<Leave>> GetAllApprovedLeaves()
        {
            return await _db.Leave.ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<List<Leave>> GetAllLeavesById(int id)
        {
            return await _db.Leave.Where(c => c.Id == id).ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<List<Leave>> GetAllLeavesByMonthById(int id, DateTime month)
        {
            var startDate = new DateTime(month.Year, month.Month, month.Day);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var leaves = await _db.Leave
                .Where(u => u.Id == id && u.LeaveStartDate >= startDate && u.LeaveEndDate <= endDate)
                .ToListAsync();
            return leaves;

            throw new NotImplementedException();
        }


        public async Task<List<Leave>> GetAllLeavesByYearById(int id, DateTime startYear, DateTime endYear)
        {
            var startDate = new DateTime(startYear.Year, 1, 1); // Start of the year
            var endDate = new DateTime(endYear.Year, 12, 31);   // End of the year

            var leaves = await _db.Leave
                .Where(u => u.Id == id && u.LeaveStartDate >= startDate && u.LeaveEndDate <= endDate)
                .ToListAsync();

            return leaves;
        }

        public async Task<List<Leave>> GetAllPendingLeaves()
        {
            return await _db.Leave.ToListAsync();
            throw new NotImplementedException();
            
        }

       
    }
}
