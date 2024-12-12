using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ULibrary.Models;

[Table("users")]
[PrimaryKey(nameof(Id))]
public class SingleUser
{
    [Required]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    [Required]
    [Column("login")]
    public string UserName { get; set; }

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
    public string RoleId { get; set; }
}
