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
                leave1.LeaveStatus = "pending";
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
            return await _db.Leave.Where(x => x.LeaveStatus.ToLower() == "approved").ToListAsync();
        }

        public async Task<List<Leave>> GetAllLeavesById(string id)
        {
            return await _db.Leave.Where(c => c.User.Id == id).ToListAsync();
        }

        public async Task<List<Leave>> GetAllLeavesByMonthById(string id, DateTime month)
        {
            var mon = month.Month;
            var year = month.Year;

            List<Leave> leaves = await _db.Leave
                .Where(u => u.User.Id == id && (u.LeaveStartDate.Month == mon && u.LeaveEndDate.Month == mon) 
                && (u.LeaveStartDate.Year == year && u.LeaveEndDate.Year == year))
                .ToListAsync();
            return leaves;

        }


        public async Task<List<Leave>> GetAllLeavesByYearById(string id, DateTime startYear, DateTime endYear)
        {
            var startyear = startYear.Year;
            var endyear = endYear.Year;
            

            List<Leave> leaves = await _db.Leave
                .Where(u => u.User.Id == id && ((u.LeaveStartDate.Year >= startyear  && u.LeaveEndDate.Year <= endyear)))
                .ToListAsync();

            return leaves;
        }


        public async Task<List<Leave>> GetAllPendingLeaves()
        {
            return await _db.Leave.Where(x => x.LeaveStatus.ToLower() == "pending").ToListAsync();

        }
        public async Task<List<Leave>> GetAllRejectedLeaves()
        {
            return await _db.Leave.Where(x => x.LeaveStatus.ToLower() == "rejected").ToListAsync();

        }

        public async Task<Leave> UpdateLeaveStatus(LeaveViewModel leave)
        {
            Leave upLeave = await _db.Leave.FirstOrDefaultAsync(x => x.Id ==  leave.LeaveId );
            if(upLeave != null)
            {
                upLeave.LeaveStatus = leave.LeaveStatus;
                upLeave.Comments = leave.Comments;
                _db.Leave.Update(upLeave);
                await _db.SaveChangesAsync();
                return upLeave;
            }
            return null;
        }
    }
}
