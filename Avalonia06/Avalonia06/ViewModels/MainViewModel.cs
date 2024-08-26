using Avalonia06.ViewModels.Sub;
using DockModule06.WLIB.Models;
using ReactiveUI;

namespace Avalonia06.ViewModels;

public class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    private IBaseDock? _Layout;
    public IBaseDock? Layout
    {
        get => _Layout;
        set => this.RaiseAndSetIfChanged(ref _Layout, value);
    }

    public MainViewModel()
    {
        Layout = CreateSubLayout();
    }

    private IBaseDock CreateSubLayout()
    {
        var mediaView = new MediaViewModel()
        {
            Id = "SubDock.MediaViewModel--",
            IsUsed = true,
        };

        var timeView = new TimelineViewModel()
        {
            Id = "SubDock.TimelineViewModel--",
            IsUsed = true,
        };

        var playerView = new PlayerViewModel()
        {
            Id = "SubDock.PlayerViewModel--",
            IsUsed = true,
        };

        return new SubDock
        {
            Id = "SubDock",
            MediaLayout = mediaView,
            TimelineLayout = timeView,
            PlayerLayout = playerView,
        };
    }
}
