using ReactiveUI;

namespace TestApp.ViewModels.ExplorerViewModels;

public class PropertyViewModel : ViewModelBase
{
    private string _propertyName;
    private object _propertyValue;

    public string PropertyName
    {
        get => _propertyName;
        set => this.RaiseAndSetIfChanged(ref  _propertyName , value);
    }

    public object PropertyValue
    {
        get => _propertyValue;
        set => this.RaiseAndSetIfChanged(ref  _propertyValue , value);
    }
}