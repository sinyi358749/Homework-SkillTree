using Homework_SkillTree.Data;
using Homework_SkillTree.Models;
using Homework_SkillTree.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<DapperDbContext>();



// µù¥U Service
builder.Services.AddScoped<IBookKeepingService, BookKeepingService>();

builder.Services.AddTransient<AccountBookRepository>();

 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
