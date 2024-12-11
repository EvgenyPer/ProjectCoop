using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.Models;

namespace ULibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ULibraryDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ULibraryDbConnectionString")));

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ULibraryDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Проверка доступа к бд.
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ULibraryDbContext>();

                try
                {
                    dbContext.Database.EnsureCreated();
                    Console.WriteLine("Успешное подключение к бд.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"НЕудачное подключение к бд. Ошибка: {ex.Message}");
                }
            }


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

        }
    }
}

