using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.Models;
using ULibrary.ViewModels;

namespace ULibrary.Controllers;
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ULibraryDbContext _dbContext;
    private readonly SignInManager<User> _signInManager;

    public ProfileController(UserManager<User> userManager, ULibraryDbContext dbContext, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Profile()
    {
        var userId = _userManager.GetUserId(User); // Получаем Id текущего авторизованного пользователя
        var user = await _userManager.FindByIdAsync(userId);
        var userBooks = await _dbContext.UserBooks
            .Include(ub => ub.Book) // Загружаем книги
            .Where(ub => ub.UserId == userId)
            .ToListAsync();

        var model = new ProfileViewModel
        {
            UserName = user!.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserBooks = userBooks.Select(ub => new BookViewModel // Создайте класс BookViewModel для передачи данных книги
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

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}
