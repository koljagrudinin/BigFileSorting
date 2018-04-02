using System;
using System.Collections.Generic;
using System.IO;
using FileSorting.Domain.Interfaces;
using FileSorting.Domain.Models;

namespace FileSorting.Domain.Services
{
    public class FileSplitterService: IFileSplitterService
    {
        public SplitFileResult SplitFile(long memorySize, string fileName, string folderName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception($"Файл {fileName} не найден");
            }
         
            double fileSize = 0;

            var newFilePaths = new List<string>();

            string notAddedString = null;

            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var sortedResult = ReadAndSortStrings(reader, memorySize, notAddedString);

                    notAddedString = sortedResult.NotAddedString;

                    fileSize += sortedResult.ReadedBytesNumber;
                    
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

        private ReadAndSortResult ReadAndSortStrings(
            StreamReader reader,
            long memorySize,
            string notAddedString)
        {
            long readedBytesNumber = 0;
            
            var newFileString = new SortedList<string, string>();

            if (!string.IsNullOrEmpty(notAddedString))
            {
                newFileString.Add(notAddedString, "");
                readedBytesNumber += notAddedString.Length;
            }

            while (!reader.EndOfStream)
            {
                var readedString = reader.ReadLine();

                if (readedString == null)
                {
                    break;
                }

                readedBytesNumber += readedString.Length;

                if (readedBytesNumber > memorySize)
                {
                    return new ReadAndSortResult
                    {
                        Strings = newFileString.Keys,
                        NotAddedString = readedString,
                        ReadedBytesNumber = readedBytesNumber
                    };
                }

                newFileString.Add(readedString, "");
            }

            return new ReadAndSortResult
            {
                Strings = newFileString.Keys,
                NotAddedString = null,
                ReadedBytesNumber = readedBytesNumber
            };
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
