using System;
using System.Collections.Generic;
using System.IO;
using FileSorting.Application.Models;

namespace FileSorting.Application.Services
{
    public class FileSplitterService
    {
        public SplitFileResult SplitFile(string fileName, string folderName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception($"Файл {fileName} не найден");
            }

            var memorySize = GetMemorySize();
            
            return SplitFile(fileName, folderName, memorySize);
        }

        private SplitFileResult SplitFile(string fileName, string folderName, long memorySize)
        {
            double fileSize = 0;
            
            var newFilePaths = new List<string>();

            string notAddedString = null;
            
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var sortedResult = ReadAndSortStrings(reader, memorySize, ref fileSize, notAddedString);

                    notAddedString = sortedResult.NotAddedString;
                    
                    var newFileName = WriteNewFile(sortedResult.Strings, folderName);

                    newFilePaths.Add(newFileName);
                }

                reader.Close();
            }
            
            return new SplitFileResult
            {
                FileSize = fileSize,
                NewFilePaths = newFilePaths
            };
        }

        private long GetMemorySize()
        {
            var result = GC.GetTotalMemory(false);

            if (result == 0)
            {
                throw new Exception("Возникла непредвиденная ошибка");
            }

#if DEBUG
            result = 2000;   
#endif
            
            return result;
        }

        class ReadAndSortResult
        {
           public  IEnumerable<string> Strings { get; set; }
            public string NotAddedString { get; set; }
        }
        
        private ReadAndSortResult ReadAndSortStrings(
            StreamReader reader, 
            long memorySize, 
            ref double fileSize,
            string notAddedString)
        {
            var newFileString = new SortedSet<string>();

            var returningFileArraySize = 0;

            if (!string.IsNullOrEmpty(notAddedString))
            {
                newFileString.Add(notAddedString);
                returningFileArraySize += notAddedString.Length;
            }
            

            while (!reader.EndOfStream)
            {
                var s = reader.ReadLine();

                if (s == null)
                {
                    break;
                }

                if (returningFileArraySize + s.Length > memorySize)
                {
                    return new ReadAndSortResult
                    {
                        Strings = newFileString,
                        NotAddedString = s
                    };
                }

                newFileString.Add(s);

                fileSize += s.Length;
                returningFileArraySize += s.Length;
            }

            return new ReadAndSortResult
            {
                Strings = newFileString,
                NotAddedString = null
            };;
        }

        private string WriteNewFile(IEnumerable<string> newFileStrings, string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            var newFileName = Path.Combine(folderName, Guid.NewGuid().ToString());

            File.WriteAllLines(newFileName, newFileStrings);

            return newFileName;
        }


        public void DeleteFolder(string folderName)
        {
            Directory.Delete(folderName, true);
        }
    }
}
