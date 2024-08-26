using DockModule06.WLIB.Models;

namespace Avalonia06.ViewModels.Sub;

public partial class PlayerViewModel : ViewModelBase, IBaseDock
{
    public string? Id { get; set; }

    public bool IsUsed { get; set; }
}
