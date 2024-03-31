using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestApp.Models.ExplorerModels;

public class ExplorerService : IExplorerService
{
    public ExplorerService(IPathValidator pathValidator)
    {
        _pathValidator = pathValidator;
    }
    
    private readonly IPathValidator _pathValidator;
    
    
    public List<FileInfo>? GetFiles(string path)
    {
        try
        {
            if (!_pathValidator.ValidatePath(path)) return null;

            return new DirectoryInfo(path)
                .GetFiles()
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
        
        
    }
}