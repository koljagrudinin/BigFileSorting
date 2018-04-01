using System;
using System.Diagnostics;
using System.IO;
using FileSorting.Domain.Interfaces;
using FileSorting.Domain.Sagas;
using FileSorting.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileSorting.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            #if DEBUG
            
            args = new[]
            {
                "bin/text.txt"
            };
            
            #endif
          
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Укажите имя файла");
                return;
            }

            var folderName = Path.Combine("bin", "temp");

            var provider = SetupServiceProvider();

            var saga = provider.GetService<IFileSorterSaga>();

            var timer = Stopwatch.StartNew();
            
            saga.SortFile(args[0], folderName, GetMemorySize());
            
            timer.Stop();

            Console.WriteLine(timer.Elapsed.TotalSeconds);
        }
        
        private static long GetMemorySize()
        {
            var result = GC.GetTotalMemory(false);

            if (result == 0)
            {
                throw new Exception("Возникла непредвиденная ошибка");
            }
            
            return result;
        }

        private static ServiceProvider SetupServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileSorterService, FileSorterService>()
                .AddSingleton<IFileSorterSaga, FileSorterSaga>()
                .AddSingleton<IFileSplitterService, FileSplitterService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}