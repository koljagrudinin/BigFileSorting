using System;
using System.Linq;
using FileSorting.Domain.Interfaces;

namespace FileSorting.Domain.Sagas
{
    public class FileSorterSaga: IFileSorterSaga
    {
        private readonly IFileSplitterService _fileSplitterService;
        private readonly IFileSorterService _fileSorterService;

        public FileSorterSaga(IFileSorterService fileSorterService, IFileSplitterService fileSplitterService)
        {
            _fileSorterService = fileSorterService;
            _fileSplitterService = fileSplitterService;
        }

        public void SortFile(string fileName, string tempFolderName, long memorySize)
        {
            var splitFileResult = _fileSplitterService.SplitFile(memorySize, fileName, tempFolderName);

            Console.WriteLine(
                $"File splitted to {splitFileResult.NewFilePaths.Count()} files with file size {splitFileResult.FileSize}");
            
            _fileSorterService.MergeFilesToOne(fileName, splitFileResult);

            Console.WriteLine("Files merged, deleting temp files");
            
            _fileSplitterService.DeleteFolder(tempFolderName);
        }
    }
}