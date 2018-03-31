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

            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var sortedString = ReadAndSortStrings(reader, memorySize, ref fileSize);

                    var newFileName = WriteNewFile(sortedString, folderName);

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

            return result;
        }

        private IEnumerable<string> ReadAndSortStrings(
            StreamReader reader, 
            long memorySize, 
            ref double fileSize)
        {
            var newFileString = new SortedSet<string>();
            
            var newFileSize = 0;

            while (!reader.EndOfStream && fileSize < memorySize && newFileSize < memorySize)
            {
                var s = reader.ReadLine();

                if (s == null)
                {
                    break;
                }

                newFileString.Add(s);
                
                fileSize += s.Length;
                newFileSize += s.Length;
            }

            return newFileString;
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
