using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.Models;

namespace ULibrary
{
    public class Program
    {
        /// <summary>
        /// Пачальная точка запуска программы.
        /// </summary>
        public static void Main(string[] args)
        {
            // Создаем экземпляр строителя приложения, который используется для настройки сервисов.
            var builder = WebApplication.CreateBuilder(args);

            // Далее идет настройка сервисов.

            // Добавление поддержки контроллеров и представлений.
            builder.Services.AddControllersWithViews();
            // Настройка контекста бд с ее подключением через строку ULibraryDbConnectionString в файле конфигурации.
            builder.Services.AddDbContext<ULibraryDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ULibraryDbConnectionString")));

            // Настройка аутентификации и авторизации пользователей с использованием классов User и Role базы данных.
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ULibraryDbContext>()
                .AddDefaultTokenProviders();

            // Создание объекта приложения на основе настроек.
            var app = builder.Build();

            // Далее идет настройка конвейера обработки HTTP запросов.

            // Перенаправляет HTTP-запросы на HTTPS.
            app.UseHttpsRedirection();

            // Обеспечивает возможность серверной отправки статических файлов.
            app.UseStaticFiles();

            // Активирует маршрутизацию, что позволяет определять, какой контроллер обрабатывает какой запрос.
            app.UseRouting();

            // Добавляет middleware для аутентификации пользователей.
            app.UseAuthentication();
            
            // Добавляет middleware для авторизации пользователей.
            app.UseAuthorization();

            // Устанавливает маршрут по умолчанию для контроллеров и действий.
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Запускает приложение и начинает прослушивание входящих запросов.
            app.Run();
        }
    }
}

