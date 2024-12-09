using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULibrary.Models;

/// <summary>
/// Автор.
/// </summary>
[Table("authors")]
public class Author
{
    /// <summary>
    /// Id автора.
    /// </summary>
    [Required]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Имя автора.
    /// </summary>
    [Required]
    [Column("first_name")]
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия автора.
    /// </summary>
    [Required]
    [Column("last_name")]
    public string LastName { get; set; }

    /// <summary>
    /// Книги автора.
    /// </summary>
    public List<Book> Books { get; set; }
}
