using System;
using System.IO;

namespace FileSorting.Application.Models
{
    public class SortingFile: IDisposable
    {
        private readonly StreamReader _fileReader;
        private string CurrentValue { get; set; }

        public SortingFile(string fileName)
        {
            _fileReader = new StreamReader(fileName);
        }

        public string GetCurrentValue()
        {
            return CurrentValue;
        }

        public void GoToNextString()
        {
            if (!_fileReader.EndOfStream)
            {
                CurrentValue = _fileReader.ReadLine();
                return;
            }

            CurrentValue = null;
        }

        public void Dispose()
        {
            _fileReader?.Close();
            _fileReader?.Dispose();
        }
    }
}