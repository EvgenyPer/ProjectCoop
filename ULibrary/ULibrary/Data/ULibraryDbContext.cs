using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ULibrary.Models;

namespace ULibrary.Data;

/// <summary>
/// Контекст бд библиотеки.
/// </summary>
public class ULibraryDbContext : IdentityDbContext<User, Role, string>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ULibraryDbContext(DbContextOptions<ULibraryDbContext> options) : base(options)
    {
    }
    
    /// <summary>
    /// Авторы.
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Книги.
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Жанры.
    /// </summary>
    public DbSet<Genre> Genres { get; set; }

    /// <summary>
    /// Роли пользователей.
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Пользователи.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Заявки на получение книг пользователями.
    /// </summary>
    public DbSet<UserBook> UserBooks { get; set; }
}
