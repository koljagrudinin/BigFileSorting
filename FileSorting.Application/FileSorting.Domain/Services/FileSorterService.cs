using System.IO;
using FileSorting.Domain.Interfaces;
using FileSorting.Domain.Models;

namespace FileSorting.Domain.Services
{
    public class FileSorterService: IFileSorterService
    {
        public void MergeFilesToOne(string fileName, SplitFileResult splitResult)
        {
            var sortingFile = new SortingMerger(splitResult.NewFilePaths);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            
            using (var fileStream = new StreamWriter(fileName))
            {
                while (true)
                {
                    var nextValue = sortingFile.GetMin();
                    
                    if (nextValue == null)
                    {
                        break;
                    }

                    fileStream.WriteLine(nextValue);
                }
                
                fileStream.Close();
            }
        }
    }
}