using ULibrary.Models;

namespace ULibrary.ViewModels;

public class WorkerProrileViewModel
{
    public User CurrentUser { get; set; }

    public List<BookViewModel> UsersBooks { get; set; }

    public string SearchUserName { get; set; }

}
