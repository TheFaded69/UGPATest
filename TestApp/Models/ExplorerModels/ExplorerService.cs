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

            var files = new DirectoryInfo(path)
                .GetFiles();
            
            return files.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
        
        
    }

    public List<DirectoryInfo>? GetDirectories(string path)
    {
        try
        {
            if (!_pathValidator.ValidatePath(path)) return null;

            var folders = new DirectoryInfo(path)
                .GetDirectories();
            
            return folders.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}