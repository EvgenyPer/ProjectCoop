using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.ViewModels;
using System.Security.Claims;
using ULibrary.Models;
using Microsoft.AspNetCore.Identity;

namespace ULibrary.Controllers;
/// <summary>
/// Контроллер для управления книгами.
/// </summary>
public class BookController : Controller
{
    private readonly ULibraryDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public BookController(ULibraryDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    /// <summary>
    /// Переход на страницу выбранной книги.
    /// </summary>
    public async Task<IActionResult> Book(int id)
    {
        var book = await _dbContext.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            // Вернуть 404, если книга не найдена.
            return NotFound();
        }

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser != null) currentUser.UserBooks = _dbContext.UserBooks.Where(ub => ub.UserId == currentUser.Id).ToList();

        var model = new BookViewModel
        {
            Book = book,
            CurrentUser = currentUser
        };

        // Передать книгу в представление.
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

    [HttpPost]
    public async Task<IActionResult> BookAsync(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);

        if (book == null || book.Count <= 0)
        {
            // Вернуть ошибку, если книга не найдена или не осталось экземпляров
            return NotFound();
        }

        // Получить идентификатор текущего пользователя
        var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Предполагается, что вы используете Identity

        if (userId == null)
        {
            // Если пользователь не авторизован, вернуть 401 (Unauthorized) или перенаправить на страницу входа
            return Unauthorized();
        }

        // Добавить запись о бронировании
        var userBook = new UserBook
        {
            UserId = userId,
            BookId = book.Id,
            ReceiptDate = default,
            ReturnDate = default,
            ConfirmationCode = new Random().Next(100000, 999999),
        };

        await _dbContext.UserBooks.AddAsync(userBook);

        // Декрементировать количество книг
        book.Count--;

        // Сохранить изменения в базе данных
        await _dbContext.SaveChangesAsync();

        // Перенаправить на страницу профиля
        return RedirectToAction("Profile", "Profile");
    }

}
