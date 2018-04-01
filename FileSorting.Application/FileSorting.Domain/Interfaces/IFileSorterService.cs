using FileSorting.Domain.Models;

namespace FileSorting.Domain.Interfaces
{
    public interface IFileSorterService
    {
        void MergeFilesToOne(string fileName, SplitFileResult splitFileResult);
    }
}