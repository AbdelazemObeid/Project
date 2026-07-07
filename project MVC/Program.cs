using Microsoft.EntityFrameworkCore;
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
            builder.Services.AddDbContext<Project_context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            builder.Services.AddScoped<IProductReposatory, ProductReposatory>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IUserReposatory, UserReposatory>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ICategoryitemReposatory, CategoryitemReposatory>();
            builder.Services.AddScoped<ICategoryitemService, CategoryitemService>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<Ifavouritereposatory, favouritereposatory>();
            builder.Services.AddScoped<Ifavouriteservice, favouriteservice>();

            builder.Services.AddScoped<Icartreposatory, cartreposatory>();
            builder.Services.AddScoped<Icartservice, cartservice>();

            builder.Services.AddScoped<Icartitemreposatory, cartitemreposatory>();
            builder.Services.AddScoped<Icartitemservice, cartitemservice>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
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