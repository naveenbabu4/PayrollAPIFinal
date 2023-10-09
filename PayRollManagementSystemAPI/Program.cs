using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayRollManagementSystemAPI.Contracts;
using PayRollManagementSystemAPI.Models;
using PayRollManagementSystemAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PayRollManagementSystemDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("con"))
    );
builder.Services.AddTransient<DbContext,PayRollManagementSystemDbContext>();
builder.Services.AddIdentity<AccountUser, IdentityRole>()
    .AddEntityFrameworkStores<PayRollManagementSystemDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddTransient<IUserRepository,UserRepository>();
builder.Services.AddTransient<IAllowanceRepository,AllowanceRepository>();
builder.Services.AddTransient<ILeaveRepository, LeaveRepository>();
builder.Services.AddTransient<ISalaryRepository, SalaryRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("http://20.253.91.44")
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(policyName: "CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
