using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.Models;
using ULibrary.Services;
using ULibrary.ViewModels;

namespace ULibrary.Controllers;

/// <summary>
/// Контроллер для страницы работника.
/// </summary>
public class WorkerProfileController : Controller
{
    // Менеджер управления пользователями в приложении.
    private readonly UserManager<User> _userManager;

    // Контекст бд.
    private readonly ULibraryDbContext _dbContext;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkerProfileController(UserManager<User> userManager, ULibraryDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Загрузка страницы и данных для таблицы по строке ввода логина пользователя.
    /// Если в строке ничего не вводилось, то выводятся все брони.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> WorkerProfile(string username)
    {
        SyncDbService.SyncUsersToAspNetUsersAsync(_dbContext, _userManager).GetAwaiter().GetResult();

        // Авторизованный работник.
        var currentUser = await _userManager.GetUserAsync(User);

        // Все брони.
        var userBooks = _dbContext.UserBooks
            .Include(ub => ub.Book)
            .Include(ub => ub.User).ToList();

        // Если в поле ввода логина что-то введено, то грузим его брони.
        if (!string.IsNullOrWhiteSpace(username))
        {
            userBooks = userBooks.Where(ub => ub.User.UserName.Contains(username)).ToList();
        }

        // Данные для их вывода на экране пользователя.
        var model = new WorkerProrileViewModel
        {
            CurrentUser = currentUser!,
            UsersBooks = userBooks.Select(ub => new BookViewModel
            {
                User = ub.User,
                Book = ub.Book,
                Penalty = ub.ReturnDate == default || DateTime.Now <= ub.ReturnDate
                    ? 0
                    : ub.Book.Penalty * (DateTime.Now - ub.ReturnDate).Days,
                ConfirmationCode = ub.ConfirmationCode,
                BookStatus = ub.ReturnDate == default ? "Книга еще не выдана" : "Книга выдана"
            }).ToList(),
            SearchUserName = username // Сохраните введённое значение
        };
        return View("WorkerProfile", model);
    }

    /// <summary>
    /// Метод с выдачей или возвратом книги.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ProcessConfirmationCode(string confirmationCode)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var model = new WorkerProrileViewModel();

        // Поиск книги по ConfirmationCode.
        var userBook = await _dbContext.UserBooks
            .Include(ub => ub.Book)
            .Include(ub => ub.User)
            .FirstOrDefaultAsync(ub => ub.ConfirmationCode == int.Parse(confirmationCode));

        if (userBook != null)
        {
            // Если ReleaseDate == default, устанавливаем новую дату.
            if (userBook.ReturnDate == default)
            {
                userBook.ReceiptDate = DateTime.UtcNow;
                userBook.ReturnDate = DateTime.UtcNow.AddDays(21);
                _dbContext.UserBooks.Update(userBook);
                await _dbContext.SaveChangesAsync();

                // 
                model = new WorkerProrileViewModel
                {
                    CurrentUser = currentUser!,
                    UsersBooks = await _dbContext.UserBooks
                                .Include(ub => ub.Book)
                                .Include(ub => ub.User)
                                .Where(ub => ub.ConfirmationCode == int.Parse(confirmationCode))
                                .Select(ub => new BookViewModel
                                {
                                    User = ub.User,
                                    Book = ub.Book,
                                    Penalty = ub.ReturnDate == default || DateTime.UtcNow <= ub.ReturnDate
                                        ? 0
                                        : ub.Book.Penalty * (DateTime.UtcNow - ub.ReturnDate).Days,
                                    ConfirmationCode = ub.ConfirmationCode,
                                    BookStatus = "Выдать книгу"

                                }).ToListAsync(),
                };
            }
            else
            {
                model = new WorkerProrileViewModel
                {
                    CurrentUser = currentUser!,
                    UsersBooks = await _dbContext.UserBooks
                        .Include(ub => ub.Book)
                        .Include(ub => ub.User)
                        .Where(ub => ub.ConfirmationCode == int.Parse(confirmationCode))
                        .Select(ub => new BookViewModel
                        {
                            User = ub.User,
                            Book = ub.Book,
                            Penalty = ub.ReturnDate == default || DateTime.UtcNow <= ub.ReturnDate
                                ? 0
                                : ub.Book.Penalty * (DateTime.UtcNow - ub.ReturnDate).Days,
                            ConfirmationCode = ub.ConfirmationCode,
                            BookStatus = "Книга возвращена"

                        }).ToListAsync(),
                };

                // Удаляем книгу из БД
                _dbContext.UserBooks.Remove(userBook);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Возвращаем модель вместе с обновленной таблицей
        return View("WorkerProfile", model);
    }

}
