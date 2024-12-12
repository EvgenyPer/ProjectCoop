using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ULibrary.Data;
using ULibrary.Models;
using ULibrary.Services;
using ULibrary.ViewModels;

namespace ULibrary.Controllers;

/// <summary>
/// Контроллер для авторизации, регистрации и т.д.
/// </summary>
public class AccountController : Controller
{
    // Контекст бд.
    private readonly ULibraryDbContext _dbContext;

    // Менеджер управления процессами входа и выхода пользователей.
    private readonly SignInManager<User> _signInManager;

    // Менеджер управления пользователями в приложении.
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public AccountController(ULibraryDbContext dbContext, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    /// <summary>
    /// Действие для отображения страницы входа.
    /// </summary>
    [HttpGet]
    public IActionResult Login()
    {
        SyncDbService.SyncUsersToAspNetUsersAsync(_dbContext, _userManager).GetAwaiter().GetResult();
        return View();
    }

    /// <summary>
    /// Метод авторизации под видом пользователя (1) или кассира (2).
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        // Проверка на верные данные для авторизации.
        if (ModelState.IsValid)
        {
            // Аутентификация пользователя на основе введенных данных.
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            // Если аутентификация успешна.
            if (result.Succeeded)
            {
                // Получаем пользователя по имени.
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    // Проверяем роль пользователя.
                    if (user.RoleId == "1")
                        // Перенаправление на страницу Profile с одноименным контроллером.
                        return RedirectToAction("Profile", "Profile");
                    else if (user.RoleId == "2")
                        // Перенаправление на страницу WorkerProfile с одноименным контроллером.
                        return RedirectToAction("WorkerProfile", "WorkerProfile");
                    else if (user.RoleId == "3")
                        return Redirect("http://localhost:8080/main.html");
                }
            }
            else
            {
                // Если что-то не так, выводим ошибку.
                ModelState.AddModelError(string.Empty, "Некорректный логин или пароль");
            }
        }
        // По идее тут всегда будем переходить на экран с ошибкой.
        return View(model);
    }



    /// <summary>
    /// Действие для отображения страницы регистрации.
    /// </summary>
    [HttpGet]
    public IActionResult Register()
    {
        SyncDbService.SyncUsersToAspNetUsersAsync(_dbContext, _userManager).GetAwaiter().GetResult();
        return View();
    }

    /// <summary>
    /// Регистрация.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Создание нового пользователя.
            var user = new User
            {
                // Не совсем верно задаем id, но без этого код почему-то дает ошибку.
                Id = _dbContext.Users.Count() + 1.ToString(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                RoleId = "1",
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            // Если пользователь успешно добавлен.
            if (result.Succeeded)
            {
                // Параллельно добавляем нового юзера в таблицу users.
                _dbContext.SingleUsers.Add(new SingleUser
                {
                    Id = int.Parse(user.Id),
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    RoleId = user.RoleId
                });
                await _dbContext.SaveChangesAsync();

                // Входим в систему после регистрации.
                await _signInManager.SignInAsync(user, isPersistent: false);
                // Переход на страницу профиля с одноименным контроллером.
                return RedirectToAction("Profile", "Profile");
            }

            // Иначе выводим ошибку.
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description); // Если произошла ошибка, добавляем её в модель состояния
            }
        }

        // Если модель не корректна, возвращаем её для отображения отладочных ошибок.
        return View(model);
    }
}
