using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using project_MVC.data;
using project_MVC.Repositories;
using project_MVC.Service;

namespace project_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentication/Login";
        options.AccessDeniedPath = "/Authentication/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(14);
    });
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<Project_context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            builder.Services.AddScoped<IProductReposatory, ProductReposatory>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IUserReposatory, UserReposatory>();
            builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<project_MVC.Models.User>, PasswordHasher<project_MVC.Models.User>>();
builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<ICategoryitemReposatory, CategoryitemReposatory>();
            builder.Services.AddScoped<ICategoryitemService, CategoryitemService>();

            var app = builder.Build();

// Seed admin user and role
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Project_context>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<project_MVC.Models.User>>();
    // Ensure admin role exists (simple string role in User)
    var adminEmail = "admin@example.com";
    if (!context.Users.Any(u => u.Email == adminEmail))
    {
        var adminUser = new project_MVC.Models.User
        {
            UserName = "admin",
            Email = adminEmail,
            Role = "Admin"
        };
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");
        context.Users.Add(adminUser);
        context.SaveChanges();
    }
}

// Continue with middleware configuration

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}