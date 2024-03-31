using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI;
using TestApp.Models.ExplorerModels;
using TestApp.ViewModels.ExplorerViewModels;

namespace TestApp.ViewModels;

public class MainWindowViewModel : ViewModelBase, IDisposable
{
    public MainWindowViewModel(IExplorerService explorerService)
    {
        _explorerService = explorerService;
        
        OpenFolderCommand = ReactiveCommand.CreateFromTask<Window>(OpenFolderCommandHandler);
    }
    private readonly IExplorerService _explorerService;

    private ExplorerItemViewModel _selectedExplorerItemViewModel;
    private string _path;

    public ObservableCollection<ExplorerItemViewModel> ExplorerItemViewModels { get; set; } = [];

    public ExplorerItemViewModel SelectedExplorerItemViewModel
    {
        get => _selectedExplorerItemViewModel;
        set => this.RaiseAndSetIfChanged(ref _selectedExplorerItemViewModel, value);
    }

    public string Path
    {
        get => _path;
        set
        {
            this.RaiseAndSetIfChanged(ref _path, value);

            var fileList = _explorerService.GetFiles(value);

            ExplorerItemViewModels.Clear();
            foreach (var fileInfo in fileList)
            {
                ExplorerItemViewModels.Add(new ExplorerItemViewModel()
                {
                    Name = fileInfo.Name,
                    Extension = fileInfo.Extension,
                    Size = (float)fileInfo.Length / 1024,
                });
            }
        }
    }

    #region Commands

    public ICommand OpenFolderCommand { get; }
    private async Task OpenFolderCommandHandler(Window window)
    {
        var pathList = await TopLevel.GetTopLevel(window)
            ?.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
            {
                AllowMultiple = false,
            })!;
        
        if (pathList.Count == 0) return;

        var path = pathList.FirstOrDefault();

        Path = path == null ? "Путь не выбран" : path.Path.AbsolutePath;
    }
    
    #endregion
    
    #region Dispose

    public void Dispose()
    {
        // TODO release managed resources here
    }

    #endregion
}