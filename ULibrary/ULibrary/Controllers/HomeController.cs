using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ULibrary.Data;
using ULibrary.Models;
using ULibrary.Services;
using ULibrary.ViewModels;

namespace ULibrary.Controllers;

/// <summary>
/// Контроллер главной страницы.
/// </summary>
public class HomeController : Controller
{
    // Контекст бд.
    private readonly ULibraryDbContext _dbContext;

    // Менеджер управления пользователями в приложении.
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public HomeController(ULibraryDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    /// <summary>
    /// Вывод главной страницы со всеми жанрами.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        await SyncDbService.SyncUsersToAspNetUsersAsync(_dbContext, _userManager);
        // Авторизованный пользователь.
        var user = await _userManager.GetUserAsync(User);

        // Загрузка всех жанров из бд.
        var genres = await _dbContext.Genres.ToListAsync();

        var model = new MainViewModel
        {
            CurrentUser = user,
            Genres = genres
        };

        // Заходим на главную страницу с авторизованным пользователем (если есть) и жанрами.
        return View(model);
    }

    /// <summary>
    /// Переход на страницу авторизации.
    /// </summary>
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Переход на страницу с ошибками.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// Переход на страницу выбранного жанра.
    /// </summary>
    public async Task<IActionResult> Genre(int id)
    {
        var genre = await _dbContext.Genres
            .Include(g => g.Books)
            .ThenInclude(g => g.Author)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            // Вернуть 404, если жанр не найден.
            return NotFound();
        }

        // Авторизованный пользователь.
        var currentUser = await _userManager.GetUserAsync(User);
        var model = new GenreViewModel
        {
            Genre = genre,
            CurrentUser = currentUser
        };

        // Передать жанр в представление.
        return View(model); 
    }

    /// <summary>
    /// Переход на страницу авторизованного пользователя.
    /// </summary>
    public async Task<IActionResult> Profile()
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null)
        {
            // Если юзер не авторизован, переводим на страницу авторизации.
            return RedirectToAction("Login");
        }

        // Здесь вы можете передать модель пользователя в представление
        return View(currentUser);
    }

}
