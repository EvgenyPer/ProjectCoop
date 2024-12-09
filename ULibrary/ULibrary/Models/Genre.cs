using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULibrary.Models;

/// <summary>
/// Жанр книги.
/// </summary>
[Table("genres")]
public class Genre
{
    /// <summary>
    /// Id жанра.
    /// </summary>
    [Required]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Название жанра.
    /// </summary>
    [Required]
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// Книги жанра.
    /// </summary>
    public List<Book> Books { get; set; }
}
