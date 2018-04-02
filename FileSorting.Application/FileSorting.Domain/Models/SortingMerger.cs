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

                var key = GetUniqueKey(fileReader.GetCurrentValue());

                _readers.Add(key, fileReader);
            }
        }

        private string GetUniqueKey(string key)
        {
            var result = key;

            var i = 0;

            while (_readers.ContainsKey(result))
            {
                result = key + i++;
            }

            return result;
        }

        public string GetMin()
        {
            if (_readers.Count == 0)
            {
                return null;
            }

            var key = _readers.Keys.First();

            var minValue = _readers[key];

            var result = minValue.GetCurrentValue();
            
            _readers.Remove(key);

            minValue.GoToNextString();

            var nextValue = minValue.GetCurrentValue();

            if (nextValue == null)
            {
                Console.WriteLine($"Readers count {_readers.Count}");
            }
            else
            {
                _readers.Add(GetUniqueKey(nextValue), minValue);
            }

            return result;
        }
    }
}