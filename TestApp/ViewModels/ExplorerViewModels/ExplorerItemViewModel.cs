using System.Collections.ObjectModel;
using ReactiveUI;

namespace TestApp.ViewModels.ExplorerViewModels;

public class ExplorerItemViewModel : ViewModelBase
{
    private ExplorerItemType _explorerItemType;
    private string _name;
    private string _description;
    private float _size;
    private string _extension;

    public ExplorerItemType ExplorerItemType
    {
        get => _explorerItemType;
        set => this.RaiseAndSetIfChanged(ref _explorerItemType, value);
    }

    [PropertyName("Название")]
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    

    [PropertyName("Размер")]
    [Size]
    public float Size
    {
        get => _size;
        set => this.RaiseAndSetIfChanged(ref _size, value);
    }

    
    [PropertyName("Тип")]
    public string Extension
    {
        get => _extension;
        set => this.RaiseAndSetIfChanged(ref _extension, value);
    }

    public ObservableCollection<ExplorerItemViewModel> ExplorerItemViewModels { get; set; } 
}