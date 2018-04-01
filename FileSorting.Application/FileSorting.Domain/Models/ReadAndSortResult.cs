using System.Collections.Generic;

namespace FileSorting.Domain.Models
{
    public class ReadAndSortResult
    {
        public IEnumerable<string> Strings { get; set; }
            
        public string NotAddedString { get; set; }
            
        public long ReadedBytesNumber { get; set; }
    }
}