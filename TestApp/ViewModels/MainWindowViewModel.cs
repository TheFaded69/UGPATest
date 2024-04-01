using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI;
using TestApp.Extensions;
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
    public ObservableCollection<PropertyViewModel> PropertyViewModels { get; set; } = [];
    
    public ExplorerItemViewModel SelectedExplorerItemViewModel
    {
        get => _selectedExplorerItemViewModel;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedExplorerItemViewModel, value);
            
            PropertyViewModels.Clear();

            foreach (var fieldInfo in value.GetType().GetProperties())
            {
                if (fieldInfo.GetCustomAttribute(typeof(PropertyNameAttribute)) != null)
                    PropertyViewModels.Add(new PropertyViewModel
                    {
                        PropertyName = (fieldInfo.GetCustomAttribute(typeof(PropertyNameAttribute)) as PropertyNameAttribute).PropertyName,
                        PropertyValue = fieldInfo.GetCustomAttribute(typeof(SizeAttribute)) != null 
                            ? Math.Round((float)fieldInfo.GetValue(value), 2) + " кБ"
                            : fieldInfo.GetValue(value),
                    });
            }
        }
    }

    public string Path
    {
        get => _path;
        set
        {
            this.RaiseAndSetIfChanged(ref _path, value);
            
            ExplorerItemViewModels.Clear();

            var directoryList = _explorerService.GetDirectories(value);
            foreach (var directory in directoryList)
            {
                var daughterDirectoryList = _explorerService.GetDirectories(value + '/' + directory.Name);
                var daughterFileList = _explorerService.GetFiles(value + '/' + directory.Name);
                var daughterList = new ObservableCollection<ExplorerItemViewModel>();
                
                foreach (var directoryInfo in daughterDirectoryList)
                {
                    daughterList.Add(new ExplorerItemViewModel()
                    {
                        Name = directoryInfo.Name,
                        Extension = directoryInfo.Extension,
                        ExplorerItemType = ExplorerItemType.Folder,
                    });
                }
                foreach (var fileInfo in daughterFileList)
                {
                    daughterList.Add(new ExplorerItemViewModel()
                    {
                        Name = fileInfo.Name,
                        Extension = fileInfo.Extension,
                        ExplorerItemType = ExplorerItemType.File,
                    });
                }
                
                ExplorerItemViewModels.Add(new ExplorerItemViewModel()
                {
                    Name = directory.Name,
                    Extension = directory.Extension,
                    ExplorerItemType = ExplorerItemType.Folder,
                    ExplorerItemViewModels = daughterList,
                });
            }
            
            var fileList = _explorerService.GetFiles(value);
            foreach (var fileInfo in fileList)
            {
                ExplorerItemViewModels.Add(new ExplorerItemViewModel()
                {
                    Name = fileInfo.Name.Split('.')[0],
                    Extension = fileInfo.Extension,
                    Size = (float)fileInfo.Length / 1024,
                    ExplorerItemType = ExplorerItemType.File
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