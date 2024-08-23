
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
}
