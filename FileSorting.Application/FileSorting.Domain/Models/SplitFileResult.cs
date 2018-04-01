using System.Collections.Generic;

namespace FileSorting.Domain.Models
{
    public class SplitFileResult
    {
        public IEnumerable<string> NewFilePaths { get; set; }
        
        public double FileSize { get; set; }
    }
}