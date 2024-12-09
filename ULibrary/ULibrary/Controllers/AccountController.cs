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
    public IActionResult Login(LoginViewModel model)
    {
        // TODO решить проблему авторизации.

        // Возврат на страницу юзера.
        return RedirectToAction("Profile", "Profile");
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
        // TODO Решить проблему авторизации.
        //if (ModelState.IsValid)
        //{
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Пароли не совпадают!");
                return View(model);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                RoleId = 1,
                Password = model.Password
            };

            // Используем UserManager для создания пользователя.
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Если есть необходимость добавить роли, можно это сделать здесь
                await _userManager.AddToRoleAsync(user, "User");

                // Авторизация пользователя.
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Profile", "Profile");
            }

            // Получение ошибок.
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        //}

        return View(model);
    }

}
