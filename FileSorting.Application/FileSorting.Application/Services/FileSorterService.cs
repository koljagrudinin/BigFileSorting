using System.IO;
using FileSorting.Application.Models;

namespace FileSorting.Application.Services
{
    public class FileSorterService
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