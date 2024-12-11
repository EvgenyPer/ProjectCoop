using ULibrary.Models;

namespace ULibrary.ViewModels;

public class ProfileViewModel
{
    /// <summary>
    /// Логин пользователя.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; }

    public virtual Role Role { get; set; }

    public List<BookViewModel> UserBooks { get; set; }
}
