using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace TestApp.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
    }
    
    private void PathBox_OnKeyUp(object? sender, KeyEventArgs e)
    {

        if (e is { Key: Key.Return }) Focus();
    }
    
    private void FilesGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        var source = e.Source;
        if (source is TextBlock textBlockOfCell)
        {
            //todo Open text editor
        }
        else if (source is Grid gridOfCell)
        {
            
        }
    }
}