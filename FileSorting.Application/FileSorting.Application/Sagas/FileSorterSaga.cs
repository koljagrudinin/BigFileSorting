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

            _fileSorterService.MergeFilesToOne(fileName, splitFileResult);

            _fileSplitterService.DeleteFolder(tempFolderName);
        }
    }
}