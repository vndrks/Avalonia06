using Avalonia.Media.Imaging;
using DockModule06.WLIB.Models;

namespace Avalonia06.ViewModels.Sub;

public partial class TimelineViewModel : ViewModelBase, IBaseDock
{
    public required string Id { get; set; }

    public bool IsUsed { get; set; }

    public Bitmap? ImageFromBinding { get; } = ImageHelper.LoadFromResource(new("avares://Avalonia06/Assets/avalonia-logo.ico"));
}
