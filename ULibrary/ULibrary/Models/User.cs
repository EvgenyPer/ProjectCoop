using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ULibrary.Models;

/// <summary>
/// Пользователи.
/// </summary>
[Table("users")]
public class User : IdentityUser
{
    /// <summary>
    /// Id пользователя.
    /// </summary>
    [Required]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    [Required]
    [Column("login")]
    public override string UserName { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    [Required]
    [Column("first_name")]
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    [Required]
    [Column("last_name")]
    public string LastName { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required]
    [Column("password")]
    public string Password { get; set; }

    /// <summary>
    /// Id роли пользователя.
    /// </summary>
    [Required]
    [Column("role_id")]
    public int RoleId { get; set; }

    /// <summary>
    /// Роль пользователя.
    /// </summary>
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }

    /// <summary>
    /// Заявки пользователя.
    /// </summary>
    public virtual List<UserBook> UserBooks { get; set; }
}
