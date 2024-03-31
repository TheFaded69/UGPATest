using System;

namespace TestApp.ViewModels.ExplorerViewModels;

public class PropertyNameAttribute(string propertyName) : Attribute
{
    public string PropertyName { get; } = propertyName;
}