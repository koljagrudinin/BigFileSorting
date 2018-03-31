using System;
using System.Linq;
using FileSorting.Application.Services;

namespace FileSorting.Application.Sagas
{
    public class FileSorterSaga
    {
        private FileSplitterService _fileSplitterService;
        private FileSorterService _fileSorterService;

        public FileSorterSaga()
        {
            _fileSplitterService = new FileSplitterService();
            _fileSorterService = new FileSorterService();
        }

        public void SortFile(string fileName, string tempFolderName)
        {
            var splitFileResult = _fileSplitterService.SplitFile(fileName, tempFolderName);

            Console.WriteLine(
                $"File splitted to {splitFileResult.NewFilePaths.Count()} files with file size {splitFileResult.FileSize}");
            
            _fileSorterService.MergeFilesToOne(fileName, splitFileResult);

            Console.WriteLine("Files merged, deleting temp files");
            
            _fileSplitterService.DeleteFolder(tempFolderName);
        }
    }
}