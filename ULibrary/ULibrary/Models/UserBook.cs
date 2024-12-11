using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ULibrary.Models;

/// <summary>
/// Заявка пользователя на получение книги.
/// </summary>
[PrimaryKey(nameof(ConfirmationCode))]
[Table("users_books")]
public class UserBook
{
    /// <summary>
    /// Id пользователя.
    /// </summary>
    [Required]
    [Column("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// Id книги
    /// </summary>
    [Required]
    [Column("book_id")]
    public int BookId { get; set; }

    /// <summary>
    /// Код подтверждения.
    /// </summary>
    [Required]
    [Column("confirmation_code")]
    public int ConfirmationCode { get; set; }

    /// <summary>
    /// Дата получения книги.
    /// </summary>
    [Required]
    [Column("receipt_date")]
    public DateTime ReceiptDate { get; set; }

    /// <summary>
    /// Дата получения книги.
    /// </summary>
    [Required]
    [Column("return_date")]
    public DateTime ReturnDate { get; set; }

    /// <summary>
    /// Пользователь заявки.
    /// Оператор virtual нужно для ленивой загрузки.
    /// </summary>
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    /// <summary>
    /// Книга заявки.
    /// Оператор virtual нужно для ленивой загрузки.
    /// </summary>
    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }
}
