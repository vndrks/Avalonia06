namespace DockModule06.WLIB.Models;

internal class NormalDock : IBaseDock
{
    public string Id { get; set; }
    public bool IsUsed { get; set; }

    private IBaseDock? _MediaLayout;
    public IBaseDock? MediaLayout
    {
        get => _MediaLayout;
        set => _MediaLayout = value;
    }

    private IBaseDock? _PlayerLayout;
    public IBaseDock? PlayerLayout
    {
        get => _PlayerLayout;
        set => _PlayerLayout = value;
    }

    private IBaseDock? _TimelineLayout;
    public IBaseDock? TimelineLayout
    {
        get => _TimelineLayout;
        set => _TimelineLayout = value;
    }

    private IBaseDock? _BottomLayout;
    public IBaseDock? BottomLayout
    {
        get => _BottomLayout;
        set => _BottomLayout = value;
    }

    private string? _TestContent;
    public string? TestContent
    {
        get => _TestContent;
        set => _TestContent = "SetTestContent";
    }
}
