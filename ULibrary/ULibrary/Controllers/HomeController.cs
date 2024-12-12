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
/// ���������� ������� ��������.
/// </summary>
public class HomeController : Controller
{
    // �������� ��.
    private readonly ULibraryDbContext _dbContext;

    // �������� ���������� �������������� � ����������.
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// �����������.
    /// </summary>
    public HomeController(ULibraryDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    /// <summary>
    /// ����� ������� �������� �� ����� �������.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        await SyncDbService.SyncUsersToAspNetUsersAsync(_dbContext, _userManager);
        // �������������� ������������.
        var user = await _userManager.GetUserAsync(User);

        // �������� ���� ������ �� ��.
        var genres = await _dbContext.Genres.ToListAsync();

        var model = new MainViewModel
        {
            CurrentUser = user,
            Genres = genres
        };

        // ������� �� ������� �������� � �������������� ������������� (���� ����) � �������.
        return View(model);
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

        // �������������� ������������.
        var currentUser = await _userManager.GetUserAsync(User);
        var model = new GenreViewModel
        {
            Genre = genre,
            CurrentUser = currentUser
        };

        // �������� ���� � �������������.
        return View(model); 
    }

    /// <summary>
    /// ������� �� �������� ��������������� ������������.
    /// </summary>
    public async Task<IActionResult> Profile()
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null)
        {
            // ���� ���� �� �����������, ��������� �� �������� �����������.
            return RedirectToAction("Login");
        }

        // ����� �� ������ �������� ������ ������������ � �������������
        return View(currentUser);
    }

}
