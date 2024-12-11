using ULibrary.Models;

namespace ULibrary.ViewModels;

public class MainViewModel
{
    public User CurrentUser { get; set; }

    public List<Genre> Genres { get; set; }
}
