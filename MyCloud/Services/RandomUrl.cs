using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloud.Services
{
    public class RandomUrl : IRandomUrl
    {
        public string Generate()
        {
            string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";

            StringBuilder randomText = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < 11; i++)
            {
                randomText.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
            }
            return randomText.ToString();
        }
    }
}
