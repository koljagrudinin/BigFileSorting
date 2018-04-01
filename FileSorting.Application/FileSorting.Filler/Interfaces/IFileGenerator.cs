namespace FileSorting.Filler
{
    public interface IFileGenerator
    {
        void Generate(double stringsCount, int stringsLength, string pathToFile);
    }
}