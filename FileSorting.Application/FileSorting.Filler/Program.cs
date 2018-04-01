using System;
using FileSorting.Filler.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileSorting.Filler
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new[]
            {
                10000000.ToString(),
                100.ToString(),
                "bin/text.txt"
            };
#endif
            
            if (args == null || args.Length == 0 || args.Length < 3)
            {
                Console.WriteLine("Укажите количество строк, длину и имя файла");
                return;
            }

            var provider = SetupServiceProvider();

            var fileGenerator = provider.GetService<IFileGenerator>();
            
            fileGenerator.Generate(Convert.ToDouble(args[0]), Convert.ToInt32(args[1]), args[2]);
        }
        
        private static ServiceProvider SetupServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IStringGenerator, StringGenerator>()
                .AddSingleton<IFileGenerator, FileGenerator>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}