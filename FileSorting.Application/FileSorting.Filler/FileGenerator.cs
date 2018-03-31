using System;
using System.IO;

namespace FileSorting.Filler
{
    public class FileGenerator
    {
        private readonly StringGenerator _stringGenerator;

        public FileGenerator()
        {
            _stringGenerator = new StringGenerator();
        }
        
        public void Generate(double stringNumber, int stringLength, string filePath)
        {
            if (File.Exists(filePath))
            {
                throw new Exception("Файл уже существует");
            }

            var rand = new Random();

            using (var streamWriter = new StreamWriter(filePath))
            {
                for (var i = 0; i < stringNumber; i++)
                {
                    streamWriter.WriteLine(_stringGenerator.GenerateString(stringLength));
                }
                
                streamWriter.Close();
            }
        }
    }
}