using System.Collections.Generic;
using System.IO;

namespace TestApp.Models.ExplorerModels;

public interface IExplorerService
{
    List<FileInfo>? GetFiles(string path);

    List<DirectoryInfo>? GetDirectories(string path);
}