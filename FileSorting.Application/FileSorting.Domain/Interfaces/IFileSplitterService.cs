using FileSorting.Domain.Models;

namespace FileSorting.Domain.Interfaces
{
    public interface IFileSplitterService
    {
        SplitFileResult SplitFile(long memorySize, string fileName, string tempFolderName);
        
        void DeleteFolder(string tempFolderName);
    }
}