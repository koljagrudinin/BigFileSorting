using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSorting.Application.Models
{
    public class SortingMerger
    {
        private readonly Dictionary<SortingFile, string> _readers;

        public SortingMerger(IEnumerable<string> paths)
        {
            _readers = new Dictionary<SortingFile, string>();
            
            foreach (var pathToFile in paths)
            {
                var fileReader = new SortingFile(pathToFile);
                
                fileReader.GoToNextString();

                _readers.Add(fileReader, fileReader.GetCurrentValue());
            }
        }
        
        public string GetMin()
        {
            if (_readers.Count == 0)
            {
                return null;
            }
            
            var minKey = _readers.OrderBy(q => q.Value).First();

            var result = minKey.Value;
            
            minKey.Key.GoToNextString();

            var nextValue = minKey.Key.GetCurrentValue();

            if (nextValue == null)
            {
                _readers.Remove(minKey.Key);

                Console.WriteLine($"Readers count {_readers.Count}");
            }
            else
            {
                _readers[minKey.Key] = nextValue;
            }

            return result;
        }
    }
}