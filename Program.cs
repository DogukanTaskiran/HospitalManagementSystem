using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login/";
        options.AccessDeniedPath = "/Account/AccessDenied/";
        options.LogoutPath = "/Account/Logout/";
        options.Cookie.HttpOnly = true; // 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // 20dk süresi var
        options.SlidingExpiration = true; // aktivite kontrol

    });


builder.Services.AddScoped<IAuthorizationHandler, CustomRoleAuthorizationHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PatientPolicy", policy => policy.Requirements.Add(new CustomRoleRequirement("Patient")));
    options.AddPolicy("DoctorPolicy", policy => policy.Requirements.Add(new CustomRoleRequirement("Doctor")));
    options.AddPolicy("NursePolicy", policy => policy.Requirements.Add(new CustomRoleRequirement("Nurse")));
    options.AddPolicy("AdminPolicy", policy => policy.Requirements.Add(new CustomRoleRequirement("Admin")));
    options.AddPolicy("ReceptionistPolicy", policy => policy.Requirements.Add(new CustomRoleRequirement("Receptionist")));
});

builder.Services.AddHttpContextAccessor(); // burda sıkıntı olabilir dokümantasyonda kullanmak performansta sıkıntı yaratabilir diyor
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
