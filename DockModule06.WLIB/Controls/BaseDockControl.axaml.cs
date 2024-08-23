using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Metadata;
using DockModule06.WLIB.Models;

namespace DockModule06.WLIB.Controls;

public partial class BaseDockControl : TemplatedControl
{
    public static readonly StyledProperty<IBaseDock?> LayoutProperty =
        AvaloniaProperty.Register<BaseDockControl, IBaseDock?>(nameof(Layout));

    public BaseDockControl()
    {
        // InitializeComponent();
    }

    [Content]
    public IBaseDock? Layout
    {
        get => GetValue(LayoutProperty);
        set => SetValue(LayoutProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        // Initialize
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        //Func<object?> func = null;
        //func = GetContext;
        //object? GetContext() => DataContext;
    }
}