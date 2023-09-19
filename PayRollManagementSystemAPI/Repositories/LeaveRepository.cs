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
        public async Task<Leave> Create(string id, LeaveViewModel leave)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            string obj = JsonConvert.SerializeObject(leave);
            Leave leave1 = JsonConvert.DeserializeObject<Leave>(obj);
            if (user != null)
            {
                leave1.User = user;
                leave1.LeaveStatus = "pending";
                TimeSpan NoOfDays = leave1.LeaveEndDate.AddDays(1) - leave1.LeaveStartDate;
                leave1.TotalNoOfDays = (int) NoOfDays.TotalDays;
                if (_db.Leave != null)
                {
                    await _db.Leave.AddAsync(leave1);
                    await _db.SaveChangesAsync();
                    return leave1;
                }
            }
            return null;

        }

        public async Task<List<DisplayLeaveModel>> GetAllApprovedLeaves()
        {

            List<Leave> leaves = await _db.Leave.Include(x => x.User).Where(x => x.LeaveStatus.ToLower() == "approved").ToListAsync();
            
            List<DisplayLeaveModel> models = new List<DisplayLeaveModel>();
            foreach(var leave in leaves)
            {
                string obj1 = JsonConvert.SerializeObject(leave);
                DisplayLeaveModel model =  JsonConvert.DeserializeObject<DisplayLeaveModel>(obj1);
                model.FirstName = leave.User.FirstName;
                model.LastName = leave.User.LastName;
                model.FullName = leave.User.FullName;
                model.Email = leave.User.Email;
                models.Add(model);
            }
            return models;
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
                && (u.LeaveStartDate.Year == year && u.LeaveEndDate.Year == year) && u.LeaveStatus.ToLower() == "approved")
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


        public async Task<List<DisplayLeaveModel>> GetAllPendingLeaves()
        {
            List<Leave> leaves = await _db.Leave.Include(x=>x.User).Where(x => x.LeaveStatus.ToLower() == "pending").ToListAsync();
            List<DisplayLeaveModel> models = new List<DisplayLeaveModel>();
            foreach (var leave in leaves)
            {
                string obj1 = JsonConvert.SerializeObject(leave);
                DisplayLeaveModel model = JsonConvert.DeserializeObject<DisplayLeaveModel>(obj1);
                model.FirstName = leave.User.FirstName;
                model.LastName = leave.User.LastName;
                model.FullName = leave.User.FullName;
                model.Email = leave.User.Email;
                models.Add(model);
            }
            return models;

        }
        public async Task<List<DisplayLeaveModel>> GetAllRejectedLeaves()
        {
            List<Leave> leaves =  await _db.Leave.Include(x => x.User).Where(x => x.LeaveStatus.ToLower() == "rejected").ToListAsync();
            List<DisplayLeaveModel> models = new List<DisplayLeaveModel>();
            foreach (var leave in leaves)
            {
                string obj1 = JsonConvert.SerializeObject(leave);
                DisplayLeaveModel model = JsonConvert.DeserializeObject<DisplayLeaveModel>(obj1);
                model.FirstName = leave.User.FirstName;
                model.LastName = leave.User.LastName;
                model.FullName = leave.User.FullName;
                model.Email = leave.User.Email;
                models.Add(model);
            }
            return models;

        }

        public async Task<Leave> UpdateLeaveStatus(int id)
        {
            Leave upLeave = await _db.Leave.FirstOrDefaultAsync(x => x.Id ==  id );
            if(upLeave != null)
            {
                upLeave.LeaveStatus = "approved";
                _db.Leave.Update(upLeave);
                await _db.SaveChangesAsync();
                return upLeave;
            }
            return null;
        }
        public async Task<Leave> UpdateRejectLeave(int id)
        {
            Leave upLeave = await _db.Leave.FirstOrDefaultAsync(x => x.Id == id);
            if (upLeave != null)
            {
                upLeave.LeaveStatus = "rejected";
                upLeave.Comments = "rejected";
                _db.Leave.Update(upLeave);
                await _db.SaveChangesAsync();
                return upLeave;
            }
            return null;
        }
    }
}
