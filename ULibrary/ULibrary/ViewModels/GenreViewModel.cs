using ULibrary.Models;

namespace ULibrary.ViewModels;

public class GenreViewModel
{
    public Genre Genre { get; set; }

    public User? CurrentUser { get; set; }
}
