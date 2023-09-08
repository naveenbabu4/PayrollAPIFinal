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
        public async Task<Leave> Create(string id, Leave leave)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                leave.User = user;
                if (_db.Leave != null)
                {
                    await _db.Leave.AddAsync(leave);
                    await _db.SaveChangesAsync();
                    return leave;
                }
            }
            return null;

        }

        public async Task<List<Leave>> GetAllApprovedLeaves()
        {
            return await _db.Leave.ToListAsync();
        }

        public async Task<List<Leave>> GetAllLeavesById(string id)
        {
            return await _db.Leave.Where(c => c.User.Id == id).ToListAsync();
        }

        public async Task<List<Leave>> GetAllLeavesByMonthById(string id, DateTime month)
        {
            var startDate = new DateTime(month.Year, month.Month, month.Day);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var leaves = await _db.Leave
                .Where(u => u.User.Id == id && u.LeaveStartDate >= startDate && u.LeaveEndDate <= endDate)
                .ToListAsync();
            return leaves;

        }


        public async Task<List<Leave>> GetAllLeavesByYearById(string id, DateTime startYear, DateTime endYear)
        {
            var startDate = new DateTime(startYear.Year, 1, 1); // Start of the year
            var endDate = new DateTime(endYear.Year, 12, 31);   // End of the year

            var leaves = await _db.Leave
                .Where(u => u.User.Id == id && u.LeaveStartDate >= startDate && u.LeaveEndDate <= endDate)
                .ToListAsync();

            return leaves;
        }


        public async Task<List<Leave>> GetAllPendingLeaves()
        {
            return await _db.Leave.ToListAsync();

        }


    }
}
