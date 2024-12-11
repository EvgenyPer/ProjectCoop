using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.Models;
using ULibrary.ViewModels;

namespace ULibrary.Controllers;

/// <summary>
/// Контроллер для авторизации, регистрации и т.д.
/// </summary>
public class AccountController : Controller
{
    private readonly ULibraryDbContext _dbContext;
    private readonly SignInManager<User> _signInManager;
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
        return View();
    }

    /// <summary>
    /// Обработка входа.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Переносим пользователя на страницу профиля после успешного входа
                return RedirectToAction("Profile", "Profile");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Некорректный логин или пароль"); // Обработка ошибок
            }
        }

        return View(model); // Если ошибка, возвращаем обратно к форме входа
    }


    /// <summary>
    /// Действие для отображения страницы регистрации.
    /// </summary>
    [HttpGet]
    public IActionResult Register()
    {
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
            var user = new User
            {
                Id = _dbContext.Users.Count() + 1.ToString(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password, // Не забудьте хешировать пароль перед сохранением!
                RoleId = "1",
            };

            var result = await _userManager.CreateAsync(user, model.Password); // Создаём пользователя

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false); // Входим в систему после регистрации
                return RedirectToAction("Profile", "Profile"); // Переход на страницу профиля
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description); // Если произошла ошибка, добавляем её в модель состояния
            }
        }

        return View(model); // Если модель не корректна, возвращаем её для отображения отладочных ошибок
    }

}
