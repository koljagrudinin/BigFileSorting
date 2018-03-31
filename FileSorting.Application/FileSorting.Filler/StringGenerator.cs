using System;
using System.Text;

namespace FileSorting.Filler
{
    public class StringGenerator
    {
        private readonly Random _rand = new Random();
        private readonly int _aNumber = (int) 'a';
        private readonly int _zNumber = (int) 'z';

        public string GenerateString(int length)
        {
            var result = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                result.Append((char) _rand.Next(_aNumber, _zNumber));
            }

            return result.ToString();
        }
    }
}