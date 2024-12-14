using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.Models;

namespace ULibrary
{
    public class Program
    {
        /// <summary>
        /// ��������� ����� ������� ���������.
        /// </summary>
        public static void Main(string[] args)
        {
            // ������� ��������� ��������� ����������, ������� ������������ ��� ��������� ��������.
            var builder = WebApplication.CreateBuilder(args);

            // ����� ���� ��������� ��������.

            // ���������� ��������� ������������ � �������������.
            builder.Services.AddControllersWithViews();
            // ��������� ��������� �� � �� ������������ ����� ������ ULibraryDbConnectionString � ����� ������������.
            builder.Services.AddDbContext<ULibraryDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ULibraryDbConnectionString")));

            // ��������� �������������� � ����������� ������������� � �������������� ������� User � Role ���� ������.
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ULibraryDbContext>()
                .AddDefaultTokenProviders();

            // �������� ������� ���������� �� ������ ��������.
            var app = builder.Build();

            // ����� ���� ��������� ��������� ��������� HTTP ��������.

            // �������������� HTTP-������� �� HTTPS.
            app.UseHttpsRedirection();

            // ������������ ����������� ��������� �������� ����������� ������.
            app.UseStaticFiles();

            // ���������� �������������, ��� ��������� ����������, ����� ���������� ������������ ����� ������.
            app.UseRouting();

            // ��������� middleware ��� �������������� �������������.
            app.UseAuthentication();
            
            // ��������� middleware ��� ����������� �������������.
            app.UseAuthorization();

            // ������������� ������� �� ��������� ��� ������������ � ��������.
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // ��������� ���������� � �������� ������������� �������� ��������.
            app.Run();
        }
    }
}

