using System.Collections.Generic;

namespace FileSorting.Application.Models
{
    public class SplitFileResult
    {
        public IEnumerable<string> NewFilePaths { get; set; }
        
        public double FileSize { get; set; }
    }
}