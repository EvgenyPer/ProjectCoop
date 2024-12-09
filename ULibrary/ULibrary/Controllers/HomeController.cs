using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ULibrary.Data;
using ULibrary.ViewModels;

namespace ULibrary.Controllers;

/// <summary>
/// Контроллер главной страницы.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ULibraryDbContext _dbContext;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public HomeController(ILogger<HomeController> logger, ULibraryDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Вывод главной страницы со всеми жанрами.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        var genres = await _dbContext.Genres.ToListAsync();
        return View(genres);
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

        // Передать жанр в представление.
        return View(genre); 
    }

}
