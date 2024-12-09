using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ULibrary.Models;

/// <summary>
/// Роль пользователя (обычный пользователь, работник, админ).
/// </summary>
public class Role : IdentityRole
{
    /// <summary>
    /// Id роли.
    /// </summary>
    [Required]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Название роли.  
    /// </summary>
    [Required]
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// Пользователи данной роли.
    /// </summary>
    public virtual List<User> Users { get; set; }
}
