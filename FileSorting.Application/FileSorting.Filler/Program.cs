using System;

namespace FileSorting.Filler
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new[]
            {
                100.ToString(),
                10.ToString(),
                "text.txt"
            };
#endif
            
            
            if (args == null || args.Length == 0 || args.Length < 3)
            {
                Console.WriteLine("Укажите количество строк, длину и имя файла");
                return;
            }

            new FileGenerator().Generate(Convert.ToDouble(args[0]), Convert.ToInt32(args[1]), args[2]);
        }
    }
}