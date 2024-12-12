using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULibrary.Models;

[Table("roles")]
public class SingleRole
{
    [Required]
    [Column("id")]
    public string Id { get; set; }

    /// <summary>
    /// Название роли.  
    /// </summary>
    [Required]
    [Column("name")]
    public string Name { get; set; } 
}
