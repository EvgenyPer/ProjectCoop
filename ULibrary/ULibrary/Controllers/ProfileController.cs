using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.Models;
using ULibrary.Services;
using ULibrary.ViewModels;

namespace ULibrary.Controllers;

/// <summary>
/// Контроллер страницы пользователя.
/// </summary>
public class ProfileController : Controller
{
    // Менеджер управления пользователями в приложении.
    private readonly UserManager<User> _userManager;

    // Контекст бд.
    private readonly ULibraryDbContext _dbContext;

    // Менеджер управления процессами входа и выхода пользователей.
    private readonly SignInManager<User> _signInManager;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ProfileController(UserManager<User> userManager, ULibraryDbContext dbContext, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Метод загрузки данных для страницы пользователя.
    /// </summary>
    public async Task<IActionResult> Profile()
    {
        SyncDbService.SyncUsersToAspNetUsersAsync(_dbContext, _userManager).GetAwaiter().GetResult();

        // Получаем Id текущего авторизованного пользователя.
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);
        var userBooks = await _dbContext.UserBooks
            // Загружаем книги.
            .Include(ub => ub.Book)
            .Where(ub => ub.UserId == userId)
            .ToListAsync();

        // Все данные для вывода на странице.
        var model = new ProfileViewModel
        {
            UserName = user!.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            // Все брони.
            UserBooks = userBooks.Select(ub => new BookViewModel
            {
                ReceiptDate = ub.ReceiptDate,
                ReturnDate = ub.ReturnDate,
                Book = ub.Book,
                Penalty = ub.ReturnDate == default || DateTime.Now <= ub.ReturnDate
                    ? 0
                    : ub.Book.Penalty * (DateTime.Now - ub.ReturnDate).Days,
                ConfirmationCode = ub.ConfirmationCode,
            }).ToList()
        };

        return View(model);
    }

    /// <summary>
    /// Выйти из аккаунта.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}
