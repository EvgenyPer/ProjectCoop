using ULibrary.Models;

namespace ULibrary.ViewModels;
public class BookViewModel
{
    public Book Book { get; set; }

    /// <summary>
    /// Дата получения книги.
    /// </summary>
    public DateTime? ReceiptDate { get; set; }

    /// <summary>
    /// Дата получения книги.
    /// </summary>
    public DateTime? ReturnDate { get; set; }

    public int ConfirmationCode { get; set; }

    public decimal Penalty { get; set; }

    public User? CurrentUser { get; set; }
}
