namespace FileSorting.Domain.Interfaces
{
    public interface IFileSorterSaga
    {
        void SortFile(string pathToFile, string tempFolderName, long memorySize);
    }
}