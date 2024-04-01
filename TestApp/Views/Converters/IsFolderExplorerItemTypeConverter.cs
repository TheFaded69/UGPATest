using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using TestApp.ViewModels.ExplorerViewModels;

namespace TestApp.Views.Converters;

public class IsFolderExplorerItemTypeConverter: MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ExplorerItemType explorerItemType)
            return explorerItemType == ExplorerItemType.Folder;
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}