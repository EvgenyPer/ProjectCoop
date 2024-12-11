using System.Collections.Generic; // Не забудьте добавить для List
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ULibrary.Models;

/// <summary>
/// Роль пользователя (обычный пользователь, работник, админ).
/// </summary>
[Table("roles")]
public class Role : IdentityRole<string> // Изменено на string
{
    /// <summary>
    /// Название роли.  
    /// </summary>
    [Required]
    [Column("name")]
    public override string Name { get; set; } // Используется, чтобы переопределить Name

    /// <summary>
    /// Пользователи данной роли.
    /// </summary>
    public virtual List<User> Users { get; set; }
}