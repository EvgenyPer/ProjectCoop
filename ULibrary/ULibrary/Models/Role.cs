using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ULibrary.Models;

/// <summary>
/// Роль пользователя (обычный пользователь, работник, админ).
/// </summary>
public class Role : IdentityRole<string> 
{
    /// <summary>
    /// Название роли.  
    /// </summary>
    [Required]
    [Column("name")]
    public override string Name { get; set; }

    /// <summary>
    /// Пользователи данной роли.
    /// </summary>
    public virtual List<User> Users { get; set; }
}