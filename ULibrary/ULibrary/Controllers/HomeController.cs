using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ULibrary.Data;
using ULibrary.ViewModels;

namespace ULibrary.Controllers;

/// <summary>
/// ���������� ������� ��������.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ULibraryDbContext _dbContext;

    /// <summary>
    /// �����������.
    /// </summary>
    public HomeController(ILogger<HomeController> logger, ULibraryDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// ����� ������� �������� �� ����� �������.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        var genres = await _dbContext.Genres.ToListAsync();
        return View(genres);
    }

    /// <summary>
    /// ������� �� �������� �����������.
    /// </summary>
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// ������� �� �������� � ��������.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// ������� �� �������� ���������� �����.
    /// </summary>
    public async Task<IActionResult> Genre(int id)
    {
        var genre = await _dbContext.Genres
            .Include(g => g.Books)
            .ThenInclude(g => g.Author)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            // ������� 404, ���� ���� �� ������.
            return NotFound(); 
        }

        // �������� ���� � �������������.
        return View(genre); 
    }

}
