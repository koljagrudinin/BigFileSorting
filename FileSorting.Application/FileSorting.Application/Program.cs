using System;
using FileSorting.Application.Sagas;

namespace FileSorting.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            
            #if DEBUG

            args = new[]
            {
                "text.txt"
            };
            
            #endif
          
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Укажите имя файла");
                return;
            }

            var folderName = "temp";

            new FileSorterSaga().SortFile(args[0], folderName);
        }
    }
}