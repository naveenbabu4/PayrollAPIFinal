using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.ViewModels;
using System;
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
        public async Task<LeaveViewModel> Create(string id, LeaveViewModel leave)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            string obj = JsonConvert.SerializeObject(leave);
            Leave leave1 = JsonConvert.DeserializeObject<Leave>(obj);
            if (user != null)
            {
                leave1.User = user;
                TimeSpan NoOfDays = leave1.LeaveEndDate - leave1.LeaveStartDate;
                leave1.TotalNoOfDays = (int) NoOfDays.TotalDays;
                if (_db.Leave != null)
                {
                    await _db.Leave.AddAsync(leave1);
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
            var mon = month.ToString("MMM");
            var year = month.ToString("yyyy");

            var leaves = await _db.Leave
                .Where(u => u.User.Id == id && (u.LeaveStartDate.ToString("MMM") == mon && u.LeaveEndDate.ToString("MMM") == mon))
                .ToListAsync();
            return leaves;

        }


        public async Task<List<Leave>> GetAllLeavesByYearById(string id, DateTime startYear, DateTime endYear)
        {
            var startyear = startYear.ToString("yyyy");
            var endyear = endYear.ToString("yyyy");
            

            var leaves = await _db.Leave
                .Where(u => u.User.Id == id && ((u.LeaveStartDate.ToString("yyyy") == startyear && u.LeaveEndDate.ToString("yyyy") == endyear) || (u.LeaveStartDate.ToString("yyyy") == startyear && u.LeaveEndDate.ToString("yyyy")== endyear)))
                .ToListAsync();

            return leaves;
        }


        public async Task<List<Leave>> GetAllPendingLeaves()
        {
            return await _db.Leave.ToListAsync();

        }


    }
}
