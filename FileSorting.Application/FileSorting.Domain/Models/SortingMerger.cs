using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSorting.Domain.Models
{
    public class SortingMerger
    {
        private readonly SortedDictionary<string, SortingFile> _readers;

        public SortingMerger(IEnumerable<string> paths)
        {
            _readers = new SortedDictionary<string, SortingFile>();

            foreach (var pathToFile in paths)
            {
                var fileReader = new SortingFile(pathToFile);

                fileReader.GoToNextString();

                _readers.Add(fileReader.GetCurrentValue(), fileReader);
            }
        }

        public string GetMin()
        {
            if (_readers.Count == 0)
            {
                return null;
            }

            var min = _readers[_readers.Keys.First()];

            var result = min.GetCurrentValue();
            
            _readers.Remove(result);

            min.GoToNextString();

            var nextValue = min.GetCurrentValue();

            if (nextValue == null)
            {
                Console.WriteLine($"Readers count {_readers.Count}");
            }
            else
            {
                _readers.Add(nextValue, min);
            }

            return result;
        }
    }
}