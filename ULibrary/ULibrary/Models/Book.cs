using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULibrary.Models;

/// <summary>
/// Книга.
/// </summary>
[Table("books")]
public class Book
{
    /// <summary>
    /// Id книги.
    /// </summary>
    [Required]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Название книги.
    /// </summary>
    [Required]
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// Id автора книги.
    /// </summary>
    [Required]
    [Column("author_id")]
    public int AuthorId { get; set; }

    /// <summary>
    /// Дата выпуска книги.
    /// </summary>
    [Required]
    [Column("release_date")]
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Описание книги.
    /// </summary>
    [Required]
    [Column("description")]
    public string Description { get; set; }

    /// <summary>
    /// Штраф за невозврат за день.
    /// </summary>
    [Required]
    [Column("penalty")]
    public decimal Penalty { get; set; }

    /// <summary>
    /// Количество экземпляров данной книги.
    /// </summary>
    [Required]
    [Column("count")]
    public int Count { get; set; }

    /// <summary>
    /// Id жанра книги.
    /// </summary>
    [Required]
    [Column("genre_id")]
    public int GenreId { get; set; }

    /// <summary>
    /// Автор книги.
    /// </summary>
    [Required]
    [ForeignKey("AuthorId")]
    public virtual Author Author { get; set; }

    /// <summary>
    /// Жанр книги.
    /// </summary>
    [Required]
    [ForeignKey("GenreId")]
    public virtual Genre Genre { get; set; }

    /// <summary>
    /// Заявки по книге.
    /// </summary>
    public virtual List<UserBook> UserBooks { get; set; }

    /// <summary>
    /// Путь до картинки книги.
    /// </summary>
    public string CoverImagePath => $"~/bookimages/{Id}.jpg";
}
