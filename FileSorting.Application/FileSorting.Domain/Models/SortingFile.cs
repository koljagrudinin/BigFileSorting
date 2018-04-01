using System;
using System.IO;

namespace FileSorting.Domain.Models
{
    public class SortingFile: IDisposable, IComparable<SortingFile>, IComparable
    {
        private readonly StreamReader _fileReader;
        
        private string _currentValue { get; set; }

        public SortingFile(string fileName)
        {
            _fileReader = new StreamReader(fileName);
        }

        public string GetCurrentValue()
        {
            return _currentValue;
        }

        public void GoToNextString()
        {
            if (!_fileReader.EndOfStream)
            {
                _currentValue = _fileReader.ReadLine();
                return;
            }
            
            _currentValue = null;
        }

        public void Dispose()
        {
            _fileReader?.Close();
            _fileReader?.Dispose();
        }

        public int CompareTo(SortingFile other)
        {
            return String.CompareOrdinal(GetCurrentValue(), other.GetCurrentValue());

        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as SortingFile);
        }
    }
}