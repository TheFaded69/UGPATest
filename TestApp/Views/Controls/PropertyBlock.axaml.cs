using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace TestApp.Views.Controls;

public class PropertyBlock : TemplatedControl
{
    public static readonly StyledProperty<string> NamePropProperty =
        AvaloniaProperty.Register<PropertyBlock, string>(nameof(NameProp));
    
    public string NameProp
    {
        get => GetValue(NamePropProperty); 
        set => SetValue(NamePropProperty, value);
    }
    
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<PropertyBlock, string>(nameof(Text));
    
    public string Text 
    {
        get => GetValue(TextProperty); 
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<int> NamePropWidthProperty =
        AvaloniaProperty.Register<PropertyBlock, int>(nameof(NamePropWidth));
    public int NamePropWidth
    {
        get => GetValue(NamePropWidthProperty); 
        set => SetValue(NamePropWidthProperty, value);
    }
}